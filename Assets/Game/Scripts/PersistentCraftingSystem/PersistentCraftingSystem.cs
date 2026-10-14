using UnityEngine;

public class PersistentCraftingSystem : MonoBehaviour
{
    public static PersistentCraftingSystem Instance { get; private set; }

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
