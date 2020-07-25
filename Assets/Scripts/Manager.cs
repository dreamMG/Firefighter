using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Manager : MonoBehaviour
{
    [Space(1)]
    [Header("Attributes")]
    [SerializeField] private SetOnFire setOnFire;
    [SerializeField] private string namePlayer;

    [Header("UI")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private Text startText;
    [SerializeField] private Text objectOnFire;
    [SerializeField] private Text timerText;
    [SerializeField] private Text quantitySourcesText;

    [Header("Win UI")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Text nameText;
    [SerializeField] private Text timeText;

    private float timer;
    private bool isWin;
    private bool isStarted;

    private void Start()
    {
        namePlayer = MenuManager.namePlayer;
        isStarted = true;
        objectOnFire.text = setOnFire.buildings[setOnFire.ran].name + " ON FIRE !!!!";
    }

    private void Update()
    {
        OnStart();
        if (isWin || isStarted) return;

        timer += Time.deltaTime;

        timerText.text = "" + Mathf.Round(timer * 10f) / 10f;

        if ((int)timer % 2 == 0)
        {

            setOnFire.CheckHowManyIsOnFire();
            quantitySourcesText.text = "FIRE PLACE: " + setOnFire.fires.Length;
        }
    }

    private void OnStart()
    {
        if (isStarted)
        {
            timer += Time.deltaTime;

            startText.text = " " + (3 - (int)timer);

            if (timer > 3)
            {
                isStarted = false;
                startPanel.SetActive(false);
                timer = 0;
            }
        }
    }

    public void SetWin()
    {
        isWin = true;
        Save();
        winPanel.SetActive(true);
        nameText.text = "Name: " + namePlayer;
        timeText.text = "Timer: " + Mathf.Round(timer * 100f) / 100f;
    }

    private void Save()
    {
        if (PlayerPrefs.HasKey("Time"))
        {
            int time = PlayerPrefs.GetInt("Time");
            if (timer < time)
            {
                PlayerPrefs.SetString("Lead", namePlayer);
                PlayerPrefs.SetFloat("Time", Mathf.Round(timer * 100f) / 100f);
            }
        } else
        {
            PlayerPrefs.SetString("Lead", namePlayer);
            PlayerPrefs.SetFloat("Time", Mathf.Round(timer * 100f) / 100f);
        }
    }

        

    #region Buttons

    public void ResetButton()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void BackToMenuButton()
    {
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
    }

    #endregion
}
