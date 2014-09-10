//
//                       _oo0oo_
//                      o8888888o
//                      88" . "88
//                      (| -_- |)
//                      0\  =  /0
//                    ___/`---'\___
//                  .' \\|     |// '.
//                 / \\|||  :  |||// \
//                / _||||| -:- |||||- \
//               |   | \\\  -  /// |   |
//               | \_|  ''\---/''  |_/ |
//               \  .-\__  '-'  ___/-. /
//             ___'. .'  /--.--\  `. .'___
//          ."" '<  `.___\_<|>_/___.' >' "".
//         | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//         \  \ `_.   \_ __\ /__ _/   .-` /  /
//     =====`-.____`.___ \_____/___.-`___.-'=====
//                       `=---='
//
//
//     ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//
//               佛祖保佑         永无BUG
//
//
//

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
            if (Load.configRead()) {
                InitializeComponent();
                if (!SSHConnect.sshConnected(Common.sshServer, Common.sshPort, Common.sshUID, Common.sshPWD)) {
                    MessageBox.Show(Messages.SSH_ERROR);
                }
                loginWindow login = new loginWindow();
                login.ShowDialog();
                this.Loaded += MainWindow_Loaded;
            } else {
                MessageBox.Show(Messages.CONFIG_LOAD_ERROR);
                Application.Current.Shutdown();
            }

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
            this.lswData.ItemsSource = Filter.searchList(dataList, txtFilter.Text);
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
                DataOperate.singedData(guid, (bool)cbx.IsChecked);
            } catch (Exception ex) {
                throw ex;
            }
        }

        

        private void lswData_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            Data data = (Data)lswData.SelectedItem;
            if (data != null) {
                DataModify dm = new DataModify(data);
                if (dm.ShowDialog() == true) {
                    try {
                        //lswData.ItemsSource = Filter.searchList(dataList, txtFilter.Text);
                        dataList = DataOperate.query();
                        this.lswData.ItemsSource = Filter.searchList(dataList, txtFilter.Text);
                    }catch(Exception ex){
                        throw ex;
                    }
                } else {
                    dataList = DataOperate.query();
                    this.lswData.ItemsSource = Filter.searchList(dataList, txtFilter.Text);
                }
            }
        }
        /// <summary>
        /// 生成WORD文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblBuilt_MouseDown(object sender, MouseButtonEventArgs e) {
            #region 单线程生成
            int count = 0;
            prbState.Maximum = lswData.Items.Count;
            prbState.Foreground = Common.AFTER_BRUSH;
            biult = new Thread(() => {
                foreach (Data data in lswData.Items) {
                    data.Code = Common.getCode();
                    string path = System.IO.Directory.GetCurrentDirectory() + @"\" + data.Town + @"\" + data.ID + data.Name + @".doc";
                    WordBiult.Generate(path, data);
                    count++;
                    DelegateProgressBar progressBar = new DelegateProgressBar(prbState);
                    progressBar.output(count);///控制进度条
                    //Thread.Sleep(30);
                }
                MessageBox.Show(Messages.BUILT_COMPLETE);
            });
            biult.Start();
            #endregion

            #region 多线程生成
            //int maxThread = 10;
            //int currentNum = 0;
            //prbState.Maximum = lswData.Items.Count;
            //prbState.Foreground = Common.AFTER_BRUSH;
            //WaitHandle[] whs = new WaitHandle[maxThread];
            //for (int i = 0; i < whs.Length; i++) {
            //    whs[i] = new AutoResetEvent(false);
            //}
            //int sortIdx = 0;
            //for (int i = 0; i < lswData.Items.Count; i++) {
            //    var ex = i;
            //    currentNum++;
            //    if (currentNum > maxThread) {
            //        int freeInx = WaitHandle.WaitAny(whs);
            //        currentNum--;
            //        sortIdx = freeInx;
            //    } else {
            //        sortIdx = currentNum - 1;
            //    }
            //    ThreadPool.QueueUserWorkItem(new WaitCallback((p) => {
            //        Data data = (Data)lswData.Items[i];
            //        data.Code = Common.getCode();
            //        string path = System.IO.Directory.GetCurrentDirectory() + @"\" + data.Town + @"\" + data.ID + data.Name + @".doc";
            //        WordBiult.Generate(path, data);
            //        DelegateProgressBar progressBar = new DelegateProgressBar(prbState);
            //        progressBar.output(i);///控制进度条
            //        (whs[sortIdx] as AutoResetEvent).Set();
            //    }), ex);
            //}
            #endregion
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e) {
            if (dataList != null) {

                this.lswData.ItemsSource = Filter.searchList(dataList, txtFilter.Text);
            }
        }
        /// <summary>
        /// 删除选中行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e) {
            Button button = sender as Button;
            foreach (Data data in dataList) {
                if (button.Tag.Equals(data.Guid)) {
                    dataList.Remove(data);
                    this.lswData.ItemsSource = Filter.searchList(dataList, txtFilter.Text);
                    if (!DataOperate.deleteData(data)) {
                        MessageBox.Show("删除失败");
                    }
                    
                    break;
                }
            }           

        }
        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblExport_MouseDown(object sender, MouseButtonEventArgs e) {
            string savePath=Load.getSavePath();
            List<Data> list=new List<Data>();
            if (lswData.ItemsSource != null) {
                foreach (Data data in lswData.ItemsSource) {
                    list.Add(data);
                }
                if (!string.IsNullOrEmpty(savePath)) {
                    ExcelOperate.exportDataToExcel(savePath, list);
                    MessageBox.Show("生成完成");
                    System.Diagnostics.Process.Start(savePath);
                }
            }
        }




    }
}
