using UnityEngine;

public class LeftHandActions : MonoBehaviour
{
    public bool EnableMouse = false;

    private LeftHand m_hand;

    private void Start()
    {
        m_hand = GetComponent<LeftHand>();

        Debug.Assert(m_hand != null);
    }

    private void Update()
    {
        if (EnableMouse && Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                HandleAction(hit.collider.gameObject);
            }
        }
    }

    public void HandleAction(GameObject obj)
    {
        Debug.Assert(obj != null);

        switch (obj.tag)
        {
            case "Cauldron":
                ClickCauldron();
                break;

            case "Mortar":
                m_hand.TakeMortar();
                m_hand.PutMortarContentIntoCauldron();
                break;

            case "WaterBottle":
                m_hand.TakeWaterBottle();
                break;
        }
    }

    private void ClickCauldron()
    {
        if (m_hand.HasWaterBottle)
        {
            m_hand.PutWater();
        }
        else if (m_hand.HasMortar)
        {
            m_hand.PutMortarContentIntoCauldron();
        }
    }
}
