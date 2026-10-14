using UnityEngine;

public class PlayerDontDestroyOnload : MonoBehaviour
{
    public static PlayerDontDestroyOnload Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}