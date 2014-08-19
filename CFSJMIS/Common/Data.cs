using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace CFSJMIS{

    public class Data : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }  
        public Data() {
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
            }
        }

        private string _control;

        public string Control {
            get {
                return _control;
            }
            set {
                _control = value;
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
        /// 没收价格
        /// </summary>
        private double _confiscateAreaPrice;

        public double ConfiscateAreaPrice {
            get {
                return _confiscateAreaPrice;
            }
            set {
                _confiscateAreaPrice = value;
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
                _birthDate = new DateTime[_cardIDs.Length];
                if (_cardIDs != null) {
                    for (int i = 0; i < _cardIDs.Length; i++) {
                        string id = _cardIDs[i].Replace("\n", "").Trim();
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
                _sex = new string[_cardIDs.Length];
                if (_cardIDs != null) {
                    for (int i = 0; i < _cardIDs.Length; i++) {
                        string id = _cardIDs[i].Replace("\n", "").Trim();
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
    }
}
