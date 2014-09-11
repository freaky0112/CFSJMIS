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
using CFSJMIS.Collections;
using System.ComponentModel;
using CFSJMIS.ViewModel;

namespace CFSJMIS {
    /// <summary>
    /// CharacterProperty.xaml 的交互逻辑
    /// </summary>
    public partial class CharacterProperty : Window {
        public CharacterProperty(Character _character) {
            InitializeComponent();
            this.character = _character;
            this.DataContext = new CharacterModel(character);
        }

        private Character character;

        private void lblOK_MouseDown(object sender, MouseButtonEventArgs e) {
            ((IEditableObject)DataContext).EndEdit();
            this.DialogResult = true;

        }

        private void lblCancel_MouseDown(object sender, MouseButtonEventArgs e) {
            ((IEditableObject)DataContext).CancelEdit();
            this.DialogResult = false;
        }
    }
}
