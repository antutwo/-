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
    public partial class recommendManage : System.Web.UI.Page
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
                    /*Label1.Text = "欢迎您," + dt.Rows[0]["teacher_name"] + "老师";*/
                    Label1.Text = "欢迎您登录学生推选管理系统，" + dt.Rows[0]["teacher_name"] + "老师";

                }

                ///
                string sql2 = "select distinct position from Tx_temporary where grade_id='" + Session["gid"]+"'";//查找所有职位   ---绑定dropdownlist
                findinfoDropdl.DataSource = Operation.getDatatable(sql2);
                findinfoDropdl.DataTextField = "position";
                findinfoDropdl.DataValueField = "position";
                findinfoDropdl.DataBind();
                this.findinfoDropdl.Items.Insert(0, new ListItem("全部", "0"));

                bind();
                //绑定
             
            }
        }
        public void bind()
        {
            string sqlstr = null;
            if (this.findinfoDropdl.SelectedValue.ToString() == "0")//查找全部
            {
                ///
                sqlstr = "select * from Tx_temporary where refer_state='正在推荐' and grade_id='" + Session["gid"] + "'";
            }
            else
            {
                ///
                sqlstr = "select * from Tx_temporary where position='"+findinfoDropdl.SelectedValue.ToString()+ "'and refer_state='正在推荐' and grade_id='" + Session["gid"] + "'";
            }
            GridView1.DataSource = Operation.getDatatable(sqlstr);
            GridView1.DataKeyNames = new string[] { "temporary_id" };//设置主键
            GridView1.DataBind();
        }

      

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btn_addT")//查看是否是按钮事件  ---添加候选人
            {
                int index = Convert.ToInt32(e.CommandArgument);//获取当前行
                string key = this.GridView1.DataKeys[index].Value.ToString();//获取当前选点击行的主键值  position_name
                string position = this.GridView1.Rows[index].Cells[1].Text;
                string stu_id = this.GridView1.Rows[index].Cells[2].Text;
                string name = this.GridView1.Rows[index].Cells[3].Text;
                string grade_id = this.GridView1.Rows[index].Cells[5].Text;
                ///
                string sql1 = "select state,maximun from Tx_Gposition where position_name='" + position + "' and grade_id='" + Session["gid"] + "'";
                string state = null;
                int max = 0;
                ///
                string sql2 = "select count(*)as counts from Tx_candidate where position='"+position+ "' and grade_id='" + Session["gid"] + "'";
                int count = 0;
                DataTable dt = Operation.getDatatable(sql1);
                if (dt.Rows.Count>0)
                {
                    state = dt.Rows[0]["state"].ToString();
                    max = Convert.ToInt32(dt.Rows[0]["maximun"].ToString());
                }
                dt = Operation.getDatatable(sql2);
                if(dt.Rows.Count > 0)
                {
                    count = Convert.ToInt32(dt.Rows[0]["counts"].ToString());
                }
                if (state == "正在候选")
                {
                    if (max - count > 0)
                    {
                        string sql3 = "insert into Tx_candidate(position,stu_id,candidate_name,grade_id) " +
                            "values('"+position+"','"+stu_id+"','"+name+"','"+grade_id+"')";
                        string sql4 = "update Tx_temporary set refer_state='推荐成功' where temporary_id='" + this.GridView1.DataKeys[index].Value.ToString() + "'";
                        Operation.runSql(sql3);
                        Operation.runSql(sql4);
                        bind();
                    }
                    else
                    {
                        WebMessageBox.Show("候选人数已满");

                    }
                }
                else
                {
                    WebMessageBox.Show("正在候选中，不可添加");
                }

            }
            else if (e.CommandName == "btn_del")//查看是否是按钮事件  
            {
                int index = Convert.ToInt32(e.CommandArgument);//获取当前行
                string sql = "update Tx_temporary set refer_state='推荐失败' where temporary_id='"+ this.GridView1.DataKeys[index].Value.ToString() + "'";
                Operation.runSql(sql);
                bind();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            //重新绑定　
            bind();
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