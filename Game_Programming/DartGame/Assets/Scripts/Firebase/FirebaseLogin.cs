using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.UI;
using TMPro;

public class FirebaseLogin : MonoBehaviour
{
	public Text outputText = null;

	public Button playButton = null;

	public Text username = null;
	public Text password = null;

	private void Start()
	{
		playButton.interactable = false;
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
		{
			if (task.Exception != null)
			{
				Debug.LogError(task.Exception);
			}
		});
	}

	public void Login(int user = 0)
	{
		if (user != 0)
		{
			StartCoroutine(SignIn("test" + user + "@test.test", "password"));
			return;
		}

		StartCoroutine(SignIn(username.text, password.text));
	}
	private IEnumerator SignIn(string email, string password)
	{
		outputText.text = "Attempting to log in";
		var auth = FirebaseAuth.DefaultInstance;
		var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

		yield return new WaitUntil(() => loginTask.IsCompleted);

		if (loginTask.Exception != null)
		{
			outputText.text = loginTask.Exception.InnerExceptions[0].InnerException.Message;
		}
		else
		{
			outputText.text = "Logged in as: " + loginTask.Result.Email;
			playButton.interactable = true;
		}

	}

	public void Register()
	{
		StartCoroutine(RegUser(username.text, password.text));
	}
	private IEnumerator RegUser(string email, string password)
	{
		outputText.text = "Attempting to register";
		var auth = FirebaseAuth.DefaultInstance;
		var loginTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

		yield return new WaitUntil(() => loginTask.IsCompleted);

		if (loginTask.Exception != null)
		{
			outputText.text = loginTask.Exception.InnerExceptions[0].InnerException.Message;
		}
		else
		{
			outputText.text = "Registration completed";
		}
	}
}
