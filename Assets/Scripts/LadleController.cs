using System.Collections;                               // allows coroutine features
using System.Collections.Generic;                       // allows list/dictionary types
using UnityEngine;                                      // Unity engine functions/classes


public class LadleController : MonoBehaviour
{
   private bool isDragging = false;                     // true when player is dragging ladle
   private bool hasBatter = false;                      // true when ladle scooped batter


   public GameObject pancakeBatterPrefab;               // prefab to spawn when batter is poured
   public Transform griddleCenter;                      // optional position to pour batter onto
   public float pourYOffset = 0.0f;                     // slight vertical offset for spawn position


   void OnMouseDown()  { isDragging = true; }           // start dragging when mouse is pressed
   void OnMouseUp()    { isDragging = false; }          // stop dragging when mouse is released


   void Update()
   {
       if (isDragging)                                  // only move ladle while dragging
       {
           Vector3 mousePos = Camera.main
               .ScreenToWorldPoint(Input.mousePosition); // convert mouse to world position
           mousePos.z = 0;                              // position is 2D
           transform.position = mousePos;               // move ladle to mouse
       }


       if (hasBatter) return;                           // if carrying batter, nothing else here
   }


   void OnTriggerEnter2D(Collider2D other)              // called when ladle touches trigger objects
   {
       if (other.CompareTag("Bowl") && !hasBatter)      // touching bowl & not full
       {
           hasBatter = true;                            // ladle scoops batter
       }
       else if (other.CompareTag("Griddle") && hasBatter) // touching griddle & holding batter
       {
           hasBatter = false;                           // ladle loses batter when pouring


           if (pancakeBatterPrefab != null)             // prefab assigned
           {
               Vector3 spawnPos;                        // where pancake will appear


               if (griddleCenter != null)               // if a specific center is provided
                   spawnPos = new Vector3(
                       griddleCenter.position.x,
                       griddleCenter.position.y + pourYOffset,
                       0f
                   );                                   // spawn near center reference
               else
               {
                   Vector3 c = other.bounds.center;     // fallback: use griddle collider center
                   spawnPos = new Vector3(
                       c.x,
                       c.y + pourYOffset,
                       0f
                   );
               }


               GameObject newPancake =
                   Instantiate(pancakeBatterPrefab, spawnPos, Quaternion.identity);
                                                       // create pancake in scene


               Pancake pancakeScript =
                   newPancake.GetComponent<Pancake>();  // get pancake script behavior
               if (pancakeScript != null)
               {
                   pancakeScript.StartCooking();        // begin cooking timer


                   CookRingUI ring =
                       FindObjectOfType<CookRingUI>();  // find cooking UI in scene
                   if (ring != null)
                   {
                       ring.target = pancakeScript;     // tell UI what pancake to track


                       if (ring.ring != null)
                           ring.ring.enabled = true;    // show the ring image if assigned


                       ring.gameObject.SetActive(true); // turn UI on
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


