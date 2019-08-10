using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CanliYayinApi.Controllers
{
    public class YayinKontrolController : ApiController
    {
        public bool Get(string name,string sifre)
        {
            bool yayinDurum = false;
            string sorgu = string.Format("select case when ISNULL(YayinDurum,'True')=1 then 'True' else 'False' end as YayinDurum from kullanici where Name='{0}' and Sifre='{1}' and not ISNULL(Sil,'False')='True'", name, sifre);
            SqlCommand command = new SqlCommand(sorgu, new SqlConnection(ConfigurationManager.ConnectionStrings["baglanti"].ToString()));
            command.Connection.Open();
            object sonuc = command.ExecuteScalar();
            yayinDurum = bool.Parse(sonuc==null?"false":sonuc.ToString());
            return yayinDurum;
        }
    }
}