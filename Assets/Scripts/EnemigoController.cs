using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoController : MonoBehaviour
{
    private float velocidad = 8;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private GameManagerController gameManager;


    /*
    Para la única responsabilidad, se ha dividido la lógica del código
    en métodos más específicos, cada uno con una única responsabilidad.
    Esto mejora la legibilidad, la mantenibilidad y facilita la comprensión
    del código.Para DRY, se ha eliminado la repetición de código al utilizar 
    métodos para realizar tareas comunes, como el cambio de dirección del
    enemigo cuando choca con un límite.
    Además, se ha separado la inicialización de componentes en un método
    aparte, lo que mejora la estructura del código y evita repetir el mismo
    código en el método Start().
     */
    void Start()
    {
        InitializeComponents();
        gameManager = FindObjectOfType<GameManagerController>();
    }

    void Update()
    {
        Move();
        FlipSprite();
        CheckScore();
    }

    private void InitializeComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Move()
    {
        rb.velocity = new Vector2(velocidad, rb.velocity.y);
    }

    private void FlipSprite()
    {
        sr.flipX = (velocidad < 0) ? true : false;
    }

    private void CheckScore()
    {
        if (gameManager.Score() >= 30)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "limit")
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        velocidad = -velocidad;
    }
}