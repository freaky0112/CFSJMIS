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
using System.ComponentModel;
using System.Reflection;

namespace CFSJMIS.Collections {

    public class BiultSetting : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }


        string _unit;
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit {
            get {
                return _unit;
            }
            set {
                _unit = value;
                this.NotifyPropertyChanged("Unit");
            }
        }

        string _bank;
        /// <summary>
        /// 开户银行
        /// </summary>
        public string Bank {
            get {
                return _bank;
            }
            set {
                _bank = value;
                this.NotifyPropertyChanged("Bank");
            }
        }

        string _account;
        /// <summary>
        /// 账号
        /// </summary>
        public string Account {
            get {
                return _account;
            }
            set {
                _account = value;
                this.NotifyPropertyChanged("Account");
            }
        }

        string _userName;
        /// <summary>
        /// 户名
        /// </summary>
        public string UserName {
            get {
                return _userName;
            }
            set {
                _userName = value;
                this.NotifyPropertyChanged("UserName");
            }
        }

        string _address;
        /// <summary>
        /// 交款地点
        /// </summary>
        public string Address {
            get {
                return _address;
            }
            set {
                _address = value;
                this.NotifyPropertyChanged("Address");
            }
        }
    }
}
