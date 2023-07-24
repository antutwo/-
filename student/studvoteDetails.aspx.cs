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
    public partial class studvoteDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["stuid"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/studentLogin.aspx");
                }
                string sql = "select stu_name from Tx_student where stu_id='" + Session["stuid"].ToString() + "'";
                DataTable dt = Operation.getDatatable(sql);
                string name = null;
                if (dt.Rows.Count > 0)
                {
                    Label1.Text = "欢迎您登录学生推选管理系统," + dt.Rows[0]["stu_name"].ToString() + "同学";
                    name = dt.Rows[0]["stu_name"].ToString();
                }
                title.Text = "投票详情表";
                string sql2 = " select position_name from Tx_position where position_name in(select distinct position from Tx_vote where vote_name='"+name+"')  ";//查找所有我投过票的职位   ---绑定dropdownlist
                findinfoDropdl.DataSource = Operation.getDatatable(sql2);
                findinfoDropdl.DataTextField = "position_name";
                findinfoDropdl.DataValueField = "position_name";
                findinfoDropdl.DataBind();

                this.findinfoDropdl.Items.Insert(0, new ListItem("全部", "0"));

                bind();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            //重新绑定　
            bind();
        }
        public void bind()
        {
            string sql1 = "select * from Tx_student as a,Tx_grade as b where a.grade_id=b.grade_id and a.stu_id='" + Session["stuid"].ToString() + "'";
            string stu_name = null;
            DataTable dt = Operation.getDatatable(sql1);
            if (dt.Rows.Count > 0)
            {
                stu_name = dt.Rows[0]["stu_name"].ToString();//当前使用系统的人的名字

            }
            string sqlstr = null;
            if (this.findinfoDropdl.SelectedValue.ToString() == "0")
            {
                //待修改 --需要显示状态
                sqlstr = "select distinct a.* ,b.candidate_state from Tx_vote as a,(select candidate_state,position,stu_id from Tx_candidate where stu_id in" +
                "(select stu_id from Tx_vote where vote_name='" + stu_name + "')) as b where a.stu_id=b.stu_id and a.vote_name='" + stu_name + "'and a.caste='1'";
            }
            else
            {
                sqlstr = "select distinct a.* ,b.candidate_state from Tx_vote as a,(select candidate_state,position,stu_id from Tx_candidate where stu_id in" +
                "(select stu_id from Tx_vote where vote_name='" + stu_name + "')) as b where a.stu_id=b.stu_id and a.vote_name='" + stu_name + "'and a.caste='1' and a.position='"+findinfoDropdl.SelectedValue.ToString() + "'";
            }

            GridView1.DataSource = Operation.getDatatable(sqlstr);
            GridView1.DataKeyNames = new string[] { "vote_id" };//设置主键
            GridView1.DataBind();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btn_delvote")//查看是否是按钮事件
            {
                int index = Convert.ToInt32(e.CommandArgument);//获取当前行
                string key = this.GridView1.DataKeys[index].Value.ToString();//获取当前选点击行的主键值
                string sqlstr = "update Tx_vote set caste='0' where vote_id='" + key + "'";
                Operation.runSql(sqlstr);
                bind();
            }

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

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("../student/studvotes.aspx");
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            /*if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标经过时，行背景色变
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#E6F5FA'");
                //鼠标移出时，行背景色变
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");
            }*/
        }
    }
}