

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Threading;

using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.IO;

namespace DETAITHUCTAP
{
    /// <summary>
    /// Interaction logic for thi.xaml
    /// </summary>
    public partial class thi : Window
    {

        string ngaythi = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
        private String _hoten1;
        private String _dethi;
        bool trangthai = false;
        private String _Doithi;
        //code xu ly thi
        private List<tbCAUHOI> arrCauHoi = new List<tbCAUHOI>();
        tbCAUHOI CauTracNghiem = new tbCAUHOI();
        


        public int diem = 0;

        public int SoCauSai = 0;

        //int CauHienTai = 0;
        //int SoCauHoi = 0;

        //int SoCauNgauNhien = 20;//quy định số câu ngẫu nhiên
        DataClasses1DataContext context = new DataClasses1DataContext();
        //public thi()
        //{
        //    InitializeComponent();

        //}

        //timer trong wpf khác winform
        //int s, p, h;
        //DispatcherTimer dispatcherTimer = new DispatcherTimer();

        private void bntthilai(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Nếu bạn quay lại, bài thi của bạn sẽ kết thúc ?", "Thông báo");


            Chondethi fr = new Chondethi(_hoten1);
            fr.Show();
            this.Close();

        }
        private int time = 15;
        private DispatcherTimer Timer;

        public thi(string hoten, string dethi, string doithi)
        {

            InitializeComponent();
            _hoten1 = hoten;
            _dethi = dethi;
            _Doithi = doithi;
            lbHoTenTTNC.Text = _hoten1;
            txtloaide.Text = _dethi;
            txtdoithi.Text = _Doithi;


            DataClasses1DataContext context = new DataClasses1DataContext();
            TaiKhoanDN data = context.TaiKhoanDNs.Single(t => t.TenDangnhap == _hoten1);
            txtmathisinh.Text = data.MaTS;

            //CauHoi();
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick1;
            //Timer.Start();






        }
        public void xuatExcel()
        {
            IEnumerable<tbBaitraloi> arr = Excel01();
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed !");
            }
            Excel.Workbook xlWorkbook;
            Excel.Worksheet xlWordSheet;
            object misvalue = System.Reflection.Missing.Value;
            xlWorkbook = xlApp.Workbooks.Add(misvalue);
            xlWordSheet = (Excel.Worksheet)xlWorkbook.Worksheets.get_Item(1);

