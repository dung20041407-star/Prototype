using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyHealthAndStability : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHp = 100;
    [SerializeField] private int currentHp;

    [Header("Stability")]
    [SerializeField] private int maxStability = 100;
    [SerializeField] private int currentStability;

    [Header("Damage Flash")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color damageColor = new Color(1f, 0.5f, 0.5f, 1f);
    [SerializeField] private float flashDuration = 0.1f;

    private Color originalColor;
    private Coroutine flashCoroutine;
    public int CurrentHp => currentHp;
    public int MaxHp => maxHp;
    public int CurrentStability => currentStability;
    public int MaxStability => maxStability;

    public bool IsDead { get; private set; }
    public bool IsKnockedDown { get; private set; }

    private void Awake()
    {
        currentHp = maxHp;
        currentStability = maxStability;

        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            TakeDamage(100, 100);
        }
    }

    public void TakeDamage(int damage, int stabilityDamage)
    {
        if (IsDead)
            return;
        FlashDamage();

        currentHp = Mathf.Max(0, currentHp - damage);
        currentStability = Mathf.Max(0, currentStability - stabilityDamage);

        if (currentHp <= 0)
        {
            Die();
        }
        else if (currentStability <= 0)
        {
            KnockDown();
        }
    }

    private void Die()
    {
        IsDead = true;

        Debug.Log($"{gameObject.name} died.");

        DestroySelf();
    }

    private void KnockDown()
    {
        IsKnockedDown = true;
        Debug.Log($"{gameObject.name} was knocked down.");
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
    private void FlashDamage()
    {
        if (spriteRenderer == null)
            return;

        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(FlashDamageCoroutine());
    }

    private System.Collections.IEnumerator FlashDamageCoroutine()
    {
        float timer = 0f;

        spriteRenderer.color = damageColor;

        while (timer < flashDuration)
        {
            timer += Time.deltaTime;

            float t = Mathf.Clamp01(timer / flashDuration);

            spriteRenderer.color = Color.Lerp(
                damageColor,
                originalColor,
                t
            );

            yield return null;
        }

        spriteRenderer.color = originalColor;
        flashCoroutine = null;
    }
}
