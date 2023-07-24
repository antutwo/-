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
    public partial class addElect : System.Web.UI.Page
    {
        /*  string flag = null;*/
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["teachid"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/teacherLogin.aspx");
                }

                ///
                string sql1 = "select position_name from Tx_Gposition where grade_id='" + Session["gid"] + "' and state='正在候选'";//查找所有职位   ---绑定dropdownlist
                DropDownList1.DataSource = Operation.getDatatable(sql1);
                DropDownList1.DataTextField = "position_name";
                DropDownList1.DataValueField = "position_name";
                DropDownList1.DataBind();
                this.DropDownList1.Items.Insert(0, new ListItem("请选择", "0"));

                string sql = "select teacher_name,grade_id from Tx_teacher where teacher_id='" + Session["teachid"].ToString() + "'";
                DataTable dt = Operation.getDatatable(sql);
                if (dt.Rows.Count > 0)
                {
                    /*Label1.Text = "欢迎您," + dt.Rows[0]["teacher_name"] + "老师";*/
                    Label1.Text = "欢迎您登录学生推选管理系统，" + dt.Rows[0]["teacher_name"] + "老师";

                }
                TextBox3.Text = Session["gid"].ToString();

                 string position = DropDownList1.SelectedValue.ToString();
                 string sql4 = "select stu_id,stu_name from Tx_student where stu_id not in(select stu_id from Tx_elect where position='" + position + "')" +
                " and stu_id not in(select stu_id from Tx_candidate where position='" + position + "') and grade_id='" + TextBox3.Text + "' ";

                    DropDownList3.DataSource = Operation.getDatatable(sql4);
                    DropDownList3.DataTextField = "stu_id";
                    DropDownList3.DataValueField = "stu_id";
                    DropDownList3.DataBind();
                    string sql2 = "select stu_name from Tx_student where stu_id='" + DropDownList3.SelectedValue.ToString() + "'";

                    DataTable dt1 = Operation.getDatatable(sql2);
                    if (dt1.Rows.Count > 0)
                    {
                        TextBox1.Text = dt1.Rows[0]["stu_name"].ToString();
                    }
                    ///
                    string sql3 = "select position_vote from Tx_Gposition where position_name='" + position + "' and grade_id='" + Session["gid"] + "'";//查找某个职位的票数
                    dt1 = Operation.getDatatable(sql3);
                    if (dt1.Rows.Count > 0)
                    {
                        TextBox2.Text = dt1.Rows[0]["position_vote"].ToString();
                    }
                

        }
            

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string position = DropDownList1.SelectedValue.ToString();
            string sid = DropDownList3.SelectedValue.ToString();
            string sname = TextBox1.Text;
            string vote= TextBox2.Text;
            string gid = TextBox3.Text;
            //判断是否还可以在添加
            ///
            string sql1 = " select a.elect_num,isNULL(d.num,0)as num from Tx_Gposition as a left join " +
                "(select count(*) as num, position from Tx_elect where position in (select position_name from Tx_position)" +
                "group by position ) as d on a.position_name = d.position where a.position_name = '"+position+"' and grade_id = '"+Session["gid"]+"' order by a.position_name; ";
            string sql2 = "select * from Tx_elect where position='"+position+"' and grade_id='"+gid+"' and elect_name='"+sname+"'";
            int tnum=0, num=0;
            DataTable dt = Operation.getDatatable(sql1);
            if (dt.Rows.Count > 0)
            {
                tnum = int.Parse(dt.Rows[0]["elect_num"].ToString());//总
                num = int.Parse(dt.Rows[0]["num"].ToString());//已有
            }
            if (tnum - num > 0)
            {
                if (Operation.getDatatable(sql2).Rows.Count > 0)
                {
                    WebMessageBox.Show("此同学已是此职位的评选人了");
                }
                else
                {
                    string sql3 = "insert into Tx_elect(position,stu_id,elect_name,grade_id) values" +
                    "('" + position + "','" + sid + "','" + sname + "','" + gid + "')";
                    Operation.runSql(sql3);
                    WebMessageBox.Show("插入成功");
                }
                
            }
            
            
            else
            {
                WebMessageBox.Show("不能再新增");
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("electManage.aspx");
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

         
            string position = DropDownList1.SelectedValue.ToString();
            string sql = "select stu_id,stu_name from Tx_student where stu_id not in(select stu_id from Tx_elect where position='" + position + "')" +
                " and stu_id not in(select stu_id from Tx_candidate where position='"+position+"') and grade_id='"+TextBox3.Text+"' ";
            DropDownList3.DataSource = Operation.getDatatable(sql);
            DropDownList3.DataTextField = "stu_id";
            DropDownList3.DataValueField = "stu_id";
            DropDownList3.DataBind();

            TextBox1.Text = " ";
            ///
            string sql3 = "select position_vote from Tx_Gposition where position_name='" + position + "' and grade_id='"+Session["gid"]+"'";//查找某个职位的票数
            DataTable dt = Operation.getDatatable(sql3);
            if (dt.Rows.Count > 0)
            {
                TextBox2.Text = dt.Rows[0]["position_vote"].ToString();
            }
          
        /*    flag = "false";*/

        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql2 = "select stu_name from Tx_student where stu_id='" + DropDownList3.SelectedValue.ToString() + "'";
            DataTable dt = Operation.getDatatable(sql2);
            if (dt.Rows.Count > 0)
            {
                TextBox1.Text = dt.Rows[0]["stu_name"].ToString();
            }

        }
    }
}