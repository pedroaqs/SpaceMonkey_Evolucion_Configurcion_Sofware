using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MonkeyController : MonoBehaviour
{
    public float maxSpeed = 40f; // 5 m s^-1
    public float acceleration = 60f; // 10 m s^-2

    AudioSource audioSource;
    public AudioClip chocar;
    public AudioClip chimpance;
    public AudioClip ganaste;
    public AudioClip perdiste;
    public AudioClip denegado;

    private GameManagerController gameManager;
    private Rigidbody2D rb2d;
    private Animator animator;
    private Vector3 lastCheckpointPosition;
    private float timeLeft = 0;
    private bool estadoVida = true;
    private bool quitarVidaSegundoChoque = true;
    private bool cogerfrutamuerto = true;
    private bool nextEscene = false;

    const int ANIMACION_IDLE = 0;
    const int ANIMACION_UP = 1;
    const int ANIMACION_LEFT = 2;
    const int ANIMACION_RIGHT = 3;
    const int ANIMACION_DEAD = 4;


    /*
    El codigo refactorizado:
    -Se reorganizaron algunas secciones de código para 
    mejorar la legibilidad y el flujo de ejecución.
    -Se crearon nuevos métodos para separar la lógica
    y mejorar la claridad.
    -Se eliminaron comentarios innecesarios y líneas 
    de código comentadas.
    -Se utilizaron nombres de métodos y variables más 
    descriptivos.
    */
    void Start()
    {
        gameManager = FindObjectOfType<GameManagerController>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CambiarAnimacion(ANIMACION_IDLE);
        timeLeft += Time.deltaTime;
        Debug.Log("Tiempo: " + timeLeft);

        if (estadoVida && gameManager.GasolinaActual() > 0 && gameManager.VidaRestante() > 0)
        {
            rb2d.freezeRotation = true;

            if (rb2d.velocity.magnitude > maxSpeed)
                rb2d.velocity = rb2d.velocity.normalized * maxSpeed;

            float horizontal = Input.GetAxis("Horizontal") * acceleration * Time.deltaTime;
            float vertical = Input.GetAxis("Vertical") * acceleration * Time.deltaTime;

            rb2d.AddForce(new Vector2(horizontal, vertical), ForceMode2D.Impulse);

            // ANIMACIONES
            if (Input.GetKey(KeyCode.UpArrow))
            {
                CambiarAnimacion(ANIMACION_UP);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                CambiarAnimacion(ANIMACION_RIGHT);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                CambiarAnimacion(ANIMACION_LEFT);
            }
        }
        else if (!estadoVida)
        {
            if (timeLeft > 1 && gameManager.VidaRestante() > 0)
            {
                ResetMonkey();
            }
            else
            {
                CambiarAnimacion(ANIMACION_DEAD);
                if (gameManager.VidaRestante() == 0)
                {
                    HandleGameOver();
                }
            }
        }

        // SIGUIENTE ESCENA
        if (nextEscene && timeLeft > 43 && timeLeft < 46)
        {
            SceneManager.LoadScene(2);
        }
        // PASAR A MUNDO2
        if (nextEscene && timeLeft > 23 && timeLeft < 26)
        {
            SceneManager.LoadScene(3);
        }
        // PASAR A GANAR
        if (nextEscene && timeLeft > 13 && timeLeft < 16)
        {
            SceneManager.LoadScene(4);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PlataformaPrincipal"))
        {
            lastCheckpointPosition = transform.position;
        }
        if (other.gameObject.CompareTag("Obstaculo"))
        {
            HandleObstacleCollision();
        }

        if (gameManager.Score() >= 30)
        {
            HandleMapCompletion(other);
        }
        else
        {
            HandleMapDenied();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fruta"))
        {
            if (cogerfrutamuerto)
            {
                Destroy(other.gameObject);
                gameManager.GanarPuntos(10);
            }
        }
    }

    void CambiarAnimacion(int animacion)
    {
        animator.SetInteger("Estado", animacion);
    }

    void ResetMonkey()
    {
        transform.position = lastCheckpointPosition;
        gameManager.LlenarGasolina(100);
        rb2d.rotation = 0;
        estadoVida = true;
        quitarVidaSegundoChoque = true;
        cogerfrutamuerto = true;
    }

    void HandleGameOver()
    {
        audioSource.volume = 0.1f;
        audioSource.PlayOneShot(perdiste);
        if (timeLeft > 3)
        {
            SceneManager.LoadScene(0);
        }
    }

    void HandleObstacleCollision()
    {
        cogerfrutamuerto = false;
        audioSource.PlayOneShot(chocar);
        audioSource.volume = 0.3f;
        rb2d.freezeRotation = false;

        if (quitarVidaSegundoChoque)
        {
            quitarVidaSegundoChoque = false;
            gameManager.PerderVida();
        }

        estadoVida = false;
        timeLeft = 0;
    }

    void HandleMapCompletion(Collision2D other)
    {
        if (other.gameObject.CompareTag("PlataformaFinal"))
        {
            audioSource.PlayOneShot(ganaste);
            audioSource.volume = 0.3f;
            nextEscene = true;
            timeLeft = 42;
        }
        else if (other.gameObject.CompareTag("PlataformaFinal2"))
        {
            audioSource.PlayOneShot(ganaste);
            audioSource.volume = 0.3f;
            nextEscene = true;
            timeLeft = 20;
        }
        else if (other.gameObject.CompareTag("PlataformaFinal3"))
        {
            audioSource.PlayOneShot(ganaste);
            audioSource.volume = 0.3f;
            nextEscene = true;
            timeLeft = 10;
        }
    }

    void HandleMapDenied()
    {
        audioSource.volume = 0.1f;
        audioSource.PlayOneShot(denegado);
    }
}