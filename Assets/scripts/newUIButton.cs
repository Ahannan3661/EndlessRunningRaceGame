using System.Collections;
using UnityEngine;

public class newUIButton : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private string message;
    [SerializeField] private Vector3 startScale;
    [SerializeField] private Vector3 endScale;

    public bool isHold;

    private bool isMoved = false;

    public Color HighlightColor = Color.blue;

    private Collider2D myCollider;

    public GameObject targetObject2;
    public string message2;

    private void Start()
    {
        myCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Collider2D hitCollider = Physics2D.OverlapPoint(touchPosition);

                if (hitCollider != null && hitCollider == myCollider)
                {
                    HandleTouch(hitCollider, touch.phase, touchPosition);
                }
            }
        }
    }

    private void HandleTouch(Collider2D collider, TouchPhase touchPhase, Vector3 touchPosition)
    {
        switch (touchPhase)
        {
            case TouchPhase.Began:
                OnTouchBegin();
                break;

            case TouchPhase.Moved:
                if (isHold && !IsTouchWithinCollider(touchPosition))
                {
                    isMoved = true;
                    OnTouchMove();
                }
                break;

            case TouchPhase.Stationary:
                // Optionally handle stationary touch
                break;

            case TouchPhase.Ended:
                OnTouchEnd();
                break;

            case TouchPhase.Canceled:
                break;
        }
    }

    private bool IsTouchWithinCollider(Vector3 touchPosition)
    {
        return myCollider.OverlapPoint(touchPosition);
    }

    private void OnTouchBegin()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        transform.localScale = endScale;

        if (sprite != null) sprite.color = HighlightColor;

        if (targetObject != null && isHold)
        {
            targetObject.SendMessage(message);
            if (targetObject2 != null) { targetObject2.SendMessage(message2); }
        }
    }

    private void OnTouchEnd()
    {
        if (targetObject != null && (!isHold || (isHold && !isMoved)))
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            transform.localScale = startScale;

            if (sprite != null) sprite.color = Color.white;
            targetObject.SendMessage(message);
            if (targetObject2 != null) { targetObject2.SendMessage(message2); }
        }

        isMoved = false;
    }

    private void OnTouchMove()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        transform.localScale = startScale;

        if (sprite != null) sprite.color = Color.white;
        targetObject.SendMessage(message);

        if (targetObject2 != null) { targetObject2.SendMessage(message2); }
    }
}
