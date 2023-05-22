using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogOcus
{
    public class InputData
    {
        public Dictionary<string, Parameter> parameters = new();
    }

    public class Parameter
    {

        // all this sh*t because input is string
        public string?          name        { get; set; }
        public string?          nice_name   { get; set; }
        public ParameterType    type        { get; set; }
        public bool?            editable    { get; set; }
        public string?          description { get; set; }
        public double?          value       { get; set; }
        public double?          low_bound   { get; set; }
        public double?          high_bound  { get; set; }
        public double?          increment   { get; set; }

        public void SetParameterType (string input_value)
        {
            type = (ParameterType)Convert.ToInt32(input_value);
        }

        public void SetParameterEditable (string input_value)
        {
            editable = Convert.ToBoolean(Convert.ToInt32(input_value));
        }

        public void SetParameterValue(string input_value)
        {
            value = Convert.ToDouble(input_value);
        }

        public void SetParameterLowBound(string input_value)
        {
            low_bound = Convert.ToDouble(input_value);
        }

        public void SetParameterHighBound(string input_value)
        {
            high_bound = Convert.ToDouble(input_value);
        }

        public void SetParameterIncrement(string input_value)
        {
            increment = Convert.ToDouble(input_value);
        }


        /*// all this sh*t because input is string
        public string?          name        { get;                          set; }
        public string?          nice_name   { get;                          set; }
        public ParameterType   type        { get { return type; }          set { type = (ParameterType)Convert.ToInt32(value); } }
        public bool?            editable    { get { return editable; }      set { editable = Convert.ToBoolean(Convert.ToInt32(value)); } }
        public string?          description { get;                          set; }
        public double?          value       { get { return value; }         set { this.value = Convert.ToDouble(value); } }       // "this" because its name is already reserved by C#
        public double?          low_bound   { get { return low_bound; }     set { low_bound = Convert.ToDouble(value); } }
        public double?          high_bound  { get { return high_bound; }    set { high_bound = Convert.ToDouble(value); } }
        public double?          increment   { get { return increment; }     set { increment = Convert.ToDouble(value); } }*/



    }

    public enum ParameterType
    {
        varied,     // changes every trial in accordance with the choice of the subject
        constant    // permanent values as low_bound, high_bound, sigma etc.
    }
}
