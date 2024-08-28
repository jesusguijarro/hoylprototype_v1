using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal
{
    public Quest Quest { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }

    public virtual void Init()
    {
        //default init stuff        
    }
    public void Evaluate()
    {
        Debug.Log("Entro al if de evaluar.");
        Debug.Log("CurrentAmount: " + CurrentAmount);
        Debug.Log("RequiredAmount: " + RequiredAmount);
        if(CurrentAmount >= RequiredAmount)
        {
            Debug.Log("Entro al if de completada.");
            Complete();
        }
    }

    public void Complete()
    {
        Completed = true;
        Quest.CheckGoals();        
        Debug.Log("Goal marked as completed.");
    }
}   
