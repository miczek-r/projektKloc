using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievments
{
    public int dodge { get; set; } = 0;
    public int block { get; set; } = 0;
    public int attack { get; set; } = 0;
    public int jump { get; set; } = 0;

    public void Increment(string name)
    {
        try
        {
            var param = this.GetType().GetProperty(name);
            param.SetValue(this, (int)param.GetValue(this) + 1);
        }
        catch
        {
            
        }
    }
    public Dictionary<string, string> ReturnAchievmentsDictionary()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        foreach (var value in this.GetType().GetProperties())
        {
            dic.Add(value.Name, value.GetValue(this).ToString());
        }
        return dic;
    }
    public int GetValue(string value)
    {
        try
        {
            return (int)this.GetType().GetProperty(value).GetValue(this);
        }
        catch
        {
            //sprawdzasz czy jest ten i jest git i nie tu sprawdzasz jak cos no i jak cos Rafal to usunie
            return -1;
        }
    }
}
