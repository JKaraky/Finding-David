using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerEmotion : MonoBehaviour
{

    public float fear;
    public float maxCalmness;
    public Image fearBar;
    public float dmgPerSecond;
    private float timer = 0;
    public GameObject entity;


    // Start is called before the first frame update
    void Start()
    {
        maxCalmness = fear;
        
    }

    // Update is called once per frame
    void Update()
    {
        // for gradual bar decrease
        timer += Time.deltaTime;

        if(timer >= 1)
        {
            fear -= dmgPerSecond;
            timer = 0;
        }

        // To display fear and clamp values
        fearBar.fillAmount = Mathf.Clamp(fear / maxCalmness, 0,1 );

        if(fear < 0)
        {
            fear = 0;
        }

        // to activate entity when bar is empty
        if(fear == 0)
        {
            entity.SetActive(true);
        }
    }

    // Subscribe and subscribe from event of hitting entity
    private void OnEnable()
    {
        FearControls.EntityIsHit += FillBar;
    }

    private void OnDisable()
    {
        FearControls.EntityIsHit -= FillBar;
    }

    public void FillBar()
    {
        fear = maxCalmness;
    }
}
