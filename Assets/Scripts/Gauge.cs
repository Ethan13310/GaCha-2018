using UnityEngine;

public class Gauge : MonoBehaviour
{
    public int MaxValue = 4;
    public int Value = 4;
    public int LastValue = 4;

    public Transform ValueImage;
    public Transform LastValueImage;

    private void Start()
    {
        Debug.Assert(ValueImage != null);
        Debug.Assert(LastValueImage != null);

        LastValue = Value;
    }

    public void SetValue(int newValue)
    {
        if (newValue > MaxValue)
        {
            // Update max value
            MaxValue = newValue;
        }

        if (newValue >= 0)
        {
            LastValue = Value;
            Value = newValue;

            float height = (float) Value / MaxValue * 100.0f;

            ValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(100.0f, height);
            ValueImage.GetComponent<RectTransform>().localPosition = new Vector3(0.0f, -(height / 2.0f), 0.0f);
            LastValueImage.GetComponent<RectTransform>().localPosition = new Vector3(0.0f, 50.0f, 0.0f);
        }
    }

    public void ResetValues()
    {
        Value = MaxValue;
        LastValue = Value;

        ValueImage.GetComponent<RectTransform>().sizeDelta = new Vector2(100.0f, 100.0f);
        ValueImage.GetComponent<RectTransform>().localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        LastValueImage.GetComponent<RectTransform>().localPosition = new Vector3(0.0f, 50.0f, 0.0f);
    }
}
