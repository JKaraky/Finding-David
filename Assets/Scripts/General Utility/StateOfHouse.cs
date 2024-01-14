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
        PlayerPrefs.SetFloat("PlayerPositionX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPositionY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", player.transform.position.z);
        PlayerPrefs.SetFloat("PlayerOrientation", player.transform.localScale.x);
        PlayerPrefs.SetInt("TvState", tvScript.TvOn ? 0 : 1);
    }

    public void LoadHouseState()
    {
        Vector3 playerTransform;
        playerTransform.x = PlayerPrefs.GetFloat("PlayerPositionX");
        playerTransform.y = PlayerPrefs.GetFloat("PlayerPositionY");
        playerTransform.z = PlayerPrefs.GetFloat("PlayerPositionZ");
        player.transform.position = playerTransform;

        Vector3 playerScale;
        playerScale.x = PlayerPrefs.GetFloat("PlayerOrientation");
        playerScale.y = player.transform.localScale.y;
        playerScale.z = player.transform.localScale.z;
        player.transform.localScale = playerScale;

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
