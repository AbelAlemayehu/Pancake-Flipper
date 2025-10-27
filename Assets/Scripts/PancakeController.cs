using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PancakeController : MonoBehaviour
{
    private bool isFlipped = false;

    // Call this when spatula clicks under pancake
    public void TryFlip()
    {
        if (isFlipped) { return; }

        // Flip the pancake 180 degrees
        transform.Rotate(0, 0, 180f);
        isFlipped = true;
        Debug.Log("Pancake flipped!");

        isFlipped = true;
    }
}
