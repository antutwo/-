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
    public partial class studrecommend : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["stuid"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/studentLogin.aspx");
                }
                string sql1 = "select stu_name from Tx_student where stu_id='" + Session["stuid"].ToString() + "'";
                DataTable dt = Operation.getDatatable(sql1);
                if (dt.Rows.Count > 0)
                {
                    Label1.Text = "欢迎您登录学生推选管理系统," + dt.Rows[0]["stu_name"].ToString() + "同学";
                }
                ///
                string sql = "select position_name from Tx_Gposition where grade_id='"+Session["grade_id"]+"'";//查找本班所有职位   ---绑定dropdownlist
                findinfoDropdl.DataSource = Operation.getDatatable(sql);
                findinfoDropdl.DataTextField = "position_name";
                findinfoDropdl.DataValueField = "position_name";
                findinfoDropdl.DataBind();
                this.findinfoDropdl.Items.Insert(0, new ListItem("全部", "0"));

                //绑定
                bind();
            }
        }
        public void bind()

        {
        
            string sqlstr = null;
            if (this.findinfoDropdl.SelectedValue.ToString() == "0")//查找全部
            {

                ///
                /*统计某班的职位信息以及现有候选人数  并将查询到为空的值设置为0*/
                sqlstr = " select a.*,isNULL(b.num,0)as num  from (select * from Tx_Gposition where grade_id='"+Session["grade_id"]+"') as a left join" +
                    "(select count(*) as num, position from Tx_candidate where grade_id = '" + Session["grade_id"] + "' group by position) as b on a.position_name = b.position ";
                /*"select a.position_name,num,a.number,state,text,a.elect_num,a.maximun from Tx_position as a,(select number,position_name from Tx_position)as b," +
                                    "(select count(*) as num, position from Tx_candidate group by position) as c where a.position_name = b.position_name and b.position_name = c.position and a.grade_id = '"+Session["grade_id"] +"' ";*/
            }
            else
            {
                ///
                sqlstr = " select distinct b.* from  Tx_Gposition as a,(select a.*,isNULL(b.num,0)as num  from (select * " +
                    "from Tx_Gposition where grade_id='" + Session["grade_id"] + "') as a left join (select count(*) as num, position from Tx_candidate " +
                    "where grade_id = '" + Session["grade_id"] + "' and position = '" + this.findinfoDropdl.SelectedValue.ToString() + "' group by position) as b on a.position_name = b.position)as b " +
                    "where a.position_name = b.position_name and a.position_name = '"+this.findinfoDropdl.SelectedValue.ToString()+"'";

               
            }
            GridView1.DataSource = Operation.getDatatable(sqlstr);
            GridView1.DataKeyNames = new string[] { "position_id" };//设置主键
            GridView1.DataBind();
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
            if (e.CommandName == "btn_add")//查看是否是按钮事件
            {
              
                int index = Convert.ToInt32(e.CommandArgument);//获取当前行
                string key = this.GridView1.DataKeys[index].Value.ToString();//获取当前选点击行的主键值
                ///
                string position = this.GridView1.Rows[index].Cells[1].Text;
                string state = this.GridView1.Rows[index].Cells[4].Text;
                ///
                if (this.GridView1.Rows[index].Cells[5].Text == this.GridView1.Rows[index].Cells[6].Text)//候选人数已到了最大候选
                {
                    WebMessageBox.Show(position + "项目候选人已满，再看看其他项目吧");
                }
                else//人数没满
                {

                    if (state == "正在候选")
                    {
                        Response.Redirect("../student/add.aspx?position=" + position);
                    }
                    else
                    {
                        WebMessageBox.Show("该项目的状态不可再推荐");
                    }
                }

            }

            /*
             
                int index = Convert.ToInt32(e.CommandArgument);//获取当前行
                string key = this.GridView1.DataKeys[index].Value.ToString();//获取当前选点击行的主键值
                string position = this.GridView1.Rows[index].Cells[1].Text;//获取当前点击行，某列的值
             
                if (this.GridView1.Rows[index].Cells[2].Text != "投票中")//如果候选状态是“投票结束”则可以查看投票详情
                {
                    Response.Redirect("../student/studvoteDetails.aspx?position=" + position );//跳转到投票详情页
                }*/


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
    }
}