<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tuixuan.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>学生推选管理系统</title>
    <link rel="stylesheet" href="../css/font.css"/>
    <link rel="stylesheet" href="../css/login.css"/>
    <link rel="stylesheet" href="../css/xadmin.css" />
    <link href="../css/adminLogin.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style4{
            width:auto;
            height:100px;
        }
        .body{
            background-image:url(../Images/index.jpg);
            
        }
        .imgbtn{
            text-align:center;
            text-decoration:none;
            color:#009688;
        }
        #index{
            text-align:center;
            margin-top: 50px;
            margin-right: auto;
            
            margin-left: auto;
            width:800px;
            height:auto;
            
        }
        .style2 {
           height:30px;
        }
        .btn{
         
            Height:50px;
            Width:370px;
            margin-left: 0px;
            background-color:#10b0ba;
            color:white;
            border-color:#ffffff;
               
        }
    </style>
</head>
<body class="body">
    <div id="index">
         <h1>
        <asp:Image ID="Image1" runat="server" Height="94px" ImageUrl="~/Images/学校标志.png" Width="794px" />
        </h1>
        <h1>&nbsp;</h1>
         <h1>学生推选管理系统</h1>
        <p>&nbsp;</p>
      <div>
      <form id="form1" runat="server">
    <table style="width:100%;">
         <tr class="style4">
            <td class="auto-style2"></td>
            <td></td>
            <td></td>
        </tr>
        <tr style="height:80px; text-align:left;">
            <td colspan="3" >
              <div>
                <asp:Label ID="Label1" runat="server" style="color:black;font-size:20px;" Text="请选择你的身份：" ></asp:Label>
              </div>
            </td>
        </tr>
        <tr>
            <td colspan="3" ></td>
        </tr>
        <tr>
            <td class="auto-style2" >
                <div>
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="48px" ImageUrl="~/Images/login4.jpg" Width="54px" />                  
                    &nbsp;
                </div>               
            </td>
            <td >
                <div>
                    <asp:ImageButton ID="ImageButton2" runat="server" Height="48px" ImageUrl="~/Images/login3.jpg" Width="54px" />
                   &nbsp;
                </div>
            </td>       
            <td>
                <div>
                    <asp:ImageButton ID="ImageButton3" runat="server" Height="48px" ImageUrl="~/Images/login2.jpg" Width="54px" />
                   &nbsp;
                </div>
            </td>
            
        </tr>
        <tr class="style2" >
            <td style="background-color:azure; ">
                <div>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Login/studentLogin.aspx" class="imgbtn">学生</asp:HyperLink>
                </div>
            </td>
            <td style="background-color:azure;">
                <div>
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Login/teacherLogin.aspx" class="imgbtn">教师</asp:HyperLink>
                </div>
                
            </td>
            <td style="background-color:azure;">
                <div>
                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Login/adminLogin.aspx" class="imgbtn">管理员</asp:HyperLink>
                </div>                
            </td>
        </tr>
        <tr>
            <td style="height:60px"></td>
        </tr>
    </table>
              </form>
          </div>
     <div id="foot" style="bottom:10px;" >
        <p style="padding-top:20px;">版权所有：湖南工业职业技术学院 地址：XXXXXXXXXXXXXXXXXXXXXX</p>
        <p>联系人：XXXXXXX   电话：0000-00000000   联系传真：0000-00000000</p>
    </div>
    </div>
</body>
</html>
