using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

// Tutorial : https://medium.com/xrpractices/practical-playmode-testing-in-unity3d-5ea455bf28b0
// This tutorial use existing scene for tests instead of mocking objects.

namespace Project.Tests.PlayMode.Example
{
    [TestFixture]
    public class ExampleSceneTest
    {
        [OneTimeSetUp]
        public void LoadScene()
        {
            //SceneManager.LoadScene("ExampleTestScene2D");
            SceneManager.LoadScene("ExampleTestScene3D");
        }

        [UnityTest]
        public IEnumerator ExampleSceneTestPasses()
        {
            // Note : "yield return WaitForEndOfFrame" vs "null" => https://answers.unity.com/questions/755196/yield-return-null-vs-yield-return-waitforendoffram.html
            // Null is in "GameLogic" (before scene rendering) and WaitForEndOfFrame is after GUI Rendering

            //var testedComponent = GameObject.Find("Square");
            var testedComponent = GameObject.Find("Cube");

            // Setup gameobject for test
            testedComponent.transform.position = Vector3.zero;

            // Now the GameObject is at position 0,0,0 (like show in the debug)
            Debug.Log("position in update (0) ? =>" + testedComponent.transform.position);


            yield return null; // skip 1 frame to let "Update" perform

            Debug.Log("position in update (2) ? =>" + testedComponent.transform.position);

            Assert.That(testedComponent.transform.position, Is.EqualTo(new Vector3(2, 2, 2)).Using(Vector3EqualityComparer.Instance));
        }
    }
}
