using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class Manager : MonoBehaviour
{
    public Animator lightAnim;

    public AudioMixerSnapshot defSound;
    public AudioMixerSnapshot pauseShot;
    public AudioMixerSnapshot Hightpass;

    public AudioSource camAudio;
    public AudioSource audioSource;
    public AudioClip tapSound;

    public GameObject forCamera;
    public Slider slider;
    public GameObject PauseMenu;
    public GameObject ButtonClose;
    public GameObject DeathScreen;
    public Image Headstone;

    public int[] WhatAchievComplited = new int[20] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
    public Image[] AchievComplite;

    public Sprite[] HeadstoneOnOff;

    public bool HeadstoneCheck = false;
    public bool DivergentCheck = false;
    public float score;
    public Text moneyText;
    public Text moneyDeathScreenText;
    public Text scoreDeathScreenText;
    public Text scoreText;
    public Text BestScoreText;
    public Animator HeadStoneAnim;
    public Animator SheildAnim;
    public Animator OilAnim;
    public Animator PutinAnim;
    public Animator NewRecordAnim;
    public InputField InField;

    // public roadBlockScr rBS;
    public GameObject[] prefabs;
    public GameObject[] prefabs2;
    public GameObject[] prefabs3;
    public GameObject road;
    public GameObject Player;
    public GameObject Poo;
    public GameObject CornModel;
    public GameObject moneyInCorn;
    public GameObject moneyFromCorn;
    public GameObject border;
    public GameObject winCoinPrefab;
    public GameObject falseCoinPrefab;

    GameObject Skin;
    public float realMoveSpeed = 0;
    public float MoveSpeed = 15;
	public int corn = 0;
    public int ScoreBaffPoint = 1;
    public bool deathScreenEnable = false;
    public int money;
    public int BussiMoney;
    public int scoreReal = 0;
    public int BestScore = 0;
    public int achieveLevel = 0;
    public int CheckBest = 0;
    public AchievementSave AS = new AchievementSave();
    public AchievementCheckInGame ACIG;
    public poo Po;
    public bike bike;

    public float SizeHeight;
    public float SizeWidth;
    public float ScreenSize;

    public float count = 0;
    public float Uskoreniye = 0.0007f;
    public int CornChance;
    public float moneyDeathScreen = 0;
    public float scoreDeathScreen = 0;
    public float pooSpeed = 0.05f;
    public bool GOVNO = true;
    public bool POPCORN = false;

    public GameObject LoadScreen;
    public GameObject LoadScreenBar;

    public Image LoadBar;
    public Text LoadingText;
    public Text PercentText;

    void Start()
	{
        StartCoroutine(setNightTime());
        // foreach(GameObject x in prefabs) x.transform.Rotate(new Vector3(0, 180, 0));
        // Application.targetFrameRate = 120;
        Application.targetFrameRate = 60;
        AS = JsonUtility.FromJson<AchievementSave>(PlayerPrefs.GetString("AchievementSave"));
        money = 0;
        moneyText.text = "" + money;
        if (AS.ScoreBaff > 0) ScoreBaffPoint = 2;
        audioSource = GetComponent<AudioSource>();
        foreach (int x in AS.AchievementsLevel) achieveLevel += x;
        pooSpeed += achieveLevel / (AS.AchievementsLevel.Length * 4 * 5);
        if (bike.studyScene) pooSpeed = 0f;

        if (PlayerPrefs.GetInt("Music") == 0) camAudio.volume = 0f;
        if (PlayerPrefs.GetInt("Sound") == 0) GetComponent<AudioSource>().volume = 0f;

        SizeHeight = Screen.height;
        SizeWidth = Screen.width;
        ScreenSize = SizeHeight / SizeWidth * 1.0f;
    }

    public void getAwardCoin()
    {
        GameObject winCoin = Instantiate(winCoinPrefab, transform.parent);
        winCoin.GetComponent<winningEffect>().Mng = this;
        winCoin.transform.position = bike.transform.position + new Vector3(0f, 0f, 0f);
        winCoin.transform.parent = moneyFromCorn.transform.parent;
    }

    public void getFalseCoin()
    {
        GameObject falseCoin = Instantiate(falseCoinPrefab, transform.parent);
        falseCoin.GetComponent<winningEffect>().Mng = this;
        falseCoin.GetComponent<winningEffect>().falseCoin = true;
        falseCoin.transform.position = bike.transform.position + new Vector3(0f, 0f, 0f);
        falseCoin.transform.parent = moneyFromCorn.transform.parent;
    }

    public IEnumerator setNightTime()
    {
        yield return new WaitForSeconds(100f);
        lightAnim.Play("nightLight");
        GOVNO = false;

        yield return new WaitForSeconds(30f);

        GOVNO = true;
        lightAnim.Play("dayLight");
        StartCoroutine(setNightTime());
    }

    void Update()
    {
        if (deathScreenEnable && !bike.studyScene)
        {
            moneyDeathScreen = Mathf.Lerp(moneyDeathScreen, money, Time.deltaTime * 3f);
            moneyDeathScreenText.text = "+ " + Mathf.RoundToInt(moneyDeathScreen);

            scoreDeathScreen = Mathf.Lerp(scoreDeathScreen, scoreReal, Time.deltaTime * 3f);
            scoreDeathScreenText.text = "+ " + Mathf.RoundToInt(scoreDeathScreen);
        }
        
        if (realMoveSpeed != MoveSpeed) realMoveSpeed = Mathf.Lerp(realMoveSpeed, MoveSpeed, Time.deltaTime * 3);

        if (Uskoreniye != 0f && MoveSpeed > 30f) Uskoreniye = 0f;
        if (Uskoreniye != 0f && MoveSpeed == 0f) Uskoreniye = 0f;
        else if (Uskoreniye == 0f && MoveSpeed != 0f) Uskoreniye = 0.001f;
        if (scoreReal >= PlayerPrefs.GetInt("Score") && CheckBest == 0 && !bike.studyScene) 
        {
            BestScoreText.gameObject.SetActive(true);
            if (ScreenSize > 2.1f) NewRecordAnim.Play("NewRecordAnim");
            else if ((ScreenSize < 2.1f) && (ScreenSize > 1.9f)) NewRecordAnim.Play("NewRecordAnim 1");
            else if (ScreenSize < 1.9f) NewRecordAnim.Play("NewRecordAnim 2");

            BestScoreText.text = LanguageSistem.lng.DeathScreenText[0];
            CheckBest = 1;
        }
        
        // MoveSpeed *= 1.001f;
        //Debug.Log(scoreReal);
        slider.value = corn;
        if (slider.value == 25)
        {
            // winCoin.GetComponent<winningEffect>().endPos = moneyInCorn.transform.position;
            // Debug.Log(moneyInCorn.transform.position + " == " + moneyInCorn.GetComponent<RectTransform>().position);
            // winCoin.GetComponent<winningEffect>().endPos = new Vector3(-0.14f, -0.14f, 7.63f);
            // winCoin.GetComponent<winningEffect>().endPos = new Vector3(-0.73f, 2.235f, 5.015f);

            getAwardCoin();

            if (AS.MoneyBaff > 0)
            {
                money += 1;
            }
            AS.corn += corn;
            slider.value = 0;
            corn = 0;
            ACIG.CheckAchievement();
        }
        if (!PauseMenu.activeSelf && !DeathScreen.activeSelf)
        {
            MoveSpeed += Uskoreniye;

            if (AS.UpgradeLevel[1] != 0)
            {
                for (int i = 1; i < 6; i++)
                {
                    if (AS.UpgradeLevel[1] == i)
                    {
                        score += 0.03f * MoveSpeed / 10 * (i * i * ScoreBaffPoint) * Time.timeScale;
                    }
                }
                
            }
            else
            {
                score += 0.03f * MoveSpeed / 10 * ScoreBaffPoint * Time.timeScale;
            }
            if (score >= 1f)
            {
                score = 0;
                scoreReal += 1;
                //BestScoreText.text = "" + Mng.BestScore;
                scoreText.text = "" + scoreReal;
            }
        }
        if (scoreReal % 100 == 0) ACIG.CheckAchievement();
        if (DivergentCheck == false && scoreReal > AS.DivergentMax)
        {
            AS.DivergentMax = scoreReal;
        }
        if (bike.IsLazy == false && scoreReal > AS.LazyMax)
        {
            AS.LazyMax = scoreReal;
        }
        if (Application.platform == RuntimePlatform.Android && (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Menu)))
        {
            PauseOn();
        }
    }

	void FixedUpdate()
	{
        if (bike.affectedByDeath && PutinAnim.speed != 0) PutinAnim.speed = 0;
        else if (!bike.affectedByDeath && PutinAnim.speed == 0) PutinAnim.speed = 1;


        if (GOVNO) count += pooSpeed;
        else if (count != 0) count = 0;

        if (count > 15f)
        {
            count = 0;
            makePoo();
        }


        if (POPCORN)
        {
            GameObject block1 = Instantiate(CornModel, transform);
            block1.transform.position = new Vector3(0,
                                                    Player.transform.position.y + 100,
                                                    Player.transform.position.z + Random.Range(50, 55));
        }
    }

    public void AddCoins(int number)
    {
        if (bike.skeletOn) return;
        DivergentCheck = true;
        corn += number;
        if(!(AS.UpgradeLevel[0] == 0))
        {
            for (int i = 1; i < 6; i++)
            {
                CornChance = Random.Range(1, 100);
                if (AS.UpgradeLevel[0] == i && CornChance <=  i * i)
                {
                    corn += number;
                }
            }
        }
    }

    public void makePoo()
    {
        float posZ = Player.transform.position.z;
    	GameObject block = Instantiate(Poo, transform);
        // block.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 10, Player.transform.position.z+1);
        if (bike.studyScene) posZ += 6;
        else posZ += Random.Range(6, 12);
        block.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 20, posZ);
    }

    public void PauseOn()
    {
        audioSource.PlayOneShot(tapSound, 1f);
        bike.Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        pauseShot.TransitionTo(0f);
        Time.timeScale = 0f;
        bike.cantSwap = true;
        bike.affectedByDeath = true;
        PauseMenu.SetActive(true);
        ButtonClose.SetActive(true);
    }
    public void PauseOff()
    {
        audioSource.PlayOneShot(tapSound, 1f);
        bike.Canvas.renderMode = RenderMode.ScreenSpaceCamera;
        PauseMenu.SetActive(false);
        ButtonClose.SetActive(false);
        bike.cantSwap = false;
        bike.affectedByDeath = false;
        Time.timeScale = 1f;
        defSound.TransitionTo(0.5f);
    }
    public void Loadlevel(int button)
    {
        Time.timeScale = 1f;
        scoreReal = 0;
        LoadingText.text = LanguageSistem.lng.LoadingText[Random.Range(0, 15)];
        LoadScreen.SetActive(true);
        LoadScreenBar.SetActive(true);
        StartCoroutine(AsyncLoad(button));
        
    }
    public void restartStudyScene()
    {
        AS.corn += corn;
        AS.LanguageCheck[AS.WhatIsLanguage] += 1;
        if (AS.MoneyBaff != 0) AS.MoneyBaff -= 1;
        if (AS.ScoreBaff != 0) AS.ScoreBaff -= 1;
        PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
    }
    public void RelifeMoney()
    {
        audioSource.PlayOneShot(tapSound, 1f);
        AS.corn += corn;
        AS.LanguageCheck[AS.WhatIsLanguage] += 1;
        if (AS.MoneyBaff != 0) AS.MoneyBaff -= 1;
        if (AS.ScoreBaff != 0) AS.ScoreBaff -= 1;
        PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void RelifeAdvertising()
    {
        audioSource.PlayOneShot(tapSound, 1f);
        AS.corn += corn;
        PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
        bike.DeathScreenTrue.SetActive(false);
        LoadingText.text = LanguageSistem.lng.LoadingText[Random.Range(0, 15)];
        LoadScreen.SetActive(true);
        LoadScreenBar.SetActive(true);
        StartCoroutine(AsyncLoad(0));
        
    }
    public void HeadstoneАctivation()
    {
        HeadStoneAnim.Play("HeadStoneOpen");
    }
    public void HeadstoneShutdown()
    {
        HeadStoneAnim.Play("HeadStoneEnd");
    }
    public void ShieldАctivation()
    {
        SheildAnim.Play("Shield1");
    }
    public void ShiledShutdown()
    {
        SheildAnim.Play("Shield3");
    }
    public void OilАctivation(float count)
    {
        OilAnim.SetFloat("FloatOil", (1f / count));
        OilAnim.Play("Oil1");
    }
    public void OilShutdown()
    {
        OilAnim.Play("Oil2");
    }
    public void PutinUsed(float count)
    {
        // PutinAnim.speed = 0.5f;
        PutinAnim.SetFloat("speedPutin3", (1f / (count - 1)) ); 
        PutinAnim.Play("Putin2");
        // PutinAnim.Play("Putin3");
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
    /*private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
    }*/
    public void SaveAchuevEbanoe()
    {
        PlayerPrefs.SetString("AchievementSave", JsonUtility.ToJson(AS));
    }
}