            xlWordSheet.Cells[1, 1] = "Mã Thí Sinh";
            xlWordSheet.Cells[1, 2] = "Mã Câu Hỏi";
            xlWordSheet.Cells[1, 3] = "Mã Đợt Thi";
            xlWordSheet.Cells[1, 4] = "Câu Trả Lời";
            xlWordSheet.Cells[1, 5] = "Đáp Án Đúng";
            xlWordSheet.Cells[1, 6] = "Số Câu Đúng";
            int idx = 2;
            foreach (var row in arr)
            {
                xlWordSheet.Cells[idx, 1] = row.MaTS.ToString();
                xlWordSheet.Cells[idx, 2] = row.macauhoi.ToString();
                xlWordSheet.Cells[idx, 3] = row.madoithi.ToString();
                xlWordSheet.Cells[idx, 4] = row.cautraloi.ToString();
                xlWordSheet.Cells[idx, 5] = row.Chamcau.ToString();
                xlWordSheet.Cells[idx, 6] = row.Socaudung.ToString();

                idx++;


            }
            string filename = AppDomain.CurrentDomain.BaseDirectory + DateTime.Now.Ticks + ".xls";
            xlWorkbook.SaveAs(filename, Excel.XlFileFormat.xlWorkbookNormal, misvalue, misvalue, misvalue, misvalue);
            xlWorkbook.Close(true, misvalue, misvalue);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlWordSheet);
            Marshal.ReleaseComObject(xlWorkbook);
            Marshal.ReleaseComObject(xlApp);
            MessageBox.Show("Fle Excel đã được tạo , bạn có thể tìm kiếm file " + filename);
            FileInfo fi = new FileInfo(filename);
            if (fi.Exists)
            {
                System.Diagnostics.Process.Start(filename);

            }



        }

        private IEnumerable<tbBaitraloi> Excel01()
        {
            var resul = context.tbBaitralois.Where(u => u.madoithi == txtdoithi.Text ).OrderBy(t => t.MaTS);
            return resul;
        }





        //private void CauHoi()
        // {
        //     Cauhoi data1 = context.Cauhois.Single(t => t.maloaicauhoi == _dethi);
        //     lbCauHoiGDT.Text = data1.cauhoi1;
        //     lbA.Text = data1.cau_a;
        //     lbB.Text = data1.cau_b;
        //     lbC.Text = data1.cau_c;
        //     lbD.Text = data1.cau_d;
        // }



        private void Timer_Tick1(object sender, EventArgs e)
        {
            if (time > 0)
            {


                if (time < 15)
                {
                    if (time < 5)
                    {
                        lbPhut.Foreground = Brushes.Red;
                    }
                    else
                    {
                        lbPhut.Foreground = Brushes.Black;
                    }
                    time--;
                    lbPhut.Text = string.Format("00:0{0}:{1}", time / 60, time % 60);
                }
                else
                {
                    time--;
                    lbPhut.Text = string.Format("00:0{0}:{1}", time / 60, time % 60);
                }
            }
            else
            {
                Timer.Stop();
                bntbatdau.IsEnabled = true;
                btnChoiLaiGDT.IsEnabled = true;
                bntbatdau.IsEnabled = true;
                rbA.IsEnabled = false;
                rbB.IsEnabled = false;
                rbC.IsEnabled = false;
                rbD.IsEnabled = false;


                System.Windows.MessageBox.Show("Hết thời Gian");
            }
        }

        //private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    DataClasses1DataContext context = new DataClasses1DataContext();
        //    Cauhoi sv = context.Cauhois.Single(item => item.macauhoi == (listBox1.SelectedItem).ToString());

        //    lbCauHoiGDT.Text = sv.cauhoi1;
        //    lbA.Text = sv.cau_a.ToString();
        //    lbB.Text = sv.cau_b.ToString();
        //    lbC.Text = sv.cau_c.ToString();
        //    lbD.Text = sv.cau_d.ToString();
        //}

        //private void listBox1_Loaded(object sender, RoutedEventArgs e)
        //{
        //    DataClasses1DataContext context = new DataClasses1DataContext();


        //    listBox1.ItemsSource = context.Cauhois.Where(u => u.maloaicauhoi == _dethi).Select(u => u.macauhoi);
        //}




        private int KTTraLoi()
        {
            if (rbA.IsChecked == true || rbB.IsChecked == true || rbC.IsChecked == true || rbD.IsChecked == true)
                return 1;
            return 0;
        }





        //private void LuuCauTraLoi()
        //{
        //    if (KTTraLoi() == 1)
        //    {
        //        if (rbA.IsChecked == true)
        //            arrCauHoi[CauHienTai].dapan = "A";
        //        if (rbB.IsChecked == true)
        //            arrCauHoi[CauHienTai].dapan = "B";
        //        if (rbC.IsChecked == true)
        //            arrCauHoi[CauHienTai].dapan = "C";
        //        if (rbD.IsChecked == true)
        //            arrCauHoi[CauHienTai].dapan = "D";
        //    }
        //    else
        //        arrCauHoi[CauHienTai].dapan = "E";
        //}

        //private void rbA_Checked(object sender, RoutedEventArgs e)
        //{
        //    LuuCauTraLoi();
        //}

        //private void rbB_Checked(object sender, RoutedEventArgs e)
        //{
        //    LuuCauTraLoi();
        //}

        //private void rbC_Checked(object sender, RoutedEventArgs e)
        //{
        //    LuuCauTraLoi();
        //}

        //private void rbD_Checked(object sender, RoutedEventArgs e)
        //{
        //    LuuCauTraLoi();
        //}






        //private void listBox1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{
        //    DataClasses1DataContext context = new DataClasses1DataContext();
        //    Cauhoi sv = context.Cauhois.Single(item => item.macauhoi == (listBox1.SelectedItem).ToString());

        //    lbCauHoiGDT.Text = sv.cauhoi1;
        //    lbA.Text = sv.cau_a.ToString();
        //    lbB.Text = sv.cau_b.ToString();
        //    lbC.Text = sv.cau_c.ToString();
        //    lbD.Text = sv.cau_d.ToString();
        //}

        //private void listBox1_Loaded_1(object sender, RoutedEventArgs e)
        //{
        //    DataClasses1DataContext context = new DataClasses1DataContext();


        //    listBox1.ItemsSource = context.Cauhois.Where(u => u.maloaicauhoi == _dethi).Select(u => u.macauhoi);
        //}



        //private void bntbatdau_click(object sender, RoutedEventArgs e)
        //{
        //    Timer.Start();
        //    bntbatdau.IsEnabled = false;

        //    btnChoiLaiGDT.IsEnabled = true;

        //    btnNextGDT.IsEnabled = true;
        //    btnBackGDT.IsEnabled = true;

        //    lbCauHoiGDT.Text = "1. " + arrCauHoi[0].cauhoi1;
        //    lbA.Text = arrCauHoi[0].cau_a;
        //    lbB.Text = arrCauHoi[0].cau_b;
        //    lbC.Text = arrCauHoi[0].cau_c;
        //    lbD.Text = arrCauHoi[0].cau_d;
        //}

        //private void rbD_Checked(object sender, RoutedEventArgs e)
        //{
        //    LuuCauTraLoi();
        //}

        //private void rbC_Checked(object sender, RoutedEventArgs e)
        //{
        //    LuuCauTraLoi();
        //}

        //private void rbA_Checked(object sender, RoutedEventArgs e)
        //{
        //    LuuCauTraLoi();
        //}

        //private void rbB_Checked(object sender, RoutedEventArgs e)
        //{
        //    LuuCauTraLoi();
        //}




        List<trailoi> data = new List<trailoi>();

        int index = 0;
        //int indexsamplecode = -1;
        private void taoCauHoi()
        {


            // txtInfo.Text = "";
            data.Clear();


            var result = from t1 in context.tbCAUHOIs
                         from t2 in context.Dethis
                         from t3 in context.Dotthis


                         where t1.maloaicauhoi == t2.maloaicauhoi && t2.madethi == t3.madethi && t2.madethi == _dethi
                         select new
                         {
                             Macauhoi = t1.macauhoi,
                             loaicauhoi01 = t2.maloaicauhoi,
                             Madethi = t2.madethi,
                             cauhoi = t1.cauhoi,
                             Dapan = t1.cau_a,
                             Dapan1 = t1.cau_b,
                             Dapan2 = t1.cau_c,
                             Dapan3 = t1.cau_d,
                             traloi = t1.dapan,



                             cautraloi = ""


                         };




            var data1 = result.OrderBy(x => Guid.NewGuid()).ToList();
            foreach (var item in data1)
            {
                trailoi ch = new trailoi();
                ch.Macauhoi = item.Macauhoi;
                ch.Madethi = item.Madethi;
                ch.Loaicauhoi = item.loaicauhoi01;

                ch.Cauhoi = item.cauhoi;
                ch.Dapan = item.Dapan;
                ch.Dapan1 = item.Dapan1;
                ch.Dapan2 = item.Dapan2;
                ch.Dapan3 = item.Dapan3;
                ch.Traloi = item.traloi;



                ch.Cautraloi = "";


                data.Add(ch);

            }




        }


        private void bntbatdau1(object sender, RoutedEventArgs e)
        {
            taoCauHoi();
            Timer.Start();
            bntbatdau.IsEnabled = false;

            btnChoiLaiGDT.IsEnabled = true;
            bntxemketqua.IsEnabled = false;
            btnNextGDT.IsEnabled = true;
            btnBackGDT.IsEnabled = true;
        



        }

        public void showCH()



        {


            //if (_dethi == "")
            //{ MessageBox.Show("ban Chua Chon De Thi"); }
            //else
            //if (data[index].Madethi == _dethi)
            // {
            lbCauHoiGDT.Text = data[index].Cauhoi;
            txtloaide.Text = data[index].Madethi;

            lbA.Text = data[index].Dapan;
            lbB.Text = data[index].Dapan1;
            lbC.Text = data[index].Dapan2;
            lbD.Text = data[index].Dapan3;


            if (data[index].Cautraloi == "A")
            {
                rbA.IsChecked = true;


            }

            if (data[index].Cautraloi == "B")
            {
                rbB.IsChecked = true;


            }
            if (data[index].Cautraloi == "C")
            {
                rbC.IsChecked = true;


            }
            if (data[index].Cautraloi == "D")
            {
                rbD.IsChecked = true;


            }



            //}
            //else
            //    MessageBox.Show("Ban chua duoc thi !!!");


            //void capnhatch()
            //{


            //    if (rbA.IsChecked == true)
            //    {
            //        data[index].Cautraloi = "A";


            //    }
            //    if (rbB.IsChecked == true)
            //    {
            //        data[index].Cautraloi = "B";
            //    }
            //     if (rbC.IsChecked == true)
            //    {
            //        data[index].Cautraloi = "C";

            //    }
            //    if (rbD.IsChecked == true)
            //    {
            //        data[index].Cautraloi = "D";



            //    }
            //private void Danh_Dap_An_ThiSinh()
            //{
            //    if (data[index].Cautraloi.ToString() == "E")
            //    {
            //        rbA.IsChecked = false;
            //        rbB.IsChecked = false;
            //        rbC.IsChecked = false;
            //        rbD.IsChecked = false;
            //    }
            //    else
            //    {
            //        if (data[index].Cautraloi.ToString() == "A")
            //        {
            //            rbA.IsChecked = true;
            //        }
            //        if (data[index].Cautraloi.ToString() == "B")
            //        {
            //            rbB.IsChecked = true;
            //        }
            //        if (data[index].Cautraloi.ToString() == "C")
            //        {
            //            rbC.IsChecked = true;
            //        }
            //        if (data[index].Cautraloi.ToString() == "D")
            //        {
            //            rbD.IsChecked = true;
            //        }

            //    }

        }
        void capnhatch()
        {

            if (rbA.IsChecked == true)
            {
                data[index].Cautraloi = "A";


            }
            else if (rbB.IsChecked == true)
            {
                data[index].Cautraloi = "B";
            }
            else if (rbC.IsChecked == true)
            {
                data[index].Cautraloi = "C";

            }
            else if (rbD.IsChecked == true)
            {
                data[index].Cautraloi = "D";



            }



        }

        //private void Dap_An_Dung()
        //{

        //    if (trangthai == true)
        //    {

        //        if (data[index].Traloi.ToString().Trim() == "A")
        //        {
        //            lbA.Background = Brushes.Red;
        //        }
        //         if (data[index].Traloi.ToString().Trim() == "B")
        //        {
        //            lbB.Background = Brushes.Red;
        //        }
        //         if (data[index].Traloi.ToString().Trim() == "C")
        //        {
        //            lbC.Background = Brushes.Red;
        //        }
        //        if (data[index].Traloi.ToString().Trim() == "D")
        //        {
        //            lbD.Background = Brushes.Red;
        //        }
        //    }


        //}
        private void ToMauCauDung(int index)
        {
            string DA = data[index].Traloi.ToString().Trim();
            //bool flag = false;
            if (DA == "A")
            {
                lbA.Background = Brushes.Red;
            }
            if (DA == "B")
            {
                lbB.Background = Brushes.Red;
            }
            if (DA == "C")
            {
                lbC.Background = Brushes.Red;
            }

            if (DA == "D")
            {
                lbD.Background = Brushes.Red;
            }
        }


        public void resetbox()
        {

            rbT.IsChecked = true;
            lbA.Background = Brushes.White;
            lbB.Background = Brushes.White;
            lbC.Background = Brushes.White;
            lbD.Background = Brushes.White;
        }

        private void btnNextGDT_Click(object sender, RoutedEventArgs e)
        {
            if (bntxemketqua.IsEnabled == true)
            {
                resetbox();
                ToMauCauDung(index);
            }
            capnhatch();
            if (index == data.Count - 1)
            {
                MessageBox.Show("Đã hết câu hỏi , Mời Bạn bấm vào nút Nộp bài ");
                //{
                //    nopbaithi();

                //}

                return;


            }
            rbA.IsChecked = false;
            rbB.IsChecked = false;
            rbC.IsChecked = false;
            rbD.IsChecked = false;
          

            index++;
           
            showCH();
            

        }

        private void bntback(object sender, RoutedEventArgs e)
        
        {
          
            if(bntxemketqua.IsEnabled == true)
            { resetbox();
                ToMauCauDung(index);
            }
                
            if (index == 0)
            {

                MessageBox.Show("Bạn đang ở câu đầu tiên");
                return;

            }

            index--;
            showCH();
          
         

        }
        private void traloi()
        {

           
        }
        //private void AddNewKetqua(trailoi tl)
        // {
        //     Ketquadoithi kq = new Ketquadoithi();


        //     kq.MaTS = txtmathisinh.Text;
        //     kq.madothi = txtdoithi.Text;
        //     kq.Socaudung = tl.Socaudung1.ToString();
        //     kq.Ketqua = tl.Ketquathi1;

        // }
        private void AddNewTraloi(trailoi tl)
        {


            tbBaitraloi ch = new tbBaitraloi();
            //ch.macauhoi = (txtmacauhoi.Text);
            //ch.maloaicauhoi = (txtloaicauhoi.Text);
            //ch.cauhoi = (txtcauhoi.Text);
            //ch.cau_a = (txtcaua.Text);
            //ch.cau_b = (txtcaub.Text);
            //ch.cau_c = (txtcauc.Text);
            //ch.cau_d = (txtcaud.Text);
            //ch.dapan = (txtdapan.Text);
            ch.MaTS = txtmathisinh.Text;
            ch.madoithi = txtdoithi.Text;
            ch.macauhoi = tl.Macauhoi;
            ch.cautraloi = tl.Cautraloi;
            ch.Chamcau = tl.Traloi;
            int dem = 0;
            if (ch.cautraloi == ch.Chamcau)
            {
                dem++;
                ch.Socaudung = dem.ToString();

            }
            else
            {
                dem--;

                ch.Socaudung = dem.ToString();
            }
            int dem1 = 0;
            if (ch.Socaudung == "1")
            {
                ch.Ketqua = dem1.ToString();
            }
            else
            {
                dem1--;
                ch.Ketqua = dem1.ToString();
            }

            context.tbBaitralois.InsertOnSubmit(ch);
            context.SubmitChanges();
        }
       

        public void nopbaithi()
        {
            int diem = 0;
            trangthai = true;
            //Dap_An_Dung();
            rbA.IsEnabled = false;
            rbB.IsEnabled = false;
            rbC.IsEnabled = false;
            rbD.IsEnabled = false;

            foreach (trailoi item in data)
            {
                //if (item.Cautraloi.Equals(item.Traloi.Trim().ToUpper()))
                if (item.Cautraloi.Equals(item.Traloi.Trim().ToUpper()))
                {
                    diem++;
                   
                 

                }
                AddNewTraloi(item);
              

                //rbA.IsEnabled = false;
                //rbB.IsEnabled = false;
                //rbC.IsEnabled = false;
                //rbD.IsEnabled = false;

                //if (data[index].Traloi.ToString() == "A")
                //{
                //    lbA.Background = Brushes.Red;
                //}
                //if (data[index].Traloi.ToString() == "B")
                //{
                //    lbB.Background = Brushes.Red;
                //}
                //if (data[index].Traloi.ToString() == "C")
                //{
                //    lbC.Background = Brushes.Red;
                //}
                //if (data[index].Traloi.ToString() == "D")
                //{
                //    lbD.Background = Brushes.Red;
                //}

            }
           
            MessageBoxResult d_result = MessageBox.Show(" Bạn muốn nộp bài", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (d_result == MessageBoxResult.Yes)

            { 
               
                rbA.IsEnabled = false;
                rbB.IsEnabled = false;
                rbC.IsEnabled = false;
                rbD.IsEnabled = false;
               
                if (diem > 16)
                {

                    MessageBox.Show("ket quả" + diem + "/" + data.Count + "Bạn Đã Đậu !!! ( ^ ^ )");


                }
                else MessageBox.Show("ket quả" + diem + "/" + data.Count + "Bạn Đã Rớt!!! ( @ @ )");







               

            }



        }
      
        //private void Dap_An_Dung(trailoi item)
        //{
        //    if ((item.Traloi.Trim()) =="A")
        //    {
        //        lbA.Foreground = Brushes.Red;
        //    }
        //   if ((item.Traloi.Trim()) == "B")
        //    {
        //        lbB.Foreground = Brushes.Red;
        //    }
        //  if ((item.Traloi.Trim()) == "C")
        //    {
        //        lbC.Foreground = Brushes.Red;
        //        }
        //  if ((item.Traloi.Trim()) == "D")
        //    {
        //        lbD.Foreground = Brushes.Red;
        //    }
        //}










        //public bool KiemTra_DieuKien_NopBai()
        //{
        //    for (int i = 0; i < time-1; i++)
        //    {
        //        if (data[i].Cautraloi.ToString() == "E")
        //        {
        //            MessageBox.Show("Vui lòng chọn tất cả đáp án\nNếu nộp bài sớm hơn");
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        private void bntnopbai(object sender, RoutedEventArgs e)
        {
          nopbaithi();
            bntxemketqua.IsEnabled = true;

       

            if (time > 0)
            {
               
                

                    MessageBox.Show("Thời gian thi vẫn còn , Bạn vẫn muốn nộp bài thi !!! ");
                    btnChoiLaiGDT.IsEnabled = true;
                    
                Timer.Stop();
              

            }
            else
            {
                MessageBox.Show("Hết thời gian thi ");
               
                btnChoiLaiGDT.IsEnabled = true;
              

              
            }

                      

                    
                
           
        }

     

        private void bntxuatexcel01(object sender, RoutedEventArgs e)
        {
            xuatExcel();
        }

     

        //private void bntxemketqua_Click(object sender, RoutedEventArgs e)
        //{
        //    //ToMauCauDung(index);
        //    //resetbox();
        //}

        //private void bntxemdapan(object sender, RoutedEventArgs e)
        //{
        //    if (data[index].Traloi.ToString() == "A")
        //    {
        //        lbA.Background = Brushes.Red;
        //    }
        //    if (data[index].Traloi.ToString() == "B")
        //    {
        //        lbB.Background = Brushes.Red;
        //    }
        //    if (data[index].Traloi.ToString() == "C")
        //    {
        //        lbC.Background = Brushes.Red;
        //    }
        //    if (data[index].Traloi.ToString() == "D")
        //    {
        //        lbD.Background = Brushes.Red;
        //    }

    }





}
