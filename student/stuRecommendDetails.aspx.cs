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
    public partial class stuRecommendDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["stuid"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/studentLogin.aspx");
                }
                string sql1 = "select * from Tx_student as a,Tx_grade as b where a.grade_id=b.grade_id and a.stu_id='" + Session["stuid"].ToString() + "'";
                DataTable dt = Operation.getDatatable(sql1);
                if (dt.Rows.Count > 0)
                {
                    Label1.Text = "欢迎您登录学生推选管理系统," + dt.Rows[0]["stu_name"].ToString() + "同学";
                }

                string sql = "select position,temporary_id from Tx_temporary where refer_name=(select stu_name from Tx_student where stu_id='" + Session["stuid"] + "')";//查找所有职位   ---绑定dropdownlist
                findinfoDropdl.DataSource = Operation.getDatatable(sql);
                findinfoDropdl.DataTextField = "position";
                findinfoDropdl.DataValueField = "position";
                findinfoDropdl.DataBind();
                this.findinfoDropdl.Items.Insert(0, new ListItem("全部", "0"));

                //绑定
                bind();
            }
        }
        public void bind()
        {
            string sqlstr = null;
            if (this.findinfoDropdl.SelectedValue.ToString() == "0")
            {
                sqlstr= "select * from  Tx_temporary where refer_name=(select stu_name from Tx_student where stu_id='" + Session["stuid"] + "') and grade_id='"+Session["grade_id"]+"'";
            }
            else
            {
                sqlstr = "select * from  Tx_temporary where refer_name=(select stu_name from Tx_student where stu_id='" + Session["stuid"] + "') and position='"+this.findinfoDropdl.SelectedValue+ "'and grade_id='" + Session["grade_id"] + "'";
            }   

            GridView1.DataSource = Operation.getDatatable(sqlstr);
            GridView1.DataKeyNames = new string[] { "temporary_id" };//设置主键d
            GridView1.DataBind();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("../student/studrecommend.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            //重新绑定　
            bind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btn_del")//查看是否是按钮事件
            {

                int index = Convert.ToInt32(e.CommandArgument);//获取当前行
                string key = this.GridView1.DataKeys[index].Value.ToString();//获取当前选点击行的主键值
                string tem_state = this.GridView1.Rows[index].Cells[6].Text;
                if (tem_state != "推荐成功")
                {
                    
                    string sql = "delete from Tx_temporary where temporary_id='" + key + "'";
                    Operation.runSql(sql);
                    WebMessageBox.Show("取消推荐成功");
                    bind();
                }
                else
                {
                    WebMessageBox.Show( "已经推荐成功了，不能再取消");
                    
                }

            }
            if (e.CommandName == "btn_look")//查看是否是按钮事件
            {

                int index = Convert.ToInt32(e.CommandArgument);//获取当前行
                string key = this.GridView1.DataKeys[index].Value.ToString();//获取当前选点击行的主键值
                string position = this.GridView1.Rows[index].Cells[1].Text;
                string tem_state = this.GridView1.Rows[index].Cells[6].Text;
                string sid = this.GridView1.Rows[index].Cells[3].Text;
                string sname = this.GridView1.Rows[index].Cells[4].Text;
                string state = null;
                if (tem_state == "推荐成功")
                {
                    string sql = "select * from Tx_Gposition where position_name='"+position+"' and grade_id='"+Session["grade_id"] +"'";
                    DataTable dt = Operation.getDatatable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        state = dt.Rows[0]["state"].ToString();
                    }
                    if (state == "候选结束")
                    {
                        Response.Redirect("../student/temVoteDetails.aspx?sid="+sid+"&position="+position+"&sname="+sname);
                    }
                    else
                    {
                        WebMessageBox.Show("该项目还未结束，不可查看");
                    }
                          
                }
                else
                {
                    WebMessageBox.Show("未推荐成功");

                }

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
    }
}