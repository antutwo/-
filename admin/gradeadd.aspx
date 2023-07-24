<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gradeadd.aspx.cs" Inherits="tuixuan.admin.gradeadd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>管理员</title>
    <link rel="stylesheet" type="text/css" href="../css/common.css"/>
    <link rel="stylesheet" type="text/css" href="../css/main.css"/>
    <link href="../css/adminLogin.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/modernizr.min.js"></script>
   <!-- <script type="text/javascript" src="../Fonts"></script>-->
     <link rel="stylesheet" href="https://at.alicdn.com/t/font_2915269_pnq3bh4b96.css" />
    <!--<script src="../layui/layui.js" charset="utf-8"></script>-->
  
</head>
<body>
  <div class="topbar-wrap white">
    <div class="topbar-inner clearfix">
        <div class="topbar-logo-wrap clearfix">
            <h1 class="topbar-logo none"><a href="adminindex.aspx" class="navbar-brand">管理员</a></h1>
            <ul class="navbar-list clearfix">
                <li><a class="on" href="adminindex.aspx">首页</a></li>
            </ul>
        </div>
        <div class="top-info-wrap">
            <ul class="top-info-list clearfix">                   
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
                   <!-- <a href="#"><i class="icon-font">&#xe018;</i>系统管理</a>-->
                      <a href="#"><i class="iconfont icon-xitongguanli-copy"></i>系统管理</a>
                    <ul class="sub-menu">
                        <li><a href="adminindex.aspx" ><i class="iconfont icon-gerenxinxi"></i>个人信息</a></li>                         
                    </ul>
                </li>
                <li>
                    <a href="#"><i class="iconfont  icon-caidan"></i>基本操作</a>
                    <ul class="sub-menu">
                        <li><a href="teachermanage.aspx"><i class="iconfont icon-jiaoshiguanli2"></i>教师管理</a></li>
                        <li><a href="studentmanage.aspx" ><i class="iconfont icon-xueshengguanli"></i>学生管理</a></li>
                        <li><a href="grademanage.aspx" style="background-color:#009688;color:white"><i class="iconfont icon-banjiguanli"></i>班级管理</a></li>
                    </ul>
                </li>
               
            </ul>
        </div>
    </div>
     <div class="main-wrap">
        <div class="crumb-wrap">
            <div class="crumb-list"><i class="icon-font"></i><a href="adminindex.aspx">首页</a><span class="crumb-step">&gt;</span><a href="grademanage.aspx">班级管理</a><span class="crumb-step">&gt;</span><span class="crumb-name">新增班级</span></div>
        </div>
        <form id="login_form" runat="server">
        <div class="result-wrap">
            <div class="result-title">
               
            </div>
            <div class="result-content">
                <ul class="sys-info-list">
                    <li>
                        <label class="res-lab">班号</label><asp:TextBox ID="TextBox1" runat="server" class="common-text"
                            Height="25px" Width="341px"></asp:TextBox>
                    </li>
                    <li>
                        <label class="res-lab">班名</label><asp:TextBox ID="TextBox2" runat="server" class="common-text"
                            Height="25px" Width="341px"></asp:TextBox>
                    </li>
                    <li>
                        <label class="res-lab"></label>
                        <asp:Button ID="Button1" runat="server" class="btn btn-primary btn6 mr10"
                            Text="保存" Height="33px" Width="72px" onclick="Button1_Click" />

                            <asp:Button ID="Button2" runat="server" class="btn btn-success btn6 mr10"
                            Text="返回" Height="33px" Width="72px" onclick="Button2_Click"  />
                    </li>
                </ul>
            </div>
        </div>
        </form>
    </div>
    <!--/main-->
</div>

    <div id="foot">
       <p style="padding-top:20px;">版权所有：<span style="font-family:arial;">Copyright &copy;</span>湖南工业职业技术学院</p>
    </div>
</body>
</html>

