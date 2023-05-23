using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private MapGen mapGen;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject levelUI;

    [Header("Vars")]
    [SerializeField] private static int level = 1;

    private void Start()
    {
        mapGen.SetCount(1);
        mapGen.SetRows(level * 5);
    }

    private void Update()
    {
        Pause();

        if (player.position.y < -20)
        {
            RestartLevel();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            CompleteLevel();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("levels");
        mapGen.SetCount(1);
        mapGen.Start();
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene("lobby");
        level++;
        mapGen.SetRows(level * 5);

    }

    public int GetLevel()
    {
        return level;
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }



}
