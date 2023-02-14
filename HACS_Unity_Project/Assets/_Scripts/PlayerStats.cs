using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    [SerializeField] private PlayerHUD hud;

    [SerializeField] private int maxStam;
    [SerializeField] private int stam;
    [SerializeField] private int stamRegenRate;
    private float deicmalStam;
    [SerializeField] private bool stamRegen;

    private void Start()
    {
        maxHealth = 100;
        health = maxHealth;
        isDead = false;

        maxStam = 100;
        stam = maxStam;
        stamRegen = true;
        deicmalStam = maxStam;

        checkHealth();
        CheckStam();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            heal(10);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            takeDamge(10);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            DecreaseStam(10);
        }
        
        
        RegenerateStam();
    }

    public override void checkHealth()
    {
        base.checkHealth();
        hud.UpdateHealthUI(health, maxHealth);
    }

    private void CheckStam()
    {
        if (stam <= 0)
        {
            stam = 0;
        }
        if (stam < maxStam)
        {
            stamRegen = true;
        }
        if (stam >= maxStam)
        {
            stam = maxStam;
            stamRegen = false;
        }

        hud.UpdateStamUI(stam, maxStam);

    }

    private void RegenerateStam()
    {
        if (stamRegen)
        {
            deicmalStam += stamRegenRate * Time.deltaTime;
            stam = Mathf.RoundToInt(deicmalStam);
            CheckStam();
        }
    }

    private void DecreaseStam(int amount)
    {
        if (stam > 0)
        {
            stam -= amount;
            deicmalStam = stam;
            CheckStam();

        }
    }


}
