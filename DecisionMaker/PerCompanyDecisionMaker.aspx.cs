using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace DecisionMaker
{
    public partial class PerCompanyDecisionMaker : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
           

        }

    

        //Connection String method
        public void DB_Connection()
        {
            string connectionstring;
            SqlConnection con;
            try
            {
                connectionstring = @"Data Source=PITLLTP097; Initial Catalog=Dashboard; User ID = sa; Password=password1!";

                con = new SqlConnection(connectionstring);
                con.Open();
                Response.Write("DB connection successfull!");
                con.Close();
            }
            catch (Exception e)
            {
                Response.Write("Error in setting up db connection:" + e.Message);
            }
        }

        public void Retrieve_Company_Number()
        {
            string connectionstring;
            SqlConnection con;
            try
            {
                connectionstring = @"Data Source=VITLWF05; Initial Catalog=Dashboard; User ID = sa; Password=password1!";

                con = new SqlConnection(connectionstring);
                con.Open();
                Response.Write("DB connection successfull!");
                SqlCommand command;
                SqlDataReader dr;
                string sql;

                sql = "Select * from DecisionMaker_StatutoryInformation where Company_Name=@companyname ";
                command = new SqlCommand(sql, con);
                command.Parameters.Add(new SqlParameter("@companyname", System.Data.SqlDbType.NVarChar, 100, "Company_Name"));
                command.Parameters["@companyname"].Value = txtCompanyName.Text;
                dr = command.ExecuteReader();
                if (dr.Read())
                {
                    lblNumber.Text = dr["Number"].ToString();
                    //lblCountry.Text = dr["Country"].ToString();
                    //lblClient.Text = dr["Decision_Maker"].ToString();
                    //lblIndustry.Text = dr["Industry"].ToString();
                    //lblTypeOfClient.Text = dr["Type_Client"].ToString();
                    //lblAcquisition.Text = dr["Acquisition_Method"].ToString();
                    //lblProduct.Text = dr["Product_Type"].ToString();
                }

                con.Close();
                
            }
            catch (Exception e)
            {
                Response.Write("The following error occured:" + e.Message);
            }
           
        }

        //Load data into gridview
        public void BindDataGrid()
        {
            string connectionstring;
            SqlConnection con;
            try
            {
                connectionstring = @"Data Source=VITLWF05; Initial Catalog=Dashboard; User ID = sa; Password=password1!";

                con = new SqlConnection(connectionstring);
                con.Open();
                Response.Write("DB connection successfull!");
                SqlCommand command;
                SqlDataReader dr;
                string sql;

                sql = "Select * from DecisionMaker_StatutoryInformation where Number=@num ";
                command = new SqlCommand(sql, con);
                command.Parameters.Add(new SqlParameter("@num", System.Data.SqlDbType.NVarChar, 100, "Number"));
                command.Parameters["@num"].Value = lblNumber.Text;
                dr = command.ExecuteReader();
                gdvDecisionMaker.DataSource = dr;
                gdvDecisionMaker.DataBind();
                con.Close();
                
            }
            catch (Exception e)
            {
                Response.Write("Couldnot load gridview:" + e.Message);
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //Search if company name exist in decision maker/statutory information tb and display number in label
            Retrieve_Company_Number();
            //load gridview
            if (IsPostBack)
            {
                BindDataGrid();
                lblNumber.Text = "";
                if (gdvDecisionMaker.Rows.Count == 0)
                {
                    Response.Write("<script>alert('No records were found! You will not be able to proceed further.');</script>");
                    //disable button here
                }
            }
           
        }

       

        //public static void Main(string[] args)
        //{

        //}
        





    }


}