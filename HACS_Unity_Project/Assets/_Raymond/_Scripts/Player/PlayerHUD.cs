using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private StamBar stamBar;

    [SerializeField] private TextMeshProUGUI currentHealthText;
    [SerializeField] private TextMeshProUGUI maxHealthText;

    [SerializeField] private TextMeshProUGUI currentStamText;
    [SerializeField] private TextMeshProUGUI maxStamText;

    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private GunData gunData;



    public void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        currentHealthText.text = "" + currentHealth;
        //maxHealthText.text = "" + maxHealth;
        
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

    }
    public void UpdateStamUI(int currentEnd, int maxEnd)
    {
        currentStamText.text = "" + currentEnd;

        stamBar.UpdateEndBar(currentEnd, maxEnd);
    }
    
    private  void Update()
    {
        ammoText.text = gunData.currentAmmo + " / " + gunData.magSize;
    }
}
