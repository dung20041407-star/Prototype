using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        InteractionTrigger trigger = other.GetComponent<InteractionTrigger>();

        if (trigger == null)
            return;
        trigger.SetDetected(true);
        Debug.Log($"Entered interaction: {trigger.name}");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        InteractionTrigger trigger = other.GetComponent<InteractionTrigger>();

        if (trigger == null)
            return;
        trigger.SetDetected(false);
        Debug.Log($"Exited interaction: {trigger.name}");
    }
}