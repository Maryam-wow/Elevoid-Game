using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    #region Buttons
    public Button startButton;
    public Button quitButton;
    //public Button startGameButton;
    public Button settingsButton;
    public Button manifestButton;
    public Button closeSettingsButton;
    public Button backToMainButton;
    //public Button inventoryButton;
    #endregion

    #region Gameobjects
    public GameObject mainScreen;
    public GameObject quizScreen;
    public GameObject selectedCharacterScreen;
    public GameObject rewardDailyScreen;
    public GameObject settingsScreen;
    #endregion


    #region Texts
    public TextMeshProUGUI scoreText;

    #endregion


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        InitializUI();
    }

    public void InitializUI()
    {
        startButton.onClick.AddListener(StartButtonClicked);
        //inventoryButton.onClick.AddListener(InventoryButtonClicked);
        manifestButton.onClick.AddListener(ManifestButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);
        //startGameButton.onClick.AddListener(StartGameClicked);
        closeSettingsButton.onClick.AddListener(CloseSettingsButtonClicked);
        backToMainButton.onClick.AddListener(BackToMainButtonClicked);
        settingsButton.onClick.AddListener(SettingsButtonClicked);
    }

    public void StartButtonClicked()
    {
        rewardDailyScreen.SetActive(true);
        quizScreen.SetActive(true);
        //mainScreen.SetActive(true);
    }
    //public void InventoryButtonClicked()
   // {
   //    SelectionManager.Instance.CharacterTabClicked((CharacterType)GameManager.Instance.selectedCharacter);
   //    selectedCharacterScreen.SetActive(true);
   //}
    public void ManifestButtonClicked()
    {

        //mainScreen.SetActive(true);
        SelectionManager.Instance.CharacterTabClicked((CharacterType)GameManager.Instance.selectedCharacter);
        selectedCharacterScreen.SetActive(true);
        quizScreen.SetActive(false);
        SceneManager.LoadScene("AR");
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    public void EndQuiz()
    {
        SelectionManager.Instance.CharacterTabClicked((CharacterType)GameManager.Instance.selectedCharacter);
        selectedCharacterScreen.SetActive(true);
        quizScreen.SetActive(false);
    }

    //public void StartGameClicked()
    //{
    //   SelectionManager.Instance.CharacterTabClicked((CharacterType)GameManager.Instance.selectedCharacter);
    //  selectedCharacterScreen.SetActive(false);
    //  quizScreen.SetActive(false);
    //  SceneManager.LoadScene("TRY 1");
    // }
    public void CloseSettingsButtonClicked()
    {
        settingsScreen.SetActive(false);
    }
    public void SettingsButtonClicked()
    {
        settingsScreen.SetActive(true);

    }
    public void InventoryGameButtonClicked()
    {
        rewardDailyScreen.SetActive(true);
        selectedCharacterScreen.SetActive(true);
    }
    public void BackToMainButtonClicked()
    {
        selectedCharacterScreen.SetActive(false);
        mainScreen.SetActive(true);
    }

}
