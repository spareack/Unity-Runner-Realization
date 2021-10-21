using JetBrains.Annotations;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;


public class MainMenuController : MonoBehaviour, IUnityAdsListener
{
    public bool firstStart = true;
    private int adsCheck = 2;

    public GameObject [] studyHints;

    public AudioSource camAudio;
    public AudioSource audioSource;
    public AudioClip tapSound;

    public Button startGameButton;
    public Button chooseConfirm;
    public Button buyBoxButtonTicket;
    public Button buyBoxButtonCoin;
    public Button freeTicketButton;
    public Button closeAchievements;
    public Button closeShop;
    public Button closeUpgrade;

    public Text moneyText;
    public Text scoreText;
    public Text gemText;
    public Text PlayButton;

    public Text MusicSettings;
    public Text SoundSettings;
    public Text SaveSettings;
    public Text CallSettings;

    public Text SignalCountText;
    public Text WorldTableSoon;

    public Text LootPrizeText;
    public Text MoneyBaffText;
    public Text ScoreBaffText;

    public Color NonInteract = new Color(255f, 255f, 255f, 255f);
    public Color Interact = new Color(255f, 255f, 255f, 255f);

    public GameObject TopPanel;

    public GameObject [] winningObj;
    public GameObject ShopMenu;
    public GameObject FreeMoneyMenu;
    public GameObject SettingsMenu;
    public GameObject AchievementsMenu;
    public GameObject UpgradeMenu;
    public GameObject LanguageMenu;
    public GameObject ConfirmMenu;
    public GameObject WorldTableMenu;
    public GameObject[] ShopTable;
    public GameObject AchievementTable;
    public GameObject LootBoxInfo;
    public GameObject LootBoxModel;
    public GameObject SignalPoint;
    public GameObject MoneyBaff;
    public GameObject ScoreBaff;
    public GameObject AchievementPaneler;

    public GameObject FreeTicketView;

    public int confirm = -1;

    public string NonUpgrade = "ВалераЛох";

    public GameObject[] UpdatePanels;
    public GameObject[] PriceUpgrade;
    public GameObject[] AchievementLevelUp;
    public Sprite[] AchievementLevelIcon;
    public Animator CloseAchievAnim;
    public Animator OpenAchievAnim;
    public Animator studyArrowAnim;
    public Button[] BuyButton;
    public Text[] BuyButtonText;
    public Image[] ButtonSettings;
    public Image[] ButtonLanguage;
    public Sprite[] ButtonSettingsOn;
    public Sprite[] ButtonSettingsOff;
    public Sprite[] ButtonLanguageOn;
    public Sprite[] ButtonLanguageOff;

    public float SizeHeight;
    public float SizeWidth;
    public float ScreenSize;

