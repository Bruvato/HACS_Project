using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private WeaponSwitching weaponSwitching;

    [Header("Player stats")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private StamBar stamBar;

    [SerializeField] private TextMeshProUGUI currentHealthText;
    [SerializeField] private TextMeshProUGUI maxHealthText;

    [SerializeField] private TextMeshProUGUI currentStamText;
    [SerializeField] private TextMeshProUGUI maxStamText;

    [Header("Weapon")]
    [SerializeField] private GunData[] gunDataList;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private Image reloadCricle;


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
        int i = weaponSwitching.getSelectedWeapon();
        weaponName.text = "" + gunDataList[i].name;
        ammoText.text = gunDataList[i].currentAmmo + " / " + gunDataList[i].magSize;

        if (gunDataList[i].reloading)
        {
            reloadCricle.fillAmount = Mathf.MoveTowards(reloadCricle.fillAmount, 1, Time.deltaTime / gunDataList[i].reloadTime);
        }
        else
        {
            reloadCricle.fillAmount = 0;
        }
    }
}
