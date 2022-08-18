using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="AccessoryInfo")]
public class AccessoryInfo : ScriptableObject
{

    public AccessoriesType accessoriesType;

    public Sprite accessoryPhoto;
    public Sprite accessoryUIPhoto;

    public bool unlocked;

}

public enum AccessoriesType
{
    Ears = 0,
    Eyes = 1,
    Faces = 2,
    Heads = 3,
    Mouths = 4
}
