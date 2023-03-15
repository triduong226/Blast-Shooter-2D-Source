using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int HIGH_SCORE = MainMenuController.scoreSave;


    void Awake(){
        MakeSingleInstance();
    }

    void MakeSingleInstance(){
        if(instance != null){
            Destroy(gameObject);
        }else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetHighScore(string un, int score){
        MainMenuController.instance.SendLeaderBoard(score);
        //SqliteController.instance.UpdateScore(un,score);
        HIGH_SCORE = score;
    }
    public int GetHighScore(){
        return HIGH_SCORE;
    }
}
