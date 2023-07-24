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
    public partial class electManage1 : System.Web.UI.Page
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

                ///
                string sql2 = "select position_name from Tx_Gposition where grade_id='" + Session["gid"] + "'";//查找所有职位   ---绑定dropdownlist
                findinfoDropdl.DataSource = Operation.getDatatable(sql2);
                findinfoDropdl.DataTextField = "position_name";
                findinfoDropdl.DataValueField = "position_name";
                findinfoDropdl.DataBind();
                this.findinfoDropdl.Items.Insert(0, new ListItem("全部", "0"));

                bind();
            }
        }
        public void bind()
        {
            string sqlstr = null;
            if (this.findinfoDropdl.SelectedValue.ToString() == "0")//查找全部
            {
                ///
                /*  sqlstr = "select a.*,isNULL(b.elect_numbers,0)as elect_numbers ,isNULL(c.candidate_num,0)as candidate_num,isNULL(d.position_votes,0)asposition_votes from Tx_Gposition as a left join " +
                      "(select count(*) as elect_numbers, position from Tx_elect where position " +
                      "in (select position_name from Tx_Gposition) group by position )as b on a.position_name = b.position left join " +
                      "(select count(*)as candidate_num, position from Tx_candidate where position in (select position_name from Tx_Gposition) group by position) " +
                      "as c on a.position_name = c.position left join " +
                      "(select count(*) as position_votes,position from Tx_vote where position in(select position_name from Tx_Gposition) " +
                      "group by position)as d on a.position_name = d.position " +
                      "where grade_id = '1' order by a.position_name";*/

                /*  sqlstr= "	select distinct a.* ,isNULL(b.votes,0)as votes,c.state,c.position_vote from Tx_elect as a left join " +
                      "(select count(*) as votes, vote_name from Tx_vote where position in " +
                      "(select position_name from Tx_Gposition ) and grade_id = '" + Session["gid"] + "' group by all vote_name) " +
                      "as b on a.elect_name = b.vote_name left join" +
                      " Tx_Gposition as c on a.position = c.position_name " +
                      "where position in(select position_name from Tx_Gposition) order by position";*/

                /*sqlstr = "	select distinct a.* ,isNULL(b.votes,0)as votes,c.state,c.position_vote from Tx_elect as a,(select count(*) as votes,vote_name from Tx_vote where position in " +
                    "(select position_name from Tx_Gposition ) and grade_id = '"+Session["gid"]+"' group by vote_name) " +
                    "as b,Tx_Gposition as c  where a.elect_name = b.vote_name and a.position = c.position_name " +
                    "and a.position in(select position_name from Tx_Gposition) order by position";*/
                sqlstr = "select distinct a.* ,isNULL(b.votes,0)as votes,c.state,c.position_vote from Tx_elect as a left join " +
                    "(select count(*) as votes, vote_name from Tx_vote where position in" +
                    " (select position_name from Tx_Gposition ) and grade_id = '"+Session["gid"]+"' group by all vote_name) " +
                    "as b on a.elect_name = b.vote_name left join " +
                    "Tx_Gposition as c on a.position = c.position_name where position in" +
                    "(select position_name from Tx_Gposition) and a.grade_id = '" + Session["gid"] + "' order by position";

            }
            else
            {
                ///
                /*sqlstr = "	select distinct a.* ,isNULL(b.votes,0)as votes,c.state,c.position_vote from Tx_elect as a left join " +
                     "(select count(*) as votes, vote_name from Tx_vote where position in " +
                     "(select position_name from Tx_Gposition ) and grade_id = '" + Session["gid"] + "' group by all vote_name) " +
                     "as b on a.elect_name = b.vote_name left join" +
                     " Tx_Gposition as c on a.position = c.position_name " +
                     "where a.position='" + this.findinfoDropdl.SelectedValue.ToString() + "' order by position";
*/
                /*sqlstr = "	select distinct a.* ,isNULL(b.votes,0)as votes,c.state,c.position_vote from Tx_elect as a,(select count(*) as votes,vote_name from Tx_vote where position in " +
                    "(select position_name from Tx_Gposition ) and grade_id = '" + Session["gid"] + "' group by vote_name) " +
                    "as b,Tx_Gposition as c  where a.elect_name = b.vote_name and a.position = c.position_name " +
                    "and a.position='"+this.findinfoDropdl.SelectedValue.ToString()+"' order by position";*/
                sqlstr = "select distinct a.* ,isNULL(b.votes,0)as votes,c.state,c.position_vote from Tx_elect as a left join " +
                    "(select count(*) as votes, vote_name from Tx_vote where position in" +
                    " (select position_name from Tx_Gposition ) and grade_id = '" + Session["gid"] + "' group by all vote_name) " +
                    "as b on a.elect_name = b.vote_name left join " +
                    "Tx_Gposition as c on a.position = c.position_name where a.position='" + this.findinfoDropdl.SelectedValue.ToString() + "' " +
                    "and a.grade_id = '" + Session["gid"] + "' order by position";

            }
            GridView1.DataSource = Operation.getDatatable(sqlstr);
            GridView1.DataKeyNames = new string[] { "elect_id" };//设置主键
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bind();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("../teach/addElect.aspx");
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

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            bind();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            string id = GridView1.DataKeys[e.RowIndex].Value.ToString().Trim();


            string state = GridView1.Rows[e.RowIndex].Cells[6].Text.ToString().Trim();
            if (state == "正在候选")
            {
                string sql = "delete from Tx_elect where elect_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";
                Operation.runSql(sql);
                bind();
            }
            else
            {
                WebMessageBox.Show("当前项目评选人不可更改");
            }

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            /* 取触发了编辑的那行的索引值赋值GridView1.EditIndex*/
            bind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string state = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].Controls[0])).Text.ToString().Trim();
           
            if (state == "正在候选")
            {
                string position = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
                string sid = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
                string sname = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();

                string sql = "update Tx_elect set position='" + position + "',stu_id='" + sid + "',elect_name='" + sname + "' where elect_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";
                Operation.runSql(sql);
                GridView1.EditIndex = -1;//从编辑状态改为浏览状态
                bind();
            }
            else
            {
                WebMessageBox.Show("当前项目评选人不可更改");
            }
        }
    }
}