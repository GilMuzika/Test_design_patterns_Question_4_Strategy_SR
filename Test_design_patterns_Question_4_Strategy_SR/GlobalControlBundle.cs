using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace Test_design_patterns_Question_4_Strategy
{
    static public class GlobalControlBundle
    {
        static private Control _controlOwner;
        static private Dictionary<string, Control> _controls = new Dictionary<string, Control>();

        /// <summary>
        /// In the case of WinForms the controlHolder is the main form
        /// </summary>
        /// <param name="control"></param>
        /// <param name="controlHolder"></param>
        static public void Add(string controlVariableName, Control controlOwner)
        {
            if (_controlOwner == null) _controlOwner = controlOwner;

            object control = _controlOwner.GetType().GetField(controlVariableName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static).GetValue(_controlOwner);
            if (!(control is Control)) throw new ArgumentException($"The parameter {controlVariableName} must inherit from \"Control\"! ");

            _controls.Add(controlVariableName, (Control)control);
        }
        static public void Add(Control control, Control controlOwner)
        {
            if (_controlOwner == null) _controlOwner = controlOwner;

            string controlVariableName = FindControlVariableName(control);
                        
            _controls.Add(controlVariableName, control);
        }

        static public void AddRange(ControlCollection controlCollection, Control controlOWner)
        {
            string controlVariableName = string.Empty;
            if (_controlOwner == null) _controlOwner = controlOWner;
            if(controlCollection.Count > 0)
            {                
                foreach(Control s in controlCollection)
                {
                    controlVariableName = FindControlVariableName(s);
                    if (!String.IsNullOrEmpty(controlVariableName))
                    {                        
                        _controls.Add(controlVariableName, s);
                    }
                }

                
            }


        }

        static private string FindControlVariableName(Control control)
        {
            string controlVariableName = string.Empty;
            FieldInfo[] fieldsCollection = _controlOwner.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo s in fieldsCollection)
            {
                if (ReferenceEquals(control, s.GetValue(_controlOwner)))
                {
                    controlVariableName = s.Name;
                    break;
                }
            }
            return controlVariableName;
        }

        static public Control Get(string controlVariablename)
        {
            return _controls[controlVariablename];
        }
        static void Remove(string controlVariablename)
        {
            if (_controls[controlVariablename] != null)
                _controls.Remove(controlVariablename);
        }
        static void Clear()
        {
            _controls.Clear();
            _controlOwner = null;
        }
        static void KillControlsOwner()
        {
            _controlOwner = null;            
        }



    }
}
