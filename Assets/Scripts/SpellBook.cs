using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour
{
    public class Page
    {
        // Nom de l'ingrédient (titre de la page)
        public string Ingredient;

        // Liste des organes
        public List<Vitruve.Organ> Organs = new List<Vitruve.Organ>();
    }

    // Canvas
    public Canvas LeftCanvas;
    public Canvas RightCanvas;
    public Transform OrganItemPrefab;

    // Liste des ingredients (conteneurs)
    public IngredientList IngredientList;

    // Homme de Vitruve (afin de récupérer les infos sur les organes)
    public Vitruve Vitruve;

    // Pages du grimoire
    private Dictionary<string, Page> m_pages = new Dictionary<string, Page>();

    // Page actuelle
    public int CurrentPage
    {
        get;
        private set;
    }

    private void Start()
    {
        Debug.Assert(LeftCanvas != null);
        Debug.Assert(RightCanvas != null);

        Debug.Assert(Vitruve != null);
        Debug.Assert(IngredientList != null);
        Debug.Assert(OrganItemPrefab != null);

        foreach (var container in IngredientList.GetContainers())
        {
            AddPage(container.Name);
        }

        DrawPages();
    }

    // Ajouter une page au grimoire
    public void AddPage(string ingredientName)
    {
        var page = new Page
        {
            Ingredient = ingredientName
        };

        if (Vitruve.Organs != null && Vitruve.Organs.Length > 0)
        {
            foreach (var organ in Vitruve.Organs)
            {
                page.Organs.Add(new Vitruve.Organ()
                {
                    Name = organ.Name,
                    Value = organ.Value,
                    MinValue = organ.MinValue,
                    MaxValue = organ.MaxValue,
                    IsChecked = organ.IsChecked,
                    Sprite = organ.Sprite
                });
            }
        }

        Debug.Log("SpellBook: Added ingredient -> " + ingredientName);
        m_pages.Add(ingredientName, page);
    }

    // Recupère le score du joueur
    public int GetScore()
    {
        // On sauvegarde les pages affichées avant de calculer le score
        SaveCurrentPages();

        int score = 0;

        foreach (var ingredient in IngredientList.GetContainers())
        {
            if (m_pages.ContainsKey(ingredient.Name))
            {
                var page = m_pages[ingredient.Name];

            }
        }

        return score;
    }

    // Récupère les réponses
    private List<string> GetAnswers()
    {
        return null;
    }

    // Vérifie la validité de la réponse
    private bool IsAnswerValid(List<Effect> ingredientEffects, List<string> answers)
    {
        if (answers.Count != ingredientEffects.Count)
        {
            // Pas autant de réponses que d'effets
            return false;
        }

        foreach (var answer in answers)
        {
            if (!IsAnswerInEffects(ingredientEffects, answer))
            {
                // La réponse n'est pas dans la liste des effets de l'ingrédient
                return false;
            }
        }

        // Toutes les réponses sont dans la liste des effets de l'ingrédient
        return true;
    }

    // Vérifie si l'effet est un effet de l'ingrédient
    private bool IsAnswerInEffects(List<Effect> ingredientEffects, string answer)
    {
        foreach (var effect in ingredientEffects)
        {
            if (effect.OrganName == answer)
            {
                return true;
            }
        }

        return false;
    }

    // Récupérer la page 'pageNumber'
    // La première page est la page 0
    public Page GetPage(int pageNumber)
    {
        int i = 0;

        foreach (var page in m_pages)
        {
            if (i == pageNumber)
            {
                Debug.Log("SpellBook: Get page " + pageNumber);
                return page.Value;
            }

            ++i;
        }

        Debug.Log("SpellBook: Page " + pageNumber + " not found");
        return null;
    }

    // Récupérer la page de l'ingrédient 'ingredientName'
    public Page GetPage(string ingredientName)
    {
        if (m_pages.ContainsKey(ingredientName))
        {
            Debug.Log("SpellBook: Get page -> " + ingredientName);
            return m_pages[ingredientName];
        }

        Debug.Log("SpellBook: Ingredient " + ingredientName + " not found");
        return null;
    }

    // Page suivante
    public void NextPage()
    {
        if (CurrentPage + 2 < m_pages.Count)
        {
            Debug.Log("SpellBook: Next page");
            SaveCurrentPages();
            CurrentPage += 2;
            DrawPages();
        }
    }

    // Page précédente
    public void PreviousPage()
    {
        if (CurrentPage > 0)
        {
            Debug.Log("SpellBook: Previous page");
            SaveCurrentPages();
            CurrentPage = (CurrentPage > 1) ? CurrentPage - 2 : 0;
            DrawPages();
        }

        Debug.LogWarning("SpellBook: Already first page");
    }

    // Sauvegarder les pages actuelles
    private void SaveCurrentPages()
    {
        SavePage(LeftCanvas, GetPage(CurrentPage));
        SavePage(RightCanvas, GetPage(CurrentPage + 1));
    }

    // Sauvegarder une page
    private void SavePage(Canvas canvas, Page page)
    {
        Debug.Assert(canvas != null);

        if (page != null)
        {
            var organList = canvas.transform.Find("Organs");

            for (var i = 0; i < organList.childCount; ++i)
            {
                var organ = organList.GetChild(i);
                var name = organ.Find("Organ Name").GetComponent<Text>().text;

                var ptr = page.Organs.Find((Vitruve.Organ it) => {
                    return it.Name == name;
                });

                if (ptr != null)
                {
                    ptr.IsChecked = organ.Find("CheckBox").GetComponent<Toggle>().isOn;
                }
            }
        }
    }

    // Dessiner les pages actuelles
    private void DrawPages()
    {
        DrawPage(LeftCanvas, GetPage(CurrentPage));
        DrawPage(RightCanvas, GetPage(CurrentPage + 1));
    }

    // Dessiner une page dans le canvas
    private void DrawPage(Canvas canvas, Page page)
    {
        Debug.Assert(canvas != null);

        if (page != null)
        {
            // Titre de la page
            canvas.gameObject.SetActive(true);
            canvas.transform.Find("Title").GetComponent<Text>().text = page.Ingredient;

            // Liste des organs
            var organList = canvas.transform.Find("Organs");
            var i = 0;

            foreach (var organ in page.Organs)
            {
                Transform item;

                if (i < organList.childCount)
                {
                    // Mise à jour d'un item déjà instancié
                    item = organList.GetChild(i);
                    FillOrganItem(item, organ);
                }
                else
                {
                    // Instanciation d'un item
                    item = CreateOrganItem(organList, organ);
                }

                item.localPosition = new Vector3(0.0f, -142.0f + 90.0f * i, 0.0f);
                ++i;
            }
        }
        else
        {
            canvas.gameObject.SetActive(false);
        }
    }

    private void FillOrganItem(Transform item, Vitruve.Organ organ)
    {
        var icon = item.Find("Organ Icon").GetComponent<Image>();
        var name = item.Find("Organ Name").GetComponent<Text>();
        var check = item.Find("CheckBox").GetComponent<Toggle>();

        icon.sprite = organ.Sprite;
        name.text = organ.Name;
        check.isOn = organ.IsChecked;
    }

    private Transform CreateOrganItem(Transform organList, Vitruve.Organ organ)
    {
        var item = Instantiate(OrganItemPrefab, organList);

        if (item != null)
        {
            FillOrganItem(item, organ);
        }

        return item;
    }
}
