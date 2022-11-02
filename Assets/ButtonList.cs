using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonList : MonoBehaviour
{
    [SerializeField]
    public struct Quest
    {
        public string questName;
        public string questDescription;
        public Sprite questIcon;
    }
    [SerializeField] 
    Quest[] allQuest;
    
    void Start()
    {
        GameObject buttonTemplate = transform.GetChild(0).gameObject;
        GameObject newButton;
        int numberQuest=allQuest.Length;
        

        for (int i = 0; i < numberQuest; i++)
        {
            newButton = Instantiate(buttonTemplate, transform);
            newButton.transform.GetChild(0).GetComponent<Text>().text = allQuest[i].questName;
            newButton.transform.GetChild(1).GetComponent<Text>().text = allQuest[i].questDescription;
            newButton.transform.GetChild(2).GetComponent<Image>().sprite = allQuest[i].questIcon;
        }
        Destroy(buttonTemplate);
    }
}
