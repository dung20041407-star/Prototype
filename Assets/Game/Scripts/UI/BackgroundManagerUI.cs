using UnityEngine;

public class BackpackBackgroundUI : MonoBehaviour
{
    [SerializeField] private GameObject backpackPanel;
    [SerializeField] private GameObject backpackBackground;

    private bool lastState;

    private void Start()
    {
        lastState = backpackPanel.activeSelf;
        backpackBackground.SetActive(lastState);
    }

    private void Update()
    {
        if (backpackPanel.activeSelf != lastState)
        {
            lastState = backpackPanel.activeSelf;
            backpackBackground.SetActive(lastState);
        }
    }
}
