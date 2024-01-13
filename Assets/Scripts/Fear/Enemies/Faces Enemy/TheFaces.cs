using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TheFaces : MonoBehaviour
{
    #region Variables
    [Tooltip("Amount of time until going to the next face when player is choosing pattern")]
    public int intervalToChoose = 2;
    public int facesInPattern = 3;

    public List<GameObject> faces;                  // A list of all faces on enemy
    public List<GameObject> patternFaces;          // A list of randomly chosen faces involved in the pattern

    private bool faceBeingProcessed = false;       // To check if a coroutine is active
    private bool engaged = false;                  // Check if enemy is facing off with player or not
    private bool resting = true;                   // Check if enemy is displaying the patterns or not
    private bool restingCoroutine = false;         // Check if resting coroutine is being processed or not

    private int faceNumber = 0;                    // To keep track of which face in pattern to activate next
    private int correctSelection = 0;              // To keep track of how many faces the player chose right 
    private float fadeTime = 0.5f;

    private FacesAudio audioSource;
    private Animator animator;

    #endregion

    #region Start and Update
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<FacesAudio>();
        animator = gameObject.GetComponentInParent<Animator>();

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
        for(int i = 0; i < facesInPattern; i++)
        {
            int indexNumber = Random.Range(0, faces.Count);
            patternFaces.Add(faces[indexNumber]);
        }
    }

    // Show pattern when enemy not engaged
    private void PatternControl()
    {
        if(!resting)
        {
            if (!faceBeingProcessed)
            {
                // Check if beginning of pattern or not
                if (faceNumber == 0)
                {
                    StartCoroutine(ActivateFace(patternFaces[faceNumber], true));
                }
                else
                {
                    StartCoroutine(ActivateFace(patternFaces[faceNumber], false));
                }
            }
        }
        else
        {
            StartCoroutine(RestRoutine());
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
        if(engaged)
        {
            if (faces[faceNumber] == patternFaces[correctSelection])
            {
                audioSource.PlayCorrectSelection();

                // Stops all coroutines and resets flag
                StopAllCoroutines();
                faceBeingProcessed = false;

                ResetFaces(0);

                correctSelection++;
                faceNumber = 0;

                // If entire pattern is correct
                if (correctSelection >= patternFaces.Count)
                {
                    gameObject.transform.parent.gameObject.GetComponent<FacesMove>().FacesDeath();
                }
            }
            else
            {
                audioSource.PlayWrongSelection();

                ResetFaces(0);

                //Reset counter so player has to reenter entire pattern
                correctSelection = 0;
                faceNumber = 0;
            }
        }
        else
        {
            return;
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
                SpriteRenderer face = faces[i].GetComponent<SpriteRenderer>();
                face.color = new Color(face.color.r, face.color.g, face.color.b, 0);
            }
        }

    }
    #endregion

    #region Coroutines

    // Randomly turns pattern faces on and off in sequence when enemy is not engaged
    private IEnumerator ActivateFace(GameObject face, bool firstFace)
    {
        // Signal that the coroutine is processing a face
        faceBeingProcessed = true;

        if(firstFace)
        {
            StartCoroutine(face.FadeIn(fadeTime)); // Still need to add marker that this is the beginning of pattern
        }
        else
        {
            StartCoroutine(face.FadeIn(fadeTime));
        }

        yield return new WaitForSeconds(4);
        StartCoroutine(face.FadeOut(fadeTime));
        yield return new WaitForSeconds (1);

        faceNumber++;

        if (faceNumber >= patternFaces.Count)
        {
            faceNumber = 0;
            resting = true;
        }

        // Signal that the coroutine is done
        faceBeingProcessed = false;
    }

    // Goes over each face on the enemy so the player can choose
    private IEnumerator ChooseFace(GameObject face)
    {
        // Signal that coroutine is processing face
        faceBeingProcessed = true;

        // Code to be changed later when we get the faces
        StartCoroutine(face.FadeIn(fadeTime));
        yield return new WaitForSeconds(intervalToChoose);
        StartCoroutine(face.FadeOut(fadeTime));

        faceNumber++;

        if (faceNumber >= faces.Count)
        {
            faceNumber = 0;
        }

        // Signal that the coroutine is done
        faceBeingProcessed = false;
    }

    // Defines what enemy is doing while resting
    private IEnumerator RestRoutine()
    {
        if (restingCoroutine)
        {
            yield return null;
        }
        else
        {
            restingCoroutine = true;
            animator.SetBool("Resting", true);

            int restTime = Random.Range(3, 6);
            yield return new WaitForSeconds(restTime);

            resting = false;
            restingCoroutine = false;
            animator.SetBool("Resting", false);

        }
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
