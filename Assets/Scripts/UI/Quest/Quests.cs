using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quests
{
    //zmienic na quest
 
    public string FieldName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CurrentValue { get; set; }
    public int StartValue { get; set; } 
    public int EndValue { get; set; }
    public int exp { get; set; }
    public bool IsDone { get; set; } = false;
}
