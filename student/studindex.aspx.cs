using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tuixuan.util;

namespace tuixuan.student
{
    public partial class studindex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["stuid"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/studentLogin.aspx");
                }
               
                string sql = "select * from Tx_student as a,Tx_grade as b where a.grade_id=b.grade_id and a.stu_id='" + Session["stuid"].ToString() + "'";
                DataTable dt = Operation.getDatatable(sql);
                if (dt.Rows.Count > 0)
                {
                    Label1.Text = "欢迎您登录学生推选管理系统," + dt.Rows[0]["stu_name"].ToString() + "同学";
                    lblid.Text = dt.Rows[0]["stu_id"].ToString();
                    lblname.Text = dt.Rows[0]["stu_name"].ToString();
                    lblsex.Text = dt.Rows[0]["stu_sex"].ToString();
                    lblpwd.Text = dt.Rows[0]["stu_password"].ToString();
                    lblgid.Text = dt.Rows[0]["grade_id"].ToString();
                    lblgname.Text = dt.Rows[0]["grade_name"].ToString();

                }
                Session["grade_id"] = lblgid.Text;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("../student/studupdate.aspx");
        }
    }
}