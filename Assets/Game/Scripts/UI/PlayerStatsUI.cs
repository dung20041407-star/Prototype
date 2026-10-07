using UnityEngine;
using TMPro;
public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text HP;
    [SerializeField] private TMP_Text damage;
    [SerializeField] private TMP_Text defense;
    [SerializeField] private PlayerStats playerStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        UpdateStats();
    }
    private void Update()
    {
        UpdateStats();
    }
    private void UpdateStats()
    {
        HP.text = $"HP: {playerStats.currentHp}";
        damage.text = $"ATK: {playerStats.hpDamage}";
        defense.text = $"DEF: {playerStats.defense}";
    }
}
