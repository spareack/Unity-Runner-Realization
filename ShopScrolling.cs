using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopScrolling : MonoBehaviour
{
    public ParticleSystem expSkinSwap;

    public GameObject[] gamePrefabs1;
    public GameObject[] gamePrefabs2;
    public GameObject[] gamePrefabs3;

    public Text[] SkillDiscriptionText;
    public GameObject[] prefabs2;
    public GameObject[] prefabs3;

    public GameObject[][] TripleMasive;
    public GameObject[][] TripleGameMasive;

    public Material classic;
    public Material black;
    public GameObject Empty;
    public GameObject spawnPlace;
    public GameObject spawnPlaceMain;

    public GameObject[] prefabs;
    public List<GameObject> skinsMenu = new List<GameObject>();

    [Range(1, 50)]
    [Header("Controllers")]
    public int panCount;
    [Range(0, 1000)]
    public int panOffset;
    [Range(0f, 20f)]
    public float snapSpeed;
    [Header("Other Objects")]
    public GameObject panPrefab;
    public GameObject bike;

    private GameObject[] instPans;
    private Vector2[] pansPos;

    private RectTransform contentRect;
    private Vector2 contentVector;

    private int selectedPanID;
    private bool isScrolling;
    public ScrollRect scrollRect;
    public GameObject skin;
    public GameObject[] skins;
    public GameObject BuyPanel;
    public Text Descr;
    public Sprite[] SkinIcon;
    static public int EBALYAETUIGRU = 0;

    public GameObject[] SkinPoint;
    public GameObject[] arrow;
    public GameObject[] StyleBuyImage;
    public GameObject[] SuperPower;
    public GameObject HaveSkill;
    public GameObject IsBought;
    public Text SoonText;
    public Text PriceShop;
    public Text NeedAchiev;
    public Text ActivateText;
    public Text DiscriptionText;
    public Button BuyBut;
    public GameObject BuyButObject;
    public Text BuyText;
    public int SkinPointCheck;
    public int BuyMoney;
    public int BuyGems;
    private int Shonum;
    public Sprite[] ColorPoint;

    public AchievementSave AS = new AchievementSave();
    public MainMenuController Mmc;
    public GameObject ShopMenu;
    GameObject empty;
    GameObject chel;
    GameObject chelMainMenuDop;
    GameObject chelMainMenu;
    public GameObject chelShopMenu;
    public GameObject Prefab;
    public Color CloseColor = new Color(255f, 255f, 255f, 255f);
    public Color BuyColor = new Color(255f, 255f, 255f, 255f);
    public Color UseColor = new Color(191f, 22f, 37f, 255f);
    public Color ColorChoose = new Color(255f, 224f, 33f, 255f);

    public GameObject ContentPanel;
    public Color NotChoosePanel = new Color(255f, 255f, 255f, 255f);
    public Color ChoosePanel = new Color(255f, 255f, 0f, 255f);

    int layerChelShop;
    private int[] HowStyle = new int[40] { 3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] PassiveOrActiv = new int[40] { 2, 1, 1, 1, 1, 2, 2, 1, 2, 2, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[,] StyleBuy = new int[40, 3] { { 0, 0, 1 },  { 2, 0, 1 }, { 0, 1, 0 }, { 2, 2, 0 }, { 2, 0, 0 }, { 1, 1, 0 }, { 2, 1, 1 }, { 1, 0, 1 }, { 1, 1, 1 }, { 1, 0, 0 },
        { 0, 0, 0 },  { 1, 0, 1 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 },
        { 0, 0, 0 },  { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 },
        { 0, 0, 0 },  { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
    public int[,] StyleBuyPrice = new int[40, 3] { { 0, 50, 7 },  { 0, 100, 25 }, { 80, 25, 90 }, { 0, 0, 150 }, { 0, 90, 120 }, { 40, 40, 200 }, { 0, 30, 35 }, { 50, 175, 60 }, { 55, 55, 55 }, { 60, 320, 340 },
        { 200, 300, 400 },  { 40, 270, 40 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 },
        { 0, 0, 0 },  { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 },
        { 0, 0, 0 },  { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
    // 0 - деньги | 1 - билеты | 2 - ачивка | 3 - лутбокс

    private void Start()
    {
        AS = JsonUtility.FromJson<AchievementSave>(PlayerPrefs.GetString("AchievementSave"));
        layerChelShop = LayerMask.NameToLayer("shopChel");
        EBALYAETUIGRU = AS.WhatIsSkin;
        contentRect = GetComponent<RectTransform>();
        instPans = new GameObject[panCount];
        pansPos = new Vector2[panCount];
        //skin = Instantiate(skins[0]);

        TripleMasive = new GameObject[prefabs.Length][];
        for (int i = 0; i < prefabs.Length; i++)
        {
            TripleMasive[i] = new GameObject[3] { prefabs[i], prefabs2[i], prefabs3[i] };  
        }
        TripleGameMasive = new GameObject[prefabs.Length][];
        for (int i = 0; i < prefabs.Length; i++)
        {
            TripleGameMasive[i] = new GameObject[3] { gamePrefabs1[i], gamePrefabs2[i], gamePrefabs3[i] };  
        }

        for (int i = 0; i < panCount; i++)
        {
            instPans[i] = Instantiate(panPrefab, transform, false);
            Image[] SkinSwap = instPans[i].GetComponentsInChildren<Image>();
            for (int j = 0; j < 3; j++)
            {
                int x = i * 3 + j;
                SkinSwap[j * 2 + 2].GetComponent<Image>().sprite = SkinIcon[i * 3];
                empty = Instantiate(Empty, SkinSwap[j * 2 + 2].transform);
                empty.GetComponent<RectTransform>().anchoredPosition3D += new Vector3( 0f, 40f, -5f);
                chel = Instantiate(prefabs[x], empty.transform);
                skinsMenu.Add(chel);
                if (AS.SkinBuy[x] == 0)
                {
                    changeMaterialObj(chel, black);
                }
                SkinSwap[j * 2 + 1].GetComponent<Button>().onClick.AddListener(() => ButtonContorol(x));
            }

            if (i == 0) continue;
            instPans[i].transform.localPosition = new Vector2(instPans[i].transform.localPosition.x, instPans[i - 1].transform.localPosition.y - panPrefab.GetComponent<RectTransform>().sizeDelta.y - panOffset);
            pansPos[i] = -instPans[i].transform.localPosition;

            SkinPointCheck = AS.SkinStyleNumber;
            changeSkinShopForStart(AS.WhatIsSkinShoose, AS.SkinStyleNumber);
            SkinPoint[AS.SkinStyleNumber].GetComponent<Image>().sprite = ColorPoint[1];

            if (SkinPointCheck == 0)
            {
                arrow[0].SetActive(false);
            }
            if (SkinPointCheck == 2)
            {
                arrow[1].SetActive(false);
            }
            if (AS.WhatIsSkinShoose == 0)
            {
                HaveSkill.SetActive(false);
            }
        }
        AS.WhatIsSkin = AS.WhatIsSkinShoose;
        if (AS.SkinBuy[AS.WhatIsSkin] == 0 || HowStyle[AS.WhatIsSkin] == 0)
        {
            arrow[0].SetActive(false);
            arrow[1].SetActive(false);
            SkinPoint[0].SetActive(false);
            SkinPoint[1].SetActive(false);
            SkinPoint[2].SetActive(false);
        }
        else if (AS.SkinBuy[AS.WhatIsSkin] == 1 && HowStyle[AS.WhatIsSkin] == 1)
        {
            arrow[0].SetActive(false);
            arrow[1].SetActive(false);
            SkinPoint[0].SetActive(false);
            SkinPoint[1].GetComponent<Image>().sprite = ColorPoint[1];
            SkinPoint[1].SetActive(true);
            SkinPoint[2].SetActive(false);
        }
        changeSkinMenu(AS.WhatIsSkinShoose, AS.SkinStyleNumber);

        if (AS.WhatIsSkinShoose < 3) Shonum = AS.WhatIsSkinShoose * 2 + 1;
        if (AS.WhatIsSkinShoose >= 3 && AS.WhatIsSkinShoose < 6) Shonum = AS.WhatIsSkinShoose * 2 + 2;
        if (AS.WhatIsSkinShoose >= 6 && AS.WhatIsSkinShoose < 9) Shonum = AS.WhatIsSkinShoose * 2 + 3;
        if (AS.WhatIsSkinShoose >= 9 && AS.WhatIsSkinShoose < 12) Shonum = AS.WhatIsSkinShoose * 2 + 4;
        if (AS.WhatIsSkinShoose >= 12 && AS.WhatIsSkinShoose < 15) Shonum = AS.WhatIsSkinShoose * 2 + 5;
        Image[] SwapPanel = ContentPanel.GetComponentsInChildren<Image>();
        SwapPanel[Shonum].color = ChoosePanel;
        ShopMenu.SetActive(false);

        switch(Random.Range(0, 4))
        {
            case 0:
                spawnPlace.transform.position = new Vector3(2.556f, 0.459f, 7.131f);
                break;
            case 1:
                spawnPlace.transform.position = new Vector3(2.04f, 0.459f, 21.35f);
                break;
            case 2:
                spawnPlace.transform.position = new Vector3(-1.89f, 5.39f, 12.78f);
                break;
            case 3:
                spawnPlace.transform.position = new Vector3(3.37f, 5.39f, 6.42f);
                break;
        }
    }

    private void FixedUpdate()
    {
        if (contentRect.anchoredPosition.y <= pansPos[0].y && !isScrolling || contentRect.anchoredPosition.y >= pansPos[pansPos.Length - 1].y && !isScrolling)
        {
            scrollRect.inertia = false;
        }
        float nearestPos = float.MaxValue;
        for (int i = 1; i < panCount; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.y - pansPos[i].y);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanID = i;
            }
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.y);
        if (scrollVelocity < 1000 && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > 1000) return;
        contentVector.y = Mathf.SmoothStep(contentRect.anchoredPosition.y, pansPos[selectedPanID].y, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }

    public void ButtonContorol(int num)
    {
        Mmc.audioSource.PlayOneShot(Mmc.tapSound, PlayerPrefs.GetInt("Sound"));
        if (num != Mmc.AS.WhatIsSkinShoose) SkinPointCheck = 0;
        else SkinPointCheck = Mmc.AS.SkinStyleNumber;

        SkinPoint[0].GetComponent<Image>().sprite = ColorPoint[0];
        SkinPoint[1].GetComponent<Image>().sprite = ColorPoint[0];
        SkinPoint[2].GetComponent<Image>().sprite = ColorPoint[0];
        SkinPoint[SkinPointCheck].GetComponent<Image>().sprite = ColorPoint[1];


        if (Mmc.AS.SkinBuy[num] == 0 || HowStyle[num] == 0)
        {
            arrow[0].SetActive(false);
            arrow[1].SetActive(false);
            SkinPoint[0].SetActive(false);
            SkinPoint[1].SetActive(false);
            SkinPoint[2].SetActive(false); 
        }
        else if (Mmc.AS.SkinBuy[num] == 1 && HowStyle[num] == 1)
        {
            arrow[0].SetActive(false);
            arrow[1].SetActive(false);
            SkinPoint[0].SetActive(false);
            SkinPoint[1].GetComponent<Image>().sprite = ColorPoint[1];
            SkinPoint[1].SetActive(true);
            SkinPoint[2].SetActive(false);
        }
        else
        {
            if (SkinPointCheck == 0)
            {
                arrow[0].SetActive(false);
                arrow[1].SetActive(true);
            }
            if (SkinPointCheck == 1)
            {
                arrow[0].SetActive(true);
                arrow[1].SetActive(true);
            }
            if (SkinPointCheck == 2)
            {
                arrow[0].SetActive(true);
                arrow[1].SetActive(false);
            }
            SkinPoint[0].SetActive(true);
            SkinPoint[1].SetActive(true);
            SkinPoint[2].SetActive(true);
        }

        if (num == 0) HaveSkill.SetActive(false);
        else HaveSkill.SetActive(true);

        Mmc.AS.WhatIsSkin = num;
        EBALYAETUIGRU = num;
        changeSkinShop(Mmc.AS.WhatIsSkin, SkinPointCheck);
        Mmc.SaveGovnoEbanoe();
        SwapTextAndImage();
    }

    public void RightSkin()
    {
        Mmc.audioSource.PlayOneShot(Mmc.tapSound, PlayerPrefs.GetInt("Sound"));
        SkinPointCheck += 1;
        if (SkinPointCheck == 2)
        {
            arrow[1].SetActive(false);
        }
        else
        {
            arrow[0].SetActive(true);
            arrow[1].SetActive(true);
        }
        SkinPoint[0].GetComponent<Image>().sprite = ColorPoint[0];
        SkinPoint[1].GetComponent<Image>().sprite = ColorPoint[0];
        SkinPoint[2].GetComponent<Image>().sprite = ColorPoint[0];
        SkinPoint[SkinPointCheck].GetComponent<Image>().sprite = ColorPoint[1];
        SwapTextAndImage();
        changeSkinShop(Mmc.AS.WhatIsSkin, SkinPointCheck);
    }
    public void LeftSkin()
    {
        Mmc.audioSource.PlayOneShot(Mmc.tapSound, PlayerPrefs.GetInt("Sound"));
        SkinPointCheck -= 1;
        if (SkinPointCheck == 0)
        {
            arrow[0].SetActive(false);
        }
        else
        {
            arrow[0].SetActive(true);
            arrow[1].SetActive(true);
        }
        SkinPoint[0].GetComponent<Image>().sprite = ColorPoint[0];
        SkinPoint[1].GetComponent<Image>().sprite = ColorPoint[0];
        SkinPoint[2].GetComponent<Image>().sprite = ColorPoint[0];
        SkinPoint[SkinPointCheck].GetComponent<Image>().sprite = ColorPoint[1];
        SwapTextAndImage();
        changeSkinShop(Mmc.AS.WhatIsSkin, SkinPointCheck);
    }
    public void changeLayer(GameObject go, int Layer)
    {
        foreach (Transform transf1 in go.transform)
            foreach (Transform transf2 in transf1.transform)
                foreach (Transform transf3 in transf2.transform)
                {
                    transf3.gameObject.layer = Layer;
                    foreach (Transform transf4 in transf3.transform) transf4.gameObject.layer = Layer;
                }
    }

    public void changeSkinShop(int count, int style)
    {
        Destroy(chelShopMenu);
        if (HowStyle[count] != 0)
        {
            chelShopMenu = Instantiate(TripleMasive[count][style], bike.transform);
            changeLayer(chelShopMenu, layerChelShop);
            chelShopMenu.transform.Rotate(new Vector3(0, 180, 0));
            chelShopMenu.transform.localScale *= 0.05f;
            if (Mmc.AS.SkinBuy[count] == 0)
            {
                chelShopMenu.GetComponent<prefabsShop>().changeMaterial(black);
            }
        }
    }
    public void changeSkinMenu(int count, int style)
    {
        Destroy(chelMainMenuDop);
        chelMainMenuDop = Instantiate(TripleGameMasive[count][style], spawnPlace.transform);
        changeLayer(chelMainMenuDop, 0);
        chelMainMenuDop.GetComponent<Animator>().Play("menuGameAnim");

        Destroy(chelMainMenu);
        chelMainMenu = Instantiate(TripleGameMasive[count][style], spawnPlaceMain.transform);
        changeLayer(chelMainMenu, 0);
        chelMainMenu.GetComponent<Animator>().Play("menuGameAnim");

        // chelMainMenu.transform.parent = spawnPlace.transform;
        // chelMainMenu.transform.position = new Vector3(-0.03f, 0.5f, -4.42f);
        // chelMainMenu.transform.localScale = new Vector3(0.085f, 0.085f, 0.085f);
    }
    public void changeSkinShopForStart(int count, int style)
    {
        Destroy(chelShopMenu);
        chelShopMenu = Instantiate(TripleMasive[count][style], bike.transform);
        changeLayer(chelShopMenu, layerChelShop);
        chelShopMenu.transform.Rotate(new Vector3(0, 180, 0));
        chelShopMenu.transform.localScale *= 0.05f;
        if (AS.SkinBuy[count] == 0)
        {
            chelShopMenu.GetComponent<prefabsShop>().changeMaterial(black);
        }
    }

    public void changeMaterialObj(GameObject obj, Material mat)
    {
        foreach(Transform transf1 in obj.transform)
            foreach(Transform transf2 in transf1.transform)
                foreach(Transform transf3 in transf2.transform)
                {
                    if (transf3.gameObject.GetComponent<MeshRenderer>()) transf3.gameObject.GetComponent<MeshRenderer>().material = mat;
                    foreach(Transform transf4 in transf3.transform) transf4.gameObject.GetComponent<MeshRenderer>().material = mat;
                }
    }
    public void SwapTextAndImage()
    {
        PriceShop.text = "" + StyleBuyPrice[Mmc.AS.WhatIsSkin, SkinPointCheck];
        if (StyleBuy[Mmc.AS.WhatIsSkin, SkinPointCheck] == 0)
        {
            StyleBuyImage[0].SetActive(true);
            StyleBuyImage[1].SetActive(false);
        }
        if (StyleBuy[Mmc.AS.WhatIsSkin, SkinPointCheck] == 1)
        {
            StyleBuyImage[0].SetActive(false);
            StyleBuyImage[1].SetActive(true);
        }
        if (StyleBuy[Mmc.AS.WhatIsSkin, SkinPointCheck] == 2)
        {
            StyleBuyImage[0].SetActive(false);
            StyleBuyImage[1].SetActive(false);
        }
        if (Mmc.AS.SkinBuy[Mmc.AS.WhatIsSkin] == 0)
        {
            expSkinSwap.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            SwapTextAndImageFucrtion();
            if (HowStyle[Mmc.AS.WhatIsSkin] == 0)
            {
                BuyButObject.SetActive(false);
                IsBought.SetActive(false);
                HaveSkill.SetActive(false);
                SoonText.text = LanguageSistem.lng.SoonText;
            }
            else
            {
                SoonText.text = "";
                BuyButObject.SetActive(true);
                IsBought.SetActive(true);
                HaveSkill.SetActive(true);
            }
            if (StyleBuy[Mmc.AS.WhatIsSkin, SkinPointCheck] == 2)
            {
                BuyButObject.SetActive(false);
                if (Mmc.AS.WhatIsSkin == 1 && SkinPointCheck == 0) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[4];
                if (Mmc.AS.WhatIsSkin == 3 && SkinPointCheck == 0) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[5];
                if (Mmc.AS.WhatIsSkin == 3 && SkinPointCheck == 1) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[8];
                if (Mmc.AS.WhatIsSkin == 4 && SkinPointCheck == 0) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[6];
                if (Mmc.AS.WhatIsSkin == 6 && SkinPointCheck == 0) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[7];
                PriceShop.text = "";
            }
            else NeedAchiev.text = "";
        }
        if (Mmc.AS.SkinBuy[Mmc.AS.WhatIsSkin] == 1)
        {
            SoonText.text = "";
            NeedAchiev.text = "";
            if (SkinPointCheck == 1 && Mmc.AS.SkinBuyStyle1[Mmc.AS.WhatIsSkin] == 0)
            {
                SwapTextAndImageFucrtion();
                if (StyleBuy[Mmc.AS.WhatIsSkin, SkinPointCheck] == 2)
                {
                    BuyButObject.SetActive(false);
                    if (Mmc.AS.WhatIsSkin == 1 && SkinPointCheck == 0) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[4];
                    if (Mmc.AS.WhatIsSkin == 3 && SkinPointCheck == 0) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[5];
                    if (Mmc.AS.WhatIsSkin == 3 && SkinPointCheck == 1) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[8];
                    if (Mmc.AS.WhatIsSkin == 4 && SkinPointCheck == 0) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[6];
                    if (Mmc.AS.WhatIsSkin == 6 && SkinPointCheck == 0) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[7];
                    PriceShop.text = "";
                }
                else
                {
                    NeedAchiev.text = "";
                    BuyButObject.SetActive(true);
                }
            }
            else if (SkinPointCheck == 2 && Mmc.AS.SkinBuyStyle2[Mmc.AS.WhatIsSkin] == 0)
            {
                SwapTextAndImageFucrtion();
                if (StyleBuy[Mmc.AS.WhatIsSkin, SkinPointCheck] == 2)
                {
                    BuyButObject.SetActive(false);
                    if (Mmc.AS.WhatIsSkin == 1 && SkinPointCheck == 0) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[4];
                    if (Mmc.AS.WhatIsSkin == 3 && SkinPointCheck == 0) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[5];
                    if (Mmc.AS.WhatIsSkin == 3 && SkinPointCheck == 1) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[8];
                    if (Mmc.AS.WhatIsSkin == 4 && SkinPointCheck == 0) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[6];
                    if (Mmc.AS.WhatIsSkin == 6 && SkinPointCheck == 0) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[7];
                    PriceShop.text = "";
                }
                else 
                {
                    NeedAchiev.text = "";
                    BuyButObject.SetActive(true);
                }
            }
            else
            {
                IsBought.SetActive(false);
                BuyBut.interactable = true;
                BuyButObject.SetActive(true);
                BuyText.text = LanguageSistem.lng.ShopBuyText[2];
                BuyBut.image.color = BuyColor;
                expSkinSwap.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
            if (SkinPointCheck == Mmc.AS.SkinStyleNumber && Mmc.AS.WhatIsSkin == Mmc.AS.WhatIsSkinShoose)
            {
                expSkinSwap.Play();
                BuyBut.interactable = false;
                BuyButObject.SetActive(true);
                BuyText.text = LanguageSistem.lng.ShopBuyText[3];
                BuyBut.image.color = UseColor;
            }
            else
            {
                expSkinSwap.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }
    }
    public void SwapTextAndImageFucrtion()
    {
        if (StyleBuy[Mmc.AS.WhatIsSkin, SkinPointCheck] == 0 && PlayerPrefs.GetInt("Money") < StyleBuyPrice[Mmc.AS.WhatIsSkin, SkinPointCheck])
        {
            BuyBut.interactable = false;
            BuyText.text = LanguageSistem.lng.ShopBuyText[0];
            BuyBut.image.color = CloseColor;
        }
        else if (StyleBuy[Mmc.AS.WhatIsSkin, SkinPointCheck] == 1 && PlayerPrefs.GetInt("Gem") < StyleBuyPrice[Mmc.AS.WhatIsSkin, SkinPointCheck])
        {
            BuyBut.interactable = false;
            BuyText.text = LanguageSistem.lng.ShopBuyText[0];
            BuyBut.image.color = CloseColor;
        }
        else if (StyleBuy[Mmc.AS.WhatIsSkin, SkinPointCheck] == 2)
        {
            BuyBut.interactable = false;
            BuyText.text = LanguageSistem.lng.ShopBuyText[0];
            BuyBut.image.color = CloseColor;
        }
        else
        {
            BuyBut.interactable = true;
            BuyText.text = LanguageSistem.lng.ShopBuyText[1];
            BuyBut.image.color = BuyColor;
        }
        IsBought.SetActive(true);
    }
    public void SwapTextAndImageFucrtionFoStart()
    {
        if (StyleBuy[Mmc.AS.WhatIsSkinShoose, SkinPointCheck] == 0 && PlayerPrefs.GetInt("Money") < StyleBuyPrice[Mmc.AS.WhatIsSkinShoose, SkinPointCheck])
        {
            BuyBut.interactable = false;
            BuyText.text = LanguageSistem.lng.ShopBuyText[0];
            BuyBut.image.color = CloseColor;
        }
        else if (StyleBuy[Mmc.AS.WhatIsSkinShoose, SkinPointCheck] == 1 && PlayerPrefs.GetInt("Gem") < StyleBuyPrice[Mmc.AS.WhatIsSkinShoose, SkinPointCheck])
        {
            BuyBut.interactable = false;
            BuyText.text = LanguageSistem.lng.ShopBuyText[0];
            BuyBut.image.color = CloseColor;
        }
        else if (StyleBuy[Mmc.AS.WhatIsSkinShoose, SkinPointCheck] == 2)
        {
            BuyBut.interactable = false;
            BuyText.text = LanguageSistem.lng.ShopBuyText[0];
            BuyBut.image.color = CloseColor;
        }
        else
        {
            BuyBut.interactable = true;
            BuyText.text = LanguageSistem.lng.ShopBuyText[1];
            BuyBut.image.color = BuyColor;
        }
        IsBought.SetActive(true);
    }

    public void SwapTextAndImageFoStart()
    {
        PriceShop.text = "" + StyleBuyPrice[Mmc.AS.WhatIsSkinShoose, SkinPointCheck];
        if (StyleBuy[Mmc.AS.WhatIsSkinShoose, SkinPointCheck] == 0)
        {
            StyleBuyImage[0].SetActive(true);
            StyleBuyImage[1].SetActive(false);
        }
        if (StyleBuy[Mmc.AS.WhatIsSkinShoose, SkinPointCheck] == 1)
        {
            StyleBuyImage[0].SetActive(false);
            StyleBuyImage[1].SetActive(true);
        }
        if (StyleBuy[Mmc.AS.WhatIsSkinShoose, SkinPointCheck] == 2)
        {
            StyleBuyImage[0].SetActive(false);
            StyleBuyImage[1].SetActive(false);
        }
        if (Mmc.AS.SkinBuy[Mmc.AS.WhatIsSkinShoose] == 0)
        {
            SwapTextAndImageFucrtionFoStart();
        }
        if (Mmc.AS.SkinBuy[Mmc.AS.WhatIsSkinShoose] == 1)
        {

            if (SkinPointCheck == 1 && Mmc.AS.SkinBuyStyle1[Mmc.AS.WhatIsSkin] == 0)
            {
                SwapTextAndImageFucrtion();
                if (StyleBuy[Mmc.AS.WhatIsSkin, SkinPointCheck] == 2)
                {
                    BuyButObject.SetActive(false);
                    if (Mmc.AS.WhatIsSkin == 1 && SkinPointCheck == 0) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[4];
                    if (Mmc.AS.WhatIsSkin == 3 && SkinPointCheck == 0) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[5];
                    if (Mmc.AS.WhatIsSkin == 3 && SkinPointCheck == 1) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[8];
                    if (Mmc.AS.WhatIsSkin == 4 && SkinPointCheck == 0) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[6];
                    if (Mmc.AS.WhatIsSkin == 6 && SkinPointCheck == 0) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[7];
                    PriceShop.text = "";
                }
                else
                {
                    NeedAchiev.text = "";
                    BuyButObject.SetActive(true);
                }
            }
            else if (SkinPointCheck == 2 && Mmc.AS.SkinBuyStyle2[Mmc.AS.WhatIsSkin] == 0)
            {
                SwapTextAndImageFucrtion();
                if (StyleBuy[Mmc.AS.WhatIsSkin, SkinPointCheck] == 2)
                {
                    BuyButObject.SetActive(false);
                    if (Mmc.AS.WhatIsSkin == 1 && SkinPointCheck == 0) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[4];
                    if (Mmc.AS.WhatIsSkin == 3 && SkinPointCheck == 0) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[5];
                    if (Mmc.AS.WhatIsSkin == 3 && SkinPointCheck == 1) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[8];
                    if (Mmc.AS.WhatIsSkin == 4 && SkinPointCheck == 0) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[6];
                    if (Mmc.AS.WhatIsSkin == 6 && SkinPointCheck == 0) NeedAchiev.text = LanguageSistem.lng.ShopBuyText[7];
                    PriceShop.text = "";
                }
                else
                {
                    NeedAchiev.text = "";
                    BuyButObject.SetActive(true);
                }

            }
            else
            {
                IsBought.SetActive(false);
                BuyBut.interactable = true;
                BuyText.text = LanguageSistem.lng.ShopBuyText[2];
                BuyBut.image.color = BuyColor;
            }
            if (SkinPointCheck == Mmc.AS.SkinStyleNumber)
            {
                BuyBut.interactable = false;
                BuyText.text = LanguageSistem.lng.ShopBuyText[3];
                BuyBut.image.color = UseColor;
            }
        }
    }

    public void buyButton()
    {
        Mmc.audioSource.PlayOneShot(Mmc.tapSound, PlayerPrefs.GetInt("Sound"));
        // if (Mmc.buttonsTest != null) Mmc.buttonsTest.checkCurrentButtonsState();
        if (Mmc.AS.SkinBuy[Mmc.AS.WhatIsSkin] == 0)
        {
            if (StyleBuy[Mmc.AS.WhatIsSkin, SkinPointCheck] == 0 && PlayerPrefs.GetInt("Money") >= StyleBuyPrice[Mmc.AS.WhatIsSkin, SkinPointCheck])
            {
                BuyMoney = PlayerPrefs.GetInt("Money") - StyleBuyPrice[Mmc.AS.WhatIsSkin, SkinPointCheck];
                PlayerPrefs.SetInt("Money", BuyMoney);
                Mmc.moneyText.text = "" + PlayerPrefs.GetInt("Money");
                changeMaterialObj(skinsMenu[Mmc.AS.WhatIsSkin], classic);
                skinsMenu[Mmc.AS.WhatIsSkin].GetComponent<Animator>().Play("menuChel");
                Mmc.AS.SkinBuy[Mmc.AS.WhatIsSkin] = 1;
                changeMaterialObj(chelShopMenu, classic);
                chelShopMenu.GetComponent<Animator>().Play("menuChel");
                BuyMoney = PlayerPrefs.GetInt("Money");
            }
            else if (StyleBuy[Mmc.AS.WhatIsSkin, SkinPointCheck] == 1 && PlayerPrefs.GetInt("Gem") >= StyleBuyPrice[Mmc.AS.WhatIsSkin, SkinPointCheck])
            {
                BuyGems = PlayerPrefs.GetInt("Gem") - StyleBuyPrice[Mmc.AS.WhatIsSkin, SkinPointCheck];
                PlayerPrefs.SetInt("Gem", BuyGems);
                Mmc.gemText.text = "" + PlayerPrefs.GetInt("Gem");
                changeMaterialObj(skinsMenu[Mmc.AS.WhatIsSkin], classic);
                skinsMenu[Mmc.AS.WhatIsSkin].GetComponent<Animator>().Play("menuChel");
                Mmc.AS.SkinBuy[Mmc.AS.WhatIsSkin] = 1;
                changeMaterialObj(chelShopMenu, classic);
                chelShopMenu.GetComponent<Animator>().Play("menuChel");
                BuyGems = PlayerPrefs.GetInt("Gem");
            }
        }
        else if (Mmc.AS.SkinBuy[Mmc.AS.WhatIsSkin] == 1)
        {
            if (SkinPointCheck == 1 && Mmc.AS.SkinBuyStyle1[Mmc.AS.WhatIsSkin] == 0)
            {
                if (StyleBuy[Mmc.AS.WhatIsSkin, SkinPointCheck] == 0 && PlayerPrefs.GetInt("Money") >= StyleBuyPrice[Mmc.AS.WhatIsSkin, SkinPointCheck])
                {
                    BuyMoney = PlayerPrefs.GetInt("Money") - StyleBuyPrice[Mmc.AS.WhatIsSkin, SkinPointCheck];
                    PlayerPrefs.SetInt("Money", BuyMoney);
                    Mmc.moneyText.text = "" + PlayerPrefs.GetInt("Money");
                    skinsMenu[Mmc.AS.WhatIsSkin].GetComponent<Animator>().Play("menuChel");
                    Mmc.AS.SkinBuyStyle1[Mmc.AS.WhatIsSkin] = 1;
                    Mmc.AS.SkinStyleNumber = 1;
                    BuyMoney = PlayerPrefs.GetInt("Money");
                }
                else if (StyleBuy[Mmc.AS.WhatIsSkin, SkinPointCheck] == 1 && PlayerPrefs.GetInt("Gem") >= StyleBuyPrice[Mmc.AS.WhatIsSkin, SkinPointCheck])
                {
                    BuyGems = PlayerPrefs.GetInt("Gem") - StyleBuyPrice[Mmc.AS.WhatIsSkin, SkinPointCheck];
                    PlayerPrefs.SetInt("Gem", BuyGems);
                    Mmc.gemText.text = "" + PlayerPrefs.GetInt("Gem");
                    skinsMenu[Mmc.AS.WhatIsSkin].GetComponent<Animator>().Play("menuChel");
                    Mmc.AS.SkinBuyStyle1[Mmc.AS.WhatIsSkin] = 1;
                    Mmc.AS.SkinStyleNumber = 1;
                    BuyGems = PlayerPrefs.GetInt("Gem");
                }
            }
            else if (SkinPointCheck == 2 && Mmc.AS.SkinBuyStyle2[Mmc.AS.WhatIsSkin] == 0)
            {
                if (StyleBuy[Mmc.AS.WhatIsSkin, SkinPointCheck] == 0 && PlayerPrefs.GetInt("Money") >= StyleBuyPrice[Mmc.AS.WhatIsSkin, SkinPointCheck])
                {
                    BuyMoney = PlayerPrefs.GetInt("Money") - StyleBuyPrice[Mmc.AS.WhatIsSkin, SkinPointCheck];
                    PlayerPrefs.SetInt("Money", BuyMoney);
                    Mmc.moneyText.text = "" + PlayerPrefs.GetInt("Money");
                    skinsMenu[Mmc.AS.WhatIsSkin].GetComponent<Animator>().Play("menuChel");
                    Mmc.AS.SkinBuyStyle2[Mmc.AS.WhatIsSkin] = 1;
                    Mmc.AS.SkinStyleNumber = 2;
                    BuyMoney = PlayerPrefs.GetInt("Money");
                }
                else if (StyleBuy[Mmc.AS.WhatIsSkin, SkinPointCheck] == 1 && PlayerPrefs.GetInt("Gem") >= StyleBuyPrice[Mmc.AS.WhatIsSkin, SkinPointCheck])
                {
                    BuyGems = PlayerPrefs.GetInt("Gem") - StyleBuyPrice[Mmc.AS.WhatIsSkin, SkinPointCheck];
                    PlayerPrefs.SetInt("Gem", BuyGems);
                    Mmc.gemText.text = "" + PlayerPrefs.GetInt("Gem");
                    skinsMenu[Mmc.AS.WhatIsSkin].GetComponent<Animator>().Play("menuChel");
                    Mmc.AS.SkinBuyStyle2[Mmc.AS.WhatIsSkin] = 1;
                    Mmc.AS.SkinStyleNumber = 2;
                    BuyGems = PlayerPrefs.GetInt("Gem");
                }
            }
            else
            {
                Image[] SwapPanel = ContentPanel.GetComponentsInChildren<Image>();
                WhatIsShonem();
                SwapPanel[Shonum].color = NotChoosePanel;
                Mmc.AS.WhatIsSkinShoose = Mmc.AS.WhatIsSkin;
                WhatIsShonem();
                SwapPanel[Shonum].color = ChoosePanel;

                if (SkinPointCheck == 0) Mmc.AS.SkinStyleNumber = 0;
                if (SkinPointCheck == 1) Mmc.AS.SkinStyleNumber = 1;
                if (SkinPointCheck == 2) Mmc.AS.SkinStyleNumber = 2;
            }
            changeSkinMenu(Mmc.AS.WhatIsSkinShoose, Mmc.AS.SkinStyleNumber);
        }
        SwapTextAndImage();
        ButtonContorol(Mmc.AS.WhatIsSkin);
        Mmc.SaveGovnoEbanoe();
    }

    public void WhatIsShonem ()
    {
        if (Mmc.AS.WhatIsSkinShoose < 3) Shonum = Mmc.AS.WhatIsSkinShoose * 2 + 1;
        if (Mmc.AS.WhatIsSkinShoose >= 3 && Mmc.AS.WhatIsSkinShoose < 6) Shonum = Mmc.AS.WhatIsSkinShoose * 2 + 2;
        if (Mmc.AS.WhatIsSkinShoose >= 6 && Mmc.AS.WhatIsSkinShoose < 9) Shonum = Mmc.AS.WhatIsSkinShoose * 2 + 3;
        if (Mmc.AS.WhatIsSkinShoose >= 9 && Mmc.AS.WhatIsSkinShoose < 12) Shonum = Mmc.AS.WhatIsSkinShoose * 2 + 4;
        if (Mmc.AS.WhatIsSkinShoose >= 12 && Mmc.AS.WhatIsSkinShoose < 15) Shonum = Mmc.AS.WhatIsSkinShoose * 2 + 5;
    }
    public void ViewSuperPower(int count)
    {
        Mmc.audioSource.PlayOneShot(Mmc.tapSound, PlayerPrefs.GetInt("Sound"));
        if (count == 1)
        {
            ActivateText.text = LanguageSistem.lng.SkillWay[0];
            DiscriptionText.text = LanguageSistem.lng.SkillWay[1];
            SkillDiscriptionText[0].text = LanguageSistem.lng.SkillName[Mmc.AS.WhatIsSkin - 1];
            SkillDiscriptionText[1].text = "    - " + LanguageSistem.lng.SkillWay[PassiveOrActiv[Mmc.AS.WhatIsSkin]+1] + "\n";
            SkillDiscriptionText[2].text = "    - " + LanguageSistem.lng.SkillDiscription[Mmc.AS.WhatIsSkin - 1] + "\n";
            
            SuperPower[Mmc.AS.WhatIsSkin-1].SetActive(true);
            BuyPanel.SetActive(true);
        }
        if (count == 0)
        {
            BuyPanel.SetActive(false);
            SuperPower[Mmc.AS.WhatIsSkin-1].SetActive(false);
        }
    }
}

