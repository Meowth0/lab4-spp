
using System;
using System.Collections.Generic;
using System.IO;
using TestsGeneratorLib;

namespace TestView
{
    class Program
    {
        static void Main(string[] args)
        {
            int readingLimit = 5;
            int writingLimit = 5;
            int processingLimit = 10;
            string workPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\TestsFiles\\";
            List<string> pathes = new List<string>
            {
                workPath + "MyClass.cs",
                workPath + "Tracer.cs"
            };

            TestGeneratorConfig config = new TestGeneratorConfig(readingLimit, processingLimit, writingLimit);
            TestsGenerator generator = new TestsGenerator(config);

            generator.Generate(pathes, workPath + "GeneratedTests").Wait();
        }
    }
}
