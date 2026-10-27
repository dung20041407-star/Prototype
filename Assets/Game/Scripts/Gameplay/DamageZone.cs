using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int damage = 100;
    [SerializeField] private int stabilityDamage = 100;
    [SerializeField] private float damageInterval = 1f;

    private readonly HashSet<EnemyHealthAndStability> enemiesInside = new();

    private float attackCooldown;

    private void Update()
    {
        if (attackCooldown > 0f)
        {
            attackCooldown -= Time.deltaTime;
            return;
        }

        if (enemiesInside.Count == 0)
            return;

        Attack();
    }

    private void Attack()
    {
        foreach (EnemyHealthAndStability enemy in new List<EnemyHealthAndStability>(enemiesInside))
        {
            if (enemy == null)
            {
                enemiesInside.Remove(enemy);
                continue;
            }

            enemy.TakeDamage(damage, stabilityDamage);
        }

        attackCooldown = damageInterval;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealthAndStability enemy = other.GetComponentInParent<EnemyHealthAndStability>();

        if (enemy != null && enemiesInside.Add(enemy))
        {
            Debug.Log($"[DamageZone] Detect: {enemy.name}");

            if (attackCooldown <= 0f)
            {
                Attack();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EnemyHealthAndStability enemy = other.GetComponentInParent<EnemyHealthAndStability>();

        if (enemy != null && enemiesInside.Remove(enemy))
        {
            Debug.Log($"[DamageZone] Undetect: {enemy.name}");
        }
    }
}
