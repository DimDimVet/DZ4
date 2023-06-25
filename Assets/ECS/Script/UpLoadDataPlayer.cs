using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityGoogleDrive;
using UnityGoogleDrive.Data;
using System.Text;
using Firebase.Database;

public class UpLoadDataPlayer : MonoBehaviour
{
    private HealtComponent healtComponent;
    private DataPlayer dataPlayer;
    private string[] pole = { "shootCount", "healtPlayer" };
    public int frame;
    private string hashKey = "DataPlayer";
    private Dictionary<string, int> listData;
    private void Awake()
    {
        StartCoroutine(Example());
    }
    private IEnumerator Example()
    {
        int i = 0;
        while (i<3)
        {
            listData = FireBaseTool.LoadData(pole,hashKey);
            yield return new WaitForSeconds(1);
            i++;

        }
        StartData();
    }
    private void StartData()
    {
        healtComponent = new HealtComponent();

        if (listData!=null)
        {
            dataPlayer = new DataPlayer();
            foreach (var item in listData)
            {
                for (int i = 0; i < pole.Length; i++)
                {
                    if (pole[i]== item.Key)
                    {
                        dataPlayer.shootCount = item.Value;
                    }
                }

            }
        }
        else
        {
            //if (PlayerPrefs.HasKey($"{hashKey}"))
            //{
            //    string jsonString = PlayerPrefs.GetString($"{hashKey}");
            //    if (!jsonString.Equals(string.Empty, StringComparison.Ordinal))
            //    {
            //        dataPlayer = JsonUtility.FromJson<DataPlayer>(jsonString);
            //    }
            //}
            //else
            //{
            //    dataPlayer = new DataPlayer();
            //}
        }

        //заполняем данными
        healtComponent.Healt = dataPlayer.healtPlayer;
        Statistic.ShootCount = dataPlayer.shootCount;//обращаемся к статичному классу
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
        //fireBase.SaveData(hashKey, jsonString);
        //GoogleOld
        //SetGoogleFail(jsonString);
    }
}

//srukture 
public struct DataPlayer
{
    public int shootCount;
    public int healtPlayer;
}
