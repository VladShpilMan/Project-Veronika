using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Image bar;

    private void Start()
    {
        player.healthChange += TakeDamage;
        //bar.fillAmount = ((bar.fillAmount * 100f * player.Health) / player.MaxHealth) / 100;
        StartCoroutine(FillBar());
    }

    private IEnumerator FillBar()
    {
        //if(player.)
        yield return new WaitForSeconds(0.23f);
        float percentageРealth = bar.fillAmount;
        float subtractPercentage = (player.Health * percentageРealth) / player.MaxHealth;

        bar.fillAmount = subtractPercentage;
    }

    private void TakeDamage(float damage, float health)
    {
        float percentageРealth = bar.fillAmount;
        float subtractPercentage = (damage * percentageРealth) / health;

        bar.fillAmount -= subtractPercentage;
    }
}
