using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private GameObject levelUI;

    private void OnTriggerEnter()
    {
        levelUI.SetActive(true);
    }
}
