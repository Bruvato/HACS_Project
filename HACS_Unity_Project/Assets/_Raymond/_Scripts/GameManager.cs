using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private void Update()
    {
        // if (player.transform.position.y < -20)
        // {
        //     RestartLevel();
        // }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene("levels");

    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene("lobby");
    }

    public void StartLevel()
    {
        Debug.Log("lvl started");
    }


}
