<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Premios.aspx.cs" Inherits="WebApplication2.Premios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contenedor-premios fondo">
        <h1 class="tituloFormulario">Premios</h1>
        <p class="lead  mb-5 px-3">
            A continuación, podrá ver el catálogo de premios disponibles para canjear con su voucher. 
            Cada producto ha sido seleccionado especialmente para nuestros participantes. 
            Una vez elegido su premio, presione el botón "Seleccionar" correspondiente. 
            Recuerde que solo se permite un canje por voucher, y que la disponibilidad está sujeta a stock. 
            ¡No pierda la oportunidad de llevarse uno de estos increíbles regalos!
        </p>
        <div class="container">
            <div class="row justify-content-center">
                <asp:Repeater ID="repPremios" runat="server" OnItemDataBound="repPremios_ItemDataBound" OnItemCommand="repPremios_ItemCommand">
                    <ItemTemplate>
                        <div class="col-12 col-sm-6 col-md-4 d-flex justify-content-center mb-4">
                            <div class="card h-100 d-flex flex-column" style="width: 100%; max-width: 22rem;">
                                <div id='carousel<%# Eval("Id") %>' class="carousel slide" data-bs-ride="carousel">
                                    <div class="carousel-inner" runat="server" id="packImagenes"></div>
                                    <button class="carousel-control-prev" type="button" data-bs-target="#carousel<%# Eval("Id") %>" data-bs-slide="prev">
                                        <span class="carousel-control-prev-icon"></span>
                                    </button>
                                    <button class="carousel-control-next" type="button" data-bs-target="#carousel<%# Eval("Id") %>" data-bs-slide="next">
                                        <span class="carousel-control-next-icon"></span>
                                    </button>
                                </div>
                                <div class="card-body d-flex flex-column tarjeta">
                                    <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                    <p class="card-text "><%# Eval("Descripcion") %></p>
                                    <asp:Button
                                        ID="btnParticipar"
                                        runat="server"
                                        Text="Seleccionar"
                                        CssClass="btn btn-primary mt-auto mx-auto w-auto"
                                        CommandName="participar"
                                        CommandArgument='<%# Eval("Id") %>' />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
