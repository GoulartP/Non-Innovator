using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void UIMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Prototype", LoadSceneMode.Single);
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Acabou milho, acabou a pipoca");
    }
}
