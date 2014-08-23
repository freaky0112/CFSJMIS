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
    class Index {
        private static string pText = "";// 文本信息   
        private static int pFontSize = 24;//字体大小  
        private static string pFontName = "宋体";
        private static Microsoft.Office.Interop.Word.WdColor pFontColor = Microsoft.Office.Interop.Word.WdColor.wdColorBlack;//字体颜色 
        private static Microsoft.Office.Interop.Word.WdUnderline pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;//下划线
        private static int pFontBold = 0;//字体粗体  
        static Microsoft.Office.Interop.Word.WdParagraphAlignment ptextAlignment = 0;//方向 

        public static void biult(BiultReportForm brf, Data data) {

            addTitle(brf, data);
            brf.NewPage();
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
            pText = "";
            pFontSize = 42;
            ptextAlignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            addLine(brf);
            addLine(brf);
            pText = "青田县国土资源局";

            pFontBold = 1;//设置粗体
            addLine(brf);
            pFontSize = 24;
            pText = "青土资告字〔2014〕第" + data.Code + String.Format("{0:0000}", data.ID) + "号";
            addLine(brf);
            pText = "青土资罚〔2014〕" + data.Code + String.Format("{0:0000}", data.ID) + "号";
            addLine(brf);
            pText = data.Name;
            addLine(brf);
        }
    }
    
}
