﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio_Usu.aspx.cs" Inherits="EcoAmigios.Forms.Inicio_Usu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Inicio</title>
    <style type="text/css">
        body{
            background-color:palegreen;
        }
        .data{
            border-radius: 15px;
        }
        .auto-style1 {
            width: 100%;
        }
        .auto-style3 {
            width: 1px;
        }
        .auto-style2 {
            width: 721px;
        }
        .auto-style5 {
            width: 1px;
            height: 56px;
        }
        .auto-style6 {
            width: 721px;
            height: 56px;
        }
        .auto-style4 {
            width: 724px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table class="auto-style1">
                <tr>
                    <td class="auto-style3">
                        <asp:ImageButton ID="IBFiltrar" runat="server" Height="55px" ImageUrl="~/Imagenes/filtrar.png" Width="54px" OnClick="IBFiltrar_Click" />
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="Label2" runat="server" Font-Names="Comic Sans MS" Text="Filtrar"></asp:Label>
            <asp:Label ID="LabelNombre" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">
                        <asp:ImageButton ID="IMBFiltros" runat="server" Height="52px" ImageUrl="~/Imagenes/borrar.png" Width="53px" OnClick="IMBFiltros_Click" />
                    </td>
                    <td class="auto-style6">
                        <asp:Label ID="Label3" runat="server" Font-Names="Comic Sans MS" Text="Borrar filtros"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:ImageButton ID="IBCuenta" runat="server" Height="52px" ImageUrl="~/Imagenes/boton-de-cuenta-.png" Width="53px" OnClick="IMBFiltros0_Click" />
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="Label5" runat="server" Font-Names="Comic Sans MS" Text="Cuenta"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:ImageButton ID="IBCSesion" runat="server" Height="52px" ImageUrl="~/Imagenes/cerrar-sesion.png" Width="51px" OnClick="IBCSesion_Click" />
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="Label4" runat="server" Font-Names="Comic Sans MS" Text="Cerrar Sesion"></asp:Label>
                    </td>
                </tr>
            </table>

        </div>
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <asp:DataList ID="DataPaginas" OnItemCommand = "DataPaginas_ItemCommand" runat="server" DataSourceID="SqlDataPaginas" Font-Bold="False" Font-Italic="False" Font-Names="Comic Sans MS" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" GridLines="Both" HorizontalAlign="Center" RepeatDirection="Horizontal" CssClass="data" Font-Size="X-Large">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Comic Sans MS" Font-Size="X-Large" />
                    <ItemTemplate>
                        <asp:Image ID="ImagePagina" runat="server" CssClass="imagen" height="120" ImageUrl='<%# "~/Imagenes/"+Eval("Logo_Grupo") %>' width="140" />
                            <br />
                            <asp:Label ID="Nombre_PaginaLabel" runat="server" Text='<%# Eval("Nombre_Pagina") %>' />
                            <br />
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Tipo de Pagina:" />
                            <br />
                            <asp:Label ID="ID_Tipo_PaginaLabel" runat="server" Text='<%# Eval("Tipo_Pagina") %>' />
                            <br />
                            <br />
                            <asp:Button ID="ButtonPagina" runat="server" CommandName="Ver_Pagina" CssClass="botonP" Text="Ver Pagina" />
                    </ItemTemplate>
                </asp:DataList>
                <br />
                <asp:SqlDataSource ID="SqlDataPaginas" runat="server" ConnectionString="<%$ ConnectionStrings:EcoAmigosConnectionString %>" SelectCommand="SELECT * FROM [EAV_PAGINA]"></asp:SqlDataSource>
                <asp:DataList ID="DataPaginasFiltrar" OnItemCommand = "DataPaginasFiltrar_ItemCommand" runat="server" BorderColor="Gray" DataSourceID="SqlDataPaginasFiltrar" Font-Bold="False" Font-Italic="False" Font-Names="Comic Sans MS" Font-Overline="False" Font-Size="Large" Font-Strikeout="False" Font-Underline="False" GridLines="Both" HorizontalAlign="Center" RepeatColumns="5" CssClass="data">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:Image ID="ImagePagina0" runat="server" CssClass="imagen" height="120" ImageUrl='<%# "~/Imagenes/"+Eval("Logo_Grupo") %>' width="140" />
                            <br />
                            <asp:Label ID="Nombre_PaginaLabel0" runat="server" Text='<%# Eval("Nombre_Pagina") %>' />
                            <br />
                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Tipo de Pagina:" />
                            <br />
                            <asp:Label ID="ID_Tipo_PaginaLabel0" runat="server" Text='<%# Eval("Tipo_Pagina") %>' />
                            <br />
                            <br />
                            <asp:Button ID="ButtonPagina0" runat="server" CommandName="Ver_Pagina0" CssClass="botonP" Text="Ver Pagina" />
                        </ItemTemplate>
                    </asp:DataList>
                <asp:SqlDataSource ID="SqlDataPaginasFiltrar" runat="server" ConnectionString="<%$ ConnectionStrings:EcoAmigosConnectionString2 %>" SelectCommand="SELECT [Nombre_Pagina], [Tipo_Pagina], [Logo_Grupo] FROM [EAV_PAGINA] WHERE ([Tipo_Pagina] = @Tipo_Pagina)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="LabelTipo" Name="Tipo_Pagina" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <table class="auto-style1">
                    <tr>
                        <td class="auto-style4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="ListTipoPagina" runat="server" DataSourceID="SqlDataSource1" DataTextField="Tipo_Pagina" DataValueField="Tipo_Pagina" Font-Names="Comic Sans MS" Width="157px">
                                <asp:ListItem>Seleccione uno...</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EcoAmigosConnectionString3 %>" SelectCommand="SELECT [Tipo_Pagina] FROM [EAV_PAGINA]"></asp:SqlDataSource>
                        </td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="BtBuscar" runat="server" Font-Names="Comic Sans MS" OnClick="BtBuscar_Click" Text="BUSCAR" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Label ID="LabelTipo" runat="server" Visible="False"></asp:Label>
            </asp:View>
        </asp:MultiView>
    </form>
</body>
</html>
