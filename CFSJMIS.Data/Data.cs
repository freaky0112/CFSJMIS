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

namespace CFSJMIS.Collections
{
    public interface ITransacableObject {
        object ReadPropertyValue(PropertyInfo property);

        void SetPropertyValue(PropertyInfo property, object value);
    }

    public class Data : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

 
        private List<Character> _characters;
        /// <summary>
        /// 人物信息
        /// </summary>
        public List<Character> Characters {
            get {
                return _characters;
            }
            set {
                _characters = value;
                
            }
        }

        public Data() {
            // TODO: Complete member initialization
        }
        /// <summary>
        /// 姓名
        /// </summary>
        private string _name;

        public string Name {
            get {
                return _name;
            }
            set {
                _name = value;
                this.NotifyPropertyChanged("Name");
            }
        }
        /// <summary>
        /// 日期
        /// </summary>
        private int _biuldDate;

        public int BuildDate {
            get {
                return _biuldDate;
            }
            set {
                _biuldDate = value;
                this.NotifyPropertyChanged("BuildDate");
            }
        }
        /// <summary>
        /// 住址
        /// </summary>
        private string _address;

        public string Address {
            get {
                return _address;
            }
            set {
                _address = value;
            }
        }
        /// <summary>
        /// 土地座落
        /// </summary>
        private string _location;

        public string Location {
            get {
                return _location;
            }
            set {
                _location = value;
                this.NotifyPropertyChanged("Location");
            }
        }
        /// <summary>
        /// 户籍人数
        /// </summary>
        private string _accounts;

        public string Accounts {
            get {
                return _accounts;
            }
            set {
                _accounts = value;
                this.NotifyPropertyChanged("Accounts");
            }
        }
        /// <summary>
        /// 土地性质
        /// </summary>
        private string _landOwner;

        public string LandOwner {
            get {
                return _landOwner;
            }
            set {
                _landOwner = value;
                this.NotifyPropertyChanged("LandOwner");
            }
        }

        private string _control;

        public string Control {
            get {
                return _control;
            }
            set {
                _control = value;
                this.NotifyPropertyChanged("Control");
            }
        }
        /// <summary>
        /// 占地面积
        /// </summary>
        private double _area;

        public double Area {
            get {
                return _area;
            }
            set {
                _area = value;
                this.NotifyPropertyChanged("Area");
            }
        }
        /// <summary>
        /// 合法面积
        /// </summary>
        private double _legalArea;

        public double LegalArea {
            get {
                return _legalArea;
            }
            set {
                _legalArea = value;
                this.NotifyPropertyChanged("LegalArea");
            }
        }

        /// <summary>
        /// 违法面积
        /// </summary>
        private double _illegalArea;

        public double IllegaArea {
            get {
                return _illegalArea;
            }
            set {
                _illegalArea = value;
                this.NotifyPropertyChanged("IllegaArea");
            }
        }
        /// <summary>
        /// 违法单价
        /// </summary>
        private double _illegaUnit;

        public double IllegaUnit {
            get {
                return _illegaUnit;
            }
            set {
                _illegaUnit = value;
                this.NotifyPropertyChanged("IllegaUnit");
            }
        }

        /// <summary>
        /// 行为处罚金额
        /// </summary>
        private double _price;

        public double Price {
            get {
                return _price;
            }
            set {
                _price = value;
                this.NotifyPropertyChanged("Price");
            }
        }
        /// <summary>
        /// 是否没收
        /// </summary>
        private bool _isnotConfiscate;

        public bool IsnotConfiscate {
            get {
                return _isnotConfiscate;
            }
            set {
                _isnotConfiscate = value;
                this.NotifyPropertyChanged("IsnotConfiscate");
            }
        }
        /// <summary>
        /// 限额面积
        /// </summary>
        private int _quotaArea;

        public int QuotaArea {
            get {
                return _quotaArea;
            }
            set {
                _quotaArea = value;
            }
        }
        /// <summary>
        /// 没收占地面积
        /// </summary>
        private double _confiscateFloorArea;

        public double ConfiscateFloorArea {
            get {
                //_confiscateFloorArea = ConfiscateArea / Layer;//暂时
                return _confiscateFloorArea;
            }
            set {
                _confiscateFloorArea = value;
                this.NotifyPropertyChanged("ConfiscateFloorArea");
            }
        }

        /// <summary>
        /// 没收面积
        /// </summary>
        private double _confiscateArea;

        public double ConfiscateArea {
            get {
                return _confiscateArea;
            }
            set {
                _confiscateArea = value;
                this.NotifyPropertyChanged("ConfiscateArea");
            }
        }
        /// <summary>
        /// 没收单价
        /// </summary>
        private double _confiscateAreaUnit;

        public double ConfiscateAreaUnit {
            get {
                return _confiscateAreaUnit;
            }
            set {
                _confiscateAreaUnit = value;
                this.NotifyPropertyChanged("ConfiscateAreaUnit");
            }
        }
        //编号
        private int _id;

        public int ID {
            get {
                return _id;
            }
            set {
                _id = value;
            }
        }
        /// <summary>
        /// 没收金额
        /// </summary>
        private double _confiscateAreaPrice;

        public double ConfiscateAreaPrice {
            get {
                return _confiscateAreaPrice;
            }
            set {
                _confiscateAreaPrice = value;
                this.NotifyPropertyChanged("ConfiscateAreaPrice");
            }
        }
        /// <summary>
        /// 户主名
        /// </summary>
        private string[] _names;

