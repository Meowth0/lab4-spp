using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGeneratorLib.DataStructures
{
    public class ParsingResultStructure
    {
        public List<ClassInfo> Classes { get; }
        public ParsingResultStructure(List<ClassInfo> classes)
        {
            Classes = classes;
        }
    }
}
