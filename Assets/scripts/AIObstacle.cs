using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIObstacle : MonoBehaviour
{
    public GameObject AI;
    private Animator animator;
    private bool jumpedOver = false;
    float distanceToMove = -20f; // Set the distance you want to move

    public bool shouldUpdate = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = AI.GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldUpdate)
        {
            // Calculate the movement per frame based on the total distance and time
            float movementPerFrame = distanceToMove / 5f * Time.deltaTime;

            // Update the object's position
            transform.position = new Vector3(transform.position.x + movementPerFrame, transform.position.y, transform.position.z);

            if (transform.position.x < 0.5 && !jumpedOver)
            {
                animator.SetBool("Jump", true);
                jumpedOver = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            StartCoroutine(fadeOut());
        }
    }
    private IEnumerator fadeOut()
    {
        for(int i=0; i<10; i++)
        {
            GetComponent<SpriteRenderer>().color = new Vector4(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, GetComponent<SpriteRenderer>().color.a - 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        shouldUpdate = false;
    }
}
