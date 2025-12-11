using System.Collections;        
using UnityEngine;               

public class Pancake : MonoBehaviour
{
    public Sprite rawSprite;     // sprite when pancake is raw
    public Sprite cookedSprite;  // sprite when cooked
    public Sprite burntSprite;   // sprite when burnt

    public float cookTime = 0f;      // total time cooking
    public float perfectTime = 2f;   // finish raw before this
    public float burntTime = 4f;     // becomes burnt after this
    public float batterDelay = 5f;   // before raw stage starts

    public bool isCooking = false;   // true if currently cooking
    public bool isFlipped = false;   // true after flipping
    bool isDragging = false;         // true if player dragging
    bool isOnGriddle = true;         // true if still on griddle
    bool isServed = false;           // true if placed on plate

    private SpriteRenderer spriteRenderer; // renders sprite
    private CircleCollider2D col;          // pancake collider

    private float fadeDuration = 1f;       // fade in time
    private float fadeTimer = 0f;          // fade timer

    static int plateSortOrder = 0;         // global sprite sort order

    public Vector3 batterScale = new Vector3(4.7618f, 4.4059f, 1f); // size of batter
    public Vector3 rawScale    = new Vector3(3.0355f, 2.8083f, 1f); // size of raw
    public Vector3 doneScale   = new Vector3(0.7219f, 0.6749f, 1f); // size of cooked/burnt

    
    // AUDIO
    
    public AudioSource sizzleSFX;       // looping cooking sound
    public AudioSource flipSFX;         // flip sound
    public AudioSource successMusic;    // plays at 5 pancakes

    public static int plateCount = 0;   // global counter of served pancakes

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // get sprite renderer
        col = GetComponent<CircleCollider2D>();          // get collider

        if (!isCooking)
            transform.localScale = doneScale;            // default size before cooking

