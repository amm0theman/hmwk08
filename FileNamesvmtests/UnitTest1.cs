using System;
using hmwk08;
using NUnit;
using System.Diagnostics;
using NUnit.Framework;

namespace FileNamesvmtests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            FileNames fileVM = new FileNames();
            Assert.NotNull(fileVM.DirectoryPath);
        }

        [Test]
        public void TestMethod2()
        {
            FileNames fileVM = new FileNames();
            Assert.NotNull(fileVM.FileNamez);
        }

        [Test]
        public void TestMethod3()
        {
            FileNames fileVM = new FileNames();
            Assert.NotNull(fileVM.FilePaths);
        }

        [Test]
        public void TestMethod4()
        {
            FileNames fileVM = new FileNames();
            fileVM.DirectoryPath = "asdf";
            Assert.AreEqual(fileVM.DirectoryPath, "asdf");
            Assert.IsTrue(fileVM.HasErrors);
        }

        [Test]
        public void TestMethod5()
        {
            FileNames fileVM = new FileNames();
            fileVM.DirectoryPath = "C:\\";
            Assert.AreEqual(fileVM.DirectoryPath, "C:\\");
        }

        [Test]
        public void TestMethod6()
        {
            FileNames fileVM = new FileNames();
        }

        [Test]
        public void TestMethod7()
        {
            FileNames fileVM = new FileNames();
        }

        [Test]
        public void TestMethod8()
        {
            FileNames fileVM = new FileNames();
        }

        [Test]
        public void TestMethod9()
        {
            FileNames fileVM = new FileNames();
        }

        [Test]
        public void TestMethod10()
        {
            FileNames fileVM = new FileNames();
        }
    }
}
