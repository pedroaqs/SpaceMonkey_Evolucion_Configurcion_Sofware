using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class MonkeyController : MonoBehaviour
{
    
    public float maxSpeed = 40f; // 5 m s^-1
    public float acceleration = 60f; // 10 m s^-2

    //Sonido
    AudioSource audioSource;
    //public AudioClip acelerar;
    public AudioClip chocar;
    //public AudioClip collectItem;
    public AudioClip chimpance;
    public AudioClip ganaste;
    public AudioClip perdiste;
    public AudioClip denegado;



    private GameManagerController gameManager;
    private Rigidbody2D rb2d;
    private Animator animator;

    private Vector3 lastCheckpointPosition;

    private float timeLeft = 0;

    bool estadoVida = true;
    bool quitarVidaSegundoChoque = true;
    bool cogerfrutamuerto = true;

    bool nextEscene = false;


    const int ANIMACION_IDLE = 0;
    const int ANIMACION_UP = 1;
    const int ANIMACION_LEFT = 2;
    const int ANIMACION_RIGHT = 3;
    const int ANIMACION_DEAD = 4;


    void Start()
    {

        gameManager = FindObjectOfType<GameManagerController>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        // if(rb2d.velocity == new Vector2(0,0)){
        //     audioSource.PlayOneShot(chimpance);
        // }
        CambiarAnimacion(ANIMACION_IDLE);
        timeLeft += Time.deltaTime;
        Debug.Log("Tiempo: "+timeLeft);

        if(estadoVida == true && gameManager.GasolinaActual() > 0 && gameManager.VidaRestante() > 0 ){
            rb2d.freezeRotation = true; //rotación de player
            //algoritmo de aceleración
            if (rb2d.velocity.magnitude > maxSpeed)
                rb2d.velocity = rb2d.velocity.normalized * maxSpeed;

            float horizontal = Input.GetAxis("Horizontal") * acceleration * Time.deltaTime;
            float vertical   = Input.GetAxis("Vertical")   * acceleration * Time.deltaTime;

            rb2d.AddForce(new Vector2(horizontal, vertical), ForceMode2D.Impulse);

            print(rb2d.velocity.magnitude);
            //fin del algoritmo de aceleraciónD

            //ANIMACIONES
            if(Input.GetKey(KeyCode.UpArrow)){
                CambiarAnimacion(ANIMACION_UP);
                //audioSource.volume = 0.02f;
                //audioSource.PlayOneShot(acelerar); 
            }
            if(Input.GetKey(KeyCode.RightArrow)){
                CambiarAnimacion(ANIMACION_RIGHT);
                // audioSource.volume = 0.02f;
                //audioSource.PlayOneShot(acelerar); 
            }
            if(Input.GetKey(KeyCode.LeftArrow)){
                CambiarAnimacion(ANIMACION_LEFT);
                // audioSource.volume = 0.02f;
                //audioSource.PlayOneShot(acelerar); 
            }
            

        }else if(estadoVida == false){
            if(timeLeft > 1 && gameManager.VidaRestante() > 0){
                transform.position = lastCheckpointPosition; // Volver a la posición inicial
                gameManager.LlenarGasolina(100);
                rb2d.rotation = 0;
                estadoVida = true;
                quitarVidaSegundoChoque = true;
                cogerfrutamuerto = true;
            }else{
                CambiarAnimacion(ANIMACION_DEAD);
                if(gameManager.VidaRestante() == 0){
                    audioSource.volume = 0.1f;
                    audioSource.PlayOneShot(perdiste);
                    if(timeLeft > 3){
                        SceneManager.LoadScene(0);
                    }
                }
            }
        }

        //siguiente escena
        if(nextEscene == true && timeLeft > 43 && timeLeft < 46){
            SceneManager.LoadScene(2);
        }

        //Pasar a Mundo2
        if(nextEscene == true && timeLeft > 23 && timeLeft < 26){
            SceneManager.LoadScene(3);
        }

        //Pasar a Ganar
        if(nextEscene == true && timeLeft > 13 && timeLeft < 16){
            SceneManager.LoadScene(4);
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "PlataformaPrincipal"){
            lastCheckpointPosition = transform.position;
        }
        if(other.gameObject.tag == "Obstaculo"){

            cogerfrutamuerto = false;

            audioSource.PlayOneShot(chocar);
            audioSource.volume = 0.3f;
            rb2d.freezeRotation = false; //rotación de player
            if(quitarVidaSegundoChoque == true){
                quitarVidaSegundoChoque = false;
                gameManager.PerderVida();
            }
            estadoVida = false;
            timeLeft = 0;
        }

        //mapas y ganar
        if(gameManager.Score() >= 30 && other.gameObject.tag == "PlataformaFinal"){
            audioSource.PlayOneShot(ganaste);
            audioSource.volume = 0.3f;
            nextEscene = true;
            timeLeft = 42;
        }
        if(other.gameObject.tag == "PlataformaFinal" && gameManager.Score() < 30){
            audioSource.volume = 0.1f;
            audioSource.PlayOneShot(denegado);
        }

        //mapa2
        if(gameManager.Score() >= 30 && other.gameObject.tag == "PlataformaFinal2"){
            audioSource.PlayOneShot(ganaste);
            audioSource.volume = 0.3f;
            nextEscene = true;
            timeLeft = 20;
        }
        if(other.gameObject.tag == "PlataformaFinal2" && gameManager.Score() < 30){
            audioSource.volume = 0.1f;
            audioSource.PlayOneShot(denegado);
        }

        //Mapa3
        if(gameManager.Score() >= 30 && other.gameObject.tag == "PlataformaFinal3"){
            audioSource.PlayOneShot(ganaste);
            audioSource.volume = 0.3f;
            nextEscene = true;
            timeLeft = 10;
        }
        if(other.gameObject.tag == "PlataformaFinal3" && gameManager.Score() < 30){
            audioSource.volume = 0.1f;
            audioSource.PlayOneShot(denegado);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Fruta"){
            // audioSource.volume = 0.3f;
            // audioSource.PlayOneShot(collectItem);
            if(cogerfrutamuerto != false){
                Destroy(other.gameObject);
                gameManager.GanarPuntos(10);
            }
        }
    }

    void CambiarAnimacion(int animacion){
        animator.SetInteger("Estado", animacion);
    }
}
