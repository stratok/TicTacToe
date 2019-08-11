using UnityEngine;

public class ObjectKeeper : MonoBehaviour
{
    private static ObjectKeeper _instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_instance == null)
            _instance = this;
        else if (this != _instance)
            Destroy(this.gameObject);
    }
}
