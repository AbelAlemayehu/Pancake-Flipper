using UnityEngine;                                  // Unity engine types
using UnityEngine.UI;                               // UI Image + Canvas features


public class CookRingUI : MonoBehaviour
{
   public Pancake target;                           // pancake to track; set at pour time
   public Image ring;                               // reference to the ring UI image
   [SerializeField] private Canvas canvas;          // UI canvas (auto found if empty)


   RectTransform ringRect;                           // rect transform for ring positioning
   RectTransform canvasRect;                         // rect transform of canvas


   void Awake()
   {
       if (!ring)                                   // must have a ring image to work
       {
           Debug.LogError("CookRingUI: ring Image not assigned.");
           enabled = false;                         // stop script from running
           return;
       }


       ringRect = ring.GetComponent<RectTransform>(); 

       if (!canvas)                                 // if canvas wasnt set
           canvas = ring.canvas;                    // auto get canvas from ring

       canvasRect = canvas.GetComponent<RectTransform>(); 
   }


   void Update()
   {
       // hide UI if theres no pancake or its not cooking
       if (!target || !target.isCooking)
       {
           ring.enabled = false;                    // turn ring off
           return;
       }
       ring.enabled = true;                         // show ring when cooking


       // fill progress: based on cooking time
       float t = target.cookTime - target.batterDelay; // time past batter stage
       float total = Mathf.Max(0.0001f, target.burntTime); // avoid divide by zero
       float p = Mathf.Clamp01(t / total);          // normalize 0 to 1
       ring.fillAmount = p;                         // update radial fill


       // color by cooking stage
       if (t < 0f)                                  ring.color = Color.yellow; // still batter
       else if (t < target.perfectTime)             ring.color = Color.yellow; // raw
       else if (t < target.burntTime)               ring.color = Color.green;  // cooked
       else                                         ring.color = Color.red;    // burnt


       // world  screen position
       Vector3 screen =
           Camera.main.WorldToScreenPoint(target.transform.position);


       // convert screen  canvas space
       Vector2 local;
       Camera uiCam =
           (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
           ? null : canvas.worldCamera;

       RectTransformUtility.ScreenPointToLocalPointInRectangle(
           canvasRect, screen, uiCam, out local
       );


       // move UI ring to follow pancake
       ringRect.anchoredPosition = local;           // update UI position
       ringRect.rotation = Quaternion.identity;     // keep from rotating
   }
}


