using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Urunsatis
{
    
    class Program
    {
        public static string veribag = "data source=.;initial catalog=veri;integrated security=true";
        static void Main(string[] args)
        {
            urun_getir();
            Console.ReadKey();
        }

        static void urun_getir()
        {          
            try
            {
                SqlConnection baglanti = new SqlConnection(veribag);
                SqlCommand komut = new SqlCommand("Select * from urunler", baglanti);
                baglanti.Open();
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    Console.WriteLine("Urun No : " + oku["id"].ToString() + "\nUrun Adı : " + oku["urun_adi"].ToString() + "\nUrun Kategori : " + oku["urun_kategori"].ToString()+"\nUrun Fiyat : "+oku["urun_fiyat"].ToString());
                    if (oku["urun_durum"].ToString() == "satildi")
                    {
                        Console.WriteLine("Durum : Satıldı"+"\nSatılan Kişi : "+oku["satilan_kisi"].ToString());

                    }
                    else
                        Console.WriteLine("Durum : Aktif - Henüz Satılmadı");
                }
                oku.Close();
                baglanti.Close();               
            }
            catch
            {
                Console.WriteLine("Veri Girilemedi Bağlantıda bir Problem olabilir.");
            }
        }




    }
}
