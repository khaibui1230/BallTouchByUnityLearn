using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    

    public void StartSence()
    {
        SceneManager.LoadScene("main");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

   
}
