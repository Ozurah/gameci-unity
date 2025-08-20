using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Project.Example;

namespace Project.Tests.EditMode.Example
{
    [TestFixture]
    public class FailTest
    {
        [Test]
        public void EditModeExampleTestSimplePasses()
        {
            Assert.Fail();
        }
    }
}
