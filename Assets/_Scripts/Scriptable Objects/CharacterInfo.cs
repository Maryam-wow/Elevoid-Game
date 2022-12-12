using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="CharacterInfo")]
public class CharacterInfo : ScriptableObject
{

    public CharacterType characterType;

    public Sprite characterPhoto;
    public GameObject character3D;

    public int health;

    public bool unlcoked;
}

public enum CharacterType
{
    Water = 0,
    Earth = 1,
    Air = 2,
    Fire = 3,
    None = 4
}

