using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CanliYayinApi.Models
{
    public class InsertMedya
    {
        //int id, string Kname, string sifre, string uid,string name,string link 
        public string kullaniciAdi { set; get; }
        public string Sifre { set; get; }
        public string Uid { set; get; }
        public List<Medyas> medyalar { set; get; }
    }
    public class Medyas
    {
        public string YayinName { set; get; }
        public string Link { set; get; }
        public string Type { set; get; }
    }
}