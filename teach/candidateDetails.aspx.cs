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
    public partial class candidateDetails : System.Web.UI.Page
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
                //绑定
                bind();
            }
        }
        public void bind()
        {
            string sqlstr = " select * from Tx_candidate where position='" + Request.QueryString["position"] + "' and (candidate_name like '%" + this.findinfo.Text + "%' OR LEN('" + this.findinfo.Text + "')=0) and grade_id='"+Session["gid"]+"'";    //此职位的所有候选人信息
            GridView1.DataSource = Operation.getDatatable(sqlstr);
            GridView1.DataKeyNames = new string[] { "candidate_id" };//设置主键
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string position = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
            string stu_id = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
            string name = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
            string sql = "select * from Tx_student stu_id='" + stu_id + "'and stu_name='" + name + "'";
            if (Operation.getDatatable(sql).Rows.Count <0)
            {
                WebMessageBox.Show("你所修改的学生不存在");
            }
            else
            {
                Operation.runSql("update Tx_candidate set position='" + position + "',stu_id='" + stu_id + "',candidate_name='" + name + "'" +
                " where candidate_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'");

                bind();
            }
            
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            /* 取触发了编辑的那行的索引值赋值GridView1.EditIndex*/
            bind();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string sql = "delete from Tx_candidate where candidate_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";
            Operation.runSql(sql);
        
            bind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
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

        protected void Button3_Click(object sender, EventArgs e)
        {
            string sql = "select count(*)as counts from Tx_candidate where position='" + Request.QueryString["position"] + "'";
            ///
            string sql1 = "select maximun from Tx_Gposition where position_name='" + Request.QueryString["position"] + "' and grade_id='"+Session["gid"]+"'";
            int count = 0;
            int max = 0;
            DataTable dt = Operation.getDatatable(sql);
            if (dt.Rows.Count > 0)//统计现有的候选人数 
            {
                count = int.Parse(dt.Rows[0]["counts"].ToString());
            }
            else
            {
                Response.Redirect("../teach/addCandidate.aspx");
            }
            dt = Operation.getDatatable(sql1);
            if (dt.Rows.Count > 0) 
            {
                max = int.Parse(dt.Rows[0]["maximun"].ToString());
            }
            if (max - count > 0)
            {
                Response.Redirect("../teach/addCandidate.aspx");
            }
            else
            {
                WebMessageBox.Show("当前候选人数已经为最大，不可添加");
            }
          
        }

    /*    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标经过时，行背景色变
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#E6F5FA'");
                //鼠标移出时，行背景色变
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");
            }
        }*/
    }
}