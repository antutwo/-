using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tuixuan.util;

namespace tuixuan.admin
{
    public partial class adminupdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["admin_id"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/adminLogin.aspx");
                }
                string sql = "select * from Tx_admin where user_id='" + Session["admin_id"].ToString() + "'";
                DataTable dt = Operation.getDatatable(sql);
                if (dt.Rows.Count > 0)
                {
                    TextBox1.Text = dt.Rows[0]["user_name"].ToString();
                    TextBox2.Text = dt.Rows[0]["user_password"].ToString();
                    TextBox3.Text = dt.Rows[0]["user_id"].ToString();
                }


            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string aname = TextBox1.Text;
            string apwd = TextBox2.Text;
            if (aname != "" && apwd != "")
            {
                Operation.runSql("update Tx_admin set user_name='" + aname + "',user_password='" + apwd + "' where user_id='" + Session["admin_id"].ToString() + "'");
                WebMessageBox.Show("修改完成", " adminindex.aspx");
            }
            else
            {
                WebMessageBox.Show("修改的内容不能含空");

            }
           
        }
    }
}