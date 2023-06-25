using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FireBaseTool /*: MonoBehaviour*/
{
    private static DatabaseReference reference;//переменная удаленой БД
    public static void SaveData(string hashKey, string json)
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;//инициализируем экземпляр
        reference.Child(hashKey).SetRawJsonValueAsync(json);
    }
    public static Dictionary<string, int> LoadData(string[] pole,string hashKey)
    {
        Dictionary<string, int> listData = new Dictionary<string, int>();
        //
        reference = FirebaseDatabase.DefaultInstance.RootReference;//инициализируем экземпляр
        FirebaseDatabase.DefaultInstance.GetReference(hashKey).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log($"Error{task}");
                listData = null;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                for (int i = 0; i < pole.Length; i++)
                {
                    listData.Add(pole[i],Int32.Parse(snapshot.Child($"{pole[i]}").GetValue(true).ToString()));
                };
            }
        });
        
        return listData;
    }

}
