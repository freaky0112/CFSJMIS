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
using System.Diagnostics;
using System.Reflection;
using CFSJMIS.Collections;

namespace CFSJMIS.ViewModel {
    public interface ITransacableObject {
        object ReadPropertyValue(PropertyInfo property);

        void SetPropertyValue(PropertyInfo property, object value);
    }

    [TypeDescriptionProvider(typeof(EditableTypeDescriptorProvider))]
    public class ModelBase<T> : INotifyPropertyChanged, IEditableObject, ITransacableObject {
        private Dictionary<PropertyInfo, object> m_ChangedProperties =
            new Dictionary<PropertyInfo, object>();

        public bool HasChanges {
            get {
                return m_ChangedProperties.Count > 0;
            }
        }

        private T m_WrappedObject;

        public ModelBase(T wrappedObject) {
            m_WrappedObject = wrappedObject;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region IEditableObject Members

        public void BeginEdit() {
            m_ChangedProperties.Clear();
        }

        public void CancelEdit() {
            var properties = new PropertyInfo[m_ChangedProperties.Keys.Count];
            m_ChangedProperties.Keys.CopyTo(properties, 0);

            m_ChangedProperties.Clear();
            foreach (var property in properties) {
                NotifyPropertyChanged(property.Name);
            }
        }

        public void EndEdit() {
            foreach (var propertyValue in m_ChangedProperties) {
                if (propertyValue.Key.DeclaringType == typeof(T)) {
                    propertyValue.Key.SetValue(m_WrappedObject, propertyValue.Value, null);
                    NotifyPropertyChanged(propertyValue.Key.Name);
                }
                // else
                // ModelBase<T>里面的属性不支持任何可变更的属性
            }
            m_ChangedProperties.Clear();
        }

        #endregion

        #region ITransacableObject Members

        object ITransacableObject.ReadPropertyValue(PropertyInfo property) {
            if (m_ChangedProperties.ContainsKey(property))
                return m_ChangedProperties[property];
            else {
                if (property.DeclaringType == GetType())
                    return property.GetValue(this, null);
                else if (property.DeclaringType == typeof(T))
                    return property.GetValue(m_WrappedObject, null);
                else
                    throw new InvalidOperationException(
                        string.Format("属性{0}即不属于泛型ModelBase<>，也没有在{1}里面定义", property.Name, typeof(T)));
            }
        }

        void ITransacableObject.SetPropertyValue(PropertyInfo property, object value) {
            m_ChangedProperties[property] = value;
        }

        #endregion
    }

    public class DataModel : ModelBase<Data> {
        public DataModel(Data data)
            : base(data) {
            // EditCommand = new GenericCommand(p => BeginEdit(), p => true);
            CommitChangesCommand = new GenericCommand(p => EndEdit(), p => HasChanges);
            CancelChangesCommand = new GenericCommand(p => CancelEdit(), p => HasChanges);
            ApplyChangesCommand = new GenericCommand(delegate(object param) {
                EndEdit();
                BeginEdit();
            },
            p => HasChanges);
        }

        //public GenericCommand EditCommand { get; private set; }

        public GenericCommand CommitChangesCommand {
            get;
            private set;
        }

        public GenericCommand CancelChangesCommand {
            get;
            private set;
        }

        public GenericCommand ApplyChangesCommand {
            get;
            private set;
        }
    }
    public class CharacterModel : ModelBase<Character> {
        public CharacterModel(Character character)
            : base(character) {
            // EditCommand = new GenericCommand(p => BeginEdit(), p => true);
            CommitChangesCommand = new GenericCommand(p => EndEdit(), p => HasChanges);
            CancelChangesCommand = new GenericCommand(p => CancelEdit(), p => HasChanges);
            ApplyChangesCommand = new GenericCommand(delegate(object param) {
                EndEdit();
                BeginEdit();
            },
            p => HasChanges);
        }

        //public GenericCommand EditCommand { get; private set; }

        public GenericCommand CommitChangesCommand {
            get;
            private set;
        }

        public GenericCommand CancelChangesCommand {
            get;
            private set;
        }

        public GenericCommand ApplyChangesCommand {
            get;
            private set;
        }
    }
}
