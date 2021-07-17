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
    }

    private void TakeDamage(float damage, float health)
    {
        float percentageРealth = bar.fillAmount;
        float subtractPercentage = (damage * percentageРealth) / health;

        bar.fillAmount -= subtractPercentage;
    }
}
