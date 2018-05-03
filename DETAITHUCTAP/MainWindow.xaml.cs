using DETAITHUCTAP;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DETAITHUCTAP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public String _Username;
       public String _chucvu;
        public String _NgaySinh;
       public String _GioiTinh;
        private string _hoten;

        public MainWindow()
        {
            InitializeComponent();
        }
      

        public MainWindow( string Username , string Quyen)
        {
            
            InitializeComponent();
            _Username = Username;
            _chucvu = Quyen;
            txtName.Text = _Username;
            txtquyen.Text = _chucvu;
            DataClasses1DataContext context = new DataClasses1DataContext();
            //List<TAIKHOAN> data = context.TAIKHOANs.Where(t => t.TenDangnhap == txtName.Text || t.Quyen == txtquyen.Text).ToList();
        }

        public MainWindow(string Username)
           
        { InitializeComponent();
            _Username = Username;
            txtName.Text = _Username;

        }





        //public void setData(string str)
        //{
        //    txtName.Text = str;
        //}

        private void exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



      

       

       

     

      

        private void quanlynguoidung(object sender, RoutedEventArgs e)
        {
            if (txtquyen.Text == "User")
            {
                MessageBox.Show("Bạn Không có Quyền Dùng chức năng này!");
            }
            else
            {
                Nguoidung ND = new Nguoidung();
                ND.Show();
                
            }
        }

       

      

       

        //private void trangchu(object sender, RoutedEventArgs e)
        //{
        //    Main.Content = new Page1();
        //}

        private void quanlycauhoi(object sender, RoutedEventArgs e)
        {
            if (txtquyen.Text == "User")
            {
                MessageBox.Show("Bạn Không có Quyền Dùng chức năng này!");
            }
            else
            {
                cauhoi ch = new cauhoi();
                ch.Show();
            }
        }
       

        public void click_chuanbithi(object sender, RoutedEventArgs e)
        {
            txtName.Text = _Username;
            Chondethi chondth = new Chondethi(_Username);
            chondth.Show();
           
        }

        private void bntdangxuat(object sender, RoutedEventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            this.Close();
        }
    }
}
