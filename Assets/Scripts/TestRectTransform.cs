using UnityEngine;

public class TestRectTransform : MonoBehaviour
{
    public RectTransform testPanel;

    void Start()
    {
        if (testPanel != null)
        {
            Debug.Log("RectTransform is assigned!");
        }
        else
        {
            Debug.LogWarning("RectTransform is not assigned!");
        }
    }
}
