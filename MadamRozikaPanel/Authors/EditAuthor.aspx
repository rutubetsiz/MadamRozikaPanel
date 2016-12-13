<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EditAuthor.aspx.cs" Inherits="MadamRozikaPanel.Authors.EditAuthor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                <a>Yazarlar</a>
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
                            <label for="txtBaslik" class="control-label col-sm-1">Ad</label>
                            <div class="col-sm-5">
                                <asp:TextBox ID="txtAd" class="form-control" runat="server" />
                            </div>
                            <label for="txtEtiketler" class="control-label col-sm-1">Mail</label>
                            <div class="col-sm-5">
                                <asp:TextBox ID="txtMail" class="form-control" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="txtTwitter" class="control-label col-sm-1">Twitter</label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtTwitter" class="form-control" runat="server" />
                            </div>
                            <label for="txtFacebook" class="control-label col-sm-1">Facebook</label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtFacebook" class="form-control" runat="server" />
                            </div>
                            <label for="txtLinkedin" class="control-label col-sm-1">Linkedin</label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtLinkedin" class="form-control" runat="server" />
                            </div>
                            <label for="txtEmbed" class="control-label col-sm-1">Embed</label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtEmbed" class="form-control" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="cbDurum" class="control-label col-sm-1">Durum</label>
                            <div class="col-sm-2">
                                <asp:CheckBox Text="Aktif" name="cbDurum" Checked="true" ID="cbDurum" runat="server" class=' checkbox-inline' />
                            </div>
                            <label for="cbAnaSayfa" class="control-label col-sm-1">Anasayfa</label>
                            <div class="col-sm-2">
                                <asp:CheckBox Text="Evet" name="cbAnaSayfa" Checked="true" ID="cbAnaSayfa" runat="server" class=' checkbox-inline' />
                            </div>
                            <label for="FuGaleriGorseli" class="control-label col-sm-1">Yazar Resmi</label>
                            <div class="col-sm-5">
                                <asp:FileUpload runat="server" ID="FuAuthor" />
                            </div>
                        </div>
                        <div class="form-group">
                            <%--<label for="cblSite" class="control-label col-sm-1">Site</label>
                            <div class="col-sm-2">
                                <asp:CheckBoxList runat="server" ID="cblSite" CssClass="checkbox-inline" RepeatLayout="Flow">
                                </asp:CheckBoxList>
                            </div>--%>
                            <label for="ddListKategori" class="control-label col-sm-1">Kategori</label>
                            <div class="col-sm-2">
                                <asp:DropDownList name="ddListKategori" ID="ddListKategori" class='form-control' runat="server">
                                </asp:DropDownList>
                            </div>
                            <label for="ddListSira" class="control-label col-sm-1">Sıra</label>
                            <div class="col-sm-2">
                                <asp:DropDownList name="ddListSira" ID="ddListSira" class='form-control' runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
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
