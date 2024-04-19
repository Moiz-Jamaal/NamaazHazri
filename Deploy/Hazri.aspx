<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hazri.aspx.cs" Inherits="NamaazHazri.Hazri" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
        <meta name="description" content="" />
        <meta name="author" content="" />
        <title>Hazri | AJS Hazri.</title>
        <!-- Favicon-->
        <link rel="icon" type="image/x-icon" href="assets/favicon.ico" />
        <!-- Font Awesome icons (free version)-->
        <script src="https://use.fontawesome.com/releases/v6.1.0/js/all.js" crossorigin="anonymous"></script>
        <!-- Google fonts-->
        <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700" rel="stylesheet" type="text/css" />
        <link href="https://fonts.googleapis.com/css?family=Lato:400,700,400italic,700italic" rel="stylesheet" type="text/css" />
        <!-- Core theme CSS (includes Bootstrap)-->
        <link href="css/styles.css" rel="stylesheet" />
    </head>
    <body id="page-top">
        <form runat="server">

        
        <!-- Navigation-->
        <nav class="navbar navbar-expand-lg bg-secondary text-uppercase fixed-top" id="mainNav">
            <div class="container">
                <a class="navbar-brand" href="#page-top">Namaaz Hazri</a>
                <button class="navbar-toggler text-uppercase font-weight-bold bg-primary text-white rounded" type="button" data-bs-toggle="collapse" data-bs-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
                    Menu
                    <i class="fas fa-bars"></i>
                </button>
                <div class="collapse navbar-collapse" id="navbarResponsive">
                    <ul class="navbar-nav ms-auto">
                        <li class="nav-item mx-0 mx-lg-1"><a class="nav-link py-3 px-0 px-lg-3 rounded" href="Login.aspx?mode=logoff">Log Off</a></li>
                    </ul>
                </div>
            </div>
        </nav>
            <br />
            <br />
            <br />
        <section class="page-section" id="contact">
            <div class="container">
                <!-- Contact Section Form-->
                <div class="row justify-content-center">
                    <div class="col-lg-8 col-xl-7">
                        <div id="contactForm" data-sb-form-api-token="API_TOKEN">
                            <center style="height: 147px">
					            <div style="width:98%"><br />
						            <span style="color:blue; font-size:1.2em; font-weight:600">Hazri Date : </span>
						            <asp:Label ID="LblDate" runat="server" style="color:blue; font-size:1.2em; font-weight:600"></asp:Label>
						            <br /><br />
						            <span style="font-size:1.2em; font-weight:600">Talebe-Ilm Name :</span>
						            <telerik:radcombobox runat="server" ID="RCBStudent" Skin="Bootstrap" Width="100%" Font-Size="1.2em"></telerik:radcombobox>
						            <br /><br />
						            <table style="width:98%">
							            <tr>
								            <td>
									            <asp:Button ID="BtnPresent" runat="server" Text="Set Present" CssClass="form-control btn-success btn-xl" OnClick="BtnSave_Click" />
								            </td>
								            <td>
									            <asp:Button ID="BtnAbsent" runat="server" Text="Set Absent" CssClass="form-control btn-danger btn-xl" OnClick="BtnAbsent_Click" Visible="false" />
								            </td>
							            </tr>
						            </table>
						
					            </div>
				            </center>                            
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
            <br />
            <br />
            <center>
                <div role="alert" id="transferalertITS" runat="server"></div>
            </center>
        </section>
            
        <!-- Bootstrap core JS-->
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
        <!-- Core theme JS-->
        <script src="js/scripts.js"></script>
        <!-- * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *-->
        <!-- * *                               SB Forms JS                               * *-->
        <!-- * * Activate your form at https://startbootstrap.com/solution/contact-forms * *-->
        <!-- * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *-->
        <script src="https://cdn.startbootstrap.com/sb-forms-latest.js"></script>
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                <AjaxSettings>   
                    
                <telerik:AjaxSetting AjaxControlID="BtnPresent">
                    <UpdatedControls>                                                            
                        <telerik:AjaxUpdatedControl ControlID="transferalertITS"/>
                        <telerik:AjaxUpdatedControl ControlID="BtnPresent"/>
                    </UpdatedControls>
                </telerik:AjaxSetting>

                    <telerik:AjaxSetting AjaxControlID="BtnAbsent">
                    <UpdatedControls>                                                            
                        <telerik:AjaxUpdatedControl ControlID="transferalertITS"/>
                        <telerik:AjaxUpdatedControl ControlID="BtnAbsent"/>
                    </UpdatedControls>
                </telerik:AjaxSetting>

                </AjaxSettings>
            </telerik:RadAjaxManager>
        <telerik:radajaxloadingpanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" BackgroundPosition="Center" EnableSkinTransparency="true" Height="5000px" Modal="true" Transparency="40"></telerik:radajaxloadingpanel>
            </form>
    </body>
</html>
