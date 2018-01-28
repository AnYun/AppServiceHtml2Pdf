using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TuesPechkin;

namespace AppServiceHtml2Pdf
{
    /// <summary>
    /// Pdf 轉換工具
    /// </summary>
    public static class PdfHelper
    {
        /* 多執行續類型的程式需使用 ThreadSafeConverter 並且將 Converter 放置在 Static */
        private static IConverter converter =
            new ThreadSafeConverter(
                new RemotingToolset<PdfToolset>(
                    new WinAnyCPUEmbeddedDeployment(
                        new TempFolderDeployment())));

        /// <summary>
        /// 將 Html 文字 輸出到 PDF 檔裡
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public static byte[] ConvertHtmlTextToPDF(string htmlText)
        {
            if (string.IsNullOrEmpty(htmlText))
            {
                return null;
            }

            var document = new HtmlToPdfDocument
            {
                GlobalSettings = { },
                Objects = {
                    new ObjectSettings {
                            HtmlText = htmlText
                    }
                 }
            };

            var result = converter.Convert(document);
            return result;
        }
    }
}