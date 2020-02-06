using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    bool moveAllowed;
    Collider2D col;

    private GameMaster gm;

    public GameObject collisionEffect;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if(touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
                if (col == touchedCollider)
                {
                    moveAllowed = true;
                }
            }

            if(touch.phase == TouchPhase.Moved)
            {
                if (moveAllowed)
                {
                    transform.position = new Vector2(touchPosition.x, touchPosition.y);
                }
            }

            if(touch.phase == TouchPhase.Ended)
            {
                moveAllowed = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.enabled = false;
        gm.counterForBoom++;
        if (collision.tag == "Planet")
        {
            if (gm.counterForBoom % 2 == 0 && collisionEffect != null)
            {
                collisionEffect.transform.position = collision.transform.position;
                Instantiate(collisionEffect, collision.transform.position, Quaternion.identity);
            }
            if (collision.name == "mercury")
            {
                collision.GetComponent<CircleCollider2D>().enabled = true;
            }
            gm.GameOver();
            Destroy(gameObject);
        }
    }
}