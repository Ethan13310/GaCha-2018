using UnityEngine;

public class LeftHand : MonoBehaviour
{
    public Cauldron Cauldron;
    public Mortar Mortar;
    public SpellBook Book;
    public Transform WaterBottle;

    // Auto releases
    public bool AutoReleaseBottle = true;
    public bool AutoReleaseMortar = true;

    private Transform m_bottle;
    private Transform m_potion;
    private Transform m_mortar;

    // La main tient-elle la bouteille d'eau
    public bool HasWaterBottle
    {
        get
        {
            return m_bottle != null;
        }
    }

    // La main tient-elle une potion
    public bool HasPotion
    {
        get
        {
            return m_potion != null;
        }
    }

    // La main tient-elle le mortier
    public bool HasMortar
    {
        get
        {
            return m_mortar != null;
        }
    }

    // La main tient-elle un objet
    public bool HasObject
    {
        get
        {
            return HasWaterBottle || HasPotion || HasMortar;
        }
    }

    private void Start()
    {
        Debug.Assert(Cauldron != null);
        Debug.Assert(Mortar != null);
        Debug.Assert(Book != null);
        Debug.Assert(WaterBottle != null);
    }

    // Tourner la page de gauche du grimoire
    public bool TurnPageLeft()
    {
        if (!HasObject)
        {
            Book.PreviousPage();
            return true;
        }

        Debug.LogWarning("LeftHand: Could not turn spellbook page (hand busy)");
        return false;
    }

    // Tourner la page de droite du grimoire
    public bool TurnPageRight()
    {
        if (!HasObject)
        {
            Book.NextPage();
            return true;
        }

        Debug.LogWarning("LeftHand: Could not turn spellbook page (hand busy)");
        return false;
    }

    // Prendre la bouteille d'eau
    public bool TakeWaterBottle()
    {
        if (!HasObject)
        {
            WaterBottle.transform.parent = transform;
            m_bottle = WaterBottle.transform;
            return true;
        }

        Debug.LogWarning("LeftHand: Could not take water bottle (hand busy)");
        return false;
    }

    // Prendre le mortier
    public bool TakeMortar()
    {
        Mortar mortar = Mortar.GetComponent<Mortar>();

        if (mortar.IsIngredientCrushed)
        {
            Debug.Log("LeftHand: Mortar taken");

            Mortar.transform.parent = transform;
            m_mortar = Mortar.transform;
            return true;
        }

        Debug.LogWarning("LeftHand: Could not take mortar (mortar empty or ingredient not crushed)");
        return false;
    }

    // Verser le contenu du mortier dans le chaudron
    public bool PutMortarContentIntoCauldron()
    {
        if (HasMortar)
        {
            var ingredient = m_mortar.GetComponent<Mortar>().GetIngredient();

            Debug.Assert(ingredient != null);

            Cauldron.AddIngredient(ingredient);
            Debug.Log("LeftHand: Mortar content put into the cauldron");

            if (AutoReleaseMortar)
            {
                ReleaseMortar();
            }

            return true;
        }

        Debug.LogWarning("LeftHand: Could not put mortar content into the cauldron (mortar not held)");
        return false;
    }

    // Lâcher le mortier
    public bool ReleaseMortar()
    {
        if (HasMortar)
        {
            Debug.Log("LeftHand: Mortar released");

            m_mortar.parent = null;
            m_mortar = null;

            return true;
        }

        Debug.LogWarning("LeftHand: Could not release mortar (hand empty)");
        return false;
    }

    // Lâcher la bouteille d'eau
    public bool ReleaseWaterBottle()
    {
        if (HasWaterBottle)
        {
            Debug.Log("LeftHand: Water bottle released");

            m_bottle.parent = null;
            m_bottle = null;

            return true;
        }

        Debug.LogWarning("LeftHand: Could not release water bottle (hand empty)");
        return false;
    }

    // Verser l'eau dans le chaudron
    public bool PutWater()
    {
        if (HasWaterBottle)
        {
            var ret = Cauldron.AddWater();

            if (AutoReleaseBottle)
            {
                ReleaseWaterBottle();
            }

            return ret;
        }

        Debug.LogWarning("LeftHand: Could not add water into cauldron (water bottle not held)");
        return false;
    }

    // Prendre une potion
    public bool TakePotionFlask(Transform potion)
    {
        Debug.Assert(potion != null);

        if (!HasObject)
        {
            potion.parent = transform;
            m_potion = potion;
            return true;
        }

        return false;
    }

    // Lâcher la potion
    public bool ReleasePotion()
    {
        if (HasPotion)
        {
            m_potion.parent = null;
            m_potion = null;
            return true;
        }
        return false;
    }

    // Jeter la potion
    public bool TrashPotion()
    {
        if (HasPotion)
        {
            m_potion.gameObject.SetActive(false);
            Destroy(m_potion);
            m_potion = null;
            return true;
        }
        return false;
    }

    // Donner la potion au patient
    public bool GivePotion()
    {
        if (HasPotion)
        {
            return true;
        }
        return false;
    }
}
