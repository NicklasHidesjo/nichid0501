using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;

public class FirebaseTest : MonoBehaviour
{
    FirebaseDatabase db;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError(task.Exception);
            }

            db = FirebaseDatabase.DefaultInstance;
            db.RootReference.Child("Hello").SetValueAsync("World");
        });
    }
}
