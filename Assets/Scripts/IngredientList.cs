using System.Collections.Generic;
using UnityEngine;

public class IngredientList : MonoBehaviour
{
    [System.Serializable]
    public class IngredientTemplate
    {
        // Nom de l'ingrédient
        public string Name;

        // Nom de l'organe affecté
        public string Organ;

        // Quantité disponible pour cet ingredient
        public int Quantity = 3;

        // Valeur de l'action de l'ingredient
        public int ActionValue = 1;

        // L'ingredient est-il liquide
        public bool IsLiquid = false;

        // Sprite de l'ingredient
        public Sprite Sprite;

        // L'ingredient a t-il été pioché
        public bool IsAvailable = false;
    }

    public IngredientTemplate[] IList;

    private void Awake()
    {
        Debug.Assert(IList.Length >= 7);
    }

    public List<Container> GetContainers()
    {
        var list = new List<Container>();

        for (int i = 0; i < transform.childCount; ++i)
        {
            var container = transform.GetChild(i).GetComponent<Container>();

            if (container != null)
            {
                list.Add(container);
            }
        }

        return list;
    }

    public IngredientTemplate GetIngredient()
    {
        if (IList.Length == 0)
        {
            return null;
        }

        IngredientTemplate ingredient = null;

        while (ingredient == null)
        {
            ingredient = IList[UnityEngine.Random.Range(0, IList.Length - 1)];

            if (IList.Length < 7)
            {
                // Eviter une boucle infinie si moins de 7 ingredients renseignés
                break;
            }

            if (!ingredient.IsAvailable)
            {
                ingredient = null;
            }
        }

        Debug.Log("IngredientList: Random ingredient picked -> " + ingredient.Name);
        ingredient.IsAvailable = false;
        return ingredient;
    }
}
