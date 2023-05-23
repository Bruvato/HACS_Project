using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MapGen mapGen;
    [SerializeField] private Transform player;

    private void Update()
    {
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
        mapGen.ChangeCount(1);
        mapGen.Start();
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene("lobby");
    }



}
