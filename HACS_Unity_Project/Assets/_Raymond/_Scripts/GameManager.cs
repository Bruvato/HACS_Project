using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            RestartLevel();
        }
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
        SceneManager.LoadScene("levels");
    }


}
