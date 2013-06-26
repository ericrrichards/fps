using System.Drawing;
using System.Text;
using Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using SlimDX;

namespace EngineTests
{
    
    
    /// <summary>
    ///This is a test class for VariableTest and is intended
    ///to contain all VariableTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VariableTest {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Variable Constructor
        ///</summary>
        [TestMethod()]
        public void VariableConstructorTest_String() {
            string name = "foo";
            Stream file = new MemoryStream(Encoding.ASCII.GetBytes("string \"foo bar fizz buzz\""));
            var target = new Variable(name, file);
            Assert.AreEqual("foo bar fizz buzz", target.Data);
        }
        [TestMethod()]
        public void VariableConstructorTest_BoolT() {
            string name = "foo";
            Stream file = new MemoryStream(Encoding.ASCII.GetBytes("bool true")); 
            var target = new Variable(name, file);
            Assert.AreEqual(true, target.Data);
        }
        [TestMethod()]
        public void VariableConstructorTest_BoolF() {
            string name = "foo";
            Stream file = new MemoryStream(Encoding.ASCII.GetBytes("bool false"));
            var target = new Variable(name, file);
            Assert.AreEqual(false, target.Data);
        }
        [TestMethod()]
        public void VariableConstructorTest_Color() {
            string name = "foo";
            Stream file = new MemoryStream(Encoding.ASCII.GetBytes("color 1.0 1.0 1.0 1.0"));
            var target = new Variable(name, file);
            Assert.AreEqual(new Color4(Color.White), target.Data);
        }
        [TestMethod()]
        public void VariableConstructorTest_float() {
            string name = "foo";
            Stream file = new MemoryStream(Encoding.ASCII.GetBytes("float 47.0"));
            var target = new Variable(name, file);
            Assert.AreEqual(47.0f, target.Data);
        }
        [TestMethod()]
        public void VariableConstructorTest_Number() {
            string name = "foo";
            Stream file = new MemoryStream(Encoding.ASCII.GetBytes("number 45324"));
            var target = new Variable(name, file);
            Assert.AreEqual(45324L, target.Data);
        }
        [TestMethod()]
        public void VariableConstructorTest_Vector() {
            string name = "foo";
            Stream file = new MemoryStream(Encoding.ASCII.GetBytes("vector 3 4.0 5.0"));
            var target = new Variable(name, file);
            Assert.AreEqual(new Vector3(3,4,5), target.Data);
        }

        /// <summary>
        ///A test for Variable Constructor
        ///</summary>
        [TestMethod()]
        public void VariableConstructorTest1() {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            VariableType type = new VariableType(); // TODO: Initialize to an appropriate value
            object value = null; // TODO: Initialize to an appropriate value
            Variable target = new Variable(name, type, value);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
