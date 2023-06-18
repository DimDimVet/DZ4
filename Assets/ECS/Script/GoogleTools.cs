using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityGoogleDrive;
using UnityGoogleDrive.Data;
using System.Text;

public static class GoogleTools
{

    public static string NameFile = "DataDZ4.json";
    public static List<File> GetListFile()//Запросим из хранилища лист существующих файлов
    {
        List<File> getList = new List<File>();
        GoogleDriveFiles.List().Send().OnDone += fileList =>
         {
             getList = fileList.Files;
         };
        return getList;
    }

    public static File SaveFile(string data)//запишем файл в бинарном формиате
    {
        File file = new File {Name=$"{NameFile}",Content=Encoding.ASCII.GetBytes(data) };
        GoogleDriveFiles.Create(file).Send();
        return file;
    }

    public static File LoadFile(string idFile)//получим файл в бинарном формиате по id из листа
    {
        File getFile = new File();
        GoogleDriveFiles.Download(idFile).Send().OnDone += file =>
         {
             getFile = file;
         };
        return getFile;
    }
}
