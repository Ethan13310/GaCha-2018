using UnityEngine;

public class Container : MonoBehaviour
{
    // Liste des ingredients disponibles
    public IngredientList IngredientList;

    // Nom de l'ingrédient
    public string Name { get; set; }

    // L'ingrédient est-il liquide
    public bool IsLiquid { get; set; }

    // Liste des effets de l'ingrédient
    public Effect[] Effects { get; set; }

    // Quantité actuelle de l'ingrédient
    public int Quantity { get; set; }

    // Quantité max de l'ingrédient
    public int MaxQuantity { get; set; }

    // Sprite
    public Sprite Sprite { get; set; }

    private void Awake()
    {
        Debug.Assert(IngredientList != null);

        var ingredient = IngredientList.GetIngredient();

        Debug.Assert(ingredient != null);

        if (ingredient != null)
        {
            Name = ingredient.Name;
            IsLiquid = ingredient.IsLiquid;

            Effects = new Effect[]
            {
                new Effect()
                {
                    OrganName = ingredient.Organ,
                    ActionValue = ingredient.ActionValue
                }
            };

            MaxQuantity = ingredient.Quantity;
            Quantity = MaxQuantity;
            Sprite = ingredient.Sprite;
        }
        else
        {
            Debug.LogWarning("Container: Initialization failed (using default ingredient)");
        }
    }

    // Régupère un ingrédient
    public Ingredient Get()
    {
        if (Quantity < 1)
        {
            Debug.LogWarning("Container: Could not get ingredient (empty) -> " + Name);
            return null;
        }

        Debug.Log("Container: Get ingredient -> " + Name);
        --Quantity;

        return new Ingredient
        {
            Name = Name,
            Effects = Effects,
            // Si l'ingrédient est liquide,
            // on le considère comme écrasé
            IsCrushed = IsLiquid,
            IsLiquid = IsLiquid
        };
    }

    // Ajoute un ingrédient
    public bool Add()
    {
        if (Quantity < MaxQuantity)
        {
            Debug.Log("Container: Ingredient added -> " + Name);
            ++Quantity;
            return true;
        }

        // Conteneur plein
        Debug.LogWarning("Container: Could not added ingredient (full) -> " + Name);
        return false;
    }
}
