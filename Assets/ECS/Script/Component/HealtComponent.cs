using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealtComponent : MonoBehaviour
{
    public Settings SettingsData;
    [HideInInspector]public int Healt;
    [SerializeField] private Text text;
    private void Start()
    {
        if (Healt==0)
        {
            Healt = SettingsData.HealtPlayer;
        }
        
        text.text = $"Healt = {Healt}";
    }
    private void Update()
    {
        //HealtContoll();
    }

    public void HealtContoll(int damage)
    {
        Healt -= damage;
        text.text = $"Healt = {Healt}";
        if (Healt<=0)
        {
            Debug.Log("DestroyPlayer");
        }
        else
        {
            //
        }
    }


}
