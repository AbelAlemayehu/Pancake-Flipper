using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine;

public class TestDrag : MonoBehaviour
{
    private bool dragging = false;

    void OnMouseDown() { dragging = true; }
    void OnMouseUp() { dragging = false; }

    void Update()
    {
        if (dragging)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            transform.position = pos;
        }
    }
}
