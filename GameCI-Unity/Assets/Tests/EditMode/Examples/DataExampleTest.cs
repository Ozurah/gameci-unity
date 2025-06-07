using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Project.Example;

namespace Project.Tests.EditMode.Example
{
    [TestFixture]
    public class DataExampleTest
    {
        [Test]
        public void EditModeExampleTestSimplePasses()
        {
            DataToTest data = new DataToTest();
            data.IntVal = 10;
            data.StringVal = "Test";

            Assert.AreEqual(10, data.IntVal);
            Assert.AreEqual("Test", data.StringVal);
        }
    }
}
