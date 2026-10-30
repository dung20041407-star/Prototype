using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private readonly HashSet<EnemyHealthAndStability> enemiesInside = new();

    public bool HasEnemy => enemiesInside.Count > 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealthAndStability enemy =
            other.GetComponentInParent<EnemyHealthAndStability>();

        if (enemy != null && enemiesInside.Add(enemy))
        {
            Debug.Log($"[DamageZone] Detect: {enemy.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EnemyHealthAndStability enemy =
            other.GetComponentInParent<EnemyHealthAndStability>();

        if (enemy != null && enemiesInside.Remove(enemy))
        {
            Debug.Log($"[DamageZone] Undetect: {enemy.name}");
        }
    }
}
