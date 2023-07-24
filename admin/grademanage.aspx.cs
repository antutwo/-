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
    public partial class grademanage : System.Web.UI.Page
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
            string sqlstr = "select * from Tx_grade where (grade_name like '%" + this.findinfo.Text + "%' OR LEN('" + this.findinfo.Text + "')=0) order by grade_id";
            GridView1.DataSource = Operation.getDatatable(sqlstr);
            GridView1.DataKeyNames = new string[] { "grade_id" };//设置主键
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
                    string sqlstr = "delete from Tx_grade where grade_id='" + GridView1.DataKeys[i].Value.ToString() + "'";
                    //GridView1.DataKeys[e.RowIndex].Value.ToString() 获取当前行的主键值
                    Operation.runSql(sqlstr);
                }
            }
           /* string sqlstr = "delete from Tx_grade where grade_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'";
            //GridView1.DataKeys[e.RowIndex].Value.ToString() 获取当前行的主键值
            Operation.runSql(sqlstr);*/
            bind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            /* (GridView1.Rows[e.RowIndex].Cells[1].Controls[0])  获取GridView控件的第 e.RowIndex+1行的第二列单元格内的第一个控件
           e.RowIndex 是指当前鼠标选中行的序号，+1是因为数组的下标从0开始，0表示为1，1表示为2
          然后在使用（TextBox）强制类型转换 将获取到控件强转为TextBox控件，在获取他的Text值，转成string类型的值，Trim()再去掉文本开头和结尾的空格*/
            if (((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim() == "")
            {
                WebMessageBox.Show("请输班名"); return;
            }
            /*string gid = GridView1.DataKeys[e.RowIndex].Value.ToString().Trim();//取主键*/
            string gname = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
            //判断是否已经存在这个班级名
            if (Operation.getDatatable("select * from Tx_grade where grade_name='" + gname + "' ").Rows.Count <0)//不存在，设置班级名
            {

                Operation.runSql("update Tx_grade set grade_name='" + ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim() +
                    "' where grade_id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'");
            }
            else
            {
                WebMessageBox.Show("您所更改的班级名已经存在"); return;
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
            Response.Redirect("gradeadd.aspx");
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
                    string  gid, gname;
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
                        gid = row.GetCell(0).ToString().Trim();
                        gname = row.GetCell(1).ToString().Trim();
                            string sql1 = "select * from Tx_grade where grade_id='" + gid + "' ";
                            string sql2 = "update Tx_grade set grade_name='"+gname+"' where grade_id='"+gid+"'";//修改
                            string sql3 = "insert into Tx_grade(grade_id,grade_name) values('" +gid + "','" + gname + "')";
                            if (Operation.getDatatable(sql1).Rows.Count > 0)//返回一条记录，说明有这个班级  -- 修改
                            {
                                Operation.runSql(sql2);
                            }
                            else //没有则进行插入
                            {
                                Operation.runSql(sql3);
                            }
                        count++;
                    }
                    WebMessageBox.Show("导入完成，成功导入数据记录共" + count + "条", "grademanage.aspx");
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

        protected void Button2_Command(object sender, CommandEventArgs e)
        {

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

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            /* 取触发了编辑的那行的索引值赋值GridView1.EditIndex*/
            bind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标经过时，行背景色变
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#E6F5FA'");
                //鼠标移出时，行背景色变
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");

/*
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    ((LinkButton)e.Row.Cells[4].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除：\"" + e.Row.Cells[1].Text + "\"吗?')");
                }*/

            }

        }

        protected void findinfo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}