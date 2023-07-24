using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tuixuan.util;

namespace tuixuan.admin
{
    public partial class studentadd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["admin_id"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/adminLogin.aspx");
                }
                bind();
            }
        }
        public void bind()
        {
            string sqlstr = "select * from Tx_grade";
            DropDownList1.DataSource = Operation.getDatatable(sqlstr);
            DropDownList1.DataTextField = "grade_name";
            DropDownList1.DataValueField = "grade_id";
            DropDownList1.DataBind();
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
            if (DropDownList1.SelectedValue == "")
            {
                WebMessageBox.Show("请选择班级"); return;
            }
            if (DropDownList2.SelectedValue == "")
            {
                WebMessageBox.Show("请选择性别"); return;
            }
            string sql = "insert into Tx_student(stu_id,stu_name,stu_sex,grade_id,stu_password) values('" +
                TextBox1.Text + "','" + TextBox2.Text + "','"+this.DropDownList2.SelectedValue+"','" + this.DropDownList1.SelectedValue + "','" + TextBox4.Text + "')";
            Operation.runSql(sql);
            WebMessageBox.Show("添加完成", "studentmanage.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("studentmanage.aspx");
        }
    }
}