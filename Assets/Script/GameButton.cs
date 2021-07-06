using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameButton : MonoBehaviour
{
    




    public void StartButton()
    {
        SceneManager.LoadScene("MainScene");
        if(GameManger.instance.GetPlayerStat()== null)
        {
            SLManager.Load();
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void HomeButton()
    {
        SceneManager.LoadScene("IntroScene");
    }
}
