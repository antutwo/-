<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="studindex.aspx.cs" Inherits="tuixuan.student.studindex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>学生</title>
    <link rel="stylesheet" type="text/css" href="../css/common.css"/>
    <link rel="stylesheet" type="text/css" href="../css/main.css"/>
    <link href="../css/adminLogin.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/modernizr.min.js"></script>

   <!-- <script type="text/javascript" src="../Fonts"></script>-->
     <link rel="stylesheet" href="https://at.alicdn.com/t/font_2915269_pnq3bh4b96.css" />
   
    <script src="../layui/layui.js" charset="utf-8"></script>
    <style>
        .btn{
            float:right;
            width:150px;
            background:#009688;
            color:white
        }
       
    </style>
</head>
<body>
<div class="topbar-wrap white">
    <div class="topbar-inner clearfix">
        <div class="topbar-logo-wrap clearfix">
            <h1 class="topbar-logo none"><a href="studindex.aspx" class="navbar-brand">学生管理</a></h1>
            <ul class="navbar-list clearfix">
                <li><a class="on" href="studindex.aspx">学生首页</a></li>
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
                    <a href="#"><i class="iconfont icon-xueshengguanli-01"></i>信息管理</a>
                    <ul class="sub-menu">
                        <li><a href="studindex.aspx" style="background-color:#009688;color:white"><i class="iconfont icon-gerenxinxi"></i>个人信息</a></li>
                        <li><a href="stuGrade.aspx"><i class="iconfont icon-banjixinxi"></i>班级信息</a></li>
                        <li><a href="candidate.aspx"><i class="iconfont icon-xueshengjibenxinxiguanli"></i>我的候选信息</a></li>
                    </ul>
                </li>
                <li>
                    <a href="#"><i class="iconfont icon-toupiaoguanli"></i>投票管理</a>
                    <ul class="sub-menu">
                        <li><a href="studvotes.aspx"><i class="iconfont icon-canyutoupiao"></i>参与投票</a></li>
                         <li><a href="studvoteDetails.aspx"><i class="iconfont icon-toupiaoshu"></i>投票信息</a></li>
                        
                    </ul>
                </li>
                <li>
                    <a href="#"><i class="iconfont icon-tuijianren1"></i>推荐管理</a>
                    <ul class="sub-menu">
                        <li><a href="studrecommend.aspx"><i class="iconfont icon-info"></i>推荐信息</a></li>
                         <li><a href="stuRecommendDetails.aspx"><i class="iconfont icon-12"></i>推荐详情</a></li>
                        
                    </ul>
                </li>
            </ul>
        </div>
    </div>
    <!--/sidebar-->
    <div class="main-wrap">
        <div class="crumb-wrap">
            <div class="crumb-list"><i class="icon-font"></i><a href="studindex.aspx">首页</a><span class="crumb-step">&gt;</span><span class="crumb-name">个人信息</span></div>
        </div>
        <form id="login_form" runat="server">
        <div class="result-wrap">
            <div class="result-title">
               
            </div>
            <div class="result-content">
                <ul class="sys-info-list">
                    <li>
                        <label class="res-lab">学号</label><asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>
                          
                    </li>
                    <li>
                        <label class="res-lab">姓名</label><asp:Label ID="lblname" runat="server" Text="Label"></asp:Label>
                        
                    </li>
                    <li>
                        <label class="res-lab">性别</label><asp:Label ID="lblsex" runat="server" Text="Label"></asp:Label>
                        
                    </li>
                    <li>
                        <label class="res-lab">密码</label><asp:Label ID="lblpwd" runat="server" Text="Label"></asp:Label>
                       
                    </li>
                    <li>
                        <label class="res-lab">班号</label><asp:Label ID="lblgid" runat="server" Text="Label"></asp:Label>
                    </li>
                    <li>
                        <label class="res-lab">班级</label><asp:Label ID="lblgname" runat="server" Text="Label"></asp:Label>
                        
                    </li>
                     <li>
                        <label class="res-lab"></label>
                        <asp:Button ID="btn" runat="server" class="btn" Text="修改" onclick="Button1_Click" />
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
