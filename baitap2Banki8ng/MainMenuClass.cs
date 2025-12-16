using static System.Console;
namespace baitap2Banking
{
    public class MainMenuClass ()
    {

        public static void TitleMenu()
        {
            Clear();
            WriteLine(new string('=', 40));
            WriteLine("\tNgan Hang TEXT day nay");
            WriteLine(new string('=', 40));
        }
        public static void MenuFunc()
        {
            WriteLine("");
            WriteLine("1. Dang ki tai khoan");
            WriteLine("2. Kiem tra tai khoan");
            WriteLine("3. Nap tien");
            WriteLine("4. Rut Tien");
            WriteLine("5. Chuyen Tien");
            WriteLine("6. Thoat");
        }
        public static void Main(string[] args)
        {
            bool exitProgram = false;

            while (!exitProgram)
            {
                TitleMenu();
                MenuFunc();
                
                Write("\nChon chuc nang: ");
                int selecttion;
                
                if (!int.TryParse(ReadLine(), out selecttion))
                {
                    WriteLine("Vui long nhap so hop le!");
                    ReadKey();
                    continue;
                }

                Logger.LogAction($"User selected option: {selecttion}");

                switch(selecttion)
                {
                    case 1: regAcc();        break;
                    case 2: gdKiemtratk();   break;
                    case 3: gdNaptien();     break;
                    case 4: gdRuttien();     break;
                    case 5: gdChuyentien();  break;
                    case 6: 
                        WriteLine("Cam on ban da su dung dich vu! Hen gap lai!");
                        exitProgram = true;
                        break;
                    default:
                        WriteLine("Chuc nang khong hop le! Vui long chon tu 1-6.");
                        ReadKey();
                        break;
                }
            }
        }

        private static void regAcc()
        {
            Logger.LogAction("User started account registration.");
            UserDB objnewUser = new UserDB(); //Tao obj moi
           
            int inputValue;
            string inputText;
            Clear();
            TitleMenu();

            Write("Nhap ma ID CCCD cua ban: ");
            inputValue = Convert.ToInt32(ReadLine());
            objnewUser.ID = inputValue;

            Write("Nhap ho va ten cua ban: ");
            inputText = ReadLine() ?? "";
            objnewUser.Name = inputText;

            Write("Nhap email cua ban: ");
            inputText = ReadLine() ?? "";
            objnewUser.Email = inputText;

            Write("Nhap SDT cua ban: ");
            inputText = ReadLine() ?? "";
            objnewUser.PhoneNumber = inputText;

            objnewUser.sodutaikhoan = 0;

            tkNganhang.tkNganghangs.Add(objnewUser);

            WriteLine($"Dang ki thanh cong !!!, \n Chao mung {objnewUser.Name}!");

            ReadKey();
        }

        private static void gdKiemtratk()
        {
            Logger.LogAction("User checked accounts.");
            Clear();
            TitleMenu();
            WriteLine(new string('=', 70));
            foreach (var taikhoan in tkNganhang.tkNganghangs)
            {
                WriteLine($"ID:{taikhoan.ID,-10}|{taikhoan.Name,-20}|{taikhoan.Email,-20}|{taikhoan.PhoneNumber,-20}|{taikhoan.sodutaikhoan,-20}");
            }
            ReadKey();
        }