        public string[] Names {
            get {
                _names = Name.Split('、');
                return _names;
            }
            set {
                _names = value;
            }
        }

        /// <summary>
        /// 身份证
        /// </summary>
        private string _cardID;

        public string CardID {
            get {
                return _cardID;
            }
            set {
                _cardID = value;
            }
        }

        /// <summary>
        /// 身份证号
        /// </summary>
        private string[] _cardIDs;

        public string[] CardIDs {
            get {
                return CardID.Split('、');
            }
            set {
                _cardIDs = value;
            }
        }





        /// <summary>
        /// 出生日期
        /// </summary>
        DateTime[] _birthDate;

        public DateTime[] BirthDate {
            get {
                _birthDate = new DateTime[CardIDs.Length];
                if (CardIDs != null) {
                    for (int i = 0; i < CardIDs.Length; i++) {
                        string id = CardIDs[i].Replace("\n", "").Trim();
                        if (!string.IsNullOrEmpty(id)) {
                            DateTime birthDate = DateTime.Parse(id.Substring(6, 8).Insert(6, "-").Insert(4, "-"));
                            _birthDate.SetValue(birthDate, i);
                        }
                    }
                }

                return _birthDate;
            }
            set {
                _birthDate = value;
            }
        }


        /// <summary>
        /// 性别
        /// </summary>
        private string[] _sex;

        public string[] Sex {
            get {
                _sex = new string[CardIDs.Length];
                if (CardIDs != null) {
                    for (int i = 0; i < CardIDs.Length; i++) {
                        string id = CardIDs[i].Replace("\n", "").Trim();
                        if (id.Length == 18) {
                            string sex = "";
                            if (Int32.Parse(id.Substring(16, 1)) % 2 == 0) {
                                sex = "女";
                            } else {
                                sex = "男";
                            }
                            _sex[i] = sex;
                        }
                    }
                }



                return _sex;
            }
            set {

                _sex = value;

            }
        }
        /// <summary>
        /// 所在乡镇
        /// </summary>
        private string _town;

        public string Town {
            get {
                return _town;
            }
            set {
                _town = value;
                this.NotifyPropertyChanged("Town");
            }
        }

        /// <summary>
        /// GUID
        /// </summary>
        private string _guid;

        public string Guid {
            get {
                return _guid;
            }
            set {
                _guid = value;
            }
        }
        /// <summary>
        /// 编号编码
        /// </summary>
        private string _code;

        public string Code {
            get {
                return _code;
            }
            set {
                _code = value;
            }
        }
        /// <summary>
        /// 没收编号
        /// </summary>
        private int? _confiscateID;

        public int? ConfiscateID {
            get {
                return _confiscateID;
            }
            set {
                _confiscateID = value;
            }
        }
        //实际层数
        private double _layer;

        public double Layer {
            get {
                return _layer;
            }
            set {
                _layer = value;
                this.NotifyPropertyChanged("Layer");
            }
        }
        /// <summary>
        /// 一户一宅
        /// </summary>
        private string _oneToOne;

        public string OneToOne {
            get {
                return _oneToOne;
            }
            set {
                _oneToOne = value;
            }
        }

        private string _available;
        /// <summary>
        /// 建房资格
        /// </summary>
        public string Available {
            get {
                return _available;
            }
            set {
                _available = value;
            }
        }
        /// <summary>
        /// 土地利用总体规划
        /// </summary>
        private string _conform;

        public string Conform {
            get {
                return _conform;
            }
            set {
                _conform = value;
            }
        }

        private double? _farmArea;
        /// <summary>
        /// 耕地面积
        /// </summary>
        public double? FarmArea {
            get {
                return _farmArea;
            }
            set {
                _farmArea = value;
                this.NotifyPropertyChanged("FarmArea");
            }
        }

        private double? _farmUnit;
        /// <summary>
        /// 耕地单价
        /// </summary>
        public double? FarmUnit {
            get {
                return _farmUnit;
            }
            set {
                _farmUnit = value;
                this.NotifyPropertyChanged("FarmUnit");
            }
        }

        private Boolean _signed;
        /// <summary>
        /// 是否已处罚
        /// </summary>
        public Boolean Signed {
            get {
                return _signed;
            }
            set {
                _signed = value;
                this.NotifyPropertyChanged("Signed");
            }
        }
        private string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark {
            get {
                return _remark;
            }
            set {
                _remark = value;
                this.NotifyPropertyChanged("Remark");
            }
        }

        private string _upperEight;

        public string UpperEight {
            get {
                return _upperEight;
            }
            set {
                _upperEight = value;
                this.NotifyPropertyChanged("UpperEight");
            }
        }
    }
    /// <summary>
    /// 人物
    /// </summary>
    public class Character : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public Character() {
        }

        private string _Name;
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
                this.NotifyPropertyChanged("Name");
            }
        }

        private string _sex;
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex {
            get {
                return _sex;
            }
            set {
                _sex = value;
                this.NotifyPropertyChanged("Sex");
            }
        }

        private string _ethnic;
        /// <summary>
        /// 民族
        /// </summary>
        public string Ethnic {
            get {
                return _ethnic;
            }
            set {
                _ethnic = value;
                this.NotifyPropertyChanged("Ethnic");
            }
        }

        private string _cardID;
        /// <summary>
        /// 证件号
        /// </summary>
        public string CardID {
            get {
                return _cardID;
            }
            set {
                _cardID = value;
                this.NotifyPropertyChanged("CardID");
            }
        }
    }
}
