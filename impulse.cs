using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class impulse : MonoBehaviour
{
	Rigidbody rb;
	
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
    void Update()
    {
        rb.AddForce(new Vector3(0, 2f, 2f), ForceMode.VelocityChange);
    }
}
