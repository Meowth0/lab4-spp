using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGeneratorLib
{
    public class TestGeneratorConfig
    {
        public int MaxReadTasksCount { get; }
        public int MaxProcessingTasksCount { get; }
        public int MaxWriteTasksCount { get; }
        public TestGeneratorConfig(int maxReadTasksCount, int maxProcessingTasksCount, int maxWriteTasksCount)
        {
            MaxProcessingTasksCount = maxProcessingTasksCount;
            MaxReadTasksCount = maxReadTasksCount;
            MaxWriteTasksCount = maxWriteTasksCount;
        }
    }
}
