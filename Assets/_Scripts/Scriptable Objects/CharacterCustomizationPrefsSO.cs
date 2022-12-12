using UnityEngine;

[CreateAssetMenu(order = 1, fileName = "SomeCharacterCustomizationPrefs", menuName = "ScriptableObjects/CharacterCustomizationPrefs")]
public class CharacterCustomizationPrefsSO : ScriptableObject
{
    public GameObject selectedEar;
    public GameObject selectedEyes;
    public GameObject selectedFace;
    public GameObject selectedHead;
    public GameObject selectedMouth;
}
