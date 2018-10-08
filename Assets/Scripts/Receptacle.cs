using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptacle : MonoBehaviour
{
    public Vitruve Vitruve;

    private void Start()
    {
        Debug.Assert(Vitruve != null);
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Potion")
        {
            var potion = obj.GetComponent<Potion>();

            if (potion != null)
            {
                Vitruve.DrinkPotion(potion);
                Destroy(obj.gameObject);
            }
        }
    }
}
