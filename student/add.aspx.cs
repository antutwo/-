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
    public partial class add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["stuid"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/studentLogin.aspx");
                }
              
                string sql = "select grade_id,stu_name from Tx_student where stu_id='" + Session["stuid"].ToString() + "'";
                DataTable dt = Operation.getDatatable(sql);
                if (dt.Rows.Count > 0)
                {
                    Label1.Text = "欢迎您登录学生推选管理系统," + dt.Rows[0]["stu_name"].ToString() + "同学";
                    TextBox2.Text = dt.Rows[0]["grade_id"].ToString();
                    TextBox5.Text = dt.Rows[0]["stu_name"].ToString();
                   
                }
                /*  string position = Request.QueryString["position"];*/
                TextBox1.Text = Request.QueryString["position"];

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string gid = TextBox2.Text;
            string tname = TextBox3.Text;
            string tid = TextBox4.Text;
            string rname = TextBox5.Text;
            if (tname != "" && tid != "" )
            {
                string sql1 = "select * from Tx_candidate where candidate_name='" + tname + "' and stu_id='" + tid + "' and position='" + Request.QueryString["position"] + "'and grade_id='" + gid + "'";
                string sql2 = "select * from Tx_student where stu_id='" + tid + "' and grade_id='"+gid+"'";
                string sql3 = "select * from Tx_elect where stu_id='" + tid + "' and grade_id='" + gid + "'and position='" + Request.QueryString["position"] + "'";
                ///
                //查询推荐表中是否有了某个班上的某个人为此职位的推荐  每个职位每个人只能推荐一次
                if (Operation.getDatatable("select * from Tx_temporary where refer_name='" + rname + "' and position='" + Request.QueryString["position"] + "' and grade_id='"+gid+"'").Rows.Count > 0)//一个人只有一次推荐机会
                {
                    WebMessageBox.Show("此项目您的推荐次数已经用完");
                }
                else { 
                    if(Operation.getDatatable(sql2).Rows.Count > 0) //判断是不是本班的学生，是
                    { 
                        if (Operation.getDatatable(sql1).Rows.Count > 0)
                        {
                            WebMessageBox.Show("您推荐的这位同学已经在竞争这个职位了");
                        }
                        else
                        {
                            if(Operation.getDatatable("select * from Tx_temporary where stu_id='"+tname+"' and refer_name='"+rname+"' and position='"+ Request.QueryString["position"] + "'").Rows.Count > 0)
                            {//查询是否已经推荐
                                WebMessageBox.Show("您已经推荐过了");
                            }
                            else if (Operation.getDatatable(sql3).Rows.Count > 0)
                            {
                                WebMessageBox.Show("评选人不参与候选");
                            }
                            else
                            {
                                string sql = "insert into Tx_temporary(position,grade_id,stu_name,stu_id,refer_name) values('" + Request.QueryString["position"] + "','" + gid + "','" + tname + "','" + tid + "','" + rname + "')";
                                Operation.runSql(sql);
                                WebMessageBox.Show("推荐完成");
                                /*string sql = "insert into t_taboo(weekdays,sections,weeksstart,weeksend,tabootype) values('" + DropDownList1.SelectedIndex.ToString()
                                 + "','" + DropDownList2.SelectedIndex.ToString() + "','" + DropDownList3.SelectedIndex.ToString() + "','" + DropDownList4.SelectedIndex.ToString() + "','1')";
                                 Operation.runSql(sql);*/
                            }

                        }
                    }
                    else
                    {
                        WebMessageBox.Show("您推荐的这位同学不是本班的");
                    }
                }
            }
            else
            {
                WebMessageBox.Show("输入的内容不能含空");

            }
        }
    }
}