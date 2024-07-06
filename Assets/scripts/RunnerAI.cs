using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunnerAI : MonoBehaviour
{
    public float moveSpeed = 4f; // Adjust the speed as needed
    private bool isRunning = false;
    public GameObject[] obstacles;
    private Animator animator;
    public bool shouldMove = true;
    public bool isInAir = true;
    public GameObject blueDot;
    public TMP_Text text;
    private float travalled = 0;
    public float moveDistance;  // Distance to move  // Time to complete the movement
    private float elapsedTime = 0f;
    private bool shouldUpdate = true;
    public float startPosition;
    public float endPosition;
    public SceneController sceneController;
    public AudioSource jumpSound;
    public float totalDistance;
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.keepAnimatorStateOnDisable = true;
        moveDistance = endPosition - startPosition;
    }

    void Update()
    {
        if (isRunning)
        {
            if (shouldMove)
            {
                foreach (var obstacle in obstacles)
                {
                    obstacle.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                }
            }

            if (shouldUpdate && shouldMove)
            {
                elapsedTime += Time.deltaTime;

                // Calculate the interpolation factor between 0 and 1 based on elapsed time and duration
                float t = Mathf.Clamp01(elapsedTime / totalDistance);

                // Use Mathf.Lerp to smoothly interpolate between start and end positions
                float newX = Mathf.Lerp(startPosition, endPosition, t);

                // Update the object's position
                blueDot.gameObject.transform.position = new Vector3(newX, blueDot.gameObject.transform.position.y, blueDot.gameObject.transform.position.z);

                // Check if the movement is complete
                travalled += Time.deltaTime;
                int trav = (int)travalled;
                if (travalled >= totalDistance)
                {
                    shouldUpdate = false;
                    text.fontSize = 16;
                    text.text = totalDistance.ToString();
                    animator.SetBool("Stop", true);
                }
                else if (trav < 10)
                    text.text = "  " + trav;
                else
                { text.text = " " + trav; }

            }
        }
    }


    public void AIRun()
    {
        // This method will be called continuously while the "Run" button is held down
        isRunning = !isRunning;
        animator.SetBool("isRunning", isRunning);
    }

    public void AIJump()
    {
        if (!isRunning)
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("RunJumping", true);
        }
    }
    public void AIStopJump()
    {
        animator.SetBool("isJumping", false);
    }

    public void AIStopRunJumping()
    {
        animator.SetBool("RunJumping", false);
    }

    public void AIPlayJumpSound()
    {
        jumpSound.Play();
    }

    public void AIInAir()
    {
        isInAir = true;
        shouldMove = true;
    }

    public void AInotInAir()
    {
        isInAir = false;
    }
}
