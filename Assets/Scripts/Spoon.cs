using UnityEngine;

public class Spoon : MonoBehaviour
{
    public GameObject Content;

    public void EnableContent(bool enable = true)
    {
        if (Content != null)
        {
            Content.SetActive(enable);
        }
    }

    public void DisableContent()
    {
        EnableContent(false);
    }

    public void ToggleContent()
    {
        EnableContent(!Content.activeSelf);
    }
}
