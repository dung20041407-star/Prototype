using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private Collider2D hitbox;

    private readonly HashSet<EnemyHealthAndStability> hitEnemies = new();

    private int damage;
    private int stabilityDamage;

    private void Awake()
    {
        hitbox.enabled = false;
    }

    public void StartAttack(int damage, int stabilityDamage)
    {
        this.damage = damage;
        this.stabilityDamage = stabilityDamage;

        hitEnemies.Clear();
        hitbox.enabled = true;
    }

    public void EndAttack()
    {
        hitbox.enabled = false;
        hitEnemies.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hitbox.enabled)
            return;

        EnemyHealthAndStability enemy =
            other.GetComponentInParent<EnemyHealthAndStability>();

        if (enemy == null)
            return;

        if (!hitEnemies.Add(enemy))
            return;

        enemy.TakeDamage(damage, stabilityDamage);

        Debug.Log(
            $"[AttackHitbox] Hit {enemy.name} | " +
            $"Damage: {damage} | Stability: {stabilityDamage}"
        );
    }
}
