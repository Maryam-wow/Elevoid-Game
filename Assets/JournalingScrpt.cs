using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JournalingScrpt : MonoBehaviour
{
    public string theText;
    public GameObject ourNote;
    public GameObject placeHolder;

    void Start()
    {
        theText = PlayerPrefs.GetString("NoteContents");
        placeHolder.GetComponent<TMP_InputField>().text = theText;

    }
    public void SaveWaterNote()
    {
        theText = ourNote.GetComponent<TMP_Text>().text;
        PlayerPrefs.SetString("NoteContents", theText);
    }

}
