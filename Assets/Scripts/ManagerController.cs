using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] sonidos;
    [SerializeField] private GameObject[] hearts;

    private int life;

    // Start is called before the first frame update
    void Start()
    {
        life = hearts.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (life < 1)
        {
            DestroyHeart(0);
        }
        else if (life < 2)
        {
            DestroyHeart(1);
        }
        else if (life < 3)
        {
            DestroyHeart(2);
        }
    }

    /*
    Para el principio abierto-cerrado, se ha introducido el m�todo DestroyHeart
    que encapsula la l�gica de destrucci�n de los corazones y la reproducci�n del
    sonido asociado. Esto permite que el c�digo est� abierto a la extensi�n al
    agregar m�s corazones o sonidos en el futuro
     */
    private void DestroyHeart(int index)
    {
        if (index >= 0 && index < hearts.Length)
        {
            Destroy(hearts[index].gameObject);
            PlaySound(sonidos[index]);
            life--;
        }
    }

    private void PlaySound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
}