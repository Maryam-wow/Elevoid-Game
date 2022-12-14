using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JournalingScrpt : MonoBehaviour
{
    public string theText;
    public string theText2;
    public string theText3;
    public GameObject ourNote;
    public GameObject ourNote2;
    public GameObject ourNote3;
    public GameObject placeHolder;
    public GameObject placeHolder2;
    public GameObject placeHolder3;

    void Start()
    {   //Note 1//
        theText = PlayerPrefs.GetString("NoteContents");
        placeHolder.GetComponent<TMP_InputField>().text = theText;
        //Note 2//
        theText2 = PlayerPrefs.GetString("NoteContents2");
        placeHolder2.GetComponent<TMP_InputField>().text = theText2;
        //Note 3//
        theText3 = PlayerPrefs.GetString("NoteContents3");
        placeHolder3.GetComponent<TMP_InputField>().text = theText3;


    }
    public void SaveWaterNote()
    {   //Note 1//
        theText = ourNote.GetComponent<TMP_Text>().text;
        PlayerPrefs.SetString("NoteContents", theText);
    }
    public void SaveWaterNote2()
    {
        //Note 2//
        theText2 = ourNote2.GetComponent<TMP_Text>().text;
        PlayerPrefs.SetString("NoteContents2", theText2);
    }
    public void SaveWaterNote3()
    {
        //Note 3//
        theText3 = ourNote3.GetComponent<TMP_Text>().text;
        PlayerPrefs.SetString("NoteContents3", theText3);
    }
}
