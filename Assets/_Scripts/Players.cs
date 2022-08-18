using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    //Public Variables
    public GameObjectType type;
    public string title;
    public bool _isStandardMaterial;
    public bool _isOtherMaterial;
    public GameObject visual;

    //Private Variables
    private SettingsMultiplayer _settingsMultiplayer;
    private Renderer _renderer;
    private int _lifePoints;

    // MonoBehaviour Events
    void Start()

    {
        if (!visual.activeInHierarchy)
            visual.SetActive(true);

        _settingsMultiplayer = FindObjectOfType<SettingsMultiplayer>();
        _renderer = GetComponent<Renderer>();
        _lifePoints = LifePointsFor(type);

        if (_renderer != null && _settingsMultiplayer != null)
        {
            _renderer.material = StandardMaterialFor(type);
        }
        else
        {
            if (_renderer == null)
            {
                Debug.LogWarning($"{title}: _renderer is null", this);
            }

            else if (_settingsMultiplayer == null)
            {
                Debug.LogWarning($"{title}: _settings is null", this);
            }
        }
    }



    // <Public Methods
    public void AddLifepoints(int points)
    {
        _lifePoints += points;
    }

    public void SwitchMaterial()
    {
        _isStandardMaterial = !_isStandardMaterial;

        if (_isStandardMaterial)
        {
            _renderer.material = StandardMaterialFor(type);
        }
        else
        {
            _renderer.material = OtherMaterialFor(type);
        }
    }


    //private Methods
    private int LifePointsFor(GameObjectType _type)
    {
        switch (_type)
        {
            case GameObjectType.Player:
                return 100;
            case GameObjectType.Enemy:
                return 200;
            default:
                Debug.LogWarning("Players.LifePoints: _type is unknown");
                return 0;
        }
    }

    private Material StandardMaterialFor(GameObjectType _type)
    {
        if (_type == GameObjectType.Player)
        {
            return _settingsMultiplayer.playerMaterial;
        }
        else if (_type == GameObjectType.Enemy)
        {
            return _settingsMultiplayer.enemyMaterial;
        }
        Debug.LogWarning("Players.MaterialFor: _type is unknown");
        return _settingsMultiplayer.errorMaterial;
    }
    private Material OtherMaterialFor(GameObjectType _type)
    {
        if (_type == GameObjectType.Player)
        {
            return _settingsMultiplayer.otherPlayerMaterial;
        }
        else if (_type == GameObjectType.Enemy)
        {
            return _settingsMultiplayer.otherEnemyMaterial;
        }
        Debug.LogWarning("Players.MaterialFor: _type is unknown");
        return _settingsMultiplayer.errorMaterial;
    }
}




