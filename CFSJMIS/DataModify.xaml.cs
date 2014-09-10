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
using System.Runtime.InteropServices;
using System.ComponentModel;
using CFSJMIS.ViewModel;

namespace CFSJMIS {
    /// <summary>
    /// DataModify.xaml 的交互逻辑
    /// </summary>
    public partial class DataModify : Window {
        public DataModify(Data _data) {
            InitializeComponent();
            this.data = _data;
            this.Loaded += DataModify_Loaded;
        }


        
        void DataModify_Loaded(object sender, RoutedEventArgs e) {            
            List<string> controls = Enum.GetNames(typeof(Common.Contorls)).ToList<string>();
            cbxControls.ItemsSource = controls;
            List<string> landOwner = Enum.GetNames(typeof(Common.LandOwner)).ToList<string>();

            this.lswCharacter.ItemsSource = data.Characters;
            this.cbxLandOwner.ItemsSource = landOwner;
            //this.DataContext = new DataModel(data);
            base.DataContext = data;
        }

        private Data data;

        private void lblCancel_MouseDown(object sender, MouseButtonEventArgs e) {
            //((IEditableObject)DataContext).CancelEdit();
            this.DialogResult = false;
            this.Close();
        }

        private void lblModify_MouseDown(object sender, MouseButtonEventArgs e) {
            try {
                //((IEditableObject)DataContext).EndEdit();
                data = DataOperate.modifyData(data);
                //data = (Data)this.DataContext;
                this.DialogResult = true;
            } catch(Exception ex) {
                MessageBox.Show(Messages.MODIFY_ERROR);
                throw ex;
            }
            this.Close();
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e) {
            ComboBox cbx = (ComboBox)sender;
            cbx.ItemsSource = Load.ethnicRead();
            cbx.DataContext = data.Characters;
            
        }

        private void cbxSex_Loaded(object sender, RoutedEventArgs e) {
            ComboBox cbx = (ComboBox)sender;
            List<string> sex = Enum.GetNames(typeof(Common.Sex)).ToList<string>();
            cbx.ItemsSource = sex;
            cbx.DataContext = data.Characters;

        }

        private void lswCharacter_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            Character charcter = (Character)lswCharacter.SelectedItem;
            if (charcter != null) {
                CharacterProperty cp = new CharacterProperty(charcter);
                if (cp.ShowDialog() == true) {
                    lswCharacter.ItemsSource = data.Characters;
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
            data = AreaCalculate.getPrice(data);
        }
    }
}
