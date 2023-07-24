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
    public partial class teachGrade : System.Web.UI.Page
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
                    Label1.Text = "欢迎您登录学生推选管理系统，" + dt.Rows[0]["teacher_name"] + "老师";
                   
                }
                //绑定
                bind();
            }
        }
        public void bind()
        {
            string sqlstr = "select a.stu_id,a.stu_name,a.stu_sex,a.stu_password,a.grade_id,c.grade_name from" +
                " Tx_student as a,Tx_teacher as b,Tx_grade as c where a.grade_id=b.grade_id and b.grade_id=c.grade_id and" +
                "(stu_name like '%" + this.findinfo.Text + "%' OR LEN('" + this.findinfo.Text + "')=0) and a.grade_id='"+Session["gid"].ToString()+"' order by a.stu_id";
            GridView1.DataSource = Operation.getDatatable(sqlstr);
            GridView1.DataKeyNames = new string[] { "stu_id" };//设置主键
            GridView1.DataBind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            bind();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sqlstr = "delete from Tx_student where stu_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";
            //GridView1.DataKeys[e.RowIndex].Value.ToString() 获取当前行的主键值
            Operation.runSql(sqlstr);
            bind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            /* 取触发了编辑的那行的索引值赋值GridView1.EditIndex*/
            bind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            /* (GridView1.Rows[e.RowIndex].Cells[1].Controls[0])  获取GridView控件的第 e.RowIndex+1行的第二列单元格内的第一个控件
            e.RowIndex 是指当前鼠标选中行的序号，+1是因为数组的下标从0开始，0表示为1，1表示为2
           然后在使用（TextBox）强制类型转换 将获取到控件强转为TextBox控件，在获取他的Text值，转成string类型的值，Trim()再去掉文本开头和结尾的空格*/
            if (((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim() == "")
            {
                WebMessageBox.Show("请输入学生姓名"); return;
            }
            string gid = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
            string sname = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
            ////
            string sql1 = "select * from Tx_student where stu_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";
            string name = null;
            DataTable dt = Operation.getDatatable(sql1);
            if (dt.Rows.Count > 0)
            {
                name = dt.Rows[0]["stu_name"].ToString();///修改之前的
            }
            string sql2 = "select * from Tx_candidate where candidate_name='" + name + "'";
            if (Operation.getDatatable(sql2).Rows.Count > 0)//此人之前已在候选表
            {
                Operation.runSql("update Tx_candidate  set candidate_name='" + sname + "' where stu_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'");
            }

            string sql3 = "select * from Tx_elect where elect_name='" + name + "'";
            if (Operation.getDatatable(sql3).Rows.Count > 0)//此人之前已在评选表
            {
                Operation.runSql("update Tx_elect set elect_name='" + sname + "' where stu_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'");
            }

            string sql4 = "select * from Tx_temporary where refer_name='" + name + "'";
            if (Operation.getDatatable(sql4).Rows.Count > 0)//此人之前已在推荐表
            {
                Operation.runSql("update Tx_temporary set refer_name='" + sname + "' where refer_name='" + name + "'");
            }
            string sql6 = "select * from Tx_temporary where stu_name='" + name + "'";
            if (Operation.getDatatable(sql6).Rows.Count > 0)//此人之前已在推荐表是被推荐人
            {
                Operation.runSql("update Tx_temporary set stu_name='" + sname + "' where stu_name='" + name + "'");
            }

            string sql5 = "select * from Tx_vote where vote_name='" + name + "'";
            if (Operation.getDatatable(sql5).Rows.Count > 0)//此人之前已在投票表
            {
                Operation.runSql("update Tx_vote set vote_name='" + sname + "' where vote_name='" + name + "'");
            }
            string sql7 = "select * from Tx_vote where stu_name='" + name + "'";
            if (Operation.getDatatable(sql7).Rows.Count > 0)//此人之前已在投票表 是被投票人
            {
                Operation.runSql("update Tx_vote set stu_name='" + sname + "' where stu_name='" + name + "'");
            }

            Operation.runSql("update Tx_student set stu_name='" + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim() +
                    "',stu_sex='" + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim() +
                    "',stu_password='" + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim() +
                    "' where stu_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'");
          
            GridView1.EditIndex = -1;//从编辑状态改为浏览状态
            bind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            //重新绑定　
            bind();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //新增
            Response.Redirect("teachGradeadd.aspx");
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