    public int countCoins;
    public float currentCoins;
    public float soundsVolume = 1;
    public int countGems;
    public float currentGems;
    public int RanPrizeInt;
    public int BuyMoney;
    public int SignalCount;
    public int[,] UpgradePrise = new int[7, 6] { { 5, 20, 70, 150, 350, 1000 }, { 5, 20, 70, 150, 350, 1000 }, { 5, 20, 70, 150, 350, 1000 }, { 5, 20, 70, 150, 350, 1000 }, 
                                                 { 7, 28, 90, 200, 450, 1000 }, { 7, 28, 90, 200, 450, 1000 }, { 7, 28, 90, 200, 450, 1000 } };
    public int[,] AchievementPrize = new int[40, 4] { { 10, 50, 250, 1000}, { 1, 5, 0, 100 }, { 25, 5, 100, 50 }, { 1, 3, 15, 50 }, { 10, 50, 35, 90 }, { 15, 50, 150, 500 }, { 5, 15, 50, 700 }, { 2, 30, 100, 40 }, { 0, 40, 20, 75 }, { 2, 20, 50, 100 },
                                                        { 2, 8, 25, 75}, { 0, 0, 75, 200 }, { 10, 40, 100, 400 }, { 20, 80, 200, 800 }, { 25, 25, 25, 25 },{ 100, 300, 1000, 3000}, { 3, 7, 21, 50 }, { 10, 70, 250, 1000 }, { 1000, 2000, 5000, 10000 }, { 50, 50, 50, 100 },
                                                        { 0, 0, 0, 0}, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 },{ 0, 0, 0, 0}, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 },
                                                        { 0, 0, 0, 0}, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 },{ 0, 0, 0, 0}, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }};
    public int[,] AchievementTypePrize = new int[40, 4] { { 0, 0, 0, 0}, { 1, 2, 1, 1 }, { 0, 1, 0, 1 }, { 1, 1, 1, 1 }, { 0, 0, 1, 1 }, { 2, 0, 0, 0 }, { 0, 0, 0, 0 }, { 1, 0, 0, 1 }, { 2, 0, 1, 1 }, { 1, 1, 1, 1 },
                                                        { 0, 0, 0, 0}, { 2, 2, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 1, 1, 1, 1 },{ 0, 0, 0, 0}, { 1, 1, 1, 1 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 1, 1, 1, 1 },
                                                        { 0, 0, 0, 0}, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 },{ 0, 0, 0, 0}, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 },
                                                        { 0, 0, 0, 0}, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 },{ 0, 0, 0, 0}, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }};
    // 0 - деньги | 1 - билеты | 2 - челик 
    public Text[] UpgradePriseText;
    public Text[] UpgradeLevel;
    public Text[] UpgradeName;
    public Text[] UpgradeDoIt;
    public Text[] Titles;
    public Text[] Titles2;
    public Text[] Titles3;
    public Text[] LootInfoText;
    static public int Music = 1;
    static public int Sound = 1;
    static public int Language = 1;
    static public int Save = 1;
    static public int Call = 1;
    static public int Gem;
    public int width;
    public int height;
    public int Upgrade6Level;

    public int StartSave = 1;

    public Animator CHELSTART;

    public AchievementScrolling AchScr;
    public ShopScrolling SS;
    public manageLoot ManLoot;
    public LanguageSistem LangSis;
    public AchievementSave AS = new AchievementSave();
    public studyButtonsTest buttonsTest;

    public GameObject LoadScreen;
    public GameObject LoadScreenBar;
    public Image LoadBar;
    public Text PercentText;
    public Text LoadingText;
    private bool adsSupported = false;
    public bool FirstOpenShop = true;
    public bool GameIsStart = false;
    public Text Dubager;

    public class studyButtonsTest
    {
        public bool []buttonsChecked;
        // public Button []buttons = new Button[8];
        public List<Button> buttons = new List<Button>();
        // 0 - freePopButton allButtons[3];
        // 1 - trainingButton allButtons[9];
        // 2 - settingsButton allButtons[4];
        // 3 - laderButton this.allButtons[10];
        // 4 - upgradeButton this.allButtons[11];
        // 5 - shopButton this.allButtons[12];
        // 6 - achievementsButton this.allButtons[15];

        private Button[] allButtons;
        private int needToCheckButton;
        private int butCount = 0;
        private Button buyBoxButtonCoin1;
        private Button buyBoxButtonTicket2;
        private Button chooseRunner;
        private Animator studyArrowAnim;
        private GameObject [] studyHints;
        public MainMenuController Mmc;

        public studyButtonsTest(Button buyBoxButtonTicket1, Button buyBoxButtonCoin2, Button freeTicketButton1, Button closeAchievements1,
            Button closeShop1, Button closeUpgrade1, Button chooseConfirm1, MainMenuController Mmc, Animator studyArrowAnim, GameObject [] studyHints)
        {
            this.studyHints = studyHints;
            this.Mmc = Mmc;
            this.studyArrowAnim = studyArrowAnim;
            this.buyBoxButtonCoin1 = buyBoxButtonCoin2;
            this.buyBoxButtonTicket2 = buyBoxButtonTicket1;
            this.allButtons = FindObjectsOfType<Button>();

            // freeMenu
            buttons.Add(allButtons[3]);
            buttons.Add(freeTicketButton1);
            buttons.Add(buyBoxButtonTicket1);
            buttons.Add(allButtons[18]);

            // achieve
            buttons.Add(allButtons[15]);
            buttons.Add(closeAchievements1);

            // lader
            buttons.Add(allButtons[10]);

            // settings
            buttons.Add(allButtons[9]);
            buttons.Add(allButtons[18]);

            // upgrade
            buttons.Add(allButtons[11]);
            buttons.Add(closeUpgrade1);

            //shop
            buttons.Add(allButtons[12]);
            // buttons.Add(chooseRunner);
            // buttons.Add(chooseConfirm1);
            buttons.Add(closeShop1);

            this.needToCheckButton = 0;
            this.turnOffAllButtons();
            foreach (Button x in this.allButtons) x.interactable = false;
            buttons[0].interactable = true;
            buttonsChecked = new bool[buttons.Count];
            // studyArrowAnim.Play("freeStudy");
            studyHints[0].SetActive(true);
        }
        private void turnOffAllButtons()
        {
            foreach (Button x in this.buttons) x.interactable = false;
            buyBoxButtonCoin1.interactable = false;
            buyBoxButtonTicket2.interactable = false;
            allButtons[19].gameObject.SetActive(false);
        }
        private void turnOnAllButtons()
        {
            foreach (Button x in this.buttons) x.interactable = true;
            buyBoxButtonCoin1.interactable = true;
            allButtons[19].gameObject.SetActive(true);
        }
        public void checkCurrentButtonsState(int nullButton = 0)
        {
            if (this.needToCheckButton == buttonsChecked.Length) return;

            studyArrowAnim.Play("defStydtAbunArrow");
            foreach(GameObject x in studyHints) x.SetActive(false);

            if (this.needToCheckButton == 0) studyArrowAnim.Play("freePopStudy");
            else if (this.needToCheckButton == 1) studyArrowAnim.Play("buyBoxStudy");
            else if (this.needToCheckButton == 2) studyArrowAnim.Play("closeStudyAchiev");
            else if (this.needToCheckButton == 3) studyHints[2].SetActive(true);
            else if (this.needToCheckButton == 4) studyArrowAnim.Play("closeStudyAchiev");
            else if (this.needToCheckButton == 5) studyHints[1].SetActive(true);
            else if (this.needToCheckButton == 6) studyHints[3].SetActive(true);
            else if (this.needToCheckButton == 7) studyArrowAnim.Play("closeStudyAchiev");
            else if (this.needToCheckButton == 8) studyHints[4].SetActive(true);
            else if (this.needToCheckButton == 9) studyArrowAnim.Play("closeStudyAchiev");
            else if (this.needToCheckButton == 10) studyHints[5].SetActive(true);
            else if (this.needToCheckButton == 11) studyArrowAnim.Play("closeStudyAchiev");

            if (this.needToCheckButton == buttonsChecked.Length - 1) 
            {
                this.turnOnAllButtons();
                this.needToCheckButton += 1;
                Mmc.AS.ItsYourFirstTry = 2;
                Mmc.LoadTo(0);
            }
            else
            {
                this.turnOffAllButtons();
                this.needToCheckButton += 1;
                if (nullButton == 0)
                {
                    butCount += 1;
                    this.buttons[this.butCount].interactable = true;
                }
            }
        }
    }

    private const string leaderBoard = "CgkIofDKlOUZEAIQAQ";


    private void Awake()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("4093675", false);
            adsSupported = true;
            Advertisement.AddListener(this);
        }

        //AS = JsonUtility.FromJson<AchievementSave>(PlayerPrefs.GetString("AchievementSave"));
        //StartSave = PlayerPrefs.GetInt("StartSave");
        AchievementsMenu.SetActive(true);
        /*if (StartSave == 1)
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
            AS.corn = 0;
            AS.DeathShit = 0;
            AS.HavePrize = 0;
            AS.HowWatch = 0;
            AS.HowManOpen = 0;
            AS.HowLootBoxOpen = 0;
            AS.ProtectionShit = 0;
            AS.WhatIsSkin = 0;
            AS.WhatIsSkinShoose = 0;
            AS.UpgradeCheck = 0;
            AS.DivergentMax = 0;
            AS.JesusMax = 0;
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
            StartSave = 0;
            PlayerPrefs.SetInt("StartSave", StartSave);
            PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
        }*/
    }

    private void Start()
    {
        Time.timeScale = 1f;
        Application.targetFrameRate = 60;
        AS = JsonUtility.FromJson<AchievementSave>(PlayerPrefs.GetString("AchievementSave"));
        AS.SkinBuy[0] = 1;
        AS.WhatIsSkin = AS.WhatIsSkinShoose;
        SizeHeight = Screen.height;
        SizeWidth = Screen.width;
        ScreenSize = SizeHeight / SizeWidth * 1.0f;
        if (ScreenSize > 2.03f)
        {
            ShopTable[0].SetActive(true);
            ShopTable[1].SetActive(false);
            ShopTable[2].SetActive(false);
            // Умения
            ShopTable[3].SetActive(true);
            ShopTable[4].SetActive(false);
            ShopTable[5].SetActive(true);
            ShopTable[6].SetActive(true);
        }
        else if ((ScreenSize < 2.03f) && (ScreenSize > 1.9f))
        {
            ShopTable[0].SetActive(false);
            ShopTable[1].SetActive(true);
            ShopTable[2].SetActive(false);
            // Умения
            ShopTable[3].SetActive(true);
            ShopTable[4].SetActive(false);
            ShopTable[5].SetActive(false);
            ShopTable[6].SetActive(false);
        }
        else if (ScreenSize < 1.9f)
        {
            ShopTable[0].SetActive(false);
            ShopTable[1].SetActive(false);
            ShopTable[2].SetActive(true);
            // Умения
            ShopTable[3].SetActive(false);
            ShopTable[4].SetActive(true);
            ShopTable[5].SetActive(false);
            ShopTable[6].SetActive(false);
        }

        if (PlayerPrefs.GetInt("Music") == 0)
        {
            ButtonSettings[0].sprite = ButtonSettingsOff[0];
            camAudio.volume = 0;
        }
        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            ButtonSettings[1].sprite = ButtonSettingsOff[1];
            soundsVolume = 0;
        }
        if (PlayerPrefs.GetInt("Save") == 0)
        {
            ButtonSettings[3].sprite = ButtonSettingsOff[3];
        }
        if (PlayerPrefs.GetInt("Call") == 0)
        {
            ButtonSettings[4].sprite = ButtonSettingsOff[4];
        }
        DateTime DataNow = DateTime.Now;
        if (DataNow.Day != AS.DayCheck && (DataNow.Day - AS.DayCheck) < 2)
        {
            AS.DataCheck += 1;
            AS.DayCheck = DataNow.Day;
            AS.BonusCheck = 0f;
        }
        else if (DataNow.Day != AS.DayCheck && (DataNow.Day - AS.DayCheck) >= 2)
        {
            AS.DataCheck = 0;
            AS.DayCheck = DataNow.Day;
            AS.BonusCheck = 0f;
        }
        for (int i = 0; i < 7; i++)
        {
            UpgradeName[i].text = LanguageSistem.lng.UpgradeNameText[i];
            UpgradeDoIt[i].text = LanguageSistem.lng.UpgradeDoItText[i];
            UpgradePriseText[i].text = "" + UpgradePrise[i, AS.UpgradeLevel[i]];
            UpgradeLevel[i].text = "" + AS.UpgradeLevel[i];
        }
        PlayButton.text = LanguageSistem.lng.PlayText;
        MusicSettings.text = LanguageSistem.lng.MusicSettingsText;
        SoundSettings.text = LanguageSistem.lng.SoundSettingsText;
        SaveSettings.text = LanguageSistem.lng.SaveSettingsText;
        CallSettings.text = LanguageSistem.lng.CallSettingsText;
        WorldTableSoon.text = LanguageSistem.lng.SoonText;
        Gem = PlayerPrefs.GetInt("Gem");
        BuyMoney = PlayerPrefs.GetInt("Money");
        gemText.text = "" + PlayerPrefs.GetInt("Gem");
        moneyText.text = "" + PlayerPrefs.GetInt("Money");
        scoreText.text = "" + PlayerPrefs.GetInt("Score");
        for (int i = 0; i < 5; i++)
        {
            Titles[i].text = LanguageSistem.lng.TitlesText[i];
            Titles2[i].text = LanguageSistem.lng.TitlesText[i];
            Titles3[i].text = LanguageSistem.lng.TitlesText[i];
            LootInfoText[i].text = LanguageSistem.lng.LootInfoText[i];
        }
        for (int i = 0; i < 8; i++)
        {
            if (AS.WhatIsLanguage == i)
            {
                ButtonLanguage[i].sprite = ButtonLanguageOn[i];
            }
        }
        if (AS.MoneyBaff != 0)
        {
            MoneyBaff.SetActive(true);
            MoneyBaffText.text = AS.MoneyBaff + " " +LanguageSistem.lng.BaffTimeText;
        }
        else if (AS.MoneyBaff == 0)
        {
            MoneyBaff.SetActive(false);
        }
        if (AS.ScoreBaff != 0)
        {
            ScoreBaff.SetActive(true);
            ScoreBaffText.text = AS.ScoreBaff + " " + LanguageSistem.lng.BaffTimeText;
        }
        else if (AS.ScoreBaff == 0)
        {
            ScoreBaff.SetActive(false);
        }
        SignalAchievement();

        // if (AS.ItsYourFirstTry == 1 || firstStart)
        if (AS.ItsYourFirstTry == 1)
        {
            buttonsTest = new studyButtonsTest(buyBoxButtonTicket, buyBoxButtonCoin, freeTicketButton, closeAchievements,
                                               closeShop, closeUpgrade, chooseConfirm, this, studyArrowAnim, studyHints);
        }
        else if (AS.ItsYourFirstTry == 0) LoadTo(2);
        else
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            // PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate( sucess => { });
            Social.ReportScore(PlayerPrefs.GetInt("Score"), leaderBoard, (bool sucess) => { });
        }

        audioSource = GetComponent<AudioSource>();
        StartCoroutine(enableStartGameButton());
    }

    private void Update()
    {
        if (check1 > 0) check1 -= 0.01f;
        if (check2 > 0) check2 -= 0.01f;
        // Debug.Log(AS.SkinBuy[6] + "pol");
        if (Application.platform == RuntimePlatform.Android && Input.GetKey(KeyCode.Escape))
        {
            CloseAll();
        }

        countCoins = PlayerPrefs.GetInt("Money");
        if (countCoins != currentCoins)
        {
            currentCoins = Mathf.Lerp(currentCoins, countCoins, Time.deltaTime * 3);
            moneyText.text = Mathf.Round(currentCoins).ToString();
        }

        countGems = PlayerPrefs.GetInt("Gem");
        if (countGems != currentGems)
        {
            currentGems = Mathf.Lerp(currentGems, countGems, Time.deltaTime * 3);
            gemText.text = Mathf.Round(currentGems).ToString();
        }
    }

    IEnumerator enableStartGameButton()
    {
        yield return new WaitForSeconds(2);
        startGameButton.interactable = true;
    }

    public void LoadTo(int butt)
    {
        // StartCoroutine(StartGame());
        if (buttonsTest != null) buttonsTest.checkCurrentButtonsState();
        AS.LanguageCheck[AS.WhatIsLanguage] += 1;
        if (AS.MoneyBaff != 0) AS.MoneyBaff -= 1;
        if (AS.ScoreBaff != 0) AS.ScoreBaff -= 1;
        PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
        
        CHELSTART.SetTrigger("IsStart");
        LoadingText.text = LanguageSistem.lng.LoadingText[UnityEngine.Random.Range(0, 15)];
        GameIsStart = true;
        TopPanel.SetActive(false);
        LoadScreen.SetActive(true);
        LoadScreenBar.SetActive(true);
        StartCoroutine(AsyncLoad(butt));
    }

    IEnumerator AsyncLoad(int butt)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(butt);
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            LoadBar.fillAmount = progress;
            PercentText.text = string.Format("{0:0}%", progress * 100);
            yield return null;
        }
    }
    IEnumerator StartGame()
    {
        CHELSTART.SetTrigger("IsStart");
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(1);
    }
    public void OpenShop()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        ShopMenu.SetActive(true);
        if (buttonsTest != null) buttonsTest.checkCurrentButtonsState();
        if (FirstOpenShop == true) 
        {
            FirstOpenShop = false;
            SS.SwapTextAndImageFoStart();
        }
        
    }
    
    public void OpenFreeMoney()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        ManLoot.spawnBox();
        ManLoot.CheckLootMoney();
        if (buttonsTest != null) buttonsTest.checkCurrentButtonsState();
        FreeMoneyMenu.SetActive(true);
    }

    public void OpenSettings()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        if (buttonsTest != null) buttonsTest.checkCurrentButtonsState();
        SettingsMenu.SetActive(true);
    }

    public void OpenAchievements()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        CheckAchievement();
        if (buttonsTest != null) buttonsTest.checkCurrentButtonsState();
        AchievementsMenu.SetActive(true);
        Image[] APanel = AchievementTable.GetComponentsInChildren<Image>();
        Slider[] ASlider = AchievementTable.GetComponentsInChildren<Slider>();
        Text[] TSlider = AchievementTable.GetComponentsInChildren<Text>();
        for (int i = 0; i < 20; i++)
        {
            if (AS.WhatAchievemntHavePrize[i] > 0)
            {
                APanel[i * 10 + 9].GetComponent<Image>().enabled = true;
                APanel[i * 10 + 9].GetComponent<Button>().enabled = true;
            }
            else
            {
                APanel[i * 10 + 9].GetComponent<Image>().enabled = false;
                APanel[i * 10 + 9].GetComponent<Button>().enabled = false;
            }
            if (AS.AchievementsLevel[i] == 0)
            {
                TSlider[3 * i].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge0[i];
                ASlider[i].maxValue = AchScr.AchievementChallengeInt[i, 0];
                if (i == 1)
                {
                    ASlider[1].value = AS.HowWatch;
                }
                if (i == 2)
                {
                    ASlider[2].value = AS.HowManOpen;
                }
                if (i == 3)
                {
                    ASlider[3].value = AS.HowLootBoxOpen;
                }
                if (i == 6)
                {
                    ASlider[6].value = AS.UpgradeCheck;
                }
                if (i == 15)
                {
                    ASlider[15].value = PlayerPrefs.GetInt("Money");
                }
                if (i == 16)
                {
                    ASlider[16].value = AS.DataCheck;
                }
                if (i == 17)
                {
                    ASlider[17].value = AS.LanguageMax / 6;
                }
            }
            if (AS.AchievementsLevel[i] == 1)
            {
                TSlider[3 * i].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge1[i];
                APanel[i * 10 + 5].GetComponent<Image>().sprite = AchScr.AchievementLevelIcon[1];
                ASlider[i].maxValue = AchScr.AchievementChallengeInt[i, 1];
                if (i == 1)
                {
                    ASlider[1].value = AS.HowWatch;
                }
                if (i == 2)
                {
                    ASlider[2].value = AS.HowManOpen;
                }
                if (i == 3)
                {
                    ASlider[3].value = AS.HowLootBoxOpen;
                }
                if (i == 6)
                {
                    ASlider[6].value = AS.UpgradeCheck;
                }
                if (i == 15)
                {
                    ASlider[15].value = PlayerPrefs.GetInt("Money");
                }
                if (i == 16)
                {
                    ASlider[16].value = AS.DataCheck;
                }
                if (i == 17)
                {
                    ASlider[17].value = AS.LanguageMax / 6;
                }
            }
            if (AS.AchievementsLevel[i] == 2)
            {
                TSlider[3 * i].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge2[i];
                APanel[i * 10 + 5].GetComponent<Image>().sprite = AchScr.AchievementLevelIcon[1];
                APanel[i * 10 + 6].GetComponent<Image>().sprite = AchScr.AchievementLevelIcon[1];
                ASlider[i].maxValue = AchScr.AchievementChallengeInt[i, 2];
                if (i == 1)
                {
                    ASlider[1].value = AS.HowWatch;
                }
                if (i == 2)
                {
                    ASlider[2].value = AS.HowManOpen;
                }
                if (i == 3)
                {
                    ASlider[3].value = AS.HowLootBoxOpen;
                }
                if (i == 6)
                {
                    ASlider[6].value = AS.UpgradeCheck;
                }
                if (i == 15)
                {
                    ASlider[15].value = PlayerPrefs.GetInt("Money");
                }
                if (i == 16)
                {
                    ASlider[16].value = AS.DataCheck;
                }
                if (i == 17)
                {
                    ASlider[17].value = AS.LanguageMax / 6;
                }
            }
            if (AS.AchievementsLevel[i] == 3)
            {
                TSlider[3 * i].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge3[i];
                APanel[i * 10 + 5].GetComponent<Image>().sprite = AchScr.AchievementLevelIcon[1];
                APanel[i * 10 + 6].GetComponent<Image>().sprite = AchScr.AchievementLevelIcon[1];
                APanel[i * 10 + 7].GetComponent<Image>().sprite = AchScr.AchievementLevelIcon[1];
                ASlider[i].maxValue = AchScr.AchievementChallengeInt[i, 3];
                if (i == 1)
                {
                    ASlider[1].value = AS.HowWatch;
                }
                if (i == 2)
                {
                    ASlider[2].value = AS.HowManOpen;
                }
                if (i == 3)
                {
                    ASlider[3].value = AS.HowLootBoxOpen;
                }
                if (i == 6)
                {
                    ASlider[6].value = AS.UpgradeCheck;
                }
                if (i == 15)
                {
                    ASlider[15].value = PlayerPrefs.GetInt("Money");
                }
                if (i == 16)
                {
                    ASlider[16].value = AS.DataCheck;
                }
                if (i == 17)
                {
                    ASlider[17].value = AS.LanguageMax / 6;
                }
            }
            if (AS.AchievementsLevel[i] == 4)
            {
                TSlider[3 * i].GetComponent<Text>().text = "";
                TSlider[3 * i + 1].GetComponent<Text>().text = "";
                TSlider[3 * i + 2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                APanel[i * 10 + 1].GetComponent<Image>().sprite = AchScr.ReverseAchievementIcon[i];
                APanel[i * 10 + 5].GetComponent<Image>().sprite = AchScr.AchievementLevelIcon[1];
                APanel[i * 10 + 6].GetComponent<Image>().sprite = AchScr.AchievementLevelIcon[1];
                APanel[i * 10 + 7].GetComponent<Image>().sprite = AchScr.AchievementLevelIcon[1];
                APanel[i * 10 + 8].GetComponent<Image>().sprite = AchScr.AchievementLevelIcon[1];
                ASlider[i].maxValue = AchScr.AchievementChallengeInt[i, 3];
                if (i == 1)
                {
                    TSlider[3 * i + 2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    ASlider[1].value = AS.HowWatch;
                }
                if (i == 2)
                {
                    TSlider[3 * i + 2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    ASlider[2].value = AS.HowManOpen;
                }
                if (i == 3)
                {
                    TSlider[3 * i + 2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    ASlider[3].value = AS.HowLootBoxOpen;
                }
                if (i == 6)
                {
                    TSlider[3 * i + 2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    ASlider[6].value = AS.UpgradeCheck;
                }
                if (i == 15)
                {
                    TSlider[3 * i + 2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    ASlider[15].value = PlayerPrefs.GetInt("Money");
                }
                if (i == 16)
                {
                    TSlider[3 * i + 2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    ASlider[16].value = AS.DataCheck;
                }
                if (i == 17)
                {
                    TSlider[3 * i + 2].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[i];
                    ASlider[17].value = AS.LanguageMax / 6;
                }
            }
        }
    }

    public void OpenUpgrade()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        CheckUpgrade6Level();
        if (buttonsTest != null) buttonsTest.checkCurrentButtonsState();
        UpgradeMenu.SetActive(true);
    }
    public void OpenWorldTable()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        if (buttonsTest != null) buttonsTest.checkCurrentButtonsState();
        showLeaderboard();
        StartCoroutine(WorldTableCoron());
    }
    public void CloseAll()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        if (buttonsTest != null) buttonsTest.checkCurrentButtonsState();
        CheckUpgrade6Level();
        CheckAchievement();
        ShopMenu.SetActive(false);
        FreeMoneyMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        AchievementsMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
    }
    public void TakeGem()
    {
        if (buttonsTest != null) 
        {
            buttonsTest.checkCurrentButtonsState();
            PlayerPrefs.SetInt("Gem", PlayerPrefs.GetInt("Gem") + 1);
            gemText.text = "" + PlayerPrefs.GetInt("Gem", Gem);

            GameObject winGold = Instantiate(winningObj[1], TopPanel.transform);
            winGold.GetComponent<RectTransform>().position = new Vector3( Input.mousePosition.x / 5000f -0.1f, 1.1f, -9.5f);
            return;
        }
        if (!adsSupported)
        {
            // Debug.Log("not supported");
            return;
        }

        if (Advertisement.IsReady())
        {
            StartCoroutine(getAwardAdsCheck());
        }
        else 
        {
            // Debug.Log("wait for load and try again");
        }
        ManLoot.CheckLootMoney();
    }

    public IEnumerator getAwardAdsCheck(bool secret = false)
    {
        Time.timeScale = 0f;
        Advertisement.Show("rewardedVideo");

        while(adsCheck == 2)
        {
            yield return null;
        }
        Time.timeScale = 1f;

        if (adsCheck == 0)
        {
            Gem = PlayerPrefs.GetInt("Gem") + 5;
            if (AS.WhatIsSkinShoose == 6) Gem += 2;
            AS.HowWatch += 1;
            PlayerPrefs.SetInt("Gem", Gem);
            gemText.text = "" + PlayerPrefs.GetInt("Gem", Gem);
            CheckAchievement();

            if (buttonsTest != null) 
            {
                ManLoot.CheckLootMoney();
            }

            for (int i = 0; i < 5; i ++)
            {
                GameObject winGold = Instantiate(winningObj[1], TopPanel.transform);
                winGold.GetComponent<RectTransform>().position = new Vector3( Input.mousePosition.x / 5000f -0.1f, 1.1f, -9.5f);
                yield return new WaitForSeconds(0.2f);
                if (GameIsStart == true) 
                {
                    break;
                }
                
            }

            if (secret != false)
            {
                PlayerPrefs.SetInt("Gem", PlayerPrefs.GetInt("Gem") + 50);
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 500);

                for (int i = 0; i < 60; i ++)
                {
                    GameObject winGold = Instantiate(winningObj[1], TopPanel.transform);
                    winGold.GetComponent<RectTransform>().position = new Vector3( Input.mousePosition.x / 5000f -0.1f, 1.1f, -9.5f);

                    GameObject winGold1 = Instantiate(winningObj[0], TopPanel.transform);
                    winGold1.GetComponent<RectTransform>().position = new Vector3( Input.mousePosition.x / 5000f -0.1f, 1.1f, -9.5f);

                    yield return new WaitForSeconds(0.2f);
                    if (GameIsStart == true)
                    {
                        break;
                    }
                }
            }

            // winGold.GetComponent<RectTransform>().position = Input.mousePosition / 1000f - new Vector3(10f, 0f, 1f);
            // Debug.Log(Input.mousePosition + " eto == " + Input.mousePosition / 1000f);
        }
        adsCheck = 2;
    }


    public float check1 = 0;
    public float check2 = 0;

    public void secretFicha(int count)
    {
        if (count == 1) check1 += 0.2f;
        else if (count == 2) check2 += 0.2f;

        if (check1 > 1 && check2 > 1) 
        {
            if (adsSupported && Advertisement.IsReady())
            {
                check1 = 0f;
                check2 = 0f;
                StartCoroutine(getAwardAdsCheck(true));
            }
        }
    }


    public void CheckAchievement()
    {
        for (int i = 0; i < 4; i++)
        {
            if (AS.AchievementsLevel[1] == i && AS.HowWatch >= AchScr.AchievementChallengeInt[1, i])
            {
                AchievementOpen(AS.AchievementsLevel[1], 1);
                Slider[] ASlider = AchievementPaneler.GetComponentsInChildren<Slider>();
                ASlider[0].value = AS.HowWatch;
                AS.AchievementsLevel[1] += 1;
                AS.WhatAchievemntHavePrize[1] += 1;
                StartCoroutine(AchievementOpenWithCoroutine(AS.AchievementsLevel[1], 1));
                SaveGovnoEbanoe();
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (AS.AchievementsLevel[2] == i && AS.HowManOpen >= AchScr.AchievementChallengeInt[2, i])
            {
                AchievementOpen(AS.AchievementsLevel[2], 2);
                Slider[] ASlider = AchievementPaneler.GetComponentsInChildren<Slider>();
                ASlider[0].value = AS.HowManOpen;
                AS.AchievementsLevel[2] += 1;
                AS.WhatAchievemntHavePrize[2] += 1;
                StartCoroutine(AchievementOpenWithCoroutine(AS.AchievementsLevel[2], 2));
                SaveGovnoEbanoe();
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (AS.AchievementsLevel[3] == i && AS.HowLootBoxOpen >= AchScr.AchievementChallengeInt[3, i])
            {
                AchievementOpen(AS.AchievementsLevel[3], 3);
                Slider[] ASlider = AchievementPaneler.GetComponentsInChildren<Slider>();
                ASlider[0].value = AS.HowLootBoxOpen;
                AS.AchievementsLevel[3] += 1;
                AS.WhatAchievemntHavePrize[3] += 1;
                StartCoroutine(AchievementOpenWithCoroutine(AS.AchievementsLevel[3], 3));
                SaveGovnoEbanoe();
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (AS.AchievementsLevel[6] == i && AS.UpgradeCheck >= AchScr.AchievementChallengeInt[6, i])
            {
                AchievementOpen(AS.AchievementsLevel[6], 6);
                Slider[] ASlider = AchievementPaneler.GetComponentsInChildren<Slider>();
                ASlider[0].value = AS.UpgradeCheck;
                AS.AchievementsLevel[6] += 1;
                AS.WhatAchievemntHavePrize[6] += 1;
                StartCoroutine(AchievementOpenWithCoroutine(AS.AchievementsLevel[6], 6));
                SaveGovnoEbanoe();
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (AS.AchievementsLevel[15] == i && PlayerPrefs.GetInt("Money") >= AchScr.AchievementChallengeInt[15, i])
            {
                AchievementOpen(AS.AchievementsLevel[15], 15);
                Slider[] ASlider = AchievementPaneler.GetComponentsInChildren<Slider>();
                ASlider[0].value = PlayerPrefs.GetInt("Money");
                AS.AchievementsLevel[15] += 1;
                AS.WhatAchievemntHavePrize[15] += 1;
                StartCoroutine(AchievementOpenWithCoroutine(AS.AchievementsLevel[15], 15));
                SaveGovnoEbanoe();
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (AS.AchievementsLevel[16] == i && AS.DataCheck >= AchScr.AchievementChallengeInt[16, i])
            {
                AchievementOpen(AS.AchievementsLevel[16], 16);
                Slider[] ASlider = AchievementPaneler.GetComponentsInChildren<Slider>();
                ASlider[0].value = AS.DataCheck;
                AS.AchievementsLevel[16] += 1;
                AS.WhatAchievemntHavePrize[16] += 1;
                StartCoroutine(AchievementOpenWithCoroutine(AS.AchievementsLevel[16], 16));
                SaveGovnoEbanoe();
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                AS.LanguageMax = 0;
            }
            for (int j = 0; j < 6; j++)
            {
                if (AS.AchievementsLevel[17] == i && AS.LanguageCheck[j] < AchScr.AchievementChallengeInt[17, i])
                {
                    AS.LanguageMax += AS.LanguageCheck[j];
                }
                if (AS.AchievementsLevel[17] == i && AS.LanguageCheck[j] >= AchScr.AchievementChallengeInt[17, i])
                {
                    AS.LanguageMax += AchScr.AchievementChallengeInt[17, i];
                }
            }
            if (AS.AchievementsLevel[17] == i && AS.LanguageMax / 6 >= AchScr.AchievementChallengeInt[17, i])
            {
                AchievementOpen(AS.AchievementsLevel[17], 17);
                Slider[] ASlider = AchievementPaneler.GetComponentsInChildren<Slider>();
                ASlider[0].value = AS.LanguageMax / 6;
                AS.AchievementsLevel[17] += 1;
                AS.WhatAchievemntHavePrize[17] += 1;
                StartCoroutine(AchievementOpenWithCoroutine(AS.AchievementsLevel[17], 17));
                SaveGovnoEbanoe();
            }
        }
        SignalAchievement();
    }

    public void AchievementOpen(int level, int WhatAchiev)
    {
        Image[] APanel = AchievementPaneler.GetComponentsInChildren<Image>();
        Slider[] ASlider = AchievementPaneler.GetComponentsInChildren<Slider>();
        Text[] AText = AchievementPaneler.GetComponentsInChildren<Text>();
        ASlider[0].maxValue = AchScr.AchievementChallengeInt[WhatAchiev, level];
        APanel[1].GetComponent<Image>().sprite = AchScr.AchievementIcon[WhatAchiev];
        AText[1].GetComponent<Text>().text = LanguageSistem.lng.AchievementName[WhatAchiev];
        AText[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge0[WhatAchiev];
        if (level == 1)
        {
            APanel[5].GetComponent<Image>().sprite = AchievementLevelIcon[0];
            AText[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge1[WhatAchiev];
        }
        if (level == 2)
        {
            APanel[5].GetComponent<Image>().sprite = AchievementLevelIcon[0];
            APanel[6].GetComponent<Image>().sprite = AchievementLevelIcon[0];
            AText[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge2[WhatAchiev];
        }
        if (level == 3)
        {
            APanel[5].GetComponent<Image>().sprite = AchievementLevelIcon[0];
            APanel[6].GetComponent<Image>().sprite = AchievementLevelIcon[0];
            APanel[7].GetComponent<Image>().sprite = AchievementLevelIcon[0];
            AText[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge3[WhatAchiev];
        }
        OpenAchievAnim.Play("OpenAchiev");
    }

    IEnumerator AchievementOpenWithCoroutine(int level, int WhatAchiev)
    {
        yield return new WaitForSeconds(1);
        Image[] APanel = AchievementPaneler.GetComponentsInChildren<Image>();
        Slider[] ASlider = AchievementPaneler.GetComponentsInChildren<Slider>();
        Text[] AText = AchievementPaneler.GetComponentsInChildren<Text>();
        ASlider[0].maxValue = AchScr.AchievementChallengeInt[WhatAchiev, level];
        if (level == 1)
        {
            AchievementLevelUp[0].SetActive(true);
            yield return new WaitForSeconds(1);
            AText[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge1[WhatAchiev];
        }
        if (level == 2)
        {
            AchievementLevelUp[1].SetActive(true);
            yield return new WaitForSeconds(1);
            AText[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge2[WhatAchiev];
        }
        if (level == 3)
        {
            AchievementLevelUp[2].SetActive(true);
            yield return new WaitForSeconds(1);
            AText[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge3[WhatAchiev];
        }
        if (level == 4)
        {
            AchievementLevelUp[2].SetActive(true);
            yield return new WaitForSeconds(1);
            AText[0].GetComponent<Text>().text = LanguageSistem.lng.AchievementChallenge3[WhatAchiev];
        }
        yield return new WaitForSeconds(1);
        OpenAchievAnim.Play("CloseAchiev");
        yield return new WaitForSeconds(1);
        AchievementLevelUp[0].SetActive(false);
        AchievementLevelUp[1].SetActive(false);
        AchievementLevelUp[2].SetActive(false);
        AchievementLevelUp[3].SetActive(false);
        APanel[5].GetComponent<Image>().sprite = AchievementLevelIcon[1];
        APanel[6].GetComponent<Image>().sprite = AchievementLevelIcon[1];
        APanel[7].GetComponent<Image>().sprite = AchievementLevelIcon[1];
        APanel[8].GetComponent<Image>().sprite = AchievementLevelIcon[1];
    }
    public void CLickMusic()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            camAudio.volume = 0;
            Music = 0;
            PlayerPrefs.SetInt("Music", Music);
            ButtonSettings[0].sprite = ButtonSettingsOff[0]; ;
            //Manager.MusicInGame.volume = 0f;
        }
        else
        {
            camAudio.volume = 1;
            Music = 1;
            PlayerPrefs.SetInt("Music", Music);
            ButtonSettings[0].sprite = ButtonSettingsOn[0];
        }
    }
    public void CLickSound()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            Sound = 0;
            soundsVolume = 0;
            PlayerPrefs.SetInt("Sound", Sound);
            ButtonSettings[1].sprite = ButtonSettingsOff[1];
        }
        else
        {
            Sound = 1;
            soundsVolume = 1;
            PlayerPrefs.SetInt("Sound", Sound);
            ButtonSettings[1].sprite = ButtonSettingsOn[1];
        }
    }
    public void CLickSave()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        if (PlayerPrefs.GetInt("Save") == 1)
        {
            Save = 0;
            PlayerPrefs.SetInt("Save", Save);
            ButtonSettings[3].sprite = ButtonSettingsOff[3];
        }
        else
        {
            Save = 1;
            PlayerPrefs.SetInt("Save", Save);
            ButtonSettings[3].sprite = ButtonSettingsOn[3];
        }
    }

    public void CLickСall()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        if (PlayerPrefs.GetInt("Call") == 1)
        {
            Call = 0;
            PlayerPrefs.SetInt("Call", Call);
            ButtonSettings[4].sprite = ButtonSettingsOff[4];
        }
        else
        {
            Call = 1;
            PlayerPrefs.SetInt("Call", Call);
            ButtonSettings[4].sprite = ButtonSettingsOn[4];
        }
    }
    public void YouBrazil()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        if (AS.WhatIsLanguage != 0)
        {
            ButtonLanguage[0].sprite = ButtonLanguageOn[0];
            ButtonLanguage[1].sprite = ButtonLanguageOff[1];
            ButtonLanguage[2].sprite = ButtonLanguageOff[2];
            ButtonLanguage[3].sprite = ButtonLanguageOff[3];
            ButtonLanguage[4].sprite = ButtonLanguageOff[4];
            ButtonLanguage[5].sprite = ButtonLanguageOff[5];
            ButtonLanguage[6].sprite = ButtonLanguageOff[6];
            ButtonLanguage[7].sprite = ButtonLanguageOff[7];
            AS.WhatIsLanguage = 0;
            PlayerPrefs.SetString("Language", "pt_PO");
            LanguageSwap();
            PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
        }
    }
    public void YouChina()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        if (AS.WhatIsLanguage != 1)
        {
            ButtonLanguage[1].sprite = ButtonLanguageOn[1];
            ButtonLanguage[0].sprite = ButtonLanguageOff[0];
            ButtonLanguage[2].sprite = ButtonLanguageOff[2];
            ButtonLanguage[3].sprite = ButtonLanguageOff[3];
            ButtonLanguage[4].sprite = ButtonLanguageOff[4];
            ButtonLanguage[5].sprite = ButtonLanguageOff[5];
            ButtonLanguage[6].sprite = ButtonLanguageOff[6];
            ButtonLanguage[7].sprite = ButtonLanguageOff[7];
            AS.WhatIsLanguage = 1;
            PlayerPrefs.SetString("Language", "zh_CH");
            LanguageSwap();
            PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
        }
    }
    public void YouEngland()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        if (AS.WhatIsLanguage != 2)
        {
            ButtonLanguage[2].sprite = ButtonLanguageOn[2];
            ButtonLanguage[0].sprite = ButtonLanguageOff[0];
            ButtonLanguage[1].sprite = ButtonLanguageOff[1];
            ButtonLanguage[3].sprite = ButtonLanguageOff[3];
            ButtonLanguage[4].sprite = ButtonLanguageOff[4];
            ButtonLanguage[5].sprite = ButtonLanguageOff[5];
            ButtonLanguage[6].sprite = ButtonLanguageOff[6];
            ButtonLanguage[7].sprite = ButtonLanguageOff[7];
            AS.WhatIsLanguage = 2;
            PlayerPrefs.SetString("Language", "en_US");
            LanguageSwap();
            PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
        }
    }
    public void YouGermany()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        if (AS.WhatIsLanguage != 3)
        {
            ButtonLanguage[3].sprite = ButtonLanguageOn[3];
            ButtonLanguage[0].sprite = ButtonLanguageOff[0];
            ButtonLanguage[1].sprite = ButtonLanguageOff[1];
            ButtonLanguage[2].sprite = ButtonLanguageOff[2];
            ButtonLanguage[4].sprite = ButtonLanguageOff[4];
            ButtonLanguage[5].sprite = ButtonLanguageOff[5];
            ButtonLanguage[6].sprite = ButtonLanguageOff[6];
            ButtonLanguage[7].sprite = ButtonLanguageOff[7];
            AS.WhatIsLanguage = 3;
            PlayerPrefs.SetString("Language", "de_GE");
            LanguageSwap();
            PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
        }
    }
    public void YouIndia()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        if (AS.WhatIsLanguage != 4)
        {
            ButtonLanguage[4].sprite = ButtonLanguageOn[4];
            ButtonLanguage[0].sprite = ButtonLanguageOff[0];
            ButtonLanguage[1].sprite = ButtonLanguageOff[1];
            ButtonLanguage[2].sprite = ButtonLanguageOff[2];
            ButtonLanguage[3].sprite = ButtonLanguageOff[3];
            ButtonLanguage[5].sprite = ButtonLanguageOff[5];
            ButtonLanguage[6].sprite = ButtonLanguageOff[6];
            ButtonLanguage[7].sprite = ButtonLanguageOff[7];
            AS.WhatIsLanguage = 4;
            PlayerPrefs.SetString("Language", "hi_HI");
            LanguageSwap();
            PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
        }
    }
    public void YouRussia()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        if (AS.WhatIsLanguage != 5)
        {
            ButtonLanguage[5].sprite = ButtonLanguageOn[5];
            ButtonLanguage[0].sprite = ButtonLanguageOff[0];
            ButtonLanguage[1].sprite = ButtonLanguageOff[1];
            ButtonLanguage[2].sprite = ButtonLanguageOff[2];
            ButtonLanguage[3].sprite = ButtonLanguageOff[3];
            ButtonLanguage[4].sprite = ButtonLanguageOff[4];
            ButtonLanguage[6].sprite = ButtonLanguageOff[6];
            ButtonLanguage[7].sprite = ButtonLanguageOff[7];
            AS.WhatIsLanguage = 5;
            PlayerPrefs.SetString("Language", "ru_RU");
            LanguageSwap();
            PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
        }
    }
    public void YouJapanese()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        if (AS.WhatIsLanguage != 6)
        {
            ButtonLanguage[6].sprite = ButtonLanguageOn[6];
            ButtonLanguage[0].sprite = ButtonLanguageOff[0];
            ButtonLanguage[1].sprite = ButtonLanguageOff[1];
            ButtonLanguage[2].sprite = ButtonLanguageOff[2];
            ButtonLanguage[3].sprite = ButtonLanguageOff[3];
            ButtonLanguage[4].sprite = ButtonLanguageOff[4];
            ButtonLanguage[5].sprite = ButtonLanguageOff[5];
            ButtonLanguage[7].sprite = ButtonLanguageOff[7];
            AS.WhatIsLanguage = 6;
            PlayerPrefs.SetString("Language", "ja");
            LanguageSwap();
            PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
        }
    }
    public void YouKorean()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        if (AS.WhatIsLanguage != 7)
        {
            ButtonLanguage[7].sprite = ButtonLanguageOn[7];
            ButtonLanguage[0].sprite = ButtonLanguageOff[0];
            ButtonLanguage[1].sprite = ButtonLanguageOff[1];
            ButtonLanguage[2].sprite = ButtonLanguageOff[2];
            ButtonLanguage[3].sprite = ButtonLanguageOff[3];
            ButtonLanguage[4].sprite = ButtonLanguageOff[4];
            ButtonLanguage[5].sprite = ButtonLanguageOff[5];
            ButtonLanguage[6].sprite = ButtonLanguageOff[6];
            AS.WhatIsLanguage = 7;
            PlayerPrefs.SetString("Language", "ko_KR");
            LanguageSwap();
            PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
        }
    }
    public void LanguageSwap()
    {
        LangSis.LanguageLoad();
        PlayButton.text = LanguageSistem.lng.PlayText;
        MusicSettings.text = LanguageSistem.lng.MusicSettingsText;
        SoundSettings.text = LanguageSistem.lng.SoundSettingsText;
        SaveSettings.text = LanguageSistem.lng.SaveSettingsText;
        CallSettings.text = LanguageSistem.lng.CallSettingsText;
        WorldTableSoon.text = LanguageSistem.lng.SoonText;
        MoneyBaffText.text = AS.MoneyBaff + " " + LanguageSistem.lng.BaffTimeText;
        ScoreBaffText.text = AS.ScoreBaff + " " + LanguageSistem.lng.BaffTimeText;
        for (int i = 0; i < 7; i++)
        {
            UpgradeName[i].text = LanguageSistem.lng.UpgradeNameText[i];
            UpgradeDoIt[i].text = LanguageSistem.lng.UpgradeDoItText[i];
        }
        for (int i = 0; i < 5; i++)
        {
            Titles[i].text = LanguageSistem.lng.TitlesText[i];
            Titles2[i].text = LanguageSistem.lng.TitlesText[i];
            Titles3[i].text = LanguageSistem.lng.TitlesText[i];
            LootInfoText[i].text = LanguageSistem.lng.LootInfoText[i];
        }
        AchScr.AchivementLanguageSwap();
        SS.SwapTextAndImage();

    }
    public void OpenLanguage()
    {
        LanguageMenu.SetActive(true);
    }
    public void CloseLanguage()
    {
        LanguageMenu.SetActive(false);
    }

    public void OpenUpgradePanels(int panels)
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        UpdatePanels[panels].SetActive(true);
        confirm = panels;
    }
    public void CloseUpgradePanels(int panels)
    {
        UpdatePanels[panels].SetActive(false);
        ConfirmMenu.SetActive(false);
        confirm = -1;
    }
    public void OpenUpgradeConfirm()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        ConfirmMenu.SetActive(true);
        CheckUpgrade6Level();
        UpgradeCheckAchiv();
    }
    public void CloseUpgradeConfirm()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        ConfirmMenu.SetActive(false);
    }
    public void UpgradeCheckAchiv()
    {
        AS.UpgradeCheck = 0;
        for (int i = 0; i < 7; i++)
        {
            AS.UpgradeCheck += AS.UpgradeLevel[i];
            PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
        }
    }
    public void BuyUpgrade()
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        if (confirm == 0 && PlayerPrefs.GetInt("Money") >= UpgradePrise[0, AS.UpgradeLevel[0]] && AS.UpgradeLevel[0] < 5)
        {
            BuyMoney = PlayerPrefs.GetInt("Money") - UpgradePrise[0, AS.UpgradeLevel[0]];
            PlayerPrefs.SetInt("Money", BuyMoney);
            moneyText.text = "" + PlayerPrefs.GetInt("Money");
            AS.UpgradeLevel[0] += 1;
            UpgradePriseText[0].text = "" + UpgradePrise[0, AS.UpgradeLevel[0]];
            UpgradeLevel[0].text = "" + AS.UpgradeLevel[0];
            BuyMoney = PlayerPrefs.GetInt("Money");
            PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
            ConfirmMenu.SetActive(false);
            // Debug.Log(AS.UpgradeLevel[0]);
        }
        if (confirm == 1 && PlayerPrefs.GetInt("Money") >= UpgradePrise[1, AS.UpgradeLevel[1]] && AS.UpgradeLevel[1] < 5)
        {
            BuyMoney = PlayerPrefs.GetInt("Money") - UpgradePrise[1, AS.UpgradeLevel[1]];
            PlayerPrefs.SetInt("Money", BuyMoney);
            moneyText.text = "" + PlayerPrefs.GetInt("Money");
            AS.UpgradeLevel[1] += 1;
            UpgradePriseText[1].text = "" + UpgradePrise[1, AS.UpgradeLevel[1]];
            UpgradeLevel[1].text = "" + AS.UpgradeLevel[1];
            BuyMoney = PlayerPrefs.GetInt("Money");
            PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
            ConfirmMenu.SetActive(false);
            // Debug.Log(AS.UpgradeLevel[1]);
        }
        if (confirm == 2 && PlayerPrefs.GetInt("Money") >= UpgradePrise[2, AS.UpgradeLevel[2]] && AS.UpgradeLevel[2] < 5)
        {
            BuyMoney = PlayerPrefs.GetInt("Money") - UpgradePrise[2, AS.UpgradeLevel[2]];
            PlayerPrefs.SetInt("Money", BuyMoney);
            moneyText.text = "" + PlayerPrefs.GetInt("Money");
            AS.UpgradeLevel[2] += 1;
            UpgradePriseText[2].text = "" + UpgradePrise[2, AS.UpgradeLevel[2]];
            UpgradeLevel[2].text = "" + AS.UpgradeLevel[2];
            BuyMoney = PlayerPrefs.GetInt("Money");
            PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
            ConfirmMenu.SetActive(false);
            // Debug.Log(AS.UpgradeLevel[2]);
        }
        if (confirm == 3 && PlayerPrefs.GetInt("Money") >= UpgradePrise[3, AS.UpgradeLevel[3]] && AS.UpgradeLevel[3] < 5)
        {
            BuyMoney = PlayerPrefs.GetInt("Money") - UpgradePrise[3, AS.UpgradeLevel[3]];
            PlayerPrefs.SetInt("Money", BuyMoney);
            moneyText.text = "" + PlayerPrefs.GetInt("Money");
            AS.UpgradeLevel[3] += 1;
            UpgradePriseText[3].text = "" + UpgradePrise[3, AS.UpgradeLevel[3]];
            UpgradeLevel[3].text = "" + AS.UpgradeLevel[3];
            BuyMoney = PlayerPrefs.GetInt("Money");
            PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
            ConfirmMenu.SetActive(false);
            // Debug.Log(AS.UpgradeLevel[3]);
        }
        if (confirm == 4 && PlayerPrefs.GetInt("Money") >= UpgradePrise[4, AS.UpgradeLevel[4]] && AS.UpgradeLevel[4] < 5)
        {
            BuyMoney = PlayerPrefs.GetInt("Money") - UpgradePrise[4, AS.UpgradeLevel[4]];
            PlayerPrefs.SetInt("Money", BuyMoney);
            moneyText.text = "" + PlayerPrefs.GetInt("Money");
            AS.UpgradeLevel[4] += 1;
            UpgradePriseText[4].text = "" + UpgradePrise[4, AS.UpgradeLevel[4]];
            UpgradeLevel[4].text = "" + AS.UpgradeLevel[4];
            BuyMoney = PlayerPrefs.GetInt("Money");
            PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
            ConfirmMenu.SetActive(false);
            // Debug.Log(AS.UpgradeLevel[4]);
        }
        if (confirm == 5 && PlayerPrefs.GetInt("Money") >= UpgradePrise[5, AS.UpgradeLevel[5]] && AS.UpgradeLevel[5] < 5)
        {
            BuyMoney = PlayerPrefs.GetInt("Money") - UpgradePrise[5, AS.UpgradeLevel[5]];
            PlayerPrefs.SetInt("Money", BuyMoney);
            moneyText.text = "" + PlayerPrefs.GetInt("Money");
            AS.UpgradeLevel[5] += 1;
            UpgradePriseText[5].text = "" + UpgradePrise[5, AS.UpgradeLevel[5]];
            UpgradeLevel[5].text = "" + AS.UpgradeLevel[5];
            BuyMoney = PlayerPrefs.GetInt("Money");
            PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
            ConfirmMenu.SetActive(false);
            // Debug.Log(AS.UpgradeLevel[5]);
        }
        if (confirm == 6 && PlayerPrefs.GetInt("Money") >= UpgradePrise[6, AS.UpgradeLevel[6]] && AS.UpgradeLevel[6] < 5)
        {
            BuyMoney = PlayerPrefs.GetInt("Money") - UpgradePrise[6, AS.UpgradeLevel[6]];
            PlayerPrefs.SetInt("Money", BuyMoney);
            moneyText.text = "" + PlayerPrefs.GetInt("Money");
            AS.UpgradeLevel[6] += 1;
            UpgradePriseText[6].text = "" + UpgradePrise[6, AS.UpgradeLevel[6]];
            UpgradeLevel[6].text = "" + AS.UpgradeLevel[6];
            BuyMoney = PlayerPrefs.GetInt("Money");
            PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
            ConfirmMenu.SetActive(false);
            // Debug.Log(AS.UpgradeLevel[6]);
        }
        UpgradeCheckAchiv();
        CheckUpgrade6Level();

    }

    public void CheckUpgrade6Level()
    {
        for (int i = 0; i < 7; i++)
        {
            if (PlayerPrefs.GetInt("Money") < UpgradePrise[i, AS.UpgradeLevel[i]])
            {
                BuyButtonText[i].text = LanguageSistem.lng.UpgradeBuyText[0];
                BuyButton[i].interactable = false;
                BuyButton[i].image.color = NonInteract;
            }
            else
            {
                BuyButtonText[i].text = LanguageSistem.lng.UpgradeBuyText[1];
                BuyButton[i].interactable = true;
                BuyButton[i].image.color = Interact;
            }

            if (AS.UpgradeLevel[i] == 5)
            {
                BuyButtonText[i].text = LanguageSistem.lng.UpgradeBuyText[2];
                PriceUpgrade[i].SetActive(false);
                BuyButton[i].interactable = false;
                BuyButton[i].image.color = NonInteract;
            }
        }
        Upgrade6Level = 0;
        for (int i = 0; i < 6; i++)
        {
            if (AS.UpgradeLevel[i] > AS.UpgradeLevel[6])
            {
                Upgrade6Level += 1;
            }
        }
        if (PlayerPrefs.GetInt("Money") < UpgradePrise[6, AS.UpgradeLevel[6]])
        {
            BuyButtonText[6].text = LanguageSistem.lng.UpgradeBuyText[0];
            BuyButton[6].interactable = false;
            BuyButton[6].image.color = NonInteract;
        }
        else if (Upgrade6Level != 6) 
        {
            BuyButton[6].interactable = false;
            BuyButtonText[6].text = LanguageSistem.lng.UpgradeBuyText[3];
            BuyButton[6].image.color = NonInteract;
        }
        else
        {
            BuyButton[6].interactable = true;
            BuyButtonText[6].text = LanguageSistem.lng.UpgradeBuyText[1];
            BuyButton[6].image.color = Interact;
        }

        if (AS.UpgradeLevel[6] == 5)
        {
            BuyButtonText[6].text = LanguageSistem.lng.UpgradeBuyText[2];
            PriceUpgrade[6].SetActive(false);
            BuyButton[6].interactable = false;
            BuyButton[6].image.color = NonInteract;
        }
        // Debug.Log(Upgrade6Level);
    }
    IEnumerator WorldTableCoron()
    {
        WorldTableMenu.SetActive(true);
        yield return new WaitForSeconds(1);
        WorldTableMenu.SetActive(false);
    }
    public void SaveGovnoEbanoe()
    {
        PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
        AS = JsonUtility.FromJson<AchievementSave>(PlayerPrefs.GetString("AchievementSave"));
        AS.HowManOpen = 0;
        for (int i = 0; i < 40; i++)
        {
            if (AS.SkinBuy[i] == 1) AS.HowManOpen += 1;
        }
    }
    public void OpenCloseLootBoxInfo(int count)
    {
        if (count == 0)
        {
            LootBoxInfo.SetActive(false);
            LootBoxModel.SetActive(true);
            FreeTicketView.SetActive(true);
        }
        if (count == 1) 
        {
            audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
            FreeTicketView.SetActive(false);
            LootBoxInfo.SetActive(true);
            LootBoxModel.SetActive(false);
        }
    }

    public void BuyLootBoxButton(int count)
    {
        audioSource.PlayOneShot(tapSound, PlayerPrefs.GetInt("Sound"));
        if (buttonsTest != null) buttonsTest.checkCurrentButtonsState();
        if (count == 0 && PlayerPrefs.GetInt("Money") >= 15)
        {
            BuyMoney = PlayerPrefs.GetInt("Money") - 15;
            PlayerPrefs.SetInt("Money", BuyMoney);
            moneyText.text = "" + PlayerPrefs.GetInt("Money");
            BuyMoney = PlayerPrefs.GetInt("Money");
        }
        if (count == 1 && PlayerPrefs.GetInt("Gem") >= 1)
        {
            Gem = PlayerPrefs.GetInt("Gem") - 1;
            PlayerPrefs.SetInt("Gem", Gem);
            gemText.text = "" + PlayerPrefs.GetInt("Gem");
            Gem = PlayerPrefs.GetInt("Gem");
        }
        AS.HowLootBoxOpen += 1;
        ManLoot.boxFieldExpand();
    }
    public void LootBoxGetPrize(int num, int count)
    {
        if (num == 4)
        {
            int p = UnityEngine.Random.Range(2, 6);
            BuyMoney = PlayerPrefs.GetInt("Money") + p;
            if (p < 5) LootPrizeText.text = p + " " + LanguageSistem.lng.LootPrizeText[1];
            if (p == 5) LootPrizeText.text = p + " " + LanguageSistem.lng.LootPrizeText[2];
            PlayerPrefs.SetInt("Money", BuyMoney);
            moneyText.text = "" + PlayerPrefs.GetInt("Money");
            BuyMoney = PlayerPrefs.GetInt("Money");
            
        }
        if (num == 3)
        {
            if (count == 0)
            {
                int p = UnityEngine.Random.Range(12, 21);
                BuyMoney = PlayerPrefs.GetInt("Money") + p;
                if (p < 21) LootPrizeText.text = p + " " + LanguageSistem.lng.LootPrizeText[2];
                PlayerPrefs.SetInt("Money", BuyMoney);
                moneyText.text = "" + PlayerPrefs.GetInt("Money");
                BuyMoney = PlayerPrefs.GetInt("Money");
            }
            if (count == 1)
            {
                int p = UnityEngine.Random.Range(1, 3);
                Gem = PlayerPrefs.GetInt("Gem") + p;
                if (p == 1) LootPrizeText.text = p + " " + LanguageSistem.lng.LootPrizeText[3];
                if (p > 1) LootPrizeText.text = p + " " + LanguageSistem.lng.LootPrizeText[4];
                PlayerPrefs.SetInt("Gem", Gem);
                gemText.text = "" + PlayerPrefs.GetInt("Gem");
                Gem = PlayerPrefs.GetInt("Gem");
            }
        }
        if (num == 2)
        {
            if (count == 0)
            {
                AS.MoneyBaff += 13;
                LootPrizeText.text = LanguageSistem.lng.LootPrizeText[6];
                MoneyBaff.SetActive(true);
                MoneyBaffText.text = AS.MoneyBaff + " " + LanguageSistem.lng.BaffTimeText;
            }
            if (count == 1)
            {
                AS.ScoreBaff += 13;
                LootPrizeText.text = LanguageSistem.lng.LootPrizeText[7];
                ScoreBaff.SetActive(true);
                ScoreBaffText.text = AS.ScoreBaff + " " + LanguageSistem.lng.BaffTimeText;

            }
            if (count == 2)
            {
                int p = UnityEngine.Random.Range(5, 10);
                Gem = PlayerPrefs.GetInt("Gem") + p;
                if (p < 18) LootPrizeText.text = p + " " + LanguageSistem.lng.LootPrizeText[5];
                PlayerPrefs.SetInt("Gem", Gem);
                gemText.text = "" + PlayerPrefs.GetInt("Gem");
                Gem = PlayerPrefs.GetInt("Gem");
            }
            if (count == 3)
            {
                int p = UnityEngine.Random.Range(57, 92);
                BuyMoney = PlayerPrefs.GetInt("Money") + p;
                if (p < 92) LootPrizeText.text = p + " " + LanguageSistem.lng.LootPrizeText[2];
                PlayerPrefs.SetInt("Money", BuyMoney);
                moneyText.text = "" + PlayerPrefs.GetInt("Money");
                BuyMoney = PlayerPrefs.GetInt("Money");
            }
        }
        if (num == 1)
        {
            LootPrizeText.text = LanguageSistem.lng.LootPrizeText[8];
            if (count == 0) GetGems();
            if (count == 1) GetGems();
            if (count == 2 && AS.SkinBuy[count] == 0) AS.SkinBuy[count] = 1;
            if (count == 3) GetGems();
            if (count == 4) GetGems();
            if (count == 5 && AS.SkinBuy[count] == 0) AS.SkinBuy[count] = 1;
            if (count == 6) GetGems();
            if (count == 7 && AS.SkinBuy[count] == 0) AS.SkinBuy[count] = 1;
            if (count == 8 && AS.SkinBuy[count] == 0) AS.SkinBuy[count] = 1;
            if (count == 9 && AS.SkinBuy[count] == 0) AS.SkinBuy[count] = 1;
            if (count == 10 && AS.SkinBuy[count] == 0) 
            {
                AS.SkinBuy[count] = 1;
            } 
            SS.ButtonContorol(count);
            SS.changeMaterialObj(SS.skinsMenu[count], SS.classic);

        }
        SaveGovnoEbanoe();
    }

    void GetGems()
    {
        Gem = PlayerPrefs.GetInt("Gem") + 17;
        LootPrizeText.text = 17 + " " + LanguageSistem.lng.LootPrizeText[5];
        PlayerPrefs.SetInt("Gem", Gem);
        gemText.text = "" + PlayerPrefs.GetInt("Gem");
        Gem = PlayerPrefs.GetInt("Gem");
    }

    public void SignalAchievement()
    {
        Dubager.text = "M " + 11;
        SignalCount = 0;
        for (int i = 0; i < 50; i++)
        {
            SignalCount += AS.WhatAchievemntHavePrize[i];
        }
        if (SignalCount > 0)
        {
            SignalCountText.text = "" + SignalCount;
            SignalPoint.SetActive(true);
        }
        else SignalPoint.SetActive(false);
    }

    public void loadStudyScene()
    {
        SceneManager.LoadScene(2);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) 
    {
        if (showResult == ShowResult.Finished)
        {
            adsCheck = 0;
        }
        else adsCheck = 1;
    }
    public void OnUnityAdsReady (string placementId) { }
    public void OnUnityAdsDidError (string message) { }
    public void OnUnityAdsDidStart (string placementId) { }


    public void showLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }
    public void ExitFromGPS()
    {
        PlayGamesPlatform.Instance.SignOut();
    }
}
