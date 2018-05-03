using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace DETAITHUCTAP
{
    /// <summary>
    /// Interaction logic for Dangky.xaml
    /// </summary>
    public partial class Dangky : Window
    {
        public Dangky()
        {
            InitializeComponent();
       
        }
       bool KiemTraHopLe()
        {

            //kiểm tra kí tự nhập vào TextBox Tên đăng nhập nếu đúng thì mới thực hiện đăng ký
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (!regexItem.IsMatch(txbTenDangNhapDK.Text))
            {
                MessageBox.Show("Tên đăng nhập chỉ cho phép kí tự a-z và 0-9 .Bạn vui lòng nhập lại", "Thông báo");

                return true;
            }
            if (string.IsNullOrWhiteSpace(txbTenDangNhapDK.Text))
            {
                MessageBox.Show("Tài khoản không được bỏ trống hoặc có khoảng cách", "Thông báo");

                return true;
            }

            if (!regexItem.IsMatch(pbMatKhauDK.Password))
            {
                MessageBox.Show("Mật khẩu chỉ cho phép kí tự a-z và 0-9 .Bạn vui lòng nhập lại", "Thông báo");

                return true;
            }
            if (string.IsNullOrWhiteSpace(pbMatKhauDK.Password))
            {
                MessageBox.Show("Mật khẩu không được bỏ trống hoặc có khoảng cách", "Thông báo");

                return true;
            }

            if (!regexItem.IsMatch(pbNhapLaiDK.Password))
            {
                MessageBox.Show("Mật khẩu nhập lại chỉ cho phép kí tự a-z và 0-9 .Bạn vui lòng nhập lại", "Thông báo");

                return true;
            }
            if (string.IsNullOrWhiteSpace(pbNhapLaiDK.Password))
            {
                MessageBox.Show("Mật khẩu nhập lại không được bỏ trống hoặc có khoảng cách", "Thông báo");

                return true;
            }

            if (pbMatKhauDK.Password != pbNhapLaiDK.Password)
            {
                MessageBox.Show("Mật khẩu và mật khẩu nhập lại không trùng nhau", "Thông báo");

                return true;
            }


          

          


            if (string.IsNullOrWhiteSpace(dpNgaySinhDK.Text))
            {
                MessageBox.Show("Bạn chưa chọn ngày sinh.", "Thông báo");

                return true;
            }


            if (rbNuDK.IsChecked == false && rbNamDK.IsChecked == false)
            {
                MessageBox.Show("Chưa chọn giới tính!");
                return true;
            }
            DataClasses1DataContext context = new DataClasses1DataContext();
            List<TaiKhoanDN> data1 = context.TaiKhoanDNs.Where(t => t.TenDangnhap == txbTenDangNhapDK.Text ).ToList();

            if (data1.Count > 0)

            {
                MessageBox.Show("Tài khoản này đã tồn tại!", "Thông báo");
                return true;
            }

            return false;
        }

        private void BtnDangKyDK_Click(object sender, RoutedEventArgs e)
        {
            if (KiemTraHopLe())
            {
                return;
            }

            else
            {
                try
                { TaiKhoanDN TS = new TaiKhoanDN();
                    if (rbNamDK.IsChecked == true) { TS.GioiTinh = "Nam"; }
                    if (rbNuDK.IsChecked == true) {TS.GioiTinh ="Nữ"; }

                   
                    //Chèn vào csdl thong tin tai khoan
                    //DataTable dt2 = KetNoi.LayDuLieu("INSERT INTO TAIKHOAN  VALUES('" + txbTenDangNhapDK.Text + "','" + pbMatKhauDK.Password + "',N'" + cmCauHoiBaoMatDK.Text + "',N'" + txbDapAnDK.Text + "',N'" + txbHoTenDK.Text + "',N'" + GioiTinh + "','" + dpNgaySinhDK.Text + "','" + txbTenDangNhapDK.Text + ".png" + "','Player')");
                    ////chèn tendn va hoten vào bảng KETQUA   
                    //DataTable dt3 = KetNoi.LayDuLieu("INSERT INTO KETQUA (tendn,hoten) VALUES('" + txbTenDangNhapDK.Text + "',N'" + txbHoTenDK.Text + "')");
                   
                    TS.TenDangnhap = txbTenDangNhapDK.Text;
                    TS.Matkhau = pbMatKhauDK.Password;
                    TS.MaTS = txbMaTSDK.Text;
                    TS.NgaySinh = dpNgaySinhDK.Text;
                    TS.Quyen = cbquyen.Text;



                    MessageBox.Show("Chúc mừng bạn đã đăng ký thành công! Mời bạn đăng nhập để bắt đầu thi.", "Thành công");

                    //lưu dữ liệu vào data để đăng nhập
                    context.TaiKhoanDNs.InsertOnSubmit(TS);
                    context.SubmitChanges();




                    Login LG = new Login();
                    LG.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    string loi = ex.Message;
                    MessageBox.Show("Phát hiện lỗi như sau: " + loi, "Thông báo");
                }



            }
        }
       
        DataClasses1DataContext context = new DataClasses1DataContext();
        private void ImgShowHide_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowPassword();

        }

        private void ImgShowHide_MouseLeave(object sender, MouseEventArgs e)
        {
            HidePassword();

        }

        private void ImgShowHide_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            HidePassword();
        }

        private void PbMatKhauDK_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pbMatKhauDK.Password.Length > 0)
                ImgShowHide.Visibility = Visibility.Visible;
            else
                ImgShowHide.Visibility = Visibility.Hidden;

            //kiểm tra dữ liệu nhập hợp lệ không
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (!regexItem.IsMatch(pbMatKhauDK.Password))
            {
                MessageBox.Show("Mật khẩu chỉ được nhập các kí tự a-z và số 0-9 thôi", "Thông báo");

            }
            else if (string.IsNullOrWhiteSpace(pbMatKhauDK.Password))
            {
                MessageBox.Show("Mật khẩu không được bỏ trống hoặc có khoảng cách", "Thông báo");


            }
        }

        void ShowPassword()
        {
            ImgShowHide.Source = new BitmapImage(new Uri("Image/Hide.jpg"));
            tbMatKhauDK.Visibility = Visibility.Visible;
            pbMatKhauDK.Visibility = Visibility.Hidden;
            tbMatKhauDK.Text = pbMatKhauDK.Password;

        }
        void HidePassword()
        {
            ImgShowHide.Source = new BitmapImage(new Uri("Image/Show.jpg"));
            tbMatKhauDK.Visibility = Visibility.Hidden;
            pbMatKhauDK.Visibility = Visibility.Visible;
            pbMatKhauDK.Focus();

        }


        private void ImgShowHide2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowPassword2();

        }

        private void ImgShowHide2_MouseLeave(object sender, MouseEventArgs e)
        {
            HidePassword2();

        }

        private void ImgShowHide2_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            HidePassword2();
        }


        void ShowPassword2()
        {
            ImgShowHide2.Source = new BitmapImage(new Uri("Image/Hide.jpg"));
            tbNhapLaiDK.Visibility = Visibility.Visible;
            pbNhapLaiDK.Visibility = Visibility.Hidden;
            tbNhapLaiDK.Text = pbNhapLaiDK.Password;

        }
        void HidePassword2()
        {
            ImgShowHide2.Source = new BitmapImage(new Uri("Image/Show.jpg"));
            tbNhapLaiDK.Visibility = Visibility.Hidden;
            pbNhapLaiDK.Visibility = Visibility.Visible;
            pbNhapLaiDK.Focus();

        }


        private void PbNhapLaiDK_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //đếm kế tự để hiện ẩn ảnh showhide password
            if (pbNhapLaiDK.Password.Length > 0)
                ImgShowHide2.Visibility = Visibility.Visible;
            else
                ImgShowHide2.Visibility = Visibility.Hidden;

            //kiểm tra dữ liệu nhập hợp lệ không
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
            if (!regexItem.IsMatch(pbNhapLaiDK.Password))
            {
                MessageBox.Show("Mật khẩu chỉ được nhập các kí tự a-z và số 0-9 thôi", "Thông báo");

            }
            else if (string.IsNullOrWhiteSpace(pbNhapLaiDK.Password))
            {
                MessageBox.Show("Mật khẩu không được bỏ trống hoặc có khoảng cách", "Thông báo");

            }




        }
           

        

        private void TxbTenDangNhapDK_TextChanged(object sender, TextChangedEventArgs e)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (!regexItem.IsMatch(txbTenDangNhapDK.Text))
            {
                MessageBox.Show("Tài khoản chỉ được nhập các kí tự a-z và số 0-9 thôi", "Thông báo");

            }
            else if (string.IsNullOrWhiteSpace(txbTenDangNhapDK.Text))
            {
                MessageBox.Show("Tài khoản không được bỏ trống hoặc có khoảng cách", "Thông báo");


            }

        }

        private void bntquaylai(object sender, RoutedEventArgs e)
        {
            Login LG = new Login();
            LG.Show();
            this.Close();

        }
        //private void GetQuyen()
        //{

        //    //Đặt tên combobox vd"cbTacgia"
        //    var quyen = from item in context.GetTable<TAIKHOAN>() select item;//Table cần hiển thị lên cbb vd"tacgia"
        //    cbquyen.ItemsSource = quyen;
        //    cbquyen.DisplayMemberPath = "Quyen";//Tên các đối tượng cần hiển thị
        //    cbquyen.SelectedValuePath = "TenDangnhap";//id table cần đưa lên combobox

        //}
      
    }
}
