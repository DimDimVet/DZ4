using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityGoogleDrive;
using UnityGoogleDrive.Data;
using System.Text;
using Firebase.Database;
using UnityEngine.UI;

public class UpLoadDataPlayer : MonoBehaviour
{
    //UI
    [SerializeField] private Text saveData;
    [SerializeField] private Text loadLocalData;
    [SerializeField] private Text loadFireBaseData;
    //
    [SerializeField] private HealtComponent healtComponent;
    private DataPlayer dataPlayer;
    private string hashKey = "DataPlayer";

    private void Awake()
    {
        StartCoroutine(Example());
    }

    private IEnumerator Example()
    {
        int i = 0;
        while (i<3)
        {
            FireBaseTool.LoadData(hashKey);
            yield return new WaitForSeconds(0.2f);
            i++;
        }
        LoadData();
    }
    private void LoadData()
    {
        Time.timeScale = 1f;
        int swithTypeLoad = 0;
        DataPlayer dataPlayerFireBase=new DataPlayer();
        DataPlayer dataPlayerLocal = new DataPlayer();

        if (FireBaseTool.Snapshot!=null)
        {
            dataPlayerFireBase = new DataPlayer
            {
                healtPlayer = Int32.Parse(FireBaseTool.Snapshot.Child("healtPlayer").GetValue(true).ToString()),
                shootCount = Int32.Parse(FireBaseTool.Snapshot.Child("shootCount").GetValue(true).ToString())
            };
            swithTypeLoad = 1;
            loadFireBaseData.text = $"healtPlayer={dataPlayerFireBase.healtPlayer} shootCount={dataPlayerFireBase.shootCount}";
        }

        if (PlayerPrefs.HasKey($"{hashKey}"))
        {
            string jsonString = PlayerPrefs.GetString($"{hashKey}");
            if (!jsonString.Equals(string.Empty, StringComparison.Ordinal))
            {
                dataPlayerLocal = JsonUtility.FromJson<DataPlayer>(jsonString);
                swithTypeLoad = 2;
            }
            loadLocalData.text = $"healtPlayer={dataPlayerLocal.healtPlayer} shootCount={dataPlayerLocal.shootCount}";
        }
        else
        {
            dataPlayer = new DataPlayer();
            swithTypeLoad = 0;
        }

        //заполняем данными
        
        switch (swithTypeLoad)
        {
            case 0:
                healtComponent.Healt = dataPlayer.healtPlayer;
                Statistic.ShootCount = dataPlayer.shootCount;//обращаемся к статичному классу
                break;
            case 1:
                healtComponent.Healt = dataPlayerFireBase.healtPlayer;
                Statistic.ShootCount = dataPlayerFireBase.shootCount;//обращаемся к статичному классу
                break;
            case 2:
                healtComponent.Healt = dataPlayerLocal.healtPlayer;
                Statistic.ShootCount = dataPlayerLocal.shootCount;//обращаемся к статичному классу
                break;
            default:
                break;
        }
     
    }

    //GoogleOld
    //private string GetGoogleFile()
    //{
    //    List<UnityGoogleDrive.Data.File> files = GoogleTools.GetListFile();
    //    string jsonString="";

    //    for (int i = 0; i < files.Count; i++)
    //    {
    //        if (files[i].Name == GoogleTools.NameFile)
    //        {
    //            strId = files[i].Id;
    //        }
    //    }
    //    if (strId != null)
    //    {
    //        jsonString = GoogleTools.LoadFile(strId);
    //        return jsonString;
    //    }
    //    else
    //    {
    //        return jsonString;
    //    }
    //}

    void OnApplicationQuit()
    {
        SaveData();
        //
    }

    //GoogleOld
    //private void SetGoogleFail(string jsonString)
    //{
    //    GoogleTools.SaveFile(jsonString);
    //}

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
        PlayerPrefs.SetString(hashKey, jsonString);
        //FireBase
        FireBaseTool.SaveData(hashKey, jsonString);
        //GoogleOld
        //SetGoogleFail(jsonString);
        saveData.text = $"healtPlayer={dataPlayer.healtPlayer} shootCount={dataPlayer.shootCount}";
    }
}

//srukture 
public struct DataPlayer
{
    public int shootCount;
    public int healtPlayer;
}
