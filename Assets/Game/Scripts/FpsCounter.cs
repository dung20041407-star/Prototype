using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;

    private float deltaTime;

    private void Awake()
    {
        deltaTime = 0.016f;
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        float fps = 1f / deltaTime;

        fpsText.text = "FPS: " + Mathf.RoundToInt(fps);
    }
}
