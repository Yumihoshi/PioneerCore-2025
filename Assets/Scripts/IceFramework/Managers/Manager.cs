using UnityEngine;

public class Manager : MonoBehaviour
{
    private static GameObject GameObject;

    private void Start()
    {
        if (GameObject != null && GameObject != gameObject)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            GameObject = gameObject;
        }
    }
}
