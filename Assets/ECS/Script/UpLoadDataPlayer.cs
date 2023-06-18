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
    void Start()
    {
        GetGoogleFile();
        StartData();
        //
    }
    private void StartData()
    {
        if (PlayerPrefs.HasKey("DataPlayer"))
        {
            string jsonString = PlayerPrefs.GetString("DataPlayer");
            if (!jsonString.Equals(string.Empty, StringComparison.Ordinal))
            {
                dataPlayer = JsonUtility.FromJson<DataPlayer>(jsonString);
            }
        }
        else
        {
            dataPlayer = new DataPlayer();
        }

        //заполняем данными
        healtComponent.Healt = dataPlayer.healtPlayer;
        Statistic.ShootCount = dataPlayer.shootCount;//обращаемся к статичному классу
    }

    private void GetGoogleFile()
    {
        List<UnityGoogleDrive.Data.File> files = GoogleTools.GetListFile();
        foreach (var item in files)
        {
            Debug.Log(item);
        }
        //for (int i = 0; i < files.Count; i++)
        //{
        //    if (files[i].Name== GoogleTools.NameFile)
        //    {
        //        strId = files[i].Id;
        //    }
        //}
        //if (strId!=null)
        //{
        //    UnityGoogleDrive.Data.File file =GoogleTools.LoadFile(strId);
        //}
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
