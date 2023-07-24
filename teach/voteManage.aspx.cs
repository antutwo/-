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
    public partial class voteManage : System.Web.UI.Page
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
                //绑定
                bind();
            }
        }
        public void bind()
        {
            ///
                string sqlstr = "select a.*,isNULL(b.elect_numbers,0)as elect_numbers ,isNULL(c.candidate_num,0)as candidate_num,isNULL(d.position_votes,0)as position_votes from Tx_Gposition as a left join " +
                       "(select count(*) as elect_numbers, position from Tx_elect where position " +
                       "in (select position_name from Tx_Gposition) group by position )as b on a.position_name = b.position left join " +
                       "(select count(*)as candidate_num, position from Tx_candidate where position in (select position_name from Tx_Gposition) group by position) " +
                       "as c on a.position_name = c.position left join " +
                       "(select count(*) as position_votes,position from Tx_vote where position in(select position_name from Tx_Gposition) " +
                       "group by position)as d on a.position_name = d.position " +
                       "where (a.position_name like '%" + this.findinfo.Text + "%' OR LEN('" + this.findinfo.Text + "') = 0) and grade_id = '" + Session["gid"] + "' order by a.position_name";

            /*string sqlstr = " select a.*,b.candidate_num,c.position_votes,d.elect_numbers from Tx_Gposition as a left join " +
                "(select count(*) as candidate_num, position from Tx_candidate where position in (select position_name from Tx_Gposition) group by position) " +
                "as b on a.position_name = b.position left join(select count(*)as position_votes,position from Tx_vote where position in(select position_name from Tx_Gposition) " +
                "group by position) as c on b.position = c.position left join(select count(*)as elect_numbers,position from Tx_elect where position " +
                "in(select position_name from Tx_Gposition) group by position ) as d on c.position = d.position where " +
                "(a.position_name like '%" + this.findinfo.Text + "%' OR LEN('" + this.findinfo.Text + "') = 0) and grade_id = '"+Session["gid"]+"' order by a.position_name";*/

     
            GridView1.DataSource = Operation.getDatatable(sqlstr);
            ///
            GridView1.DataKeyNames = new string[] { "position_id" };//设置主键
            GridView1.DataBind();

          
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bind();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("../teach/addPosition.aspx");
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
            ////待修改
            string position = GridView1.Rows[e.RowIndex].Cells[1].Text.ToString().Trim();
            string sql1 = "delete  Tx_candidate from Tx_candidate,Tx_Gposition where Tx_candidate.position=Tx_Gposition.position_name and Tx_Gposition.position_name='"+position+ "'and  Tx_candidate.grade_id='" + Session["gid"] + "' ";
            string sql2 = "delete  Tx_vote from Tx_vote,Tx_Gposition where Tx_vote.position=Tx_Gposition.position_name and Tx_Gposition.position_name='" + position + "' and Tx_vote.grade_id='" + Session["gid"] + "'";
            string sql3 = "delete  Tx_elect from Tx_elect,Tx_Gposition where Tx_elect.position=Tx_Gposition.position_name and Tx_Gposition.position_name='" + position + "' and  Tx_elect.grade_id='" + Session["gid"] + "'";
            string sql4 = "delete  Tx_temporary from Tx_temporary,Tx_Gposition where Tx_temporary.position=Tx_Gposition.position_name and Tx_Gposition.position_name='" + position + "' and Tx_temporary.grade_id='" + Session["gid"] + "'";
            ///
            string sqlstr = "delete from Tx_Gposition where position_name='" + position+ "' and grade_id='"+Session["gid"]+"'";
            //GridView1.DataKeys[e.RowIndex].Value.ToString() 获取当前行的主键值
            Operation.runSql(sql1);//删除候选
            Operation.runSql(sql2);//删除投票
            Operation.runSql(sql3);//删除评选
            Operation.runSql(sql4);//删除推荐
            Operation.runSql(sqlstr);//删除职位
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
      
            string state = ((DropDownList)GridView1.Rows[e.RowIndex].FindControl("DropDownList1")).SelectedValue;
            string position = this.GridView1.Rows[e.RowIndex].Cells[1].Text.ToString().Trim();
            string total_num = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();//应评选人数
            //string state = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();//职位候选状态
            string text = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();//候选要求
            string elect = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[9].Controls[0])).Text.ToString().Trim();//评选人数
            
            string elect_num = this.GridView1.Rows[e.RowIndex].Cells[10].Text.ToString().Trim();
            string position_votes = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[7].Controls[0])).Text.ToString().Trim();//可投
            string maximun = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[6].Controls[0])).Text.ToString().Trim();//最大候选人数
            /*            if(state== "正在候选"||state== "投票中" || state == "候选结束")
                        {
            *///state --正在候选 投票中 候选结束
            if (state == "投票中" && elect_num == elect)//已有评选人数已达标  可以投票
            {
                    ///
                Operation.runSql("update Tx_Gposition set number='" + total_num + "',maximun='" + maximun + "',state='" + state + "'," +
               "text='" + text + "',elect_num='" + elect + "',position_vote='" + position_votes + "' where position_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'");

                /*   string sql = "update Tx_candidate set candidate_state=(select state from Tx_position where position_name='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "')" +
                       " where stu_id in (select stu_id from Tx_candidate where position = '" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' and candidate_state != '取消候选')" +
                       " and position = '" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "';";*/
                ///
                string sql1 = "update Tx_candidate set candidate_state='" + state + "' where position='" + position + "' and candidate_state != '取消候选'";
                string sql2 = "update Tx_temporary set refer_state='推荐失败' where refer_state='正在推荐' and position='"+position+"' and grade_id='"+Session["gid"]+"'";
                Operation.runSql(sql1);
                Operation.runSql(sql2);
                GridView1.EditIndex = -1;//从编辑状态改为浏览状态
                bind();
            }
            else if (state == "投票中" && elect_num != elect)  //评选人数未达标不可以开始投票
            {
                WebMessageBox.Show("候选人还未满，不可开始投票");
            }
            else  //其他状态
            {
                    ///
                Operation.runSql("update Tx_Gposition set number='" + total_num + "',maximun='" + maximun + "',state='" + state + "'," +
               "text='" + text + "',elect_num='" + elect + "',position_vote='" + position_votes + "' where position_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'");
                string sql = "update Tx_candidate set candidate_state='" + state + "' where position='" + position + "' and candidate_state != '取消候选'";
                Operation.runSql(sql);
                GridView1.EditIndex = -1;//从编辑状态改为浏览状态
                bind();
            }
    /*        }
            else
            {
                WebMessageBox.Show("更改的状态不对");
            }*/

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //待修改
            if (e.CommandName == "btn_addC")//查看是否是按钮事件  ---添加候选人
            {
                int index = Convert.ToInt32(e.CommandArgument);//获取当前行
                ///
                string position = this.GridView1.Rows[index].Cells[1].Text;//获取当前 position_name
                string sql = "select state from Tx_Gposition where grade_id='"+Session["gid"]+"' and position_name='"+position+"'";
                string state = null;
                DataTable dt = Operation.getDatatable(sql);
                if (dt.Rows.Count > 0)
                {
                    state = dt.Rows[0]["state"].ToString();
                }
                //string state = this.GridView1.Rows[index].Cells[3].Text; //状态
                string candidate_num = this.GridView1.Rows[index].Cells[4].Text;//候选人数
                string candidate_max = this.GridView1.Rows[index].Cells[5].Text;//最大候选人数
                if (state == "正在候选")
                {
                    if (candidate_num == candidate_max)
                    {
                        WebMessageBox.Show("当前项目候选人已满不能添加");
                    }
                    else
                    {
                        Response.Redirect("../teach/addCandidate.aspx?position=" + position);
                    }
                }
                else
                {
                    WebMessageBox.Show("当前项目不在候选中,不能添加候选人");
                }
                
               
            }
            else if (e.CommandName == "btn_addE")//查看是否是按钮事件  ---添加评选人
            {
                int index = Convert.ToInt32(e.CommandArgument);//获取当前行
                string position = this.GridView1.Rows[index].Cells[1].Text; //获取当前 position_name
                string sql = "select state from Tx_Gposition where grade_id='" + Session["gid"] + "' and position_name='" + position + "'";
                string state = null;
                DataTable dt = Operation.getDatatable(sql);
                if (dt.Rows.Count > 0)
                {
                    state = dt.Rows[0]["state"].ToString();
                }
                //string state = ((DropDownList)GridView1.Rows[index].FindControl("HDFXueli")).SelectedValue;
                //DropDownList dropTemp = (DropDownList)GridView1.Rows[index].Cells[3].FindControl("DropDownList1");
                 //string state = this.GridView1.Rows[index].Cells[3].Text; //状态
                //string state = ((HiddenField)e.Row.FindControl("HDFXueli")).Value;
                //string state = dropTemp.SelectedValue;
                string elect_num = this.GridView1.Rows[index].Cells[10].Text; //已有评选人数
                
                string elect_max = this.GridView1.Rows[index].Cells[9].Text; //最大评选人数
                
                if (state == "正在候选")
                {
                    if (elect_num == elect_max ||elect_max=="全班")
                    {
                        WebMessageBox.Show("评议人数已经为最大了,如果还需要添加请进行修改");
                        /* Response.Redirect("../teach/electManage.aspx");*/
                    }
                    else
                    {
                        Response.Redirect("../teach/addElectTwo.aspx?position=" + position + "&flag=true");
                    }
                   
                }
                else
                {

                    WebMessageBox.Show("当前项目不是候选状态,不能添加评选人");
                }
                

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

         
            if (((DropDownList)e.Row.FindControl("DropDownList1")) != null)
            {
                DropDownList DropDownList1 = (DropDownList)e.Row.FindControl("DropDownList1");

                //  生成 DropDownList 的值，也可以取得数据库中的数据绑定
                DropDownList1.Items.Clear();
                DropDownList1.Items.Add(new ListItem("正在候选", "正在候选"));
                DropDownList1.Items.Add(new ListItem("投票中", "投票中"));
                DropDownList1.Items.Add(new ListItem("候选结束", "候选结束"));
                //

                //  选中 DropDownList
                DropDownList1.SelectedValue = ((HiddenField)e.Row.FindControl("hf")).Value;
                //
            }
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
          /*  Button addButton;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                addButton = (Button)e.Row.Cells[14].Controls[0];
                if (addButton != null)
                {
                    if (addButton.CommandName == "btn_addE")
                        addButton.CommandArgument = e.Row.RowIndex.ToString();
                }
            }*/
        }
    }
}