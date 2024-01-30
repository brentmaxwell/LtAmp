using LtAmpDotNet.Lib.Model.Preset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LtAmpDotNet.DataModels
{
    public class NodeViewModel
    {
        public string NodeType { get; set; }
        public string FenderID { get; set; }
        public string DisplayName { get; set; }
        //public Dictionary<string, ParameterViewModel> Parameters { get; set; }
    }
}
