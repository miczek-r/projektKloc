using Assets.Scripts.Quest;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class QuestDisplay : MonoBehaviour
{
    public GameObject gameObject;
    public QuestSupervisor questSupervisor;
    public List<Quests> quests= new ();
    public PlayerStats playerStats;
    void Start()
    {
        questSupervisor = GameObject.FindWithTag("Player").GetComponent<PlayerStateMachine>().QuestSupervisor;
  
    }
    void Update()
    {
        UpdateQuests();
    }
    private void UpdateQuests()
    {

        questSupervisor.CheckQuests();
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (var quest in questSupervisor.quests)
        {
            if (!quest.IsDone)
            {
                if (!quests.Contains(quest))
                {
                    quests.Add(quest);
                }
                var tmp = Instantiate(gameObject);
                tmp.transform.SetParent(transform, false);
                var tmp2 = tmp.GetComponent<TMP_Text>();
                tmp2.text = quest.Name + " " + quest.CurrentValue + "/" + quest.EndValue;
            }
            if (quest.IsDone)
            {
                playerStats.AddExp(quest.exp);
                Debug.Log(playerStats.currentExp);
                Debug.Log(playerStats.Level);
                questSupervisor.quests.Remove(quest);
            }
        }
    }
}
