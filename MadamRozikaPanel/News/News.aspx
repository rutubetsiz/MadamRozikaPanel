<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="MadamRozikaPanel.News.News" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- New DataTables -->
    <script src="/static/js/plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="/static/js/plugins/datatables/extensions/dataTables.fixedColumns.min.js"></script>
    <script src="/static/js/plugins/datatables/extensions/dataTables.fixedHeader.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#dataTable').DataTable({
                "ordering": false,
                "bLengthChange": false,
                "bFilter": false
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="formNewsFilt" runat="server" class='form-horizontal form-bordered'>
        <div class="page-header">
            <div class="pull-left">
                <h1>Haber Listele</h1>
            </div>
            <div class="pull-right">
                <ul class="stats">
                    <li class='lightred'>
                        <i class="fa fa-calendar"></i>
                        <div class="details">
                            <span class="big"><%Response.Write(DateTime.Now.ToString("dd MMMM, yyyy")); %></span>
                            <span><%Response.Write(DateTime.Now.ToString("dddd, HH:MM")); %></span>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
        <div class="breadcrumbs">
            <ul>
                <li>
                    <a href="/">Anasayfa</a>
                    <i class="fa fa-angle-right"></i>
                </li>
                <li>
                    <a>Haberler</a>
                    <i class="fa fa-angle-right"></i>
                </li>
                <li>
                    <a href="/News/News.aspx">Haber Listele</a>
                </li>
            </ul>
            <div class="close-bread">
                <a href="#">
                    <i class="fa fa-times"></i>
                </a>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="box box-color box-bordered">
                    <div class="box-title">
                        <h3>
                            <i class="glyphicon-search"></i>
                            Haber Filtrele
                        </h3>
                    </div>
                    <div class="box-content">
                        <div class="col-sm-2">
                            <p>
                                <asp:DropDownList ID="ddListKategori" CssClass="btn" runat="server">
                                </asp:DropDownList>
                            </p>
                        </div>
                        <div class="col-sm-2">
                            <p>
                                <asp:DropDownList ID="ddListTurler" CssClass="btn" runat="server">
                                    <asp:ListItem Text="Tüm Türler" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="Kutu Haber" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Manşet" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Sürmanşet" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Süper Manşet" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Üst Süper Manşet" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Seo" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Manşet Üstü Süper" Value="6"></asp:ListItem>
                                </asp:DropDownList>
                            </p>
                        </div>
                        <div class="col-sm-2">
                            <p>
                                <asp:DropDownList ID="ddListDurum" CssClass="btn" runat="server">
                                    <asp:ListItem Text="Tüm Durumlar" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="Aktif" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Pasif" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Sabah 7" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </p>
                        </div>
                        <div class="col-sm-2">
                            <p>
                                <asp:TextBox ID="txtAra" CssClass="form-control" PlaceHolder="Ara" runat="server" />
                            </p>
                        </div>
                        <div class="col-sm-2">
                            <p>
                                <asp:Button Text="Filtrele" CssClass="btn btn-primary" ID="btnFiltrele" OnClick="btnFiltrele_Click" runat="server" />
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="box box-color box-bordered">
                    <div class="box-title">
                        <h3>
                            <i class="fa fa-table"></i>
                            Tüm Haberler
                        </h3>
                    </div>
                    <div class="box-content nopadding">
                        <table id="dataTable" class="table table-hover table-nomargin">
                            <thead>
                                <tr>
                                    <th style="width: 8%;">ID</th>
                                    <th style="width: 13%;">Görsel</th>
                                    <th style="width: 30%;">Başlık</th>
                                    <th style="width: 10%;">Kategori</th>
                                    <th style="width: 10%;">Durum</th>
                                    <th style="width: 20%;">Giriş Tarihi</th>
                                    <th style="width: 9%;">İşlem</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptAllNews" runat="server" OnItemDataBound="rptAllNews_ItemDataBound" OnItemCommand="rptAllNews_ItemCommand">
                                    <ItemTemplate>
                                        <tr class="<%#Eval("Status").ToString() == "0" ? "danger" : "" %>">
                                            <td><a href="/News/EditNews.aspx?NewsId=<%#Eval("NewsId")%>"><%#Eval("NewsId") %></a></td>
                                            <td>
                                                <img src="<%#Eval("ImageUrl") %>" height="50" alt="<%# MadamRozikaPanel.CrossCuttingLayer.Helper.RemoveHtml(Eval("Title").ToString()) %>"/>
                                            </td>
                                            <td>
                                                <span><%# MadamRozikaPanel.CrossCuttingLayer.Helper.RemoveHtml(Eval("Title").ToString()) %></span>
                                            </td>
                                            <td>
                                                <%--<asp:DropDownList CssClass="form-control" ID="ddListCategory" Width="100%" runat="server">
                                                </asp:DropDownList>--%>
                                                <asp:Literal ID="ltrlKategori" Text="" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Literal ID="ltrlDurum" Text="text" runat="server" />
                                            </td>
                                            <td><%#Eval("PublishDate") %></td>
                                            <td>
                                                <asp:Button Width="100%" CssClass="btn btn-primary form-control" Text="Güncelle" ID="btnGuncelle" CommandName="btnGuncelle" CommandArgument='<%#Eval("NewsId") %>' runat="server" /><br />
                                                <br />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
