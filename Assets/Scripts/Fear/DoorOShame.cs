using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOShame : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerPrefs.SetInt("GiveUpTrans", 1);
    }
}
