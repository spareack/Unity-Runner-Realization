using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud : MonoBehaviour
{
	[SerializeField]
	private float Speed;
    private float dopSpeed;
	private float posZ;
    private float posY;
    private Vector3 moveVec = new Vector3(0, 0, -1);

    public Manager Mng;

    // void Start()
    // {
    // 	posZ = transform.position.z;
    // 	dopSpeed = Random.Range(0f, 10f);
    //     Speed = Mng.MoveSpeed + dopSpeed;
    //     transform.localScale = new Vector3(Random.Range(20f, 30f), Random.Range(10f, 30f), Random.Range(20f, 40f));
    // }

    // void Update()
    // {
    //     if (Mng.MoveSpeed == 0 && Speed != dopSpeed) Speed = dopSpeed;
    //     else if (Mng.MoveSpeed != 0 && Speed == dopSpeed) Speed = Mng.MoveSpeed + dopSpeed;
    // 	if (posZ - transform.position.z > 600f) Destroy(transform.gameObject);
    //     transform.Translate(moveVec * Time.deltaTime * Speed);
    // }

    void Start()
    {
        posZ = transform.position.z;
        dopSpeed = Random.Range(0f, 10f);
        Speed = Mng.MoveSpeed;
        transform.localScale = new Vector3(Random.Range(20f, 30f), Random.Range(10f, 30f), Random.Range(20f, 40f));
    }

    void Update()
    {
        if (Mng.MoveSpeed == 0 && Speed != 0) Speed = 0;
        else if (Mng.MoveSpeed != 0 && Speed == 0) Speed = Mng.MoveSpeed;
        // if (posY - transform.position.z > 150f) Destroy(transform.gameObject);
        if (Mng.bike.transform.position.z > transform.position.z + 50f) Destroy(transform.gameObject);
        transform.Translate(moveVec * Time.deltaTime * Speed);
        transform.Translate(Vector3.up * Time.deltaTime * dopSpeed);
    }

}
