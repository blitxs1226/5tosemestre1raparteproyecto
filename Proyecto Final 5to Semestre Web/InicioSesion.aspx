<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InicioSesion.aspx.cs" Inherits="Proyecto_Final_5to_Semestre_Web.InicioSesion" %>

<!DOCTYPE html>

<html xmlns="http://www.
   
    <title>Inicio Sesion</title>
    <!-- JS, Popper.js, and jQuery -->
    <link rel="icon" href="Imagenes/IconMon.png" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" integrity="sha384-JcKb8q3iqJ61gNV9KGb8thSsNjpSL0n8PARn9HuZOnIxN0hoP+VmmDGMN5t9UJ0Z" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" />

    <script src="https://code.jquery.com/jquery-3.5.1.min.js" integrity="sha256-9/aliU8dGd2tb6OSsuzixeV4y/faTqgFtohetphbbj0=" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js" integrity="sha384-9/reFTGAW83EW2RDu2S0VKaIzap3H66lZH81PoYlFhbGU+6BZp6G7niu735Sk7lN" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js" integrity="sha384-B4gt1jrGC7Jh4AgTPSdUtOBvfO8shuf57BaghqFfPlYxofvL8/KUEfYiJOMMV+rV" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <link href="https://fonts.googleapis.com/css2?family=Dancing+Script&family=Lobster&family=Nanum+Gothic&family=Nunito:wght@200&family=Open+Sans+Condensed:wght@300&family=Raleway:wght@100&family=Space+Grotesk:wght@300&display=swap" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>
    <link rel="preconnect" href="https://fonts.gstatic.com"/>
    <link href="https://fonts.googleapis.com/css2?family=IBM+Plex+Sans:ital,wght@1,200&family=Indie+Flower&family=Pacifico&family=Staatliches&family=Ubuntu:wght@300&family=Yellowtail&display=swap" rel="stylesheet"/>
     <link rel="stylesheet" href="EstilosCSS/Stylelogin.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container h-100">
            <div class="d-flex justify-content-center h-100">
                <asp:Panel ID="pnltodo" runat="server" CssClass="user_card bajarpoco" Style="padding: 10px 5px; background: rgba(255,255,255,.5);">
                    <asp:Panel ID="pnlLlenarDatos" runat="server">
                        <div class="d-flex justify-content-center ">
                            <div class="brand_logo_container pb-2">
                                <h1>CAPTCHA</h1>
                            </div>
                        </div>
                        <div class="d-flex justify-content-center form_container ">
                            <div>
                                <div class="input-group mb-3">
                                    <div class="input-group-append">
                                        <span class="input-group-text"><i class="fas fa-user"></i></span>
                                    </div>
                                    <asp:TextBox ID="txtusuario" placeholder="Usuario" CssClass="form-control input_user" runat="server"></asp:TextBox>

                                </div>
                                <div class="input-group mb-2">
                                    <div class="input-group-append">
                                        <span class="input-group-text"><i class="fas fa-key"></i></span>
                                    </div>
                                    <asp:TextBox ID="txtPassword" TextMode="Password" placeholder="Contraseña" CssClass="form-control input_pass" runat="server"></asp:TextBox>
                                </div>
                                <div class="d-flex justify-content-center mt-3 login_container">
                                    <asp:LinkButton ID="lnkIngresar" runat="server" CssClass="btn btn-primary" OnClick="lnkIngresar_Click"><i class="fad fa-sign-in-alt"></i> Ingresar</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>
