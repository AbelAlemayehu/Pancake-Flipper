using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadleController : MonoBehaviour
{
    private bool isDragging = false;    // Tracks if the ladle is being dragged by the mouse
    private bool hasBatter = false;     // Tracks if the ladle currently has batter in it

    public GameObject pancakeBatterPrefab;  // Prefab for the pancake batter that will spawn on the griddle

    void OnMouseDown() { isDragging = true; }   // When the player clicks on the ladle
    void OnMouseUp() { isDragging = false; }    // When the player releases the mouse button

    void Update()   
    {
        if (isDragging) // If the ladle is beiing dragged, follow the mouse position
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            mousePos.z = 0; // Keep ladle at the same depth, no movement of Z
            transform.position = mousePos;  // Move the ladle to the mouse position
        }
        
        if (hasBatter) return;  // Do nothing if the ladle already has batter

    }

    void OnTriggerEnter2D(Collider2D other) // Detect collision with other objects (like the bowl or griddle)
    {
        if (other.CompareTag("Bowl") && !hasBatter) // The ladle touches the bowl and doesn't have batter yet
        {
            hasBatter = true;   // Now the ladle has batter
            Debug.Log("Scooped batter!");
        }
        else if (other.CompareTag("Griddle") && hasBatter)  // If the ladle touches the griddle and has batter
        {
            hasBatter = false;  // Now the ladle is empty
            Debug.Log("Poured batter!");

            if (pancakeBatterPrefab != null)
            {
                
                // Just spawn right below the ladle, always visible
                Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0f);

                // Create the pancake
                GameObject newPancake = Instantiate(pancakeBatterPrefab, spawnPos, Quaternion.identity);
                Debug.Log("Spawned pancake at: " + spawnPos);

                // Start cooking immediately after pouring
                Pancake pancakeScript = newPancake.GetComponent<Pancake>();
                if (pancakeScript != null)
                {
                    pancakeScript.StartCooking();
                }
            }
        }
    }
}
