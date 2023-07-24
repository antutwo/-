using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tuixuan.util;

namespace tuixuan.Login
{
    public partial class adminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submit_Click(object sender, EventArgs e)
        {

            if (this.username.Text.Length < 1)
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
            /* if (Operation.getDatatable("select * from Tx_admin where user_name='" + this.username.Text + "' and user_password='" + this.password.Text + "'").Rows.Count < 1)
             {
                 WebMessageBox.Show("用户或密码错误"); return;
             }*/
            DataTable dt = Operation.getDatatable("select * from Tx_admin where user_name='" + this.username.Text + "' and user_password='" + this.password.Text + "'");
            if (dt.Rows.Count < 1)
            {
                WebMessageBox.Show("用户名或密码错误"); return;
            }
            /*Session["username"] = username.Text;
            Session["password"] = password.Text;*/
            Session["admin_id"] = dt.Rows[0]["user_id"].ToString();
            Response.Redirect("~/admin/adminindex.aspx");
        }
    }
}