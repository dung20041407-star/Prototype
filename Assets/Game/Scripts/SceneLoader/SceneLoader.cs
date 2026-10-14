using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    [SerializeField] private string homeSceneName = "Home";
    [SerializeField] private string map1SceneName = "WorldScene1";

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (Keyboard.current.digit0Key.wasPressedThisFrame)
        {
            LoadHomeScene();
        }
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            LoadMap1();
        }
    }
    public void LoadHomeScene()
    {
        SceneManager.LoadScene(homeSceneName);
    }
    public void LoadMap1()
    {
        SceneManager.LoadScene(map1SceneName);
    }
}
