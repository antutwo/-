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
    public partial class addCandidate : System.Web.UI.Page
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
                    /* Label1.Text = "欢迎您," + dt.Rows[0]["teacher_name"] + "老师";*/
                    Label1.Text = "欢迎您登录学生推选管理系统，" + dt.Rows[0]["teacher_name"] + "老师";

                }
                TextBox3.Text = Session["gid"].ToString();

                ///
                string sql1 = "select position_name from Tx_Gposition where grade_id='"+ Session["gid"] + "' and state='正在候选'";//查找所有职位   ---绑定dropdownlist
                DropDownList1.DataSource = Operation.getDatatable(sql1);
                DropDownList1.DataTextField = "position_name";
                DropDownList1.DataValueField = "position_name";
                DropDownList1.DataBind();
                this.DropDownList1.Items.Insert(0, new ListItem("请选择", "0"));


               /* if (Request.QueryString["flag"] != null && Request.QueryString["flag"] == "true")
                {*/
                    /* flag = Request.QueryString["flag"];*/

                    string position = Request.QueryString["position"].ToString();
                    DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(position));

                    position = DropDownList1.SelectedValue.ToString();
                    string sql4 = " select stu_id,stu_name from Tx_student where stu_id not in(select stu_id from Tx_candidate where position='" + position + "') " +
                    "and stu_id not in(select stu_id from Tx_elect where position='" + position + "') and grade_id='" + TextBox3.Text + "' ";
                    DropDownList3.DataSource = Operation.getDatatable(sql4);
                    DropDownList3.DataTextField = "stu_id";
                    DropDownList3.DataValueField = "stu_id";
                    DropDownList3.DataBind();
                    string sql2 = "select stu_name from Tx_student where stu_id='" + DropDownList3.SelectedValue.ToString() + "'";

                    dt = Operation.getDatatable(sql2);
                    if (dt.Rows.Count > 0)
                    {
                        TextBox1.Text = dt.Rows[0]["stu_name"].ToString();
                    }
                  

                /*}*/
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string position = DropDownList1.SelectedValue.ToString();
            string sid = DropDownList3.SelectedValue.ToString();
            string sname = TextBox1.Text;
            string gid = TextBox3.Text;
            string sql2 = "insert into Tx_candidate(position,stu_id,candidate_name,grade_id) values" +
                "('" + position + "','" + sid + "','" + sname + "','" + gid + "')";
            Operation.runSql(sql2);

            WebMessageBox.Show("插入成功");

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("candidateManage.aspx");
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string position = DropDownList1.SelectedValue.ToString();
            string sql = " select stu_id,stu_name from Tx_student where stu_id not in(select stu_id from Tx_elect where position='" + position + "') " +
                "and stu_id not in(select stu_id from Tx_elect where position='" + position + "') and grade_id='" + TextBox3.Text + "' ";
            DropDownList3.DataSource = Operation.getDatatable(sql);
            DropDownList3.DataTextField = "stu_id";
            DropDownList3.DataValueField = "stu_id";
            DropDownList3.DataBind();

            TextBox1.Text = " ";

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