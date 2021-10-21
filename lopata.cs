using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lopata : MonoBehaviour
{
    public bool isWorking = true;
    public Animator an;

    void Start()
    {
    	an = GetComponent<Animator>();
    }

    public void refresh()
    {
    	isWorking = true;
    	an.Play("defCatapult");
    }
}
