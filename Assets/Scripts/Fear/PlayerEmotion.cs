using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerEmotion : MonoBehaviour
{

    public float fear;
    public float maxCalmness;
    public Image fearBar;


    // Start is called before the first frame update
    void Start()
    {
        maxCalmness = fear;
        
    }

    // Update is called once per frame
    void Update()
    {
        fearBar.fillAmount = Mathf.Clamp(fear / maxCalmness, 0,1 );
    }
}
