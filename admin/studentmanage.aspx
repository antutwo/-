<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="studentmanage.aspx.cs" Inherits="tuixuan.admin.studentmanage" %>

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
    <form id="form1" runat="server">
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
                        <li><a href="studentmanage.aspx" style="background-color:#009688;color:white"><i class="iconfont icon-xueshengguanli"></i>学生管理</a></li>
                        <li><a href="grademanage.aspx"><i class="iconfont icon-banjiguanli"></i>班级管理</a></li>
                    </ul>
                </li>
               
            </ul>
        </div>
    </div>
    <!--/sidebar-->
    <!--/main-->

    
   <div class="main-wrap">

        <div class="crumb-wrap">
            <div class="crumb-list"><i class="icon-font"></i><a href="adminindex.aspx">首页</a><span class="crumb-step">&gt;</span><span class="crumb-name">学生管理</span></div>
        </div>
        <div class="search-wrap">
            <div class="search-content">
                    <table class="search-tab">
                        <tr>
                            <th width="70">关键字:</th>
                            <td><asp:TextBox class="common-text" placeholder="学生姓名关键字" ID="findinfo" runat="server" Type="text"></asp:TextBox></td>
                            <td style="align-content:center"><asp:Button ID="Button1" class="btn btn-primary btn2" runat="server" Text="查询" 
                                    style="margin:auto;" onclick="Button1_Click" /></td>
                            <td>班级:</td>
                            <td>
                                <asp:DropDownList class="common-text" ID="findinfoDropdl" runat="server"   Height="25px" Width="140px" AutoPostBack="True" OnSelectedIndexChanged="findinfoDropdl_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
            </div>
        </div>
        <div class="result-wrap">
                <div class="result-title">
                    <div class="result-list">
                        <asp:FileUpload ID="FileUpload1" runat="server" class="common-text"/>&nbsp;&nbsp;
                        <asp:Button ID="Button2" class="btn btn-success btn2" runat="server" Text="导入" 
                                    style="margin:auto;" onclick="Button2_Click" OnCommand="Button2_Command"  />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button3" class="btn btn-info btn2" runat="server" Text="新增" 
                                    style="margin:auto;" onclick="Button3_Click" />
                        <asp:Button ID="DaoChu" runat="server"  class="btn btn-success btn2"  style="margin:auto; float:right; text-align:center;" Text="导出" OnClick="DaoChu_Click"/>
                    </div>
                </div>
                <div class="result-content">
                     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="15" 
                          GridLines="None"  AllowPaging="True"  CssClass="result-tab" 
                         DataKeyNames="id" Width="100%" 
                         onpageindexchanging="GridView1_PageIndexChanging" 
                         onrowcancelingedit="GridView1_RowCancelingEdit" 
                         onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing" 
                         onrowupdating="GridView1_RowUpdating" OnRowDataBound="GridView1_RowDataBound">
                         <Columns>
                             <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkcheckall" runat="server" AutoPostBack="True" oncheckedchanged="chkcheckall_CheckedChanged"  Text="全选"/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkCheck"  runat="server" />
                                </ItemTemplate>
                             </asp:TemplateField>

                            <asp:BoundField DataField="stu_id" HeaderText="学生学号" ReadOnly="True" />
                            <asp:BoundField DataField="stu_name" HeaderText="学生姓名" />
                            <asp:BoundField DataField="stu_sex" HeaderText="学生性别" />
                            <asp:BoundField DataField="grade_id" HeaderText="班级号" />
                           
                             <asp:BoundField DataField="stu_password" HeaderText="登录密码" />
                           
                            <asp:CommandField HeaderText="编辑" ShowEditButton="True" />
                             <asp:TemplateField HeaderText="删除" ShowHeader="False">
                                 <ItemTemplate>
                                     <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="删除"
                                         OnClientClick="if(confirm('你确定要删除此记录吗?')){return true;}else{return false;}"></asp:LinkButton>
                                 </ItemTemplate>
                             </asp:TemplateField>
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