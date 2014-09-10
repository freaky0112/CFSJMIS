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
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using CFSJMIS.Security;

namespace CFSJMIS {
    /// <summary>
    /// loginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class loginWindow : Window {
        public loginWindow() {
            InitializeComponent();
            this.Loaded += loginWindow_Loaded;
        }

        void loginWindow_Loaded(object sender, RoutedEventArgs e) {
            try {
                List<String> gtx = Load.gtxRead("GTX");
                this.cbxNames.ItemsSource = gtx;
                cbxNames.SelectedIndex = 0;
            } catch (NotImplementedException ex) {
                throw ex;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e) {
            if ((MessageBox.Show(Messages.EXIT, Messages.EXIT, MessageBoxButton.OKCancel) == MessageBoxResult.OK)) {
                Application.Current.Shutdown();
            } else {
                //this.Close();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e) {
            string name = cbxNames.Text;
            string password =Security.Security.getMD5(pbxPassword.Password);
            if (Common.sshConnected) {

                if (DataOperate.login(name, password)) {
                   MessageBox.Show(Messages.LOGIN_SUCCESS);
                    Common.table = name;
                    this.Close();
                } else {
                  MessageBox.Show(Messages.LOGIN_ERROR);
                }
            } else {
                MessageBox.Show(Messages.SSH_ERROR);
            }
        }

        private void btnSSH_Click(object sender, RoutedEventArgs e) {
            sshConnectWindow ssh = new sshConnectWindow();
            ssh.ShowDialog();
        }




        private void lblLogin_MouseDown(object sender, MouseButtonEventArgs e) {
            string name = cbxNames.Text;
            string password = pbxPassword.Password;
            if (Common.sshConnected) {

                if (DataOperate.login(name, password)) {
                    MessageBox.Show(Messages.LOGIN_SUCCESS);
                    Common.table = name;
                    this.Close();
                } else {
                    MessageBox.Show(Messages.LOGIN_ERROR);
                }
            } else {
                MessageBox.Show(Messages.SSH_ERROR);
            }
        }

        private void lblExit_MouseDown(object sender, MouseButtonEventArgs e) {
            if ((MessageBox.Show(Messages.EXIT, Messages.EXIT, MessageBoxButton.OKCancel) == MessageBoxResult.OK)) {
                Application.Current.Shutdown();
            }
        }

        private void lblLogin_MouseEnter(object sender, MouseEventArgs e) {
            lblLogin.Foreground = Common.AFTER_BRUSH;
        }

        private void lblLogin_MouseLeave(object sender, MouseEventArgs e) {
            lblLogin.Foreground = Common.BEFORE_BRUSH;
        }

        private void lblExit_MouseEnter(object sender, MouseEventArgs e) {
            lblExit.Foreground = Common.AFTER_BRUSH;
        }

        private void lblExit_MouseLeave(object sender, MouseEventArgs e) {
            lblExit.Foreground = Common.BEFORE_BRUSH;
        }
    }
}
