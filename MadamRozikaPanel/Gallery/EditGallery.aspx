<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EditGallery.aspx.cs" Inherits="MadamRozikaPanel.Gallery.EditGallery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".video-galeri").hide();
            $(".video-galeri-summary").hide();
            $(".ddListTur").change(function () {
                var value = $(".ddListTur").val();
                if (value == 1) {
                    $(".video-galeri").hide();
                    $(".video-galeri-summary").hide();
                    $(".galeri-gorselleri").show();
                    $(".gorsel-aciklamalari").show();
                }
                else if (value == 2) {
                    $(".video-galeri").show();
                    $(".video-galeri-summary").show();
                    $(".galeri-gorselleri").hide();
                    $(".gorsel-aciklamalari").hide();
                }
            });
        });
    </script>
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
                <a>Galeriler</a>
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
                            <div class="col-sm-11">
                                <asp:TextBox ID="txtBaslik" CssClass="form-control" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="ddListKategori" class="control-label col-sm-1">Kategori</label>
                            <div class="col-sm-2">
                                <asp:DropDownList name="ddListKategori" ID="ddListKategori" class='form-control' runat="server">
                                </asp:DropDownList>
                            </div>
                            <label for="ddListTur" class="control-label col-sm-1">Galeri Türü</label>
                            <div class="col-sm-2">
                                <asp:DropDownList name="ddListTur" ID="ddListTur" class='form-control ddListTur' runat="server">
                                    <asp:ListItem Text="Fotoğraf" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Video" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <label for="ddListAktifmi" class="control-label col-sm-1">Aktif mi?</label>
                            <div class="col-sm-2">
                                <asp:DropDownList name="ddListAktifmi" ID="ddListAktifmi" class='form-control' runat="server">
                                    <asp:ListItem Text="Hemen Yayınlansın" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Pasif Kalsın" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Sabah 7" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="FuGaleriGorseli" class="control-label col-sm-1">Galeri Resmi</label>
                            <div class="col-sm-11">
                                <asp:FileUpload runat="server" ID="FuGaleriGorseli" />
                            </div>
                        </div>
                        <div class="form-group galeri-gorselleri">
                            <label for="FuGaleriitems" class="control-label col-sm-1">Galeri Görselleri</label>
                            <div class="col-sm-11">
                                <asp:FileUpload AllowMultiple="true" ID="FuGaleriitems" runat="server" />
                            </div>
                        </div>
                        <div class="form-group gorsel-aciklamalari">
                            <label for="txtItemSummary" class="control-label col-sm-1">Görsel Açıklamaları</label>
                            <div class="col-sm-11">
                                <asp:TextBox ID="txtItemSummary" Height="300" TextMode="MultiLine" class="form-control" runat="server" />
                                <span class="help-block">Her bir görsel için tek satır kullanınız.</span>
                            </div>
                        </div>
                        <div class="form-group video-galeri">
                            <label for="FuVideo" class="control-label col-sm-1">Video</label>
                            <div class="col-sm-11">
                                <asp:FileUpload ID="FuVideo" runat="server" />
                            </div>
                        </div>
                        <div class="form-group video-galeri-summary">
                            <label for="txtVideoSummary" class="control-label col-sm-1">Video Açıklaması</label>
                            <div class="col-sm-11">
                                <asp:TextBox ID="txtVideoSummary" class="form-control" runat="server" />
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
