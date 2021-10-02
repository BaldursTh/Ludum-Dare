using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections.Specialized;

public class DragDialog : MonoBehaviour {
    public Transform tf;
    public BoxCollider2D col;
    public AudioSource audioSource;

    private void Update() {
        if(Input.GetMouseButton(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if(hit.collider != col) return;
            transform.position = mousePos2D;
            
        }
    }
}