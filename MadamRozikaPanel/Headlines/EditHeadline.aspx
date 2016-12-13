<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EditHeadline.aspx.cs" Inherits="MadamRozikaPanel.Headlines.EditHeadline" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="page-header">
        <div class="pull-left">
            <h1><%=Baslik %></h1>
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
                <a>Manşet Yönetimi</a>
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
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtBaslik" class="form-control" runat="server" />
                            </div>
                            <label for="txtSpot" class="control-label col-sm-1">Spot</label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtSpot" class="form-control" runat="server" />
                            </div>
                            <label for="txtUrl" class="control-label col-sm-1">Link</label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtUrl" class="form-control" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="cbDurum" class="control-label col-sm-1">Durum</label>
                            <div class="col-sm-3">
                                <asp:CheckBox Text="Aktif" name="cbDurum" Checked="true" ID="cbDurum" runat="server" class=' checkbox-inline' />
                            </div>
                            <label for="FuGaleriGorseli" class="control-label col-sm-1">Manşet Resmi</label>
                            <div class="col-sm-3">
                                <asp:FileUpload runat="server" ID="FuManset" />
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