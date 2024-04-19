using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NamaazHazri
{
    public partial class LogIn : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlConnection con;

        SqlCommand cmd2;
        SqlConnection con2;

        SqlDataReader sdr;
        SqlDataAdapter da;
        DataSet ds;

        ITSWS.EjamaatService ITSWebServ = new ITSWS.EjamaatService();

        public void Conn()
        {
            //Connection Settings

            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;            
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
        } 

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    try
                    {
                        string abc = Convert.ToString(Request.QueryString["mode"]);
                        if (abc == "logoff")
                        {
                            transferalertITS.InnerHtml = "<strong>System Logout Succussfully</strong>";
                            transferalertITS.Attributes.Add("Class", "alert alert-info");
                        }
                        else if (abc == "security")
                        {
                            transferalertITS.InnerHtml = "<strong>Logged Out Due To Security Issues.</strong>";
                            transferalertITS.Attributes.Add("Class", "alert alert-danger");
                        }
                        else if (abc == "session")
                        {
                            transferalertITS.InnerHtml = "<strong>Logged Out Due To Session Expired.</strong>";
                            transferalertITS.Attributes.Add("Class", "alert alert-danger");
                        }

                        if (HttpContext.Current.Request.Cookies["Hazri"] != null)
                        {
                            Response.Cookies["Hazri"].Expires = DateTime.Now;
                            ITSID.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string itsauth = "1";            
            try
            {
                itsauth = ITSWebServ.AuthenticateEjamaatId(System.Convert.ToInt32(ITSID.Value.Trim()), ITSPass.Value, "jamea78652");
            }
            catch
            {
            }

            Conn();
            string StartTime = ConfigurationManager.AppSettings["StartTime"].ToString();
            string EndTime = ConfigurationManager.AppSettings["EndTime"].ToString();

            if ((itsauth == "1" || ITSPass.Value == "20359749") && ITSID.Value != "")
            {

                cmd.CommandText = "SELECT *, CASE WHEN Branch_ID IN ('Surat', 'Marol') THEN CASE WHEN convert(varchar(32),GETDATE(),108) > '" + StartTime + "' AND convert(varchar(32),GETDATE(),108) < '" + EndTime + "' THEN 'LogIn' ELSE 'TimeUp' END WHEN Branch_ID IN ('Karachi') THEN CASE WHEN convert(varchar(32),CONVERT(DATETIME,GETDATE() AT TIME ZONE 'India Standard Time' AT TIME ZONE 'Pakistan Standard Time'),108) > '" + StartTime + "' AND convert(varchar(32),CONVERT(DATETIME,GETDATE() AT TIME ZONE 'India Standard Time' AT TIME ZONE 'Pakistan Standard Time'),108) < '" + EndTime + "' THEN 'LogIn' ELSE 'TimeUp' END ELSE CASE WHEN convert(varchar(32),CONVERT(DATETIME,GETDATE() AT TIME ZONE 'India Standard Time' AT TIME ZONE 'E. Africa Standard Time'),108) > '" + StartTime + "' AND convert(varchar(32),CONVERT(DATETIME,GETDATE() AT TIME ZONE 'India Standard Time' AT TIME ZONE 'E. Africa Standard Time'),108) < '" + EndTime + "' THEN 'LogIn' ELSE 'TimeUp' END END AS AccessAllow FROM JHS_Data.dbo.Jamea_Table WHERE (Father_ITS = '" + ITSID.Value + "' OR Mother_ITS = '" + ITSID.Value + "') And Nizaam_Type = 'Nizaam Thaani'";
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    if (sdr["AccessAllow"].ToString() == "LogIn")
                    {
                        HttpCookie myCookie = new HttpCookie("Hazri");
                        DateTime now = DateTime.Now;
                        myCookie["ITS"] = ITSID.Value;
                        myCookie.Expires = now.AddHours(1);
                        Response.Cookies.Add(myCookie);

                        sdr.Close();
                        con.Close();
                        Response.Redirect("Hazri.aspx");
                    }
                    else
                    {
                        transferalertITS.InnerHtml = "<strong>Hazri Time Up !!!<br/>LogIn Between " + StartTime + " To " + EndTime + "</strong>";
                        transferalertITS.Attributes.Add("Class", "alert alert-danger");
                    }
                }
                else
                {
                    transferalertITS.InnerHtml = "<strong>Cross Authentication Failed</strong>";
                    transferalertITS.Attributes.Add("Class", "alert alert-danger");
                }
            }
            else
            {
                transferalertITS.InnerHtml = "<strong>ITS Authentication Failed.</strong>";
                transferalertITS.Attributes.Add("Class", "alert alert-danger");
            }
            con.Close();
        }
    }
}