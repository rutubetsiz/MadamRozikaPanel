﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MadamRozikaPanel.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-header">
                    <div class="pull-left">
                        <h1>Anasayfa</h1>
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
                        </li>
                    </ul>
                    <div class="close-bread">
                        <a href="#">
                            <i class="fa fa-times"></i>
                        </a>
                    </div>
                </div>
    <div class="row">
        <div class="col-sm-6">
            <div class="box box-color box-bordered">
                <div class="box-title">
                    <h3>
                        <i class="fa fa-bar-chart-o"></i>
                        Audience Overview
								</h3>
                    <div class="actions">
                        <a href="#" class="btn btn-mini content-refresh">
                            <i class="fa fa-refresh"></i>
                        </a>
                        <a href="#" class="btn btn-mini content-remove">
                            <i class="fa fa-times"></i>
                        </a>
                        <a href="#" class="btn btn-mini content-slideUp">
                            <i class="fa fa-angle-down"></i>
                        </a>
                    </div>
                </div>
                <div class="box-content">
                    <div class="statistic-big">
                        <div class="top">
                            <div class="left">
                                <select name="category" class='chosen-select' data-nosearch="true" style="width: 150px;">
                                    <option value="1">Visits</option>
                                    <option value="2">New Visits</option>
                                    <option value="3">Unique Visits</option>
                                    <option value="4">Pageviews</option>
                                </select>
                            </div>
                            <div class="right">
                                8,195
										
                                    <span>
                                        <i class="fa fa-arrow-circle-up"></i>
                                    </span>
                            </div>
                        </div>
                        <div class="bottom">
                            <div class="flot medium" id="flot-audience"></div>
                        </div>
                        <div class="bottom">
                            <ul class="stats-overview">
                                <li>
                                    <span class="name">Visits
												</span>
                                    <span class="value">11,251
												</span>
                                </li>
                                <li>
                                    <span class="name">Pages / Visit
												</span>
                                    <span class="value">8.31
												</span>
                                </li>
                                <li>
                                    <span class="name">Avg. Duration
												</span>
                                    <span class="value">00:06:41
												</span>
                                </li>
                                <li>
                                    <span class="name">% New Visits
												</span>
                                    <span class="value">67,35%
												</span>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-6">
            <div class="box box-color lightred box-bordered">
                <div class="box-title">
                    <h3>
                        <i class="fa fa-bar-chart-o"></i>
                        HDD usage
								</h3>
                    <div class="actions">
                        <a href="#" class="btn btn-mini content-refresh">
                            <i class="fa fa-refresh"></i>
                        </a>
                        <a href="#" class="btn btn-mini content-remove">
                            <i class="fa fa-times"></i>
                        </a>
                        <a href="#" class="btn btn-mini content-slideUp">
                            <i class="fa fa-angle-down"></i>
                        </a>
                    </div>
                </div>
                <div class="box-content">
                    <div class="statistic-big">
                        <div class="top">
                            <div class="left">
                                <select name="category" class='chosen-select' data-nosearch="true" style="width: 150px;">
                                    <option value="1">Today</option>
                                    <option value="2">Yesterday</option>
                                    <option value="3">Last week</option>
                                    <option value="4">Last month</option>
                                </select>
                            </div>
                            <div class="right">
                                50%
										
                                    <span>
                                        <i class="fa fa-arrow-circle-right"></i>
                                    </span>
                            </div>
                        </div>
                        <div class="bottom">
                            <div class="flot medium" id="flot-hdd"></div>
                        </div>
                        <div class="bottom">
                            <ul class="stats-overview">
                                <li>
                                    <span class="name">Usage
												</span>
                                    <span class="value">50%
												</span>
                                </li>
                                <li>
                                    <span class="name">Usage % / User
												</span>
                                    <span class="value">0.031
												</span>
                                </li>
                                <li>
                                    <span class="name">Avg. Usage %
												</span>
                                    <span class="value">60%
												</span>
                                </li>
                                <li>
                                    <span class="name">Idle Usage %
												</span>
                                    <span class="value">12%
												</span>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <div class="box box-color box-bordered lightgrey">
                <div class="box-title">
                    <h3>
                        <i class="fa fa-check"></i>Tasks</h3>
                    <div class="actions">
                        <a href="#new-task" data-toggle="modal" class='btn btn--icon'>
                            <i class="fa fa-plus-circle"></i>Add Task</a>
                    </div>
                </div>
                <div class="box-content nopadding">
                    <ul class="tasklist">
                        <li class='bookmarked'>
                            <div class="check">
                                <input type="checkbox" class='icheck-me' data-skin="square" data-color="blue">
                            </div>
                            <span class="task">
                                <i class="fa fa-check"></i>
                                <span>Approve new users</span>
                            </span>
                            <span class="task-actions">
                                <a href="#" class='task-delete' rel="tooltip" title="Delete that task">
                                    <i class="fa fa-times"></i>
                                </a>
                                <a href="#" class='task-bookmark' rel="tooltip" title="Mark as important">
                                    <i class="fa fa-bookmark-empty"></i>
                                </a>
                            </span>
                        </li>
                        <li>
                            <div class="check">
                                <input type="checkbox" class='icheck-me' data-skin="square" data-color="blue">
                            </div>
                            <span class="task">
                                <i class="fa fa-bar-chart-o"></i>
                                <span>Check statistics</span>
                            </span>
                            <span class="task-actions">
                                <a href="#" class='task-delete' rel="tooltip" title="Delete that task">
                                    <i class="fa fa-times"></i>
                                </a>
                                <a href="#" class='task-bookmark' rel="tooltip" title="Mark as important">
                                    <i class="fa fa-bookmark-o"></i>
                                </a>
                            </span>
                        </li>
                        <li class='done'>
                            <div class="check">
                                <input type="checkbox" class='icheck-me' data-skin="square" data-color="blue" checked>
                            </div>
                            <span class="task">
                                <i class="fa fa-envelope"></i>
                                <span>Check for new mails</span>
                            </span>
                            <span class="task-actions">
                                <a href="#" class='task-delete' rel="tooltip" title="Delete that task">
                                    <i class="fa fa-times"></i>
                                </a>
                                <a href="#" class='task-bookmark' rel="tooltip" title="Mark as important">
                                    <i class="fa fa-bookmark-o"></i>
                                </a>
                            </span>
                        </li>
                        <li>
                            <div class="check">
                                <input type="checkbox" class='icheck-me' data-skin="square" data-color="blue">
                            </div>
                            <span class="task">
                                <i class="fa fa-comment"></i>
                                <span>Chat with John Doe</span>
                            </span>
                            <span class="task-actions">
                                <a href="#" class='task-delete' rel="tooltip" title="Delete that task">
                                    <i class="fa fa-times"></i>
                                </a>
                                <a href="#" class='task-bookmark' rel="tooltip" title="Mark as important">
                                    <i class="fa fa-bookmark-o"></i>
                                </a>
                            </span>
                        </li>
                        <li>
                            <div class="check">
                                <input type="checkbox" class='icheck-me' data-skin="square" data-color="blue">
                            </div>
                            <span class="task">
                                <i class="fa fa-retweet"></i>
                                <span>Go and tweet some stuff</span>
                            </span>
                            <span class="task-actions">
                                <a href="#" class='task-delete' rel="tooltip" title="Delete that task">
                                    <i class="fa fa-times"></i>
                                </a>
                                <a href="#" class='task-bookmark' rel="tooltip" title="Mark as important">
                                    <i class="fa fa-bookmark-o"></i>
                                </a>
                            </span>
                        </li>
                        <li>
                            <div class="check">
                                <input type="checkbox" class='icheck-me' data-skin="square" data-color="blue">
                            </div>
                            <span class="task">
                                <i class="fa fa-edit"></i>
                                <span>Write an article</span>
                            </span>
                            <span class="task-actions">
                                <a href="#" class='task-delete' rel="tooltip" title="Delete that task">
                                    <i class="fa fa-times"></i>
                                </a>
                                <a href="#" class='task-bookmark' rel="tooltip" title="Mark as important">
                                    <i class="fa fa-bookmark-o"></i>
                                </a>
                            </span>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-6">
            <div class="box">
                <div class="box-title">
                    <h3>
                        <i class="fa fa-bolt"></i>Server load</h3>
                    <div class="actions">
                        <a href="#" class="btn btn-mini content-refresh">
                            <i class="fa fa-refresh"></i>
                        </a>
                        <a href="#" class="btn btn-mini content-remove">
                            <i class="fa fa-times"></i>
                        </a>
                        <a href="#" class="btn btn-mini content-slideUp">
                            <i class="fa fa-angle-down"></i>
                        </a>
                    </div>
                </div>
                <div class="box-content">
                    <div class="flot flot-line"></div>
                </div>
            </div>
        </div>
        <div class="col-sm-6">
            <div class="box">
                <div class="box-title">
                    <h3>
                        <i class="fa fa-comment"></i>Chat</h3>
                    <div class="actions">
                        <a href="#" class="btn btn-mini content-refresh">
                            <i class="fa fa-refresh"></i>
                        </a>
                        <a href="#" class="btn btn-mini content-remove">
                            <i class="fa fa-times"></i>
                        </a>
                        <a href="#" class="btn btn-mini content-slideUp">
                            <i class="fa fa-angle-down"></i>
                        </a>
                    </div>
                </div>
                <div class="box-content nopadding scrollable" data-height="350" data-visible="true" data-start="bottom">
                    <ul class="messages">
                        <li class="left">
                            <div class="image">
                                <img src="img/demo/user-1.jpg" alt="">
                            </div>
                            <div class="message">
                                <span class="caret"></span>
                                <span class="name">Jane Doe</span>
                                <p>Lorem ipsum aute ut ullamco et nisi ad.</p>
                                <span class="time">12 minutes ago
											</span>
                            </div>
                        </li>
                        <li class="right">
                            <div class="image">
                                <img src="img/demo/user-2.jpg" alt="">
                            </div>
                            <div class="message">
                                <span class="caret"></span>
                                <span class="name">John Doe</span>
                                <p>Lorem ipsum aute ut ullamco et nisi ad. Lorem ipsum adipisicing nisi Excepteur eiusmod ex culpa laboris. Lorem ipsum est ut...</p>
                                <span class="time">12 minutes ago
											</span>
                            </div>
                        </li>
                        <li class="left">
                            <div class="image">
                                <img src="img/demo/user-1.jpg" alt="">
                            </div>
                            <div class="message">
                                <span class="caret"></span>
                                <span class="name">Jane Doe</span>
                                <p>Lorem ipsum aute ut ullamco et nisi ad. Lorem ipsum adipisicing nisi!</p>
                                <span class="time">12 minutes ago
											</span>
                            </div>
                        </li>
                        <li class="typing">
                            <span class="name">John Doe</span>is typing
									
                                <img src="img/loading.gif" alt="">
                        </li>
                        <li class="insert">
                            <form id="message-form" method="POST" action="#">
                                <div class="text">
                                    <input type="text" name="text" placeholder="Write here..." class="form-control">
                                </div>
                                <div class="submit">
                                    <button type="submit">
                                        <i class="fa fa-share"></i>
                                    </button>
                                </div>
                            </form>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-6">
            <div class="box">
                <div class="box-title">
                    <h3>
                        <i class="fa fa-globe"></i>User regions</h3>
                    <div class="actions">
                        <a href="#" class="btn btn-mini content-refresh">
                            <i class="fa fa-refresh"></i>
                        </a>
                        <a href="#" class="btn btn-mini content-remove">
                            <i class="fa fa-times"></i>
                        </a>
                        <a href="#" class="btn btn-mini content-slideUp">
                            <i class="fa fa-angle-down"></i>
                        </a>
                    </div>
                </div>
                <div class="box-content">
                    <div id="vmap"></div>
                </div>
            </div>
        </div>
        <div class="col-sm-6">
            <div class="box box-color box-bordered">
                <div class="box-title">
                    <h3>
                        <i class="fa fa-user"></i>Address Book</h3>
                    <div class="actions">
                        <a href="#" class="btn btn-mini content-refresh">
                            <i class="fa fa-refresh"></i>
                        </a>
                        <a href="#" class="btn btn-mini content-remove">
                            <i class="fa fa-times"></i>
                        </a>
                        <a href="#" class="btn btn-mini content-slideUp">
                            <i class="fa fa-angle-down"></i>
                        </a>
                    </div>
                </div>
                <div class="box-content nopadding scrollable" data-height="300" data-visible="true">
                    <table class="table table-user table-nohead">
                        <tbody>
                            <tr class="alpha">
                                <td class="alpha-val">
                                    <span>B</span>
                                </td>
                                <td colspan="2"></td>
                            </tr>
                            <tr>
                                <td class='img'>
                                    <img src="img/demo/user-1.jpg" alt="">
                                </td>
                                <td class='user'>Bi Doe</td>
                                <td class='icon'>
                                    <a href="#" class='btn'>
                                        <i class="fa fa-search"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td class='img'>
                                    <img src="img/demo/user-2.jpg" alt="">
                                </td>
                                <td class='user'>Boo Doe</td>
                                <td class='icon'>
                                    <a href="#" class='btn'>
                                        <i class="fa fa-search"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr class="alpha">
                                <td class="alpha-val">
                                    <span>D</span>
                                </td>
                                <td colspan="3"></td>
                            </tr>
                            <tr>
                                <td class='img'>
                                    <img src="img/demo/user-3.jpg" alt="">
                                </td>
                                <td class='user'>Dan Doe</td>
                                <td class='icon'>
                                    <a href="#" class='btn'>
                                        <i class="fa fa-search"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td class='img'>
                                    <img src="img/demo/user-4.jpg" alt="">
                                </td>
                                <td class='user'>Dane Doe</td>
                                <td class='icon'>
                                    <a href="#" class='btn'>
                                        <i class="fa fa-search"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr class="alpha">
                                <td class="alpha-val">
                                    <span>H</span>
                                </td>
                                <td colspan="3"></td>
                            </tr>
                            <tr>
                                <td class='img'>
                                    <img src="img/demo/user-3.jpg" alt="">
                                </td>
                                <td class='user'>Hilda N. Ervin</td>
                                <td class='icon'>
                                    <a href="#" class='btn'>
                                        <i class="fa fa-search"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr class="alpha">
                                <td class="alpha-val">
                                    <span>J</span>
                                </td>
                                <td colspan="3"></td>
                            </tr>
                            <tr>
                                <td class='img'>
                                    <img src="img/demo/user-5.jpg" alt="">
                                </td>
                                <td class='user'>John Doe</td>
                                <td class='icon'>
                                    <a href="#" class='btn'>
                                        <i class="fa fa-search"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td class='img'>
                                    <img src="img/demo/user-6.jpg" alt="">
                                </td>
                                <td class='user'>John Doe</td>
                                <td class='icon'>
                                    <a href="#" class='btn'>
                                        <i class="fa fa-search"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr class="alpha">
                                <td class="alpha-val">
                                    <span>L</span>
                                </td>
                                <td colspan="3"></td>
                            </tr>
                            <tr>
                                <td class='img'>
                                    <img src="img/demo/user-5.jpg" alt="">
                                </td>
                                <td class='user'>Laura J. Brown</td>
                                <td class='icon'>
                                    <a href="#" class='btn'>
                                        <i class="fa fa-search"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td class='img'>
                                    <img src="img/demo/user-6.jpg" alt="">
                                </td>
                                <td class='user'>Lilly J. Tooley</td>
                                <td class='icon'>
                                    <a href="#" class='btn'>
                                        <i class="fa fa-search"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr class="alpha">
                                <td class="alpha-val">
                                    <span>M</span>
                                </td>
                                <td colspan="3"></td>
                            </tr>
                            <tr>
                                <td class='img'>
                                    <img src="img/demo/user-1.jpg" alt="">
                                </td>
                                <td class='user'>Maxi Doe</td>
                                <td class='icon'>
                                    <a href="#" class='btn'>
                                        <i class="fa fa-search"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td class='img'>
                                    <img src="img/demo/user-2.jpg" alt="">
                                </td>
                                <td class='user'>Max Doe</td>
                                <td class='icon'>
                                    <a href="#" class='btn'>
                                        <i class="fa fa-search"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr class="alpha">
                                <td class="alpha-val">
                                    <span>O</span>
                                </td>
                                <td colspan="3"></td>
                            </tr>
                            <tr>
                                <td class='img'>
                                    <img src="img/demo/user-1.jpg" alt="">
                                </td>
                                <td class='user'>Oxx Doe</td>
                                <td class='icon'>
                                    <a href="#" class='btn'>
                                        <i class="fa fa-search"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td class='img'>
                                    <img src="img/demo/user-2.jpg" alt="">
                                </td>
                                <td class='user'>Osam Doe</td>
                                <td class='icon'>
                                    <a href="#" class='btn'>
                                        <i class="fa fa-search"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr class="alpha">
                                <td class="alpha-val">
                                    <span>P</span>
                                </td>
                                <td colspan="3"></td>
                            </tr>
                            <tr>
                                <td class='img'>
                                    <img src="img/demo/user-1.jpg" alt="">
                                </td>
                                <td class='user'>Petra Doe</td>
                                <td class='icon'>
                                    <a href="#" class='btn'>
                                        <i class="fa fa-search"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td class='img'>
                                    <img src="img/demo/user-2.jpg" alt="">
                                </td>
                                <td class='user'>Per Doe</td>
                                <td class='icon'>
                                    <a href="#" class='btn'>
                                        <i class="fa fa-search"></i>
                                    </a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-6">
            <div class="box">
                <div class="box-title">
                    <h3>
                        <i class="fa fa-calendar"></i>My calendar</h3>
                </div>
                <div class="box-content nopadding">
                    <div class="calendar"></div>
                </div>
            </div>
        </div>
        <div class="col-sm-6">
            <div class="box box-color box-bordered green">
                <div class="box-title">
                    <h3>
                        <i class="fa fa-bullhorn"></i>Feeds</h3>
                    <div class="actions">
                        <a href="#" class="btn btn-mini custom-checkbox checkbox-active">Automatic refresh
									
                                <i class="fa fa-square-o"></i>
                        </a>
                    </div>
                </div>
                <div class="box-content nopadding scrollable" data-height="400" data-visible="true">
                    <table class="table table-nohead" id="randomFeed">
                        <tbody>
                            <tr>
                                <td>
                                    <span class="label label-default"><i class="fa fa-plus-square"></i></span>
                                    <a href="#">John Doe</a>added a new photo</td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="label label-success"><i class="fa fa-user"></i></span>New user registered</td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="label label-info"><i class="fa fa-shopping-cart"></i></span>New order received</td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="label label-warning"><i class="fa fa-comment"></i></span><a href="#">John Doe</a>commented on<a href="#">News #123</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="label label-success"><i class="fa fa-user"></i></span>New user registered</td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="label label-info"><i class="fa fa-shopping-cart"></i></span>New order received</td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="label label-warning"><i class="fa fa-comment"></i></span><a href="#">John Doe</a>commented on<a href="#">News #123</a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>