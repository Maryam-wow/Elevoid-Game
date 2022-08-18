using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerLeftPackage
{
    public string packageType = "PlayerLeftPackage";

    public PlayerPackage player;

    public PlayerLeftPackage(PlayerPackage player) {
        this.player = player;
    }
}
