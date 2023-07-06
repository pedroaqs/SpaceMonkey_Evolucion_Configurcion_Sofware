using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoController : MonoBehaviour
{
    private float velocidad = 8;

    Rigidbody2D rb;
    SpriteRenderer sr;

    private GameManagerController gameManager;


    void Start()
    {
        gameManager = FindObjectOfType<GameManagerController>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocidad, rb.velocity.y);
        if(velocidad < 0){
            sr.flipX=true;
        }
        if(velocidad > 0){
            sr.flipX=false;
        }

        if(gameManager.Score() >= 30){
            Destroy(this.gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "limit"){
            velocidad = velocidad * -1;
        }
    }
}
