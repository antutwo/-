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
    public partial class studvotes : System.Web.UI.Page
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
                if (dt.Rows.Count > 0)
                {
                    Label1.Text = "欢迎您登录学生推选管理系统," + dt.Rows[0]["stu_name"].ToString() + "同学";

                }
                /*title.Text = '"' + Request.QueryString["position"] + '"' + "获票详情表";*/
                ///
                string sql2 = "select position_name from Tx_Gposition where grade_id='"+Session["grade_id"]+"'";//查找所有职位   ---绑定dropdownlist
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

            if (this.findinfoDropdl.SelectedValue.ToString() == "0")
            {
                ///
                sqlstr = "select * from Tx_Gposition where grade_id='" + Session["grade_id"] + "'";
            }
            else
            {
                ///
                sqlstr = "select * from Tx_Gposition where position_name='" + this.findinfoDropdl.SelectedValue.ToString() + "' and grade_id='" + Session["grade_id"] + "'";
            }

            GridView1.DataSource = Operation.getDatatable(sqlstr);
            GridView1.DataKeyNames = new string[] { "position_id" };//设置主键
            GridView1.DataBind(); ;
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
                string position = this.GridView1.Rows[index].Cells[1].Text;
                string state = this.GridView1.Rows[index].Cells[3].Text;
                if (state == "候选结束")
                {
                    Response.Redirect("../student/CandidateResults.aspx?position=" + position);
                }
                else
                {
                    string sql1 = "select * from Tx_candidate where stu_id='" + Session["stuid"] + "' and position='" +position+"'";
                    //在候选表中查看此职位是否为候选人  不可以投票

                    string sql2 = "select * from Tx_elect where stu_id='" + Session["stuid"] + "' and position='" + position + "'";
                    ///
                    string sql3= "select elect_num from Tx_Gposition where position_name='" + position + "' and grade_id='"+Session["grade_id"]+"'";
                    DataTable dt = Operation.getDatatable(sql3);
                    string elect_num = null;
                    if (dt.Rows.Count > 0)
                    {
                        elect_num = dt.Rows[0]["elect_num"].ToString();
                    }
                    //是否为此职位的侯选人
                    if (Operation.getDatatable(sql1).Rows.Count > 0)//是候选人
                    { 
                        //查看候选人页面
                        Response.Redirect("../student/voteCandidateDetails.aspx?position=" + position);

                    }
                     else
                    {
                        //WebMessageBox.Show(Operation.getDatatable(sql2).Rows.Count.ToString());
                        if(Operation.getDatatable(sql2).Rows.Count < 1&&elect_num!="全班")//不是评选人
                        {
                            
                            Response.Redirect("../student/voteCandidateDetails.aspx?position=" + position);
                        }
                        
                     else if(Operation.getDatatable(sql2).Rows.Count < 1 && elect_num == "全班")
                        {
                            
                            Response.Redirect("../student/voteCandidate.aspx?position=" + position);//是评选人--投票页面
                        }
                        else
                        {
                            Response.Redirect("../student/voteCandidate.aspx?position=" + position);//是评选人--投票页面
                        }
                        
                    }  
                    
                }
               

            }
        }
    }
}