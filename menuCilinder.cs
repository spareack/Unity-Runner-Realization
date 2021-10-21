using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuCilinder : MonoBehaviour
{
	public GameObject cube;


	public GameObject column1;
	public GameObject column2;

	public GameObject Cylinder;

	Quaternion startRotation;
	Quaternion endRotation;

	Vector3 scaleColumn;
	float scaleColumnX;
	float scaleColumnY;
	float scaleColumnZ;

	[SerializeField]
	float count = 0f;

    void Start()
    {
    	Debug.Log("lossy " + column1.transform.lossyScale);
    	Debug.Log("local " + column1.transform.localScale);
    	startRotation = Quaternion.Euler(0f, 0f, 0f);
    	endRotation = Quaternion.Euler(0f, 0f, 360f);
    	scaleColumnX = column1.transform.localScale.x;
        scaleColumnY = column1.transform.lossyScale.y + column1.transform.localScale.y;
        scaleColumnZ = column1.transform.lossyScale.z + column1.transform.localScale.z;
    	Debug.Log("scaleColumnYZ " + scaleColumnY + scaleColumnZ);
    }

    void Update()
    {
    	column1.transform.position = cube.transform.position;
    	column1.transform.rotation = cube.transform.rotation;

    	Debug.Log("lossyY " + column1.transform.lossyScale.y + "; localY " + column1.transform.localScale.y);
    	Debug.Log("lossyZ " + column1.transform.lossyScale.x + "; localZ " + column1.transform.localScale.z);
    	// count += 0.001f;
    	// Cylinder.transform.rotation = Quaternion.Slerp(startRotation, endRotation, count);
    }

    void LateUpdate()
    {
        // column1.transform.localScale = new Vector3(scaleColumnX, scaleColumnY - column1.transform.lossyScale.y, scaleColumnZ - column1.transform.lossyScale.z);
     

        // column2.transform.localScale = new Vector3(0f, scaleColumnY - column2.transform.lossyScale.y, scaleColumnZ - column2.transform.lossyScale.z);
        // column2.transform.localScale = scaleColumn - column2.transform.lossyScale;
    }
}
