using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerController : MonoBehaviour
{

    //Gasolina
    public Image barraGasolina;
    public float gasolinaActual;
    public float gasolinaMaxima;

    //vidas
    public GameObject[] hearts;

    private int lives;
    
    public Text scoreText;
    private int score;


    void Start()
    {
        lives = hearts.Length;
        score = 0;
        PrintLivesInScreen();
        PrintScoreInScreen();
    }
    
    void Update()
    {
        PerderGasolina();
        //barraGasolina.fillAmount = gasolinaActual/gasolinaMaxima;
    }

    public void PerderGasolina(){

        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)){
            gasolinaActual = gasolinaActual - Time.deltaTime*5;
        }
        barraGasolina.fillAmount = gasolinaActual/gasolinaMaxima;
    }


    public int Lives(){
        return lives;
    }

    public int Score(){
        return score;
    }

    public void PerderVida(){
        lives -= 1;
        PrintLivesInScreen();
    }

    public void GanarPuntos(int puntos){
        score += puntos; 
        PrintScoreInScreen();
    }

    private void PrintLivesInScreen(){
        if(lives < 1){
            Destroy(hearts[0].gameObject);
        }else if(lives < 2){
            Destroy(hearts[1].gameObject);
        }else if(lives < 3){
            Destroy(hearts[2].gameObject);
        }   
    }

    private void PrintScoreInScreen(){
        scoreText.text = "Puntaje: " + score;
    }

    public float GasolinaActual(){
        return gasolinaActual;
    }

    public int VidaRestante(){ 
        return lives;
    }

    public void LlenarGasolina(int llenar){
        gasolinaActual = llenar;
    }
}
