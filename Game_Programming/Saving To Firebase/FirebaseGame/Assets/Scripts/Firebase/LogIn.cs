using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;
using Firebase.Auth;
using TMPro;

public class LogIn : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI email = null;
    [SerializeField] TextMeshProUGUI password = null;

    [SerializeField] GameObject MainMenu = null;
    [SerializeField] GameObject RegUserScreen = null;

    public void StartUserReg()
    {
        RegUserScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SignIn()
    {
        StartCoroutine(SignIn(email.text, password.text));
    }

    public void SignInAfterReg(string email, string password)
    {
        StartCoroutine(SignIn(email, password));
    }

    private IEnumerator SignIn(string email, string password)
    {
        Debug.Log("Atempting to log in");
        var auth = FirebaseAuth.DefaultInstance;
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        //Show loading animation

        yield return new WaitUntil(() => loginTask.IsCompleted);

        //remove loading animation

        if (loginTask.Exception != null)
            Debug.LogWarning(loginTask.Exception);
        else
            Debug.Log("login completed");

        StartMainMenu();
    }

    private void StartMainMenu()
    {
        MainMenu.SetActive(true);
        FindObjectOfType<SaveManager>().LoadNamesFromFirebase();
        gameObject.SetActive(false);
    }
}
