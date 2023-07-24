<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addElectTwo.aspx.cs" Inherits="tuixuan.teach.addElectTwo" %>

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
  
     <style type="text/css">
         .auto-style1 {
             margin-bottom: 0;
         }
     </style>
  
</head>
<body>
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
                        <li><a href="electManage.aspx"  style="background-color:#009688;color:white"><i class="iconfont icon-xitongguanli_yonghuguanli"></i>评选管理</a></li>
                         <li><a href="candidateManage.aspx"><i class="iconfont icon-canyutoupiao"></i>候选管理</a></li>
                        <li><a href="voteDetails.aspx"><i class="iconfont icon-chakanxinxi"></i>查看投票信息</a></li>
                        <li><a href="recommendManage.aspx"><i class="iconfont icon-menu_tpgl"></i>推荐管理</a></li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
     <div class="main-wrap">
        <div class="crumb-wrap">
            <div class="crumb-list"><i class="icon-font"></i><a href="teachindex.aspx">首页</a><span class="crumb-step">&gt;</span><a href="electManage.aspx">评选管理</a><span class="crumb-step">&gt;</span><span class="crumb-name">新增评选人</span></div>
        </div>
        <form id="login_form" runat="server">
        <div class="result-wrap">
            <div class="result-title">
               
            </div>
            <div class="result-content">
                <ul class="sys-info-list">
                      <li>
                        <label class="res-lab">项目</label><asp:DropDownList ID="DropDownList1" runat="server"  class="common-text"
                            Height="25px" Width="341px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label class="res-lab">学号</label><asp:DropDownList ID="DropDownList3" runat="server"  class="common-text"
                            Height="25px" Width="341px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label class="res-lab">姓名</label><asp:TextBox ID="TextBox1" runat="server" class="common-text"
                            Height="25px" Width="341px" ReadOnly="True"></asp:TextBox>
                    </li>
                     <li>
                        <label class="res-lab">票数</label><asp:TextBox ID="TextBox2" runat="server" class="common-text"
                            Height="25px" Width="341px" ReadOnly="True"></asp:TextBox>
                    </li>
                     <li>
                        <label class="res-lab">班号</label><asp:TextBox ID="TextBox3" runat="server" class="common-text"
                            Height="25px" Width="341px" ReadOnly="True"></asp:TextBox>
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
