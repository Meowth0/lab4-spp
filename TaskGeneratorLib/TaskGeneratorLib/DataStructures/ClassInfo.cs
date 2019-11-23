using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGeneratorLib.DataStructures
{
    public class ClassInfo
    {
        public string Name { get; }
        public string NamespaceName { get; }
        public List<MethodInfo> Methods { get; }
        public ClassInfo(string className, string namespaceName, List<MethodInfo> methods)
        {
            Name = className;
            NamespaceName = namespaceName;
            Methods = methods;
        }
    }
}
