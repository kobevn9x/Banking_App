using static System.Console;
namespace baitap2Banking
{
    public class tkNganhang()
    {
        public static List<UserDB> tkNganghangs = new();
    }
    public class Calculator
    {

        public static double Naptien(double tiengiaodich, double sodutaikhoan)
        {
            if (tiengiaodich < 0)
            {
                WriteLine("so tien phai lon hon 0");
            }
            sodutaikhoan += tiengiaodich;
            return sodutaikhoan;
        }
        public static double Chuyentien(double tienGiaodich, double sodutaikhoan)
        {
            if (tienGiaodich < 0)
            {
                WriteLine("so tien phai lon hon 0");
            }
            if ( tienGiaodich > sodutaikhoan)
            {
                WriteLine("So tien trong tai khoan khong du");
            }
            sodutaikhoan -= tienGiaodich;
            return sodutaikhoan;
        }

    }
}
