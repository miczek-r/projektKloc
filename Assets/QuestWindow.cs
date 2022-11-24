using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestWindow : MonoBehaviour
{
    public Quests quest;
    public PlayerStats stats;
    
    public GameObject questWindow;
    public Text title;
    public Text description;
    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        //title.text = quest.titile;
        //description.text = quest.description;
    }
}
