using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance
    {
        get
        {
            return FindObjectOfType<GameManager>();
        }
    }

    //int yourPlayerid;
    int score;
    public WebSocketConnection _webSocket;
    public GameObject playerPrefab;

    private PlayerPackage _player;
    private Game _game;
    private GameObject _playerGameObject;

    private Dictionary<int, GameObject> _playerGameObjects = new Dictionary<int, GameObject>();


    public List<CharacterInfo> charactersInfo;

    public List<AccessoryInfo> earsInfo;
    public List<AccessoryInfo> eyesInfo;
    public List<AccessoryInfo> facesInfo;
    public List<AccessoryInfo> headsInfo;
    public List<AccessoryInfo> mouthsInfo;

    public int selectedCharacter;

    public GameObject GetPlayerGameObject()
    {
        return _playerGameObject;
    }
    public PlayerPackage GetPlayer()
    {
        return _player;
    }

    public List<AccessoryInfo> rewardsList;

    void Awake()
    {
        if (Instance == null) // If there is no instance already
        {
            DontDestroyOnLoad(gameObject); // Keep the GameObject, this component is attached to, across different scenes
            //Instance = this;
        }
        else if (Instance != this) // If there is already an instance and it's not `this` instance
        {
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
        }
    }

    void Start()
    {
        selectedCharacter = (int)DataManager.Instance.mData.selectedCharacter;
        // Create a Player
        int playerId = Random.Range(0, 1000000);

        //vector3 position input
        Vector3 playerPosition = Vector3.zero;
        _player = new PlayerPackage(playerId, playerPosition.x, playerPosition.y, playerPosition.z);
        print("Player created: " + _player);
        // Create health
        // Create a Game
        _game = new Game();
        _game.players.Add(_player);

        // Instantiate the Player's GameObject
        _playerGameObject = Instantiate(playerPrefab, _player.currentPosition, Quaternion.identity);
        print("Player GameObject created: " + _playerGameObject);

        // Store GameObject reference in dictionary
        _playerGameObjects.Add(_player.id, _playerGameObject);


        // Establish server connection
        _webSocket.ConnectToServer();
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

    public void SetCharacter(CharacterType characterType)
    {
        selectedCharacter = (int)characterType;
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
    // public void OnStartButtonPress()

    //{
    //Load Scene//
    // }

    void OnApplicationQuit()
    {
        _webSocket.DisconnectFromServer();
    }



    ////////////////////////
    // User Input Events
    ////////////////////////

    // public void OnPlayerMovementUpdate(Vector3 Currentposition)
    //{


    //PlayerUpdatePackage playerUpdatePackage = new PlayerUpdatePackage(string _name, int _health, int _id, float _x, float _y, float _z)
    //_webSocket.SendPlayerMovedPackage(_player);


    //vector3 position input//
    //create player updatepackage
    //Send playerupdatepackagetoserver
    // }

    public void DidReceiveMoveInput(Vector3 newPosition)
    {
        float moveSpeed = 0.2f;

        // Update the player's position based on input
        //_player.currentPosition = newPosition;
        //_playerGameObjects[_player.id].transform.position = newPosition;
        _playerGameObjects[_player.id].transform.transform.Translate(newPosition * moveSpeed * Time.deltaTime, Space.World);

        // Send a PlayerMovedPackage
        _webSocket.SendPlayerMovedPackage(_player);
    }

    public void DidReceiveShotInput()
    {
        // Trigger shot for player
        TriggerPlayerShot(_player);

        // Send PlayerShotPackage
        _webSocket.SendPlayerShotPackage(_player);
    }



    ////////////////////////
    // Game Management
    ////////////////////////
    private void UpdateGame(Game game)
    {
        // Check for any players that are new or left
        List<PlayerPackage> newPlayers = new List<PlayerPackage>();
        List<PlayerPackage> missingPlayers = new List<PlayerPackage>();

        // Check for new players and add them to the newPlayers list
        foreach (PlayerPackage p in game.players)
        {
            bool isAlreadyInGame = false;
            foreach (PlayerPackage pl in _game.players)
            {
                if (p.id == pl.id)
                {
                    isAlreadyInGame = true;
                }
            }
            if (!isAlreadyInGame)
            {
                newPlayers.Add(p);
            }
        }

        // Check for missing players and add them to the missingPlayers list
        foreach (PlayerPackage pl in _game.players)
        {
            bool isStillInGame = false;
            foreach (PlayerPackage p in game.players)
            {
                if (pl.id == p.id)
                {
                    isStillInGame = true;
                }
            }
            if (!isStillInGame)
            {
                missingPlayers.Add(pl);
            }
        }

        // Then create a GameObject for new player & update the playerGameObjects dictionary & _game Game accordingly 
        foreach (PlayerPackage newPlayer in newPlayers)
        {
            GameObject newPlayerGameObject = Instantiate(playerPrefab, newPlayer.currentPosition, Quaternion.identity);
            _playerGameObjects.Add(newPlayer.id, newPlayerGameObject);
            _game.players.Add(newPlayer);
        }

        // Remove players that have left and their gameObjects from the game
        foreach (PlayerPackage missingPlayer in missingPlayers)
        {
            Destroy(_playerGameObjects[missingPlayer.id]);
            _playerGameObjects.Remove(missingPlayer.id);
            _game.players.RemoveAll(p => p.id == missingPlayer.id);
        }
    }



    public void UpdatePlayerPosition(PlayerPackage player)
    {
        // Update player's position
        _playerGameObjects[player.id].transform.position = player.currentPosition;
    }

    private void TriggerPlayerShot(PlayerPackage player)
    {
        // Trigger a shot for this specific player

        //  _playerGameObjects[player.id].GetComponent<CharacterWaterControl>().WaterBall();
    }


    ////////////////////////
    // Server Events
    ////////////////////////

    public void OnServerConnectionOpened()
    {
        _webSocket.SendPlayerJoinedPackage(_player);

    }

    public void OnServerConnectionClosed()
    {

        // Try to re-establish a server connection
    }

    public void DidReceiveGameUpdatePackage(GameUpdatePackage package)
    {
        UpdateGame(package.game);
    }

    public void DidReceivePlayerJoinedPackage(PlayerJoinedPackage package)
    {
        // Add player to Game _game
        _game.players.Add(package.player);

        // Send out a GameUpdate package with updated _game
        _webSocket.SendGameUpdatePackage(_game);
    }

    public void DidReceivePlayerLeftPackage(PlayerLeftPackage package)
    {
        // Remove player from Game _game
        _game.players.RemoveAll(player => player.id == package.player.id);

        // Send out a GameUpdate package with updated _game
        _webSocket.SendGameUpdatePackage(_game);
    }

    public void DidReceivePlayerMovedPackage(PlayerMovedPackage package)
    {
        UpdatePlayerPosition(package.player);
    }

    public void DidReceivePlayerShotPackage(PlayerShotPackage package)
    {
        TriggerPlayerShot(package.player);
    }
}
