using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldAnimator : MonoBehaviour
{
    #region Variables

    Animator animator;
    bool coroutineRunning = false;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!coroutineRunning)
        {
            StartCoroutine("RandomHandMove");
        }
    }

    #region To Trigger Idle Movement
    private IEnumerator RandomHandMove()
    {
        coroutineRunning = true;
        animator.SetTrigger("MoveTrigger");
        int timeForChange = Random.Range(3, 6);
        yield return new WaitForSeconds(timeForChange);
        coroutineRunning = false;
    }
    #endregion

    #region To Trigger Grab And Release Animation

    private void GrabAnimation()
    {
        animator.SetTrigger("PlayerGrabbed");
    }

    private void ReleaseAnimation()
    {
        animator.SetTrigger("PlayerReleased");
    }

    #endregion

    #region Subscribing and Unsubscribing From Event

    private void OnEnable()
    {
        HoldPlayer.GrabbedPlayer += GrabAnimation;
        HoldPlayer.ReleasedPlayer += ReleaseAnimation;
    }

    private void OnDisable()
    {
        HoldPlayer.GrabbedPlayer -= GrabAnimation;
        HoldPlayer.ReleasedPlayer -= ReleaseAnimation;
    }

    #endregion
}
