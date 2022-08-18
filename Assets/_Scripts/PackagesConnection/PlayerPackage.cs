using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerPackage
{

    //public CharacterInfo playerInfo;
    public int id;
    //public string name;
    //public int health;
    public float currentPositionX;
    public float currentPositionY;
    public float currentPositionZ;

    public Vector3 currentPosition
    {
        get
        {
            return new Vector3(currentPositionX, currentPositionY, currentPositionZ);
        }
        set
        {
            currentPositionX = value.x;
            currentPositionY = value.y;
            currentPositionZ = value.z;
        }
    }

    public PlayerPackage(int id, float CurrentpositionX, float CurrentpositionY, float CurrentpositionZ)
    {
        this.id = id;
        this.currentPositionX = CurrentpositionX;
        this.currentPositionY = CurrentpositionY;
        this.currentPositionZ = CurrentpositionZ;
    }
}