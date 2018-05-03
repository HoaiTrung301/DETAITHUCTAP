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
    /// Interaction logic for Suathongtinthisinh.xaml
    /// </summary>
    public partial class Suathongtinthisinh : Window
    {
        string tendn = null;
        //string gioitinh = null;
        //string hinhanh = null;
        public Suathongtinthisinh(String tendn)
        {
            InitializeComponent();
            this.tendn = tendn;
           
        }
        bool KiemTraHopLe1()
        {
           
            {

                //kiểm tra kí tự nhập vào TextBox Tên đăng nhập nếu đúng thì mới thực hiện đăng ký
                var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

                if (!regexItem.IsMatch(txbTenDangNhapSTT.Text))
                {
                    MessageBox.Show("Tên đăng nhập chỉ cho phép kí tự a-z và 0-9 .Bạn vui lòng nhập lại", "Thông báo");

                    return true;
                }
                if (string.IsNullOrWhiteSpace(txbTenDangNhapSTT.Text))
                {
                    MessageBox.Show("Tài khoản không được bỏ trống hoặc có khoảng cách", "Thông báo");

                    return true;
                }

                if (!regexItem.IsMatch(pbMatKhauSTT.Password))
                {
                    MessageBox.Show("Mật khẩu chỉ cho phép kí tự a-z và 0-9 .Bạn vui lòng nhập lại", "Thông báo");

                    return true;
                }
                if (string.IsNullOrWhiteSpace(pbMatKhauSTT.Password))
                {
                    MessageBox.Show("Mật khẩu không được bỏ trống hoặc có khoảng cách", "Thông báo");

                    return true;
                }

                if (!regexItem.IsMatch(pbNhapLaiSTT.Password))
                {
                    MessageBox.Show("Mật khẩu nhập lại chỉ cho phép kí tự a-z và 0-9 .Bạn vui lòng nhập lại", "Thông báo");

                    return true;
                }
                if (string.IsNullOrWhiteSpace(pbNhapLaiSTT.Password))
                {
                    MessageBox.Show("Mật khẩu nhập lại không được bỏ trống hoặc có khoảng cách", "Thông báo");

                    return true;
                }

                if (pbNhapLaiSTT.Password != pbMatKhauSTT.Password)
                {
                    MessageBox.Show("Mật khẩu và mật khẩu nhập lại không trùng nhau", "Thông báo");

                    return true;
                }







                if (string.IsNullOrWhiteSpace(dpNgaySinhSTT.Text))
                {
                    MessageBox.Show("Bạn chưa chọn ngày sinh.", "Thông báo");

                    return true;
                }


               
            }
            return false;
        }

        private void Update()
        {
            TaiKhoanDN ts = context.TaiKhoanDNs.Single(item => item.TenDangnhap == (txbTenDangNhapSTT.Text));
           
           
            ts.Matkhau = pbMatKhauSTT.Password;
            ts.NgaySinh = dpNgaySinhSTT.Text;
            context.TaiKhoanDNs.InsertOnSubmit(ts);
            context.SubmitChanges();
        } DataClasses1DataContext context = new DataClasses1DataContext();
      
        private void bntUpdate(object sender, RoutedEventArgs e)
        {
            Update();

        }
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
            if (pbMatKhauSTT.Password.Length > 0)
                ImgShowHide.Visibility = Visibility.Visible;
            else
                ImgShowHide.Visibility = Visibility.Hidden;

            //kiểm tra dữ liệu nhập hợp lệ không
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (!regexItem.IsMatch(pbMatKhauSTT.Password))
            {
                MessageBox.Show("Mật khẩu chỉ được nhập các kí tự a-z và số 0-9 thôi", "Thông báo");

            }
            else if (string.IsNullOrWhiteSpace(pbMatKhauSTT.Password))
            {
                MessageBox.Show("Mật khẩu không được bỏ trống hoặc có khoảng cách", "Thông báo");


            }
        }

        void ShowPassword()
        {
            ImgShowHide.Source = new BitmapImage(new Uri("Image/Hide.jpg"));
            tbMatKhauSTT.Visibility = Visibility.Visible;
            pbMatKhauSTT.Visibility = Visibility.Hidden;
           tbMatKhauSTT.Text = pbMatKhauSTT.Password;

        }
        void HidePassword()
        {
            ImgShowHide.Source = new BitmapImage(new Uri("Image/Show.jpg"));
            tbMatKhauSTT.Visibility = Visibility.Hidden;
            pbMatKhauSTT.Visibility = Visibility.Visible;
           pbMatKhauSTT.Focus();

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
           tbNhapLaiSTT.Visibility = Visibility.Visible;
           pbNhapLaiSTT.Visibility = Visibility.Hidden;
            tbNhapLaiSTT.Text = pbNhapLaiSTT.Password;

        }
        void HidePassword2()
        {
            ImgShowHide2.Source = new BitmapImage(new Uri("Image/Show.jpg"));
            tbNhapLaiSTT.Visibility = Visibility.Hidden;
            pbNhapLaiSTT.Visibility = Visibility.Visible;
            pbNhapLaiSTT.Focus();

        }


        //private void PbNhapLaiDK_PasswordChanged(object sender, RoutedEventArgs e)
        //{
        //    //đếm kế tự để hiện ẩn ảnh showhide password
        //    if (pbNhapLaiSTT.Password.Length > 0)
        //        ImgShowHide2.Visibility = Visibility.Visible;
        //    else
        //        ImgShowHide2.Visibility = Visibility.Hidden;

        //    //kiểm tra dữ liệu nhập hợp lệ không
        //    var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
        //    if (pbMatKhauSTT.Password != pbNhapLaiSTT.Password)
        //    {
        //        MessageBox.Show("Mật Khẩu Không Trùng Khớp! Xin Nhập Lại");
        //    }
        //    else
        //    if (!regexItem.IsMatch(pbNhapLaiSTT.Password) && string.IsNullOrWhiteSpace(pbNhapLaiSTT.Password))
        //    {
        //        MessageBox.Show("Mật khẩu nhập lại chỉ được nhập các kí tự a-z và số 0-9 thôi Hoặc Mật khẩu nhập lại không được bỏ trống hoặc có khoảng cách", "Thông báo");


        //    }




        




        //private void TxbTenDangNhapDK_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

        //    if (!regexItem.IsMatch(txbTenDangNhapSTT.Text))
        //    {
        //        MessageBox.Show("Tài khoản chỉ được nhập các kí tự a-z và số 0-9 thôi", "Thông báo");

        //    }
        //    else if (string.IsNullOrWhiteSpace(txbTenDangNhapSTT.Text))
        //    {
        //        MessageBox.Show("Tài khoản không được bỏ trống hoặc có khoảng cách", "Thông báo");


        //    }

        //}

        private void BtnQuayLaiSTT_Click(object sender, RoutedEventArgs e)
        {
            Chondethi cdth = new Chondethi();
            cdth.Show();
            this.Close();

        }

      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      

      

        private void PbMatKhauSTT_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pbMatKhauSTT.Password.Length > 0)
                ImgShowHide.Visibility = Visibility.Visible;
            else
                ImgShowHide.Visibility = Visibility.Hidden;


            //kiểm tra dữ liệu nhập hợp lệ không
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (!regexItem.IsMatch(pbMatKhauSTT.Password))
            {
                MessageBox.Show("Mật khẩu chỉ được nhập các kí tự a-z và số 0-9 thôi", "Thông báo");

            }
            else if (string.IsNullOrWhiteSpace(pbMatKhauSTT.Password))
            {
                MessageBox.Show("Mật khẩu không được bỏ trống hoặc có khoảng cách", "Thông báo");

            }
        }

        private void PbNhapLaiSTT_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pbNhapLaiSTT.Password.Length > 0)
                ImgShowHide2.Visibility = Visibility.Visible;
            else
                ImgShowHide2.Visibility = Visibility.Hidden;

            //kiểm tra dữ liệu nhập hợp lệ không
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
            if (pbMatKhauSTT.Password != pbNhapLaiSTT.Password)
            {
                MessageBox.Show("Mật Khẩu Không Trùng Khớp! Xin Nhập Lại");
            }
            else
            if (!regexItem.IsMatch(pbNhapLaiSTT.Password) && string.IsNullOrWhiteSpace(pbNhapLaiSTT.Password))
            {
                MessageBox.Show("Mật khẩu nhập lại chỉ được nhập các kí tự a-z và số 0-9 thôi Hoặc Mật khẩu nhập lại không được bỏ trống hoặc có khoảng cách", "Thông báo");


            }
        }

        private void txbTenDangNhapSTT_TextChanged(object sender, TextChangedEventArgs e)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (!regexItem.IsMatch(txbTenDangNhapSTT.Text))
            {
                MessageBox.Show("Tài khoản chỉ được nhập các kí tự a-z và số 0-9 thôi", "Thông báo");

            }
            else if (string.IsNullOrWhiteSpace(txbTenDangNhapSTT.Text))
            {
                MessageBox.Show("Tài khoản không được bỏ trống hoặc có khoảng cách", "Thông báo");


            }
    }
    }
}
