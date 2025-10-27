using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SpatulaController : MonoBehaviour
{
    private bool isDragging = false;

    void OnMouseDown()
    {
        isDragging = true;

        // Check for pancake under spatula when mouse is clicked
        FlipPancakeUnder();
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            // Move spatula with mouse
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0; // keep on same plane
            transform.position = mousePos;
        }
    }

    void FlipPancakeUnder()
    {
        // Detect a pancake directly under the spatula
        Collider2D hit = Physics2D.OverlapPoint(transform.position);
        if (hit != null)
        {
            PancakeController pancake = hit.GetComponent<PancakeController>();
            if (pancake != null)
            {
                pancake.TryFlip();
            }
        }
    }
}
