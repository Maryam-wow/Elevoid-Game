using System;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int health= 5;

    public CharacterType selectedCharacter;

    public DateTime lastRewardTime;

    public int rewardCount;
}