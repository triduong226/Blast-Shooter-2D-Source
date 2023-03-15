using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class MainMenuController : MonoBehaviour
{
    [Header("Windows")]
    public GameObject nameWindow;
    public GameObject leaderboardWindow;

    [Header("Display name windows")]
    public GameObject nameError;
    public InputField nameInput;

    [Header("Leaderboard")]
    public GameObject rowPrefab;
    public Transform rowsParent;
    [Header("UI")]
    public Button leaderBtn, settingBtn, logoutBtn;
    public GameObject leaderPanel, settingPanel, mainPanel, playSelectPanel, usernameSetPanel;
    public InputField PasswordReset;
    public InputField CfmPasswordReset;
    public static bool muted = false, isEndless = false, isStory = false;
    public static int scoreSave = -1;
    public static MainMenuController instance;
    void Awake(){
        MakeInstace();
        GetHighScore();
    }

    void MakeInstace(){
        if(instance == null){
            instance = this;
        }
    }

    public void PlayGameButton(){
        playSelectPanel.SetActive(true);
        mainPanel.SetActive(false);
    }
    public void QuitGameButton(){
        SceneManager.LoadScene("Login PlayFab");
    }
    void Start(){
        leaderBtn.onClick.AddListener(OpenLeader);
        settingBtn.onClick.AddListener(OpenSetting);
    }
    public void OpenLeader(){
        mainPanel.SetActive(false);
        leaderPanel.SetActive(true);
        GetLeaderboard();
    }
    public void OpenSetting(){
        mainPanel.SetActive(false);
        settingPanel.SetActive(true);
    }
    public void endlessMode(){
        isEndless = true;
        isStory = false;
        SceneManager.LoadScene("Endless Mode");
    }
    public void storyMode(){
        isEndless = false;
        isStory = true;
        SceneManager.LoadScene("Story 1");
    }
    public void backPlaySelectPanel(){
        mainPanel.SetActive(true);
        playSelectPanel.SetActive(false);
    }
    
    public void backAll(){
        mainPanel.SetActive(true);
        leaderPanel.SetActive(false);
        settingPanel.SetActive(false);
        playSelectPanel.SetActive(false);
        usernameSetPanel.SetActive(false);
    }
    public void usernameSetBtn(){
        settingPanel.SetActive(false);
        usernameSetPanel.SetActive(true);
    }
    public void Muted(){
        if (!muted)
            AudioListener.volume = 0f;
        else
            AudioListener.volume = 1f;
    }
    public void InformationPanel(){

    }
    public void SendLeaderBoard(int score){
        var request = new UpdatePlayerStatisticsRequest{
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate{
                    StatisticName = "EndlessScore",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdate, OnError);
    }
    void OnLeaderBoardUpdate(UpdatePlayerStatisticsResult result){
        Debug.Log("Success Leaderboard sent");
    }
    public void GetLeaderboard(){
        var request = new GetLeaderboardRequest{
            StatisticName = "EndlessScore",
            StartPosition = 0, 
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }
    void OnLeaderboardGet(GetLeaderboardResult result){
        foreach(Transform item in rowsParent){
            Destroy(item.gameObject);
        }
        foreach (var item in result.Leaderboard){
            GameObject newGO = Instantiate(rowPrefab, rowsParent);
            Text[] texts = newGO.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position +1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();
            Debug.Log(string.Format("PLACE: {0} | ID: {1} | VALUE: {2}", item.Position, item.DisplayName, item.StatValue));
        }
    }
    public void GetHighScore(){
        PlayFabClientAPI.GetPlayerStatistics( 
            new GetPlayerStatisticsRequest(),
            OnGetHighScore, OnError
            );
    }
    void OnGetHighScore(GetPlayerStatisticsResult result){
        Debug.Log("Success on get high score");
        foreach(var eachStat in result.Statistics){
            switch(eachStat.StatisticName){
                case "EndlessScore":
                    scoreSave = eachStat.Value;
                    break;
            }
        }
    }
    public void changeMove(){
        if(!Plane.joystickM){
            Plane.joystickM = true;
        }
        else{
            Plane.joystickM = false;
        }
    }
    void OnError(PlayFabError error){
        Debug.Log(error.GenerateErrorReport());
    }
    public void SubmitNameButton(){
        var request = new UpdateUserTitleDisplayNameRequest{
            DisplayName = nameInput.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }
    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result){
        Debug.Log("Updated");
        nameWindow.SetActive(false);
        leaderboardWindow.SetActive(true);
        GamePlayController.displayNameForAll = result.DisplayName;
    }
}
