<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Authors.aspx.cs" Inherits="MadamRozikaPanel.Authors.Authors" %>
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
                <h1>Yazar Listele</h1>
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
                    <a>Yazarlar</a>
                    <i class="fa fa-angle-right"></i>
                </li>
                <li>
                    <a href="/Authors/Authors.aspx">Yazar Listele</a>
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
                            Yazar Filtrele
                        </h3>
                    </div>
                    <div class="box-content">
                        <div class="col-sm-2">
                            <p>
                                <asp:DropDownList ID="ddListDurum" CssClass="btn" runat="server">
                                    <asp:ListItem Text="Tüm Durumlar" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="Aktif" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Pasif" Value="0"></asp:ListItem>
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
                            Tüm Yazarlar
                        </h3>
                    </div>
                    <div class="box-content nopadding">
                        <table id="dataTable" class="table table-hover table-nomargin">
                            <thead>
                                <tr>
                                    <th style="width: 8%;">ID</th>
                                    <th style="width: 30%;">Ad - Mail</th>
                                    <th style="width: 10%;">Pozisyon</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptAllNews" runat="server">
                                    <ItemTemplate>
                                        <tr class="<%#Eval("Status").ToString() == "0" ? "danger" : "" %>">
                                            <td><a href="/Authors/EditAuthor.aspx?AuthorId=<%#Eval("AuthorId")%>"><%#Eval("AuthorId") %></a></td>
                                            <td>
                                                <span>Ad:</span>
                                                <span><%# Helper.RemoveHtml(Eval("Name").ToString()) %></span>
                                                <br />
                                                <span>Mail:</span>
                                                <span><%# Helper.RemoveHtml(Eval("Mail").ToString()) %></span>
                                            </td>
                                            <td>
                                                <span>Durum:</span>
                                                <span><%#Eval("Status").ToString() == "0" ? "Pasif" : "Aktif" %></span>
                                                <br />
                                                <span>Anasayfa Durum:</span>
                                                <span><%#Eval("MainPageStatus").ToString() == "0" ? "Yayında Değil" : "Yayında"  %></span>
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
