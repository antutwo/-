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
    public partial class studupdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                if (Session["stuid"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/studentLogin.aspx");
                }
                
                string sql = "select * from Tx_student as a,Tx_grade as b where a.grade_id=b.grade_id and a.stu_id='" + Session["stuid"].ToString() + "'";
                DataTable dt = Operation.getDatatable(sql);
                if (dt.Rows.Count > 0)
                {
                    Label1.Text = "欢迎您登录学生推选管理系统," + dt.Rows[0]["stu_name"].ToString() + "同学";
                    TextBox1.Text = dt.Rows[0]["stu_id"].ToString();
                    TextBox2.Text = dt.Rows[0]["stu_name"].ToString();
                    TextBox3.Text = dt.Rows[0]["stu_password"].ToString();
                    TextBox4.Text = dt.Rows[0]["grade_id"].ToString();
                    TextBox5.Text = dt.Rows[0]["grade_name"].ToString();
                    TextBox6.Text = dt.Rows[0]["stu_sex"].ToString();
                }


            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            string sname = TextBox2.Text;
            string spwd = TextBox3.Text;
            string sex = TextBox6.Text;
            if (sname != "" && spwd != "" && sex != "")
            {
                string sql1 = "select * from Tx_student where stu_id='" + Session["stuid"] + "'";
                string name = null;
                DataTable dt = Operation.getDatatable(sql1);
                if (dt.Rows.Count > 0)
                {
                    name = dt.Rows[0]["stu_name"].ToString();///修改之前的
                }
                Operation.runSql("update Tx_student set stu_name='" + sname + "',stu_password='" + spwd + "',stu_sex='" + sex + "' where stu_id='" + Session["stuid"].ToString() + "'");

                string sql2 = "select * from Tx_candidate where candidate_name='" + name + "'";
                if (Operation.getDatatable(sql2).Rows.Count > 0)//此人之前已在候选表
                {
                    Operation.runSql("update Tx_candidate  set candidate_name='" + sname + "' where stu_id='" + Session["stuid"].ToString() + "'");
                }

                string sql3 = "select * from Tx_elect where elect_name='" + name + "'";
                if (Operation.getDatatable(sql3).Rows.Count > 0)//此人之前已在评选表
                {
                    Operation.runSql("update Tx_elect set elect_name='" + sname + "' where stu_id='" + Session["stuid"].ToString() + "'");
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

                WebMessageBox.Show("修改完成", "studindex.aspx");
            }
            else
            {
                WebMessageBox.Show("修改的内容不能含空");

            }
           
           
        }
    }
}