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
    Para la �nica responsabilidad, se ha dividido la l�gica del c�digo
    en m�todos m�s espec�ficos, cada uno con una �nica responsabilidad.
    Esto mejora la legibilidad, la mantenibilidad y facilita la comprensi�n
    del c�digo.Para DRY, se ha eliminado la repetici�n de c�digo al utilizar 
    m�todos para realizar tareas comunes, como el cambio de direcci�n del
    enemigo cuando choca con un l�mite.
    Adem�s, se ha separado la inicializaci�n de componentes en un m�todo
    aparte, lo que mejora la estructura del c�digo y evita repetir el mismo
    c�digo en el m�todo Start().
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