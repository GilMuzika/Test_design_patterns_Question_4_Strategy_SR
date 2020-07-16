using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Test_design_patterns_Question_4_Strategy_SR;

namespace Test_design_patterns_Question_4_Strategy_SR.strategy
{
    /// <summary>
    /// compares two classes of the same type by string-valued proprty of this type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class ComparerByInt32ValuedProperty<T> : IComparer<T>
    {
        private string _properyName;

        private int _propertyValue1;
        private int _propertyValue2;


        /// <summary>
        /// Takes the name of the property by wich the compasion should be done
        /// </summary>
        /// <param name="properyName">Name of the property by wich the compasion should be done</param>
        public ComparerByInt32ValuedProperty(string properyName)
        {
            _properyName = properyName;
        }

        public bool IsIcomparer { get => true; }

        public int Compare(T x, T y) 
        {
            PropertyInfo propInfo = typeof(T)?.GetProperty(_properyName);
            if (propInfo == null)
                throw new ArgumentException($"The type \"{typeof(T).Name}\" doesn't have any property named \"{_properyName}\"");            


            if(propInfo.PropertyType.Name != "Int32")
                throw new ArgumentException($"The property \"{_properyName}\" nust be of the type \"Int32\"! Its actual type is {propInfo.PropertyType.Name}");


            var propertyValue1 = propInfo.GetValue(x);
            var propertyValue2 = propInfo.GetValue(y);


            _propertyValue1 = (int)propertyValue1;
            _propertyValue2 = (int)propertyValue2;


            return _propertyValue1 - _propertyValue2;
        }


        public override string ToString()
        {
            return $"[current comparsion by a property named \"{_properyName}\", type \"Int32\"]";
        }


        

    }
}
