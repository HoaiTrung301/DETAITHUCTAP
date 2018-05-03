using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Interaction logic for Chondethi.xaml
    /// </summary>
    public partial class Chondethi : Window
    {
          private  String _hoten ;
  

        public Chondethi()
        {
            //GetData();
            InitializeComponent();
        }
        public Chondethi(string hoten )
        {
            //GetData();
            InitializeComponent();
            _hoten = hoten;
         
            lbHoTenTTNC.Text = _hoten;
            data1();
           

        }
        private void data1()
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
           TaiKhoanDN data = context.TaiKhoanDNs.Single(t => t.TenDangnhap == _hoten);

         

        }
        //public Chondethi(string UserName, string gioitinh, string ngaysinh )
        //{
        //    //GetData();
        //    InitializeComponent();

        //    this.gioitinh = gioitinh;
        //    this.ngaysinh = ngaysinh;
        //    this.tendn = UserName;
        //    this.dethi = dethi;




        //    gbThongTin.Header = "Xin chào " + tendn + " !";
        //}



        private void bntsuathongtin(object sender, RoutedEventArgs e)
        {
            Suathongtinthisinh updateinfo = new Suathongtinthisinh(_hoten);
            updateinfo.Show();
            this.Close();
        }

    

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //        public partial class Sach : Window
            //{
            //    public Sach()
            //    {
            //        InitializeComponent();
            try
            {
                GetDataTacGia();
                //GetData();
            }
            catch
            {
                MessageBox.Show("Hiện không tìm thấy bộ đề nào...", "Lỗi");
            }
            //    }
        }
        //    QuanlysachDataContext db = new QuanlysachDataContext();
        DataClasses1DataContext context = new DataClasses1DataContext();
        private void GetDataTacGia()
        {

            //Đặt tên combobox vd"cbTacgia"
            var dethi = from item in context.GetTable<Dethi>() select item;//Table cần hiển thị lên cbb vd"tacgia"
           cbDeThiTTNC.ItemsSource =dethi;
           cbDeThiTTNC.DisplayMemberPath = "madethi";//Tên các đối tượng cần hiển thị
            cbDeThiTTNC.SelectedValuePath = "macauhoi";//id table cần đưa lên combobox

        }

        private void dgBangXepHang_Loaded(object sender, RoutedEventArgs e)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            dgBangXepHang.ItemsSource = from u in context.tbBaitralois
                                        from t in context.TaiKhoanDNs
                                        where u.MaTS == t.MaTS
                                        select new {
                                            MãThiSinh = u.MaTS,
                                            
                                            TênThiSinh = t.TenDangnhap,
                                         
                                              



                                        };




            //dgBangXepHang.DataContext = context.KETQUAs;
            //dgBangXepHang.DataContext = context.ThiSinhThis;
        }
        //private void GetData()
        //{


        //    var ThiSinhTable = from item in context.GetTable<KETQUA>() select item;
        //    dgBangXepHang.ItemsSource = ThiSinhTable;


        //}

        private void dgBangXepHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int rowindex = dgBangXepHang.SelectedIndex;
            if (rowindex == -1)
            {
                MessageBox.Show("Bạn phải chọn một dòng cần sửa!", "Chú ý!");
            }
            else
            {

                tbBaitraloi KQ = (tbBaitraloi)dgBangXepHang.SelectedItem; 
               

                
            }
        }
   

        private void bntbatdauthi(object sender, RoutedEventArgs e)
        {


            thi w = new thi(_hoten, cbDeThiTTNC.Text,txtdoithi.Text);
            w.Show();
            this.Close();
        }
    }
}
