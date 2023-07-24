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
    public partial class candidate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["stuid"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/studentLogin.aspx");
                }
               ///
                string sql1 = "select stu_name from Tx_student where stu_id='" + Session["stuid"].ToString() + "'";
                DataTable dt = Operation.getDatatable(sql1);
                if (dt.Rows.Count > 0)
                {
                    Label1.Text = "欢迎您登录学生推选管理系统," + dt.Rows[0]["stu_name"].ToString() + "同学";

                }

                string sql = "select position from Tx_candidate where stu_id='" + Session["stuid"] + "'";//根据学号查找所有职位   ---绑定dropdownlist
                findinfoDropdl.DataSource = Operation.getDatatable(sql);
                findinfoDropdl.DataTextField = "position";
                findinfoDropdl.DataValueField = "position";
                findinfoDropdl.DataBind();
                this.findinfoDropdl.Items.Insert(0, new ListItem("全部", "0"));

                //绑定
                bind();
            }
        }
        public void countUpdateVote()
        {
            Operation.runSql("update Tx_candidate set votes = b.vote from(select count(*) as vote, position from Tx_vote where stu_id='" + Session["stuid"].ToString() + "' and position in (select position from Tx_candidate where stu_id='" + Session["stuid"].ToString() + "') and caste = '1'group by position) " +
                "as b,Tx_candidate as a where a.position = b.position and stu_id='" + Session["stuid"].ToString() + "'and a.position in(select position from Tx_candidate where stu_id='" + Session["stuid"].ToString() + "')");

        }
        public void bind()
            
        {
            countUpdateVote();
            string sqlstr=null;
            if (this.findinfoDropdl.SelectedValue.ToString() == "0")//查找全部
            {
                sqlstr = "select candidate_id,position,candidate_state,votes from Tx_candidate where stu_id='" + Session["stuid"].ToString() + "' order by position";
            }
            else { 
                sqlstr = "select candidate_id,position,candidate_state,votes from Tx_candidate where position='" + 
                this.findinfoDropdl.SelectedValue.ToString() + "' and stu_id='" + Session["stuid"] .ToString()+ "' order by position";

                /*  "select candidate_id,position,candidate_state,votes from Tx_candidate as a,Tx_position as b where a.position=b.position_name and (position like '%" + this.findinfoDropdl.SelectedValue + "%' OR LEN('" + this.findinfoDropdl.SelectedValue + "')=0) and stu_id='"+ Session["stuid"] .ToString()+ "' order by position";*/
                /* select  a.candidate_id,a.position,b.state,votes=(select count(*) from Tx_vote where position='文明学生' and stu_id='1001') from Tx_candidate as a,Tx_position as b where a.position=b.position_name and a.position='文明学生' and a.stu_id='1001'order by a.position; */
                 }
            GridView1.DataSource = Operation.getDatatable(sqlstr);
            GridView1.DataKeyNames = new string[] { "candidate_id" };//设置主键
            GridView1.DataBind();
        }

      /*  protected void tanchu( string Message)
        {
            //变量初始化
            string Str_Message = Message;
            //显示信息Str_Message
            Response.Write("<script language=javascript>");
            Response.Write("alert('" + Str_Message + "');");
            Response.Write("</script>");
        }*/

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


            string position = (GridView1.Rows[e.RowIndex].Cells[1]).Text.ToString();
            string state= (GridView1.Rows[e.RowIndex].Cells[2]).Text.ToString();//职位状态
            if (state == "取消候选")
            {
                WebMessageBox.Show("当前取消状态已不可改变");

            }
            else
            {
                ///
                string sql = "select * from Tx_Gposition where state='正在候选' and position_name='" + position + "' and grade_id='"+Session["grade_id"]+"'";
                if (state == "正在候选" && (Operation.getDatatable(sql).Rows.Count > 0))//职位正在候选并且我也正在候选中,取消候选就无法再申报
                {

                    string sqlstr = "update Tx_candidate set candidate_state='取消候选' where candidate_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";

                    //GridView1.DataKeys[e.RowIndex].Value.ToString() 获取当前行的主键值
                    Operation.runSql(sqlstr);
                    bind();
                }
          
                else
                {
                    WebMessageBox.Show("候选已结束，不可取消");
                }
            }
           
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            /* 取触发了编辑的那行的索引值赋值GridView1.EditIndex*/
            bind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            //重新绑定　
            bind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if(e.CommandName== "btn_voteDetails")//查看是否是按钮事件
            {
                

                int index = Convert.ToInt32(e.CommandArgument);//获取当前行
                string key = this.GridView1.DataKeys[index].Value.ToString();//获取当前选点击行的主键值
                string position = this.GridView1.Rows[index].Cells[1].Text;//获取当前点击行，某列的值
                string sql = "select state from Tx_Gposition where position_name='"+position+"' and grade_id='"+Session["grade_id"] +"'";
                string state = null;
                DataTable dt = Operation.getDatatable(sql);
                if (dt.Rows.Count > 0)
                {
                    state = dt.Rows[0]["state"].ToString();
                }
                if (state == "候选结束")//如果候选状态是“候选结束”则可以查看投票详情
                {
                    Response.Redirect("../student/candidateVotesDetails.aspx?position=" + position );//跳转到投票详情页
                }
                /*string petID = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();//获取主键值*/
                /*string sql = "select position,candidate_name from Tx_candidate where candidate_id='" + id + "'";
                DataTable dt = Operation.getDatatable(sql);
                if (dt.Rows.Count > 0)
                {

                    string position = dt.Rows[0]["position"].ToString();
                    string cname = dt.Rows[0]["candidate_name"].ToString();

                }*/

                /* string tname = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();*/
                else
                {
                    WebMessageBox.Show("投票还未结束，不可以查看");
                }
               

            }
           
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


        protected void Button1_Click(object sender, EventArgs e)
        {
            bind();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //新增---跳转到自荐
            Response.Redirect("studrecommend.aspx");
        }

   
    }
}