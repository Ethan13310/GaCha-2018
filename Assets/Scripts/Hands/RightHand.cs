using UnityEngine;

public class RightHand : MonoBehaviour
{
    public Cauldron Cauldron;
    public Mortar Mortar;

    private Ingredient m_ingredient;

    // La cuillière contient-elle un ingrédient
    public bool HasIngredient
    {
        get
        {
            return m_ingredient != null;
        }
    }

    // Nom de l'ingrédient contenu dans la cuillère
    public string Ingredient
    {
        get
        {
            if (HasIngredient)
            {
                return m_ingredient.Name;
            }
            return string.Empty;
        }
    }

    private void Start()
    {
        Debug.Assert(Mortar != null);
        Debug.Assert(Cauldron != null);
    }

    // Prendre un ingrédient dans un bocal
    public bool TakeIngredient(Container container)
    {
        if (m_ingredient == null)
        {
            m_ingredient = container.Get();

            if (m_ingredient != null)
            {
                Debug.Log("RightHand: Ingredient taken -> " + m_ingredient.Name);
                return true;
            }
        }
        else
        {
            Debug.LogWarning("RightHand: Could not take ingredient (full)");
        }

        return false;
    }

    // Placer l'ingrédient dans le mortier
    public bool PutIngredientIntoMortar()
    {
        if (HasIngredient)
        {
            if (Mortar.AddIngredient(m_ingredient))
            {
                m_ingredient = null;
                return true;
            }
            return false;
        }

        Debug.LogWarning("RightHand: Could not put ingredient into mortar (hand empty)");
        return false;
    }

    // Placer l'ingrédient dans le chaudron
    public bool PutIngredientIntoCauldron()
    {
        if (HasIngredient)
        {
            if (Cauldron.AddIngredient(m_ingredient))
            {
                m_ingredient = null;
                return true;
            }
            return false;
        }

        Debug.LogWarning("RightHand: Could not put ingredient into cauldron (hand empty)");
        return false;
    }

    // Se débarasser de l'ingrédient
    public bool ThrowIngredient()
    {
        if (!HasIngredient)
        {
            Debug.LogWarning("RightHand: Could not throw ingredient (hand empty)");
            return false;
        }

        Debug.Log("RightHand: Ingredient thrown");
        m_ingredient = null;
        return true;
    }

    // Ecraser l'ingrédient dans le mortier
    public bool Crush()
    {
        return Mortar.Crush();
    }

    // Mélanger les ingrédients dans le chaudron
    public PotionMaker Cook()
    {
        return Cauldron.Cook();
    }
}
