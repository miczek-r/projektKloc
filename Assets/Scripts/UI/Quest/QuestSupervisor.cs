using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Quest
{
    public class QuestSupervisor
    {
        public List<Quests> quests = new();
        public Achievments Achievments = new();
        int firstQuestsValue=10;
        
        public void AddQuest(Quests quest)
        {
           quests.Add(quest);
        }

        public void CheckQuests()
        {
            foreach (var quest in quests)
            {
                //quest.CurrentValue = Achievments.GetValue("jump");
                quest.EndValue = quest.CurrentValue + firstQuestsValue;
                if (quest.CurrentValue >= quest.EndValue)
                {
                    quest.IsDone = true;
                }
            }
        }
        public void Question(object sender, int b)
        {
            CheckQuests();
        }
        public QuestSupervisor()
        {
            //chyba tu sprawdzic czy sie przepelnia ??
            //achievments.ThresholdReached += Question;
            quests.Add(new Quests() { Name = "Jump",Description="Podskocz 10 razy",exp=50,CurrentValue=Achievments.GetValue("jump")});
            quests.Add(new Quests() { Name = "Dodge", Description = "Użyj dodge 10 razy",exp=75,CurrentValue=Achievments.GetValue("dodge")});
        }
    }
}
