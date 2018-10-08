using System.Collections.Generic;
using UnityEngine;

public class PotionMaker
{
    // Effets de la potion
    private List<Effect> m_effects = new List<Effect>();

    public PotionMaker(List<Ingredient> ingredients)
    {
        Debug.Assert(ingredients != null);

        MakePotion(ingredients);
    }

    // Récupérer la liste des effets de la potion
    public List<Effect> GetEffects()
    {
        var effects = new List<Effect>();

        foreach (var it in m_effects)
        {
            effects.Add(it);
        }

        return effects;
    }

    // Fabriquer la potion (calcul des effets)
    private void MakePotion(List<Ingredient> ingredients)
    {
        foreach (var ingredient in ingredients)
        {
            foreach (var effect in ingredient.Effects)
            {
                AddEffect(effect);
            }
        }
    }

    // Recupère l'ID d'un effet
    private int GetEffect(string organ)
    {
        int i = 0;

        foreach (var effect in m_effects)
        {
            if (effect.OrganName == organ)
            {
                return i;
            }

            ++i;
        }

        // L'organe n'existe pas
        return -1;
    }

    // Ajoute un effet
    private void AddEffect(Effect effect)
    {
        int id = GetEffect(effect.OrganName);

        if (id != -1)
        {
            var tmp = m_effects[id];
            tmp.ActionValue += effect.ActionValue;
            m_effects[id] = tmp;
        }
        else
        {
            m_effects.Add(effect);
        }
    }
}
