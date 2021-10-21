using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class AchievementScrolling : MonoBehaviour
{
    [Range(1, 50)]
    [Header("Controllers")]
    public int panCount;
    [Range(0, 1000)]
    public int panOffset;
    [Range(0f, 20f)]
    public float snapSpeed;
    [Header("Other Objects")]
    public GameObject panPrefab;

    public GameObject[] awardSkins;
    private GameObject[] instPans;
    private Vector2[] pansPos;
    public Sprite[] AchievementIcon;
    public Sprite[] ReverseAchievementIcon;
    public Sprite[] AchievementLevelIcon;
    public int[,] AchievementChallengeInt = new int[20, 5] { { 25, 1500, 50000, 1000000, 1000000 }, { 1, 5, 25, 100, 100 }, { 2, 5, 9, 12, 12 }, { 2, 15, 111, 777, 777 }, { 13, 91, 666, 4664, 4664 },
                                                             { 100, 1000, 10000, 100000, 100000 }, { 1, 7, 14, 35, 35 }, { 100, 1500, 35000, 250000, 250000}, { 7, 70, 700, 7000, 7000 }, { 47, 470, 4700, 47000, 47000 },
                                                             { 50, 500, 5000, 50000, 50000 }, { 5, 55, 605, 6655, 6655 }, { 20, 100, 500, 2500, 2500 }, { 125, 500, 2000, 8000, 8000 }, { 1, 2, 3, 4, 4 },
                                                             { 300, 900, 3000, 9000, 9000 }, { 3, 7, 21, 30, 30 }, { 1, 7, 20, 100, 100}, { 1, 2, 5, 10, 10 }, { 1, 2, 3, 5, 5 } };
    public int[] AchievementChallengeInt0;
    public int[] AchievementChallengeInt1;
    public int[] AchievementChallengeInt2;
    public int[] AchievementChallengeInt3;
    private RectTransform contentRect;
    private Vector2 contentVector;
    public GameObject AchievementsMenu;
    public GameObject ShopMenu;
    public GameObject FreeMoneyMenu;

    public Sprite FullAchiev;

    public AchievementSave AS = new AchievementSave();
    public ShopScrolling SS;
    public MainMenuController Mmc;
    public manageLoot ManLoot;

    private int selectedPanID;
    private bool isScrolling;
    public ScrollRect scrollRect;
    public int CountAchievement = 1;

    //ТЕСТ
    public Text Dubager;

    private void Awake()
    {
        CountAchievement = PlayerPrefs.GetInt("CountAchievement", CountAchievement);
        if (CountAchievement == 1)
        {
            int a = 0;
            int b = 1;
            PlayerPrefs.SetInt("Money", a);
            PlayerPrefs.SetInt("Score", a);
            PlayerPrefs.SetInt("Gem", a);
            PlayerPrefs.SetInt("Music", b);
            PlayerPrefs.SetInt("Sound", b);
            PlayerPrefs.SetInt("Save", b);
            PlayerPrefs.SetInt("Call", b);
            /*Dubager.text = "Aа" + 10;
            AS.corn = 0;
            AS.DeathShit = 0;
            Dubager.text = "Aб" + 10;
            AS.HavePrize = 0;
            AS.HowWatch = 0;
            Dubager.text = "Aв" + 10;
            AS.HowManOpen = 0;
            AS.HowLootBoxOpen = 0;
            Dubager.text = "Aг" + 10;
            AS.ProtectionShit = 0;
            AS.WhatIsSkin = 0;
            AS.WhatIsSkinShoose = 0;
            AS.UpgradeCheck = 0;
            Dubager.text = "Aд" + 10;
            AS.DivergentMax = 0;
            AS.JesusMax = 0;
            AS.WhatIsLanguage = 2;
            Dubager.text = "A" + 11;
            AS.LanguageMax = 0;
            AS.СatapultMax = 0;
            AS.LazyMax = 0;
            AS.DataCheck = 0;
            AS.DayCheck = 0;
            AS.BonusCheck = 0;
            AS.SupermanMax = 0;
            AS.MoneyBaff = 0;
            AS.ScoreBaff = 0;
            AS.SecondChanceMax = 0;
            AS.SkinStyleNumber = 0;
            AS.CheckSecret = 0;
            Dubager.text = "A" + 12;
            AS.SkinBuy[0] = 1;*/
            CountAchievement = 0;
            PlayerPrefs.SetInt("CountAchievement", CountAchievement);
            PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
        }
        ShopMenu.SetActive(true);
        FreeMoneyMenu.SetActive(true);
    }
    
    private void Start()
    {
        AS = JsonUtility.FromJson<AchievementSave>(PlayerPrefs.GetString("AchievementSave"));
        contentRect = GetComponent<RectTransform>();
        instPans = new GameObject[panCount];
        pansPos = new Vector2[panCount];
        for (int i = 0; i < panCount; i++)
        {
            instPans[i] = Instantiate(panPrefab, transform, false);
            Slider[] AchievementSlider = instPans[i].GetComponentsInChildren<Slider>();
            Image[] IconSwap = instPans[i].GetComponentsInChildren<Image>();
            Text[] NameSwap = instPans[i].GetComponentsInChildren<Text>();
            Text[] ChallengeSwap = instPans[i].GetComponentsInChildren<Text>();
            IconSwap[1].GetComponent<Image>().sprite = AchievementIcon[i];
            NameSwap[1].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
            int x = i;
            if (AS.WhatAchievemntHavePrize[i] > 0)
            {
                IconSwap[9].GetComponent<Button>().onClick.AddListener(() => CheckButton(x));
                IconSwap[9].GetComponent<Image>().enabled = true;
                IconSwap[9].GetComponent<Button>().enabled = true;
            }
            else 
            {
                IconSwap[9].GetComponent<Button>().onClick.AddListener(() => CheckButton(x));
                IconSwap[9].GetComponent<Image>().enabled = false;
                IconSwap[9].GetComponent<Button>().enabled = false;
            }
            if (AS.AchievementsLevel[i] == 0)
            {
                ChallengeSwap[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge0[i];
                AchievementSlider[0].maxValue = AchievementChallengeInt[i, 0];
                if (i == 0)
                {
                    AchievementSlider[0].value = AS.corn;
                }
                if (i == 1)
                {
                    AchievementSlider[0].value = AS.HowWatch;
                }
                if (i == 2)
                {
                    AchievementSlider[0].value = AS.HowManOpen;
                }
                if (i == 3)
                {
                    AchievementSlider[0].value = AS.HowLootBoxOpen;
                }
                if (i == 4)
                {
                    AchievementSlider[0].value = AS.DeathShit;
                }
                if (i == 5)
                {
                    AchievementSlider[0].value = PlayerPrefs.GetInt("Score");
                }
                if (i == 6)
                {
                    AchievementSlider[0].value = AS.UpgradeCheck;
                }
                if (i == 7)
                {
                    AchievementSlider[0].value = AS.СatapultMax;
                }
                if (i == 8)
                {
                    AchievementSlider[0].value = AS.SecondChanceMax;
                }
                if (i == 9)
                {
                    AchievementSlider[0].value = AS.SupermanMax;
                }
                if (i == 10)
                {
                    AchievementSlider[0].value = AS.DivergentMax;
                }
                if (i == 11)
                {
                    AchievementSlider[0].value = AS.ProtectionShit;
                }
                if (i == 12)
                {
                    AchievementSlider[0].value = AS.LazyMax;
                }
                if (i == 13)
                {
                    AchievementSlider[0].value = AS.JesusMax;
                }
                if (i == 15)
                {
                    AchievementSlider[0].value = PlayerPrefs.GetInt("Money");
                }
                if (i == 16)
                {
                    AchievementSlider[0].value = AS.DataCheck;
                }
                if (i == 17)
                {
                    AchievementSlider[0].value = AS.LanguageMax / 6;
                }
            }
            if (AS.AchievementsLevel[i] == 1)
            {
                ChallengeSwap[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge1[i];
                IconSwap[5].GetComponent<Image>().sprite = AchievementLevelIcon[1];
                AchievementSlider[0].maxValue = AchievementChallengeInt[i, 1];
                if (i == 0)
                {
                    AchievementSlider[0].value = AS.corn;
                }
                if (i == 1)
                {
                    AchievementSlider[0].value = AS.HowWatch;
                }
                if (i == 2)
                {
                    AchievementSlider[0].value = AS.HowManOpen;
                }
                if (i == 3)
                {
                    AchievementSlider[0].value = AS.HowLootBoxOpen;
                }
                if (i == 4)
                {
                    AchievementSlider[0].value = AS.DeathShit;
                }
                if (i == 5)
                {
                    AchievementSlider[0].value = PlayerPrefs.GetInt("Score");
                }
                if (i == 6)
                {
                    AchievementSlider[0].value = AS.UpgradeCheck;
                }
                if (i == 7)
                {
                    AchievementSlider[0].value = AS.СatapultMax;
                }
                if (i == 8)
                {
                    AchievementSlider[0].value = AS.SecondChanceMax;
                }
                if (i == 9)
                {
                    AchievementSlider[0].value = AS.SupermanMax;
                }
                if (i == 10)
                {
                    AchievementSlider[0].value = AS.DivergentMax;
                }
                if (i == 11)
                {
                    AchievementSlider[0].value = AS.ProtectionShit;
                }
                if (i == 12)
                {
                    AchievementSlider[0].value = AS.LazyMax;
                }
                if (i == 13)
                {
                    AchievementSlider[0].value = AS.JesusMax;
                }
                if (i == 15)
                {
                    AchievementSlider[0].value = PlayerPrefs.GetInt("Money");
                }
                if (i == 16)
                {
                    AchievementSlider[0].value = AS.DataCheck;
                }
                if (i == 17)
                {
                    AchievementSlider[0].value = AS.LanguageMax / 6;
                }
            }
            if (AS.AchievementsLevel[i] == 2)
            {
                ChallengeSwap[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge2[i];
                IconSwap[5].GetComponent<Image>().sprite = AchievementLevelIcon[1];
                IconSwap[6].GetComponent<Image>().sprite = AchievementLevelIcon[1];
                AchievementSlider[0].maxValue = AchievementChallengeInt[i, 2];
                if (i == 0)
                {
                    AchievementSlider[0].value = AS.corn;
                }
                if (i == 1)
                {
                    AchievementSlider[0].value = AS.HowWatch;
                }
                if (i == 2)
                {
                    AchievementSlider[0].value = AS.HowManOpen;
                }
                if (i == 3)
                {
                    AchievementSlider[0].value = AS.HowLootBoxOpen;
                }
                if (i == 4)
                {
                    AchievementSlider[0].value = AS.DeathShit;
                }
                if (i == 5)
                {
                    AchievementSlider[0].value = PlayerPrefs.GetInt("Score");
                }
                if (i == 6)
                {
                    AchievementSlider[0].value = AS.UpgradeCheck;
                }
                if (i == 7)
                {
                    AchievementSlider[0].value = AS.СatapultMax;
                }
                if (i == 8)
                {
                    AchievementSlider[0].value = AS.SecondChanceMax;
                }
                if (i == 9)
                {
                    AchievementSlider[0].value = AS.SupermanMax;
                }
                if (i == 10)
                {
                    AchievementSlider[0].value = AS.DivergentMax;
                }
                if (i == 11)
                {
                    AchievementSlider[0].value = AS.ProtectionShit;
                }
                if (i == 12)
                {
                    AchievementSlider[0].value = AS.LazyMax;
                }
                if (i == 13)
                {
                    AchievementSlider[0].value = AS.JesusMax;
                }
                if (i == 15)
                {
                    AchievementSlider[0].value = PlayerPrefs.GetInt("Money");
                }
                if (i == 16)
                {
                    AchievementSlider[0].value = AS.DataCheck;
                }
                if (i == 17)
                {
                    AchievementSlider[0].value = AS.LanguageMax / 6;
                }
            }
            if (AS.AchievementsLevel[i] == 3)
            {
                ChallengeSwap[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge3[i];
                IconSwap[5].GetComponent<Image>().sprite = AchievementLevelIcon[1];
                IconSwap[6].GetComponent<Image>().sprite = AchievementLevelIcon[1];
                IconSwap[7].GetComponent<Image>().sprite = AchievementLevelIcon[1];
                AchievementSlider[0].maxValue = AchievementChallengeInt[i, 3];
                if (i == 0)
                {
                    AchievementSlider[0].value = AS.corn;
                }
                if (i == 1)
                {
                    AchievementSlider[0].value = AS.HowWatch;
                }
                if (i == 2)
                {
                    AchievementSlider[0].value = AS.HowManOpen;
                }
                if (i == 3)
                {
                    AchievementSlider[0].value = AS.HowLootBoxOpen;
                }
                if (i == 4)
                {
                    AchievementSlider[0].value = AS.DeathShit;
                }
                if (i == 5)
                {
                    AchievementSlider[0].value = PlayerPrefs.GetInt("Score");
                }
                if (i == 6)
                {
                    AchievementSlider[0].value = AS.UpgradeCheck;
                }
                if (i == 7)
                {
                    AchievementSlider[0].value = AS.СatapultMax;
                }
                if (i == 8)
                {
                    AchievementSlider[0].value = AS.SecondChanceMax;
                }
                if (i == 9)
                {
                    AchievementSlider[0].value = AS.SupermanMax;
                }
                if (i == 10)
                {
                    AchievementSlider[0].value = AS.DivergentMax;
                }
                if (i == 11)
                {
                    AchievementSlider[0].value = AS.ProtectionShit;
                }
                if (i == 12)
                {
                    AchievementSlider[0].value = AS.LazyMax;
                }
                if (i == 13)
                {
                    AchievementSlider[0].value = AS.JesusMax;
                }
                if (i == 15)
                {
                    AchievementSlider[0].value = PlayerPrefs.GetInt("Money");
                }
                if (i == 16)
                {
                    AchievementSlider[0].value = AS.DataCheck;
                }
                if (i == 17)
                {
                    AchievementSlider[0].value = AS.LanguageMax / 6;
                }
            }
            if (AS.AchievementsLevel[i] == 4)
            {
                ChallengeSwap[0].GetComponent<Text>().text = "";
                ChallengeSwap[1].GetComponent<Text>().text = "";
                IconSwap[1].GetComponent<Image>().sprite = ReverseAchievementIcon[i];
                IconSwap[5].GetComponent<Image>().sprite = AchievementLevelIcon[1];
                IconSwap[6].GetComponent<Image>().sprite = AchievementLevelIcon[1];
                IconSwap[7].GetComponent<Image>().sprite = AchievementLevelIcon[1];
                IconSwap[8].GetComponent<Image>().sprite = AchievementLevelIcon[1];
                IconSwap[0].GetComponent<Image>().sprite = FullAchiev;
                AchievementSlider[0].maxValue = AchievementChallengeInt[i, 0];
                if (i == 0)
                {
                    ChallengeSwap[2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    AchievementSlider[0].value = AS.corn;
                }
                if (i == 1)
                {
                    ChallengeSwap[2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    AchievementSlider[0].value = AS.HowWatch;
                }
                if (i == 2)
                {
                    ChallengeSwap[2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    AchievementSlider[0].value = AS.HowManOpen;
                }
                if (i == 3)
                {
                    ChallengeSwap[2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    AchievementSlider[0].value = AS.HowLootBoxOpen;
                }
                if (i == 4)
                {
                    ChallengeSwap[2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    AchievementSlider[0].value = AS.DeathShit;
                }
                if (i == 5)
                {
                    ChallengeSwap[2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    AchievementSlider[0].value = PlayerPrefs.GetInt("Score");
                }
                if (i == 6)
                {
                    ChallengeSwap[2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    AchievementSlider[0].value = AS.UpgradeCheck;
                }
                if (i == 7)
                {
                    ChallengeSwap[2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    AchievementSlider[0].value = AS.СatapultMax;
                }
                if (i == 8)
                {
                    ChallengeSwap[2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    AchievementSlider[0].value = AS.SecondChanceMax;
                }
                if (i == 9)
                {
                    ChallengeSwap[2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    AchievementSlider[0].value = AS.SupermanMax;
                }
                if (i == 10)
                {
                    ChallengeSwap[2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    AchievementSlider[0].value = AS.DivergentMax;
                }
                if (i == 11)
                {
                    ChallengeSwap[2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    AchievementSlider[0].value = AS.ProtectionShit;
                }
                if (i == 12)
                {
                    ChallengeSwap[2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    AchievementSlider[0].value = AS.LazyMax;
                }
                if (i == 13)
                {
                    ChallengeSwap[2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    AchievementSlider[0].value = AS.JesusMax;
                }
                if (i == 15)
                {
                    ChallengeSwap[2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    AchievementSlider[0].value = PlayerPrefs.GetInt("Money");
                }
                if (i == 16)
                {
                    ChallengeSwap[2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    AchievementSlider[0].value = AS.DataCheck;
                }
                if (i == 17)
                {
                    ChallengeSwap[2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    AchievementSlider[0].value = AS.LanguageMax / 6;
                }
            }
            if (i == 0) continue;
            instPans[i].transform.localPosition = new Vector2(instPans[i].transform.localPosition.x, instPans[i - 1].transform.localPosition.y - panPrefab.GetComponent<RectTransform>().sizeDelta.y - panOffset);
            pansPos[i] = -instPans[i].transform.localPosition;
        }
        AchievementsMenu.SetActive(false);
    }
    public void AchivementLanguageSwap()
    {
        for (int i = 0; i < panCount; i++)
        {
            Text[] NameSwap = instPans[i].GetComponentsInChildren<Text>();
            Text[] ChallengeSwap = instPans[i].GetComponentsInChildren<Text>();
            NameSwap[1].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
            if (AS.AchievementsLevel[i] == 0)
            {
                ChallengeSwap[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge0[i];
            }
            if (AS.AchievementsLevel[i] == 1)
            {
                ChallengeSwap[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge1[i];    
            }
            if (AS.AchievementsLevel[i] == 2)
            {
                ChallengeSwap[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge2[i];
            }
            if (AS.AchievementsLevel[i] == 3)
            {
                ChallengeSwap[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge3[i];
            }
        }
    }

    private void FixedUpdate()
    {
        if (contentRect.anchoredPosition.y <= pansPos[0].y && !isScrolling || contentRect.anchoredPosition.y >= pansPos[pansPos.Length - 1].y && !isScrolling)
        {
            scrollRect.inertia = false;
        }
        float nearestPos = float.MaxValue;
        for (int i = 3; i < panCount-1; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.y - pansPos[i].y);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanID = i;
            }
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.y);
        if (scrollVelocity < 700 && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > 700) return;
        contentVector.y = Mathf.SmoothStep(contentRect.anchoredPosition.y, pansPos[selectedPanID].y, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }

    IEnumerator spawnWinningObj(int type, Transform trans, int count, int styleNum = 0)
    {
        int num = 0;
        if (styleNum > 0)
        {
            if (styleNum == 8) num = 0;
            else if (styleNum == 11) num = 1;
            else if (styleNum == 5) num = 2;
            else if (styleNum == 1) num = 3;
        }

        int realCount = 1;
        if (count <= 10) realCount = count;
        else if (count > 500) realCount = 20;
        else if (count > 100) realCount = 15;
        else realCount = 10;

        for (int i = 0; i < realCount; i++)
        {
            GameObject winGold = Instantiate(Mmc.winningObj[type], transform.parent);
            winGold.GetComponent<RectTransform>().position = trans.position;
            if (styleNum > 0)
            {
                winGold.tag = "Chel";
                GameObject winSkin = Instantiate(awardSkins[num], winGold.transform);
                winSkin.GetComponent<Animator>().Play("chelsAchivki");
            }
            yield return new WaitForSeconds(0.15f);
        }
    }


    public void CheckButton(int num)
    {
        Image[] IconSwap = instPans[num].GetComponentsInChildren<Image>();
        //Mmc.AchievementTypePrize[num, Mmc.AS.AchievementsLevel[num] - Mmc.AS.WhatAchievemntHavePrize[num]];
        if (Mmc.AchievementTypePrize[num, Mmc.AS.AchievementsLevel[num] - Mmc.AS.WhatAchievemntHavePrize[num]] == 0)
        {
            StartCoroutine(spawnWinningObj(0, IconSwap[9].gameObject.transform, Mmc.AchievementPrize[num, Mmc.AS.AchievementsLevel[num] - Mmc.AS.WhatAchievemntHavePrize[num]]));
            Mmc.BuyMoney = PlayerPrefs.GetInt("Money") + Mmc.AchievementPrize[num, Mmc.AS.AchievementsLevel[num] - Mmc.AS.WhatAchievemntHavePrize[num]];
            PlayerPrefs.SetInt("Money", Mmc.BuyMoney);
            Mmc.moneyText.text = "" + PlayerPrefs.GetInt("Money");
            Mmc.BuyMoney = PlayerPrefs.GetInt("Money");
        }
        if (Mmc.AchievementTypePrize[num, Mmc.AS.AchievementsLevel[num] - Mmc.AS.WhatAchievemntHavePrize[num]] == 1)
        {
            StartCoroutine(spawnWinningObj(1, IconSwap[9].gameObject.transform, Mmc.AchievementPrize[num, Mmc.AS.AchievementsLevel[num] - Mmc.AS.WhatAchievemntHavePrize[num]]));

            MainMenuController.Gem = PlayerPrefs.GetInt("Gem") + Mmc.AchievementPrize[num, Mmc.AS.AchievementsLevel[num] - Mmc.AS.WhatAchievemntHavePrize[num]];
            PlayerPrefs.SetInt("Gem", MainMenuController.Gem);
            Mmc.gemText.text = "" + PlayerPrefs.GetInt("Gem");
            MainMenuController.Gem = PlayerPrefs.GetInt("Gem");
        }
        if (Mmc.AchievementTypePrize[num, Mmc.AS.AchievementsLevel[num] - Mmc.AS.WhatAchievemntHavePrize[num]] == 2)
        {
            StartCoroutine(spawnWinningObj(0, IconSwap[9].gameObject.transform, 1, num));
            if (num == 8) 
            {
                Mmc.AS.SkinBuy[1] = 1;
                SS.changeMaterialObj(SS.skinsMenu[1], SS.classic);
            }
            if (num == 11) 
            {
                Mmc.AS.SkinBuy[3] = 1;
                SS.changeMaterialObj(SS.skinsMenu[3], SS.classic);
            }
            if (num == 11 && Mmc.AS.AchievementsLevel[num] == 3)
            {
                Mmc.AS.SkinBuyStyle1[3] = 1;
                SS.changeMaterialObj(SS.skinsMenu[3], SS.classic);
            }
            if (num == 5) 
            {
                Mmc.AS.SkinBuy[4] = 1;
                SS.changeMaterialObj(SS.skinsMenu[4], SS.classic);
            }
            if (num == 1) 
            {
                Mmc.AS.SkinBuy[6] = 1;
                SS.changeMaterialObj(SS.skinsMenu[6], SS.classic);
            }
        }
        Mmc.AS.WhatAchievemntHavePrize[num] -= 1;
        if (Mmc.AS.WhatAchievemntHavePrize[num] == 0) 
        {
            Debug.Log(num);
            IconSwap[9].GetComponent<Image>().enabled = false;
            IconSwap[9].GetComponent<Button>().enabled = false;
        }
        Mmc.SignalAchievement();
        Mmc.SaveGovnoEbanoe();
    }

    /*private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
    }*/

    public void AchievementsLevelCheck()
    {
        for (int i = 0; i < 20; i++)
        {
            if (AS.corn >= AchievementChallengeInt0[0])
            {
                AS.AchievementsLevel[0] = 1;
                //AchievementSlider[0].value = AS.corn;
            }
        }
    }
}

[Serializable]
public class AchievementSave
{
    public int[] AchievementsLevel = new int[50];
    public int[] UpgradeLevel = new int[20];
    public int[] LanguageCheck = new int[20];
    public int[] SkinBuy = new int[40];
    public int[] SkinBuyStyle1 = new int[40];
    public int[] SkinBuyStyle2 = new int[40];
    public int[] WhatAchievemntHavePrize = new int[50];
    public int[] BaffCheck = new int[15];
    public int ItsYourFirstTry = 0;
    public int ItsYourFirstSkill = 0;
    public int SkinStyleNumber = 0;
    public int HavePrize = 0;
    public int HowWatch = 0;
    public int HowManOpen = 0;
    public int HowLootBoxOpen = 0;
    public int WhatIsLanguage = 0;
    public int corn = 0;
    public int DeathShit = 0;
    public int ProtectionShit = 0;
    public int WhatIsSkin = 0;
    public int WhatIsSkinShoose = 0;
    public int UpgradeCheck = 0;
    public int DivergentMax = 0;
    public int JesusMax = 0;
    public int LanguageMax = 0;
    public int СatapultMax = 0;
    public int LazyMax = 0;
    public int DataCheck = 0;
    public int DayCheck = 0;
    public float BonusCheck = 0;
    public int MoneyBaff = 0;
    public int ScoreBaff = 0;
    public int SupermanMax = 0;
    public int SecondChanceMax = 0;
    public int CheckSecret = 0;
}
