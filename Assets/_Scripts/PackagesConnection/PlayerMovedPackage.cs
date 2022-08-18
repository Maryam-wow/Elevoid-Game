using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerMovedPackage
{
    public string packageType = "PlayerMovedPackage";

    public PlayerPackage player;

    public PlayerMovedPackage(PlayerPackage player) {
        this.player = player;
    }
}