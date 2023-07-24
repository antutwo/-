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
    public partial class candidate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["teachid"] == null)
            {
                WebMessageBox.Show("请登录", "../Login/teacherLogin.aspx");
            }
            string sql = "select teacher_name from Tx_teacher where teacher_id='" + Session["teachid"].ToString() + "'";
            DataTable dt = Operation.getDatatable(sql);
            if (dt.Rows.Count > 0)
            {
                /*Label1.Text = "欢迎您," + dt.Rows[0]["teacher_name"] + "老师";*/
                Label1.Text = "欢迎您登录学生推选管理系统，" + dt.Rows[0]["teacher_name"] + "老师";

            }
            title.Text = '"' + Request.QueryString["position"] + '"' + "候选人表";
            //绑定
            bind();
        }
        public void bind()
        {

            string sqlstr = "select * from Tx_candidate where position='" + Request.QueryString["position"] + "' and grade_id='" + Session["gid"] + "' order by candidate_state";
            GridView1.DataSource = Operation.getDatatable(sqlstr);
            GridView1.DataKeyNames = new string[] { "candidate_id" };//设置主键
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            //重新绑定　
            bind();
        }
    }

}