using UnityEngine;

public class CharacterCustomizationManager : MonoBehaviour
{
    public CharacterCustomizationPrefsSO customizationPrefs;

    [Header("Customization Parents")]
    [SerializeField] private Transform earsParent;
    [SerializeField] private Transform eyesParent;
    [SerializeField] private Transform faceParent;
    [SerializeField] private Transform headParent;
    [SerializeField] private Transform mouthParent;
    [SerializeField] private Transform jewleryParent;
    // or we can use Start()
    private void Awake()
    {
        // instantiate the selected ear asset
        if (customizationPrefs.selectedEar != null)
            Instantiate(customizationPrefs.selectedEar, earsParent).transform.localPosition = Vector3.zero;
        
        // similarly
        if (customizationPrefs.selectedEyes != null)
            Instantiate(customizationPrefs.selectedEyes, eyesParent).transform.localPosition = Vector3.zero;
        if (customizationPrefs.selectedFace != null)
            Instantiate(customizationPrefs.selectedFace, faceParent).transform.localPosition = Vector3.zero;
        if (customizationPrefs.selectedHead != null)
            Instantiate(customizationPrefs.selectedHead, headParent).transform.localPosition = Vector3.zero;
        if (customizationPrefs.selectedMouth != null)
            Instantiate(customizationPrefs.selectedMouth, mouthParent).transform.localPosition = Vector3.zero;
        if (customizationPrefs.selectedJewlery != null)
            Instantiate(customizationPrefs.selectedJewlery, jewleryParent).transform.localPosition = Vector3.zero;
    }
}
