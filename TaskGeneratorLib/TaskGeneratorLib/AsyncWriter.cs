using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsGeneratorLib.DataStructures;

namespace TestsGeneratorLib
{
    public static class AsyncWriter
    {
        public static async Task Write(string destination, List<GeneratedTest> tests)
        {
            string path;
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            foreach(GeneratedTest generatedTest in tests)
            {
                path = destination + "\\" + generatedTest.Name;
                using (StreamWriter writer = new StreamWriter(path))
                {
                    await writer.WriteAsync(generatedTest.Content);
                }
            }
        }
    }
}
