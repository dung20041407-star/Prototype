using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;

public class CameraBoundsController : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner2D confiner;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        UpdateCameraBounds();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateCameraBounds();
    }

    private void UpdateCameraBounds()
    {
        GameObject boundsObject =
            GameObject.FindGameObjectWithTag("CameraBounds");

        if (boundsObject == null)
        {
            Debug.LogWarning("No CameraBounds in current Scene!");
            return;
        }

        Collider2D bounds = boundsObject.GetComponent<Collider2D>();

        if (bounds == null)
        {
            Debug.LogError("CameraBounds don't have Collider2D!");
            return;
        }

        confiner.BoundingShape2D = bounds;
        confiner.InvalidateBoundingShapeCache();

        Debug.Log("Update CameraBounds: " + boundsObject.name);
    }
}
