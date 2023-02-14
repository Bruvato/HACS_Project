using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private Image back;

    private float targetFillAmt;
    private float currentFillAmt;
    [SerializeField] private float lerpSpeed;

    [SerializeField] private Gradient gradient;
    [SerializeField] private Color green;
    [SerializeField] private Color red;



    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {

        targetFillAmt = currentHealth / maxHealth;
        currentFillAmt = fill.fillAmount;
    }

    private void Update()
    {
        if (currentFillAmt > targetFillAmt) //took damage
        {
            back.color = red;
            fill.fillAmount = targetFillAmt;

            back.fillAmount = Mathf.Lerp(back.fillAmount, targetFillAmt, lerpSpeed * Time.deltaTime);
        }
        if (currentFillAmt < targetFillAmt) //healed
        {
            back.color = green;
            back.fillAmount = targetFillAmt;

            fill.fillAmount = Mathf.Lerp(fill.fillAmount, targetFillAmt, lerpSpeed * Time.deltaTime);
        }

        fill.color = gradient.Evaluate(fill.fillAmount); //fill color
    }
}
