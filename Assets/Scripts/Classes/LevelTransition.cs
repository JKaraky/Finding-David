using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class LevelTransition
{
    private PlayableDirector _director;
    private string _newScene;
    public LevelTransition(PlayableDirector director, string newScene)
    {
        _director = director;
        _newScene = newScene;
    }

    public LevelTransition(string newScene)
    {
        _newScene = newScene;
    }

    public void StartTimeline()
    {
        _director.Play();
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene( _newScene );
    }
}
