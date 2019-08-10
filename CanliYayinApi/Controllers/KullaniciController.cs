using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;

namespace CanliYayinApi.Controllers
{
    public class KullaniciController : ApiController
    {
        SqlCommand command;
        public string Get(string name,string sifre,string telID="")
        {
            string uid = "";
            string sorgu = string.Format("select Id from kullanici where Name='{0}' and Sifre='{1}' and not ISNULL(Sil,'False')='True'", name,sifre);
            command = new SqlCommand(sorgu, new SqlConnection(ConfigurationManager.ConnectionStrings["baglanti"].ToString()));
            command.Connection.Open();
            object id = command.ExecuteScalar();
            if (null != id)
                uid = getUID(id.ToString(), telID);
            return uid;
        }
        private string createUID(string id)
        {
            return id + DateTime.Now.Ticks.ToString();
        }
        private string getUID(string kullaniciID,string telID)
        {
            string uid = "";
                
            string sorgu = string.Format("select Uid from YayinUID where KullaniciID='{0}' and TelID='{1}'", kullaniciID, telID);
            command = new SqlCommand(sorgu, new SqlConnection(ConfigurationManager.ConnectionStrings["baglanti"].ToString()));
            command.Connection.Open();
            object id = telID.Equals("")? null : command.ExecuteScalar();
            if (id != null)
                return id.ToString();
            else
            {
                uid = createUID(kullaniciID);
                AddUID(kullaniciID, uid, telID);
                return uid;
            }
        }
        private void AddUID(string id,string uid,string telID)
        {
            string sorgu = string.Format("insert into YayinUID(Uid,KullaniciId,TelID) values('{0}',{1},'{2}')",uid,id,telID);
            command.CommandText = sorgu;
            command.ExecuteNonQuery();
        }
    }
}