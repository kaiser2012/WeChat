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
                TokenModel.getAccesss_Token();
            }
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
            string data = getMenu();
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", TokenHelper.token.access_token);
            WeChatHelper.loadUrl(url, data);
            LogHelper.Write(TokenModel.token.access_token);
        }
        /// <summary>
        /// 获取菜单数据
        /// </summary>
        /// <returns></returns>
        private string getMenu()
        {
            FileStream fs1 = new FileStream(System.Web.HttpContext.Current.Server.MapPath(".") + "\\file\\menu.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs1, Encoding.GetEncoding("GBK"));
            string menu = sr.ReadToEnd();
            sr.Close();
            fs1.Close();
            return menu;
        }
    }
}