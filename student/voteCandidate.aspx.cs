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
    public partial class voteCandidate : System.Web.UI.Page
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

                title.Text = '"' + Request.QueryString["position"] + '"' + "候选人信息表";
                //绑定

                bind();
            }
        }
        public void bind()
        {
            string sql = "select elect_num from Tx_Gposition where grade_id='" + Session["grade_id"] + "'";
            
            string sql1 = "select * from Tx_student as a,Tx_grade as b where a.grade_id=b.grade_id and a.stu_id='" + Session["stuid"].ToString() + "'";
            string stu_name = null;
            DataTable dt = Operation.getDatatable(sql1);
            if (dt.Rows.Count > 0)
            {
               stu_name= dt.Rows[0]["stu_name"].ToString();

            }
            ///

            string sql2 = "select a.position_vote,isNULL(b.vote,0) as vote from(select * from Tx_Gposition) a " +
                "left join(select COUNT(0) as 'vote', position from Tx_vote " +
                "where position = '" + Request.QueryString["position"] + "' and vote_name = '" + stu_name + "' and grade_id = '" + Session["grade_id"] + "' and caste = '1' group by position) " +
                "b on b.position = a.position_name and b.position = '" + Request.QueryString["position"] + "' and a.grade_id = '" + Session["grade_id"] + "'";
            /*string sql2 = "select b.position_vote,isNULL(a.vote,0) as vote from(select position,isNULL(count(*),0)as vote from Tx_vote" +
                " where position ='"+ Request.QueryString["position"] + "' and vote_name = '"+stu_name+ "' and grade_id='" + Session["grade_id"] + "' and caste='1' group by all position)as a ," +
                "Tx_Gposition as b where a.position = b.position_name and a.position = '" + Request.QueryString["position"] + "' and b.grade_id='"+Session["grade_id"]+"'";
            */
            DataTable dt2 = Operation.getDatatable(sql2);
            if (dt2.Rows.Count > 0)
            {
                TextBox1.Text = dt2.Rows[0]["position_vote"].ToString();//可投
              
                TextBox2.Text = dt2.Rows[0]["vote"].ToString();//已投

            }
            ///
            string sqlstr = " select * from Tx_candidate where position='"+ Request.QueryString["position"] + "' and grade_id='"+Session["grade_id"]+"' and candidate_state!='取消候选'";    //此职位的所有候选人信息
            GridView1.DataSource = Operation.getDatatable(sqlstr);
            GridView1.DataKeyNames = new string[] { "candidate_id" };//设置主键
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            //重新绑定　
            bind();
        }

        protected void DaoChu_Click(object sender, EventArgs e)
        { DGToExcel(GridView1);
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
           

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btn_votesAdd")//查看是否是按钮事件--投票
            {
                
                    int index = Convert.ToInt32(e.CommandArgument);//获取当前行
                    string key = this.GridView1.DataKeys[index].Value.ToString();//获取当前选点击行的主键值
                    string state = this.GridView1.Rows[index].Cells[5].Text;
                    string id = this.GridView1.Rows[index].Cells[2].Text;
                    string name = this.GridView1.Rows[index].Cells[3].Text;
                    string gid = this.GridView1.Rows[index].Cells[4].Text;

                    string sql1 = "select * from Tx_student as a,Tx_grade as b where a.grade_id=b.grade_id and a.stu_id='" + Session["stuid"].ToString() + "'";
                    string stu_name = null;
                    DataTable dt = Operation.getDatatable(sql1);
                    if (dt.Rows.Count > 0)
                    {
                        stu_name= dt.Rows[0]["stu_name"].ToString();//当前使用系统的人的名字

                    }
                if (state == "投票中")
                    {
                        int vote = int.Parse(TextBox1.Text);//已投
                        int voted = int.Parse(TextBox2.Text);//可投
                        
                    //待修改
                    if (vote-voted>0)//查看评选人是否还有投票机会
                        {
                            string sql = "insert into Tx_vote(position,grade_id,stu_id,stu_name,vote_name)values" +
                          "('" + Request.QueryString["position"] + "','" + gid + "','" + id + "','" + name + "','" + stu_name + "')";
                            Operation.runSql(sql);
                        WebMessageBox.Show("投票成功");
                        }
                        else
                        {
                            WebMessageBox.Show("你的投票机会已用完");
                        }
                       
                        
                    }
                    else
                    {
                        WebMessageBox.Show("投票还未开始");
                    }
                
                

            }
        }
    }
}