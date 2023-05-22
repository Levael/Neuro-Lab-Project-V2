using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogOcus
{
    public class InputData
    {
        public Dictionary<string, Dictionary<string, string>> parameters = new();

        public double? GetValueOf(string parameterName)
        {
            return (parameters.ContainsKey(parameterName) && parameters[parameterName].ContainsKey("value") ? double.Parse(parameters[parameterName]["value"]) : null);
        }

        public ParameterType? GetTypeOf(string parameterName)
        {
            return (parameters.ContainsKey(parameterName) && parameters[parameterName].ContainsKey("type") ? (ParameterType)Int32.Parse(parameters[parameterName]["type"]) : null);
        }

        public double? GetLowBoundOf(string parameterName)
        {
            return (parameters.ContainsKey(parameterName) && parameters[parameterName].ContainsKey("low_bound") ? double.Parse(parameters[parameterName]["low_bound"]) : null);
        }

        public double? GetHighBoundOf(string parameterName)
        {
            return (parameters.ContainsKey(parameterName) && parameters[parameterName].ContainsKey("high_bound") ? double.Parse(parameters[parameterName]["high_bound"]) : null);
        }

        public double? GetStepPlusOf(string parameterName)
        {
            return (parameters.ContainsKey(parameterName) && parameters[parameterName].ContainsKey("step_plus") ? double.Parse(parameters[parameterName]["step_plus"]) : null);
        }

        public double? GetStepMultOf(string parameterName)
        {
            return (parameters.ContainsKey(parameterName) && parameters[parameterName].ContainsKey("step_mult") ? double.Parse(parameters[parameterName]["step_mult"]) : null);
        }
    }


    public enum ParameterType
    {
        varied,     // changes every trial in accordance with the choice of the subject
        constant    // permanent values as low_bound, high_bound, sigma etc.
    }
}
