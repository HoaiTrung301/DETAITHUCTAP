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

namespace DETAITHUCTAP
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private MainWindow mainWindow;

        public Login()
        {
            InitializeComponent();
           
        }

        public Login(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        //public Login(String str)
        //{
        //    InitializeComponent();
        //    txtUserName.Text = str;
        //}
        //MainWindow frm;
        //public Login(MainWindow frm)
        //{
        //    InitializeComponent();
        //    this.frm = frm;
        //}


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            DataClasses1DataContext context = new DataClasses1DataContext();
          
            List<TaiKhoanDN> data = context.TaiKhoanDNs.Where(t => t.TenDangnhap == txtUserName.Text && t.Matkhau == txtPass.Password && t.Quyen == cbQuyen.Text).ToList();
            

            if (txtUserName.Text == "" || txtPass.Password == "" || cbQuyen.Text=="")
            {
                MessageBox.Show("Bạn hãy nhập đầy đủ thông tin Cơ Sở Dữ Liệu vào!", "Thông Báo!");
            }
            else
            {

                if (data.Count > 0 )
                {
                    MainWindow frm1 = new MainWindow(txtUserName.Text, cbQuyen.Text);
                    frm1.Show();
                    Login lg = new Login();
                    this.Close();
                    //MainWindow frm1 = new MainWindow();
                    //frm1.Show();
                    //frm.setData(txtUserName.Text);
                }
                else
                {

                    MessageBoxResult result = MessageBox.Show("Tên Đăng Nhập  Hoặc Mật Khẩu Bạn Nhập Không Đúng Hoắc Chức Vụ ko đúng ! Xin Nhập lại!", "Thông Báo!");
                    txtUserName.Focus();
                    txtPass.Focus();
                    return;
                }
            }
           
        }

       

        private void bntdangky_click(object sender, RoutedEventArgs e)
        {
            Dangky dk = new Dangky();
            dk.Show();
            this.Close();
        }

        private void bntthoat(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
