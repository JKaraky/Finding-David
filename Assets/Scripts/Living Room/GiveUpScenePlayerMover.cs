using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveUpScenePlayerMover : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    Vector3 playerStartPosition;

    [SerializeField]
    Vector3 goalPosition;

    [SerializeField]
    float moveSpeed;

    private bool movePlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        player.transform.localScale = Vector3.one;
        player.transform.position = playerStartPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (movePlayer)
        {
            float step = moveSpeed * Time.deltaTime;
            player.transform.position = Vector3.MoveTowards(player.transform.position, goalPosition, step);

            if (player.transform.position.x >= goalPosition.x)
            {
                movePlayer = false;
            }
        }
    }

    public void MovePlayer()
    {
        movePlayer = true;
    }
}
