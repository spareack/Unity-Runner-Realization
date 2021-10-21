using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trupChela : MonoBehaviour
{
    public bool Pomer = false;
    public Manager Mng;
    public bike bk;
    GameObject chel;
    GameObject skin;

    void Start()
    {
    	Mng = GameObject.Find("Manager").GetComponent<Manager>();
    	chel = transform.Find("trup1").gameObject;
    	skin = Instantiate(bk.TripleMasive[Mng.AS.WhatIsSkinShoose][Mng.AS.SkinStyleNumber], chel.transform);
        skin.GetComponent<Animator>().speed = 0.2f;

    }

	void OnCollisionStay(Collision other)
    {
        if ((other.gameObject.tag == "road" || other.gameObject.tag == "obstacle") && !Pomer)
        {
        	transform.parent = other.gameObject.transform;
        	Pomer = true;
        }
	}
}
