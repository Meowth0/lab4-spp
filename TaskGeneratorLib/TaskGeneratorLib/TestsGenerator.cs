using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using TestsGeneratorLib.DataStructures;

namespace TestsGeneratorLib
{
    public class TestsGenerator
    {
        private readonly TestGeneratorConfig config;
        public TestsGenerator(TestGeneratorConfig config)
        {
            this.config = config;
        }

        public async Task Generate(List<string> pathes, string destination)
        {
            DataflowLinkOptions linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
            ExecutionDataflowBlockOptions readBlockOptions = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = config.MaxReadTasksCount
            };
            ExecutionDataflowBlockOptions processingOptions = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = config.MaxProcessingTasksCount
            };
            ExecutionDataflowBlockOptions writeBlockOptions = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = config.MaxWriteTasksCount
            };
            TransformBlock<string, string> readBlock = new TransformBlock<string, string>(fileName => AsyncReader.Read(fileName), readBlockOptions);
            TransformBlock<string, List<GeneratedTest>> processBlock = new TransformBlock<string, List<GeneratedTest>>(sourceCode => GenerateTestClasses(sourceCode), processingOptions);

        }
        private List<GeneratedTest> GenerateTestClasses(string sourceCode)
        {
            ParsingResultBuilder builder = new ParsingResultBuilder();
        }
    }
}
