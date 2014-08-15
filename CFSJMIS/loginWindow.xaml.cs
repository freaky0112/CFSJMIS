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

namespace CFSJMIS {
    /// <summary>
    /// loginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class loginWindow : Window {
        public loginWindow() {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e) {
            if ((MessageBox.Show(Messages.EXIT, Messages.EXIT, MessageBoxButton.OKCancel) == MessageBoxResult.OK)) {
                Application.Current.Shutdown();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e) {
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

        private void btnSSH_Click(object sender, RoutedEventArgs e) {
            sshConnectWindow ssh = new sshConnectWindow();
            ssh.ShowDialog();
        }
    }
}
