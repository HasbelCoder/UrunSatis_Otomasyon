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
        public static SqlConnection baglanti = new SqlConnection(veribag);

        static void Main(string[] args)
        {
            urun_ekle();
            Console.ReadKey();
        }

        static void urun_getir()
        {          
            try
            {
                
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

        static void urun_ekle()
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into urunler(urun_adi,urun_kategori,urun_fiyat,satilan_kisi,urun_durum)values(@urun_adi,@urun_kategori,@urun_fiyat,@satilan_kisi,@urun_durum)", baglanti);
                komut.Parameters.AddWithValue("@urun_adi", Console.ReadLine());
                komut.Parameters.AddWithValue("@urun_kategori", Console.ReadLine());
                komut.Parameters.AddWithValue("@urun_fiyat", Console.ReadLine());
                komut.Parameters.AddWithValue("@satilan_kisi", Console.ReadLine());
                Console.Write("Urun Durumu(satildi/satilmadi yazınız) : ");
                string durum = Console.ReadLine();
                if (durum == "satildi" || durum == "satilmadi" || durum=="")
                    komut.Parameters.AddWithValue("@urun_durum", durum);
                else
                    Console.WriteLine("Kaydedilemedi Yanlış İşlem Yaptınız..!");
                
                komut.ExecuteNonQuery();
                baglanti.Close();
                Console.WriteLine("---KAYIT BAŞARIYLA YAPILDI...!!!---");
            }
            catch
            {
                Console.WriteLine("Veri Girilemedi Bağlantıda bir Problem olabilir.");
            }

                
            
        }


    }
}
