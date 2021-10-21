using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StyleSwap : MonoBehaviour
{
    private RectTransform contentRect;
    private Vector2 contentVector;
    private Vector2 pansPos;
    public float snapSpeed;
    public GameObject panPrefab;

    void Start()
    {
        contentRect = GetComponent<RectTransform>();
    }


    void FixedUpdate()
    {
        //panPrefab.transform.localPosition = new Vector2(100, 0);
    }
}
