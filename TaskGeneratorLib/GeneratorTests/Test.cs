using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsGeneratorLib;

namespace GeneratorTests
{
    [TestClass]
    public class Test
    {
        private CompilationUnitSyntax root;

        [TestInitialize]
        public void SetUp()
        {
            int readingLimit = 3, writingLimit = 3, processingLimit = 8;
            string sourceCode;
            string workPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\TestsFiles\\";
            SyntaxTree codeTree;
            List<string> pathes = new List<string>();
            TestGeneratorConfig config;
            TestsGenerator generator;

            pathes.Add(workPath + "MyClass.cs");
            config = new TestGeneratorConfig(readingLimit, processingLimit, writingLimit);
            generator = new TestsGenerator(config);
            generator.Generate(pathes, workPath + "GeneratedTests").Wait();

            sourceCode = File.ReadAllText(workPath + "GeneratedTests\\MyClassTest.dat");
            codeTree = CSharpSyntaxTree.ParseText(sourceCode);
            root = codeTree.GetCompilationUnitRoot();
        }

        [TestMethod]
        public void UsingDeclarationsTest()
        {
            Assert.AreEqual("System", root.Usings[0].Name.ToString());
            Assert.AreEqual("System.Collections.Generic", root.Usings[1].Name.ToString());
            Assert.AreEqual("System.Linq", root.Usings[2].Name.ToString());
            Assert.AreEqual("System.Text", root.Usings[3].Name.ToString());
            Assert.AreEqual("Microsoft.VisualStudio.TestTools.UnitTesting", root.Usings[4].Name.ToString());
            Assert.AreEqual("MyNamespace", root.Usings[5].Name.ToString());
        }
        [TestMethod]
        public void ClassTest()
        {
            IEnumerable<ClassDeclarationSyntax> classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();

            Assert.AreEqual(1, classes.Count());
            Assert.AreEqual("MyClassTests", classes.ElementAt<ClassDeclarationSyntax>(0).Identifier.ToString());
            Assert.AreEqual(1, classes.ElementAt<ClassDeclarationSyntax>(0).AttributeLists.Count);
            Assert.AreEqual("TestClass", classes.ElementAt<ClassDeclarationSyntax>(0).AttributeLists[0].Attributes[0].Name.ToString());
        }

        public void MethodAttributesTest(MethodDeclarationSyntax method)
        {
            Assert.AreEqual(1, method.AttributeLists.Count);
            Assert.AreEqual(1, method.AttributeLists[0].Attributes.Count);
            Assert.AreEqual("TestMethod", method.AttributeLists[0].Attributes[0].Name.ToString());
        }

        [TestMethod]
        public void MethodsTest()
        {
            IEnumerable<MethodDeclarationSyntax> methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();

            Assert.AreEqual(4, methods.Count());
            Assert.AreEqual("Method1Test", methods.ElementAt<MethodDeclarationSyntax>(0).Identifier.ToString());
            MethodAttributesTest(methods.ElementAt<MethodDeclarationSyntax>(0));
            Assert.AreEqual("Method2Test", methods.ElementAt<MethodDeclarationSyntax>(1).Identifier.ToString());
            MethodAttributesTest(methods.ElementAt<MethodDeclarationSyntax>(1));
            Assert.AreEqual("Method3Test", methods.ElementAt<MethodDeclarationSyntax>(2).Identifier.ToString());
            MethodAttributesTest(methods.ElementAt<MethodDeclarationSyntax>(2));
        }
    }
}
