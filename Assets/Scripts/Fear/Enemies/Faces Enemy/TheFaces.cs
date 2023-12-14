using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheFaces : MonoBehaviour
{
    #region Variables
    [Tooltip("Amount of time until going to the next face when player is facing enemy")]
    public int intervalToChoose = 2;

    public List<GameObject> faces;                  // A list of all faces on enemy
    public List<GameObject> patternFaces;          // A list of randomly chosen faces involved in the pattern

    private bool faceBeingProcessed = false;       // To check if a coroutine is active
    private bool engaged = false;                  // Check if enemy is facing off with player or not

    private int faceNumber = 0;                    // To keep track of which face in pattern to activate next
    private int correctSelection = 0;              // To keep track of how many faces the player chose right 

    #endregion

    #region Start and Update
    // Start is called before the first frame update
    void Start()
    {
        // Fill the faces list with all of this enemy's faces
        for (int i = 0; i < transform.childCount; i++)
        {
            faces.Add(transform.GetChild(i).gameObject);
        }

        // Picks faces that will make up the pattern
        ChooseFaces();
    }

    // Update is called once per frame
    void Update()
    {
        if(!engaged)
        {
            PatternControl();
        }
        else
        {
            ChoiceControl();
        }
    }
    #endregion

    #region Methods
    
    // Create a pattern from the faces which can be made up of 3 or more faces
    private void ChooseFaces()
    {
        int facesQuantity = Random.Range(3, faces.Count + 1);

        for(int i = 0; i < facesQuantity; i++)
        {
            int indexNumber = Random.Range(0, faces.Count);
            patternFaces.Add(faces[indexNumber]);
        }
    }

    // Show pattern when enemy not engaged
    private void PatternControl()
    {
        if(!faceBeingProcessed)
        {
            // Check if beginning of pattern or not
            if(faceNumber == 0)
            {
                StartCoroutine(ActivateFace(patternFaces[faceNumber], true));
            }
            else
            {
                StartCoroutine(ActivateFace(patternFaces[faceNumber], false));
            }
        }
    }

    // Show face to choose when enemy is engaged
    private void ChoiceControl()
    {
        if (!faceBeingProcessed)
        {
            StartCoroutine(ChooseFace(faces[faceNumber]));
        }
    }

    // Face selection by player and its handling
    private void PlayerFaceSelection()
    {
        if (faces[faceNumber] == patternFaces[correctSelection])
        {
            // Stops all coroutines and resets flag
            StopAllCoroutines();
            faceBeingProcessed = false;

            // Highlights the correct choice
            faces[faceNumber].GetComponent<SpriteRenderer>().color = Color.green;

            correctSelection++;
            faceNumber = 0;

            // If entire pattern is correct
            if(correctSelection >= patternFaces.Count)
            {
                gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
        else
        {
            ResetFaces(0);
            
            //Reset counter so player has to reenter entire pattern
            correctSelection = 0;
            faceNumber = 0;
        }
    }

    private void ResetFaces(int index)
    {
        if(index > faces.Count)
        {
            Debug.LogError("Index number is higher than faces in list");
        }
        else
        {
            // Code inside loop must be replaced later on
            for (int i = index; i < faces.Count; i++)
            {
                faces[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

    }
    #endregion

    #region Coroutines

    // Randomly turns pattern faces on and off in sequence when enemy is not engaged
    private IEnumerator ActivateFace(GameObject face, bool firstFace)
    {
        // Random generated time in seconds in which the pattern will show on a face
        int patternHoldTime = Random.Range(2, 6);

        // Random generated time in seconds in which no pattern is showing
        int patternDisappearTime = Random.Range(2, 6);

        // Signal that the coroutine is processing a face
        faceBeingProcessed = true;

        // Code to be changed later when we get the faces
        if(firstFace)
        {
            face.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            face.GetComponent<SpriteRenderer>().color = Color.red;
        }

        yield return new WaitForSeconds(patternHoldTime);
        face.GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds (patternDisappearTime);

        faceNumber++;

        if (faceNumber >= patternFaces.Count)
        {
            faceNumber = 0;
        }

        // Signal that the coroutine is done
        faceBeingProcessed = false;
    }

    // Goes over each face on the enemy in order so the player can choose
    private IEnumerator ChooseFace(GameObject face)
    {
        // Signal that coroutine is processing face
        faceBeingProcessed = true;

        // Code to be changed later when we get the faces
        face.GetComponent<SpriteRenderer>().color = Color.yellow;
        yield return new WaitForSeconds(intervalToChoose);
        face.GetComponent<SpriteRenderer>().color = Color.white;

        faceNumber++;

        if (faceNumber >= faces.Count)
        {
            faceNumber = 0;
        }

        // Signal that the coroutine is done
        faceBeingProcessed = false;
    }
    #endregion

    #region Collider Trigger Actions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(faceBeingProcessed)
            {
                StopAllCoroutines();
                ResetFaces(0);
                faceBeingProcessed = false;
            }

            engaged = true;
            faceNumber = 0;   
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (faceBeingProcessed)
            {
                StopAllCoroutines();
                ResetFaces(0);
                faceBeingProcessed = false;
            }

            engaged = false;
            faceNumber = 0;
        }
    }

    #endregion

    #region On Enable and Disable Actions

    private void OnEnable()
    {
        PlayerInput.Interacted += PlayerFaceSelection;
    }

    private void OnDisable()
    {
        PlayerInput.Interacted -= PlayerFaceSelection;
    }

    #endregion
}
