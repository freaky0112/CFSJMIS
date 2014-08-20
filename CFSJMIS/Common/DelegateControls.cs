using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace CFSJMIS {
    class DelegateProgressBar {
        private ProgressBar _progressBar;
        /// <summary>
        /// 初始化变量
        /// </summary>
        /// <param name="progressBar">传入进度条控件</param>
        public DelegateProgressBar(ProgressBar progressBar) {
            this._progressBar = progressBar;
        }

        private delegate void outputDelegate(double value);

        public void output(double value) {
            this._progressBar.Dispatcher.Invoke(new outputDelegate(outputAction), value);
        }

        private void outputAction(double value) {
            this._progressBar.Value = value;
        }
    }
}
