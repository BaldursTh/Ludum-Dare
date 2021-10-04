using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections.Specialized;

public class DragDialog : MonoBehaviour
{
    public Transform tf;
    public BoxCollider2D col;
	public BoxCollider2D clo;
    public AudioSource audioSource;

    private void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        transform.position = mousePos2D;
        col.enabled = false;
		clo.enabled = false;
    }
    private void OnMouseUp()
    {
        col.enabled = true;
		clo.enabled = true;
    }
}