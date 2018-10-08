using UnityEngine;

public class RightHandActions : MonoBehaviour
{
    // Prefab de d'une potion
    public GameObject PotionPrefab;

    public bool EnableMouse = false;
    public bool EnableCollisions = true;

    public Spoon Spoon;

    private RightHand m_hand;
    private SimpleSoundLauncher m_audio;

    private void Start()
    {
        m_hand = GetComponent<RightHand>();

        m_audio = GetComponent<SimpleSoundLauncher>();

        Debug.Assert(m_hand != null);
        Debug.Assert(PotionPrefab != null);

        if (Spoon != null)
        {
            Spoon.DisableContent();
        }
    }

    private void Update()
    {
        if (EnableMouse && Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                HandleAction(hit.collider.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (EnableCollisions)
        {
       //     HandleAction(obj.gameObject);
        }
    }

    public void HandleAction(GameObject obj)
    {
        Debug.Assert(obj != null);

        if (EnableCollisions)
        {
            switch (obj.tag)
            {
                case "Ingredient":
                    ClickPot(obj.GetComponent<Container>());
                    break;

                case "Mortar":
                    ClickMortar();
                    break;

                case "Cauldron":
                    ClickCauldron();
                    break;
            }
        }
    }

    private void ClickPot(Container container)
    {
        Debug.Assert(container != null);

        if (m_hand.HasIngredient)
        {
            if (m_hand.Ingredient == container.Name)
            {
                // On remet l'ingrédient dans son pot
             /*   m_hand.ThrowIngredient();
                container.Add();
                m_audio.LaunchSound();
                EnableSpoonContent(false);*/
            }
            else
            {
                // Remise de l'ingrédient dans le mauvais conteneur
                Debug.LogWarning("RightHand: Could not throw ingredient (wrong container)");
            }
        }
        else
        {
            // Prendre un ingrédient
            if (m_hand.TakeIngredient(container))
            {
                m_audio.LaunchSound();
                EnableSpoonContent(true);
            }
        }
    }

    private void ClickMortar()
    {
        if (m_hand.HasIngredient)
        {
            if (m_hand.PutIngredientIntoMortar())
            {
                GetComponent<RightHand>().Mortar.content.SetActive(true);
                EnableSpoonContent(false);
            }
        }
        else
        {
            m_hand.Crush();
        }
    }

    private void ClickCauldron()
    {
        if (m_hand.HasIngredient)
        {
              Debug.Log("OK");
            if (m_hand.PutIngredientIntoCauldron())
            {
                EnableSpoonContent(false);
                
            }
        }
        else
        {
            // Préparation de la potion
            var maker = m_hand.Cook();

            if (maker != null)
            {
                // Création du GameObject de la potion
                var potion = Instantiate(PotionPrefab);
                potion.transform.position=new Vector3(100f,100f,100f);
                potion.GetComponent<Potion>().Effects = maker.GetEffects();
                Vitruve.instance.DrinkPotion(potion.GetComponent<Potion>());
            }
        }
    }

    // Activer/désactiver l'affichage du contenu de la cuillère
    private void EnableSpoonContent(bool enable = true)
    {
        if (Spoon != null)
        {
            Spoon.EnableContent(enable);
        }
    }
}
