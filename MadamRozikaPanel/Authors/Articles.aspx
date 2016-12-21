<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Articles.aspx.cs" Inherits="MadamRozikaPanel.Authors.Articles" %>
<%@ Import Namespace="MadamRozikaPanel.CrossCuttingLayer" %>
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
                <h1>Yazı Listele</h1>
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
                    <a>Yazılar</a>
                    <i class="fa fa-angle-right"></i>
                </li>
                <li>
                    <a href="/Authors/Articles.aspx">Yazı Listele</a>
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
                            Yazı Filtrele
                        </h3>
                    </div>
                    <div class="box-content">
                        <div class="col-sm-2">
                            <p>
                                <asp:DropDownList ID="ddListYazarlar" CssClass="btn" runat="server">
                                </asp:DropDownList>
                            </p>
                        </div>
                        <div class="col-sm-4">
                            <p>
                                <asp:TextBox ID="txtAra" CssClass="form-control" PlaceHolder="Ara" runat="server" />
                            </p>
                        </div>
                        <div class="col-sm-6">
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
                            Tüm Yazılar
                        </h3>
                    </div>
                    <div class="box-content nopadding">
                        <table id="dataTable" class="table table-hover table-nomargin">
                            <thead>
                                <tr>
                                    <th style="width: 8%;">ID</th>
                                    <th style="width: 30%;">Başlık</th>
                                    <th style="width: 10%;">Pozisyon</th>
                                    <th style="width: 9%;">İşlem</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptAllNews" runat="server" OnItemCommand="rptAllNews_ItemCommand">
                                    <ItemTemplate>
                                        <tr class="<%#Eval("Status").ToString() == "0" ? "danger" : "" %>">
                                            <td><a href="/Authors/EditArticle.aspx?ArticleId=<%#Eval("ArticleId")%>"><%#Eval("ArticleId") %></a></td>
                                            <td>
                                                <span>Başlık:</span>
                                                <asp:TextBox CssClass="form-control" Style="width: 100%;" ID="txtTitle" runat="server" Text='<%# Helper.RemoveHtml(Eval("Title").ToString()) %>' />
                                                </td>
                                            <td>
                                                <span>Durum:</span>
                                                <asp:CheckBox CssClass="form-control" ID="cbStatus" Text="Aktif" Checked='<%#Eval("Status").ToString() == "0" ? false : true %>' Width="100%" runat="server" />
                                                </td>
                                            <td>
                                                <br />
                                                <asp:Button Width="100%" CssClass="btn btn-primary form-control" Text="Güncelle" ID="btnGuncelle" CommandName="btnGuncelle" CommandArgument='<%#Eval("ArticleId") %>' runat="server" /><br />
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