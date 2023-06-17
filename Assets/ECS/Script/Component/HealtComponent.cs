using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealtComponent : MonoBehaviour
{
    public int Healt = 100;
    [SerializeField] private Text text;
    private void Update()
    {
        text.text = $"{Healt}";
    }
}
