using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class itemDatabase : MonoBehaviour
{
    public static itemDatabase Instance { get; set; }
    private List<Item> Items { get; set; }
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        BuildDatabase();

    }

    private void BuildDatabase()
    {
        Items = JsonConvert.DeserializeObject<List<Item>>(Resources.Load<TextAsset>("JSON/Items").ToString());
        //Debug.Log(Items[1].ItemName + " is a " + Items[1].ItemType.ToString());
        //Debug.Log(Items[0].ItemName);
    }

    public Item GetItem(string itemSlug) {
        foreach (Item item in Items)
        {
            if(item.ObjectSlug == itemSlug)
                return item;
        }
        Debug.Log("Coulnd't find item: " + itemSlug);
        return null;
    }
}
