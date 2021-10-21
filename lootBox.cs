using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lootBox : MonoBehaviour
{
    public GameObject[] particles;

	AudioSource audioSource;
	public manageLoot mngLoot;
	public GameObject[] awards;
    public GameObject[] awardSkins;
	Transform forLootBoxTrans;
	Transform LootBoxTrans;

    public GameObject[] PrizePrefab;
    public GameObject PrizePlace;
    GameObject prize;
    public Animator glowInside;
    public Animator glowOutside;
    public Animator an;
    public bool closed = true;
    public bool ended = false;
    bool startOpen = false;
    bool finalOpen = false;

    bool isMobilePlatform;
    float r = 0;
	float chanceTir1 = 5f;
	float chanceTir2 = 20f;
	float chanceTir3 = 55f;
	float chanceTir4 = 100f;

    public MainMenuController Mmc;

    void Start()
	{
		LootBoxTrans = transform.parent;
		forLootBoxTrans = LootBoxTrans.gameObject.transform.parent;
		an = GetComponent<Animator>();
		audioSource = LootBoxTrans.GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("Sound") == 0) audioSource.volume = 0f;
    }

    void Update()
    {
    	if (an == null || glowInside == null || glowOutside == null) return;

        if (startOpen && closed)
        {
            an.Play("startOpenBox");
        }
        if (finalOpen && !ended)
        {
        	closed = false;
        	ended = true;
        	audioSource.Play();
            an.Play("endOpenBox2");
            Debug.Log(mngLoot.boxCount);
            StartCoroutine(glowIn());
            // glowInside.Play("glowIn");
            glowOutside.Play("glowOut");

        }
        // Debug.Log(mngLoot.Mmc.AS.BonusCheck + "ol");
    }

	void getPrize()
	{
		r = Random.Range(1, 101) * mngLoot.Mmc.AS.BonusCheck;
        Debug.Log(r);
        
        if (r < chanceTir1) getPrizeTir1();
		else if (r < chanceTir2) getPrizeTir2();
		else if (r < chanceTir3) getPrizeTir3();
		else getPrizeTir4();
    }

    public void OnPointerDown()
    {
    	startOpen = true;
    }

    public void OnPointerUp()
    {
    	finalOpen = true;
    }

    void getPrizeTir1()
    {
        SearchPrize1();
    }

    void SearchPrize1()
    {
        if (mngLoot.Mmc.AS.HowManOpen < 11)
        { 
            int p = Random.Range(0, 11);
            if (p == 0) 
            {
                prize = Instantiate(awards[1], forLootBoxTrans);
                prize.transform.parent = LootBoxTrans;
                mngLoot.LootCheck(1, 0);
            } 
            else if (mngLoot.Mmc.AS.SkinBuy[p] == 0 && p != 1 && p != 3 && p != 4 && p != 6)
            {
                prize = Instantiate(awardSkins[p], forLootBoxTrans);
                prize.GetComponent<Animator>().Play("chelVipal");
                prize.transform.parent = LootBoxTrans;
                mngLoot.LootCheck(1, p);
            }
            else if (mngLoot.Mmc.AS.SkinBuy[p] == 1 || p == 1 || p == 3 || p == 4 || p == 6)
            {
                SearchPrize1();
            }
        }
        else if (mngLoot.Mmc.AS.HowManOpen >= 11)
        {
            prize = Instantiate(awards[1], forLootBoxTrans);
            prize.transform.parent = LootBoxTrans;
            mngLoot.LootCheck(1, 0);
        }
    }

    void getPrizeTir2()
    {
        int p = Random.Range(0, 4);
        prize = Instantiate(awards[p+2], forLootBoxTrans);
    	prize.transform.parent = LootBoxTrans;
        mngLoot.LootCheck(2, p);
    }
    void getPrizeTir3()
    {
        int p = Random.Range(0, 2);
        prize = Instantiate(awards[p], forLootBoxTrans);
    	prize.transform.parent = LootBoxTrans;
        mngLoot.LootCheck(3, p);
    }
    void getPrizeTir4()
    {
    	prize = Instantiate(awards[0], forLootBoxTrans);
    	prize.transform.parent = LootBoxTrans;
        mngLoot.LootCheck(4, 0);
    }

    IEnumerator glowIn()
    {
        if (an != null && glowInside != null & glowOutside != null)
        {
            yield return new WaitForSeconds(1.7f);
            glowInside.Play("glowIn");
            yield return new WaitForSeconds(1.2f);
            // for (int i = 0; i < 3; i ++) Instantiate(particles[i], forLootBoxTrans);
            gameObject.SetActive(false);
            Destroy(glowOutside.gameObject);
            mngLoot.boxOpened();
            getPrize(); 
        }
    }
}
