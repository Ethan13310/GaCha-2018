using UnityEngine;
using UnityEngine.SceneManagement;

public class Vitruve : MonoBehaviour
{
    [System.Serializable]
    public class Organ
    {
        // Nom de l'organe
        public string Name;

        // Etat de l'organe
        public int Value = 5;

        // Valeurs min/max
        public int MinValue = 0;
        public int MaxValue = 5;

        // L'organe est-il coché dans le grimoire
        public bool IsChecked = false;

        // Sprite de l'organe
        public Sprite Sprite;

        // Jauge de l'organe
        public Gauge Gauge;
    }

    // Nombre de cobayes
    public int Count = 3;

    // Liste des organes
    public Organ[] Organs;

    // Boire une potion
    public bool DrinkPotion(Potion potion)
    {
        Debug.Assert(potion != null);

        if (Count > 0)
        {
            ApplyPotionEffects(potion);

            if (!IsAlive())
            {
                // Si le patient est mort, on prend le suivant
                Resurrect();
            }

            Debug.Log("Vitruve: Potion drank");
            return true;
        }

        Debug.LogWarning("Vitruve: No try remaining");
        return false;
    }

    private void ApplyPotionEffects(Potion potion)
    {
        foreach (var effect in potion.Effects)
        {
            foreach (var organ in Organs)
            {
                if (effect.OrganName == organ.Name)
                {
                    organ.Value -= effect.ActionValue;
                    organ.Gauge.SetValue(organ.Value);
                }
            }
        }
    }

    private bool IsAlive()
    {
        foreach (var organ in Organs)
        {
            if (organ.Value <= organ.MinValue)
            {
                // Un organe est détruit -> mort
                return false;
            }
        }

        return true;
    }

    private bool Resurrect()
    {
        if (Count > 0)
        {
            foreach (var organ in Organs)
            {
                organ.Value = organ.MaxValue;
                organ.Gauge.ResetValues();
            }

            --Count;
            return true;
        }

        SceneManager.LoadScene(Global.s.SceneIndex_ScoreScreen);
        return false;
    }

    private static Vitruve s_Instance = null;

    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static Vitruve instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                s_Instance = FindObjectOfType(typeof(Vitruve)) as Vitruve;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
				Debug.LogError("No player in scene");
				/* 
                GameObject obj = Instantiate(Resources.Load("PlayerController") as GameObject);
                s_Instance = obj.GetComponent<PlayerController>();*/
            }
            return s_Instance;
        }
	}
}
