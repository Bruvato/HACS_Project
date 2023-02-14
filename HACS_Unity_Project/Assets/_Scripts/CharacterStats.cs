using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int health;

    [SerializeField] protected bool isDead;

    private void Start()
    {
        maxHealth = 100;
        health = maxHealth;
        isDead = false;
    }

    public virtual void checkHealth() //checks if dead or health over max
    {
        if (health <= 0)
        {
            health = 0;
            die();
        }
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public void die()
    {
        isDead = true;
    }
    private void setHealth(int newHealth)
    {
        health = newHealth;
        checkHealth();
    }
    public void takeDamge(int damageAmount)
    {
        health -= damageAmount;
        checkHealth();
    }
    public void heal(int healAmount)
    {
        health += healAmount;
        checkHealth();
    }

}
