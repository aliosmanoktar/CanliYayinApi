using CanliYayinApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.Http;

namespace CanliYayinApi.Controllers
{
    public class MedyaYayinController : ApiController
    {
        [HttpPost]
        public IHttpActionResult  AddMedya([FromBody] InsertMedya medyas)
        {
            string sorgu =string.Format("select Id from YayinUID where Uid='{0}' and KullaniciId=(select Id from Kullanici where sifre='{1}' and Name='{2}')"
                ,medyas.Uid,medyas.Sifre,medyas.kullaniciAdi);
            Debug.WriteLine(sorgu);
            SqlCommand command = new SqlCommand(sorgu, new SqlConnection(ConfigurationManager.ConnectionStrings["baglanti"].ToString()));
            command.Connection.Open();
            object o = command.ExecuteScalar();
            if (o == null)
                return InternalServerError();
            int UId = (int)o;
            foreach(Medyas item in medyas.medyalar)
            {
                sorgu = string.Format("insert into MedyaYayin(Name,UidId,Link,YayinType) values('{0}',{1},'{2}',(select Id from SosyalMedya where name='{3}'))",item.YayinName,UId,item.Link,item.Type);
                command.CommandText = sorgu;
                command.ExecuteNonQuery();
            }
            return Ok();
        }

        public IEnumerable<MedyaYayinInfo> Get(string YayinName)
        {
            List<MedyaYayinInfo> medya = new List<MedyaYayinInfo>();
            string sorgu = "select * from MedyaYayin where UidId= (select Id from YayinUID where Uid='{0}') and Sil ='false' ";
            sorgu = string.Format(sorgu, YayinName);
            SqlCommand command = new SqlCommand(sorgu, new SqlConnection(ConfigurationManager.ConnectionStrings["baglanti"].ToString()));
            command.Connection.Open();
            SqlDataReader rd = command.ExecuteReader();
            while (rd.Read())
            {
                medya.Add(new MedyaYayinInfo()
                {
                    Link = rd["Link"].ToString(),
                    YayinName = YayinName
                });
            }
            rd.Close();
            sorgu = string.Format("update MedyaYayin set sil='True' where UidId=(select Id from YayinUID where Uid ='{0}') and not YayinType=(Select Id from SosyalMedya where name='Default') ", YayinName);
            command.CommandText = sorgu;
            command.ExecuteNonQuery();
            return medya;
        }
        //public static final String AddYayin="http://192.168.1.106/api/MedyaYayin?url=%s&uid=%s&name=%s";
        /*[HttpGet]
        public int Add(string url, string uid, string name = "Yayin")
        {

            string sorgu = string.Format("insert into MedyaYayin(Name,UidId,Link) values('{0}',(select Id from YayinUID where Uid='{1}'),'{2}'); select SCOPE_IDENTITY();", name, uid, url);
            SqlCommand command = new SqlCommand(sorgu, new SqlConnection(ConfigurationManager.ConnectionStrings["baglanti"].ToString()));
            command.Connection.Open();
            try {
                int id = int.Parse(command.ExecuteScalar().ToString());
                return id;
            }catch(Exception) {

                return -1;
            }
        }*/
        //public static final String AddYayin="http://192.168.1.106/api/MedyaYayin?id=%d&name=%s&sifre=%s&uid=%s";
        [HttpDelete]
        public int Delete(int id,string name,string sifre,string uid)
        {
            string sorgu = string.Format("update MedyaYayin set Sil='True' where Id={0} and UidId =(select Id from YayinUID where Uid='{1}' and KullaniciId = (select Id from Kullanici where Name = '{2}' and Sifre='{3}'))", id,uid,name,sifre);
            SqlCommand command = new SqlCommand(sorgu, new SqlConnection(ConfigurationManager.ConnectionStrings["baglanti"].ToString()));
            command.Connection.Open();
            try
            {
                int i =command.ExecuteNonQuery();
                if (i > 0)
                    return 200;
                else return -1;
            }
            catch (Exception)
            {
           
                return -1;
            }
        }
        //public static final String AddYayin="http://192.168.1.106/api/MedyaYayin?id=%d&Kname=%s&sifre=%s&uid=%s&name=%s&link=%s";

        /*public int Update(int id, string Kname, string sifre, string uid,string name,string link )
        {
            string sorgu = string.Format("update MedyaYayin set Name='{0}',Link='{1}' where Id={2} and UidId =(select Id from YayinUID where Uid='{3}' and KullaniciId = (select Id from Kullanici where Name = '{4}' and Sifre='{5}'))",name,link, id, uid, Kname, sifre);
            SqlCommand command = new SqlCommand(sorgu, new SqlConnection(ConfigurationManager.ConnectionStrings["baglanti"].ToString()));
            command.Connection.Open();
            /* try
             {
                 command.ExecuteNonQuery();
                 return 200;
             }
             catch (Exception)
             {

                 return -1;
             }
            try
            {
                int i = command.ExecuteNonQuery();
                if (i > 0)
                    return 200;
                else return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 200;
        }*/
    }
}