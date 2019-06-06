﻿using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour
{
    public void SceneLoader(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }

    public void QuitGame()
    {
        //Debug.Log("QUIT!");
        Application.Quit();
    }
}
