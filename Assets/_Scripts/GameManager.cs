using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


using TMPro;

public class GameManager : GenericSingletonClass<GameManager>
{
    int score;
    public GameObject _playerGameObject;

    //private Dictionary<int, GameObject> _playerGameObjects = new Dictionary<int, GameObject>();


    public List<CharacterInfo> charactersInfo;

    public List<AccessoryInfo> earsInfo;
    public List<AccessoryInfo> eyesInfo;
    public List<AccessoryInfo> facesInfo;
    public List<AccessoryInfo> headsInfo;
    public List<AccessoryInfo> mouthsInfo;

    public int selectedCharacter;

    public List<AccessoryInfo> rewardsList;

    [Header("Self-component references")]
    [SerializeField] private GameSceneManager gameSceneManager;

    void Start()
    {
        selectedCharacter = (int)DataManager.Instance.mData.selectedCharacter;
        // Create a Player
        int playerId = Random.Range(0, 1000000);
    }

    public void IncreaseScore(int value)
    {
        score += value;
        UIManager.Instance.UpdateScoreText(score);
    }

    public void EndGame()
    {
        if (score > DataManager.Instance.mData.health)
        {
            DataManager.Instance.mData.health = score;
            DataManager.Instance.SaveLocal();
        }
    }

    public void SetGameOver()
    {
        GameObject.FindWithTag("GameOverMenu").transform.GetChild(0).gameObject.SetActive(true);
    }

    public void SetCharacter(CharacterType characterType)
    {
        selectedCharacter = (int)characterType;
        DataManager.Instance.mData.selectedCharacter = characterType;
    }


    public Sprite GetSelectedCharacterSprite()
    {
        return charactersInfo[selectedCharacter].characterPhoto;
    }


    public Sprite GetCharacterSprite(CharacterType characterType)
    {
        return charactersInfo[(int)characterType].characterPhoto;
    }
    public void RewardUser(int rewardCount)
    {
        int tomorrowReward = rewardCount < rewardsList.Count - 1 ? tomorrowReward = rewardCount + 1
            : tomorrowReward = 0;// TODO close tomorrowPhoto
        UnlockAccessory(rewardsList[rewardCount].name, rewardsList[rewardCount].accessoriesType);
        DailyReward.Instance.RewardUser(rewardsList[rewardCount].accessoryUIPhoto,
        rewardsList[tomorrowReward].accessoryUIPhoto);
        Debug.Log("Rewarding User: " + rewardCount + " : "
            + rewardsList[rewardCount].name);
    }
    public void UnlockAccessory(string id, AccessoriesType type)
    {
        List<AccessoryInfo> selectedList = new List<AccessoryInfo>();

        if (type == AccessoriesType.Ears)
        {
            selectedList = earsInfo;
        }
        else if (type == AccessoriesType.Eyes)
        {
            selectedList = eyesInfo;
        }
        else if (type == AccessoriesType.Faces)
        {
            selectedList = facesInfo;
        }
        else if (type == AccessoriesType.Heads)
        {
            selectedList = headsInfo;
        }
        else if (type == AccessoriesType.Mouths)
        {
            selectedList = mouthsInfo;
        }


        for (int i = 0; i < selectedList.Count; i++)
        {
            if (selectedList[i].name == id)
            {
                selectedList[i].unlocked = true;//TODO binary saving operation
                Debug.Log("Unlocked " + id);
                return;
            }
        }

        Debug.Log("Did not unlock anything verify operation");
    }

    public void LoadGameplay()
    {
        gameSceneManager.LoadScene(selectedCharacter);
        Start();
    }
}
