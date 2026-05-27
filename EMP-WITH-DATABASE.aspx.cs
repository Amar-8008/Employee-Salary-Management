using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace EMPL_1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection con=new SqlConnection();



        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["slow"].ConnectionString
            );

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into [dbo].[Table] values(@eno,@ename,@add,@sal)";
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@eno", TextBox1.Text);
            cmd.Parameters.AddWithValue("@ename", TextBox2.Text);
            cmd.Parameters.AddWithValue("@add", TextBox3.Text);
            cmd.Parameters.AddWithValue("@sal", TextBox4.Text);

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            Disp_rec();

            cursor_rec();
        }

        private void cursor_rec()
        {
            TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
            TextBox3.Text = string.Empty;
            TextBox4.Text = string.Empty;
            TextBox1.Focus();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Disp_rec ();

        }

        private void Disp_rec()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from [dbo].[Table]";
            cmd.Connection = con;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            ListBox1.DataTextField = "Name";
            ListBox1.DataValueField = "Emp no.";
            ListBox1.DataSource = dr;
            ListBox1.DataBind();
            dr.Close();
            cmd.Dispose();
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from [dbo].[Table] where [Emp no.]=@eno ";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@eno", ListBox1.SelectedValue);
            SqlDataReader dr=cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                TextBox1.Text = dr["Emp no."].ToString();
                TextBox2.Text = dr["Name"].ToString();
                TextBox3.Text = dr["Address"].ToString() ;
                TextBox4.Text = dr["Salary"].ToString();
            }
            dr.Close ();
            cmd.Dispose();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "update [dbo].[Table] set Name=@en,Address=@add,Salary=@sal where [Emp no.]=@eno ";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@en", TextBox2.Text);
            cmd.Parameters.AddWithValue("@add", TextBox3.Text);
            cmd.Parameters.AddWithValue("@sal",TextBox4.Text);
            cmd.Parameters.AddWithValue("@eno", TextBox1.Text);
            cmd.ExecuteNonQuery();
            cmd.Dispose ();
            cursor_rec();
            Disp_rec();

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "delete from [dbo].[Table] where [Emp no.]=@eno";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@eno", TextBox1.Text);
            cmd.ExecuteNonQuery ();
            cmd.Dispose ();
            Disp_rec();
            cursor_rec();
        }
    }
}