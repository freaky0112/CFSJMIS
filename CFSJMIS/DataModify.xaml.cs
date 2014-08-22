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
            grid.DataContext = data;
            //base.DataContext = data;
        }

        private Data data;

        private void lblCancel_MouseDown(object sender, MouseButtonEventArgs e) {
            this.DialogResult = false;
            this.Close();
        }

        private void lblModify_MouseDown(object sender, MouseButtonEventArgs e) {
            try {
                data = DataOperate.modifyData(data);
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
    }
}
