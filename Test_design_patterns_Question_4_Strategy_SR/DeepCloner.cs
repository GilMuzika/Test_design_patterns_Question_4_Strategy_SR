using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace Test_design_patterns_Question_4_Strategy_SR
{
    /// <summary>
    /// This class provides some methods to deep cloning objects
    /// </summary>
    public static class DeepCloner
    {      
        /// <summary>
        /// this files is used by method "MemberWiseDeepCloneWithRecursiveMemberWiseClone"
        /// It contains the ordinary "MemberwiseClone" method that taken out by reflection ("MethodInfo" object)
        /// </summary>
        private static readonly MethodInfo CloneMethod = typeof(Object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);



        /// <summary>
        /// Strategy selection method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalObj"></param>
        /// <returns></returns>
        public static T DeepClone<T>(this T originalObj)
        {
            if (typeof(T).IsSerializable) return originalObj.DeepCloneWithBinaryFormatter();

            MethodInfo getEnumeratorMethod = typeof(T).GetMethod("GetEnumerator", BindingFlags.Public | BindingFlags.Instance);
            if (getEnumeratorMethod == null) return originalObj.MemberWiseDeepClone(4);

            throw new ArgumentException($"Sorry, but the type {originalObj.GetType().Name} must either be serializable or not be IEnumerable");



        }

        /// <summary>
        /// This method implements the ordinary MemberWiseClone recursively on each field or property
        /// except primitive types.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        static private T MemberWiseDeepCloneWithRecursiveMemberWiseClone<T>(this T t) where T : class, new()
        {

            T toReturn = (T)CloneMethod.Invoke(t, null);


                for (int i = 0; i < typeof(T).GetFields().Length; i++)
                {
                    if (typeof(T).GetFields()[i].GetValue(t).GetType().IsPrimitive) continue;

                    typeof(T).GetFields()[i].SetValue(toReturn, MemberWiseDeepCloneWithRecursiveMemberWiseClone((T)typeof(T).GetFields()[i].GetValue(t)));
                }

                for (int i = 0; i < typeof(T).GetProperties().Length; i++)
                {
                    if (typeof(T).GetProperties()[i].GetValue(t).GetType().IsPrimitive) continue;

                    typeof(T).GetProperties()[i].SetValue(toReturn, MemberWiseDeepCloneWithRecursiveMemberWiseClone(typeof(T).GetProperties()[i].GetValue(t)));
                }


            return toReturn;

        }


        static public T SimpleMemberWiseClone<T>(this T t) where T : class, new()
        {
            return (T)CloneMethod.Invoke(t, null);
        }

        /// <summary>
        /// Creates deep copy using BinaryFormatter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T DeepCloneWithBinaryFormatter<T>(this T t)
        {
            if (!typeof(T).IsSerializable) throw new ArgumentException("The type must be serializable.", "t");
            if (Object.ReferenceEquals(t, null)) return default(T);
            
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, t);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }


        /// <summary>
        /// This method provides deep memberwise cloning of object, applying the system provided "MemberWiseClone()"
        /// methos recursively to all its fields that aren't primitive or string, to all their subfields and so on.
        /// The method also treat circular references, stopping the etarnal loop they're causing.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original">The object to be cloned</param>
        /// <param name="recursiveIterationsStopper">Number of empty circular reference iterations 
        /// we let to occur before stopping them. If not provided, it's 50
        /// </param>
        /// <returns></returns>
        private static T MemberWiseDeepClone<T>(this T original, int recursiveIterationsStopper)
        {
            T rez = original.MemberWiseDeepCloneInternal("", recursiveIterationsStopper);
            File.WriteAllText("MemberWiseDeepClone_logFile.txt", _logText);
            return rez;
        }
        private static T MemberWiseDeepClone<T>(this T original)
        {
            T rez = original.MemberWiseDeepCloneInternal("", 50);
            File.WriteAllText("MemberWiseDeepClone_logFile.txt", _logText);
            return rez;
        }


        private static int _circularReferencesCount = 0;
        private static List<Dictionary<object, Type>[]> _fieldsStore = new List<Dictionary<object, Type>[]>();
        private static string _logText = string.Empty;
        private static T MemberWiseDeepCloneInternal<T>(this T original, string tabulation, int recursiveIterationsStopper)
        {
             MethodInfo getEnumeratorMethod = typeof(T).GetMethod("GetEnumerator", BindingFlags.Public | BindingFlags.Instance);
            if (getEnumeratorMethod != null) throw new ArgumentException("Sorry, but this methos has its restriction. Sadly, but it doesn't know to work with cillection, only with plain objects :(");
            //if(original) throw 

            //invoking the "MemberWiseClone()" methos using reflection
            T result = (T)CloneMethod.Invoke(original, null);


            if (CatchCircularReferences()) _circularReferencesCount++;            
            _logText += "CircularReferences: " + _circularReferencesCount + "\n" + Environment.NewLine;
            if (_circularReferencesCount > recursiveIterationsStopper) return result;



            tabulation += "   ";            
            _logText += $"{tabulation}{original.GetType().Name}, count: {_circularReferencesCount}\n" + Environment.NewLine;

            //this section is dedicated to accumulate the fields in special "store" List field.            
            FieldInfo[] fieldsInOriginal = original.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Instance);
            Dictionary<object, Type>[] suitcaseArr = new Dictionary<object, Type>[fieldsInOriginal.Length];
            int count = 0;
            foreach (FieldInfo field in fieldsInOriginal)
            {
                Dictionary<object, Type> containerDict = new Dictionary<object, Type>();                               
                var fieldValue = field.GetValue(original);
                Type ft = field.FieldType;

                if (fieldValue != null) 
                { 
                    containerDict.Add(fieldValue, ft);
                    suitcaseArr[count] = containerDict;
                    count++;
                }
            }
            _fieldsStore.Add(suitcaseArr);
            //End: this section is dedicated to accumulate the fields in special "store" List field.            


            //in the foreach "GetFileds" must be applied to the type of the object "original.GetType()", not to the "typeof(T)" because they arent the same!
            //typeof(T) is the generic entrance type T and cannot be changed in iterations, "original.GetType()" is the actual underlying type of each property in the acording iteration
            foreach (FieldInfo field in fieldsInOriginal)
            {
                var fieldValue = field.GetValue(original);
                Type ft = field.FieldType;

                if (fieldValue != null && !ft.IsValueType && ft != typeof(String))
                {
                    field.SetValue(result, MemberWiseDeepCloneInternal(fieldValue, tabulation, recursiveIterationsStopper));
                }
                else
                {                    
                    _logText += $"{tabulation}{ft.Name}: {JsonConvert.SerializeObject(fieldValue)}\n" + Environment.NewLine;
                    field.SetValue(result, fieldValue);
                }
            }
                       
            return result;
        }

        private static bool CatchCircularReferences()
        {
            Dictionary<object, Type>[] lastSuitcase = new Dictionary<object, Type>[2];
            Dictionary<object, Type>[] beforelastSuitcase = new Dictionary<object, Type>[2];
            bool isSame = false;

            if (_fieldsStore.Count >= 2)
            {
                lastSuitcase = _fieldsStore[_fieldsStore.Count - 1];
                beforelastSuitcase = _fieldsStore[_fieldsStore.Count - 2];

                
                for(int i = 0; i < lastSuitcase.Length; i++)
                {
                    if (lastSuitcase[i] != null && lastSuitcase[i].Keys.First() != null && !lastSuitcase[i].Values.First().IsValueType && lastSuitcase[i].Values.First() != typeof(String))
                    {
                        for (int j = 0; j < beforelastSuitcase.Length; j++)
                        {
                            if (beforelastSuitcase[j].Keys.First() != null && !beforelastSuitcase[j].Values.First().IsValueType && beforelastSuitcase[j].Values.First() != typeof(String))
                            {
                                if (ReferenceEquals(lastSuitcase[i].Keys.First(), beforelastSuitcase[j].Keys.First()))
                                {
                                    isSame = true;
                                    break;
                                }
                            }

                        }
                    }
                }
            }

            return isSame;
        }


    }
}
