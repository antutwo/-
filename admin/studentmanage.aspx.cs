using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tuixuan.util;

namespace tuixuan.admin
{
    public partial class studentmanage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["admin_id"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/adminLogin.aspx");
                }
                /*排序
                 * ViewState[""] = "";*/
                string sql2 = "select * from Tx_grade";//查找所有职位   ---绑定dropdownlist
                findinfoDropdl.DataSource = Operation.getDatatable(sql2);
                findinfoDropdl.DataTextField = "grade_name";
                findinfoDropdl.DataValueField = "grade_id";
                findinfoDropdl.DataBind();

                this.findinfoDropdl.Items.Insert(0, new ListItem("全部", "0"));
                //绑定
                bind();
            }
        }
        public void bind()
        {
            string sqlstr = null;
            if (this.findinfoDropdl.SelectedValue.ToString() == "0")
            {
                sqlstr = "select * from Tx_student where (stu_name like '%" + this.findinfo.Text + "%' OR LEN('" + this.findinfo.Text + "')=0) order by stu_id";
            }
            else
            {
                sqlstr = "select * from Tx_student where (stu_name like '%" + this.findinfo.Text + "%' OR LEN('" + this.findinfo.Text + "')=0) " +
                    "and grade_id='"+ this.findinfoDropdl.SelectedValue.ToString() + "' order by stu_id";
            }
            GridView1.DataSource = Operation.getDatatable(sqlstr);
            GridView1.DataKeyNames = new string[] { "stu_id" };//设置主键
            GridView1.DataBind();
        }
        protected void chkcheckall_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                //建立模板列中CheckBox控件的引用
                CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chkCheck");
                //建立头模板中全选控件的引用
                CheckBox chkcheckall = (CheckBox)GridView1.HeaderRow.FindControl("chkcheckall");
                if (chkcheckall.Checked == true)   //如果选中全选
                {
                    chk.Checked = true;  //将每一行复选框选中
                }
                else                //否则取消每一行复选框选中
                {
                    chk.Checked = false;
                }
            }
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            bind();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chkCheck");
                if (chk.Checked == true)
                {
                    string sqlstr = "delete from Tx_student where stu_id='" + GridView1.DataKeys[i].Value.ToString() + "'";
                    //GridView1.DataKeys[e.RowIndex].Value.ToString() 获取当前行的主键值
                    Operation.runSql(sqlstr);
                }
            }
            /*string sqlstr = "delete from Tx_student where stu_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";
            //GridView1.DataKeys[e.RowIndex].Value.ToString() 获取当前行的主键值
            Operation.runSql(sqlstr);*/
            bind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            /* 取触发了编辑的那行的索引值赋值GridView1.EditIndex*/
            bind();
        }
        

        //修改
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            /* (GridView1.Rows[e.RowIndex].Cells[1].Controls[0])  获取GridView控件的第 e.RowIndex+1行的第二列单元格内的第一个控件
            e.RowIndex 是指当前鼠标选中行的序号，+1是因为数组的下标从0开始，0表示为1，1表示为2
           然后在使用（TextBox）强制类型转换 将获取到控件强转为TextBox控件，在获取他的Text值，转成string类型的值，Trim()再去掉文本开头和结尾的空格*/
            if (((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim() == "")
            {
                WebMessageBox.Show("请输入学生姓名"); return;
            }
            string gid = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();
            string sname = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
            //判断是否有所修改的班级号
            if (Operation.getDatatable("select * from Tx_grade where grade_id='" + gid + "' ").Rows.Count > 0)
            {
                ////
                string sql1 = "select * from Tx_student where stu_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";
                string name = null;
                DataTable dt = Operation.getDatatable(sql1);
                if (dt.Rows.Count > 0)
                {
                    name = dt.Rows[0]["stu_name"].ToString();///修改之前的
                }
                ////
                string sql2 = "select * from Tx_candidate where candidate_name='" + name + "'";
                if (Operation.getDatatable(sql2).Rows.Count > 0)//此人之前已在候选表
                {
                    Operation.runSql("update Tx_candidate  set candidate_name='" + sname + "' where stu_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'");
                }

                string sql3 = "select * from Tx_elect where elect_name='" + name + "'";
                if (Operation.getDatatable(sql3).Rows.Count > 0)//此人之前已在评选表
                {
                    Operation.runSql("update Tx_elect set elect_name='" + sname + "' where stu_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'");
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

                Operation.runSql("update Tx_student set stu_name='" + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim() +
                    "',stu_sex='"+ ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim() +
                    "',grade_id='" + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim() +
                    "',stu_password='" + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[5].Controls[0])).Text.ToString().Trim() +
                    "' where stu_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'");
           
            }

            else
            {
                WebMessageBox.Show("您所更改的班级不存在，请确认是否有这个班级"); return;
            }

            GridView1.EditIndex = -1;//从编辑状态改为浏览状态
            bind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            //重新绑定　
            bind();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //新增
            Response.Redirect("studentadd.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (this.FileUpload1.HasFile)//控件是否有文件上传
            {
                //导入
                if (FileUpload1.FileName.Length < 1)
                {
                    WebMessageBox.Show("请选择规范化excel文件"); return;
                }
                if (Path.GetExtension(FileUpload1.FileName).ToLower() != ".xls" && Path.GetExtension(FileUpload1.FileName).ToLower() != ".xlsx")
                {
                    WebMessageBox.Show("请选择规范化excel文件"); return;
                }
                IWorkbook workbook = null; FileStream fs = null;
                ISheet sheet = null;
                //保存的路径
                string filepath = Server.MapPath("~//upload//") + FileUpload1.FileName;  //Server.MapPath("~//upload//") +    
                if (File.Exists(filepath))
                    File.Delete(filepath);
                FileUpload1.SaveAs(filepath);

                fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);//new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (Path.GetExtension(filepath).ToLower() == ".xlsx") // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (Path.GetExtension(filepath).ToLower() == ".xls") // 2003版本
                    workbook = new HSSFWorkbook(fs);
                if (workbook == null)
                {
                    WebMessageBox.Show("导入excel文件失败"); return;
                }
                sheet = workbook.GetSheetAt(0);  // 读取sheet
                int count = 0;
                if (sheet != null)
                {
                    string sid, sname, grade_id, s_password, grade_name,s_sex;
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum; // 行数
                    for (int i = 1; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　
                        int cellCount = row.LastCellNum;
                        if (cellCount < 2) continue; //没有数据的行默认是null　
                        if (row.GetCell(0) == null) continue; //没有数据的行默认是null

                        if (row.GetCell(0).ToString().Trim() == "" || row.GetCell(1).ToString().Trim() == "") continue;
                        /*if (Operation.getDatatable("select * from Tx_teacher where teacher_id='" + row.GetCell(0).ToString().Trim() + "' or teacher_name='" + row.GetCell(1).ToString().Trim() + "'").Rows.Count > 0) continue;*/
                        sid = row.GetCell(0).ToString().Trim();
                        sname = row.GetCell(1).ToString().Trim();
                        s_sex = "";
                        grade_id = "";
                        s_password = "";
                        grade_name = "";
                        if (row.GetCell(2) !=null) s_sex = row.GetCell(2).ToString().Trim();
                        if (row.GetCell(3) != null) grade_id = row.GetCell(3).ToString().Trim();
                        if (row.GetCell(4) != null) grade_name = row.GetCell(4).ToString().Trim();
                        if (row.GetCell(5) != null) s_password = row.GetCell(5).ToString().Trim();
                        if (grade_id != null &&s_password != null && grade_name != null)
                        {
                            string sql1 = "select * from Tx_grade where grade_id='" + grade_id + "' ";
                            string sql2 = "insert into Tx_grade values('" + grade_id + "','" + grade_name + "')";
                            string sql3 = "insert into Tx_student(stu_id,stu_name,stu_sex,grade_id,stu_password) values('" +
                            sid + "','" + sname + "','"+s_sex+"','" + grade_id + "','" + s_password + "')";
                            if (Operation.getDatatable(sql1).Rows.Count > 0)//返回一条记录，说明有这个班级
                            {
                                Operation.runSql(sql3);
                            }
                            else
                            {
                                Operation.runSql(sql2);
                                Operation.runSql(sql3);
                            }
                        }
                        count++;
                    }
                    WebMessageBox.Show("导入完成，成功导入数据记录共" + count + "条", "studentmanage.aspx");
                }
                else
                {
                    WebMessageBox.Show("excel表没有数据");
                }

            }
            else
            {
                WebMessageBox.Show("您并没有上传文件");
            }
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

        protected void Button2_Command(object sender, CommandEventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            /*int i;
            //执行循环，保证每条数据都可以更新
            for (i = 0; i < GridView1.Rows.Count; i++)
            {
                //首先判断是否是数据行
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //当鼠标停留时更改背景色
                    e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#00A9FF'");
                    //当鼠标移开时还原背景色
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
                }
            }*/

            //如果是绑定数据行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标经过时，行背景色变
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#E6F5FA'");
                //鼠标移出时，行背景色变
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");

               /* if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    ((LinkButton)e.Row.Cells[7].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除：\"" + e.Row.Cells[2].Text + "\"吗?')");
                }*/
            }
            
        }

        protected void findinfoDropdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind();
        }
    }
}