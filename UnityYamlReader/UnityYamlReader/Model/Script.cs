using System;
using System.Collections.Generic;
using System.Text;

namespace UnityYamlReader.Model
{
    public class Script : UObject
    {
        public string guid { get; set; } = null!;
        public int type { get; set; }
    }
}
