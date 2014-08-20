using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office;
using Word = Microsoft.Office.Interop.Word;

namespace CFSJMIS.Biult
{       
    public class BiultReportForm {/// <SUMMARY>WORD生成</SUMMARY>   
        /// word 应用对象   
        ///    
        private Microsoft.Office.Interop.Word.Application _wordApplication;

        /// <SUMMARY></SUMMARY>   
        /// word 文件对象   
        ///    
        private Microsoft.Office.Interop.Word.Document _wordDocument;
        /// <SUMMARY></SUMMARY>   
        /// 创建文档   
        ///    
        public void CreateAWord() {
            //实例化word应用对象   
            this._wordApplication = new Microsoft.Office.Interop.Word.ApplicationClass();
            Object myNothing = System.Reflection.Missing.Value;

            this._wordDocument = this._wordApplication.Documents.Add(ref myNothing, ref myNothing, ref myNothing, ref myNothing);
        }
        /// <SUMMARY></SUMMARY>   
        /// 添加页眉   
        ///    
        /// <PARAM name="pPageHeader" />   
        public void SetPageHeader(string pPageHeader) {
            //添加页眉   
            this._wordApplication.ActiveWindow.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdOutlineView;
            this._wordApplication.ActiveWindow.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekPrimaryHeader;
            this._wordApplication.ActiveWindow.ActivePane.Selection.InsertAfter(pPageHeader);
            //设置中间对齐   
            this._wordApplication.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            //跳出页眉设置   
            this._wordApplication.ActiveWindow.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekMainDocument;
        }
        /// <SUMMARY></SUMMARY>   
        /// 插入文字   
        ///    
        /// <PARAM name="pText" />文本信息   
        /// <PARAM name="pFontSize" />字体大小   
        /// <PARAM name="pFontName" />字体颜色     
        /// <PARAM name="pFontColor" />字体颜色  
        /// <PARAM name="pFontUnderline" />下划线  
        /// <PARAM name="pFontBold" />字体粗体    
        /// <PARAM name="ptextAlignment" />方向
        public void InsertText(string pText, int pFontSize, string pFontName, Microsoft.Office.Interop.Word.WdColor pFontColor, Microsoft.Office.Interop.Word.WdUnderline pFontUnderline,
            int pFontBold, Microsoft.Office.Interop.Word.WdParagraphAlignment ptextAlignment) {
            //设置字体样式以及方向   
            this._wordApplication.Application.Selection.Font.Size = pFontSize;
            this._wordApplication.Application.Selection.Font.Bold = pFontBold;
            this._wordApplication.Application.Selection.Font.Name = pFontName;
            this._wordApplication.Application.Selection.Font.Color = pFontColor;
            this._wordApplication.Application.Selection.Font.Underline = pFontUnderline;
            this._wordApplication.Application.Selection.ParagraphFormat.Alignment = ptextAlignment;
            this._wordApplication.Application.Selection.TypeText(pText);
        }
        /// <summary>
        /// 退格
        /// </summary>
        public void TypeBackspace() {
            this._wordApplication.Application.Selection.TypeBackspace();
        }

        /// <SUMMARY></SUMMARY>   
        /// 换行   
        ///    
        public void NewLine() {
            //换行   
            this._wordApplication.Application.Selection.TypeParagraph();
        }
        /// <summary>
        /// 设置行间距
        /// </summary>
        public void SetLineSpacing(float spacing, Microsoft.Office.Interop.Word.WdLineSpacing lineSpacing) {
            this._wordApplication.Application.Selection.ParagraphFormat.LineSpacingRule = lineSpacing;
            this._wordApplication.Application.Selection.ParagraphFormat.LineSpacing = spacing;
        }
        /// <summary>
        /// 分页
        /// </summary>
        public void NewPage() {
            //分页
            object myPageBreak = Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak;
            this._wordApplication.Application.Selection.InsertBreak(ref myPageBreak);
        }
        /// <SUMMARY></SUMMARY>   
        /// 插入一个图片   
        ///    
        /// <PARAM name="pPictureFileName" />   
        public void InsertPicture(string pPictureFileName) {
            object myNothing = System.Reflection.Missing.Value;
            //图片居中显示   
            this._wordApplication.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            this._wordApplication.Application.Selection.InlineShapes.AddPicture(pPictureFileName, ref myNothing, ref myNothing, ref myNothing);
        }
        /// <SUMMARY></SUMMARY>   
        /// 保存文件    
        ///    
        /// <PARAM name="pFileName" />保存的文件名   
        public void SaveWord(string pFileName) {
            object myNothing = System.Reflection.Missing.Value;
            object myFileName = pFileName;
            object myWordFormatDocument = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocument;
            object myLockd = false;
            object myPassword = "";
            object myAddto = true;
            try {

                this._wordDocument.SaveAs(ref myFileName, ref myWordFormatDocument, ref myLockd, ref myPassword, ref myAddto, ref myPassword,
                    ref myLockd, ref myLockd, ref myLockd, ref myLockd, ref myNothing, ref myNothing, ref myNothing,
                    ref myNothing, ref myNothing, ref myNothing);
                object saveOption = Word.WdSaveOptions.wdDoNotSaveChanges;
                this._wordDocument.Close(ref myNothing, ref myNothing, ref myNothing);
                this._wordApplication.Quit(ref saveOption, ref myNothing);
                this._wordApplication = null;
                this._wordDocument = null;

            } catch {
                throw new Exception("导出word文档失败!");
            }
        }
    }
}
