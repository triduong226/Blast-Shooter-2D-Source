using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class PlayfabManager : MonoBehaviour
{
    public Text messageText;
    [Header("UI Login")]
    public InputField emailInput;
    public InputField passwordInput;

    [Header("UI Register")]
    public InputField emailRegInput;
    public InputField passwordRegInput;
    public InputField cfmPasswordRegInput;
    public InputField displayNameRegInput;

    [Header("UI Forget")]
    public InputField emailForInput;

    [Header("Panels")]
    public GameObject loginPanel;
    public GameObject registerPanel;
    public GameObject forgetPanel;

    public void RegisterButton(){
        if(passwordRegInput.text.Length < 6){
            messageText.text = "Password too short!!!";
            return;
        }
        if(passwordRegInput.text == cfmPasswordRegInput.text){
            var request = new RegisterPlayFabUserRequest{
            Email = emailRegInput.text,
            Password = passwordRegInput.text,
            DisplayName = displayNameRegInput.text,
            RequireBothUsernameAndEmail = false
            };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
            backAll();
        }
        else{
            messageText.text = "Wrong password or confirm password";
        }
        
    }
    public void backAll(){
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
        forgetPanel.SetActive(false);
    }
    public void OpenRegister(){
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
    }
    public void OpenForget(){
        loginPanel.SetActive(false);
        forgetPanel.SetActive(true);
    }
    void OnRegisterSuccess(RegisterPlayFabUserResult result){
        messageText.text = "Register Success";
    }

    public void LoginButton(){
        var request = new LoginWithEmailAddressRequest{
            Email = emailInput.text,
            Password = passwordInput.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams{
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }
    public void ResetPasswordButton(){
        var request = new SendAccountRecoveryEmailRequest{
            Email = emailForInput.text,
            TitleId = "D29AE"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }
    void OnPasswordReset(SendAccountRecoveryEmailResult result){
        messageText.text = "Password reset mail sent!";
        
    }

    public static PlayfabManager instance;
    void Awake(){
        MakeInstace();
    }

    void MakeInstace(){
        if(instance == null){
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    /*void Login(){
        var request = new LoginWithCustomIDRequest {
            CustomId = "TD",
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams{
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }*/
    void OnLoginSuccess(LoginResult result){
        messageText.text = "Logged in";
        SceneManager.LoadScene("MainMenu"); 
        if(result.InfoResultPayload.PlayerProfile != null)
            GamePlayController.displayNameForAll = result.InfoResultPayload.PlayerProfile.DisplayName;
    }
    
    void OnError(PlayFabError error){
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }
    
    
}
