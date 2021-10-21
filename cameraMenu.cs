using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMenu : MonoBehaviour
{
    void Start()
    {
        GetComponent<Animator>().Play("startMenuCam" + Random.Range(1, 4).ToString());
    }

}
