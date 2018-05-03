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
    /// Interaction logic for Nguoidung.xaml
    /// </summary>
    public partial class Nguoidung : Window
    {
        public Nguoidung()
        {
            InitializeComponent();
          
            GetData();
        }

        private void datagrid_SelectionChanged()
        {
            throw new NotImplementedException();
        }

        private void GetData()
        {


            var NguoiDungTable = from item in context.GetTable<TaiKhoanDN>() select item;
            datagrid.ItemsSource = NguoiDungTable;

        }
        DataClasses1DataContext context = new DataClasses1DataContext();
       

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int rowindex = datagrid.SelectedIndex;
            if (rowindex == -1)
            {
                MessageBox.Show("Bạn phải chọn một dòng cần sửa!", "Chú ý!");
            }
            else
            {

               TaiKhoanDN sv = (TaiKhoanDN)datagrid.SelectedItem;

                txtHoTen.Text = sv.TenDangnhap;
                txtMaNguoidung.Text = sv.MaTS.ToString();
                txtMatkhau.Text = sv.Matkhau;
                txtNgaySinh.Text = sv.NgaySinh.ToString();
               if(sv.GioiTinh == "Nam") { rbNamDK.IsChecked = true; }
                if (sv.GioiTinh == "Nữ") { rbNuDK.IsChecked = true; }

                txtquyen.Text = sv.Quyen.ToString();
               



                

            }
        }
        private void AddNewNguoidung()
        {

TaiKhoanDN sv = new TaiKhoanDN();
            sv.MaTS = (txtMaNguoidung.Text);
            sv.TenDangnhap = (txtHoTen.Text);
            sv.Matkhau = (txtMatkhau.Text);
            sv.NgaySinh = (txtNgaySinh.Text);
            if (rbNamDK.IsChecked == true) { sv.GioiTinh = "Nam"; }
            if (rbNuDK.IsChecked == true) { sv.GioiTinh = "Nữ"; }

            context.TaiKhoanDNs.InsertOnSubmit(sv);
            context.SubmitChanges();
        }

        private void btnthem_Click(object sender, RoutedEventArgs e)
        {
            AddNewNguoidung();
            GetData();
        }

        private void datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            datagrid.DataContext = context.TaiKhoanDNs;
        }

        private void bntsua(object sender, RoutedEventArgs e)
        {
            Update();
            GetData();
        }

        private void Update()
        {
           TaiKhoanDN sv = context.TaiKhoanDNs.Single(item => item.MaTS == (txtMaNguoidung.Text));
            if (sv.MaTS != txtMaNguoidung.Text)
            {
                MessageBox.Show("Không được sửa Mã Người Dùng");
            }
            sv.TenDangnhap = (txtHoTen.Text);
            sv.Matkhau = (txtMatkhau.Text);
            sv.NgaySinh = (txtNgaySinh.Text);
            if (rbNamDK.IsChecked == true) { sv.GioiTinh = "Nam"; }
            if (rbNuDK.IsChecked == true) { sv.GioiTinh = "Nữ"; }
            sv.Quyen = (txtquyen.Text);
            
            context.SubmitChanges();
        }
        private void bntxoa(object sender, RoutedEventArgs e)
        {
            DeleteNguoidung();
            GetData();
        }
        private void DeleteNguoidung()
        {

           
        TaiKhoanDN sv = context.TaiKhoanDNs.Single(item => item.MaTS == (txtMaNguoidung.Text));

            context.TaiKhoanDNs.DeleteOnSubmit(sv);

            context.SubmitChanges();
        }

        private void bnttimkiem(object sender, RoutedEventArgs e)
        {

            if (txtTimKiem.Text == "")
            {
                MessageBox.Show("Bạn hãy nhập mã thông tin tìm kiếm vào!", "Thông báo!");
            }
            else
            {
                if (txtTimKiem.Text == "*")
                {
                    GetData();
                }
                else
                {
                    datagrid.ItemsSource = context.TaiKhoanDNs
  .Where(item =>
      item.MaTS.ToString().Contains(txtTimKiem.Text) ||
      item.TenDangnhap.Contains(txtTimKiem.Text)
      )
  .ToList();
                    txtTimKiem.Text = "*";
                }
            }
        }

       
    }
}
