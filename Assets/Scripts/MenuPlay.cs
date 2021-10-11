using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuPlay : MonoBehaviour
{
    public GameObject loadingIndicator;

    public void LoadMainScene()
    {
        loadingIndicator.SetActive(true);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
