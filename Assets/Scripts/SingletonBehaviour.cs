using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
    public static T Instance { get; private set; }

    public SingletonBehaviour() {
        Instance = this as T;
    }
}
