using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityGoogleDrive;
using UnityGoogleDrive.Data;
using System.Text;

public class UpLoadDataPlayer : MonoBehaviour
{
    public HealtComponent healtComponent;
    private DataPlayer dataPlayer;
    private string strId;
    private string hashKey = "DataPlayer";
    private void Awake()
    {
        GetGoogleFile();
        StartData();
    }

    //private IEnumerator Start()
    //{
    //    AuthController.RefreshAccessToken();
    //    while (AuthController.IsRefreshingAccessToken)
    //        yield return null;
    //    // Listing files.
    //    GoogleDriveFiles.List().Send().OnDone += fileList =>
    //    {
    //        var files = fileList.Files;
    //        foreach (var f in files)
    //        {
    //            Debug.Log(f);

    //        }
    //    };
    //    //GetGoogleFile();
    //    //StartData();
    //    //
    //}
    private void StartData()
    {
        if (GetGoogleFile()!="")
        {
            string jsonString = GetGoogleFile();
            if (!jsonString.Equals(string.Empty, StringComparison.Ordinal))
            {
                dataPlayer = JsonUtility.FromJson<DataPlayer>(jsonString);
            }
            else
            {
                dataPlayer = new DataPlayer();
            }
        }
        else
        {
            if (PlayerPrefs.HasKey($"{hashKey}"))
            {
                string jsonString = PlayerPrefs.GetString($"{hashKey}");
                if (!jsonString.Equals(string.Empty, StringComparison.Ordinal))
                {
                    dataPlayer = JsonUtility.FromJson<DataPlayer>(jsonString);
                }
            }
            else
            {
                dataPlayer = new DataPlayer();
            }
        }

        //заполняем данными
        healtComponent.Healt = dataPlayer.healtPlayer;
        Statistic.ShootCount = dataPlayer.shootCount;//обращаемся к статичному классу
    }

    private string GetGoogleFile()
    {
        List<UnityGoogleDrive.Data.File> files = GoogleTools.GetListFile();
        string jsonString="";

        for (int i = 0; i < files.Count; i++)
        {
            if (files[i].Name == GoogleTools.NameFile)
            {
                strId = files[i].Id;
            }
        }
        if (strId != null)
        {
            jsonString = GoogleTools.LoadFile(strId);
            return jsonString;
        }
        else
        {
            return jsonString;
        }
    }

    void OnApplicationQuit()
    {
        SaveData();
        //
    }
    private void SetGoogleFail(string jsonString)
    {
        GoogleTools.SaveFile(jsonString);
    }

    private void SaveData()
    {
        dataPlayer = new DataPlayer
        {
            healtPlayer = healtComponent.Healt,
            shootCount = Statistic.ShootCount
        };
        string jsonString = JsonUtility.ToJson(dataPlayer);
        Debug.Log(jsonString);
        //local
        PlayerPrefs.SetString("DataPlayer", jsonString);
        //google
        SetGoogleFail(jsonString);
    }
}

//srukture 
public struct DataPlayer
{
    public int shootCount;
    public int healtPlayer;
}
