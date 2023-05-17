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
        public string? name { get; set; }
        public string? nice_name { get; set; }
        public ParameterType? type { get; set; }
        public bool? editable { get; set; }
        public string? description { get; set; }
        public double? value { get; set; }
        public double? low_bound { get; set; }
        public double? high_bound { get; set; }
        public double? increment { get; set; }

    }

    public enum ParameterType
    {
        varied,     // changes every trial in accordance with the choice of the subject
        constant    // permanent values as low_bound, high_bound, sigma etc.
    }
}
