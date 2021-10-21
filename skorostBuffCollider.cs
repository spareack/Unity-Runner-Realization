using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skorostBuffCollider : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
    	if (other.gameObject.tag == "obstacle" || other.gameObject.tag == "poo")
    	{
    		other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
    	}
    }
}
