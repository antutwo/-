using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tuixuan.util;

namespace tuixuan.admin
{
    public partial class teacheradd : System.Web.UI.Page
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
           /* this.DropDownList1.Items.Insert(0, new ListItem(" ", "0"));*/
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //添加
            if (TextBox1.Text == "")
            {
                WebMessageBox.Show("请输入教师工号"); return;
            }
            if (Operation.getDatatable("select * from Tx_teacher where teacher_id='" + TextBox1.Text + "'").Rows.Count > 0)
            {
                WebMessageBox.Show("此教师工号已经存在"); return;
            }
            if (TextBox2.Text == "")
            {
                WebMessageBox.Show("请输入教师姓名"); return;
            }
            if (DropDownList1.SelectedValue == "")
            {
                WebMessageBox.Show("请选择班级"); return;
            }
           
            if (Operation.getDatatable("select teacher_name from Tx_teacher where grade_id='" + this.DropDownList1.SelectedValue + "'").Rows.Count > 0)
            {
                WebMessageBox.Show("您选择的班级已经有老师在带了，请另选班级或新建班级"); return;
            }
            
                string sql = "insert into Tx_teacher(teacher_id,teacher_name,grade_id,teacher_password) values('" +
                TextBox1.Text + "','" + TextBox2.Text + "','" + this.DropDownList1.SelectedValue + "','" + TextBox4.Text + "')";
            Operation.runSql(sql);
            WebMessageBox.Show("添加完成", "teachermanage.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("teachermanage.aspx");
        }
    }
}