using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static ButtonList;

namespace Assets.Scripts.Quest
{
    public class QuestSupervisor 
    {
        public List<Quests> quests = new();
        public Achievments Achievments = new();
        int firstQuestsValue = 10;

        public void AddQuest(Quests quest)
        {
           quests.Add(quest);
        }

        public void CheckQuests()
        {
            foreach (var quest in quests)
            {
                quest.CurrentValue = Achievments.GetValue(quest.FieldName);
                quest.EndValue = firstQuestsValue+quest.StartValue;
                if (quest.CurrentValue >= quest.EndValue)
                {
                    quest.IsDone = true;
                }
            }
        }
        public QuestSupervisor()
        {
            quests.Add(new Quests() { Name = "Jump",Description="Podskocz 10 razy",exp=50,FieldName="jump"});
            quests.Add(new Quests() { Name = "Dodge", Description = "Użyj dodge 10 razy",exp=75,FieldName="dodge"});
            quests.Add(new Quests() { Name = "Enemy", Description = "Zabij 10 przeciwników",exp=125,FieldName= "enemyDead" });
        }
    }
}
