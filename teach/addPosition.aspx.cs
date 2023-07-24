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
    public partial class addPosition : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["teachid"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/teacherLogin.aspx");
                }


                string sql = "select teacher_name,grade_id from Tx_teacher where teacher_id='" + Session["teachid"].ToString() + "'";
                DataTable dt = Operation.getDatatable(sql);
                if (dt.Rows.Count > 0)
                {
                    Label1.Text = "欢迎您登录学生推选管理系统," + dt.Rows[0]["teacher_name"] + "老师";

                }

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string position = TextBox1.Text;
            string number = TextBox2.Text;
            string maximun = TextBox3.Text;
            string text = TextBox4.Text;
            string elect_num = TextBox5.Text;
            string position_vote = TextBox6.Text;
            ///
            //查询职位总表中是否有这个职位的信息
            string sql1 = "select * from Tx_position where position_name='"+position+"'";
            string sql2 = "insert into Tx_position(position_name) values('" + position + "')";
            //查看是否本班已存在这个职位 
            string sql3 = "select * from Tx_Gposition where position_name='" + position + "' and grade_id='" + Session["gid"] + "'";
            if (Operation.getDatatable(sql1).Rows.Count >0)
            {
                if (position == "" || number == "" || position_vote == "")
                {
                    WebMessageBox.Show("项目，人数，投票数不能为空");
                }
                else if(Operation.getDatatable(sql3).Rows.Count > 0)
                {
                    WebMessageBox.Show("本班此项目已发起");
                }
                else //有则在班级职位表中插入
                {
                    
                    string sql = null;
                    if (maximun == "" && text != "" && elect_num != "")
                    {
                        ///
                        
                        sql = "insert into Tx_Gposition (position_name,number,text,elect_num,position_vote,grade_id)values" +
                             "('" + position + "','" + number + "','" + text + "','" + elect_num + "','" + position_vote + "','" + Session["gid"] + "')";
                    }
                    else if (maximun == "" && text != "" && elect_num == "")
                    {
                        sql = "insert into Tx_Gposition (position_name,number,text,position_vote,grade_id)values" +
                       "('" + position + "','" + number + "','" + text + "','" + position_vote + "','" + Session["gid"] + "')";
                    }
                    else if (maximun == "" && text == "" && elect_num != "")
                    {
                        sql = "insert into Tx_Gposition (position_name,number,elect_num,position_vote,grade_id)values" +
                        "('" + position + "','" + number + "','" + elect_num + "','" + position_vote + "','" + Session["gid"] + "')";
                    }
                    else if (maximun == "" && text == "" && elect_num == "")
                    {
                        sql = "insert into Tx_Gposition (position_name,number,position_vote,grade_id)values" +
                           "('" + position + "','" + number + "','" + position_vote + "','" + Session["gid"] + "')";
                    }

                    else if (maximun != "" && text== "" && elect_num != "")//
                    {
                        sql = "insert into Tx_Gposition (position_name,number,maximun,elect_num,position_vote,grade_id)values" +
                            "('" + position + "','" + number + "','" + maximun + "','" + elect_num + "','" + position_vote + "','" + Session["gid"] + "')";
                    }
                    else if (maximun != "" && text!= "" && elect_num == "")//
                    {
                        sql = "insert into Tx_Gposition (position_name,number,maximun,text,position_vote,grade_id)values" +
                            "('" + position + "','" + number + "','" + maximun + "','" + text + "','" + elect_num + "','" + position_vote + "','" + Session["gid"] + "')";
                    }

                    else if (maximun != "" && text == "" && elect_num == "")//
                    {
                        sql = "insert into Tx_Gposition (position_name,number,maximun,position_vote,grade_id)values" +
                         "('" + position + "','" + number + "','" + maximun + "','" + position_vote + "','" + Session["gid"] + "')";
                    }
                    else
                    {
                       /* if (text == "")
                        {
                            WebMessageBox.Show("111");
                        }
                        else
                        {
                            WebMessageBox.Show("222");
                        }*/
                        sql = "insert into Tx_Gposition (position_name,number,maximun,text,elect_num,position_vote,grade_id)values" +
                           "('" + position + "','" + number + "','" + maximun + "','" + text + "','" + elect_num + "','" + position_vote + "','" + Session["gid"] + "')";
                    }
                    Operation.runSql(sql);
                    WebMessageBox.Show("插入成功");
                }
                
            }
           else {
                Operation.runSql(sql2);//在总职位表中插入
                if (position == "" || number == "" || position_vote == "")
                {
                    WebMessageBox.Show("项目，人数，投票数不能为空");
                }
                else //有则在班级职位表中插入
                {

                    string sql = null;
                    if (maximun == "" && text != "" && elect_num != "")
                    {
                        ///

                        sql = "insert into Tx_Gposition (position_name,number,text,elect_num,position_vote,grade_id)values" +
                             "('" + position + "','" + number + "','" + text + "','" + elect_num + "','" + position_vote + "','" + Session["gid"] + "')";
                    }
                    else if (maximun == "" && text != "" && elect_num == "")
                    {
                        sql = "insert into Tx_Gposition (position_name,number,text,position_vote,grade_id)values" +
                       "('" + position + "','" + number + "','" + text + "','" + position_vote + "','" + Session["gid"] + "')";
                    }
                    else if (maximun == "" && text == "" && elect_num != "")
                    {
                        sql = "insert into Tx_Gposition (position_name,number,elect_num,position_vote,grade_id)values" +
                        "('" + position + "','" + number + "','" + elect_num + "','" + position_vote + "','" + Session["gid"] + "')";
                    }
                    else if (maximun == "" && text == "" && elect_num == "")
                    {
                        sql = "insert into Tx_Gposition (position_name,number,position_vote,grade_id)values" +
                           "('" + position + "','" + number + "','" + position_vote + "','" + Session["gid"] + "')";
                    }

                    else if (maximun != "" && text == "" && elect_num != "")//
                    {
                        sql = "insert into Tx_Gposition (position_name,number,maximun,elect_num,position_vote,grade_id)values" +
                            "('" + position + "','" + number + "','" + maximun + "','" + elect_num + "','" + position_vote + "','" + Session["gid"] + "')";
                    }
                    else if (maximun != "" && text != "" && elect_num == "")//
                    {
                        sql = "insert into Tx_Gposition (position_name,number,maximun,text,position_vote,grade_id)values" +
                            "('" + position + "','" + number + "','" + maximun + "','" + text + "','" + elect_num + "','" + position_vote + "','" + Session["gid"] + "')";
                    }

                    else if (maximun != "" && text == "" && elect_num == "")//
                    {
                        sql = "insert into Tx_Gposition (position_name,number,maximun,position_vote,grade_id)values" +
                         "('" + position + "','" + number + "','" + maximun + "','" + position_vote + "','" + Session["gid"] + "')";
                    }
                    else
                    {
                        /* if (text == "")
                         {
                             WebMessageBox.Show("111");
                         }
                         else
                         {
                             WebMessageBox.Show("222");
                         }*/
                        sql = "insert into Tx_Gposition (position_name,number,maximun,text,elect_num,position_vote,grade_id)values" +
                           "('" + position + "','" + number + "','" + maximun + "','" + text + "','" + elect_num + "','" + position_vote + "','" + Session["gid"] + "')";
                    }
                    Operation.runSql(sql);
                    WebMessageBox.Show("插入成功");
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("voteManage.aspx");
        }
    }
}