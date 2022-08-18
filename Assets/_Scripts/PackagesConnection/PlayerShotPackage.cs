using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerShotPackage
{
    public string packageType = "PlayerShotPackage";

    public PlayerPackage player;

    public PlayerShotPackage(PlayerPackage player) {
        this.player = player;
    }
}
