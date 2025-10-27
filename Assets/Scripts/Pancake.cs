using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pancake : MonoBehaviour
{
    public Sprite rawSprite;
    public Sprite cookedSprite;
    public Sprite burntSprite;

    public float cookTime = 0f;
    public float perfectTime = 2f;
    public float burntTime = 4f;
    public float batterDelay = 5f; // time before batter starts cooking

    private SpriteRenderer spriteRenderer;
    public bool isCooking = false;
    private bool hasStartedCooking = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // starts as batter, whatever the prefabs default sprite is
    }

    void Update()
    {
        if (!isCooking) return;

        // wait for the batter delay before actual cooking begins
        cookTime += Time.deltaTime;

        if (cookTime < batterDelay)
        {
            // stay as batter, on't change sprite yet
            return;
        }

        float adjustedTime = cookTime - batterDelay; // subtract delay time

        if (adjustedTime < perfectTime)
        {
            spriteRenderer.sprite = rawSprite;   // becomes raw
        }
        else if (adjustedTime < burntTime)
        {
            spriteRenderer.sprite = cookedSprite;  // golden
        }
        else
        {
            spriteRenderer.sprite = burntSprite;   // burnt
        }
    }

    public void StartCooking()
    {
        isCooking = true;
        cookTime = 0f;
        Debug.Log("Pancake cooking started (batter first)!");
    }
}
