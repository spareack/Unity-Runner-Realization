using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopPlane : MonoBehaviour
{
	public GameObject chel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	transform.position = new Vector3(transform.position.x, chel.transform.position.y + 55 , chel.transform.position.z + 60);

    }
}
