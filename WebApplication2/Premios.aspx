<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Premios.aspx.cs" Inherits="WebApplication2.Premios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Premios</h1>
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
                            <div class="card-body d-flex flex-column">
                                <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                <p class="card-text"><%# Eval("Descripcion") %></p>
                                <asp:Button
                                    ID="btnParticipar"
                                    runat="server"
                                    Text="Participar"
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
</asp:Content>
