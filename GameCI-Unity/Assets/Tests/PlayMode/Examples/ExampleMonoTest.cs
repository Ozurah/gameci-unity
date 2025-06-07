using NUnit.Framework;
using Project.Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

namespace Project.Tests.PlayMode.Example
{
    [TestFixture]
    public class ExampleMonoTest
    {

        [UnityTest]
        public IEnumerator ExampleMonoTestPasses()
        {
            // Note : "yield return WaitForEndOfFrame" vs "null" => https://answers.unity.com/questions/755196/yield-return-null-vs-yield-return-waitforendoffram.html
            // Null is in "GameLogic" (before scene rendering) and WaitForEndOfFrame is after GUI Rendering

            var gameObject = new GameObject();
            var component = gameObject.AddComponent<MonoToTest>();
            var testedComponent = new GameObject();
            component.GameObjectToTest = testedComponent;

            yield return null; // skip 1 frame to let "Update" perform

            Debug.Log("position in update (2) ? =>" + testedComponent.transform.position);

            Assert.That(testedComponent.transform.position, Is.EqualTo(new Vector3(2, 2, 2)).Using(Vector3EqualityComparer.Instance));
        }
    }
}