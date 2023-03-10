using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{

    [Header("UIS")]
    [SerializeField] private GameObject tapToPlayUI;
    [SerializeField] private GameObject nextLvlUI;
    [SerializeField] private GameObject restartLvlUI;
    [SerializeField] private GameObject nextLevelButton;
    [Header("Texts")]

    [SerializeField] private TMP_Text currentLvl;
    [SerializeField] private TMP_Text nextLvlButtonText;
    [SerializeField] private TMP_Text StatusText;

    public bool isPaused;
    private void Start()
    {
        isPaused = true;
        LevelText();
    }
    public void PlayResButton()
    {
        if (tapToPlayUI.activeSelf)
        {
            tapToPlayUI.SetActive(false);
            isPaused = false;
            PlayerManagement.Instance.CanRun();
        }
        if (nextLvlUI.activeSelf)
        {
            nextLvlUI.SetActive(false);
            isPaused = false;

           
            LevelManager.Instance.LevelUp();
            LevelText();
            PlayerManagement.Instance.CharacterReset();
            LevelManager.Instance.GenerateCurrentLevel();
        }
        if (restartLvlUI.activeSelf)
        {
            restartLvlUI.SetActive(false);
            isPaused = false;
            Debug.Log("Restarted");

            PlayerManagement.Instance.CharacterReset();
            LevelManager.Instance.GenerateCurrentLevel();

        }



    }//PlayResButton
    public void LevelText()
    {
        int LevelInt = LevelManager.Instance.GetGlobalLevelIndex() + 1;
        currentLvl.text = "Level " + LevelInt;
    }
    public void NextLvlUI()
    {
        if (!isPaused)
        {
            tapToPlayUI.SetActive(false);
            nextLvlUI.SetActive(true);
            isPaused = true;
            NextLvl();
            SetStatusText();
        }

    }//NextLvlUI
    public void RestartButtonUI()
    {
        if (!isPaused)
        {
            restartLvlUI.SetActive(true);

            isPaused = true;
        }
    }//restartButton
    public void TapToPlay()
    {
        if (!isPaused)
        {
            tapToPlayUI.SetActive(true);
            isPaused = true;
        }
    }

    void SetStatusText() {
        int status = PlayerManagement.Instance.GetStatus();
        if (status == 0 || status == 1)
            StatusText.text = "low";
        else if (status == 2)
            StatusText.text = "average";
        else
            StatusText.text = "high";
    }
    void NextLvl()
    {
        nextLevelButton.SetActive(true);

    }

}
