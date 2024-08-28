using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionLog : MonoBehaviour, IConsumable
{
    public void Consume()
    {
        Debug.Log("Acabas de beber un poco de la posi�n. Bien");
        Destroy(gameObject);
    }

    public void Consume(CharacterStats stats)
    {
        Debug.Log("Acabas de beber un poco de la posi�n. Muy bien.");
    }
}
