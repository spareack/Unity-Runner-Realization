using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
	public GameObject bike;
	public Transform forCameraTrans;
	private bike bikeScr;
	private bool shaking = false;
	private Vector3 startPosition;
	public float elapsed = 0;

    void Start()
    {
        forCameraTrans = transform.parent;
        bikeScr = bike.GetComponent<bike>();
        startPosition = transform.localPosition;
    }

    void LateUpdate()
    {
    	// transform.position = new Vector3(bike.transform.position.x, bike.transform.position.y + 5 , bike.transform.position.z - 8);
        // forCameraTrans.position = Vector3.Lerp(forCameraTrans.position, checkObstacleUpper(bike.transform.position + new Vector3(0f, 5f, -8f)), Time.deltaTime *3.5f);
        // forCameraTrans.position = Vector3.Lerp(forCameraTrans.position, new Vector3(forCameraTrans.position.x, bike.transform.position.y + 5f, forCameraTrans.position.z),  Time.deltaTime *3f);
    }

    void Update()
    {
    	if (!shaking)
    	{
	    	transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, Time.deltaTime * 3f);
    	}
    }

    public Vector3 checkObstacleUpper(Vector3 pos)
    {
    	if (bikeScr.affectedByDeath) return pos;
    	if (Physics.Raycast(forCameraTrans.position, Vector3.up, 0.5f) && pos.y > forCameraTrans.position.y) 
    		return new Vector3(pos.x, forCameraTrans.position.y, pos.z);
    	else return pos;
    }

	public void ShakeCamera(float duration, float magnitude, float noize)
	{
	    StartCoroutine(ShakeCameraCor(duration, magnitude, noize));
	    // StartCoroutine(ShakeCameraCor(duration, magnitude * -1f, noize));
	}

	private IEnumerator ShakeCameraCor(float duration, float magnitude, float noize)
	{
		shaking = true;
	    elapsed = 0f;
	    startPosition = transform.localPosition;
	    Vector2 noizeStartPoint0 = Random.insideUnitCircle * noize;
	    Vector2 noizeStartPoint1 = Random.insideUnitCircle * noize;

	    while (elapsed < duration)
	    {
	        Vector2 currentNoizePoint0 = Vector2.Lerp(noizeStartPoint0, Vector2.zero, elapsed / duration);
	        Vector2 currentNoizePoint1 = Vector2.Lerp(noizeStartPoint1, Vector2.zero, elapsed / duration);
	        Vector2 cameraPostionDelta = new Vector2(Mathf.PerlinNoise(currentNoizePoint0.x, currentNoizePoint0.y), Mathf.PerlinNoise(currentNoizePoint1.x, currentNoizePoint1.y));
	        cameraPostionDelta *= magnitude;
	        transform.localPosition = startPosition + (Vector3)cameraPostionDelta;
	        elapsed += Time.deltaTime;
	        yield return null;
	    }
	    shaking = false;
	}
}
