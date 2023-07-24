using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tuixuan.util;

namespace tuixuan.admin
{
    public partial class teachermanage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["admin_id"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/adminLogin.aspx");
                }
                //绑定
                bind();
            }
        }

        public void bind()
        {
            string sqlstr = "select * from Tx_teacher where (teacher_name like '%" + this.findinfo.Text + "%' OR LEN('" + this.findinfo.Text + "')=0) order by teacher_id";
            GridView1.DataSource = Operation.getDatatable(sqlstr);
            GridView1.DataKeyNames = new string[] { "teacher_id" };//设置主键
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
            /*
             通过 EditIndex 判断 GridView 中的某一 Row，是否处于编辑状态。
            编辑状态中的 EditIndex >= 0 ;
            EditIndex < 0 或 EditIndex =-1 都表示 GridView 中没有正在编辑的Row
            即此语句的作用是将编辑状态改为浏览状态*/
            bind();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chkCheck");
                if (chk.Checked == true)
                {
                    string sqlstr = "delete from Tx_teacher where teacher_id='" + GridView1.DataKeys[i].Value.ToString() + "'";
                    //GridView1.DataKeys[e.RowIndex].Value.ToString() 获取当前行的主键值
                    Operation.runSql(sqlstr);
                }
            }
            bind();
            /*string sqlstr = "delete from Tx_teacher where teacher_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";
            //GridView1.DataKeys[e.RowIndex].Value.ToString() 获取当前行的主键值
            Operation.runSql(sqlstr);
            bind();*/
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            /* 取触发了编辑的那行的索引值赋值GridView1.EditIndex*/
            bind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            /* (GridView1.Rows[e.RowIndex].Cells[1].Controls[0])  获取GridView控件的第 e.RowIndex+1行的第二列单元格内的第一个控件
             e.RowIndex 是指当前鼠标选中行的序号，+1是因为数组的下标从0开始，0表示为1，1表示为2
            然后在使用（TextBox）强制类型转换 将获取到控件强转为TextBox控件，在获取他的Text值，转成string类型的值，Trim()再去掉文本开头和结尾的空格*/
            if (((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim() == "")
            {
                WebMessageBox.Show("请输入教师姓名"); return;
            }
            string gid = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
            string tname = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
            //判断是否有所修改的班级号
            if (Operation.getDatatable("select * from Tx_grade where grade_id='" + gid + "' ").Rows.Count > 0)
            {
                
                    Operation.runSql("update Tx_teacher set teacher_name='" + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim() +
                        "',grade_id='" + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim() +
                        "',teacher_password='" + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim() +
                        "' where teacher_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'");

                if (Operation.getDatatable("select teacher_name from Tx_teacher where grade_id='" + gid + "'").Rows.Count > 1)
                {
                    Response.Write("<script>alert('您刚刚修改的班级数据--"+gid+"--已经有多个教师在带，请您尽快确认')</script>");
                }

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
            Response.Redirect("teacheradd.aspx");
        }

        protected void Button2_Command(object sender, CommandEventArgs e)
        {

        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                string teachid, teachname, grade_id, teacher_password,grade_name;
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
                    teachid = row.GetCell(0).ToString().Trim(); 
                    teachname = row.GetCell(1).ToString().Trim();
                    grade_id = ""; 
                    teacher_password = "";
                    grade_name = "";
                    if (row.GetCell(2) != null) grade_id = row.GetCell(2).ToString().Trim();
                    if (row.GetCell(3) != null) teacher_password = row.GetCell(3).ToString().Trim();
                    if (row.GetCell(4) != null) grade_name = row.GetCell(4).ToString().Trim();
                    if (grade_id != null && teacher_password != null&& grade_name!=null)
                    {
                        string sql1 = "select * from Tx_grade where grade_id='"+ grade_id + "' ";
                        string sql2 = "insert into Tx_grade values('" + grade_id + "','" + grade_name + "')";
                        string sql3 = "insert into Tx_teacher(teacher_id,teacher_name,grade_id,teacher_password) values('" +
                        teachid + "','" + teachname + "','" + grade_id + "','" + teacher_password + "')";
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
                WebMessageBox.Show("导入完成，成功导入数据记录共" + count + "条", "teachermanage.aspx");
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
;        }
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标经过时，行背景色变
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#E6F5FA'");
                //鼠标移出时，行背景色变
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");

          
                
              /*  if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    ((LinkButton)e.Row.Cells[6].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除：\"" + e.Row.Cells[2].Text + "\"吗?')");
                }*/
               
            }
        }
    }
}