        private static void gdNaptien()
        {
            Logger.LogAction("User started depositing money.");
            double tiengiaodich;
            string inputValue;
            bool isNumber;
            int userID;

            Clear();
            TitleMenu();
            // Tim user theo ID
            Write("Nhap ID tai khoan cua ban: ");
            userID = Convert.ToInt32(ReadLine());

            UserDB? user = tkNganhang.tkNganghangs.FirstOrDefault(u => u.ID == userID);

            if (user == null)
            {
                WriteLine("Khong tim thay tai khoan voi ID nay!");
                ReadKey();
                return;
            }

            while (true)
            {
                Write("Nhap so tien can nap: ");
                inputValue = string.Concat(ReadLine());

                if (string.IsNullOrEmpty(inputValue))
                {
                    WriteLine("Ban chua nhap so tien, Vui long nhap lai");
                    continue;
                }

                isNumber = double.TryParse(inputValue, out tiengiaodich);

                if (isNumber == false)
                {
                    WriteLine("so tien giao dich phai la so! Enter de thu lai");
                    ReadKey();
                    continue;
                }

                if (tiengiaodich <= 0)
                {
                    WriteLine("so tien can > 0");
                    continue ;
                }
                if (tiengiaodich % 10000 != 0)
                {
                    WriteLine("So tien phai la boi cua 10.000vnd");
                    continue;
                }
                break;
            }

            // Cap nhat so du tai khoan
            user.sodutaikhoan = Calculator.Naptien(tiengiaodich, user.sodutaikhoan);

            WriteLine($"Nap thanh cong {tiengiaodich} vao tai khoan");
            WriteLine($"So du hien tai: {user.sodutaikhoan} VND");

            ReadKey();
        }
        public static void gdRuttien()
        {
            Logger.LogAction("User started withdrawing money.");
            double tiengiaodich;
            string inputValue;
            int userID;

            Clear();
            TitleMenu();

            // Tim user theo ID
            Write("Nhap ID tai khoan cua ban: ");
            userID = Convert.ToInt32(ReadLine());

            UserDB? user = tkNganhang.tkNganghangs.FirstOrDefault(u => u.ID == userID);

            if (user == null)
            {
                WriteLine("Khong tim thay tai khoan voi ID nay!");
                ReadKey();
                return;
            }

            while (true)
            {
                Write("Nhap so tien can rut: ");
                inputValue = ReadLine() ?? "";

                if (string.IsNullOrEmpty(inputValue))
                {
                    WriteLine("Ban chua nhap so tien, Vui long nhap lai");
                    continue;
                }

                if (!double.TryParse(inputValue, out tiengiaodich))
                {
                    WriteLine("So tien giao dich phai la so! Enter de thu lai");
                    ReadKey();
                    continue;
                }

                if (tiengiaodich <= 0)
                {
                    WriteLine("So tien phai lon hon 0");
                    continue;
                }

                if (tiengiaodich % 10000 != 0)
                {
                    WriteLine("So tien phai la boi cua 10.000vnd");
                    continue;
                }

                if (tiengiaodich > user.sodutaikhoan)
                {
                    WriteLine($"So du khong du! So du hien tai: {user.sodutaikhoan} VND");
                    continue;
                }

                break;
            }

            // Cap nhat so du tai khoan (su dung Chuyentien de rut tien)
            user.sodutaikhoan = Calculator.Chuyentien(tiengiaodich, user.sodutaikhoan);

            WriteLine($"Rut thanh cong {tiengiaodich} VND");
            WriteLine($"So du hien tai: {user.sodutaikhoan} VND");

            ReadKey();
        }
        private static void gdChuyentien()
        {
            Logger.LogAction("User started transferring money.");
            double tienGiaodich;
            string inputValue;
            int tkChuyen;
            int tkNhan;


            Clear();
            TitleMenu();

            // Tim tai khoan nguoi gui
            Write("Nhap ID tai khoan nguoi gui: ");
            tkChuyen = Convert.ToInt32(ReadLine());

            UserDB? userChuyen = tkNganhang.tkNganghangs.FirstOrDefault(u => u.ID == tkChuyen);

            if (userChuyen == null)
            {
                WriteLine("Khong tim thay tai khoan nguoi gui!");
                ReadKey();
                return;
            }

            // Tim tai khoan nguoi nhan
            Write("Nhap ID tai khoan nguoi nhan: ");
            tkNhan = Convert.ToInt32(ReadLine());

            UserDB? userNhan = tkNganhang.tkNganghangs.FirstOrDefault(u => u.ID == tkNhan);

            if (userNhan == null)
            {
                WriteLine("Khong tim thay tai khoan nguoi nhan!");
                ReadKey();
                return;
            }

            if (tkChuyen == tkNhan)
            {
                WriteLine("Khong the chuyen tien cho chinh minh!");
                ReadKey();
                return;
            }

            while (true)
            {
                Write("Nhap so tien can chuyen: ");
                inputValue = ReadLine() ?? "";

                if (string.IsNullOrEmpty(inputValue))
                {
                    WriteLine("Ban chua nhap so tien, Vui long nhap lai");
                    continue;
                }

                if (!double.TryParse(inputValue, out tienGiaodich))
                {
                    WriteLine("So tien giao dich phai la so! Enter de thu lai");
                    ReadKey();
                    continue;
                }

                if (tienGiaodich <= 0)
                {
                    WriteLine("So tien phai lon hon 0");
                    continue;
                }

                if (tienGiaodich % 10000 != 0)
                {
                    WriteLine("So tien phai la boi cua 10.000vnd");
                    continue;
                }

                if (tienGiaodich > userChuyen.sodutaikhoan)
                {
                    WriteLine($"So du khong du! So du hien tai: {userChuyen.sodutaikhoan} VND");
                    continue;
                }

                break;
            }

            //Nguoi chuyen thi lam giao dich tru tien
            userChuyen.sodutaikhoan = Calculator.Chuyentien(tienGiaodich, userChuyen.sodutaikhoan);
            
            // Nguoi nhan thi lam giao dich tang tien
            userNhan.sodutaikhoan = Calculator.Naptien(tienGiaodich, userNhan.sodutaikhoan);

            WriteLine($"Chuyen thanh cong {tienGiaodich} VND den tai khoan {userNhan.Name}");

            WriteLine($"So du hien tai cua ban: {userChuyen.sodutaikhoan} VND");

            ReadKey();

        }
    }
}
