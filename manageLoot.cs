using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class manageLoot : MonoBehaviour
{
    public GameObject[] particles;
	public GameObject[] buyButton;
    public Button[] ButBuyes;
    public GameObject backButton;

	public AudioMixerSnapshot defSound;
	public AudioMixerSnapshot openBox;

	public GameObject Box;
	public AudioSource audioSource;

    public GameObject FreeMoneyMenu;

    GameObject box;
	GameObject lootBox;
	lootBox lootBoxScr;
	Animator an;
	Animator anSelf;
	manageLoot scrSelf;
	GameObject forLootBox;
	public int boxCount = 0;
    public float bonus;
	bool closed = true;

    public MainMenuController Mmc;

    void Start()
    {
    	audioSource = GetComponent<AudioSource>();
    	scrSelf = GetComponent<manageLoot>();
    	anSelf = GetComponent<Animator>();
    	forLootBox = transform.Find("forLootBox").gameObject;
        FreeMoneyMenu.SetActive(false);
    }

    public void spawnBox()
    {
        Destroy(box);
    	box = Instantiate(Box, forLootBox.transform);
    	lootBox = box.transform.Find("lootBox").gameObject;
    	lootBoxScr = lootBox.GetComponent<lootBox>();
    	lootBoxScr.mngLoot = scrSelf;
    	an = lootBox.GetComponent<Animator>();
    }

    public void changeBox()
    {
        StartCoroutine(changeBoxx());
    }

    IEnumerator changeBoxx()
    {
    	an.Play("slideLeftBox");
        yield return new WaitForSeconds(0.7f);
        
        spawnBox();
        an.Play("slideLeftBox2");
        yield return new WaitForSeconds(0.7f);
        an.Play("defaultBox");
    }

    public void OnPointerDown()
    {
    	if (closed) return;
    	lootBoxScr.OnPointerDown();
    }

    public void OnPointerUp()
    {
    	if (closed) return;
    	lootBoxScr.OnPointerUp();
    }

    public void boxFieldExpand()
    {
        if (Mmc.AS.BonusCheck < 1f)
        {
            Mmc.AS.BonusCheck += 0.25f;
            Mmc.SaveGovnoEbanoe();
        }
        else if (Mmc.AS.BonusCheck >= 1f && Mmc.AS.BonusCheck < 3f)
        {
            Mmc.AS.BonusCheck += 0.25f;
            Mmc.SaveGovnoEbanoe();
        }
        buyButton[0].SetActive(false);
        buyButton[1].SetActive(false);
        audioSource.Play();
    	openBox.TransitionTo(1.5f);

    	anSelf.Play("boxFieldExpand");
        Mmc.FreeTicketView.SetActive(false);
        StartCoroutine(boxFieldExpandCoroutine());
    }

    IEnumerator boxFieldExpandCoroutine()
    {
        yield return new WaitForSeconds(1f);
    	closed = false;
    }

    public void boxFieldDefault()
    {
        closed = true;
        Mmc.LootPrizeText.text = "";
        backButton.SetActive(false);
        buyButton[0].SetActive(true);
        buyButton[1].SetActive(true);
        audioSource.Stop();
    	audioSource.Stop();
    	defSound.TransitionTo(0.5f);
    	anSelf.Play("default");
        Mmc.FreeTicketView.SetActive(true);
        spawnBox();
    }

    public void boxOpened()
    {
    	boxCount += 1;
    	backButton.SetActive(true);
        CheckLootMoney();
    }

    public void CheckLootMoney()
    {
        if (Mmc.AS.ItsYourFirstTry == 1) return;
        ButBuyes[0].interactable = true;
        ButBuyes[1].interactable = true;
        if (PlayerPrefs.GetInt("Money") < 15) ButBuyes[0].interactable = false;
        if (PlayerPrefs.GetInt("Gem") < 1) ButBuyes[1].interactable = false;
    }
    public void LootCheck(int num, int count)
    {
        if (num == 1 && Mmc.AS.BonusCheck >= 1f && Mmc.AS.BonusCheck < 3f) Mmc.AS.BonusCheck += 0.6f;
        if (num == 2 && Mmc.AS.BonusCheck >= 1f && Mmc.AS.BonusCheck < 3f) Mmc.AS.BonusCheck += 0.3f;
        Mmc.LootBoxGetPrize(num, count);
    }
}
