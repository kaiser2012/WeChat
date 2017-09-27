using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using WeChat.Common;
using WeChat.WXModel;

namespace WeChat
{
    /// <summary>
    /// index 的摘要说明
    /// </summary>
    public class index : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (TokenModel.token == null)
            {
                LogHelper.Write("222");
                TokenModel.getAccesss_Token();
            }
            LogHelper.Write("-----------------------------------------------------------------------");
            createMenu();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        private void createMenu()
        {
            LogHelper.Write("54321");
            string data = getMenu();
            LogHelper.Write(data);
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", TokenModel.token.access_token);
            WeChatHelper.loadUrl(url, data);
           
        }
        /// <summary>
        /// 获取菜单数据
        /// </summary>
        /// <returns></returns>
        private string getMenu()
        {
            try
            {
                LogHelper.Write("path:" + System.Web.HttpContext.Current.Server.MapPath(".") + "\\file\\menu.txt");
                FileStream fs1 = new FileStream(System.Web.HttpContext.Current.Server.MapPath(".") + "\\file\\menu.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs1, Encoding.GetEncoding("GBK"));
                string menu = sr.ReadToEnd();
                sr.Close();
                fs1.Close();
                return menu;
            }
            catch(Exception e)
            {
                LogHelper.Write("getMenu异常：" + e.Message);
                return "";
            }
           
        }
    }
}