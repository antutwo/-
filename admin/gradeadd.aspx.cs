using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tuixuan.util;

namespace tuixuan.admin
{
    public partial class gradeadd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["admin_id"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/adminLogin.aspx");
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //添加
            if (TextBox1.Text == "")
            {
                WebMessageBox.Show("请输入班号"); return;
            }
            if (Operation.getDatatable("select * from Tx_grade where grade_id='" + TextBox1.Text + "'").Rows.Count > 0)
            {
                WebMessageBox.Show("此班号已经存在"); return;
            }
            if (TextBox2.Text == "")
            {
                WebMessageBox.Show("请输入班名"); return;
            }
         
            string sql = "insert into Tx_grade(grade_id,grade_name) values('" +TextBox1.Text + "','" + TextBox2.Text + "')";
            Operation.runSql(sql);
            WebMessageBox.Show("添加完成", "grademanage.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("grademanage.aspx");
        }
    }
}