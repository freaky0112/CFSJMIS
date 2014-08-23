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


namespace CFSJMIS {
    /// <summary>
    /// sshConnectWindow.xaml 的交互逻辑
    /// </summary>
    public partial class sshConnectWindow : Window {
        public sshConnectWindow() {
            InitializeComponent();
            tbServer.Text = Common.sshServer;
            tbPort.Text = Common.sshPort.ToString();
        }

        private void btnTestConnect_Click(object sender, RoutedEventArgs e) {
            string server = tbServer.Text;
            string port = tbPort.Text;
            if (Common.isIP(server) && Common.isPort(port)) {
                try {

                    Common.sshServer = server;
                    Common.sshPort = Int32.Parse(port);
                    if (SSHConnect.sshConnected(Common.sshServer, Common.sshPort, Common.sshUID, Common.sshPWD)) {
                        MessageBox.Show(Messages.SSHCONNECTED);
                        this.Close();
                    } else {

                    }
                } catch (Exception ex) {
                    throw ex;
                }
            } else {
                if (!Common.isIP(server)) {
                    MessageBox.Show(Messages.SERVER_ERROR);
                }
                if (!Common.isPort(port)) {
                    MessageBox.Show(Messages.PORT_ERROR);
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
