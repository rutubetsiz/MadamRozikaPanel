<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="HeadlinesManagement.aspx.cs" Inherits="MadamRozikaPanel.Headlines.HeadlinesManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-header">
        <div class="pull-left">
            <h1>Manşet Yönetimi</h1>
        </div>
        <div class="pull-right">
            <ul class="minitiles">
                <li class='grey'>
                    <a href="#">
                        <i class="fa fa-cogs"></i>
                    </a>
                </li>
                <li class='lightgrey'>
                    <a href="#">
                        <i class="fa fa-globe"></i>
                    </a>
                </li>
            </ul>
            <ul class="stats">
                <li class='satgreen'>
                    <i class="fa fa-money"></i>
                    <div class="details">
                        <span class="big">$324,12</span>
                        <span>Balance</span>
                    </div>
                </li>
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
                <a href="/News/ReOrder.aspx">Manşet Yönetimi</a>
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
                        <i class="fa fa-table"></i>
                        Manşetler
                    </h3>
                    <div class="pull-right">
                        <a href="/Headlines/EditHeadline.aspx" class="btn btn-success" rel="tooltip" title="" data-original-title="Manşet Ekle"><i class="fa fa-edit"></i>Manşet Ekle</a>
                    </div>
                </div>
                <div class="box-content nopadding">
                    <form id="formReOrder" runat="server" class='form-horizontal form-bordered'>
                        <script>
                            $(function () {
                                $("#sortable").sortable({
                                    update: function (event, ui) {
                                        var liste = "";
                                        $(this).children().each(function (i, val) {
                                            liste += (i + 1) + "#" + $(val).data("id") + ",";
                                        });
                                        liste = liste.slice(0, -1);
                                        $.ajax({
                                            type: 'POST',
                                            url: '/Headlines/HeadlinesManagement.aspx/SiralamaUpdate',
                                            data: "{'Data':'" + liste + "'}",
                                            contentType: "application/json; charset=utf-8",
                                            success: function (cevap) {
                                                location.reload();
                                            }
                                        });
                                    }
                                });
                                $("#sortable").disableSelection();
                            });
                        </script>
                        <style type="text/css">
                            .ui-sortable-placeholder {
                                float: left;
                                height: 100px;
                                padding: 4px;
                                margin: 2px 5px 5px 5px;
                                box-sizing: border-box;
                            }

                            .span12 .btn {
                                padding-right: 5px;
                                padding-left: 5px;
                            }
                        </style>
                        <table class="table table-hover table-nomargin">
                            <thead>
                                <tr>
                                    <th style="width: 5%;">Sıra</th>
                                    <th style="width: 25%;">Görsel</th>
                                    <th style="width: 45%;">Başlık</th>
                                    <th style="width: 20%;">Pozisyom</th>
                                    <th style="width: 5%;">İşlem</th>
                                </tr>
                            </thead>
                            <tbody id="sortable">
                                <asp:Repeater ID="rptAllNews" runat="server" OnItemCommand="rptAllNews_ItemCommand">
                                    <ItemTemplate>
                                        <tr class="<%#Eval("Status").ToString() == "0" ? "danger" : "" %> ui-sortable-handle" data-id="<%#Eval("HeadlineId") %>">
                                            <td>
                                                <%# Container.ItemIndex + 1 %>
                                            </td>
                                            <td><a href="/News/EditNews.aspx?newsId=<%#Eval("HeadlineId")%>">
                                                <img width="270" height="150" src="<%#Eval("ImageUrl") %>" /></a>
                                                <br />
                                                <br />
                                                <asp:FileUpload ID="fuGorsel" runat="server" />
                                            </td>
                                            <td>
                                                <span>Başlık:</span>
                                                <asp:TextBox CssClass="form-control" Style="width: 100%;" ID="txtTitle" runat="server" Text='<%# Helper.RemoveHtml(Eval("Title").ToString()) %>' />
                                                <br />
                                                <span>Spot:</span>
                                                <asp:TextBox CssClass="form-control" Style="width: 100%;" ID="txtSummary" runat="server" Text='<%#Eval("Description") %>' />
                                                <br />
                                                <span>Link:</span>
                                                <asp:TextBox CssClass="form-control" Style="width: 100%;" ID="txtUrl" runat="server" Text='<%#Eval("Url") %>' />
                                            </td>
                                            <td><span>Durum:</span>
                                                <asp:CheckBox CssClass="form-control" ID="cbStatus" Text="Aktif" Checked='<%#Eval("Status").ToString() == "0" ? false : true %>' Width="100%" runat="server" /></td>
                                            <td>
                                                <asp:Button Width="100%" CssClass="btn btn-primary form-control" Text="Güncelle" ID="btnGuncelle" CommandName="btnGuncelle" CommandArgument='<%#Eval("HeadlineId") %>' runat="server" /><br />
                                                <br />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
