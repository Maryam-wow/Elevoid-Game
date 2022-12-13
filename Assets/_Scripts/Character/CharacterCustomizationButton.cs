using UnityEngine;

public class CharacterCustomizationButton : MonoBehaviour
{
    public void UpdateAccessorySelectionOnCharacterPreferences(
        CharacterCustomizationPrefsSO prefs,
        AccessoryInfo accessoryInfoSO,
        AccessoriesType accessoriesType)
    {
        switch (accessoriesType)
        {
            case AccessoriesType.Ears:
                prefs.selectedEar = accessoryInfoSO.accessory3D;
                break;
            case AccessoriesType.Eyes:
                prefs.selectedEyes = accessoryInfoSO.accessory3D;
                break;
            case AccessoriesType.Faces:
                prefs.selectedFace = accessoryInfoSO.accessory3D;
                break;
            case AccessoriesType.Heads:
                prefs.selectedHead = accessoryInfoSO.accessory3D;
                break;
            case AccessoriesType.Mouths:
                prefs.selectedMouth = accessoryInfoSO.accessory3D;
                break;
            
            default:
                return;
        }

        // Similar to `if` statement like this:
        //
        // if (accessoriesType == AccessoriesType.Ears)
        // {
        //     prefs.selectedEar = accessoryInfoSO.accessory3D;
        // } else if (accessoriesType == AccessoriesType.Eyes)
        // {
        //     prefs.selectedEyes = accessoryInfoSO.accessory3D;
        // } // else if() ...
        // ...
    }
}