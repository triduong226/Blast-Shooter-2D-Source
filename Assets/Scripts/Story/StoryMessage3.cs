using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class StoryMessage3 : MonoBehaviour
{
    public Text messageText;
    public void SkipButton3(){
        SceneManager.LoadScene("Stage 3");
    }
    public void SkipButton1(){
        SceneManager.LoadScene("Stage 1");
    }
    public void SkipButton2(){
        SceneManager.LoadScene("Stage 2");
    }
}
