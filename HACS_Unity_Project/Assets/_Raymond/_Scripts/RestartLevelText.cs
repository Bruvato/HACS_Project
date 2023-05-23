using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RestartLevelText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameManager gameManager;

    private void Update()
    {
        levelText.text = "LEVEL " + gameManager.GetLevel();
    }
}
