using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField]Text healthText; 
    [SerializeField]Text scoreText;
    [SerializeField]Text timeText;
    [SerializeField]GameObject gameOverScreen;
    [SerializeField]Text finalScore; 


    private void Awake(){
        if(Instance == null){
            Instance = this;
        }
    }

    public void  UpdateUIScore(int newScore){
        scoreText.text = newScore.ToString();

    }

    public void UpdateUIHealth (int newHealth){
        healthText.text = newHealth.ToString();
    }

    public void UpdateUITime(int newTime){
        timeText.text = newTime.ToString();
    }
    public void showGameOverScreen(){
        gameOverScreen.SetActive(true);
        finalScore.text = ""+GameManager.Instance.Score;
    }
}
