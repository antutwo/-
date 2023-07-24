using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tuixuan.util;

namespace tuixuan.adminLogin
{
    public partial class teacherLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void image_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            if (this.teachid.Text.Length < 1)
            {
                WebMessageBox.Show("用户名不能为空"); return;
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
            DataTable dt = Operation.getDatatable("select * from Tx_teacher where teacher_id='" + this.teachid.Text + "' and teacher_password ='" + this.password.Text + "'");
            if (dt.Rows.Count < 1)
            {
                WebMessageBox.Show("用户或密码错误"); return;
            }
            Session["teachid"] =teachid.Text;
           /* Session["teachname"] = dt.Rows[0]["teacher_name"].ToString();*/ //将返回的表中的第一行的teacher_name字段返回
      
            Response.Redirect("~/teach/teachindex.aspx");

        }
    }
}