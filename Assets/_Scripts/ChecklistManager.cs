using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;


public class ChecklistManager : MonoBehaviour
{
    public Transform content;
    public GameObject addPanel;
    public Button createButton;
    public GameObject checklistItemPrefab;



    string filePath;

    private List<ChecklistObject> checklistObjects = new List<ChecklistObject>();

    private InputField[] addInputFields;

    public class ChecklistItem
    {
        public string objName;
        public string type;
        public int index;

        public ChecklistItem(string name, string type, int index)
        {
            this.objName = name;
            this.type = type;
            this.index = index;
        }
    }
    private void Start()
    {
        filePath = Application.persistentDataPath + "/checklist.txt";
        LoadJSONData();
        addInputFields = addPanel.GetComponentsInChildren<InputField>();
        createButton.onClick.AddListener(delegate { CreateChecklistItem(addInputFields[0].text, addInputFields[1].text); });
    }

    public void SwitchMode(int mode)
    {
        switch (mode)
        {
            //Regular checklistmode

            case 0:
                addPanel.SetActive(false);
                break;
            //Adding new checlist item
            case 1:
                addPanel.SetActive(true);
                break;
        }
    }
    void CreateChecklistItem(string name, string type, int loadIndex = 0, bool loading=false)
    {
        GameObject item = Instantiate(checklistItemPrefab);

        item.transform.SetParent(content);
        ChecklistObject itemObject = item.GetComponent<ChecklistObject>();
        int index = loadIndex;
        if (loading)
            index = checklistObjects.Count;
        itemObject.SetObjectInfo(name, type, index);
        checklistObjects.Add(itemObject);
        ChecklistObject temp = itemObject;
        itemObject.GetComponent<Toggle>().onValueChanged.AddListener(delegate { CheckItem(temp); });
        if (!loading)
        {
            SaveJSONData();
            SwitchMode(0);
        }
        
    }
    void CheckItem(ChecklistObject item)
    {
        checklistObjects.Remove(item);
        Destroy(item.gameObject);
    }
    void SaveJSONData()
    {
        string contents = "";

        for (int i = 0; i < checklistObjects.Count; i++)
        {
            contents += JsonUtility.ToJson(checklistObjects[i]) + "\n";
        }

        File.WriteAllText(filePath, contents);
    }
    void LoadJSONData()
    {
        if (File.Exists(filePath))
        {
            string contents = File.ReadAllText(filePath);
            string[] splitContents = contents.Split("\n");
            foreach (string content in splitContents)
            {
                if (content.Trim() != "")
                {
                    Debug.Log(content);
                    ChecklistItem temp = JsonUtility.FromJson<ChecklistItem>(content.Trim());
                    CreateChecklistItem(temp.objName, temp.type, temp.index, true);
                }
            }
                
        }
    }
}
 
