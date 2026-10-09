using UnityEngine;
using UnityEngine.InputSystem;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    [Header("Level")]
    [SerializeField] private int level = 1;
    [SerializeField] private int currentExp;

    [Header("Config")]
    [SerializeField] private int expToNextLevel = 1000;
    private float expMultiplier = 1.01f;
    [SerializeField] private int hpPerLevel = 10;
    [SerializeField] private int damagePerLevel = 1;
    [SerializeField] private bool toggleLevelUpHpOrDamage = false;

    public int Level => level;
    public int CurrentExp => currentExp;
    public int ExpToNextLevel => expToNextLevel;

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            LevelUp();
        }
    }
    void LevelUp()
    {
        if (currentExp < expToNextLevel)
        {
            Debug.Log("Not enough experience");
            return;
        }

        currentExp -= expToNextLevel;

        level++;

        if (toggleLevelUpHpOrDamage)
        {
            playerStats.IncreaseMaxHp(hpPerLevel);
        }
        else
        {
            playerStats.IncreaseDamage(damagePerLevel);
        }

        expToNextLevel = Mathf.RoundToInt(expToNextLevel * expMultiplier);

        Debug.Log($"Level up! Current level: {level}");
    }

    public void LoadFromSave(PlayerSaveData data)
    {
        level = data.level;
        currentExp = data.currentExp;
        expToNextLevel = data.expToNextLevel;
    }
}
