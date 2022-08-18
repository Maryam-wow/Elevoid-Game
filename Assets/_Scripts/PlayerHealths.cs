using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealths : MonoBehaviour
{
    public int maxHealth = 100;
    public int playerHealth;
    public HealthBar healthBar;

    void Start()
    {
        playerHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
    }
public void PlayerTakeDamage(int damageAmount)

    {
    playerHealth = playerHealth - damageAmount;
    healthBar.SetHealth(playerHealth);

        if (playerHealth <= 0)
    {
        Destroy(gameObject);
    }
}
}

