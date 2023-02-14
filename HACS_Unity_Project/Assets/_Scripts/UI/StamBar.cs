using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StamBar : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private Image back;

    private float targetFillAmt;
    private float currentFillAmt;
    [SerializeField] private float lerpSpeed;

    public void UpdateEndBar(float currentHealth, float maxHealth)
    {
        targetFillAmt = currentHealth / maxHealth;
        currentFillAmt = fill.fillAmount;
    }
    
    private void Update()
    {
        if (currentFillAmt > targetFillAmt) //took damage
        {
            fill.fillAmount = targetFillAmt;

            back.fillAmount = targetFillAmt;
        }
        if (currentFillAmt < targetFillAmt) //healed
        {
            back.fillAmount = targetFillAmt;

            fill.fillAmount = targetFillAmt;
        }
    }

}
