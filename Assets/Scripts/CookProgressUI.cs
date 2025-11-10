using UnityEngine;
using UnityEngine.UI;

public class CookProgressUI : MonoBehaviour
{
    // Drag the Pancake component (the parent) into this slot in the Inspector
    public Pancake targetPancake; 
    
    // Drag the 'PancakeProgressTimer' Image object (the one set to Filled) into this slot
    public Image fillImage; 

    void Update()
    {
        // 1. Check if the pancake is cooking and if references are set
        if (targetPancake == null || !targetPancake.isCooking)
        {
            // Deactivate the whole UI when not cooking (hiding the bar)
            gameObject.SetActive(false); 
            return;
        }

        // Ensure the UI is visible when cooking starts
        gameObject.SetActive(true);

        // --- Calculate Progress ---

        // The time spent cooking *after* the initial batter delay
        float adjustedTime = targetPancake.cookTime - targetPancake.batterDelay;

        // The total time range from the end of the batter phase to the burnt phase
        // This is the duration that the bar will take to fill from 0 to 1
        float totalCookingRange = targetPancake.burntTime;

        // Calculate the progress ratio (0.0 to 1.0)
        // Mathf.Clamp01 ensures the value stays between 0 and 1
        float progressRatio = Mathf.Clamp01(adjustedTime / totalCookingRange);
        
        // Apply the ratio to the UI Image fill amount
        fillImage.fillAmount = progressRatio;

        // Optional: Color Feedback
        UpdateQualityColor(adjustedTime);
    }
    
    // Function to change the timer color based on the current cooking phase
    private void UpdateQualityColor(float currentTime)
    {
        // currentTime < 0 is the batter phase (progressRatio is 0)
        if (currentTime < targetPancake.perfectTime)
        {
            fillImage.color = Color.yellow; // Undercooked/Cooking
        }
        else if (currentTime < targetPancake.burntTime)
        {
            fillImage.color = Color.green; // Perfect Zone
        }
        else
        {
            fillImage.color = Color.red; // Burnt
        }
    }
}


