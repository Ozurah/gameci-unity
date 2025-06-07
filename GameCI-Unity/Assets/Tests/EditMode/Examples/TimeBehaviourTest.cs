using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;
using UnityEngine;

namespace Project.Tests.EditMode.Example
{
    [TestFixture]
    public class TimeBehaviourTest
    {
        [Test]
        public void NUnitDelayedConstraints()
        {
            DateTime time = new DateTime(0) ;

            // Perform
            Debug.Log("Before delay : " + DateTime.Now);
            Assert.That(() => time = DateTime.Now, Is.Not.EqualTo(new DateTime(0)).After(10000));
            Debug.Log("After delay : " + DateTime.Now);
            Debug.Log("Time var has value : " + time);

            DateTime now = DateTime.Now ;
            DateTime expectedTimeMin = now.AddSeconds(-2); // We do a min/max margin to avoid false error due to exec time
            DateTime expectedTimeMax = now.AddSeconds(2);

            // Verify
            Assert.LessOrEqual(expectedTimeMin, time);
            Assert.GreaterOrEqual(expectedTimeMax, time);
        }
    }
}
