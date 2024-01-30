using LtAmpDotNet.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LtAmpDotNet.Base
{
    public class ParameterPropertyGridAdapter : ICustomTypeDescriptor
    {

        private List<DspUnitParameterViewModel> _parameters;
        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string? GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string? GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor? GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor? GetDefaultProperty()
        {
            return null;
        }

        public object? GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[]? attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return ((ICustomTypeDescriptor)this).GetProperties(new Attribute[0]);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[]? attributes)
        {
            ArrayList properties = new ArrayList();
            foreach (var item in _parameters)
            {
                properties.Add(new DspUnitParameterPropertyDescriptor(_parameters, item.Name));
            }

            PropertyDescriptor[] props =
                (PropertyDescriptor[])properties.ToArray(typeof(PropertyDescriptor));

            return new PropertyDescriptorCollection(props);
        }

        public object? GetPropertyOwner(PropertyDescriptor? pd)
        {
            return _parameters;
        }

        public ParameterPropertyGridAdapter(List<DspUnitParameterViewModel> parameters)
        {
            _parameters = parameters;
        }
    }

    public class DspUnitParameterPropertyDescriptor : PropertyDescriptor
    {
        private List<DspUnitParameterViewModel> _parameters;
        object _key;
        public DspUnitParameterPropertyDescriptor(List<DspUnitParameterViewModel> parameters, object key): base(key.ToString(), null)
        {
            _parameters = parameters;
            _key = key;
        }

        public override Type ComponentType => null;

        public override bool IsReadOnly => false;

        public override Type PropertyType
        {
            get { return _parameters.SingleOrDefault(x => x.Name == (string)_key).GetType(); }
        }

        public override bool CanResetValue(object? component) => false;

        public override void SetValue(object? component, object? value)
        {
            _parameters.SingleOrDefault(x => x.Name == (string)_key).Value = value;
        }

        public override object GetValue(object? component)
        {
            return _parameters.SingleOrDefault(x => x.Name == (string)_key).Value;
        }

        public override void ResetValue(object? component)
        {
            throw new NotImplementedException();
        }

        public override bool ShouldSerializeValue(object component) => false;
    }
}
