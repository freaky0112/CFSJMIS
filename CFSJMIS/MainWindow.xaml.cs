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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CFSJMIS {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            //if (!SSHConnect.sshConnected(Common.sshServer, Common.sshPort, Common.sshUID, Common.sshPWD)) {
            //    MessageBox.Show(Messages.SSH_ERROR);
            //}
            loginWindow login = new loginWindow();
            login.ShowDialog();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            List<string> towns = Load.townRead("Town");
            this.cbxTown.ItemsSource = towns;
            //throw new NotImplementedException();
        }
    }
}
