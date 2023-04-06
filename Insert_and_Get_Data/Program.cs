using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace insert_and_get_data
{
    class Program
    {
        static void Main(string[] args)
        {
            string jwb;
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi ke Database\n");
                    Console.WriteLine("Masukan User ID : ");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukan password : ");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukan database tujuan : ");
                    string db = Console.ReadLine();
                    Console.WriteLine("\nketik k ntuk terhubung ke database : ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'k':
                            {
                                SqlConnection conn = null;
                                string strkoneksi = "data source = LAPTOP-1MAR30K4; " +
                                    "initial catalog = {0}; " +
                                    "user ID = {1}; password = {2}";
                                conn = new SqlConnection(string.Format(strkoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh Data");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Keluar");
                                        Console.WriteLine("4. Hapus Data");
                                        Console.Write("Enter your choice (1-4); ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Data Murid");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Input Data Murid");
                                                    Console.WriteLine("Masukan NIS: ");
                                                    string NIS = Console.ReadLine();

                                                    Console.WriteLine("Masukan Nama Murid: ");
                                                    string NmaMrd = Console.ReadLine();
                                                    Console.WriteLine("Masukan Kelas: ");
                                                    string kl = Console.ReadLine();
                                                    Console.WriteLine("Masukan Jalan: ");
                                                    string jl = Console.ReadLine();
                                                    Console.WriteLine("Masukan Kecamatan: ");
                                                    string kcmtn = Console.ReadLine();
                                                    Console.WriteLine("Masukan Kota: ");
                                                    string kta = Console.ReadLine();

                                                    pr.insert(NIS, NmaMrd, jl, kcmtn, kta, kl, conn);


                                                }
                                                break;
                                            case '3': 
                                                conn.Close();
                                                return;
                                            case '4':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Input Data Mahasiswa");
                                                    Console.WriteLine("Masukan NIM: ");
                                                    string NIM = Console.ReadLine();
                                                    Console.WriteLine("Apakah anda yakin ingin menghapus NIM ini?(y)");
                                                    jwb = Console.ReadLine();

                                                    if (jwb.Equals("y") || jwb.Equals("Y"))
                                                    {
                                                        try
                                                        {
                                                            pr.delete(NIM, conn);
                                                        }
                                                        catch
                                                        {
                                                            Console.WriteLine("Anda tidak memiliki " +
                                                                "akses untuk menghapus data");
                                                        }
                                                    }
                                                    else break;
                                                }break;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Invalid Option");
                                                }break;
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Check for the value entered");
                                    }
                                }
                            }
                        default:
                            {
                                Console.WriteLine("invalid Option");
                            }break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak Dapat Mengakses Database Menggunakan User Tersebut");
                    Console.ResetColor();
                }
            }
        }
        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select * From Murid", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
            r.Close();
        }
        public void insert(string NIS, string NmaMrd, string jl, string kcmtn, string kta, string kl, SqlConnection con)
        {
            string str = "";
            str = "Insert into Murid (NIS, Nama, Kelas, Jalan, Kecamatan, Kota)" +
                "values(@nis,@nma,@kl, @Jl,@Kcmtn, @Kta)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("nis", NIS)); 
            cmd.Parameters.Add(new SqlParameter("nma", NmaMrd));
            cmd.Parameters.Add(new SqlParameter("kl", kl));
            cmd.Parameters.Add(new SqlParameter("Jl", jl)); 
            cmd.Parameters.Add(new SqlParameter("Kcmtn", kcmtn));
            cmd.Parameters.Add(new SqlParameter("Kta", kta));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambah");
        }
        public void delete(string NIM, SqlConnection con)
        {
            string str = "";
            str = "delete from HRD.MAHASISWA where NIM = @nim";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("nim", NIM));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Dihapus");
        }
        public string searchdata(string NIM, SqlConnection con)
        {
            string str = "";
            string nim = "";
            str = "select * from HRD.MAHASISWA where NIM = @nim ";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("nim", NIM));
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                nim = r.GetValue(0).ToString();
                Console.WriteLine();
            }
            r.Close();
            return nim;
        }
    }
}