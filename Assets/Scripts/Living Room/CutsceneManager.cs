using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField]
    PlayableDirector startingCutscene;
    [SerializeField]
    PlayableDirector gaveUpCutscene;

    int gaveUp = 0;
    void Start()
    {
        if (PlayerPrefs.HasKey("GiveUpTrans"))
            gaveUp = PlayerPrefs.GetInt("GiveUpTrans");
        if (gaveUp == 0)
            startingCutscene.Play();
        else
            gaveUpCutscene.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}