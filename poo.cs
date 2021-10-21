using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poo : MonoBehaviour
{
    public GameObject particles;
    Manager Mng;
	Vector3 moveVec;
	Rigidbody rb;
    static public bool JesusCheck = false;
    static public int JesusMa;
    public bool inactive = false;
    public AchievementSave AS = new AchievementSave();

    //private bool upal = false;

    void Start()
    {
        AS = JsonUtility.FromJson<AchievementSave>(PlayerPrefs.GetString("AchievementSave"));
        JesusMa = AS.JesusMax;
        rb = GetComponent<Rigidbody>();
        moveVec = new Vector3(0,0,-1);
        Mng = GameObject.Find("Manager").GetComponent<Manager>();

    }

    void Update()
    {
        if (JesusCheck == false && Mng.scoreReal > JesusMa)
        {
            JesusMa = Mng.scoreReal;
        }
    }

    void OnCollisionStay (Collision other)
    {

    	// transform.Translate(moveVec * Time.deltaTime * MS);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "platform")
        {
            rb.AddForce(new Vector3( 0, 30f, 10f), ForceMode.VelocityChange);
            return;
        }
        if (other.gameObject.tag != "coin" && other.gameObject.tag != "lopata" && other.gameObject.tag != "Player")
        {
            GameObject govnoParticles = Instantiate(particles, transform);
            transform.parent = other.gameObject.transform;
            return;
        }
        if (other.gameObject.tag == "coin") 
        {
            Destroy(other.gameObject);
            JesusCheck = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "lopata") GetCatapulted(other.gameObject.GetComponent<lopata>());
        else if (other.gameObject.tag == "deathZone") Destroy(gameObject);
    }


    void GetCatapulted(lopata lopata)
    {
        if (lopata.isWorking)
        {
            lopata.isWorking = false;
            rb.Sleep();
            rb.AddForce(new Vector3( 0, 35f, 35f), ForceMode.VelocityChange);
            lopata.gameObject.GetComponent<Animator>().Play("lopataUp");
        }
    }

    public IEnumerator shieldCrush()
    {
        inactive = true;
        yield return new WaitForSeconds(1);
        inactive = false;
    }

    public void shreakCrush()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

}
