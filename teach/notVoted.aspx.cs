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
    public partial class notVoted : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["teachid"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/teacherLogin.aspx");
                }
                string sql = "select teacher_name from Tx_teacher where teacher_id='" + Session["teachid"].ToString() + "'";
                DataTable dt = Operation.getDatatable(sql);
                if (dt.Rows.Count > 0)
                {
                    /* Label1.Text = "欢迎您," + dt.Rows[0]["teacher_name"] + "老师";*/
                    Label1.Text = "欢迎您登录学生推选管理系统，" + dt.Rows[0]["teacher_name"] + "老师";

                }
                title.Text = '"' + Request.QueryString["position"] + '"' + "未投票详情表";
                //绑定
                bind();
            }
        }
        public void bind()
        {
            ///
            string str = "select elect_num from Tx_Gposition where position_name='"+Request.QueryString["position"]+"'";
            DataTable dt = Operation.getDatatable(str);//当前职位的评选人数
            string num = null;
            if (dt.Rows.Count>0)
            {
                num = dt.Rows[0]["elect_num"].ToString();
            }
            string sqlstr = null;
            if (num == "全班")
            {
                sqlstr = "select stu_id,stu_name,grade_id from Tx_student  where stu_name not in " +
                    "(select vote_name from Tx_vote where position='"+Request.QueryString["position"]+"'" +
                    " and grade_id='"+ Session["gid"] + "')and grade_id='" + Session["gid"] + "'";

                GridView1.DataSource = Operation.getDatatable(sqlstr);
                GridView1.DataKeyNames = new string[] { "stu_id" };//设置主键
                GridView1.DataBind();
            }
            else
            {
                /* sqlstr = "select stu_id,stu_name,grade_id from Tx_student where stu_name in " +
                     "(select elect_name from Tx_elect where elect_name not in(select vote_name from Tx_vote " +
                     "where position='"+Request.QueryString["position"]+ "' and grade_id='"+Session["gid"]+"'")
                     "and position='" + Request.QueryString["position"] + "')";*/
                sqlstr = "select stu_id,stu_name,grade_id from Tx_student where stu_name in (select elect_name from Tx_elect " +
                    "where elect_name not in (select vote_name from Tx_vote where position = '" + Request.QueryString["position"] + "' and " +
                    "grade_id = '"+Session["gid"]+"' and caste = '1')and position = '" + Request.QueryString["position"] + "')";
                GridView1.DataSource = Operation.getDatatable(sqlstr);
                GridView1.DataKeyNames = new string[] { "stu_id" };//设置主键
                GridView1.DataBind();
            }
           

            
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            //重新绑定　
            bind();
        }

        protected void DaoChu_Click(object sender, EventArgs e)
        {

            DGToExcel(GridView1);
        }
        public void DGToExcel(System.Web.UI.Control ctl)
        {
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=Excel.xls");
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
            HttpContext.Current.Response.ContentType = "application/ms-excel";//image/JPEG;text/HTML;image/GIF;vnd.ms-excel/msword
            ctl.Page.EnableViewState = false;
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            ctl.RenderControl(hw);
            HttpContext.Current.Response.Write(tw.ToString());
            HttpContext.Current.Response.End();
        }
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {

        }

        
    }
}