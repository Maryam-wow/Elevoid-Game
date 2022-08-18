using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager Instance;

    #region  non-Binary data
    public PlayerData mData;
    #endregion


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            mData = new PlayerData();
            DontDestroyOnLoad(gameObject);
        }

        LoadLocal();
    }

    public void LoadLocal()
    {
        if (File.Exists(Application.persistentDataPath + "/playerData.data"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.OpenRead(Application.persistentDataPath + "/playerData.data");
            mData = (PlayerData)bf.Deserialize(file);
            file.Close();
        }
    }

    public void SaveLocal()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;

        if (File.Exists(Application.persistentDataPath + "/playerData.data"))
        {
            file = File.OpenWrite(Application.persistentDataPath + "/playerData.data");
        }
        else
        {
            file = File.Create(Application.persistentDataPath + "/playerData.data");
        }

        bf.Serialize(file, mData);
        file.Close();
    }

    private void OnApplicationQuit()
    {
        SaveLocal();
    }

}
