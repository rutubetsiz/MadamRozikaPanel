<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Comments.aspx.cs" Inherits="MadamRozikaPanel.Moderation.Comments" %>
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
                <h1>Yorumlar</h1>
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
                    <a>Moderasyon</a>
                    <i class="fa fa-angle-right"></i>
                </li>
                <li>
                    <a href="/Moderation/Comments.aspx">Yorumlar</a>
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
                            Filtrele
                        </h3>
                    </div>
                    <div class="box-content">
                        <div class="col-sm-3">
                            <p>
                                <asp:DropDownList name="ddListDurum" ID="ddListDurum" class='form-control' runat="server">
                                    <asp:ListItem Text="Yeni" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Aktif" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Pasif" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </p>
                        </div>
                        <div class="col-sm-4">
                            <p>
                                <asp:TextBox ID="txtAra" CssClass="form-control" PlaceHolder="Ara" runat="server" />
                            </p>
                        </div>
                        <div class="col-sm-5">
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
                            Yorumlar
                        </h3>
                    </div>
                    <div class="box-content nopadding">
                        <table id="dataTable" class="table table-hover table-nomargin">
                            <thead>
                                <tr>
                                    <th style="width: 30%;">Yorum</th>
                                    <th style="width: 10%;">Aktifle</th>
                                    <th style="width: 9%;">Pasifle</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptAllNews" runat="server" OnItemCommand="rptAllNews_ItemCommand">
                                    <ItemTemplate>
                                        <tr class="<%#Eval("IsActive").ToString() == "0" ? "danger" : "" %>">
                                            <td>
                                                <span><strong><%# Helper.RemoveHtml(Eval("UserName").ToString()) %></strong></span><br />
                                                <span><%#Eval("RecordDate") %></span><br />
                                                <span><%# Helper.RemoveHtml(Eval("Text").ToString()) %></span><br />
                                                <span><strong><%#Eval("Type") %> : </strong> <%#CommentOprt.GetUrl(Eval("ContentId").ToString(),Eval("Type").ToString()) %> </span>
                                            </td>
                                            <td>
                                                <asp:Button Width="100%" CssClass="btn btn-primary form-control" Text="Aktifle" ID="btnAktifle" CommandName="btnAktifle" CommandArgument='<%#Eval("CommentId") %>' runat="server" />
                                            </td>
                                            <td>
                                                <asp:Button Width="100%" CssClass="btn btn-primary form-control" Text="Pasifle" ID="btnPasifle" CommandName="btnPasifle" CommandArgument='<%#Eval("CommentId") %>' runat="server" />
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