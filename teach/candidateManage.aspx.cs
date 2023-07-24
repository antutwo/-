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
    public partial class candidateManage : System.Web.UI.Page
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
                string sql2 = "select position_name from Tx_Gposition where grade_id='"+Session["gid"]+"'";//查找所有职位   ---绑定dropdownlist
                findinfoDropdl.DataSource = Operation.getDatatable(sql2);
                findinfoDropdl.DataTextField = "position_name";
                findinfoDropdl.DataValueField = "position_name";
                findinfoDropdl.DataBind();
                this.findinfoDropdl.Items.Insert(0, new ListItem("全部", "0"));

                bind();
            }
        }
        public void countUpdateVote()
        {
            //将候选表中的票数进行修改
            /*  Operation.runSql("update Tx_candidate set votes=b.vote from (select count(*)as vote,stu_id from Tx_vote where" +
                  " stu_id in(select stu_id from Tx_candidate where position = '" + Request.QueryString["position"] + "')and position = '" + Request.QueryString["position"] + "' and caste = '1'" +
                  "group by stu_id) as b,Tx_candidate as a where a.stu_id = b.stu_id and a.position = '" + Request.QueryString["position"] + "'and a.stu_id " +
                  "in(select stu_id from Tx_candidate where position = '" + Request.QueryString["position"] + "')");*/
            //统计每个职位候选人的票数，在将其修改
           /* Operation.runSql("update Tx_candidate set votes=b.vote from (select count(*)as vote,stu_id from Tx_vote where " +
                "stu_id in(select stu_id from Tx_candidate where position in (select position_name from Tx_position))and " +
                "position in (select position_name from Tx_position) and caste = '1'group by stu_id) Tx_candidate as a " +
                "where a.stu_id = b.stu_id and a.position in (select position_name from Tx_position)and a.stu_id in" +
                "(select stu_id from Tx_candidate where position in (select position_name from Tx_position))");*/
        }
        public void voteResult()
        {
           /* //将票数前几位的状态设置为成功候选
            Operation.runSql("update Tx_candidate set candidate_state='成功候选' where stu_id in (select top " +
                "(select number from Tx_position where position_name='" + Request.QueryString["position"] + "') stu_id from Tx_candidate order by votes desc)" +
                "and position='" + Request.QueryString["position"] + "'");*/
        }
        public void bind()
        {
            string sqlstr = null;

            if (this.findinfoDropdl.SelectedValue.ToString() == "0")
            {
                ///
                sqlstr = "select a.*,isNULL(b.candidate_num,0)as num from Tx_Gposition as a left join " +
                    "(select count(*) as candidate_num, position from Tx_candidate where position in (select position_name from Tx_Gposition) group by position)as b " +
                    "on a.position_name = b.position where grade_id = '" + Session["gid"] + "'";
            }
            else
            {
                ///
                sqlstr= "select a.*,isNULL(b.candidate_num,0)as num from Tx_Gposition as a left join " +
                    "(select count(*) as candidate_num, position from Tx_candidate where position in (select position_name from Tx_Gposition) group by position)as b " +
                    "on a.position_name = b.position where position_name = '" + this.findinfoDropdl.SelectedValue.ToString() + "' and grade_id = '" + Session["gid"] + "'";
               /* sqlstr = "select * from Tx_Gposition where position_name='" + this.findinfoDropdl.SelectedValue.ToString() + "' and grade_id='" + Session["gid"] + "'";*/
            }

            GridView1.DataSource = Operation.getDatatable(sqlstr);
            ///
            GridView1.DataKeyNames = new string[] { "position_id" };//设置主键
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

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btn_votes")//查看是否是按钮事件
            {

                int index = Convert.ToInt32(e.CommandArgument);//获取当前行
                ///
                string position = this.GridView1.Rows[index].Cells[1].Text;//获取当前选点击行的主键值
                ///
                string state = this.GridView1.Rows[index].Cells[3].Text;
                if (state == "候选结束")
                {
                    Response.Redirect("../teach/candidateManageResult.aspx?position=" + position);
                }
                else if(state =="正在候选")
                {
                    //可对候选人进行增删改查
                    Response.Redirect("../teach/candidateDetails.aspx?position=" + position);

                }
                else
                {
                    Response.Redirect("../teach/candidate.aspx?position=" + position);
                }

            }
        }
    }
}