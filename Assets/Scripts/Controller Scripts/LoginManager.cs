using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System.Data;

public class LoginManager : MonoBehaviour
{
    public InputField UsernameLogin;
    public InputField PasswordLogin;
    public InputField UsernameRegister;
    public InputField PasswordRegister;
    public InputField CfmPasswordRegister;
    public InputField EmailRegister;
    public InputField UsernameForget;
    public InputField EmailForget;
    public InputField PasswordReset;
    public InputField CfmPasswordReset;
    public Button loginButton, registerButton, openRegisterButton, cancelRegisterButton, 
                    openForgetButton, cancelforgetButton, resetButton, ForgetButton;
    [SerializeField]
    private GameObject loginPanel, registerPanel, forgetPanel, resetPanel;

    public static string userNameText = "N/A",unforget = "",emforget ="";
    public static int idforget = -1, bsForget= -1, bScoreSave;
    static Dictionary<string, string> Account = new Dictionary<string, string>();

    void Start()
    {
        //Subscribe to onClick event
        loginButton.onClick.AddListener(Login);
        registerButton.onClick.AddListener(Register);
        openRegisterButton.onClick.AddListener(OpenRegister);
        cancelRegisterButton.onClick.AddListener(cancelRegister);
        openForgetButton.onClick.AddListener(OpenForget);
        cancelforgetButton.onClick.AddListener(cancelForget);
        ForgetButton.onClick.AddListener(Forget);
        resetButton.onClick.AddListener(Reset);
    }

    public void OpenForget(){
        loginPanel.SetActive(false);
        forgetPanel.SetActive(true);
    }
    public void OpenRegister(){
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
    }
    public void cancelRegister(){
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
    }
    public void cancelForget(){
        loginPanel.SetActive(true);
        forgetPanel.SetActive(false);
    }
    public void Login()
    {
        
        //Get Username from Input then convert it to int
        userNameText = UsernameLogin.text;
        //Get Password from Input 
        string passwordText = PasswordLogin.text;
        foreach (Account aAccount in SqliteController.accounts)
        {
            if(aAccount.userName == userNameText){
                if(aAccount.passWord == passwordText){
                    SceneManager.LoadScene("MainMenu"); 
                    bScoreSave = aAccount.bestScore;
                    Debug.Log("Sucessed");
                    break;
                }
                else{
                    Debug.Log("Invalid password");
                }
            }
            else{
                Debug.Log("Invalid username");
            }
        }
    }
    public void Register(){
        userNameText = UsernameRegister.text;
        //Get Password from Input 
        string password = PasswordRegister.text;
        string cfmPassword = CfmPasswordRegister.text;
        string email = EmailRegister.text;

        if(password == cfmPassword && password != ""){
            if(email.Contains("@")){
                SqliteController.instance.CreateNewAccount(userNameText, password, email);
                Debug.Log("Sucessed");
                loginPanel.SetActive(true);
                registerPanel.SetActive(false);
            }
            else{
                Debug.Log("Invalid Email");
            }            
        }
        else{
            Debug.Log("Invalid password or confirm password");
        }
        
    }
    public void Forget(){
        unforget = UsernameForget.text;
        emforget = EmailForget.text;
        foreach (Account aAccount in SqliteController.accounts)
        {
            if(aAccount.userName == unforget){
                if(aAccount.email == emforget){
                    idforget = aAccount.id;
                    bsForget = aAccount.bestScore;
                    resetPanel.SetActive(true);
                    forgetPanel.SetActive(false);
                    break;
                }
                else{
                    Debug.Log("Invalid email");
                }
            }
            else{
                Debug.Log("Invalid username");
            }
        }
    }
    public void Reset(){
        string password = PasswordReset.text;
        string cfmPassword = CfmPasswordReset.text;
        if(password == cfmPassword && password != ""){
            SqliteController.instance.ResetPassword(idforget, unforget, password, emforget, bsForget);
            Debug.Log("Sucessed");
        }
        else{
            Debug.Log("Invalid password or confirm password");
        }
        loginPanel.SetActive(true);
        resetPanel.SetActive(false);
    }
}
