using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppServiceHtml2Pdf.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult index()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]  // 這邊是為了範例方便，正式環境不建議這樣使用，導致所有欄位都可以讀 Html
        public ActionResult PDF(string Html)
        {
            if (string.IsNullOrWhiteSpace(Html))
            {
                Html = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/PDF.html"));
                // 取代 {BasePath} 成實體路徑
                Html = Html.Replace("{BasePath}", AppDomain.CurrentDomain.BaseDirectory);
            }

            var pdf = PdfHelper.ConvertHtmlTextToPDF(Html);

            return File(pdf, "application/pdf");
        }
    }
}