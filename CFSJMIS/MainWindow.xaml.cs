using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using CFSJMIS.Collections;
using CFSJMIS.Biult;
using System.Threading;
using CFSJMIS.Search;

namespace CFSJMIS {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {

        private List<Data> dataList;
        private Thread biult;

        public MainWindow() {
            InitializeComponent();
            if (!SSHConnect.sshConnected(Common.sshServer, Common.sshPort, Common.sshUID, Common.sshPWD)) {
                MessageBox.Show(Messages.SSH_ERROR);
            }
            loginWindow login = new loginWindow();
            login.ShowDialog();
            this.Loaded += MainWindow_Loaded;

        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            List<string> towns = Load.townRead("Town");
            this.cbxTown.ItemsSource = towns;
            this.cbxTown.SelectedIndex = 0;
            this.lblDepartment.Content = Common.table;

            this.lswData.ItemsSource = dataList;
            //ImageBrush b = new ImageBrush();
            //b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CFSJMIS;component/Images/gray-x.png"));
            //b.Stretch = Stretch.Fill;
            //this.borderExit.Background = b;
            //throw new NotImplementedException();
        }


        private void borderExit_MouseDown(object sender, MouseButtonEventArgs e) {

            Application.Current.Shutdown();
        }

        private void borderExit_MouseEnter(object sender, MouseEventArgs e) {
            this.borderExit.Background = Common.EXIT_AFTER_BRUSH;
        }

        private void borderExit_MouseLeave(object sender, MouseEventArgs e) {
            this.borderExit.Background = Common.EXIT_BEFORE_BRUSH;
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e) {
            gridImport.Visibility = Visibility.Hidden;
            gridQuery.Visibility = Visibility.Hidden;

        }

        private void tbxPath_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            tbxPath.Text = Load.getPath();
            if (string.IsNullOrEmpty(tbxPath.Text)) {
                tbxPath.Text = Messages.IMPORT_TIPS;
                tbxPath.Foreground = Brushes.Gray;
            } else {
                tbxPath.Foreground = Brushes.Black;
            }
        }

        private void tagImport_MouseDown(object sender, MouseButtonEventArgs e) {
            if (tagImport.Focus()) {
                tagImport.Foreground = Brushes.Black;
            }
            gridImport.Visibility = Visibility.Visible;
            gridQuery.Visibility = Visibility.Hidden;
            tagImport.Foreground = Brushes.White;
            tagImport.Background = Common.AFTER_BRUSH;
            tagQuery.Background = Brushes.Transparent;
            tagQuery.Foreground = Brushes.Gray;
        }

        private void lblImport_MouseDown(object sender, MouseButtonEventArgs e) {

            foreach (Data data in ExcelOperate.getDataList(tbxPath.Text, cbxTown.Text)) {
                DataOperate.insertData(data);
            }
        }

        private void tagImport_GotFocus(object sender, RoutedEventArgs e) {
            tagImport.Foreground = Brushes.Black;
        }

        private void tagImport_LostFocus(object sender, RoutedEventArgs e) {
            tagImport.Foreground = Brushes.Gray;
        }

        private void lblQuery_MouseDown_1(object sender, MouseButtonEventArgs e) {
            gridImport.Visibility = Visibility.Hidden;
            gridQuery.Visibility = Visibility.Visible;
            tagQuery.Foreground = Brushes.White;
            tagQuery.Background = Common.AFTER_BRUSH;
            tagImport.Background = Brushes.Transparent;
            tagImport.Foreground = Brushes.Gray;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                DragMove();
            }
        }

        private void lblQuery_MouseDown(object sender, MouseButtonEventArgs e) {
            dataList = DataOperate.query();
            this.lswData.ItemsSource = dataList;
        }

        /// <summary>  
        /// 由ChecBox的Click事件修改是否已处罚
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        private void CheckBox_Click(object sender, RoutedEventArgs e) {
            CheckBox cbx = sender as CheckBox;
            string guid = cbx.Tag.ToString();
            try {
                DataOperate.dataSinged(guid, (bool)cbx.IsChecked);
            } catch (Exception ex) {
                throw ex;
            }
        }

        private void lswData_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            Data data = (Data)lswData.SelectedItem;
            if (data != null) {
                MessageBox.Show(data.ID.ToString());
            }
        }

        private void lblBuilt_MouseDown(object sender, MouseButtonEventArgs e) {
            int count = 0;
            prbState.Maximum = lswData.Items.Count;
            prbState.Foreground = Common.AFTER_BRUSH;
            biult = new Thread(() => {
                foreach (Data data in lswData.Items) {
                    string path = System.IO.Directory.GetCurrentDirectory() + @"\" + data.Town + @"\" + data.ID + data.Name + @".doc";
                    WordBiult.Generate(path, data);
                    count++;
                    DelegateProgressBar progressBar = new DelegateProgressBar(prbState);
                    progressBar.output(count);
                    Thread.Sleep(30);
                }
            });
            biult.Start();
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e) {
            this.lswData.ItemsSource = Filter.searchList(dataList, txtFilter.Text);
        }



    }
}
