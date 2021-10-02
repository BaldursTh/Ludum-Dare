using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections.Specialized;

public class DeleteButton : MonoBehaviour
{
    public BoxCollider2D col;
    public SpriteRenderer spriteRenderer;
    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != col) { spriteRenderer.enabled = false; return; }
        spriteRenderer.enabled = true;
        print("hover");

        if (Input.GetMouseButtonDown(0))
        {
            gameObject.transform.parent.gameObject.SetActive(false);

            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}