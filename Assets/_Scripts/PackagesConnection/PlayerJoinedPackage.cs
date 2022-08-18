using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerJoinedPackage
{
    public string packageType = "PlayerJoinedPackage";

    public PlayerPackage player;

    public PlayerJoinedPackage(PlayerPackage player) {
        this.player = player;
    }
}
