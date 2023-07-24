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
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["teachid"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/teacherLogin.aspx");
                }
               
                string sql = "select * from Tx_teacher as a,Tx_grade as b where a.grade_id=b.grade_id and a.teacher_id='" + Session["teachid"].ToString() + "'";
                DataTable dt = Operation.getDatatable(sql);
                if (dt.Rows.Count > 0)
                {
                    /* Label1.Text = "欢迎您," + dt.Rows[0]["teacher_name"] + "老师";*/
                    Label1.Text = "欢迎您登录学生推选管理系统，" + dt.Rows[0]["teacher_name"] + "老师";
                    lblid.Text = dt.Rows[0]["teacher_id"].ToString();
                    lblname.Text = dt.Rows[0]["teacher_name"].ToString();
                    lblpwd.Text = dt.Rows[0]["teacher_password"].ToString();
                    lblgid.Text = dt.Rows[0]["grade_id"].ToString();
                    lblgname.Text = dt.Rows[0]["grade_name"].ToString();

                }
                Session["gid"] = lblgid.Text;
                
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            /*链接到修改页面*/
            
            Response.Redirect("../teach/teachupdate.aspx");
        }
    }
}