using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using NativeWebSocket;
using SimpleJSON;
using System.Text;


public class WebSocketConnection : MonoBehaviour
{
    private WebSocket _webSocket;
    private string _serverUrl = "ws://elevoid.uber.space:42960/nodejs-server";
    private int _serverErrorCode;


    //////////////////////////////////
    // MonoBehaviour Lifecycle Event Handlers
    //////////////////////////////////

    void Start()
    {
        _webSocket = new WebSocket(_serverUrl);

        _webSocket.OnOpen += OnOpen;
        _webSocket.OnMessage += OnMessage;
        _webSocket.OnClose += OnClose;
        _webSocket.OnError += OnError;
    }
    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        _webSocket.DispatchMessageQueue();
#endif
    }
    private async void OnApplicationQuit()
    {
        await _webSocket.Close();
    }

    // Event Handlers

    //  if (json["classType"].Value == PlayerUpdatePackage.classType)
    //{
    //    PlayerUpdatePackage playerData = JsonUtility.FromJson<PlayerUpdatePackage>(inboundString);
    //  print($"Received PlayerUpdatePackage || name: {playerData.id}, //health: {playerData.health};");
    // }
    //                    else if (json["classType"].Value == LevelDataPackage.classType)
    // {
    // LevelDataPackage levelData = JsonUtility.FromJson<LevelDataPackage>(inboundString);
    // print($"Received LevelDataPackage || name: {levelData.mapTitle}, level: {levelData.difficultyLevel}");
    private void OnOpen()
    {
        print("Connection opened");
        GameManager.Instance.OnServerConnectionOpened();
       // Invoke("SendPlayerJoinedPackage", 0f);
    }

    private void OnMessage(byte[] inboundBytes)
    {
        print("Message received");
        string inboundString = System.Text.Encoding.UTF8.GetString(inboundBytes);
        print($"Maryam inboundString: {inboundString}");

        if (int.TryParse(inboundString, out _serverErrorCode))
        {
            // If server returns an integer, it is an error
            print($"Server Error: {_serverErrorCode}");
        }
        else
        {
            JSONNode json = JSON.Parse(inboundString);

            if (json["packageType"])
            {

                if (json["packageType"].Value == "PlayerJoinedPackage")
                {
                    // Received PlayerJoinedPackage
                    print($"Received PlayerJoinedPackage");
                    PlayerJoinedPackage playerJoinedPackage = JsonUtility.FromJson<PlayerJoinedPackage>(inboundString);
                    GameManager.Instance.DidReceivePlayerJoinedPackage(playerJoinedPackage);
                }

                else if (json["packageType"].Value == "GameUpdatePackage")
                {
                    // Received GameUpdatePackage
                    print($"Received GameUpdatePackage");
                    GameUpdatePackage gameUpdatePackage = JsonUtility.FromJson<GameUpdatePackage>(inboundString);
                    GameManager.Instance.DidReceiveGameUpdatePackage(gameUpdatePackage);
                }
                else if (json["packageType"].Value == "PlayerLeftPackage")
                {
                    // Received PlayerLeftPackage
                    print($"Received PlayerLeftPackage");
                    PlayerLeftPackage playerLeftPackage = JsonUtility.FromJson<PlayerLeftPackage>(inboundString);
                    GameManager.Instance.DidReceivePlayerLeftPackage(playerLeftPackage);
                }
                else if (json["packageType"].Value == "PlayerMovedPackage")
                {
                    // Received PlayerMovedPackage
                    print($"Received PlayerMovedPackage");
                    PlayerMovedPackage playerMovedPackage = JsonUtility.FromJson<PlayerMovedPackage>(inboundString);
                    GameManager.Instance.DidReceivePlayerMovedPackage(playerMovedPackage);
                }
                else if (json["packageType"].Value == "PlayerShotPackage")
                {
                    // Received PlayerShotPackage
                    print($"Received PlayerShotPackage");
                    PlayerShotPackage playerShotPackage = JsonUtility.FromJson<PlayerShotPackage>(inboundString);
                    GameManager.Instance.DidReceivePlayerShotPackage(playerShotPackage);
                }
            }
        }
    }

    private void OnClose(WebSocketCloseCode closeCode)
    {
        print($"Connection closed: {closeCode}");
        GameManager.Instance.OnServerConnectionClosed();
    }

    private void OnError(string errorMessage)
    {
        print($"Connection error: {errorMessage}");
    }


    //////////////////////////////////
    // Public Methods
    //////////////////////////////////

    public async void ConnectToServer()
    {
        await _webSocket.Connect();
    }

    public async void DisconnectFromServer()
    {
        await _webSocket.Close();
    }

    public async void SendPlayerJoinedPackage(PlayerPackage player)
    {
        PlayerJoinedPackage package = new PlayerJoinedPackage(player);

        if (_webSocket.State == WebSocketState.Open)
        {
            string json = JsonUtility.ToJson(package);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            await _webSocket.Send(bytes);
        }
    }

    public async void SendPlayerMovedPackage(PlayerPackage player)
    {
        PlayerMovedPackage package = new PlayerMovedPackage(player);

        if (_webSocket.State == WebSocketState.Open)
        {
            string json = JsonUtility.ToJson(package);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            await _webSocket.Send(bytes);
        }
    }

    public async void SendPlayerShotPackage(PlayerPackage player)
    {
        PlayerShotPackage package = new PlayerShotPackage(player);

        if (_webSocket.State == WebSocketState.Open)
        {
            string json = JsonUtility.ToJson(package);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            await _webSocket.Send(bytes);
        }
    }

    public async void SendGameUpdatePackage(Game game)
    {
        GameUpdatePackage package = new GameUpdatePackage(game);

        if (_webSocket.State == WebSocketState.Open)
        {
            string json = JsonUtility.ToJson(package);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            await _webSocket.Send(bytes);
        }
    }
    //////////////////////////////////
    // Helper Methods
    //////////////////////////////////

  //  private async void SendPlayerJoinedPackage()
    //{
    //    float CurrentpositionX = 2f;
      //  float CurrentpositionY = 2f;

      //  float CurrentpositionZ = 2f;
     //   int id = 222;
     //  PlayerPackage playerPackage = new PlayerPackage(id, CurrentpositionX, CurrentpositionY, CurrentpositionZ);

       // if (_webSocket.State == WebSocketState.Open)

       // {
         //   string json = JsonUtility.ToJson(playerPackage);
          //  byte[] bytes = Encoding.UTF8.GetBytes(json);
          //  await _webSocket.Send(bytes);
        //}
   // }
}

