using UnityEngine;

public class PersistentSingleton : MonoBehaviour
{
    private static PersistentSingleton instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}