<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ReOrder.aspx.cs" Inherits="MadamRozikaPanel.News.ReOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-header">
        <div class="pull-left">
            <h1>Konumlandır</h1>
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
                <a href="/News/ReOrder.aspx">Konumlandır</a>
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
                        Haberler
                    </h3>
                </div>
                <div class="box-content nopadding">
                    <form id="formReOrder" runat="server" class='form-horizontal form-bordered'>
                        <div class="">
                            <p>
                                <asp:DropDownList ID="ddListTurler" CssClass="btn" AutoPostBack="true" OnSelectedIndexChanged="ddListTurler_SelectedIndexChanged" runat="server">
                                    <asp:ListItem Text="Manşet" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Kutu Haber" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </p>
                        </div>
                        <script>
                            $(function () {
                                $("#sortable").sortable({
                                    update: function (event, ui) {
                                        var liste = "";
                                        $(this).children().each(function (i, val) {
                                            liste += (i + 1) + "#" + $(val).data("id") + ",";
                                        });
                                        liste = liste.slice(0, -1);
                                        //alert(liste);
                                        //console.log(liste);
                                        $.ajax({ // ajax işlemi başlar
                                            type: 'POST', // veri gönderme tipimiz. get olabilirdi json olabilirdi. ama biz post kullanıyoruz
                                            url: '/News/ReOrder.aspx/SiralamaUpdate', // post edilecek adres
                                            data: "{'Data':'" + liste + "'}", //post edilecek veriler
                                            contentType: "application/json; charset=utf-8",
                                            success: function (cevap) {// işlem başarılıysa
                                                //console.log(cevap.d);
                                                //alert(cevap.d); //sonuc id'sine ajaxPost.php den dönen verileri basıyoruz.
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
                                    <th style="width: 10%;">ID</th>
                                    <th style="width: 56%;">Başlık</th>
                                    <th style="width: 20%;">Giriş Tarihi</th>
                                    <th style="width: 9%;">İşlem</th>
                                </tr>
                            </thead>
                            <tbody id="sortable">
                                <asp:Repeater ID="rptAllNews" runat="server" OnItemCommand="rptAllNews_ItemCommand">
                                    <ItemTemplate>
                                        <tr class="<%#Eval("Status").ToString() == "0" ? "danger" : "" %> ui-sortable-handle" data-id="<%#Eval("NewsId") %>">
                                            <td>
                                                <%# Container.ItemIndex + 1 %>
                                            </td>
                                            <td><a href="/News/EditNews.aspx?newsId=<%#Eval("NewsId")%>"><%#Eval("NewsId") %></a></td>
                                            <td>
                                                <span><%# Helper.RemoveHtml(Eval("Title").ToString()) %></span>
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
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
