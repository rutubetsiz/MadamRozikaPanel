<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EditArticle.aspx.cs" Inherits="MadamRozikaPanel.Authors.EditArticle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- CKEditor -->
    <script src="/static/js/plugins/ckeditor/ckeditor.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-header">
        <div class="pull-left">
            <h1><%=Baslik %></h1>
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
                <a href="<%=Link %>"><%=Baslik %></a>
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
            <div class="box box-bordered box-color">
                <div class="box-title">
                    <h3>
                        <i class="fa fa-th-list"></i><%=AltBaslik %></h3>
                </div>
                <div class="box-content nopadding">
                    <form id="formEditNews" runat="server" class='form-horizontal form-bordered'>
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <div class="form-group">
                            <label for="txtBaslik" class="control-label col-sm-1">Başlık</label>
                            <div class="col-sm-5">
                                <asp:TextBox ID="txtBaslik" class="form-control" runat="server" />
                            </div>
                            <label for="ddListYazarlar" class="control-label col-sm-1">Yazar</label>
                            <div class="col-sm-5">
                                <asp:DropDownList name="ddListYazarlar" ID="ddListYazarlar" class='form-control' runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="cbDurum" class="control-label col-sm-1">Durum</label>
                            <div class="col-sm-2">
                                <asp:CheckBox Text="Aktif" name="cbDurum" Checked="true" ID="cbDurum" runat="server" class=' checkbox-inline' />
                            </div>
                            <label for="cbYorum" class="control-label col-sm-1">Yorum</label>
                            <div class="col-sm-2">
                                <asp:CheckBox Text="Evet" name="cbYorum" Checked="true" ID="cbYorum" runat="server" class=' checkbox-inline' />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="ckEditor" class="control-label col-sm-1">Metin</label>
                            <div class="col-sm-11">
                                <textarea name="ckEditor" id="ckEditor" runat="server" class='ckeditor span12' rows="5"></textarea>
                            </div>
                        </div>
                        <div class="form-actions col-sm-offset-1 col-sm-10">
                            <asp:Button Text="Kaydet" ID="btnKaydet" runat="server" OnClick="btnKaydet_Click" class="btn btn-primary" />
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
