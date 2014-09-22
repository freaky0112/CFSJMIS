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
using CFSJMIS.Collections;

namespace CFSJMIS.Biult {
    class JKTZ {
        /// <summary>
        /// 初始化应用对象
        /// </summary>
        private static string pText = "";// 文本信息   
        private static int pFontSize = 24;//字体大小  
        private static string pFontName = "宋体";
        private static Microsoft.Office.Interop.Word.WdColor pFontColor = Microsoft.Office.Interop.Word.WdColor.wdColorBlack;//字体颜色 
        private static Microsoft.Office.Interop.Word.WdUnderline pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;//下划线
        private static int pFontBold = 0;//字体粗体  
        private static Microsoft.Office.Interop.Word.WdParagraphAlignment ptextAlignment = 0;//方向 

        private static EcanNum ecanNum = new EcanNum();//数字大写转换
        /// <summary>
        /// 输入交款通知单
        /// </summary>
        /// <param name="brf"></param>
        public static void biult(BiultReportForm brf, Data data) {
            brf.NewSection();
            brf.SetPageFooter(data.Town);
            addTitle(brf, data);
            addText(brf, data);
            //brf.NewPage();
        }

        /// <summary>
        /// 输入并插入下一行
        /// </summary>
        private static void addLine(BiultReportForm brf) {
            brf.InsertText(pText, pFontSize, pFontName, pFontColor, pFontUnderline, pFontBold, ptextAlignment);
            brf.NewLine();
        }
        /// <summary>
        /// 插入文字
        /// </summary>
        /// <param name="brf"></param>
        private static void addTxt(BiultReportForm brf) {
            brf.InsertText(pText, pFontSize, pFontName, pFontColor, pFontUnderline, pFontBold, ptextAlignment);
        }

        /// <summary>
        /// 标题
        /// </summary>
        /// <param name="brf"></param>    
        private static void addTitle(BiultReportForm brf, Data data) {
            if (GetOffice.isNotNewOffice()) {
                brf.TypeBackspace();
            }
            pText = "";
            pFontName = "宋体";
            pFontSize = 22;
            pFontBold = 1;
            ptextAlignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            addLine(brf);
            pText = "罚没交款通知单";
            addLine(brf);
        }

        /// <summary>
        /// 输入正文
        /// </summary>
        /// <param name="brf"></param>
        /// <param name="data"></param>
        private static void addText(BiultReportForm brf, Data data) {
            ptextAlignment = 0;
            pFontBold = 0;
            pText = "";
            pFontName = "仿宋_GB2312";
            pFontSize = 16;
            pText = "执收单位：青田县国土资源监察大队";
            addLine(brf);
            pText = "开户银行：";
            addLine(brf);
            pText = "账号：";
            addLine(brf);
            pText = "户名：";
            addLine(brf);
            pText = "交款地点：";
            addLine(brf);
            pText = "";
            addLine(brf);
            addLine(brf);
            pText = "交款单位（或个人）：" + data.Name;
            addLine(brf);
            pText = "金额：￥" + Math.Round((data.Price + data.ConfiscateAreaPrice), 2).ToString();
            addLine(brf);
            pText = "（大写）  " + ecanNum.CmycurD((data.Price + data.ConfiscateAreaPrice).ToString());
            addLine(brf);
            pText = "";
            addLine(brf);
            addLine(brf);
            addLine(brf);
            addLine(brf);
            pText = "                            青田县国土资源局";
            addLine(brf);
            pText = "                            2014年  月  日";
            addLine(brf);
        }
    }
}
