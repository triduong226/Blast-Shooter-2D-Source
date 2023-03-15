using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;
    void Awake(){
        MakeInstace();
        _SetScore(0);
        if(!Plane.joystickM){
            joystickPanel.SetActive(false);
        }
        else{
            joystickPanel.SetActive(true);
        }
    }

    void MakeInstace(){
        if(instance == null){
            instance = this;
        }
    }

    [SerializeField]
    private AudioSource audioSouce;

    [SerializeField]
    private GameObject pausePanel, gameOverPanel, gameWinPanel, joystickPanel;
    
    [SerializeField]
    private Text scoreText, scorePauseText, endScoreOverText, bestScoreOverText, endScoreWonText, bestScoreWonText, usernameText, healthText;
    public static string displayNameForAll;
    public static bool muted = false;

    public void PauseGameButton(){
        usernameText.text = "ID: " + displayNameForAll;
        scorePauseText.text = "Have a nice day";
        audioSouce.Stop();
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    
    void OnError(PlayFabError error){
        Debug.Log(error.GenerateErrorReport());
    }
    public void ResumeButton(){
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartButton(){
        Plane.health = 3;
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
        if(MainMenuController.isStory){
            SceneManager.LoadScene("Stage 1");
            Debug.Log("Story");
        }
        else{
            SceneManager.LoadScene("Endless Mode");
            Debug.Log("Endless"); 
        }
        
    }
    public void OptionButton(){
        Time.timeScale = 1f;
        MainMenuController.isEndless = false;
        MainMenuController.isStory = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void PlaneDiedShowPanel(int score){
        gameOverPanel.SetActive(true);
        audioSouce.Stop();
        Time.timeScale = 0f;
        if(MainMenuController.isEndless){
            endScoreOverText.text = "SCORE: " + score;
            if(score > GameManager.instance.GetHighScore()){
                GameManager.instance.SetHighScore(LoginManager.userNameText, score);
            }
            bestScoreOverText.text = "Best Score: " + GameManager.instance.GetHighScore();
        }
        else{
            endScoreOverText.text = "Hi " + displayNameForAll;
            bestScoreOverText.text = "You are in " + SceneManager.GetActiveScene().name;
        }
        
    }
    public void PlaneWonShowPanel(int score){
        gameWinPanel.SetActive(true);
        audioSouce.Stop();
        Time.timeScale = 0f;
        if(MainMenuController.isEndless){
            endScoreWonText.text = "SCORE: " + score;
            if(score > GameManager.instance.GetHighScore()){
                GameManager.instance.SetHighScore(LoginManager.userNameText, score);
            }
            bestScoreOverText.text = "Best Score: " + GameManager.instance.GetHighScore();
        }
        else{
            endScoreWonText.text = "Hi " + displayNameForAll;
            bestScoreWonText.text = "Congratulation!!!           You have won this stage";
        }
    }
    public void Muted(){
        if (!muted){
            AudioListener.volume = 0f;
            muted = true;
        }
        else{
            AudioListener.volume = 1f;  
            muted = false;
        }
            

    }
    public void _SetScore(int score)
    {
        if(MainMenuController.isEndless)
            scoreText.text = "SCORE: " + score;
        else
            scoreText.text = SceneManager.GetActiveScene().name;
    }
    public void _SetHeart(int heal){
        healthText.text = " x " + heal;
    }
}
