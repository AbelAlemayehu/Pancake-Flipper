using System.Collections;                     // lets us use coroutines
using UnityEngine;                            // Unity engine tools


public class Pancake : MonoBehaviour
{
   public Sprite rawSprite;                   // image when raw
   public Sprite cookedSprite;                // image when cooked
   public Sprite burntSprite;                 // image when burnt


   public float cookTime = 0f;                // time being cooked
   public float perfectTime = 2f;             // becomes cooked before this
   public float burntTime = 4f;               // becomes burnt after this
   public float batterDelay = 5f;             // delay before raw stage starts


   public bool isCooking = false;             // if pancake is cooking
   public bool isFlipped = false;             // true if flipped once
   bool isDragging = false;                   // true if player dragging
   bool isOnGriddle = true;                   // true if on griddle
   bool isServed = false;                     // true if placed on plate


   private SpriteRenderer spriteRenderer;     // shows pancake image
   private CircleCollider2D col;              // handles collision


   private float fadeDuration = 1f;           // fadein length
   private float fadeTimer = 0f;              // fadein timer


   static int plateSortOrder = 0;             // stack order on plate


   public Vector3 batterScale = new Vector3(4.7618f, 4.4059f, 1f); // size as batter
   public Vector3 rawScale    = new Vector3(3.0355f, 2.8083f, 1f); // size as raw
   public Vector3 doneScale   = new Vector3(0.7219f, 0.6749f, 1f); // size when done


   void Start()
   {
       spriteRenderer = GetComponent<SpriteRenderer>();   // get sprite component
       col = GetComponent<CircleCollider2D>();            // get collider


       if (!isCooking)
           transform.localScale = doneScale;              // set default scale


       if (spriteRenderer != null)
       {
           Color c = spriteRenderer.color;                // grab color
           c.a = 1f;                                      // make visible
           spriteRenderer.color = c;                      // apply
       }
   }


   void Update()
   {
       if (isServed)                                      // if served, stop updating
       {
           spriteRenderer.sortingOrder = plateSortOrder;  // keep stacked
           return;                                        // exit
       }


       if (Input.GetKeyDown(KeyCode.Space))               // press space to flip
           TryFlip();


       if (!isOnGriddle)                                  // ignore if not on griddle
           return;


       if (!isCooking) return;                            // ignore if not cooking


       if (fadeTimer < fadeDuration)                      // fade in
       {
           fadeTimer += Time.deltaTime;                   // update fade timer
           float t = Mathf.Clamp01(fadeTimer / fadeDuration); // 0 to 1 fade amount


           if (spriteRenderer != null)
           {
               Color c = spriteRenderer.color;            // get color
               c.a = t;                                   // update alpha
               spriteRenderer.color = c;                  // apply
           }
       }


       cookTime += Time.deltaTime;                        // count cook time


       if (cookTime < batterDelay)                        // still batter stage
       {
           transform.localScale = batterScale;            // set batter size
           ApplyBatterCollider();                         // batter collider
           return;                                        // stop
       }


       float adjustedTime = cookTime - batterDelay;       // time after batter


       if (adjustedTime < perfectTime)                    // raw stage
       {
           SetStage(rawSprite, rawScale);                 // change to raw
           ApplyRawCollider();                            // raw collider
       }
       else if (adjustedTime < burntTime)                 // cooked stage
       {
           SetStage(cookedSprite, doneScale);             // change to cooked
           ApplyCookedCollider();                         // cooked collider
       }
       else                                               // burnt stage
       {
           SetStage(burntSprite, doneScale);              // change to burnt
           ApplyCookedCollider();                         // cooked collider
       }
   }




   void SetStage(Sprite sprite, Vector3 scale)
   {
       if (spriteRenderer != null)
           spriteRenderer.sprite = sprite;                // swap image


       transform.localScale = scale;                      // swap size
   }



   public void StartCooking()
   {
       // components exist
       if (spriteRenderer == null)
           spriteRenderer = GetComponent<SpriteRenderer>();   // get sprite again


       if (col == null)
           col = GetComponent<CircleCollider2D>();            // get collider again


       isCooking = true;                                      // mark cooking
       isOnGriddle = true;                                    // on griddle
       isFlipped = false;                                     // reset flip
       isServed = false;                                      // not served


       cookTime = 0f;                                         // reset cook time
       fadeTimer = 0f;                                        // reset fade


       if (spriteRenderer != null)
       {
           Color c = spriteRenderer.color;                    // get color
           c.a = 0f;                                          // start transparent
           spriteRenderer.color = c;                          // apply
       }


       transform.localScale = batterScale;                    // start as batter
       ApplyBatterCollider();                                 // apply batter collider
   }



   void ApplyBatterCollider()
   {
       if (!col) return;                                      // safety check
       col.offset = new Vector2(0.0046f, 0.0202f);            // set collider offset
       col.radius = 0.46255f;                                 // set collider size
   }


   void ApplyRawCollider()
   {
       if (!col) return;
       col.offset = new Vector2(-0.014f, 0.0158f);
       col.radius = 0.73635f;
   }


   void ApplyCookedCollider()
   {
       if (!col) return;
       col.offset = new Vector2(-0.03f, -0.03f);
       col.radius = 3.285f;
   }



   // FLIP
   public void TryFlip()
   {
       if (isFlipped) return;                // only flip once
       isFlipped = true;                     // mark flipped
       StartCoroutine(FlipAnim());           // play animation
   }


   IEnumerator FlipAnim()
   {
       float time = 0f;                      // animation timer
       float duration = 0.15f;               // length of flip
       Quaternion start = transform.rotation;                 // start rotation
       Quaternion end = start * Quaternion.Euler(0, 180f, 0); // end rotation


       while (time < duration)               // animate rotation
       {
           time += Time.deltaTime;           // update timer
           float t = time / duration;        // 0 to 1
           transform.rotation = Quaternion.Slerp(start, end, t); // rotate
           yield return null;                // wait next frame
       }


       transform.rotation = end;             // set final rotation
   }




   // DRAG
   void OnMouseDown()
   {
       isDragging = true;                    // start dragging
       isOnGriddle = false;                  // leave griddle
       isCooking = false;                    // stop cooking
   }


   void OnMouseUp()
   {
       isDragging = false;                   // stop dragging
       CheckPlateServe();                    // check if put on plate
   }


   void OnMouseDrag()
   {
       if (!isDragging) return;              // ignore if not dragging
       Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // get mouse position
       pos.z = 0f;                           // keep Z at 0
       transform.position = pos;             // move pancake to mouse
   }


   void CheckPlateServe()
   {
       Collider2D hit = Physics2D.OverlapPoint(transform.position); // check for collider
       if (hit != null && hit.CompareTag("Plate"))                  // if plate under it
           Serve();                                                 // serve pancake
   }


   void Serve()
   {
       Debug.Log("Served pancake!");         // log message


       plateSortOrder++;                     // raise order
       spriteRenderer.sortingOrder = plateSortOrder; // update layer
       isServed = true;                      // mark served
   }
}


