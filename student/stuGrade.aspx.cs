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
    public partial class stuGrade : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["stuid"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/studentLogin.aspx");
                }
                string sql = "select stu_name from Tx_student where stu_id='" + Session["stuid"].ToString() + "'";
                DataTable dt = Operation.getDatatable(sql);
                if (dt.Rows.Count > 0)
                {
                    Label1.Text = "欢迎您登录学生推选管理系统," + dt.Rows[0]["stu_name"].ToString() + "同学";

                }
                //绑定
                bind();
            }
        }
        public void bind()
        {
            string sqlstr = "select distinct a.stu_id,a.stu_name,a.stu_sex,a.grade_id,b.candidate_state,b.position FROM " +
                "(select * from Tx_student where grade_id='" + Session["grade_id"] + "' and " +
                "(stu_name like '%" + this.findinfo.Text + "%' OR LEN('" + this.findinfo.Text + "')=0)) " +
                "as a left join Tx_candidate as b ON a.stu_id = b.stu_id " +"order by a.stu_id";

            /*SELECT a.stu_id,a.stu_name,a.stu_sex,a.grade_id,b.candidate_state,b.position
            FROM Tx_student as a
            left JOIN Tx_candidate as b
            ON a.stu_id = b.stu_id and a.grade_id = '1' order by a.stu_id*/
            GridView1.DataSource = Operation.getDatatable(sqlstr);
            GridView1.DataKeyNames = new string[] { "stu_id" };//设置主键
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            //重新绑定　
            bind();
        }

        protected void findinfo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}