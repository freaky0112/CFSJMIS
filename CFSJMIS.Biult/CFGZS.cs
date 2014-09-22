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
    class CFGZS {
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
        /// 输入处罚告知书
        /// </summary>
        /// <param name="brf"></param>
        public static void biult(BiultReportForm brf, Data  data) {
            brf.NewPage();
            addTitle(brf, data);
            addText(brf, data);
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
            pFontName = "宋体";
            pText = "青田县国土资源局";
            pFontSize = 24;
            ptextAlignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            pFontBold = 1;//设置粗体
            addLine(brf);
            pText = "行 政 处 罚 告 知 书";
            addLine(brf);
            pFontSize = 12;
            pFontBold = 0;//设置细体
            pText = "青土资告字〔2014〕第" + data.Code + String.Format("{0:0000}", data.ID) + "号";
            addLine(brf);
            pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle;
            pText = "                                                       ";
            pFontSize = 15;
            ptextAlignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphJustifyHi;
            brf.SetLineSpacing(4f, Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceExactly);
            addLine(brf);
        }
        /// <summary>
        /// 输入正文
        /// </summary>
        /// <param name="brf"></param>
        /// <param name="data"></param>
        private static void addText(BiultReportForm brf, Data data) {
            brf.SetLineSpacing(28f, Microsoft.Office.Interop.Word.WdLineSpacing.wdLineSpaceExactly);
            pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
            pFontName = "仿宋_GB2312";
            pText = "";
            foreach (string name in data.Names) {
                pText += name + "、";
            }
            pText = pText.TrimEnd('、');
            pText += ":";
            pFontSize = 15;
            ptextAlignment = 0;
            addLine(brf);
            pText = "    经查明，你未经批准于";
            pText += data.BuildDate.ToString().Substring(0, 4);
            pText += "年";
            pText += data.BuildDate.ToString().Substring(4, 2);
            pText += "月擅自在青田县";
            pText += data.Town;
            pText += data.Location;
            pText += "非法占用土地";
            pText += Math.Round(data.IllegaArea, 2);
            pText += "平方米";
            if (data.FarmArea > 0) {
                pText += "，其中耕地面积";
                pText += data.FarmArea;
                pText += "平方米";
            }
            if (data.ConfiscateAreaPrice > 0) {
                if (data.BuildDate >= 201304 && !data.Control.Equals("四级")) {
                    pText += "，并在该土地上建造建筑物（房屋），非法建筑物面积为";
                    pText += data.ConfiscateArea;
                    pText += "平方米。经核对青田县";
                } else {
                    pText += "（其中超土地审批限额";
                    pText += data.ConfiscateFloorArea;
                    pText += "平方米），并在该土地上建造建筑物（房屋），其中超出审批限额占用的土地上的建筑物面积为";
                    pText += data.ConfiscateArea;
                    pText += "平方米。经核对青田县";
                }            
            } else {
                pText += "，用于建房。经核对青田县";
            }
            pText += data.Town;
            pText += "土地利用总体规划，该地块符合土地利用总体规划。以上事实有调查摸底登记表、违法建筑照片、违法建筑处置公示清单等证据证实。其行为违反了《中华人民共和国土地管理法》、《浙江省实施〈中华人民共和国土地管理法〉办法》等法律法规有关规定。依照《中华人民共和国土地管理法》、《青田县人民政府关于印发青田县实施〈浙江省违法建筑处置规定〉细则（暂行）》（青政发〔2014〕62号）";
            if (data.BuildDate >= 201304) {
                pText += "、《青田县人民政府关于印发青田县实施浙江省违法建筑处置规定细则（暂行）的补充意见的通知》（青政发〔2014〕101号）";
            }
            pText+="有关规定，拟对你的违法行为作如下行政处罚：";
            pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
            addLine(brf);
            pText = "";
            addTxt(brf);
            //如果没收
            if (data.ConfiscateAreaPrice > 0) {
                if (data.BuildDate >= 201304 && !data.Control.Equals("四级")) {
                    pText = "    1.没收你户非法占用" + Math.Round(data.IllegaArea, 2);
                } else {
                    pText = "    1.没收你户超出审批限额占用的" + data.ConfiscateFloorArea.ToString();
                }
                pText+= "平方米的土地上的建筑物，建筑面积为" + data.ConfiscateArea.ToString() + "平方米。";
                addLine(brf);
                pText = "    2.对你户非法占用土地" + Math.Round(data.IllegaArea, 2) + "平方米的行为处以罚款，";
                addTxt(brf);
            } else {
                pText = "    对你户非法占用土地的行为处以罚款，";
                addTxt(brf);
            }

            pText = "罚款额为";
            if (data.FarmArea > 0) {
                pText += "非法占用耕地每平方米";
                pText += data.FarmUnit.ToString();
                pText += "元，";
            }
            pText += "非法占用其他土地每平方米";
            pText += data.IllegaUnit.ToString();
            pText += "元，合计人民币" + ecanNum.CmycurD(data.Price.ToString()) + "（￥" + Math.Round(data.Price, 2) + ")。";
            addLine(brf);
            pText = "    根据《中华人民共和国行政处罚法》第三十二条之规定，你享有陈述、申辩";
            if (data.ConfiscateAreaPrice + data.Price > 50000) {
                pText += "、要求听证";
            }

            pText += "的权利，可在接到本告知书之日起三日内向我局提出，逾期不提出的，视为放弃陈述、申辩";
            //处罚总金额大于5W有听证
            if (data.ConfiscateAreaPrice + data.Price > 50000) {
                pText += "、要求听证的权利。";
            } else {
                pText += "的权利。";
            }
            addLine(brf);
            //pText = "";
            //addLine(brf);
            pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
            //ptextAlignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
            pText = "                              2014年   月   日";
            addLine(brf);
        }
    }
}
