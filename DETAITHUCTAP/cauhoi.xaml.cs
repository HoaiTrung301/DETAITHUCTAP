using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for cauhoi.xaml
    /// </summary>
    public partial class cauhoi : Window
    {
        String hinhanh;
        public cauhoi()
        {
            InitializeComponent();
            GetData();
        }
        private void GetData()
        {


            var CauhoiTable = from item in context.GetTable<tbCAUHOI>() select item;
            dgBangcauhoi.ItemsSource = CauhoiTable;

        }
      
        DataClasses1DataContext context = new DataClasses1DataContext();


        private void AddNewCauhoi()
        {


          tbCAUHOI nh = new tbCAUHOI();
           nh.macauhoi = (txtmacauhoi.Text);
            nh.maloaicauhoi = (txtloaicauhoi.Text);
            nh.cauhoi = (txtcauhoi.Text);
            nh.cau_a = (txtcaua.Text);
           nh.cau_b = (txtcaub.Text);
            nh.cau_c = (txtcauc.Text);
            nh.cau_d = (txtcaud.Text);
           nh.dapan = (txtdapan.Text);

            context.tbCAUHOIs.InsertOnSubmit(nh);
            context.SubmitChanges();
        }
        private void btnthem_Click(object sender, RoutedEventArgs e)
        {
            AddNewCauhoi();
            GetData();
        }

      

        private void Update()
        {
          tbCAUHOI sv = context.tbCAUHOIs.Single(item => item.macauhoi == (txtmacauhoi.Text));
            if (sv.macauhoi != txtmacauhoi.Text)
            {
                MessageBox.Show("Không được sửa Mã Người Dùng");
            }
            sv.cauhoi = (txtcauhoi.Text);
            sv.macauhoi = txtmacauhoi.Text;
            sv.maloaicauhoi = txtloaicauhoi.Text;
            sv.cau_a = (txtcaua.Text);
            sv.cau_b = (txtcaub.Text);
            sv.cau_c = (txtcauc.Text);
            sv.cau_d = (txtcaud.Text);
            sv.dapan = (txtdapan.Text);
            
            context.SubmitChanges();
        }



      

        private void bntsua(object sender, RoutedEventArgs e)
        {
            Update();
            GetData();
        }
        private void bntxoa(object sender, RoutedEventArgs e)
        {
            DeleteNguoidung();
            GetData();
        }
        private void DeleteNguoidung()
        {


           tbCAUHOI sv = context.tbCAUHOIs.Single(item => item.macauhoi == (txtmacauhoi.Text));

            context.tbCAUHOIs.DeleteOnSubmit(sv);

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
                    dgBangcauhoi.ItemsSource = context.tbCAUHOIs.Where(item => item.macauhoi.ToString().Contains(txtTimKiem.Text) ||
      item.maloaicauhoi.Contains(txtTimKiem.Text)
      )
  .ToList();
                    txtTimKiem.Text = "*";
                }
            }
        }

      

       
     

       
      
        //private void dgBangcauhoi_Loaded(object sender, RoutedEventArgs e)
        //{
           
        //    dgBangcauhoi.ItemsSource = context.Cauhois;
        //}

        private void dgBangcauhoi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int rowindex = dgBangcauhoi.SelectedIndex;
            if (rowindex == -1)
            {
                MessageBox.Show("Bạn phải chọn một dòng cần sửa!", "Chú ý!");
            }
            else
            {

             tbCAUHOI sv = (tbCAUHOI)dgBangcauhoi.SelectedItem;

                txtmacauhoi.Text = sv.macauhoi;
                txtloaicauhoi.Text = sv.maloaicauhoi;
                txtcauhoi.Text = sv.cauhoi;
                txtcaua.Text = sv.cau_a;
                txtcaub.Text = sv.cau_b;
                txtcauc.Text = sv.cau_c;
                txtcaud.Text = sv.cau_d;
                txtdapan.Text = sv.dapan;

                
            }
        }

        private void dgBangcauhoi_Loaded(object sender, RoutedEventArgs e)
        {
            dgBangcauhoi.ItemsSource = context.tbCAUHOIs;
        }

        private void BtnChonAnhSTT_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog()
            {
                Title = "Select a picture",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png"
            };
            if (op.ShowDialog() == true)
            {


                imgHinhAnhSTT.Source = new BitmapImage(new Uri(op.FileName));
                hinhanh = op.FileName;

            }
        }
    }
}



