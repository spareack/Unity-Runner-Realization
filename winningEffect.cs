using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winningEffect : MonoBehaviour
{
	public Vector3 endPos;
	public Manager Mng;
	RectTransform rectTrans;
	Vector3 startPos;
	bool firstPart = true;
	[SerializeField]
	float count = 0;

	private bool getRect;
    public bool falseCoin = false;

    void Start()
    {
    	GetComponent<Animator>().Play("winningRotate" + Random.Range(1, 3).ToString());

    	if (transform.gameObject.tag == "coin") endPos = new Vector3(-0.017f, 1.506f, -9.015f); 
    	// else if (transform.gameObject.tag == "GOLD1") endPos = new Vector3(-0.7f, 7.65f, 5.1f);
    	// else if (transform.gameObject.tag == "GOLD1") endPos = new Vector3(-0.73f, 2.235f, 5.015f);
    	// else if (transform.gameObject.tag == "GOLD1") endPos = new Vector3(-0.73f, 5.235f, 5.015f);
    	else if (transform.gameObject.tag == "Chel") endPos = new Vector3(0.058f, 1.115f, -9.2f);
    	else endPos = new Vector3(0.158f, 1.515f, -9f);
        rectTrans = GetComponent<RectTransform>();
        if (rectTrans) startPos = rectTrans.position;
        else startPos = transform.localPosition;
    }

    void Update()
    {
    	if (rectTrans)
    	{
	    	if (Mathf.Abs(rectTrans.position.y - endPos.y) < (transform.gameObject.tag == "Chel" ? 0.003f : 0.005f)) 
	    	{
	    		if (transform.gameObject.tag == "Chel" && firstPart) 
	    		{
	    			firstPart = false;
	    			endPos += new Vector3(0.3f, 0f, 0f);
	    			rectTrans.position += new Vector3(0.05f, 0f, 0f);
	    		}
	    		else 
                {
                    Destroy(transform.gameObject);
                }
	        }
	        rectTrans.position = Vector3.Lerp(rectTrans.position, endPos, Time.deltaTime * 2f);
    	}
    	else 
    	{
    		count += 0.008f;
    		if (count > 1) 
    		{
                if (falseCoin)
                {
                    Mng.money -= 1;
                    Mng.moneyText.text = "" + Mng.money;
                }
                else
                {
                    Mng.moneyFromCorn.GetComponent<Animator>().Play("getCoinAn");
                    Mng.money += 1;
                    Mng.moneyText.text = "" + Mng.money;
                }
    			Destroy(transform.gameObject);
	    	}
	    	// if (Mathf.Abs(transform.localPosition.y - endPos.y) < 0.15f) Destroy(transform.gameObject);
	        transform.localPosition = Vector3.Lerp(startPos, endPos, count);
	        // transform.position = Vector3.Lerp(startPos, endPos, count);

    	}
    }
}
