using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGeneratorLib.DataStructures
{
    public class MethodInfo
    {
        public string Name { get; }

        public MethodInfo(string methodName)
        {
            Name = methodName; 
        }
    }
}
