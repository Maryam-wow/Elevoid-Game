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
    public Button SinglePlayerButton;
    public Button quitButton;
    //public Button startGameButton;
    public Button settingsButton;
    public Button manifestButton;
    public Button closeSettingsButton;
    public Button backToMainButton;
    public Button backFromCreditsButton;
    public Button backFromEncyclopediaButton;
    public Button backFromQuizButton;
    public Button inventoryButton;
    public Button encyclopediaButton;
    public Button creditsButton;
    #endregion

    #region Gameobjects
    public GameObject mainScreen;
    public GameObject quizScreen;
    public GameObject selectedCharacterScreen;
    public GameObject rewardDailyScreen;
    public GameObject settingsScreen;
    public GameObject encyclopediaScreen;
    public GameObject creditsScreen;
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
        SinglePlayerButton.onClick.AddListener(SinglePlayerClicked);
        inventoryButton.onClick.AddListener(InventoryButtonClicked);
        backToMainButton.onClick.AddListener(BackToMainButtonClicked);
        encyclopediaButton.onClick.AddListener(EncyclopediaButtonClicked);
        manifestButton.onClick.AddListener(ManifestButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);
        creditsButton.onClick.AddListener(CreditsButtonClicked);
        backFromQuizButton.onClick.AddListener(BackToMainButtonClicked);
        backFromCreditsButton.onClick.AddListener(BackToMainButtonClicked);
        backFromEncyclopediaButton.onClick.AddListener(BackToMainButtonClicked);
        closeSettingsButton.onClick.AddListener(CloseSettingsButtonClicked);
        backToMainButton.onClick.AddListener(BackToMainButtonClicked);
        settingsButton.onClick.AddListener(SettingsButtonClicked);
    }

    public void SinglePlayerClicked()
    {
        rewardDailyScreen.SetActive(true);
        quizScreen.SetActive(true);
        //mainScreen.SetActive(true);
    }
    public void InventoryButtonClicked()
    {
       //SelectionManager.Instance.CharacterTabClicked((CharacterType)GameManager.Instance.selectedCharacter);
       selectedCharacterScreen.SetActive(true);
   }
    public void ManifestButtonClicked()
    {

        //mainScreen.SetActive(true);
        SelectionManager.Instance.CharacterTabClicked((CharacterType)GameManager.Instance.selectedCharacter);
        selectedCharacterScreen.SetActive(true);
        quizScreen.SetActive(false);
    }
    public void EncyclopediaButtonClicked()
    {
        encyclopediaScreen.SetActive(true);
    }
    public void CreditsButtonClicked()
    {
        creditsScreen.SetActive(true);
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
        quizScreen.SetActive(false);
        encyclopediaScreen.SetActive(false);
        selectedCharacterScreen.SetActive(false);
        creditsScreen.SetActive(false);
        mainScreen.SetActive(true);
    }

}