        if (spriteRenderer != null)
        {
            Color c = spriteRenderer.color;              // get current color
            c.a = 1f;                                    // make fully visible
            spriteRenderer.color = c;                    // apply color
        }
    }

    void Update()
    {
        if (isServed)                                    // once served
        {
            spriteRenderer.sortingOrder = plateSortOrder; // keep stack order
            return;                                       // skip all cooking
        }

        if (Input.GetKeyDown(KeyCode.Space))             // press space to flip
            TryFlip();

        if (!isOnGriddle)                                // if not on griddle:
            return;                                      // skip cook logic

        if (!isCooking) return;                          // skip until cooking starts

        if (fadeTimer < fadeDuration)                    // fade in pancake
        {
            fadeTimer += Time.deltaTime;                 // update fade timer
            float t = Mathf.Clamp01(fadeTimer / fadeDuration); // percent fade

            if (spriteRenderer != null)
            {
                Color c = spriteRenderer.color;          // get color
                c.a = t;                                  
                spriteRenderer.color = c;                // apply color
            }
        }

        cookTime += Time.deltaTime;                      // cook counter

        if (cookTime < batterDelay)                      // still batter stage
        {
            transform.localScale = batterScale;          // set size
            ApplyBatterCollider();                       // apply collider
            return;                                      // stop early
        }

        float adjustedTime = cookTime - batterDelay;     // raw/cooked/burnt timer

        if (adjustedTime < perfectTime)                  // raw stage
        {
            SetStage(rawSprite, rawScale);               // show raw sprite
            ApplyRawCollider();                          // raw collider
        }
        else if (adjustedTime < burntTime)               // cooked stage
        {
            SetStage(cookedSprite, doneScale);           // cooked sprite
            ApplyCookedCollider();                       // cooked collider
        }
        else                                              // burnt stage
        {
            SetStage(burntSprite, doneScale);            // burnt sprite
            ApplyCookedCollider();                       // same collider as cooked
        }
    }

    void SetStage(Sprite sprite, Vector3 scale)
    {
        if (spriteRenderer != null)
            spriteRenderer.sprite = sprite;               // change sprite

        transform.localScale = scale;                    // change size
    }

    public void StartCooking()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>(); // recover renderer if missing

        if (col == null)
            col = GetComponent<CircleCollider2D>();          // recover collider if missing

        isCooking = true;                                   // start cooking
        isOnGriddle = true;                                 // on griddle
        isFlipped = false;                                  // reset flip
        isServed = false;                                   // reset serve flag

        cookTime = 0f;                                      // reset timer
        fadeTimer = 0f;                                     // reset fade

        if (spriteRenderer != null)
        {
            Color c = spriteRenderer.color;                 // grab color
            c.a = 0f;                                       // start invisible
            spriteRenderer.color = c;                       // apply
        }

        transform.localScale = batterScale;                 // show batter size
        ApplyBatterCollider();                              // set collider

        if (sizzleSFX != null)
            sizzleSFX.Play();                               // start sizzling
    }

    void ApplyBatterCollider()
    {
        if (!col) return;
        col.offset = new Vector2(0.0046f, 0.0202f);         // batter offset
        col.radius = 0.46255f;                              // batter size
    }

    void ApplyRawCollider()
    {
        if (!col) return;
        col.offset = new Vector2(-0.014f, 0.0158f);         // raw offset
        col.radius = 0.73635f;                              // raw size
    }

    void ApplyCookedCollider()
    {
        if (!col) return;
        col.offset = new Vector2(-0.03f, -0.03f);           // cooked offset
        col.radius = 3.285f;                                // cooked size
    }

    
    // FLIP LOGIC
    
    public void TryFlip()
    {
        if (isFlipped) return;                              // only flip once

        isFlipped = true;                                   // update flag

        if (flipSFX != null)
            flipSFX.Play();                                 // play flip sound

        StartCoroutine(FlipAnim());                         // animate flip
    }

    IEnumerator FlipAnim()
    {
        float time = 0f;                                    // animation timer
        float duration = 0.15f;                             // flip duration
        Quaternion start = transform.rotation;              // start rotation
        Quaternion end = start * Quaternion.Euler(0, 180f, 0); // end rotation

        while (time < duration)                             // animate each frame
        {
            time += Time.deltaTime;
            float t = time / duration;                      // percent of animation
            transform.rotation = Quaternion.Slerp(start, end, t); // set rotation
            yield return null;                              // wait next frame
        }

        transform.rotation = end;                           // final rotation
    }

    
    // DRAG & SERVE
    
    void OnMouseDown()
    {
        isDragging = true;                                  // start dragging
        isOnGriddle = false;                                // removed from griddle
        isCooking = false;                                  // stop cooking

        if (sizzleSFX != null)
            sizzleSFX.Stop();                               // stop sizzle
    }

    void OnMouseUp()
    {
        isDragging = false;                                 // stop dragging
        CheckPlateServe();                                  // check if served
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector3 pos = Camera.main.WorldToScreenPoint(Input.mousePosition); // mouse pos
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);         // convert back
        pos.z = 0f;                                                        // keep 2D
        transform.position = pos;                                          // move pancake
    }

    void CheckPlateServe()
    {
        Collider2D hit = Physics2D.OverlapPoint(transform.position); // check objects under pancake
        if (hit != null && hit.CompareTag("Plate"))                  // if plate detected:
        {
            Serve();                                                 // serve pancake
        }
    }

    void Serve()
    {
        Debug.Log("Served pancake!");                                // log output

        plateSortOrder++;                                            // increase layer
        spriteRenderer.sortingOrder = plateSortOrder;                // set layer

        isServed = true;                                             // mark served

        plateCount++;                                                // add to global count

        if (plateCount == 5)                                         // when 5 served:
        {
            if (successMusic != null)
                successMusic.Play();                                 // play success music
            else
                Debug.LogWarning("No success music assigned!");       // warn if missing
        }
    }
}