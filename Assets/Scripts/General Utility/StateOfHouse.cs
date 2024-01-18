using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateOfHouse : MonoBehaviour
{
    [SerializeField] 
    private GameObject player;
    [SerializeField]
    private GameObject tv;
    private TVControl tvScript;

    private void Start()
    {
        tvScript = tv.GetComponent<TVControl>();
    }

    public void SaveHouseState()
    {
        PlayerPrefs.SetInt("TvState", tvScript.TvOn ? 0 : 1);
    }

    public void LoadHouseState()
    {
        tvScript.TvOn = getTvState(PlayerPrefs.GetInt("TvState"));
    }

    private bool getTvState(int state)
    {
        if(state == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
