using Firebase.Database;
using System.Collections;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }

    public delegate void OnLoadedDelegate(string jsonData);
    public delegate void OnSaveDelegate();

    FirebaseDatabase db;

    private void Awake()
	{
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        db = FirebaseDatabase.DefaultInstance;
    }

	public IEnumerator LoadData(string path, OnLoadedDelegate onLoadedDelegate)
    {
        var dataTask = db.RootReference.Child(path).GetValueAsync();
        yield return new WaitUntil(() => dataTask.IsCompleted);

        if (dataTask.Exception != null)
            Debug.LogWarning(dataTask.Exception);

        string jsonData = dataTask.Result.GetRawJsonValue();

        if(onLoadedDelegate != null)
		{
            onLoadedDelegate(jsonData);
        }

    }

    public IEnumerator LoadDataMultiple(string path, OnLoadedDelegate onLoadedDelegate)
    {
        var dataTask = db.RootReference.Child(path).GetValueAsync();
        yield return new WaitUntil(() => dataTask.IsCompleted);
        string jsonData = dataTask.Result.GetRawJsonValue();

        if (dataTask.Exception != null)
            Debug.LogWarning(dataTask.Exception);

        foreach (var item in dataTask.Result.Children)
        {
            onLoadedDelegate(item.GetRawJsonValue());
        }
    }

    public IEnumerator SaveData(string path, string data, OnSaveDelegate onSaveDelegate = null)
    {
        var dataTask = db.RootReference.Child(path).SetRawJsonValueAsync(data);
        yield return new WaitUntil(() => dataTask.IsCompleted);

        if (dataTask.Exception != null)
            Debug.LogWarning(dataTask.Exception);

        if (onSaveDelegate != null)
        {
            onSaveDelegate();
        }
    }

    public IEnumerator CheckForGame(string path, OnLoadedDelegate onLoadedDelegate = null)
    {
        var dataTask = db.GetReference("games").OrderByChild("status").EqualTo("new").GetValueAsync();

        yield return new WaitUntil(() => dataTask.IsCompleted);

        string jsonData = dataTask.Result.GetRawJsonValue();

        if (dataTask.Exception != null)
            Debug.LogWarning(dataTask.Exception);

        if (dataTask.Result.ChildrenCount > 0)
        {
            foreach (var item in dataTask.Result.Children)
            {
                onLoadedDelegate(item.GetRawJsonValue());
                break;
            }
        }
        else
        {
            onLoadedDelegate(jsonData);
        }
    }

    public IEnumerator RemoveCompletedGames()
	{
        var dataTask = db.GetReference("games").OrderByChild("status").EqualTo("Completed").GetValueAsync();

        yield return new WaitUntil(() => dataTask.IsCompleted);

        string jsonData = dataTask.Result.GetRawJsonValue();

        if (dataTask.Exception != null)
            Debug.LogWarning(dataTask.Exception);

        if (dataTask.Result.ChildrenCount > 0)
        {
            foreach (var item in dataTask.Result.Children)
            {
                var gameID = JsonUtility.FromJson<GameInfo>(item.GetRawJsonValue()).gameID;
                var removeTask = db.GetReference("games/" + gameID).RemoveValueAsync();
                yield return new WaitUntil(() => removeTask.IsCompleted);
            }
        }
    }
}
