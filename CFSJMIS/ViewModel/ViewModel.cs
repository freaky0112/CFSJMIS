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


namespace CFSJMIS.ViewModel {
    public class EditableTypeDescriptor : CustomTypeDescriptor {
        private PropertyDescriptorCollection m_MergedProperties;

        public EditableTypeDescriptor(Type type) {
            if (type == null)
                throw new ArgumentNullException("type");
            if (type.GetInterface("IEditableObject") == null)
                throw new ArgumentException(string.Format("类型{0}必须支持IEditableObject接口", type));

            m_MergedProperties = new PropertyDescriptorCollection(new PropertyDescriptor[] { });
            MergeEditableProperties(type);
        }

        private void MergeEditableProperties(Type type) {
            while (type != typeof(object)) {
                Type transactionType = null;
                if (type.IsGenericType) {
                    if (type.GetGenericTypeDefinition() == typeof(ModelBase<>)) {
                        AddNonTransactionProperties(type);
                        var arguments = type.GetGenericArguments();
                        if (arguments.Length != 1)
                            throw new ArgumentException("需要传递从泛型ModelBase<>实例化出来的类型，而不是ModelBase<>类型本身");

                        transactionType = arguments[0];
                    } else {
                        throw new NotSupportedException(
                            string.Format("对于泛型类型{0}，只支持从ModelBase<>实例化出来的类型", type));
                    }
                } else {
                    transactionType = type;
                }

                AddTransactionProperties(transactionType);
                type = type.BaseType;
            }
        }

        private void AddTransactionProperties(Type transactionType) {
            var properties = transactionType.GetProperties();
            foreach (PropertyInfo info in properties)
                m_MergedProperties.Add(new TransacablePropertyDescriptor(info));

            transactionType = transactionType.BaseType;
        }

        private void AddNonTransactionProperties(Type type) {
            var properties = type.GetProperties();
            foreach (PropertyInfo info in properties)
                m_MergedProperties.Add(new NonTransacablePropertyDescriptor(info));

            type = type.BaseType;
        }

        public override PropertyDescriptorCollection GetProperties() {
            var properties = new PropertyDescriptor[m_MergedProperties.Count];
            m_MergedProperties.CopyTo(properties, 0);
            return new PropertyDescriptorCollection(properties);
        }
    }

    public class EditableTypeDescriptorProvider : TypeDescriptionProvider {
        private Dictionary<Type, EditableTypeDescriptor> m_TypeDescriptors = new Dictionary<Type, EditableTypeDescriptor>();

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance) {
            if (m_TypeDescriptors.ContainsKey(objectType)) {
                return m_TypeDescriptors[objectType];
            } else {
                var descriptor = new EditableTypeDescriptor(objectType);
                m_TypeDescriptors[objectType] = descriptor;
                return descriptor;
            }
        }
    }

}
