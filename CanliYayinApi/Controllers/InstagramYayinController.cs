using CanliYayinApi.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.Http;

namespace CanliYayinApi.Controllers
{
    public class InstagramYayinController : ApiController
    {
        [HttpPost]
        public int YayinBaslat([FromBody] InstagramYayinInfo yayin)
        {
            string sorgu = string.Format("insert into InstagramYayin(YayinAdres,BroadcastID,YayinUID,Sil)" +
                "values('{0}','{1}',(select Id from YayinUID where Uid='{2}'),'False')",yayin.url,yayin.BroadCastid,yayin.YayinUid);
            SqlCommand command = new SqlCommand(sorgu, new SqlConnection(ConfigurationManager.ConnectionStrings["baglanti"].ToString()));
            command.Connection.Open();
            try
            {
                return command.ExecuteNonQuery();
            }
            catch
            {
                return -1;
            }
            
        }

        [HttpDelete]
        public void YayinDurdur(string yayinAdres)
        {
            string sorgu = string.Format("update InstagramYayin set sil='True' where YayinUID=(select Id from YayinUID where Uid ='{0}')", yayinAdres);
            SqlCommand command = new SqlCommand(sorgu, new SqlConnection(ConfigurationManager.ConnectionStrings["baglanti"].ToString()));
            command.Connection.Open();
            command.ExecuteNonQuery();
        }
    }
}