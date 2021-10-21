using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class background : MonoBehaviour
{
	public GameObject chel;
	public GameObject bigChel;
	public GameObject BigChel;
	public GameObject [] bigChels;
    public List<Animator> bigChelsListAnim = new List<Animator>();

	 // -40 < z < 113

	// 45 < rotY < 90

	// y = -89

	// x = -146 || 20

    void Start()
    {
        if (!(SceneManager.GetActiveScene().name == "studyScene")) StartCoroutine(spawnBigChels());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, chel.transform.position.z + 180f);

        for(int i = 0; i < bigChelsListAnim.Count; i++)
        {
            if (bigChelsListAnim[i].GetCurrentAnimatorStateInfo(0).IsName("null"))
            {
                Destroy(bigChelsListAnim[i].gameObject.transform.parent.gameObject);
                bigChelsListAnim.RemoveAt(i);
            }
        }
    }

    void spawnBigChel()
    {
        int yRot = 0;
    	int z = Random.Range(-40, 114);
    	int count = Random.Range(0, 2);
    	int chelCount = Random.Range(0, bigChels.Length);
        if (count == 0)
        {
            if (z > 70) yRot = Random.Range(0, 40);
            else yRot = Random.Range(-40, 0);
            yRot += 90;
        }
        else 
        {
            if (z > 70) yRot = Random.Range(-40, 0);
            else yRot = Random.Range(0, 40);
            yRot -= 90;
        }

    	BigChel = Instantiate(bigChels[chelCount], transform);
    	BigChel.transform.localScale = new Vector3(19.9f, 19.9f, 19.9f);
    	BigChel.transform.localPosition = new Vector3((count == 0 ? -146 : 20), -89, z);
    	BigChel.transform.Rotate(new Vector3(0f, yRot, 0f));
        BigChel.transform.Find("chelAnim").gameObject.GetComponent<Animator>().speed = Random.Range(1, 5);
        bigChelsListAnim.Add(BigChel.transform.Find("chelAnim").gameObject.GetComponent<Animator>());
    }

    IEnumerator spawnBigChels()
    {
        spawnBigChel();
        yield return new WaitForSeconds(Random.Range(10, 25));
        StartCoroutine(spawnBigChels());
    }
}
