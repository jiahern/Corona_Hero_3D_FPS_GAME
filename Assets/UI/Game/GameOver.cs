using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{   
    public void RestartButton()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("Menu Scene");
    }
}




