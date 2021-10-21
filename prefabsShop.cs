using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefabsShop : MonoBehaviour
{
	// MeshRenderer[] foundMeshObjects;

	// public GameObject body;
	// public GameObject rightFoot;
	// public GameObject leftFoot;
	// public GameObject leftShoulder;
	// public GameObject leftHand;
	// public GameObject rightShoulder;
	// public GameObject rightHand;

	// MeshRenderer bodyMesh;
	// MeshRenderer rightFootMesh;
	// MeshRenderer leftFootMesh;
	// MeshRenderer leftShoulderMesh;
	// MeshRenderer leftHandMesh;
	// MeshRenderer rightShoulderMesh;
	// MeshRenderer rightHandMesh;

	

	void Start()
	{
        // foundMeshObjects = FindObjectsOfType<MeshRenderer>();

	}


	public void changeMaterial(Material mat)
	{

		// foreach(MeshRenderer mesh in foundMeshObjects)
		// {
		// 	mesh.material = mat;
		// }

		// bodyMesh.material = mat;
		// rightFootMesh.material = mat;
		// leftFootMesh.material = mat;
		// leftShoulderMesh.material = mat;
		// leftHandMesh.material = mat;
		// rightShoulderMesh.material = mat;
		// rightHandMesh.material = mat;

        foreach(Transform transf1 in transform)
            foreach(Transform transf2 in transf1.transform)
                foreach(Transform transf3 in transf2.transform)
                {
                	if (transf3.gameObject.GetComponent<MeshRenderer>()) transf3.gameObject.GetComponent<MeshRenderer>().material = mat;
                    foreach(Transform transf4 in transf3.transform) transf4.gameObject.GetComponent<MeshRenderer>().material = mat;
                }
	}
}
