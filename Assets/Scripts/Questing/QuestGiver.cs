using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : NPC
{
    public bool AssignedQuest { get; set; }
    public bool Helped { get; set; }
    
    [SerializeField] // exposed to the inspecto
    private GameObject quests;

    [SerializeField]
    private string questType;
    public Quest Quest { get; set; }
    public override void Interact()
    {        
        if (!AssignedQuest && !Helped)
        {
            // asign
            base.Interact();
            AssignQuest();
        }
        else if (AssignedQuest && !Helped)
        {
            CheckQuest();
        }
        else 
        {
            //DialogueSystem.Instance.AddNewDialogue(new string[] { "Gracias por ayudarme aquella vez!."}, name);
        }
    }

    void AssignQuest()
    {
        AssignedQuest = true;
        Quest = (Quest)quests.AddComponent(System.Type.GetType(questType));       
    }

    void CheckQuest()
    {
        if (Quest.Completed)
        {
            Quest.GiveReward();
            Helped = true;
            AssignedQuest = false;
            //DialogueSystem.Instance.AddNewDialogue(new string[] { "Gracias por eso!, te daré una recompensa."}, name);
        }
        else
        {
            //DialogueSystem.Instance.AddNewDialogue(new string[] { "Aún no terminas, vuelve a ello!"}, name);
        }
    }
}
