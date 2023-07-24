using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tuixuan.util;

namespace tuixuan.teach
{
    public partial class teachupdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!this.IsPostBack)
            {
                if (Session["teachid"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/teacherLogin.aspx");
                }             
               /* Label1.Text = "欢迎您," + Session["teachname"].ToString() + "老师";*/
                string sql = "select * from Tx_teacher as a,Tx_grade as b where a.grade_id=b.grade_id and a.teacher_id='" + Session["teachid"].ToString() + "'";
                DataTable dt = Operation.getDatatable(sql);
                if (dt.Rows.Count > 0)
                {
                    /* Label1.Text = "欢迎您," + dt.Rows[0]["teacher_name"] + "老师";*/
                    Label1.Text = "欢迎您登录学生推选管理系统，" + dt.Rows[0]["teacher_name"] + "老师";
                    TextBox1.Text = dt.Rows[0]["teacher_id"].ToString();
                    TextBox2.Text = dt.Rows[0]["teacher_name"].ToString();
                    TextBox3.Text = dt.Rows[0]["teacher_password"].ToString();
                    TextBox4.Text = dt.Rows[0]["grade_id"].ToString();
                    TextBox5.Text = dt.Rows[0]["grade_name"].ToString();

                }  
            }
            
            }
        

        protected void Button1_Click(object sender, EventArgs e)
        {

            string tname = TextBox2.Text;
            string tpwd = TextBox3.Text;
            if (tname != "" && tpwd != "")
            {
                Operation.runSql("update Tx_teacher set teacher_name='" + tname + "',teacher_password='" + tpwd + "' where teacher_id='" + Session["teachid"].ToString() + "'");
               /* Response.Write("<script>alert('修改完成')</script>");
                Response.Redirect("../teach/teachindex.aspx");*/

                WebMessageBox.Show("修改完成", "teachindex.aspx");
            }
            else
            {
                WebMessageBox.Show("修改的内容不能含空");
            }


        }
    }
}