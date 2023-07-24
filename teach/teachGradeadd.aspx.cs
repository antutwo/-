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
    public partial class teachGradeadd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["teachid"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/teacherLogin.aspx");
                }
            }
            string sql = "select grade_id,teacher_name from Tx_teacher where teacher_id='" + Session["teachid"].ToString() + "'";//因为一个老师只能带一个班级所以查询出来的班号一定是唯一的
            DataTable dt = Operation.getDatatable(sql);
            if (dt.Rows.Count > 0)
            {
                /*Label1.Text = "欢迎您," + dt.Rows[0]["teacher_name"] + "老师";*/
                Label1.Text = "欢迎您登录学生推选管理系统，" + dt.Rows[0]["teacher_name"] + "老师";
                TextBox3.Text = dt.Rows[0]["grade_id"].ToString();
            }
           
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //添加
            if (TextBox1.Text == "")
            {
                WebMessageBox.Show("请输入学生学号"); return;
            }
            if (Operation.getDatatable("select * from Tx_student where stu_id='" + TextBox1.Text + "'").Rows.Count > 0)
            {
                WebMessageBox.Show("此学生学号已经存在"); return;
            }
            if (TextBox2.Text == "")
            {
                WebMessageBox.Show("请输入学生姓名"); return;
            }
            if (DropDownList2.SelectedValue == "")
            {
                WebMessageBox.Show("请选择性别"); return;
            }
            string sql = "insert into Tx_student(stu_id,stu_name,stu_sex,grade_id,stu_password) values('" +
                TextBox1.Text + "','" + TextBox2.Text + "','" + this.DropDownList2.SelectedValue + "','" + TextBox3.Text + "','" + TextBox4.Text + "')";
            Operation.runSql(sql);
            WebMessageBox.Show("添加完成", "teachGrade.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("teachGrade.aspx");
        }
    }
}