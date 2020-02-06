using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameMaster : MonoBehaviour
{
    public TextMeshProUGUI score;

    public GameObject restartPanel;
    public GameObject winningPanel;
    public GameObject tinyPlanet5, tinyPlanet6, tinyPlanet7, tinyPlanet8;

    private bool hasLost;
    private bool hasToWait;

    private int i = 0;

    bool isMute;

    public int counterForBoom = 0;

    private void Start()
    {
        if (PlayerPrefs.GetInt("sumPlanets") != null && PlayerPrefs.GetInt("sumPlanets") == 0)
        {
            return;
        }
        else if (PlayerPrefs.GetInt("sumPlanets") == 1)
        {
            tinyPlanet5.SetActive(true);
            tinyPlanet6.SetActive(true);
        } else if (PlayerPrefs.GetInt("sumPlanets") == 2)
        {
            tinyPlanet5.SetActive(true);
            tinyPlanet6.SetActive(true);
            tinyPlanet7.SetActive(true);
            tinyPlanet8.SetActive(true);
        }
    }

    private void Update()
    {
        if (hasLost == false && hasToWait == false)
        {
            Invoke("UpdateCounter", 1.0f);
            hasToWait = true;
            //score.text = Time.time.ToString("F0");
        }
    }

    private void UpdateCounter()
    { 
        if (hasLost == false)
        {
            if (i >= 8 && SceneManager.GetActiveScene().buildIndex == 1)
            {
                winningPanel.SetActive(true);
                //StartLevel2();
            } else if (i >= 6 && SceneManager.GetActiveScene().buildIndex == 2)
            {
                winningPanel.SetActive(true);
                //StartLevel3();
            } else if (i >= 6 && SceneManager.GetActiveScene().buildIndex == 3)
            {
                winningPanel.SetActive(true);
                //StartLevel3();
            }

            i += 1;
            score.text = i.ToString();
            hasToWait = false;
        }
    }

    public void GoToGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void GameOver()
    {
        hasLost = true;
        //restartPanel.SetActive(true);
        Invoke("Delay", 1.5f);
    }

    void Delay()
    {
        restartPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void StartLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void StartLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void StartLevel4()
    {
        SceneManager.LoadScene("Level4");
    }

    public void Mute()
    {
        isMute = !isMute;

        AudioListener.volume = isMute ? 0 : 1;
    }

    public void SetDropDownEntry(TMP_Dropdown dropDown)
    {
        if(PlayerPrefs.GetInt("sumPlanets") != null)
        {
            dropDown.value = PlayerPrefs.GetInt("sumPlanets");
        }
    }

    public void OnDropDownChanged(TMP_Dropdown dropDown)
    {
        PlayerPrefs.SetInt("sumPlanets", dropDown.value);
        dropDown.RefreshShownValue();
    }
}