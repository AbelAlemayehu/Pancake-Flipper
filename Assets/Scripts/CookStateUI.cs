using UnityEngine;
using TMPro;

public class CookStateUI : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Update()
    {
        if (GameManager.Instance != null)
        {
            text.text =
                "State: " + GameManager.Instance.pancakeStatus +
                "\nLast: " + GameManager.Instance.lastQuality;

            // === COLOR CODING ===
            if (GameManager.Instance.pancakeStatus == "Raw")
                text.color = Color.white;
            else if (GameManager.Instance.pancakeStatus == "Perfect")
                text.color = Color.green;
            else if (GameManager.Instance.pancakeStatus == "Burnt")
                text.color = Color.red;
            else
                text.color = Color.white;
        }
    }
}



