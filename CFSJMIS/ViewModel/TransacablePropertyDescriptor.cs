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
using CFSJMIS.ViewModel;

namespace CFSJMIS.ViewModel {
    public class TransacablePropertyDescriptor : PropertyDescriptor {
        private PropertyInfo m_PropertyInfo;
        private bool m_IsReadOnly;

        public TransacablePropertyDescriptor(PropertyInfo property) :
            base(property.Name, null) {
            if (property == null)
                throw new ArgumentNullException("propertyName");

            m_PropertyInfo = property;
            m_IsReadOnly = property.GetSetMethod() == null;
        }

        public override bool CanResetValue(object component) {
            return false;
        }

        public override Type ComponentType {
            get {
                return m_PropertyInfo.DeclaringType;
            }
        }

        public override object GetValue(object component) {
            var transacable = component as ITransacableObject;
            if (transacable == null)
                return m_PropertyInfo.GetValue(component, null);
            else
                return transacable.ReadPropertyValue(m_PropertyInfo);
        }

        public override bool IsReadOnly {
            get {
                return m_IsReadOnly;
            }
        }

        public override Type PropertyType {
            get {
                return m_PropertyInfo.PropertyType;
            }
        }

        public override void ResetValue(object component) {
        }

        public override void SetValue(object component, object value) {
            var transacable = component as ITransacableObject;
            if (transacable == null)
                m_PropertyInfo.SetValue(component, value, null);
            else
                transacable.SetPropertyValue(m_PropertyInfo, value);
        }

        public override bool ShouldSerializeValue(object component) {
            // 先简单起见
            return false;
        }
    }

    public class NonTransacablePropertyDescriptor : PropertyDescriptor {
        private PropertyInfo m_PropertyInfo;
        private bool m_IsReadOnly;

        public NonTransacablePropertyDescriptor(PropertyInfo property) :
            base(property.Name, null) {
            if (property == null)
                throw new ArgumentNullException("propertyName");

            m_PropertyInfo = property;
            m_IsReadOnly = property.GetSetMethod() == null;
        }

        public override bool CanResetValue(object component) {
            return false;
        }

        public override Type ComponentType {
            get {
                return m_PropertyInfo.DeclaringType;
            }
        }

        public override object GetValue(object component) {
            return m_PropertyInfo.GetValue(component, null);
        }

        public override bool IsReadOnly {
            get {
                return m_IsReadOnly;
            }
        }

        public override Type PropertyType {
            get {
                return m_PropertyInfo.PropertyType;
            }
        }

        public override void ResetValue(object component) {
        }

        public override void SetValue(object component, object value) {
            m_PropertyInfo.SetValue(component, value, null);
        }

        public override bool ShouldSerializeValue(object component) {
            return false;
        }
    }
}
