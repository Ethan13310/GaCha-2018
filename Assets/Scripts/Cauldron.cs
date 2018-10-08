using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    // Nombre min. d'ingrédients pour créer une potion
    public int MinIngredients = 2;

    // L'eau est-elle requise pour créer une potion
    public bool WaterRequired = true;

    // Autoriser l'ajout d'un même ingrédient plus d'un fois
    [Tooltip("Allow Ingredient Duplication")]
    public bool AllowDuplication = true;

    // Multiplier l'effet de la potion quand l'ingrédient est dupliqué
    public bool MultiplyOnDuplication = true;

    // Liste des ingrédients dans le chaudron
    private List<Ingredient> m_ingredients = new List<Ingredient>();

    // Y at-il de l'eau dans le chaudron
    private bool m_water = false;

    // Ajoute un ingrédient
    public bool AddIngredient(Ingredient ingredient)
    {
        Debug.Assert(ingredient != null);

        if (m_ingredients.Count >= MinIngredients)
        {
            // Le chaudron est plein
            Debug.LogWarning("Cauldron: Could not add ingredient (full)");
        }
        else if (!ingredient.IsCrushed)
        {
            // L'ingrédient n'est pas écrasé
            Debug.LogWarning("Cauldron: Could not add ingredient (ingredient not crushed)");
        }
        else if (!AllowDuplication && IngredientExists(ingredient))
        {
            // L'ingrédient a déjà été ajouté une fois
            Debug.LogWarning("Cauldron: Ingredient already added (duplication not allowed)");
        }
        else
        {
            Debug.Log("Cauldron: Added ingredient " + ingredient.Name);

            if (MultiplyOnDuplication && IngredientExists(ingredient))
            {
                ingredient.MultiplyEffects(10);
            }

            m_ingredients.Add(ingredient);
            //ajouter le son de bubulle
            GetComponentInChildren<CauldronSounds>().LaunchBubulleSound(m_ingredients.Count-1);
            return true;
        }

        return false;
    }

    // L'ingrédient a t-il déjà été ajouté
    public bool IngredientExists(Ingredient ingredient)
    {
        foreach (var it in m_ingredients)
        {
            if (it.Name == ingredient.Name)
            {
                return true;
            }
        }

        return false;
    }

    // Ajouter de l'eau
    public bool AddWater()
    {
        if (m_water || !WaterRequired)
        {
            // Il y a déjà de l'eau ou il n'y en a pas besoin
            Debug.LogWarning("Cauldron: Water already added");
            return false;
        }

        Debug.Log("Cauldron: Water added");
        m_water = true;

        return true;
    }

    // Fabriquer la potion
    public PotionMaker Cook()
    {
        if (m_ingredients.Count < MinIngredients || (WaterRequired && !m_water))
        {
            // Pas assez d'ingrédients ou d'eau
            Debug.LogWarning("Cauldron: Could not cook. Missing ingredients or water");
            return null;
        }

        Debug.Log("Cauldron: Cook");

        var maker = new PotionMaker(m_ingredients);
        m_ingredients = null;

        //Ajouter le son de la potion terminée
        GetComponentInChildren<CauldronSounds>().LaunchFinishPotionSound();

        return maker;
    }
}
