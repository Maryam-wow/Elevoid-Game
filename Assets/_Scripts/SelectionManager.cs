using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{

    public static SelectionManager Instance;
    
    public Image selectedCaharcter;
    public Image selectedEar;
    public Image selectedEye;
    public Image selectedFace;
    public Image selectedHead;
    public Image selectedMouth;
    public Image selectedJewlery;


    public Button waterTab;
    public Button earthTab;
    public Button airTab;
    public Button fireTab;

    public Button earsTab;
    public Button eyesTab;
    public Button facesTab;
    public Button headsTab;
    public Button mouthsTab;
    public Button jewleryTab;

    public List<Button> selectionButtons;

    public List<Image> locksImages;
    public List<Image> charactersLocksImages;

    public AccessoriesType currentAccessorType;
    public CharacterType currentCharacterType;

    public CharacterCustomizationPrefsSO[] characterCustomizationPrefsSOs;

    private void Start()
    {
        Instance = this;
        Initializ();
    }

    public void Initializ()
    {
        earsTab.Select();
        AccessoriesTabSelected(AccessoriesType.Ears);

        CheckCharactersLocks();

        currentCharacterType = (CharacterType)GameManager.Instance.selectedCharacter;
        selectedCaharcter.sprite = GameManager.Instance.GetSelectedCharacterSprite();

        earsTab.onClick.AddListener(delegate { AccessoriesTabSelected(AccessoriesType.Ears); });
        eyesTab.onClick.AddListener(delegate { AccessoriesTabSelected(AccessoriesType.Eyes); });
        facesTab.onClick.AddListener(delegate { AccessoriesTabSelected(AccessoriesType.Faces); });
        headsTab.onClick.AddListener(delegate { AccessoriesTabSelected(AccessoriesType.Heads); });
        mouthsTab.onClick.AddListener(delegate { AccessoriesTabSelected(AccessoriesType.Mouths); });
        jewleryTab.onClick.AddListener(delegate { AccessoriesTabSelected(AccessoriesType.Jewlery); });

        for (int i = 0; i < selectionButtons.Count; i++)
        {
            int v = i;

            selectionButtons[i].onClick.AddListener(delegate { SelectionButtonClicked(v); });
        }

        waterTab.onClick.AddListener(delegate {CharacterTabClicked(CharacterType.Water); });
        earthTab.onClick.AddListener(delegate { CharacterTabClicked(CharacterType.Earth); });
        airTab.onClick.AddListener(delegate { CharacterTabClicked(CharacterType.Air); });
        fireTab.onClick.AddListener(delegate { CharacterTabClicked(CharacterType.Fire); });
    }

   
    public void AccessoriesTabSelected(AccessoriesType accessoriesType)
    {
        currentAccessorType = accessoriesType;
        if (accessoriesType == AccessoriesType.Ears)
        {
            for (int i = 0; i < selectionButtons.Count; i++)
            {
                if (i < GameManager.Instance.earsInfo.Count)
                {
                    selectionButtons[i].image.sprite = GameManager.Instance.earsInfo[i].accessoryUIPhoto;
                    selectionButtons[i].gameObject.SetActive(true);

                    if(GameManager.Instance.earsInfo[i].unlocked)
                    {
                        locksImages[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        locksImages[i].gameObject.SetActive(true);
                    }
                }
                else
                {
                    selectionButtons[i].gameObject.SetActive(false);
                }
            }
        }
        else if (accessoriesType == AccessoriesType.Eyes)
        {
            for (int i = 0; i < selectionButtons.Count; i++)
            {
                if (i < GameManager.Instance.eyesInfo.Count)
                {
                    selectionButtons[i].image.sprite = GameManager.Instance.eyesInfo[i].accessoryUIPhoto;
                    selectionButtons[i].gameObject.SetActive(true);

                    if (GameManager.Instance.eyesInfo[i].unlocked)
                    {
                        locksImages[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        locksImages[i].gameObject.SetActive(true);
                    }

                }
                else
                {
                    selectionButtons[i].gameObject.SetActive(false);
                }
            }
        }
        else if (accessoriesType == AccessoriesType.Faces)
        {
            for (int i = 0; i < selectionButtons.Count; i++)
            {
                if (i < GameManager.Instance.facesInfo.Count)
                {
                    selectionButtons[i].image.sprite = GameManager.Instance.facesInfo[i].accessoryUIPhoto;
                    selectionButtons[i].gameObject.SetActive(true);

                    if (GameManager.Instance.facesInfo[i].unlocked)
                    {
                        locksImages[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        locksImages[i].gameObject.SetActive(true);
                    }
                }
                else
                {
                    selectionButtons[i].gameObject.SetActive(false);
                }
            }
        }
        else if (accessoriesType == AccessoriesType.Heads)
        {
            for (int i = 0; i < selectionButtons.Count; i++)
            {
                if (i < GameManager.Instance.headsInfo.Count)
                {
                    selectionButtons[i].image.sprite = GameManager.Instance.headsInfo[i].accessoryUIPhoto;
                    selectionButtons[i].gameObject.SetActive(true);

                    if (GameManager.Instance.headsInfo[i].unlocked)
                    {
                        locksImages[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        locksImages[i].gameObject.SetActive(true);
                    }
                }
                else
                {
                    selectionButtons[i].gameObject.SetActive(false);
                }
            }
        }
        else if (accessoriesType == AccessoriesType.Mouths)
        {
            for (int i = 0; i < selectionButtons.Count; i++)
            {
                if (i < GameManager.Instance.mouthsInfo.Count)
                {
                    selectionButtons[i].image.sprite = GameManager.Instance.mouthsInfo[i].accessoryUIPhoto;
                    selectionButtons[i].gameObject.SetActive(true);

                    if (GameManager.Instance.mouthsInfo[i].unlocked)
                    {
                        locksImages[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        locksImages[i].gameObject.SetActive(true);
                    }
                }
                else
                {
                    selectionButtons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void SelectionButtonClicked(int number)
    {
        if (currentAccessorType == AccessoriesType.Ears)
        {
            if(GameManager.Instance.earsInfo[number].unlocked)
            {
                selectedEar.enabled = true;
                selectedEar.sprite = GameManager.Instance.earsInfo[number].accessoryPhoto;

                characterCustomizationPrefsSOs[GameManager.Instance.selectedCharacter].selectedEar
                    = GameManager.Instance.earsInfo[number].accessory3D;
            }
        }
        else if (currentAccessorType == AccessoriesType.Eyes)
        {
            if (GameManager.Instance.eyesInfo[number].unlocked)
            {
                selectedEye.enabled = true;
                selectedEye.sprite = GameManager.Instance.eyesInfo[number].accessoryPhoto;

                characterCustomizationPrefsSOs[GameManager.Instance.selectedCharacter].selectedEyes
                    = GameManager.Instance.eyesInfo[number].accessory3D;
            }
        }
        else if (currentAccessorType == AccessoriesType.Faces)
        {
            if (GameManager.Instance.facesInfo[number].unlocked)
            {
                selectedFace.enabled = true;
                selectedFace.sprite = GameManager.Instance.facesInfo[number].accessoryPhoto;

                characterCustomizationPrefsSOs[GameManager.Instance.selectedCharacter].selectedFace
                    = GameManager.Instance.facesInfo[number].accessory3D;
            }
        }
        else if (currentAccessorType == AccessoriesType.Heads)
        {
            if (GameManager.Instance.headsInfo[number].unlocked)
            {
                selectedHead.enabled = true;
                selectedHead.sprite = GameManager.Instance.headsInfo[number].accessoryPhoto;

                characterCustomizationPrefsSOs[GameManager.Instance.selectedCharacter].selectedHead
                    = GameManager.Instance.headsInfo[number].accessory3D;
            }
        }
        else if (currentAccessorType == AccessoriesType.Mouths)
        {
            if (GameManager.Instance.mouthsInfo[number].unlocked)
            {
                selectedMouth.enabled = true;
                selectedMouth.sprite = GameManager.Instance.mouthsInfo[number].accessoryPhoto;

                characterCustomizationPrefsSOs[GameManager.Instance.selectedCharacter].selectedMouth
                    = GameManager.Instance.mouthsInfo[number].accessory3D;
            }
        }
        else if (currentAccessorType == AccessoriesType.Jewlery)
        {
            if (GameManager.Instance.jewleryInfo[number].unlocked)
            {
                selectedJewlery.enabled = true;
                selectedJewlery.sprite = GameManager.Instance.jewleryInfo[number].accessoryPhoto;

                characterCustomizationPrefsSOs[GameManager.Instance.selectedCharacter].selectedJewlery
                    = GameManager.Instance.jewleryInfo[number].accessory3D;
            }
        }
    }

    public void CharacterTabClicked(CharacterType characterType)
    {
        if(GameManager.Instance.charactersInfo[(int)characterType].unlcoked)
        {
            currentCharacterType = characterType;
            selectedCaharcter.sprite = GameManager.Instance.GetCharacterSprite(characterType);
        }
    }

    public void CheckCharactersLocks()
    {
        for (int i = 0; i < charactersLocksImages.Count; i++)
        {
            if(GameManager.Instance.charactersInfo[i].unlcoked)
            {
                charactersLocksImages[i].gameObject.SetActive(false);
            }
            else
            {
                charactersLocksImages[i].gameObject.SetActive(true);
            }
        }
    }

}
