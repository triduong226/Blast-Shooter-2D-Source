using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighScoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;
    public Sprite goldImage, silverImage, bronzeImage;
    public Button backBtn;
    public GameObject leaderPanel, mainPanel;

    private class HighScoreEntry{
        public int score;
        public string name;
    }
    
    private void Awake(){
        backBtn.onClick.AddListener(Back);
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);
        
        highscoreEntryList = new List<HighScoreEntry>(){
            new HighScoreEntry{ score = 123, name = "AAA"},
            new HighScoreEntry{ score = 1200, name = "BBB"},
            new HighScoreEntry{ score = 122, name = "CCC"}
        };

        foreach (Account aAccount in SqliteController.accounts){
            highscoreEntryList.Add(new HighScoreEntry() { score = aAccount.bestScore, name = aAccount.userName });
        }



        for (int i = 0; i < highscoreEntryList.Count; i++){
            for(int j = i+1; j < highscoreEntryList.Count; j++){
                if(highscoreEntryList[j].score > highscoreEntryList[i].score){
                    HighScoreEntry tmp = highscoreEntryList[i];
                    highscoreEntryList[i] = highscoreEntryList[j];
                    highscoreEntryList[j] = tmp;
                }
            }
        }


        highscoreEntryTransformList = new List<Transform>();

        foreach(HighScoreEntry highscoreEntry in highscoreEntryList){
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
        
    }
    
    private void CreateHighscoreEntryTransform(HighScoreEntry highscoreEntry, Transform container, List<Transform> transformList){
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count+1;
        string rankString;
        switch(rank){
            default: rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }
        entryTransform.Find("PosText").GetComponent<Text>().text = rankString;
        int score = highscoreEntry.score;
        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();
        string name = highscoreEntry.name;;
        entryTransform.Find("UNText").GetComponent<Text>().text = name;
        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);

        if(rank ==1){
            entryTransform.Find("UNText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("PosText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("ScoreText").GetComponent<Text>().color = Color.green;
        }
        switch(rank){
            default:
                entryTransform.Find("trophy").gameObject.SetActive(false);
                break;
            case 1:
                entryTransform.Find("trophy").GetComponent<Image>().sprite = goldImage;
                break;
            case 2:
                entryTransform.Find("trophy").GetComponent<Image>().sprite = silverImage;
                break;
            case 3:
                entryTransform.Find("trophy").GetComponent<Image>().sprite = bronzeImage;
                break;
        }
        transformList.Add(entryTransform);
    }
    public void Back(){
        leaderPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}
