using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using DataAnalysis.Framework.Common.Json;
using DataAnalysis.Framework.File;
using DataAnalysis.Framework.Logging;

namespace DataAnalysis.Common
{
    /// <summary>
    /// uploadFile 的摘要说明
    /// </summary>
    public class uploadFile : IHttpHandler
    {
        string filepath = System.Configuration.ConfigurationManager.AppSettings["orderfilepaht"].ToString();
        string fileBaseUrl = System.Configuration.ConfigurationManager.AppSettings["orderfilebaseurl"].ToString();
        public void ProcessRequest(HttpContext context)
        {
            JsonResult result = new JsonResult();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            HttpPostedFile postedFile = context.Request.Files[0];

            result.DataSource = SavePostedFile(postedFile);

            JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            context.Response.ContentType = "text/plain";
            context.Response.Write(oSerializer.Serialize(result));
        }

        private FileUpload SavePostedFile(HttpPostedFile postedFile)
        {
            string sourceFile = Path.GetFileName(postedFile.FileName);
            string strExtension = Path.GetExtension(postedFile.FileName).ToLower();
            string targetFileName = string.Format("{0:hhmmss-ddMMyyyy}", DateTime.Now) + "-" + sourceFile;
            string nowdir = DateTime.Now.ToString("yyyyMMdd");
            if (!Directory.Exists(filepath + nowdir))
            {
                Directory.CreateDirectory(filepath + "/" + nowdir);
            }
            string targetFile = filepath + "/" + nowdir + "/" + targetFileName;
            LoggerManager.WebApplicationLogger.InfoFormat("The file {0} is being saved as file {1}", sourceFile, targetFile);
            //Save the file.
            postedFile.SaveAs(targetFile);
            LoggerManager.WebApplicationLogger.InfoFormat("The file {0} is saved as file {1}", sourceFile, targetFile);
            FileUpload upload = new FileUpload();
            upload.SourceFile = sourceFile;
            upload.SourceFileName = sourceFile;
            upload.TargetFile = targetFileName;
            upload.TargetFileName = string.Format("{0}/{1}/{2}", fileBaseUrl, nowdir, targetFileName);
            return upload;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}