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
    public partial class Hazri : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlConnection con;

        SqlCommand cmd2;
        SqlConnection con2;

        SqlDataReader sdr;
        SqlDataAdapter da;
        DataSet ds;

        ITSWS.EjamaatService ITSWebServ = new ITSWS.EjamaatService();
        MarolWS.AJSServices MrlWS = new MarolWS.AJSServices();
        KarachiWS.AJSServices KhiWS = new KarachiWS.AJSServices();
        SuratWS.AJSServices SrtWS = new SuratWS.AJSServices();
        NairobiWS.AJSServices NboWS = new NairobiWS.AJSServices();

        public void Conn()
        {
            //Connection Settings

            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
        } 

        public void AccessCheck()
        {
            Conn();
            string Access = "";
            string PITS = Request.Cookies["Hazri"]["ITS"].ToString();
            string StartTime = ConfigurationManager.AppSettings["StartTime"].ToString();
            string EndTime = ConfigurationManager.AppSettings["EndTime"].ToString();
            cmd.CommandText = "SELECT *, CASE WHEN Branch_ID IN ('Surat', 'Marol') THEN CASE WHEN convert(varchar(32),GETDATE(),108) > '" + StartTime + "' AND convert(varchar(32),GETDATE(),108) < '" + EndTime + "' THEN 'LogIn' ELSE 'TimeUp' END WHEN Branch_ID IN ('Karachi') THEN CASE WHEN convert(varchar(32),CONVERT(DATETIME,GETDATE() AT TIME ZONE 'India Standard Time' AT TIME ZONE 'Pakistan Standard Time'),108) > '" + StartTime + "' AND convert(varchar(32),CONVERT(DATETIME,GETDATE() AT TIME ZONE 'India Standard Time' AT TIME ZONE 'Pakistan Standard Time'),108) < '" + EndTime + "' THEN 'LogIn' ELSE 'TimeUp' END ELSE CASE WHEN convert(varchar(32),CONVERT(DATETIME,GETDATE() AT TIME ZONE 'India Standard Time' AT TIME ZONE 'E. Africa Standard Time'),108) > '" + StartTime + "' AND convert(varchar(32),CONVERT(DATETIME,GETDATE() AT TIME ZONE 'India Standard Time' AT TIME ZONE 'E. Africa Standard Time'),108) < '" + EndTime + "' THEN 'LogIn' ELSE 'TimeUp' END END AS AccessAllow FROM JHS_Data.dbo.Jamea_Table WHERE (Father_ITS = '" + PITS + "' OR Mother_ITS = '" + PITS + "') And Nizaam_Type = 'Nizaam Thaani'";
            sdr = cmd.ExecuteReader();
            while (sdr.Read() && Access != "LogIn")
            {
                Access = sdr["AccessAllow"].ToString();
            }
            sdr.Close();            
            con.Close();
            if (Access == "TimeUp" || Access == "")
            {
                Response.Redirect("LogIn.aspx?mode=session");
            }            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    try
                    {
                        string PITS = Request.Cookies["Hazri"]["ITS"].ToString();
                        Conn();
                        cmd.CommandText = "Select FullName, TrNo + '|' + Branch_ID As Dtls From JHS_Data.dbo.Jamea_Table Where (Father_ITS = '" + PITS + "' OR Mother_ITS = '" + PITS + "') And Nizaam_Type = 'Nizaam Thaani' Order By Darajah";
                        da = new SqlDataAdapter(cmd);
                        ds = new DataSet();
                        da.Fill(ds);
                        RCBStudent.DataTextField = "FullName";
                        RCBStudent.DataValueField = "Dtls";
                        RCBStudent.DataSource = ds.Tables[0];
                        RCBStudent.DataBind();

                        cmd.CommandText = "SELECT TOP (1) G_Dt FROM dbo.Calender ORDER BY T_Stamp DESC";
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            LblDate.Text = sdr["G_Dt"].ToString();
                        }
                        sdr.Close();
                        con.Close();

                        AccessCheck();
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("LogIn.aspx?mode=session");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                AccessCheck();
                string[] Stdnt = RCBStudent.SelectedValue.ToString().Split('|');
                string Rslt = "";
                if (Stdnt[1] == "Surat")
                {
                    Rslt = SrtWS.UpdateNamaazHazri(Stdnt[0], "N@m@@z#W$");
                }
                else if (Stdnt[1] == "Marol")
                {
                    Rslt = MrlWS.UpdateNamaazHazri(Stdnt[0], "N@m@@z#W$");
                }
                else if (Stdnt[1] == "Karachi")
                {
                    Rslt = KhiWS.UpdateNamaazHazri(Stdnt[0], "N@m@@z#W$");
                }
                else if (Stdnt[1] == "Nairobi")
                {
                    Rslt = NboWS.UpdateNamaazHazri(Stdnt[0], "N@m@@z#W$");
                }
                transferalertITS.InnerHtml = "<strong>" + Rslt + "</strong>";
                transferalertITS.Attributes.Add("Class", "alert alert-success");
            }
            catch
            {
                Response.Redirect("LogIn.aspx?mode=session");
            }            
        }

        protected void BtnAbsent_Click(object sender, EventArgs e)
        {
                      
        }
    }
}