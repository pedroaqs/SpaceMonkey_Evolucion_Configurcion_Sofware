using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ManagerController : MonoBehaviour
{

    AudioSource audioSource;
    public AudioClip sonido1;
    public AudioClip sonido2;
    public AudioClip sonido3;

    public GameObject[] hearts;
    private int life;

    // Start is called before the first frame update
    void Start()
    {
        life = hearts.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(life < 1){
            Destroy(hearts[0].gameObject);
        }else if(life < 2){
            Destroy(hearts[1].gameObject);
        }else if(life < 3){
            Destroy(hearts[2].gameObject);
        }   
    }
}
