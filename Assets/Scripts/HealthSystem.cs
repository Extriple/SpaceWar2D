using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler onDamage;
    public event EventHandler onDie;
    [SerializeField] private int healthMaxAmount;
    
    private int healthAmount;
    

    private void Awake()
    {
        healthAmount = healthMaxAmount;
    }

    public void Damage(int damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthMaxAmount);
        
        onDamage?.Invoke(this, EventArgs.Empty);
        if (isDead())
        {
            onDie?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool isDead()
    {
        return healthAmount == 0; 
    }

    public bool isFullHealth()
    {
        return healthAmount == healthMaxAmount;
    }

    public int GetHealthAmount()
    {
        return healthAmount;
    }

    public float GetHealthAmountNormalized()
    {
        return (float) healthAmount / healthMaxAmount;
    }

    public void SetHealthAmountMax (int healthAmoumtMax, bool updateHealAmount)
    {
        this.healthMaxAmount = healthAmoumtMax;
        if (updateHealAmount)
        {
            healthAmount = healthAmoumtMax;
        }
    }
}
