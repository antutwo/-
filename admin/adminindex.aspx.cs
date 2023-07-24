using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tuixuan.util;

namespace tuixuan.admin
{
    public partial class index1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)//如果是首次加载
            {
                if (Session["admin_id"] == null)
                {
                    WebMessageBox.Show("请登录", "../Login/adminLogin.aspx");
                }
                string sql = "select * from Tx_admin where user_id='" + Session["admin_id"].ToString() + "'";
                DataTable dt = Operation.getDatatable(sql);
                if (dt.Rows.Count > 0)
                {
                    lblname.Text = dt.Rows[0]["user_name"].ToString();
                    lblpwd.Text = dt.Rows[0]["user_password"].ToString();    
                    lblid.Text=dt.Rows[0]["user_id"].ToString();
                    /*lblid.Text=Session["admin_id"].ToString();*/
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("../admin/adminupdate.aspx");
        }
    }
}