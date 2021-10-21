using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadBlockScr : MonoBehaviour
{
	Vector3 moveVec;
    public float MS;

    public Manager Mng;
    public bike bike;
    public GameObject[] coinsObjs;
    public GameObject[] puddles;
    public GameObject[] shields;
    public GameObject[] headstones;

    public GameObject[] obstaclesStudy;

    [SerializeField]
    private int coinChance = 50;
    [SerializeField]
    private int puddleChance = 50;
    [SerializeField]
    private int shieldChance = 50;
    [SerializeField]
    private int headstoneChance = 20;
    float Speed = 0.001f;

    public void setChances(int coin, int puddle, int shield, int headstone)
    {
        coinChance = coin;
        puddleChance = puddle;
        shieldChance = shield;
        headstoneChance = headstone;
    }

    void Start()
    {
        bike = GameObject.FindGameObjectWithTag("Player").GetComponent<bike>();
        Mng = GameObject.Find("Manager").GetComponent<Manager>();
        moveVec = new Vector3(0,0,-1);
        MS = Mng.MoveSpeed;

        if (bike.valueResp == true) headstoneChance = 0;
        if (bike.shield_baff != 0) shieldChance = 0;

        if (!bike.studyScene)
        {
            foreach (Transform objTrans in GetComponentsInChildren<Transform>())
            {
                if (objTrans.gameObject.tag == "CoinStack") objTrans.gameObject.SetActive(Random.Range(0, 101) < coinChance);
                else if (objTrans.gameObject.tag == "shield") objTrans.gameObject.SetActive(Random.Range(0, 101) < shieldChance);
                else if (objTrans.gameObject.tag == "headstone") objTrans.gameObject.SetActive(Random.Range(0, 101) < headstoneChance);
                else if (objTrans.gameObject.tag == "puddle") objTrans.gameObject.SetActive(Random.Range(0, 101) < puddleChance);
            }
        }

        if (bike.skorostnoyBuff != 0)
        {
            foreach (Transform objTrans in GetComponentsInChildren<Transform>())
            {
                if (objTrans.gameObject.tag == "obstacle") 
                {
                    if (objTrans.GetComponent<BoxCollider>()) objTrans.GetComponent<BoxCollider>().isTrigger = true;
                    if (objTrans.GetComponent<Rigidbody>()) objTrans.GetComponent<Rigidbody>().isKinematic = true;
                }
            }        
        }


        // foreach (GameObject obj in coinsObjs)
        // {
        //     obj.SetActive(Random.Range(0, 101) < coinChance);
        // }
        // foreach (GameObject obj in puddles)
        // {
        //     obj.SetActive(Random.Range(0, 101) < puddleChance);
        // }
        // foreach (GameObject obj in shields)
        // {
        //     obj.SetActive(Random.Range(0, 101) < shieldChance);
        // }
        // foreach (GameObject obj in headstones)
        // {
        //     obj.SetActive(Random.Range(0, 101) < headstoneChance);
        // }
    }



    void Update()
    {
        // MS = Mng.MoveSpeed;
        MS = Mng.realMoveSpeed;
        
        // MS += Speed;
        // MS += 0.02f;
        transform.Translate(moveVec * Time.deltaTime * MS);
    }
}
