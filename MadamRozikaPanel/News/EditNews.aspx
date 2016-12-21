<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EditNews.aspx.cs" Inherits="MadamRozikaPanel.News.EditNews" %>
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
                <a>Haberler</a>
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
                            <label for="txtMansetBaslik" class="control-label col-sm-1">Başlık</label>
                            <div class="col-sm-11">
                                <asp:TextBox ID="txtMansetBaslik" name="txtMansetBaslik" class="form-control" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="txtSeoTitle" class="control-label col-sm-1">Seo Başlık</label>
                            <div class="col-sm-11">
                                <asp:TextBox ID="txtSeoTitle" name="txtSeoTitle" class="form-control" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="txtOzet" class="control-label col-sm-1">Özet</label>
                            <div class="col-sm-11">
                                <asp:TextBox ID="txtOzet" name="txtOzet" class="form-control" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="txtEtiketler" class="control-label col-sm-1">Etiketler</label>
                            <div class="col-sm-5">
                                <asp:TextBox ID="txtEtiketler" name="txtEtiketler" class="form-control" runat="server" />
                            </div>
                            <label for="ddListKategori" class="control-label col-sm-1">Kategori</label>
                            <div class="col-sm-2">
                                <asp:DropDownList name="ddListKategori" ID="ddListKategori" class='form-control' runat="server">
                                </asp:DropDownList>
                            </div>
                            <label for="ddListAktifmi" class="control-label col-sm-1">Aktif mi?</label>
                            <div class="col-sm-2">
                                <asp:DropDownList name="ddListAktifmi" ID="ddListAktifmi" class='form-control' runat="server">
                                    <asp:ListItem Text="Aktif" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Pasif" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Sabah 7" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="fuHaberGorseli" class="control-label col-sm-1">Haber Görseli</label>
                            <div class="col-sm-2">
                                <asp:FileUpload runat="server" ID="fuHaberGorseli" />
                            </div>
                            <label for="cbYorum" class="control-label col-sm-1">Yorum</label>
                            <div class="col-sm-2">
                                <asp:CheckBox Text="Evet" name="cbYorum" Checked="true" ID="cbYorum" runat="server" class=' checkbox' />
                            </div>
                            <label for="cbManset" class="control-label col-sm-1">Tip</label>
                            <div class="col-sm-2">
                                <asp:DropDownList name="ddListTip" ID="ddListTip" class='form-control' runat="server">
                                    <asp:ListItem Text="Manşet" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Üçlü Manşet" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Tepe Manşet" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <label for="ddlYorumAktifmi" class="control-label col-sm-1">Yorum Aktif mi?</label>
                            <div class="col-sm-2">
                                <asp:DropDownList name="ddlYorumAktifmi" ID="DropDownList1" class='form-control' runat="server">
                                    <asp:ListItem Text="Aktif" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Pasif" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="cblDigerKategoriler" class="control-label col-sm-1">Diğer Kategoriler</label>
                            <div class="col-sm-8">
                                <asp:CheckBoxList runat="server" ID="cblDigerKategoriler" CssClass="checkbox" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                </asp:CheckBoxList>
                            </div>
                             <label for="ddlYazarAktifmi" class="control-label col-sm-1">Yazar Aktif mi?</label>
                            <div class="col-sm-2">
                                <asp:DropDownList name="ddlYazarAktifmi" ID="DropDownList2" class='form-control' runat="server">
                                    <asp:ListItem Text="Aktif" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Pasif" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row" style="margin: 0px!important;">
                            <div class="col-sm-8 nopadding">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <textarea name="ckEditor" id="ckEditor" runat="server" class='ckeditor span12' rows="5"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 nopadding">
                                <div class="form-group">
                                    <label for="fuVideo" class="control-label col-sm-2">Video</label>
                                    <div class="col-sm-10">
                                        <ajaxToolkit:AjaxFileUpload ID="fuVideo" OnUploadComplete="fuVideo_UploadComplete" runat="server" />

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="fuResim" class="control-label col-sm-2">Metin İçi Resim</label>
                                    <div class="col-sm-10">
                                        <ajaxToolkit:AjaxFileUpload ID="fuResim" OnUploadComplete="fuResim_UploadComplete" runat="server" OnClientUploadComplete="imgUploaded" Mode="Client" />
                                    </div>
                                </div>
                                <div class="form-group" id="galeriEkleButon" runat="server">
                                    <div class="col-sm-12">
                                        <input class="btn btn-primary btnGaleriEkle" id="btnGaleriEkle" type="button" value="Galeri ekle" name="btnGaleriEkle" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group galeriPanel">
                            <label for="cbFuGaleri" class="control-label col-sm-3">Haber Galerisi Ekle</label>
                            <div class="col-sm-9">
                                <asp:FileUpload AllowMultiple="true" ID="fuGaleri" runat="server" />
                            </div>
                            <label for="txtItemSummary" class="control-label col-sm-3">Görsel Açıklamaları</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtItemSummary" name="txtItemSummary" TextMode="MultiLine" class="form-control" runat="server" />
                                <span class="help-block">Her bir görsel için tek satır kullanınız.</span>
                            </div>
                            <div class="form-actions col-sm-offset-3 col-sm-9">
                                <asp:Button Text="Galeri Kaydet" ID="btnGaleriKaydet" runat="server" OnClick="btnGaleriKaydet_Click" class="btn btn-primary" />
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
    <script type="text/javascript">
        function imgUploaded(sender, args) {
            var d = new Date();
            var year = d.getFullYear();
            var month = d.getMonth() + 1;
            var day = d.getDate();
            var url = getPath() + args._fileName;

            CKEDITOR.instances['ContentPlaceHolder1_ckEditor'].insertHtml('<img alt="" src="' + url + '" />');
        }

        function getPath() {
            return '/Files/images/news/' + getDatePath() + '/' + getDatePathForInline();
        }

        function getDatePath() {
            var d = new Date();
            var year = d.getFullYear().toString();
            var month = (d.getMonth() + 1).toString();
            var day = d.getDate().toString();
            return year + '/' + month + '/' + day;
        }
        function getDatePathForInline() {
            var d = new Date();
            var year = d.getFullYear().toString();
            var month = (d.getMonth() + 1).toString();
            var day = d.getDate().toString();
            var hours = (d.getHours()).toString();
            return year + '_' + month + '_' + day + '_' + hours + '_';
        }
        $(document).ready(function () {
            $(".galeriPanel").hide();
            $(".btnGaleriEkle").click(function () {
                $(".galeriPanel").show();
            });
            AjaxFileUpload_change_text();
        });
        function AjaxFileUpload_change_text() {
            Sys.Extended.UI.Resources.AjaxFileUpload_SelectFile = "Dosya Seç";
            Sys.Extended.UI.Resources.AjaxFileUpload_DropFiles = "Buraya sürükle";
            Sys.Extended.UI.Resources.AjaxFileUpload_Pending = "bekliyor";
            Sys.Extended.UI.Resources.AjaxFileUpload_Remove = "Sil";
            Sys.Extended.UI.Resources.AjaxFileUpload_Upload = "Yükle";
            Sys.Extended.UI.Resources.AjaxFileUpload_Uploaded = "Yüklendi";
            Sys.Extended.UI.Resources.AjaxFileUpload_UploadedPercentage = "yüklenen {0} %";
            Sys.Extended.UI.Resources.AjaxFileUpload_Uploading = "Yükleniyor";
            Sys.Extended.UI.Resources.AjaxFileUpload_FileInQueue = "{0} dosya kuyrukta.";
            Sys.Extended.UI.Resources.AjaxFileUpload_AllFilesUploaded = "Tüm dosyalar yüklendi.";
            Sys.Extended.UI.Resources.AjaxFileUpload_FileList = "Yüklenen Dosyalar:";
            Sys.Extended.UI.Resources.AjaxFileUpload_SelectFileToUpload = "";
            Sys.Extended.UI.Resources.AjaxFileUpload_Cancelling = "İpta ediliyor...";
            Sys.Extended.UI.Resources.AjaxFileUpload_UploadError = "Dosya yükleme sırasında bir sorun oluştu.";
            Sys.Extended.UI.Resources.AjaxFileUpload_CancellingUpload = "Yükleme iptal ediliyor...";
            Sys.Extended.UI.Resources.AjaxFileUpload_UploadingInputFile = "Yükelenen dosya: {0}.";
            Sys.Extended.UI.Resources.AjaxFileUpload_Cancel = "İptal";
            Sys.Extended.UI.Resources.AjaxFileUpload_Canceled = "iptal edildi";
            Sys.Extended.UI.Resources.AjaxFileUpload_UploadCanceled = "Dosya yükleme iptal edildi";
            Sys.Extended.UI.Resources.AjaxFileUpload_DefaultError = "Dosya yükleme hatası";
            Sys.Extended.UI.Resources.AjaxFileUpload_UploadingHtml5File = "Yüklenen dosya: {0} boyutunun {1} byteı yüklendi.";
            Sys.Extended.UI.Resources.AjaxFileUpload_error = "hata";
        }
    </script>
</asp:Content>