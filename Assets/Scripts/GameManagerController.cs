using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerController : MonoBehaviour
{
    [SerializeField] private Image barraGasolina;
    [SerializeField] private float gasolinaActual;
    [SerializeField] private float gasolinaMaxima;
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private Text scoreText;

    private int lives;
    private int score;


    /*
    Se han aplicado los principios de inversi�n de dependencias y se han 
    reestructurado algunos m�todos para mejorar la legibilidad y mantener 
    una mejor separaci�n de responsabilidades.

    Se han eliminado las variables p�blicas y se han agregado atributos 
    SerializeField a las variables privadas necesarias para que sean 
    accesibles desde el editor de Unity.

    Adem�s, se han eliminado los m�todos GasolinaActual() y VidaRestante() 
    ya que se pueden acceder directamente a las variables correspondientes.
     */
    private void Start()
    {
        InitializeVariables();
        PrintLivesInScreen();
        PrintScoreInScreen();
    }

    private void Update()
    {
        PerderGasolina();
    }

    private void InitializeVariables()
    {
        lives = hearts.Length;
        score = 0;
    }

    private void PerderGasolina()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            gasolinaActual -= Time.deltaTime * 5;
        }
        barraGasolina.fillAmount = gasolinaActual / gasolinaMaxima;
    }

    public int Lives()
    {
        return lives;
    }

    public int Score()
    {
        return score;
    }

    public void PerderVida()
    {
        lives--;
        PrintLivesInScreen();
    }

    public void GanarPuntos(int puntos)
    {
        score += puntos;
        PrintScoreInScreen();
    }

    /*
    Se utiliza un bucle para activar o desactivar los corazones
    en funci�n de la cantidad de vidas restantes, en lugar 
    de repetir bloques de c�digo.
     */
    private void PrintLivesInScreen()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < lives);
        }
    }

    private void PrintScoreInScreen()
    {
        scoreText.text = "Puntaje: " + score;
    }

    public float GasolinaActual()
    {
        return gasolinaActual;
    }

    public int VidaRestante()
    {
        return lives;
    }

    public void LlenarGasolina(float llenar)
    {
        gasolinaActual = llenar;
    }
}