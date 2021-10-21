using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volna : MonoBehaviour
{
	public float count = 0f;
	public impulse imp;
    public Animator anim;
    private float VolnaPov;
    public ParticleSystem system;

    void Start()
    {
        VolnaPov = bike.VolnaPover;
    }

    void Update()
    {
    	if (count > VolnaPov) StartCoroutine(endFireBall());
    	count += 0.016f;
        transform.position += new Vector3(0, 0, 0.5f);
    }

    IEnumerator endFireBall()
    {
        anim.Play("fireBallEnd");
        system.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        yield return new WaitForSeconds(2f);
        Destroy(transform.gameObject);
    }

    void OnCollisionStay(Collision other)
    {
    	if (other.gameObject.tag == "Player") return;
    	if (!other.gameObject.GetComponent<Rigidbody>()) other.gameObject.AddComponent<Rigidbody>();
    	other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 500f), ForceMode.Acceleration);
    }

 // void OnTriggerEnter(Collider other)
 //    {
 //    	if (!other.gameObject.GetComponent<Rigidbody>()) other.gameObject.AddComponent<Rigidbody>();
 //       	other.gameObject.GetComponent<Rigidbody>().mass = 5f;
 //    	other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 50, 100), ForceMode.Impulse);
 //    }
}
