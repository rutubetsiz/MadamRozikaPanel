<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Galleries.aspx.cs" Inherits="MadamRozikaPanel.Gallery.Galleries" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="formNewsFilt" runat="server" class='form-horizontal form-bordered'>
        <div class="page-header">
            <div class="pull-left">
                <h1>Galeri Listele</h1>
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
                    <a>Galeriler</a>
                    <i class="fa fa-angle-right"></i>
                </li>
                <li>
                    <a href="/Gallery/Galleries.aspx">Galeri Listele</a>
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
                            Galeri Filtrele
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
                                    <asp:ListItem Text="Fotoğraf" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Video" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </p>
                        </div>
                        <div class="col-sm-2">
                            <p>
                                <asp:DropDownList ID="ddListKaynak" CssClass="btn" runat="server">
                                    <asp:ListItem Text="Tüm Kaynaklar" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="Bugün" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Ajanslar" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </p>
                        </div>
                        <div class="col-sm-2">
                            <p>
                                <asp:DropDownList ID="ddListDurum" CssClass="btn" runat="server">
                                    <asp:ListItem Text="Tüm Durumlar" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="Aktif" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Pasif" Value="0"></asp:ListItem>
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
                            Tüm Galeriler
                        </h3>
                    </div>
                    <div class="box-content nopadding">
                        <table id="dataTable" class="table table-hover table-nomargin">
                            <thead>
                                <tr>
                                    <th style="width: 8%;">ID</th>
                                    <th style="width: 50%;">Başlık</th>
                                    <th style="width: 10%;">Kategori - Durum</th>
                                    <th style="width: 10%;">Giriş Tarihi</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptAllNews" runat="server" OnItemCommand="rptAllNews_ItemCommand">
                                    <ItemTemplate>
                                        <tr class="<%#Eval("Status").ToString() == "0" ? "danger" : "" %>">
                                            <td><a href="/Gallery/EditGallery.aspx?GalleryId=<%#Eval("GalleryId")%>"><%#Eval("GalleryId") %></a></td>
                                            <td>
                                                <span>Başlık:</span>
                                                <span><%# Helper.RemoveHtml(Eval("Title").ToString()) %></span>
                                            </td>
                                            <td>Kategori:
                                                <span><%#CrossOprt.KategoriAdi(Convert.ToInt32(Eval("CategoryId"))) %></span>
                                            <%--<asp:DropDownList CssClass="form-control" ID="ddListCategory" Width="100%" runat="server">
                                            </asp:DropDownList>--%>
                                                <br />
                                                Durum:
                                             <span><%#Eval("Status").ToString() == "0" ? "Pasif" : "Aktif" %></span>
                                            </td>
                                            <td><%#Eval("PublishDate") %></td>
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