using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAnalysis.Framework.Common.Json;
namespace DataAnalysis
{
    public partial class user_info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static JsonResult GetUserState()
        {
            JsonResult jr = new JsonResult();
            jr.ResultMessage = "success";
            return jr;
        }


    }
}