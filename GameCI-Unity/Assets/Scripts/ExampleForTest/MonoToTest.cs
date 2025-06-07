using UnityEngine;

namespace Project.Example
{
    /// <summary>
    /// Example monobehaviour class, used to train unity test framework
    /// </summary>
    public class MonoToTest : MonoBehaviour
    {
        [SerializeField]
        private GameObject gameObjectToTest;
        public GameObject GameObjectToTest { get => gameObjectToTest; set => gameObjectToTest = value; } // Require for mocking private serializefield

        // Start is called before the first frame update
        void Start()
        {
            gameObjectToTest.transform.position = new Vector3(1, 1, 1);
            Debug.Log("Start finished");
        }

        void Update()
        {
            Debug.Log("Update called");

            if (gameObjectToTest != null)
            {
                gameObjectToTest.transform.position = new Vector3(2, 2, 2);
            }

        }
    }
}