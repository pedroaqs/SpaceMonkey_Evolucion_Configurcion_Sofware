using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FrutaManager : MonoBehaviour
{

    public AudioSource audioSource;
    // public AudioClip collectItem;

    // private Rigidbody2D rb2d;

    // bool contacto = false;
    
    void Start()
    {
        // audioSource = GetComponent<AudioSource>();
        // rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(contacto == true){
        //     audioSource.volume = 0.4f;
        //     audioSource.PlayOneShot(collectItem);
        // }
    }

    void OnTriggerEnter2D(Collider2D other){

        if(other.gameObject.tag == "Player"){
            audioSource.Play();
            Debug.Log("Choca fruta");
        }
    }
}
