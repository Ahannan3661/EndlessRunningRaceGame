using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlayerObstacle : MonoBehaviour
{
    public GameObject parent;
    public GameObject pauseScreen;
    public SceneController sceneController;
    private bool startFading = false;
    private bool startedFading = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startFading && !startedFading)
        {
            startedFading = true;
            StartCoroutine(FadeOutAndMoveLeft());
            startFading = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RunnerPlayer rp = collision.gameObject.GetComponent<RunnerPlayer>();
        if(rp == null)
        {
            RunnerAI ri = collision.gameObject.GetComponent<RunnerAI>();
            if (!ri.isInAir)
            {
                ri.shouldMove = false;
            }
            else
            {
                //sceneController.PauseGame(pauseScreen);
                ri.shouldMove = true;
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.excludeLayers = LayerMask.NameToLayer("Everything");
                startFading = true;
            }
        }
        else
        {
            if (!rp.isInAir)
            {
                rp.shouldMove = false;
            }
            else
            {
                sceneController.PauseGame(pauseScreen);
                rp.shouldMove = true;
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.excludeLayers = LayerMask.NameToLayer("Everything");
                startFading = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        RunnerPlayer rp = collision.gameObject.GetComponent<RunnerPlayer>();
        if (rp == null)
        {
            RunnerAI ri = collision.gameObject.GetComponent<RunnerAI>();
            if (!ri.isInAir)
            {
                ri.shouldMove = false;
            }
            else
            {
                //sceneController.PauseGame(pauseScreen);
                ri.shouldMove = true;
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.excludeLayers = LayerMask.NameToLayer("Everything");
                startFading = true;
            }
        }
        else
        {
            if (!rp.isInAir)
            {
                rp.shouldMove = false;
            }
            else
            {
                sceneController.PauseGame(pauseScreen);
                rp.shouldMove = true;
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.excludeLayers = LayerMask.NameToLayer("Everything");
                startFading = true;
            }
        }
    }

    private IEnumerator fadeOut()
    {
        for (int i = 0; i < 10; i++)
        {
            GetComponent<SpriteRenderer>().color = new Vector4(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, GetComponent<SpriteRenderer>().color.a - 0.1f);
            
            parent.GetComponent<SpriteRenderer>().color = new Vector4(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, GetComponent<SpriteRenderer>().color.a - 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator FadeOutAndMoveLeft()
    {
        float fadeSpeed = 1f;  // Adjusted for a one-second fade-out
        float moveSpeed = 4f;

        while (GetComponent<SpriteRenderer>().color.a > 0)
        {
            float alpha = GetComponent<SpriteRenderer>().color.a - fadeSpeed * Time.deltaTime;
            GetComponent<SpriteRenderer>().color = new Vector4(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, alpha);

            parent.GetComponent<SpriteRenderer>().color = new Vector4(parent.GetComponent<SpriteRenderer>().color.r, parent.GetComponent<SpriteRenderer>().color.g, parent.GetComponent<SpriteRenderer>().color.b, alpha);

            // Move the object left
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            yield return null;
        }
    }

}
