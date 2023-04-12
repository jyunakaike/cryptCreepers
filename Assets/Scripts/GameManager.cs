using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;  
    public int time = 30;
    public int difficulty = 1; 
    public bool GameOver;
    [SerializeField] int score; 
    public int Score {
        get => score;
        set { 
            score = value;
            UIManager.Instance.UpdateUIScore(score);
            if(score % 1000 == 0){
                difficulty++;
            }
        }
    }

    public int Time {
        get=> time;
        set{
            time= value;
            UIManager.Instance.UpdateUITime(time);
        }
    }

    private void Awake() {
        if(Instance == null){ 
            Instance = this ;
        }
    }

    private void Start() {
        StartCoroutine(CountDownRoutine());
    }

    IEnumerator CountDownRoutine(){
        while (Time > 0){
            yield return new WaitForSeconds(1);
            Time -= 1;
        }
        // Game over
        GameOver = true; 
        UIManager.Instance.showGameOverScreen();
    }

    public void PlayAgain(){
        SceneManager.LoadScene("Game");
    }
}
