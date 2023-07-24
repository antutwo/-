using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using tuixuan.util;

namespace tuixuan.adminLogin
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void image_Click(object sender, ImageClickEventArgs e)
        {
          /*  if (this.stuid.Text.Length < 1)
            {

                WebMessageBox.Show("学号不能为空"); return;
            }
            if (this.password.Text.Length < 1)
            {
                WebMessageBox.Show("密码不能为空"); return;
            }*/
           

            /*  if (RadioButton2.Checked)  //管理员
              {
                  if (Operation.getDatatable("select * from t_admin where username='" + this.username.Text + "' and userpwd='" + this.password.Text + "'").Rows.Count < 1)
                  {
                      WebMessageBox.Show("用户或密码错误"); return;
                  }
                  Session["username"] = username.Text;
                  Response.Redirect("admin/index.aspx");
              }
             *//* e*//*lse
              {
                  //教师
                  DataTable dt = Operation.getDatatable("select * from t_teacher where teachid='" + this.username.Text + "' and pwd='" + this.password.Text + "'");
                  if (dt.Rows.Count < 1)
                  {
                      WebMessageBox.Show("用户或密码错误"); return;
                  }
                  Session["teachid"] = username.Text;
                  Session["teachname"] = dt.Rows[0]["name"].ToString();
                  Response.Redirect("teacher/index.aspx");
              }*/
        }

        protected void submit_Click(object sender, EventArgs e)
        {

            if (this.stuid.Text.Length < 1)
            {
                WebMessageBox.Show("学号不能为空"); return;
            }
            if (this.password.Text.Length < 1)
            {

                WebMessageBox.Show("密码不能为空"); return;
            }
           if (this.code.Value.Length < 1)
            {
                WebMessageBox.Show("验证码不能为空"); return;
            }
            String num = Session["LVNum"].ToString();
            if (!num.Equals(this.code.Value))
            {
                WebMessageBox.Show("验证码输入错误");

                return;
            }
            DataTable dt = Operation.getDatatable("select * from Tx_student where stu_id='" + this.stuid.Text + "' and stu_password='" + this.password.Text + "'");
            if (dt.Rows.Count < 1)
            {
                WebMessageBox.Show("用户id或密码错误"); return;
            }
            /*if (Operation.getDatatable("select * from Tx_student where stu_id='" + this.stuid.Text + "' and stu_password='" + this.password.Text + "'").Rows.Count < 1)
            {
                WebMessageBox.Show("用户名或学号错误"); return;//错误直接返回
            }*/
            Session["stuid"] = stuid.Text;
          /*  Session["stuname"] = dt.Rows[0]["stu_name"].ToString(); *///将返回的表中的第一行的stu_name字段返回
            Response.Redirect("~/student/studindex.aspx");

        }
    }
}