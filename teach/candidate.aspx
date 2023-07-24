﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="candidate.aspx.cs" Inherits="tuixuan.teach.candidate" %>

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
                         <li><a href="candidateManage.aspx"  style="background-color:#009688;color:white"><i class="iconfont icon-canyutoupiao"></i>候选管理</a></li>
                        <li><a href="voteDetails.aspx"><i class="iconfont icon-chakanxinxi"></i>查看投票信息</a></li>
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
            <div class="crumb-list"><i class="icon-font"></i><a href="teachindex.aspx">首页</a><span class="crumb-step">&gt;</span><a href="candidateManage.aspx">候选管理</a><span class="crumb-step">&gt;</span><span class="crumb-name">候选人</span></div>
        </div>

        <div class="result-wrap">
               
            <div align="center">
            <asp:Label ID="title" runat="server" Font-Bold="False" Font-Size="X-Large"></asp:Label>
            </div>
                <div class="result-content">
                     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="15" 
                          GridLines="None"  AllowPaging="True"  CssClass="result-tab" 
                         DataKeyNames="id" Width="100%" 
                         onpageindexchanging="GridView1_PageIndexChanging">
                         <Columns>
<asp:BoundField DataField="candidate_id" HeaderText="候选人ID" Visible="False"></asp:BoundField>
                             <asp:BoundField DataField="position" HeaderText="项目" />
                            <asp:BoundField DataField="stu_id" HeaderText="候选人学号" ReadOnly="True" />
                            <asp:BoundField DataField="candidate_name" HeaderText="候选人姓名" />
                           
                             <asp:BoundField DataField="grade_id" HeaderText="班号" />
                             <asp:BoundField DataField="votes" HeaderText="获票数" />
                             <asp:BoundField DataField="candidate_state" HeaderText="候选状态" />
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
