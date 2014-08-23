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
using System.Windows.Input;

namespace CFSJMIS.ViewModel {
    public class GenericCommandBase<T> : ICommand {
        private Action<T> m_Execute;
        private Predicate<T> m_CanExecute;

        public GenericCommandBase(Action<T> execute, Predicate<T> canExecute) {
            m_Execute = execute;
            m_CanExecute = canExecute;
        }

        #region ICommand Members

        public bool CanExecute(object parameter) {
            if (m_CanExecute != null)
                return m_CanExecute((T)parameter);
            else
                return true;
        }

        public event EventHandler CanExecuteChanged {
            add {
                CommandManager.RequerySuggested += value;
            }
            remove {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter) {
            if (m_Execute != null)
                m_Execute((T)parameter);
        }

        #endregion
    }

    public class GenericCommand : GenericCommandBase<object> {
        public GenericCommand(Action<object> execute, Predicate<object> canExecute)
            : base(execute, canExecute) {
        }
    }
}
