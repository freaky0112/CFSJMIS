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
using System.Collections;
using CFSJMIS.Collections;
using System.Text.RegularExpressions;

namespace CFSJMIS.Search {
    public class Filter {
        /// <summary>
        /// 重复项查找
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public static List<Data> duplicateList(List<Data> dataList) {
            Hashtable unduplicateList=new Hashtable();
            List<Data> duplicateList = new List<Data>();
            foreach (Data data in dataList) {
                foreach (Character character in data.Characters) {
                    string hash = character.Name + character.CardID;
                    if (unduplicateList.ContainsKey(hash)) {
                        duplicateList.Add(data);
                        duplicateList.Add((Data)unduplicateList[hash]);
                    } else {
                        unduplicateList.Add(hash, data);
                    }
                }
            }
            return duplicateList;
        }


        /// <summary>
        /// 筛选
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public static List<Data> searchList(List<Data> dataList, string keywords) {
            //List<Data> datas = new List<Data>();
            //IEnumerable<Data> datas = from data in dataList
            //                          where data.ID.ToString().Contains(keyword)
            //                          || data.Name.Contains(keyword)
            //                          || data.Location.Contains(keyword)
            //                          || data.Town.Contains(keyword)
            //                          select data;
            foreach (string keyword in keywords.Split(' ')) {

                IEnumerable<Data> datas = containKeyWord(dataList, keyword);

                dataList = new List<Data>();
                foreach (Data data in datas) {
                    dataList.Add(data);
                }
            }
            return dataList;
        }

        private static IEnumerable<Data> containKeyWord(List<Data> dataList, string keyword) {
            IEnumerable<Data> datas;
            string date;
            if (keyword.Contains('-')) {
                string[] ids = keyword.Split('-');
                if (Regex.Matches(keyword, "-").Count == 1) {
                    string start = ids[0];
                    string end = ids[1];
                    int startNo = 0;
                    int endNo = 0;
                    if (string.IsNullOrEmpty(start)) {
                        startNo = 0;
                    } else if (IsNumber(start)) {
                        startNo = Int32.Parse(start);
                    }
                    if (string.IsNullOrEmpty(end)) {
                        endNo = int.MaxValue;
                    } else if (IsNumber(end)) {
                        endNo = Int32.Parse(end);
                    }
                    datas = from data in dataList
                            where data.ID >= startNo
                            && data.ID <= endNo
                            select data;
                    return datas;
                }
            } else if (keyword.Contains('>')) {
                date = keyword.Replace(">", "");
                if (date.Length == 6 && IsNumber(date)) {
                    datas = from data in dataList
                            where data.BuildDate > Int32.Parse(date)
                            select data;
                    return datas;
                }

            } else if (keyword.Contains('<')) {

                date = keyword.Replace("<", "");
                if (date.Length == 6 && IsNumber(date)) {
                    datas = from data in dataList
                            where data.BuildDate < Int32.Parse(date)
                            select data;
                    return datas;
                }
            }
            datas = from data in dataList
                    where data.ID.ToString().Contains(keyword)
                    || data.Name.Contains(keyword)
                    || data.Location.Contains(keyword)
                    || data.Town.Contains(keyword)
                    || data.PunishDate.ToString().Contains(keyword)
                    select data;
            return datas;


        }

        public static bool IsNumber(string strNumber) {
            //看要用哪種規則判斷，自行修改strValue即可

            //strValue = @"^\d+[.]?\d*$";//非負數字
            string strValue = @"^\d+(\.)?\d*$";//數字
            //strValue = @"^\d+$";//非負整數
            //strValue = @"^-?\d+$";//整數
            //strValue = @"^-[0-9]*[1-9][0-9]*$";//負整數
            //strValue = @"^[0-9]*[1-9][0-9]*$";//正整數
            //strValue = @"^((-\d+)|(0+))$";//非正整數

            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(strValue);
            return r.IsMatch(strNumber);
        }
    }
}
