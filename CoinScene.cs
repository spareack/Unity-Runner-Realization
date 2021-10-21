using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScene : MonoBehaviour
{
    float MS;
	Vector3 moveVec;
	Rigidbody rb;

	public int coinLife;

    void Start()
    {
    	rb = GetComponent<Rigidbody>();
        moveVec = new Vector3(0,0,-1);
        MS = GameObject.Find("Manager").GetComponent<Manager>().MoveSpeed;
    }

    void FixedUpdate()
    {
    	coinLife++;
    	if (coinLife > 1000) Destroy(gameObject);
    	// rb.AddForce(new Vector3( 0, Physics.gravity.y * 3, 0), ForceMode.Acceleration);
    }
    
    void Update()
    {
        MS += 0.02f;
    }

    void OnCollisionStay (Collision other)
    {
    	if (other.gameObject.tag == "deathCoinZone") Destroy(gameObject);
    	transform.Translate(moveVec * Time.deltaTime * MS);


    }

}
