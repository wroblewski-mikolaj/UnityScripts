using UnityEngine;


public class DontDestroy : MonoBehaviour
{
    [HideInInspector] public string objectID;

    private void Awake()
    {
        objectID = name;
    }
    private void Start()
    {
        var dontDestroyObjects = Object.FindObjectsOfType<DontDestroy>();

        for (int i = 0; i < dontDestroyObjects.Length; i++)
        {
            if (dontDestroyObjects[i] != this && dontDestroyObjects[i].objectID == objectID)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}
