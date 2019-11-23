
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGeneratorLib.DataStructures
{
    public class GeneratedTest
    {
        public string Name { get; }
        public string Content { get; }

        public GeneratedTest(string name, string content)
        {
            Name = name;
            Content = content;
        }
    }
}
