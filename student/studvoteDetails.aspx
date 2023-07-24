<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="studvoteDetails.aspx.cs" Inherits="tuixuan.student.studvoteDetails" %>

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
</head>
<body>
    <form id="form1" runat="server">
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
                        <li><a href="studindex.aspx"><i class="iconfont icon-gerenxinxi"></i>个人信息</a></li>
                        <li><a href="stuGrade.aspx"><i class="iconfont icon-banjixinxi"></i>班级信息</a></li>
                        <li><a href="candidate.aspx"><i class="iconfont icon-xueshengjibenxinxiguanli"></i>我的候选信息</a></li>
                    </ul>
                </li>
                <li>
                    <a href="#"><i class="iconfont icon-toupiaoguanli"></i>投票管理</a>
                    <ul class="sub-menu">
                        <li><a href="studvotes.aspx"><i class="iconfont icon-canyutoupiao"></i>参与投票</a></li>
                         <li><a href="studvoteDetails.aspx"  style="background-color:#009688;color:white"><i class="iconfont icon-toupiaoshu"></i>投票信息</a></li>
                        
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
<div class="main-wrap">

        <div class="crumb-wrap">
            <div class="crumb-list"><i class="icon-font"></i><a href="studindex.aspx">首页</a><span class="crumb-step">&gt;</span><span class="crumb-name">投票详情</span></div>
        </div>
        <div class="search-wrap">
            <div class="search-content">
                    <table class="search-tab">
                        <tr>
                            <th width="70">项目:</th>
                            <!--<td><asp:TextBox class="common-text" placeholder="关键字" ID="findinfo" runat="server" Type="text"></asp:TextBox></td>-->
                            <td>
                                <asp:DropDownList class="common-text" ID="findinfoDropdl" runat="server"   Height="25px" Width="140px">
                                </asp:DropDownList>
                            </td>
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
                         <asp:Button ID="Button3" class="btn btn-info btn2" runat="server" Text="新增" 
                                    style="margin:auto;" onclick="Button3_Click" />
                        <asp:Button ID="DaoChu" runat="server"  class="btn btn-success btn2"  style="margin:auto; float:right; text-align:center;" Text="导出" OnClick="DaoChu_Click"/>
                    </div>
                </div>
            <div align="center">
            <asp:Label ID="title" runat="server" Font-Bold="False" Font-Size="X-Large"></asp:Label>
            </div>
                <div class="result-content">
                     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="15" 
                          GridLines="None"  AllowPaging="True"  CssClass="result-tab" 
                         DataKeyNames="id" Width="100%" 
                         onpageindexchanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                         <Columns>
                             <asp:BoundField DataField="vote_id" HeaderText="投票信息ID" Visible="False" />
                            <asp:BoundField DataField="position" HeaderText="项目" ReadOnly="True" />
                           
                             <asp:BoundField DataField="grade_id" HeaderText="班号" />
                             <asp:BoundField DataField="stu_id" HeaderText="学号" />
                            <asp:BoundField DataField="stu_name" HeaderText="姓名" />
                           
                             <asp:BoundField DataField="candidate_state" HeaderText="候选状态" />
                           
                             <asp:ButtonField ButtonType="Button" CommandName="btn_delvote" Text="取消投票" />
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
