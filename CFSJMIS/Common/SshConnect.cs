using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Renci.SshNet;
using System.Windows;
namespace CFSJMIS {
    public abstract class SSHConnect {
        /// <summary>
        /// SSH连接端口转发
        /// </summary>
        /// <param name="server">服务器地址</param>
        /// <param name="port">服务器端口</param>
        /// <param name="uid">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public static bool sshConnected(string server, int port, string uid, string pwd) {
            bool sshState = false;
            var client = new SshClient(server, port, uid, pwd);
            try {
                client.Connect();
                Common.sshConnected = client.IsConnected;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return sshState;
            }
            var porcik = new ForwardedPortLocal("localhost", 3306, "localhost", 3306);
            try {
                client.AddForwardedPort(porcik);
                porcik.Start();
                sshState = true;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return sshState;
            }
            return sshState;
        }
    }
}
