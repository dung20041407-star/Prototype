using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPBarUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private Image fillBackgroundImage;
    [SerializeField] private PlayerStats playerStats;

    private Coroutine hpCoroutine;

    private void Start()
    {
        playerStats.OnHPChanged += UpdateHP;
        UpdateHP();
    }

    private void OnDestroy() { playerStats.OnHPChanged -= UpdateHP; }
    public void UpdateHP()
    {
        float targetHP = (float)playerStats.currentHp/playerStats.maxHp;
        if (hpCoroutine != null)
        {
            StopCoroutine(hpCoroutine);
        }
        fillBackgroundImage.fillAmount = targetHP;
        hpCoroutine = StartCoroutine(AnimateHP(targetHP));
    }
    private IEnumerator AnimateHP(float targetHP)
    {
        float startHP = fillImage.fillAmount;
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;

            fillImage.fillAmount = Mathf.Lerp(startHP, targetHP, t);

            yield return null;
        }

        fillImage.fillAmount = targetHP;
    }
}
