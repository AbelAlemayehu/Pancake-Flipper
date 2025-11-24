using System.Collections;                     
using System.Collections.Generic;             
using UnityEngine;                           

public class LadleController : MonoBehaviour
{
    private bool isDragging = false;          // true while player is dragging the ladle
    private bool hasBatter = false;           // true after scooping batter from the bowl

    public GameObject pancakeBatterPrefab;    // prefab that becomes the pancake on the griddle
    public Transform griddleCenter;           // pour location on the griddle
    public float pourYOffset = 0.0f;          // small vertical offset so the pancake sits correctly

    // NEW AUDIO 
    public AudioClip scoopSFX;                // sound to play when scooping batter
    private AudioSource audioSource;          // AudioSource component that plays the clip

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // get the AudioSource attached to this ladle
    }

    void OnMouseDown()  
    { 
        isDragging = true;                    // start dragging the ladle
    }

    void OnMouseUp()    
    { 
        isDragging = false;                   // stop dragging the ladle
    }

    void Update()
    {
        if (isDragging)                       // if the player is dragging:
        {
            // convert mouse position to a world position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;                   // force z = 0 so it stays in 2D
            transform.position = mousePos;    // move the ladle to the mouse
        }

        if (hasBatter) return;                // if holding batter, nothing else needed here
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        // SCOOP BATTER
       
        if (other.CompareTag("Bowl") && !hasBatter)
        {
            hasBatter = true;                 // ladle now contains batter

            if (audioSource != null && scoopSFX != null)
                audioSource.PlayOneShot(scoopSFX);  // play scoop sound once
        }
        
        // POUR BATTER ON GRIDDLE
       
        else if (other.CompareTag("Griddle") && hasBatter)
        {
            hasBatter = false;                // ladle is now empty again

            if (pancakeBatterPrefab != null)
            {
                Vector3 spawnPos;             // position where pancake appears

                if (griddleCenter != null)    // if griddle center object exists:
                {
                    // spawn exactly at its location + offset
                    spawnPos = new Vector3(
                        griddleCenter.position.x,
                        griddleCenter.position.y + pourYOffset,
                        0f
                    );
                }
                else
                {
                    // fallback: use center of griddle collider
                    Vector3 c = other.bounds.center;
                    spawnPos = new Vector3(
                        c.x,
                        c.y + pourYOffset,
                        0f
                    );
                }

                // create a new pancake object in the scene
                GameObject newPancake =
                    Instantiate(pancakeBatterPrefab, spawnPos, Quaternion.identity);

                // get the Pancake script so we can start cooking
                Pancake pancakeScript = newPancake.GetComponent<Pancake>();
                if (pancakeScript != null)
                {
                    pancakeScript.StartCooking();      // begin cooking logic

                    // find the cooking UI ring
                    CookRingUI ring = FindObjectOfType<CookRingUI>();
                    if (ring != null)
                    {
                        ring.target = pancakeScript;   // tell UI which pancake to track

                        if (ring.ring != null)
                            ring.ring.enabled = true;  // make sure the ring graphic is on

                        ring.gameObject.SetActive(true);
                        Debug.Log("CookRing now following: " + pancakeScript.name);
                    }
                    else
                    {
                        Debug.LogWarning("CookRingUI not found in scene.");
                    }
                }
            }
        }
    }
}