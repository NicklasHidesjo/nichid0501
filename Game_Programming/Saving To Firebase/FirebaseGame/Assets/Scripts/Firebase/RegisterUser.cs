using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;
using Firebase.Auth;
using TMPro;

public class RegisterUser : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI user = null;
    [SerializeField] TextMeshProUGUI password = null;

    [SerializeField] GameObject LoginScreen = null;

    public void Register()
    {
        StartCoroutine(RegUser(user.text, password.text));
    }

    private IEnumerator RegUser(string email, string password)
    {
        Debug.Log("Starting Registration");
        var auth = FirebaseAuth.DefaultInstance;
        var regTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => regTask.IsCompleted);

        if (regTask.Exception != null)
            Debug.LogWarning(regTask.Exception);
        else
            Debug.Log("Registration Complete");

        LoginScreen.SetActive(true);
        //FindObjectOfType<LogIn>().SignInAfterReg(email, password);
        gameObject.SetActive(false);
    }
}
