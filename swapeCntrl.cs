using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapeCntrl : MonoBehaviour
{
    public bike bicycle2;

    Vector2 tapPoint, swipeDelta;
    bool isDragging, isMobilePlatform;
    float minSwipeDelta = 50;
    float doubleCount = 0f;

    public enum SwipeType
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
        DOUBLECLICK
    }

    private void Awake()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        isMobilePlatform = false;
#else
        isMobilePlatform = true;
#endif
    }

    public delegate void OnSwipeInput(SwipeType type);
    public static event OnSwipeInput SwipeEvent;

    void Update()
    {
        doubleCount -= 0.1f;
        if (!isMobilePlatform)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (doubleCount > 0) SwipeEvent(SwipeType.DOUBLECLICK);
                else doubleCount = 1.6f;
                isDragging = true;
                tapPoint = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0)) ResetSwipe();
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    if (doubleCount > 0) SwipeEvent(SwipeType.DOUBLECLICK);
                    else doubleCount = 1.6f;
                    // else doubleCount = 1.2f;
                    isDragging = true;
                    tapPoint = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Canceled ||
                         Input.touches[0].phase == TouchPhase.Ended) ResetSwipe();
            }
        }

        CalculateSwipe();
    }

    void CalculateSwipe()
    {
        swipeDelta = Vector2.zero;

        if (isDragging)
        {
            if (!isMobilePlatform && Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - tapPoint;
            else if (Input.touchCount > 0)
                swipeDelta = Input.touches[0].position - tapPoint;
        }

        if (swipeDelta.magnitude > minSwipeDelta)
        {
            if (SwipeEvent != null)
            {
                if (Mathf.Abs(swipeDelta.x) < Mathf.Abs(swipeDelta.y) && swipeDelta.y > 0) SwipeEvent(SwipeType.UP);
                if (Mathf.Abs(swipeDelta.x) < Mathf.Abs(swipeDelta.y) && swipeDelta.y < 0) SwipeEvent(SwipeType.DOWN);
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y) && swipeDelta.x > 0) SwipeEvent(SwipeType.RIGHT);
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y) && swipeDelta.x < 0) SwipeEvent(SwipeType.LEFT);
                doubleCount = 0;
                ResetSwipe();
            }
            // ResetSwipe();
        }
    }

    void ResetSwipe()
    {
        isDragging = false;
        tapPoint = swipeDelta - Vector2.zero;
    }

}
