﻿<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="voteDetails.aspx.cs" Inherits="tuixuan.teach.voteDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>教师</title>
    <link rel="stylesheet" type="text/css" href="../css/common.css"/>
    <link rel="stylesheet" type="text/css" href="../css/main.css"/>
     <link href="../css/adminLogin.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../js/modernizr.min.js"></script>
   <!-- <script type="text/javascript" src="../Fonts"></script>-->
     <link rel="stylesheet" href="https://at.alicdn.com/t/font_2915269_pnq3bh4b96.css" />
    <!--<script src="../layui/layui.js" charset="utf-8"></script>-->
  
</head>
<body>
    <form id="form1" runat="server">
  <div class="topbar-wrap white">
    <div class="topbar-inner clearfix">
        <div class="topbar-logo-wrap clearfix">
            <h1 class="topbar-logo none"><a href="teachindex.aspx" class="navbar-brand">教师管理</a></h1>
            <ul class="navbar-list clearfix">
                <li><a class="on" href="teachindex.aspx">教师首页</a></li>
            </ul>
        </div>
        <div class="top-info-wrap">
            <ul class="top-info-list clearfix">
                <li> 
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label> </li>
                <li><a href="../Default.aspx">退出登录</a></li>
            </ul>
        </div>
    </div>
</div>
<div class="container clearfix">
    <div class="sidebar-wrap">
        <div class="sidebar-title">
            <h1>菜单</h1>
        </div>
        <div class="sidebar-content">
            <ul class="sidebar-list">
                <li>
                    <a href="#"><i class="iconfont icon-jiaoshijibenxinxiguanli"></i>信息管理</a>
                    <ul class="sub-menu">
                        <li><a href="teachindex.aspx"><i class="iconfont icon-gerenxinxi"></i>个人信息</a></li>
                        <li><a href="teachGrade.aspx"><i class="iconfont icon-banjixinxi"></i>班级信息</a></li>
                    </ul>
                </li>
                <li>
                    <a href="#"><i class="iconfont icon-caidan"></i>基本操作</a>
                    <ul class="sub-menu">
                        <li><a href="voteManage.aspx"><i class="iconfont icon-toupiaoguanli"></i>投票管理</a></li>
                         <li><a href="electManage.aspx"><i class="iconfont icon-xitongguanli_yonghuguanli"></i>评选管理</a></li>
                         <li><a href="candidateManage.aspx"><i class="iconfont icon-canyutoupiao"></i>候选管理</a></li>
                        <li><a href="voteDetails.aspx" style="background-color:#009688;color:white"><i class="iconfont icon-chakanxinxi"></i>查看投票信息</a></li>
                        <li><a href="recommendManage.aspx"><i class="iconfont icon-menu_tpgl"></i>推荐管理</a></li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
    <!--/sidebar-->
    <!--/main-->

    
   <div class="main-wrap">

        <div class="crumb-wrap">
            <div class="crumb-list"><i class="icon-font"></i><a href="teachindex.aspx">首页</a><span class="crumb-step">&gt;</span><span class="crumb-name">投票信息</span></div>
        </div>
        <div class="search-wrap">
            <div class="search-content">
                    <table class="search-tab">
                        <tr>
                            <th width="70">关键字:</th>
                            <td><asp:DropDownList class="common-text" ID="findinfoDropdl" runat="server"   Height="25px" Width="140px">
                                </asp:DropDownList></td>
                            <td style="align-content:center"><asp:Button ID="Button1" class="btn btn-primary btn2" runat="server" Text="查询" 
                                    style="margin:auto;" onclick="Button1_Click" /></td>
                            <td></td>
                        </tr>
                    </table>
            </div>
        </div>
        <div class="result-wrap">
                <div class="result-title">
                    <div class="result-list">
                        <asp:Button ID="DaoChu" runat="server"  class="btn btn-success btn2"  style="margin:auto; float:right; text-align:center;" Text="导出" OnClick="DaoChu_Click"/>
                    </div>
                </div>

                <div class="result-content">
                     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="15" 
                          GridLines="None"  AllowPaging="True"  CssClass="result-tab" 
                         DataKeyNames="id" Width="100%" 
                         onpageindexchanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand">
                         <Columns>
                             <asp:BoundField DataField="position_id" HeaderText="职位ID" Visible="False" />
                            <asp:BoundField DataField="position_name" HeaderText="项目" ReadOnly="True" />
                            <asp:BoundField DataField="state" HeaderText="评选状态" ReadOnly="True" />
                           
                             <asp:BoundField DataField="candidate_num" HeaderText="在候选人数" ReadOnly="True" />
                           
                             <asp:BoundField DataField="maximun" HeaderText="最大候选人数" ReadOnly="True" >
                             <ControlStyle Width="20px" />
                             </asp:BoundField>
                             <asp:BoundField DataField="position_vote" HeaderText="可投" ReadOnly="True">
                             <ControlStyle Width="20px" />
                             </asp:BoundField>
                             <asp:BoundField DataField="position_votes" HeaderText="已投票数" ReadOnly="True" />
                             <asp:BoundField DataField="elect_num" HeaderText="评议人数" ReadOnly="True" >
                           
                             <ControlStyle Width="20px" />
                             </asp:BoundField>
                           
                             <asp:ButtonField ButtonType="Button" CommandName="btn_Vote" HeaderText="查看" Text="投票信息" />
                             <asp:ButtonField ButtonType="Button" CommandName="btn_NotVote" HeaderText="查看" Text="未投票信息" />
                        </Columns>
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="Black" />

                   </asp:GridView>
                </div>

        </div>
    </div>


</div>

</form>

    <div id="foot">
       <p style="padding-top:20px;">版权所有：<span style="font-family:arial;">Copyright &copy;</span>湖南工业职业技术学院</p>
    </div>
</body>
</html>