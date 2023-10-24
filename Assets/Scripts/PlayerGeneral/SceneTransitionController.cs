using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class SceneTransitionController : MonoBehaviour
{
    [SerializeField]
    private string newScene;
    [SerializeField]
    private PlayableDirector director;
    [SerializeField]
    private InputActionReference nextSceneTrigger;
    [SerializeField]
    private string triggerName = "TransitionTrigger";

    private LevelTransition transitionor;
    // transitionor to control if we want to trigger a timeline or quickly jump to a new scene
    void Start()
    {
        transitionor = new LevelTransition(director, newScene);
    }
    // This is called when the button assigned in nextSceneTrigger is pressed while within the trigger area
    // Will quickly jumpt to next scene if there is no timeline set
    private void GoToNextScene()
    {
        if (director != null)
        {
            transitionor.StartTimeline();
        }
        else
            SceneTrans();
    }
    // This is called after a cutscene finishes
    public void SceneTrans()
    {
        transitionor.GoToNextScene();
    }
    // Subscribing and unsubscribing from the trigger button so we only change scenes when in the trigger area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(triggerName))
        {
            nextSceneTrigger.action.performed += ctx => GoToNextScene();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(triggerName))
        {
            nextSceneTrigger.action.performed -= ctx => GoToNextScene();
        }
    }
}
