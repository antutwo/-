<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="studentLogin.aspx.cs" Inherits="tuixuan.adminLogin.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" class="x-admin-sm">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>后台登录</title>
<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
<link rel="stylesheet" href="../css/font.css"/>
<link rel="stylesheet" href="../css/login.css"/>
<link rel="stylesheet" href="../css/xadmin.css" />
<link href="../css/adminLogin.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="https://cdn.bootcss.com/jquery/3.2.1/jquery.min.js"></script>
<script src="../layui/layui.js" charset="utf-8"></script>
    <style>
          .auto-style1 {
            width: auto;
            height: 50px;
        }
    </style>
</head>
<body class="login-bg"  onkeydown="onEnterDown();">
    <div class="login layui-anim layui-anim-up">
        <div style="text-align:center;" >
            <h2 style="font-weight:normal;">学生推选管理系统</h2>
        </div>
            <div class="message">学生登录</div>
            <div id="darkbannerwrap"></div>
            <form id="form1" runat="server" class="layui-form" >
                
                <div>
                    <asp:Label runat="server" ID="lbl1">学号:</asp:Label>
                    <asp:TextBox ID="stuid" runat="server" lay-verify="required" class="layui-input" CssClass="layui-form-switch"></asp:TextBox>
                </div>
                  <div>
                   <asp:Label runat="server" ID="lbl2">密码:</asp:Label>
                      <br />
                      <asp:TextBox ID="password" runat="server" TextMode="Password" lay-verify="required" class="layui-input"></asp:TextBox>
                </div>
                <div class="login_code">
                       验证码:<br />
                        <input name="code" type="text" class="input" id="code" style="width:auto; height: 50px;" maxlength="4" autocomplete="off" runat="server" />
                        <img alt="" src="../Control/validate.aspx" id="getcode_img" class=" auto-style1" title="看不清请点击！" />
                       
                 </div>
                <br />
                <div>
                    <asp:Button runat="server" ID="submit" lay-submit lay-filter="login" style="width:100%;" Text="登录" OnClick="submit_Click"/>
                    <asp:HyperLink ID="HyperLink1" runat="server" style="font-size:14px;float:right;color: #999" NavigateUrl="~/Default.aspx">返回</asp:HyperLink>
                </div>
                    
            </form>
        <div class="note">
            <br />
                        * 不要在公共场合保存登录信息；<br />
                      
                        <span id="msg_tip"></span>
                    </div>
    </div>

    <div id="foot">
        <p style="padding-top:20px;">版权所有：湖南工业职业技术学院 地址：XXXXXXXXXXXXXXXXXXXXXX</p>
        <p>联系人：XXXXXXX   电话：0000-00000000   联系传真：0000-00000000</p>
    </div>

    <script src="../Scripts/jquery-1.6.min.js" type="text/javascript"></script>
    <script src="../Scripts/js_login.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#userid").focus();
            $("#getcode_img").click(ShowValidImage);
        })

        function onEnterDown() {//body的onkeydown事件时调用
            if (window.event.keyCode == 13) {
                submit_login();
            }
        }
    </script>  
</body>
</html>
