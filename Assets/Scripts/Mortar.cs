using UnityEngine;

public class Mortar : MonoBehaviour
{
    private Ingredient m_ingredient;

    public GameObject content;

    void Start(){
        content.SetActive(false);
    }

    // Le mortier est-il plein
    public bool IsFull
    {
        get
        {
            return m_ingredient != null;
        }
    }

    // L'ingrédient dans le mortier est-il écrasé
    public bool IsIngredientCrushed
    {
        get
        {
            return IsFull && m_ingredient.IsCrushed;
        }
    }

    // Ajoute l'ingrédient
    public bool AddIngredient(Ingredient ingredient)
    {
        Debug.Assert(ingredient != null);

        if (m_ingredient != null)
        {
            // Le mortier est plein
            Debug.LogWarning("Mortar: Could not add ingredient (full)");
            return false;
        }

        if (ingredient.IsLiquid)
        {
            // L'ingrédient est liquide
            Debug.LogWarning("Mortar: Could not add ingredient (ingredient is liquid)");
            return false;
        }

        Debug.Log("Mortar: Ingredient added -> " + ingredient.Name);
        m_ingredient = ingredient;
        return true;
    }

    // Ecraser l'ingrédient
    public bool Crush()
    {
        if (m_ingredient != null && !m_ingredient.IsCrushed)
        {
            Debug.Log("Mortar: Ingredient crushed -> " + m_ingredient.Name);
            m_ingredient.IsCrushed = true;

            GetComponent<SimpleSoundLauncher>().LaunchSound();

            return true;
        }

        Debug.LogWarning("Mortar: No ingredient to crush");
        return false;
    }

    // Récupérer l'ingrédient
    public Ingredient GetIngredient()
    {
        if (m_ingredient != null)
        {
            Debug.Log("Mortar: Get ingredient -> " + m_ingredient.Name);
            Ingredient ingredient = m_ingredient;
            Clear();
            return ingredient;
        }

        Debug.LogWarning("Mortar: Could not get ingredient (empty)");
        return null;
    }

    // Vider le mortier
    public void Clear()
    {
        m_ingredient = null;
        Debug.Log("Mortar: Cleared");
    }
}
