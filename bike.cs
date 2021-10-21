using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class bike : MonoBehaviour, IUnityAdsListener
{
    class Constants
    {
        public const int prefabsCount = 15;
    }

    bool Impregnable = false;
    private int adsCheck = 2;

    public GameObject doubleAwardButton;
    public GameObject stunnEffectNimb;

    public GameObject winTab;
    public AudioClip speedFonk;
    public Animator studyAnimPower;
    public Animator studyAnim;
    public GameObject invokerSpheres;

    public Button respawnButton;
    public Text respawnRemaningText;

    public GameObject BusinessRoll;
    public GameObject strelkaBussiness;
    public GameObject forSkorostBuff;
    public GameObject headstoneSpirit;
    public GameObject forFall;
    public GameObject forStunn;
    public GameObject forPop;
    public GameObject forLava;
    public GameObject lava;
    public GameObject forLoseChel;
    public GameObject camera;
    public GameObject explosion;
    public GameObject explosionGovno;
    public GameObject DeathScreenTrue;
    public GameObject DeathScreen;
    public GameObject PopcornFill;
    public GameObject ShieldBlock;
    private GameObject BlockShield;

    public AudioClip eatSound;
    public AudioClip deathSound;
    public AudioClip SoundJump;
    public AudioClip breakSound;
    public AudioClip fallSound;
    public AudioClip[] stepSounds;

    public GameObject SuperPowerIcon;
    public GameObject SuperPowerBar;
    public Slider SuperPowerSlider;

    public GameObject[][] TripleMasive = new GameObject[Constants.prefabsCount][];

    GameObject Chel1;
    Animator an;
    Animator cameraAnim;
    cam cameraScr;
    public GameObject deathZone;
    public GameObject RoadSpawner;

    public GameObject KRAN;
    public GameObject trup;
    public GameObject volna;
    public Animator runnerEffect;
    public roadSpawn RoadSpawnerScr;
    public GameObject objKRAN;

    public Canvas Canvas;
    private bool studyPoo = false;
    private float camSpeed;

    enum causeDeathType
    {
        POP,
        FALL,
        STUNN
    }
    causeDeathType causeDeath;

    int prLaneNumber = 1,
        laneNumber = 1,
        lanesCount = 2;

    public float firstLanePos,
                 laneDistance,
                 SideSpeed;

    public bool studyPromptPower = false;
    public bool finalStudy = false;
    public bool wannaJump = false;
    public bool wannaSlide = false;
    public bool cantSwap = false;
    public bool cantSwapDouble = false;
    public bool valueResp = false;
    public bool IsLazy = false;
    public bool affectedByDeath = false;
    public bool acvivateDoubleJump = false;
    public bool canDoubleJump = true;
    private bool clercOn = false;
    public bool skeletOn = false;
    private bool studyAnimOn = false;
    public bool soundsEffectOff = false;

    public int[] AchievCheckSave = new int[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int DeathShitSave = 0;
    public int SecondChanceSave = 0;
    public int SupermanSave = 0;
    public int respawnRemaning = 3;

    static public float VolnaPover = 1f;

    bool checkParticle = true;
    [SerializeField]
    float superPowerKD = 0;
    [SerializeField]
    float puddle_debaf = 0;
    [SerializeField]
    float putin_baff = 0;
    [SerializeField]
    public float shield_baff = 0;
    [SerializeField]
    float doctor_baff = 0;
    [SerializeField]
    float shreak_Buff = 0;
    [SerializeField]
    float moonMan_Buff = 0;
    [SerializeField]
    public float skorostnoyBuff = 0;
    // [SerializeField]
    float raycastLen = 0.8f;
    bool shreakBuffOn = false;
    float JumpSpeed = 20;
    private RaycastHit hit1;
    private RaycastHit hit2;

    private int roadLayer = 1 << 9;

    private float mass = 5;
    public float forCameraChecY;
    private float cloudMoney = 0f;
    private bool antiGovno = false;
    private Rigidbody rb;
    private Vector3 moveVec;
    private Vector3 scaleChange = new Vector3(+0.08f, +0.08f, +0.08f);
    private Vector3 scaleChangeShreak = new Vector3(+0.03f, +0.03f, +0.03f);
    public Manager Mng;

    public AchievementSave AS = new AchievementSave();
    public AchievementCheckInGame ACIG;
    private int layerMaskWithoutDeathZone = ~( (1 << 15) );
    private int layerMask1 = 1 << 11;
    private int layerMaskObstacle = 1 << 11;
    private int layerMaskCatapult = 1 << 16;
    private int platformMaskCatapult = 1 << 17;
    private int layerMaskPoo = 1 << 10;
    private int[] PassiveOrActiv = new int[40] { 2, 1, 1, 1, 1, 2, 2, 1, 2, 2, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public GameObject particles;
    // private GameObject particles1;

    private ParticleSystem system;
    private Vector3 lerpStart;
    private Vector3 lerpEnd;
    private Vector3 cameraEndVector;

    private Transform child;
    private Transform forCameraTrans;

    private Collider collider;
    public bool studyScene = false;
    private bool alreadyJumpStudy = false;
    private bool adsSupported = false;

    public Text[] DeathText;
    private GameObject[] studyObstacles;
    private GameObject lavaBlock;
    private GameObject invSpheres;
    private AudioSource audioSource;
    private float soundEffectsVolume = 1f;
    private float stepCount = 0f;
    private int skeletPartsRemain = 5;

    public class chelParts
    {
        public GameObject body;
        public GameObject leftArm;
        public GameObject rightArm;
        public GameObject leftFoot;
        public GameObject rightFoot;

        public chelParts(Transform chel)
        {
            // foreach(Transform tr in chel) Debug.Log(tr);
            this.body = chel.Find("body").gameObject;
            this.leftArm = chel.Find("Left arm").gameObject;
            this.rightArm = chel.Find("Right arm").gameObject;
            this.leftFoot = chel.Find("Foots/left foot").gameObject;
            this.rightFoot = chel.Find("Foots/right foot").gameObject;
        }
    }
    private chelParts skeletParts;

    void Awake()
    {
        Mng.defSound.TransitionTo(0f);
        Time.timeScale = 1f;
        if (SceneManager.GetActiveScene().name == "studyScene") studyScene = true;
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("4093675", false);
            adsSupported = true;
            Advertisement.AddListener(this);
        }
    }

    void Start()
    {
        if (studyScene) Mng.pooSpeed = 0.04f;
        AS = JsonUtility.FromJson<AchievementSave>(PlayerPrefs.GetString("AchievementSave"));
        /*Debug.Log(AS.corn + "Ch");
        Debug.Log(AS.DeathShit + "Ch");
        Debug.Log(AS.ProtectionShit + "Ch");
        Debug.Log(AS.WhatIsSkin + "Ch");
        Debug.Log(AS.UpgradeCheck + "Ch");
        Debug.Log(AS.DivergentMax + "Ch");
        Debug.Log(AS.JesusMax + "Ch");
        Debug.Log(AS.LanguageMax + "Ch");
        Debug.Log(AS.СatapultMax + "Ch");
        Debug.Log(AS.LazyMax + "Ch");
        Debug.Log(AS.DataCheck + "Ch");
        Debug.Log(AS.DayCheck + "Ch");
        Debug.Log(AS.LazyMax + "Ch");
        Debug.Log(AS.SupermanMax + "Ch");
        Debug.Log(AS.SecondChanceMax + "Ch");*/
        for (int i = 0; i < Constants.prefabsCount; i++)
        {
            TripleMasive[i] = new GameObject[3] { Mng.prefabs[i], Mng.prefabs2[i], Mng.prefabs3[i] };
        }
        DeathShitSave = AS.DeathShit;
        SecondChanceSave = AS.SecondChanceMax;
        SupermanSave = AS.SupermanMax;
        RoadSpawnerScr = RoadSpawner.GetComponent<roadSpawn>();
        VolnaPover = 1f + (0.2f * AS.UpgradeLevel[1]);

        GetSkin(TripleMasive[AS.WhatIsSkinShoose][AS.SkinStyleNumber]);

        moveVec = new Vector3(0, 0, 1);
        rb = GetComponent<Rigidbody>();
        // Mng = FindObjectOfType<Manager>();
        swapeCntrl.SwipeEvent += CheckInput;

        cameraAnim = camera.GetComponent<Animator>();
        cameraScr = camera.GetComponent<cam>();
        forCameraTrans = camera.transform.parent;
        SuperPowerBar.SetActive(false);
        lerpStart = new Vector3(PopcornFill.transform.localPosition.x, PopcornFill.transform.localPosition.y, PopcornFill.transform.localPosition.z);
        lerpEnd = new Vector3(PopcornFill.transform.localPosition.x, PopcornFill.transform.localPosition.y + 20f, PopcornFill.transform.localPosition.z);

        if (AS.WhatIsSkinShoose == 0) 
        {            
            SuperPowerIcon.SetActive(false);
            SuperPowerBar.SetActive(false);
        }
        if (AS.WhatIsSkinShoose == 4)
        {
            // GetComponent<AudioSource>().Play();
            // Mng.GetComponent<AudioSource>().clip = speedFonk;
            // Mng.GetComponent<AudioSource>().Play();

            // IEnumerator startFonk()
            // {
            //     yield return new WaitForSeconds(12.2f);
            //     GetRunnerBaff();
            // }
            // StartCoroutine(startFonk());
        }
        if (AS.WhatIsSkinShoose == 5) 
        {
            // clercOn = true;
            skeletOn = true;
            skeletParts = new chelParts(Chel1.transform);

            SuperPowerIcon.SetActive(false);
            SuperPowerBar.SetActive(false);
        }
        else if (AS.WhatIsSkinShoose == 7)
        {
            invSpheres = Instantiate(invokerSpheres, transform);
            StartCoroutine(enableSpheres());
        }
        else if (AS.WhatIsSkinShoose == 8)
        {
            acvivateDoubleJump = true;
            SuperPowerIcon.SetActive(false);
            SuperPowerBar.SetActive(false);
        }
        else if (AS.WhatIsSkinShoose == 9) 
        {
            shreakBuffOn = true;
            SuperPowerIcon.SetActive(false);
            SuperPowerBar.SetActive(false);
        } 
        collider = GetComponent<BoxCollider>();

        if (studyScene) studyObstacles = RoadSpawnerScr.studyBlockScr.obstaclesStudy;

        if (!studyScene)
        {
            for(int j = 0; j < 3; j++)
            {
                for(int i = 0; i < 1; i++)
                {
                    lavaBlock = Instantiate(lava, forLava.transform);
                    lavaBlock.transform.localPosition = new Vector3(0.48f * i, 0f, 0.48f * j);
                }
            }

            // forLava.transform.position = new Vector3(-17f, -15f, 1f);
            forLava.transform.localScale = new Vector3(13.1f * 5f / 1f, 18f * 5f / 1f, 20f * 5f / 1f);
        }

        audioSource = GetComponent<AudioSource>();

        if (PassiveOrActiv[AS.WhatIsSkinShoose] == 1)
        {
            superPowerKD = 15f;
            Mng.PutinUsed(15f);
        }

        if (PlayerPrefs.GetInt("Sound") == 0) soundEffectsVolume = 0; 
    }

    void Update()
    {
        if (studyScene && !affectedByDeath)
        {
            if (!finalStudy && transform.position.z > studyObstacles[7].transform.position.z)
            {
                finalStudy = true;
                StartCoroutine(makePooStudy());
            }

            if (studyObstacles[0].transform.position.z - transform.position.z < 20f && 
                studyObstacles[0].transform.position.z - transform.position.z > 0f) 
            {
                if (laneNumber == 1)
                {
                    if (!studyAnimOn) 
                    {
                        studyAnimOn = true;
                        studyAnim.Play("turnOnRight");
                    }
                    Time.timeScale = Mathf.Lerp(0f, 1f, (studyObstacles[0].transform.position.z - transform.position.z - 3f) / 30f);
                }
                else 
                {
                    studyAnimOn = false;
                    Time.timeScale = 1f;
                    studyAnim.Play("def");
                }
            }
            if (studyObstacles[1].transform.position.z - transform.position.z < 20f && 
                studyObstacles[1].transform.position.z - transform.position.z > 0f) 
            {
                if (laneNumber == 2)
                {
                    if (!studyAnimOn) 
                    {
                        studyAnimOn = true;
                        studyAnim.Play("turnOnLeft");
                    }
                    Time.timeScale = Mathf.Lerp(0f, 1f, (studyObstacles[1].transform.position.z - transform.position.z - 3f) / 30f);
                }
                else
                {
                    studyAnimOn = false;
                    Time.timeScale = 1f;
                    studyAnim.Play("def");
                }
            }
            if (studyObstacles[2].transform.position.z - transform.position.z < 10.5f)
            {
                if (studyObstacles[2].transform.position.z - transform.position.z > 10f) alreadyJumpStudy = false;
                if (studyObstacles[2].transform.position.z - transform.position.z < 10f && 
                    studyObstacles[2].transform.position.z - transform.position.z > 0f) 
                {
                    if (!alreadyJumpStudy)
                    {
                        if (!studyAnimOn) 
                        {
                            studyAnimOn = true;
                            studyAnim.Play("turnOnUp");
                        }
                        Time.timeScale = Mathf.Lerp(0f, 1f, (studyObstacles[2].transform.position.z - transform.position.z - 3f) / 25f);
                    }
                    else
                    {
                        studyAnimOn = false;
                        Time.timeScale = 1f;
                        studyAnim.Play("def");
                    }
                }
            }
            if (studyObstacles[3].transform.position.z - transform.position.z < 10.5f)
            {
                Mng.pooSpeed = 0.05f;
                if (studyObstacles[3].transform.position.z - transform.position.z > 10f) alreadyJumpStudy = false;

                if (studyObstacles[3].transform.position.z - transform.position.z < 10f && 
                    studyObstacles[3].transform.position.z - transform.position.z > 0f) 
                {
                    if (!alreadyJumpStudy)
                    {
                        if (!studyAnimOn) 
                        {
                            studyAnimOn = true;
                            studyAnim.Play("turnOnUp");
                        }
                        Time.timeScale = Mathf.Lerp(0f, 1f, (studyObstacles[3].transform.position.z - transform.position.z - 3f) / 25f);
                    }
                    else
                    {
                        studyAnimOn = false;
                        Time.timeScale = 1f;
                        studyAnim.Play("def");
                    }
                }
            }
            if (studyObstacles[4].transform.position.z - transform.position.z < 7f && 
                studyObstacles[4].transform.position.z - transform.position.z > 0f) 
            {
                if (!an.GetCurrentAnimatorStateInfo(0).IsName("chel_slide") && !an.GetCurrentAnimatorStateInfo(0).IsName("chel_slide2") && !wannaSlide)
                {
                    if (!studyAnimOn) 
                    {
                        studyAnimOn = true;
                        studyAnim.Play("turnOnDown");
                    }
                    Time.timeScale = Mathf.Lerp(0f, 1f, (studyObstacles[4].transform.position.z - transform.position.z - 3f) / 25f);
                }
                else
                {
                    studyAnimOn = false;
                    Time.timeScale = 1f;
                    studyAnim.Play("def");
                }
            }      
            if (studyObstacles[5].transform.position.z - transform.position.z < 7f && 
                studyObstacles[5].transform.position.z - transform.position.z > 0f) 
            {
                if (!an.GetCurrentAnimatorStateInfo(0).IsName("chel_slide") && !an.GetCurrentAnimatorStateInfo(0).IsName("chel_slide2") && !wannaSlide)
                {
                    if (!studyAnimOn) 
                    {
                        studyAnimOn = true;
                        studyAnim.Play("turnOnDown");
                    }
                    Time.timeScale = Mathf.Lerp(0f, 1f, (studyObstacles[5].transform.position.z - transform.position.z - 3f) / 25f);
                }
                else
                {
                    studyAnimOn = false;
                    Time.timeScale = 1f;
                    studyAnim.Play("def");
                }
            }
            if (studyObstacles[6].transform.position.z - transform.position.z < -10f)
            {
                if (laneNumber != 1)
                {
                    if (!studyAnimOn) 
                    {
                        if (laneNumber == 0)
                        {
                            studyAnimOn = true;
                            studyAnim.Play("turnOnRight");
                        }
                        else if (laneNumber == 2)
                        {
                            studyAnimOn = true;
                            studyAnim.Play("turnOnLeft");
                        }
                    }
                    Time.timeScale = 0.2f;
                }
                else
                {
                    studyAnimOn = false;
                    Time.timeScale = 1f;
                    studyAnim.Play("def");
                }
            }
        }
        if (finalStudy && !affectedByDeath)
        {
            Time.timeScale = 1f;
            studyAnim.Play("def");
        }
        Debug.DrawLine(transform.position, transform.position + new Vector3(0, 0, 0.6f), Color.red);
        if (PopcornFill != null) PopcornFill.transform.localPosition = Vector3.Lerp(lerpStart, lerpEnd, Mng.corn/24f);
        if (!affectedByDeath)
        {
            if (cloudMoney > 1)
            {
                cloudMoney = 0;
                Mng.AddCoins(1);
            }
            if (skorostnoyBuff > 0)
            {
                SuperPowerSlider.value = skorostnoyBuff;
                if (skorostnoyBuff > 0.5f)
                {
                    skorostnoyBuff -= 0.01666f;
                }
                if (skorostnoyBuff < 1 && isGrounded())
                {
                    makeObstaclesTrigger(false);
                    runnerEffect.Play("default");
                    cameraScr.elapsed = 10000;
                    cameraAnim.Play("skorostBuffOff"); 
                    skorostnoyBuff = 0;
                    Mng.MoveSpeed = 15f;
                    superPowerKD += 25 - (2 * Mng.AS.UpgradeLevel[6]);
                    SuperPowerBar.SetActive(false);
                    Mng.PutinUsed(superPowerKD);
                }
            }
            if (putin_baff > 0)
            {
                putin_baff -= 0.01666f;
                SuperPowerSlider.value = putin_baff;
                if (putin_baff < 1)
                {
                    transform.localScale -= scaleChange;
                    raycastLen = 0.8f;
                    rb.mass = 1f;
                    putin_baff = 0;
                    superPowerKD += 45 - (4 * Mng.AS.UpgradeLevel[6]);
                    SuperPowerBar.SetActive(false);
                    Mng.PutinUsed(superPowerKD);
                }
            }
            if (shield_baff > 0)
            {
                //shield_baff -= 0.01f;
                if (shield_baff < 1)
                {
                    // Destroy(BlockShield);
                    GameObject shield1 = BlockShield.transform.Find("Shield1").gameObject;
                    shield1.transform.parent = null;
                    shield1.GetComponent<Rigidbody>().isKinematic = false;
                    // shield1.GetComponent<BoxCollider>().enabled = true;
                    shield1.GetComponent<Rigidbody>().AddForce(new Vector3(1, 5f, 0f), ForceMode.VelocityChange);

                    GameObject shield2 = BlockShield.transform.Find("Shield2").gameObject;

                    shield2.transform.parent = null;
                    shield2.GetComponent<Rigidbody>().isKinematic = false;
                    // shield2.GetComponent<BoxCollider>().enabled = true;
                    shield2.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 5f, 0f), ForceMode.VelocityChange);
                    Destroy(BlockShield);
                    antiGovno = false;
                    shield_baff = 0;
                    Mng.ShiledShutdown();
                }
            }

            if (shreak_Buff > 0)
            {
                if (shreak_Buff > 0.5f) 
                {
                    shreak_Buff -= 0.01666f;
                    SuperPowerSlider.value = shreak_Buff;
                }
               
                if (shreak_Buff < 1 && isGrounded())
                {
                    shreak_Buff = 0;
                    Mng.MoveSpeed = 15f;
                    raycastLen = 0.8f;
                    transform.localScale -= scaleChangeShreak;
                    JumpSpeed = 20f;
                    SuperPowerBar.SetActive(false);
                    SuperPowerIcon.SetActive(false);
                }
            }

            if (moonMan_Buff > 0)
            {
                moonMan_Buff -= 0.01666f;
                SuperPowerSlider.value = moonMan_Buff;
                if (moonMan_Buff < 1)
                {
                    moonMan_Buff = 0;
                    mass = 5f;
                    superPowerKD += 45f - (3 * Mng.AS.UpgradeLevel[6]);
                    Mng.PutinUsed(superPowerKD);
                    SuperPowerBar.SetActive(false);
                }
            }

            /*if (superPowerKD > 0)
            {
                superPowerKD -= 0.01666f;
                if (superPowerKD < 1)
                {
                    Mng.PutinAnim.speed = 1f;
                    superPowerKD = 0;
                }
                Debug.Log(superPowerKD);
            }*/
        }

        // Debug.Log(superPowerKD);
        Vector3 newPos = transform.position;
        newPos.x = Mathf.Lerp(newPos.x, firstLanePos + (laneNumber * laneDistance), Time.deltaTime * SideSpeed);
        transform.position = newPos;

        bool rayCheckUp = Physics.Raycast(transform.position, Vector3.up, out hit1, 5f, roadLayer);
        cameraEndVector = cameraScr.checkObstacleUpper(transform.position + new Vector3(0f, 5f, -8f));

        if (rayCheckUp && hit1.point.y < cameraEndVector.y) 
        {
            cameraEndVector = new Vector3(cameraEndVector.x, hit1.point.y, cameraEndVector.z);
            camSpeed = 10f;
        }
        else camSpeed = 3.5f;
        if (cameraEndVector.y < -10f) cameraEndVector = new Vector3(cameraEndVector.x, -9f, cameraEndVector.z);
        forCameraTrans.position = Vector3.Lerp(forCameraTrans.position, cameraEndVector, Time.deltaTime * camSpeed);
    }

    void forCameraUpCheck()
    {
        collider.ClosestPoint(Vector3.up);
        // Debug.Log(collider.ClosestPoint(Vector3.up));
    }

    void forceJump(float JSpeed)
    {
        Mng.audioSource.PlayOneShot(SoundJump, soundEffectsVolume); 
        rb.Sleep();
        // transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rb.AddForce(new Vector3(0, JSpeed, 0), ForceMode.VelocityChange);
    }

    void FixedUpdate()
    {
        if (catapultInFront()) GetCatapulted();
        if (platformUnderChel()) forceJump(40);

        if (stepCount > 1)
        {
            stepCount = 0f;
            audioSource.PlayOneShot(stepSounds[Random.Range(0, 6)], soundEffectsVolume);
        }
        if (isGrounded() && !affectedByDeath) stepCount += 0.09f;
        if (studyScene && !studyPoo && !affectedByDeath)
        {
            bool rayPoo = (Physics.Raycast(transform.position, new Vector3(0f, 0f, 1f), out hit1, 10f, layerMaskPoo));
            if (rayPoo && hit1.distance > 1f)
            {
                studyPoo = true;
                Time.timeScale = 0.1f;
                if (!studyAnimOn) 
                {
                    studyAnimOn = true;
                    studyAnim.Play("turnOnUp");
                }      
            }
        }

        rb.AddForce(new Vector3(0, Physics.gravity.y * mass, 0), ForceMode.Acceleration);

        if (!isGrounded() && checkParticle == true)
        {
            system.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            checkParticle = false;
        }
        else if (isGrounded() && checkParticle == false)
        {
            if (an.GetCurrentAnimatorStateInfo(0).IsName("flyingChel")) an.Play("chel_run");
            if (acvivateDoubleJump) canDoubleJump = true;
            checkParticle = true;
            system.Play();
            audioSource.PlayOneShot(fallSound, soundEffectsVolume);
        }

        if (superPowerKD > 0 && !affectedByDeath)
        {
            superPowerKD -= 0.02f;
            if (superPowerKD < 1)
            {
                if (Mng.AS.ItsYourFirstSkill == 0)
                {
                    studyPromptPower = true;
                    Time.timeScale = 0.2f;
                    if (!studyScene) studyAnimPower.Play("studyPower");
                    Mng.AS.ItsYourFirstSkill = 1;
                }
                if (Mng.AS.WhatIsSkinShoose == 7) StartCoroutine(enableSpheres());
                Mng.PutinAnim.speed = 1f;
                superPowerKD = 0;
            }
        }
        if (puddle_debaf > 0 && !affectedByDeath)
        {
            puddle_debaf -= 0.01666f;
            if (puddle_debaf < 1)
            {
                // cameraAnim.Play("farViewAnimEnd");
                rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY |
                                 RigidbodyConstraints.FreezeRotationX;
                transform.rotation = Quaternion.identity;
                // transform.rotation = startRotation;

                puddle_debaf = 0;
                Mng.OilShutdown();
                SideSpeed = 8.12f;
            }
        }
        if (wannaJump && !affectedByDeath)
        {
            if (isGrounded())
            {
                wannaJump = false;
                forceJump(JumpSpeed);
                an.Play("chel_jump");
            }
            else if (acvivateDoubleJump ? canDoubleJump : false)
            {
                canDoubleJump = false;
                wannaJump = false;
                forceJump(JumpSpeed);
                an.Play("doubleJumpChel");
            }
        }
        if (wannaSlide && !affectedByDeath && an != null)
        {
            if (!an.GetCurrentAnimatorStateInfo(0).IsName("chel_slide") && !an.GetCurrentAnimatorStateInfo(0).IsName("chel_slide2"))
            {
                rb.AddForce(new Vector3(0, JumpSpeed * -1, 0), ForceMode.Impulse);
                an.Play("chel_slide");
                wannaSlide = false;
            }
        }
    }

    void CheckInput(swapeCntrl.SwipeType type)
    {
        if (IsLazy == false) ACIG.CheckAchievement();
        IsLazy = true;
        if (an == null) return;
        if (studyScene && studyPoo) 
        {
            studyPoo = false;
            studyAnimOn = false;
            Time.timeScale = 1f;
            studyAnim.Play("def");
        }
        if (type == swapeCntrl.SwipeType.UP && (!an.GetCurrentAnimatorStateInfo(0).IsName("chel_jump") || canDoubleJump))
        {
            alreadyJumpStudy = true;
            wannaJump = true;
            // wannaSlide = false;
        }
        else if (type == swapeCntrl.SwipeType.DOWN && !an.GetCurrentAnimatorStateInfo(0).IsName("chel_slide"))
        {
            wannaSlide = true;
        }

        if (cantSwap) return;

        if (type == swapeCntrl.SwipeType.DOUBLECLICK)
        {
            if (superPowerKD != 0 || putin_baff != 0 || skorostnoyBuff != 0 || moonMan_Buff != 0) return;
            else 
            {
                if (studyPromptPower) 
                {
                    studyPromptPower = false;
                    Time.timeScale = 1f;
                    studyAnimPower.Play("def");
                }
                GetSuperPower();
                return;
            }
        }

        if (cantSwapDouble) return;

        int sign = 0;
        if (type == swapeCntrl.SwipeType.LEFT)
        {
            Mng.audioSource.PlayOneShot(SoundJump, soundEffectsVolume);
            if (!an.GetCurrentAnimatorStateInfo(0).IsName("chel_jump") && !an.GetCurrentAnimatorStateInfo(0).IsName("flyingChel") &&
                !an.GetCurrentAnimatorStateInfo(0).IsName("chel_jump2") && !an.GetCurrentAnimatorStateInfo(0).IsName("doubleJumpChel"))
                if (an.GetCurrentAnimatorStateInfo(0).IsName("chel_slide") ||
                    an.GetCurrentAnimatorStateInfo(0).IsName("chel_slide2")) an.Play("chel_leftSl");
                else an.Play("chel_left");

            sign = -1;
        }
        else if (type == swapeCntrl.SwipeType.RIGHT)
        {
            Mng.audioSource.PlayOneShot(SoundJump, soundEffectsVolume);
            if (!an.GetCurrentAnimatorStateInfo(0).IsName("chel_jump") && !an.GetCurrentAnimatorStateInfo(0).IsName("flyingChel") &&
                !an.GetCurrentAnimatorStateInfo(0).IsName("chel_jump2") && !an.GetCurrentAnimatorStateInfo(0).IsName("doubleJumpChel"))
                if (an.GetCurrentAnimatorStateInfo(0).IsName("chel_slide") ||
                    an.GetCurrentAnimatorStateInfo(0).IsName("chel_slide2")) an.Play("chel_rightSl");
                else an.Play("chel_right");

            sign = 1;
        }
        else return;

        prLaneNumber = laneNumber;
        laneNumber += sign;
        laneNumber = Mathf.Clamp(laneNumber, 0, lanesCount);
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, raycastLen);
    }
    bool obstacleInFront()
    {
        return Physics.Raycast(transform.position + new Vector3(0, -0.3f, 0), new Vector3(0, 0, 1), 0.8f, layerMaskObstacle);
    }
    bool catapultInFront()
    {
        return Physics.Raycast(transform.position + new Vector3(0, -0.3f, 0), new Vector3(0, 0, 1), 2f, layerMaskCatapult);
    }
    bool platformUnderChel()
    {
        return Physics.Raycast(transform.position, Vector3.down, 2f, platformMaskCatapult);
    }
    bool obstacleInTheSide()
    {
        return (Physics.Raycast(transform.position + new Vector3(0, -0.3f, 0), new Vector3(1, 0, 0), 0.8f, layerMaskObstacle)
             || Physics.Raycast(transform.position + new Vector3(0, -0.3f, 0), new Vector3(-1, 0, 0), 0.8f, layerMaskObstacle) );
    }

    // bool isGrounded1()
    // {
    //     return Physics.Raycast(transform.position, Vector3.down, 0.8f, layerMask = layerMaskWithoutDeathZone);
    // }

    void GetCoin(Collision other)
    {
        if (!an.GetCurrentAnimatorStateInfo(0).IsName("chel_slide") && !an.GetCurrentAnimatorStateInfo(0).IsName("chel_slide2")) return;
        Mng.AddCoins(1);
        Mng.audioSource.PlayOneShot(eatSound, soundsEffectOff ? 0 : 0.4f);
        Destroy(other.gameObject);
        if (PopcornFill != null)
            PopcornFill.transform.position = new Vector3(PopcornFill.transform.position.x, PopcornFill.transform.position.y + 0.027f, PopcornFill.transform.position.z);
    }

    void StackInPuddle()
    {
        rb.constraints = RigidbodyConstraints.None;
        SideSpeed = 1.5f;
        if (!(Mng.AS.UpgradeLevel[2] == 0))
        {
            for (int i = 1; i < 6; i++)
            {
                if (Mng.AS.UpgradeLevel[2] == i)
                {
                    puddle_debaf = 10 - i;
                }
            }
        }
        else
        {
            puddle_debaf = 10;
        }
        Mng.OilАctivation(puddle_debaf - 1);
        ACIG.CheckAchievement();
        // cameraAnim.Play("farViewAnim");
    }

    void buffsDeactivation()
    {
        if (valueResp)
        {
            valueResp = false;
            Mng.HeadstoneShutdown();
        }

        if (puddle_debaf > 1) 
        {
            puddle_debaf = 0.5f;
            Mng.OilShutdown();
        }
        if (shield_baff > 1) 
        {
            shield_baff = 0.5f;
            Mng.ShiledShutdown();
        }
        if (moonMan_Buff > 1) moonMan_Buff = 0.2f;
        if (skorostnoyBuff > 1) 
        {
            makeObstaclesTrigger(false);
            runnerEffect.Play("default");
            cameraScr.elapsed = 10000;
            cameraAnim.Play("skorostBuffOff"); 
            skorostnoyBuff = 0;
            Mng.MoveSpeed = 15f;
            superPowerKD += 25 - (2 * Mng.AS.UpgradeLevel[6]);
            SuperPowerBar.SetActive(false);
            Mng.PutinUsed(superPowerKD);        
        }
        if (putin_baff > 1) putin_baff = 0.2f;
        if (shreak_Buff > 1)
        {
            shreak_Buff = 0;
            Mng.MoveSpeed = 15f;
            raycastLen = 0.8f;
            transform.localScale -= scaleChangeShreak;
            JumpSpeed = 20f;
            SuperPowerBar.SetActive(false);
            SuperPowerIcon.SetActive(false);
        }
    }

    IEnumerator buyBackOffer()
    {
        if (studyScene) TrueDeath();
        DeathText[0].text = LanguageSistem.lng.DeathScreenText[1];
        affectedByDeath = true;
        system.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        Mng.Hightpass.TransitionTo(3f);
        Mng.MoveSpeed = 0;
        Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        buffsDeactivation();
        Mng.GOVNO = false;
        DeathScreen.SetActive(true);

        if (respawnRemaning == 0 || !Advertisement.IsReady()) respawnButton.interactable = false;
        respawnRemaningText.text = respawnRemaning + "x";

        yield return new WaitForSeconds(5);
        TrueDeath();
    }

    void TrueDeath()
    {
        if (!Advertisement.IsReady() || !adsSupported) doubleAwardButton.SetActive(false);
        DeathText[1].text = LanguageSistem.lng.DeathScreenText[2];
        DeathText[2].text = LanguageSistem.lng.DeathScreenText[3];
        DeathText[3].text = LanguageSistem.lng.DeathScreenText[4];
        affectedByDeath = true;
        Time.timeScale = 1f;
        if (studyScene) studyAnim.Play("def");

        StopAllCoroutines();
        buffsDeactivation();

        if (studyScene && finalStudy)
        {
            DeathText[3].text = LanguageSistem.lng.DeathScreenText[5];
            Mng.AS.ItsYourFirstTry = 1;
            DeathScreen.SetActive(false);
            winTab.SetActive(true);
            DeathScreenTrue.SetActive(true);
            
            
        }
        else
        {
            Mng.Hightpass.TransitionTo(3f);
            rb.isKinematic = true;
            RoadSpawnerScr.transform.gameObject.SetActive(false);

            for (int i = 0; i < 14; i++)
            {
                if (Mng.WhatAchievComplited[i] != -1)
                {
                    Mng.AchievComplite[i].sprite = ACIG.AchievementIcon[Mng.WhatAchievComplited[i]];
                }
            }
            Canvas.renderMode = RenderMode.ScreenSpaceCamera;
            DeathScreen.SetActive(false);
            DeathScreenTrue.SetActive(true);

            if (!studyScene)
            {
                GameObject loseChel = Instantiate(TripleMasive[AS.WhatIsSkinShoose][AS.SkinStyleNumber], forLoseChel.transform);
                changeLayer(loseChel, LayerMask.NameToLayer("shopChel"));

                if (causeDeath == causeDeathType.POP) 
                {
                    forPop.SetActive(true);
                    loseChel.GetComponent<Animator>().Play("chel_run_lose");
                }
                else if (causeDeath == causeDeathType.STUNN) 
                {
                    forStunn.SetActive(true);
                    GameObject stunnEffect = Instantiate(stunnEffectNimb, loseChel.transform);
                    stunnEffect.transform.parent = loseChel.transform.Find("body");
                    loseChel.GetComponent<Animator>().Play("stunnDeath");
                }
                else 
                {
                    forFall.SetActive(true);
                    loseChel.GetComponent<Animator>().Play("loseThrowAnim");
                }
            }

            Mng.deathScreenEnable = true;
            PlayerPrefs.SetInt("Money", Mng.money + PlayerPrefs.GetInt("Money"));
            if (Mng.scoreReal >= PlayerPrefs.GetInt("Score", Mng.scoreReal))
            {
                PlayerPrefs.SetInt("Score", Mng.scoreReal);
            }
            for (int i = 0; i < 20; i++)
            {
                if (AchievCheckSave[i] != 0)
                {
                    Mng.AS.AchievementsLevel[i] = AchievCheckSave[i];
                }
            }
            Mng.AS.JesusMax = poo.JesusMa;
            poo.JesusCheck = false;
            ACIG.CheckAchievement();
        }
    }
    public void doubleAward()
    {
        StartCoroutine(doubleAwardCor());
    }
    private IEnumerator doubleAwardCor()
    {
        Time.timeScale = 0f;
        Advertisement.Show("rewardedVideo");

        while(adsCheck == 2)
        {
            yield return null;
        }
        Time.timeScale = 1f;

        Mng.AS.HowWatch += 1;
        if (adsCheck == 0)
        {
            adsCheck = 2;
            PlayerPrefs.SetInt("Money", Mng.money + PlayerPrefs.GetInt("Money"));
            if (Mng.scoreReal * 2 >= PlayerPrefs.GetInt("Score"))
            {
                PlayerPrefs.SetInt("Score", Mng.scoreReal * 2);
            }
            Mng.scoreReal *= 2;
            Mng.money *= 2;
        }
        doubleAwardButton.SetActive(false);
    }

    public void GoNextGame()
    {
        TrueDeath();
    }
    IEnumerator DeathWithCoroutine()
    {
        affectedByDeath = true;
        Time.timeScale = 1f;
        system.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        Canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        buffsDeactivation();

        Mng.audioSource.PlayOneShot(deathSound, soundEffectsVolume);
        Mng.MoveSpeed = 0;
        if (clercOn) 
        {
            SuperPowerIcon.SetActive(true);
            cantSwapDouble = true;
        } 
        else cantSwap = true;
        Mng.GOVNO = false;
        an.Play("death");
        yield return new WaitForSeconds(3);

        if (studyScene) TrueDeath();
        else StartCoroutine(buyBackOffer());
    }

    IEnumerator RespWithCoroutine()
    {
        affectedByDeath = true;
        valueResp = false;
        if (studyScene) StartCoroutine(makePooStudy());
        finalStudy = true;
        system.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        if (puddle_debaf > 1) puddle_debaf = 0.5f;
        Mng.audioSource.PlayOneShot(deathSound, soundEffectsVolume);

        Mng.MoveSpeed = 0;
        cantSwap = true;
        GameObject hdSpirit = Instantiate(headstoneSpirit, null);
        hdSpirit.transform.position = transform.position + new Vector3(0f, 0f, -5f);
        an.Play("deathResp");

        rb.AddForce(new Vector3(0, 15f, -30f), ForceMode.VelocityChange);

        yield return new WaitForSeconds(2);
        Destroy(hdSpirit);
        Mng.MoveSpeed = 15f;
        cantSwap = false;
        system.Play();
        affectedByDeath = false;
        ACIG.CheckAchievement();
    }

    public IEnumerator BackToLife()
    {
        Canvas.renderMode = RenderMode.ScreenSpaceCamera;

        Time.timeScale = 0f;
        Advertisement.Show("video");

        while(adsCheck == 2)
        {
            yield return null;
        }
        Time.timeScale = 1f;

        if (adsCheck == 1) TrueDeath();
        else adsCheck = 2;

        Mng.AS.HowWatch += 1;
        superPowerKD = 0;
        Mng.PutinAnim.Play("Putin1");

        DeathScreen.SetActive(false);
        GameObject trupChel = Instantiate(trup);
        trupChel.GetComponent<trupChela>().bk = this;
        trupChel.transform.position = transform.position + new Vector3(0f, -0.4f, 0f);
        trupChel.transform.parent = RoadSpawnerScr.CurrBlocs[2].transform;
        // trupChel.transform.parent = null;
        // trupChel.transform.localScale = new Vector3(1f, 1f, 1f);

        GameObject[] lopats = GameObject.FindGameObjectsWithTag("lopata");

        foreach (GameObject x in lopats) x.GetComponent<lopata>().refresh();

        GameObject[] allPoo = GameObject.FindGameObjectsWithTag("poo");
        const float rangeDelete = 10f;

        foreach (GameObject x in allPoo)
        {
            if (x.transform.position.z - transform.position.z < rangeDelete &&
                x.transform.position.z - transform.position.z > -1 * rangeDelete)
            Destroy(x);
        }

        superPowerKD = 0;
        laneNumber = 1;
        // Impregnable = true;
        Mng.MoveSpeed = 0;
        cantSwap = true;
        rb.isKinematic = true;
        an.Play("defaultChel");
        poo.JesusCheck = true;
        Mng.GOVNO = false;
        Time.timeScale = 1f;
        DeathScreenTrue.SetActive(false);

        int numRespPrefab;
        if (transform.position.z > RoadSpawnerScr.CurrBlocs[3].transform.position.z) numRespPrefab = 3;
        else if (transform.position.z > RoadSpawnerScr.CurrBlocs[2].transform.position.z) numRespPrefab = 2;
        else numRespPrefab = 1;

        transform.position = RoadSpawnerScr.CurrBlocs[numRespPrefab].transform.position + new Vector3(0, 20f , 13);

        Destroy(objKRAN);
        objKRAN = Instantiate(KRAN, transform);
        objKRAN.transform.rotation = Quaternion.identity;

        objKRAN.transform.parent = null;
        objKRAN.transform.localScale = new Vector3(1f, 1f, 1f);

        GameObject kran = objKRAN.transform.Find("kran").gameObject;
        transform.parent = kran.transform;
        kran.GetComponent<Animator>().Play("moveChelKleshnya");

        yield return new WaitForSeconds(3);

        Mng.GOVNO = true;
        transform.parent = null;
        rb.isKinematic = false;
        an.Play("chel_run");
        Mng.MoveSpeed = 15f;
        wannaSlide = false;
        wannaJump = false;
        cantSwap = false;
        cantSwapDouble = false;
        affectedByDeath = false;
        // Impregnable = false;

        yield return new WaitForSeconds(8);

        ACIG.CheckAchievement();
    }

    public void startBackToLife()
    {
        respawnRemaning -= 1;
        StopAllCoroutines();
        Mng.defSound.TransitionTo(0.5f);
        ACIG.CheckAchievement();
        StartCoroutine(BackToLife());
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

    void GetAllBaff()
    {
        GetDoctorBaff();
        superPowerKD = 0;
        GetEricBaff();
        superPowerKD = 0;
        GetKnightBaff();
        superPowerKD = 0;
        GetRunnerBaff();
        superPowerKD = 0;
        GetClerkBaff();
        superPowerKD = 0;
        GetMikeBaff();
        superPowerKD = 0;
        GetTideBaff();
        superPowerKD = 0;
        GetBussinesBaff();
        superPowerKD = 0;
        GetMoonManBaff();
    }

    void GetSuperPower()
    {
        switch (Mng.AS.WhatIsSkinShoose)
        {
            case 0:
                // GetAllBaff();
                // Debug.Log("Classic, i mean pleasantly");
                break;  
            case 1:
                GetDoctorBaff();
                break;      
            case 2:
                GetEricBaff();
                break;
            case 3:
                GetKnightBaff();
                break;
            case 4:
                GetRunnerBaff();
                break;
            case 5:
                GetClerkBaff();
                break;
            case 6:
                GetMikeBaff();
                break;
            case 7:
                GetTideBaff();
                break;
            case 8:

                break;
            case 9:

                break;
            case 10:
                GetBussinesBaff();
                break;
            case 11:
                GetMoonManBaff();
                break;
            default:
                Debug.Log("Huynia kakaya to");
                break;
        }
        ACIG.CheckAchievement();
    }

    void GetDoctorBaff()
    {
        GetRespBaff(null);
        superPowerKD += 10 - (10 * Mng.AS.UpgradeLevel[6]);
        Mng.PutinUsed(superPowerKD);
        Mng.AS.SupermanMax += 1;
    }

    void GetEricBaff()
    {
        transform.localScale += scaleChange;
        raycastLen = 1.5f;
        rb.mass = 200f;
        putin_baff += 10 + (1 * Mng.AS.UpgradeLevel[6]);
        Mng.AS.SupermanMax += 1;
        SuperPowerBar.SetActive(true);
        SuperPowerSlider.maxValue = putin_baff;
    }

    void GetKnightBaff()
    {
        GetShieldBaff(null);
        superPowerKD += 45 - (4 * Mng.AS.UpgradeLevel[6]);
        Mng.PutinUsed(superPowerKD);
        Mng.AS.SupermanMax += 1;
    }

    void makeObstaclesTrigger(bool count)
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("obstacle");
        foreach (GameObject x in obstacles)
        {
            if (x.GetComponent<BoxCollider>()) x.GetComponent<BoxCollider>().isTrigger = count;
            if (x.GetComponent<Rigidbody>())x.GetComponent<Rigidbody>().isKinematic = count;
        }
    }

    void GetRunnerBaff()
    {
        makeObstaclesTrigger(true);
        cameraAnim.Play("skorostBuff"); 
        skorostnoyBuff = 8f;
        runnerEffect.Play("runnerEffect");
        Mng.MoveSpeed = 35f;
        Mng.AS.SupermanMax += 1;
        SuperPowerBar.SetActive(true);
        SuperPowerSlider.maxValue = skorostnoyBuff;
        cameraScr.ShakeCamera(skorostnoyBuff, 0.5f, 70f);
    }

    void GetClerkBaff()
    {
        if (!affectedByDeath || valueResp) return;
        // скелет
        SuperPowerIcon.SetActive(false);
        StopAllCoroutines();
        // Destroy(Chel1);

        GetSkin(TripleMasive[AS.WhatIsSkinShoose][1]);
        StartCoroutine(RespWithCoroutine());

        cantSwapDouble = false;

        // Mng.MoveSpeed = 15f;
        // cantSwap = false;
        // affectedByDeath = false;
        superPowerKD += 3600000;
        Mng.AS.SupermanMax += 1;
    }

    void GetMikeBaff()
    {
        // 2x реклама
        superPowerKD += 36;
        Mng.AS.SupermanMax += 1;
    }


    void GetTideBaff()
    {
        StopAllCoroutines();
        GameObject Volna = Instantiate(volna, transform);
        Volna.transform.parent = null;
        Volna.transform.localScale = new Vector3(1f, 1f, 1f);

        if (invSpheres != null) foreach(Transform x in invSpheres.transform.Find("animator")) x.gameObject.SetActive(false);

        cameraScr.ShakeCamera(2f + (0.3f * Mng.AS.UpgradeLevel[6]), 0.5f, 70f);
        superPowerKD += 60 - (4 * Mng.AS.UpgradeLevel[6]);
        Mng.PutinUsed(superPowerKD);
        Mng.AS.SupermanMax += 1;
    }

    void GetShrekBaff()
    {
        raycastLen = 0.98f;
        if (shreak_Buff == 0)
        {
            transform.localScale += scaleChangeShreak;
        }
        shreak_Buff = 6f + (0.5f * Mng.AS.UpgradeLevel[6]);
        Mng.MoveSpeed = 18f;
        JumpSpeed = 25f;
        Mng.AS.SupermanMax += 1;
        SuperPowerIcon.SetActive(true);
        SuperPowerBar.SetActive(true);
        SuperPowerSlider.maxValue = shreak_Buff;
    }

    void GetBussinesBaff()
    {
        // int win = 50 + (7 * Mng.AS.UpgradeLevel[6]);
        BusinessRoll.SetActive(true);
        if (Random.Range(0, 101) < 50)
        {
            strelkaBussiness.GetComponent<Animator>().Play("strelkaLose" + Random.Range(1, 4));
            StartCoroutine(forBussinesMenAwardCoin((int)(Mng.money / 2)));
        }
        else
        {
            strelkaBussiness.GetComponent<Animator>().Play("strelkaWin" + Random.Range(1, 3));
            StartCoroutine(forBussinesMenAwardCoin((int)(Mng.money / 2), true));
        }
        superPowerKD += 30;
        Mng.PutinUsed(superPowerKD);
        Mng.AS.SupermanMax += 1;
        StartCoroutine(WaitRoll());
    }
    IEnumerator WaitRoll()
    {
        yield return new WaitForSeconds(6.5f);
        BusinessRoll.SetActive(false);
    }

    IEnumerator forBussinesMenAwardCoin(int count, bool falseCoin = false)
    {
        yield return new WaitForSeconds(6f);
        if (count > 30) count = 30;
        for (int i = 0; i < count; i++)
        {
            if (falseCoin) Mng.getFalseCoin();
            else Mng.getAwardCoin();
            yield return new WaitForSeconds(0.2f);
        }
    }

    void GetMoonManBaff()
    {
        moonMan_Buff += 10f;
        mass = 1f;
        SuperPowerBar.SetActive(true);
        SuperPowerSlider.maxValue = moonMan_Buff;
    }

    void GetShieldBaff(Collider other)
    {
        if (other != null) Destroy(other.gameObject);
        Destroy(BlockShield);
        BlockShield = Instantiate(ShieldBlock, transform);
        BlockShield.transform.position = new Vector3(transform.position.x, transform.position.y + 0.65f, transform.position.z - 0.7f);
        antiGovno = true;
        shield_baff = 1.5f + Mng.AS.UpgradeLevel[3];
        Mng.ShieldАctivation();
        ACIG.CheckAchievement();
    }

    void GetRespBaff(Collider other)
    {
        if (other != null) Destroy(other.gameObject);
        if (studyScene && !finalStudy) 
        {
            finalStudy = true;
            StartCoroutine(makePooStudy());
        }
        valueResp = true;
        Mng.HeadstoneАctivation();
        ACIG.CheckAchievement();
    }

    IEnumerator makePooStudy()
    {
        yield return new WaitForSeconds(6f);
        // StartCoroutine(makePooStudy2());

        cantSwap = true;
        affectedByDeath = true;
        Mng.MoveSpeed = 0;
        an.Play("studyWin");
        yield return new WaitForSeconds(2.5f);
        TrueDeath();
    }

    IEnumerator makePooStudy2()
    {
        Mng.makePoo();
        yield return new WaitForSeconds(0.001f);
        StartCoroutine(makePooStudy2());
    }

    void GetCatapulted(lopata lopata = null)
    {
        if (lopata == null) return;

        lopata.gameObject.GetComponent<Animator>().Play("lopataUp");

        rb.Sleep();
        rb.AddForce(new Vector3( 0, 35f + (Mng.AS.UpgradeLevel[4] * 8), 35f + (Mng.AS.UpgradeLevel[4] * 8)), ForceMode.VelocityChange);
        an.Play("flyingChel");
        Mng.scoreReal += ((Mng.AS.UpgradeLevel[4] * 7) + (Mng.AS.UpgradeLevel[1] * Mng.AS.UpgradeLevel[1] * 7));
        Mng.AS.СatapultMax += ((Mng.AS.UpgradeLevel[4] * 7) + (Mng.AS.UpgradeLevel[1] * Mng.AS.UpgradeLevel[1] * 7));
        ACIG.CheckAchievement();        
    }

    void GetSkin(GameObject count)
    {
        Destroy(Chel1);
        Chel1 = Instantiate(count, transform);
        an = Chel1.GetComponent<Animator>();
        PopcornFill = Chel1.transform.Find("body/popFill").gameObject;
        system = particles.GetComponent<ParticleSystem>();
    }

    void checkSkeletParts()
    {
        if (affectedByDeath) return;
        if (skeletPartsRemain == 5)
        {
            skeletParts.rightArm.transform.parent = RoadSpawnerScr.CurrBlocs[1].transform;
        }
        else if (skeletPartsRemain == 4)
        {
            skeletParts.leftArm.transform.parent = RoadSpawnerScr.CurrBlocs[1].transform;
        }
        else if (skeletPartsRemain == 3)
        {
            skeletParts.body.transform.parent = RoadSpawnerScr.CurrBlocs[1].transform;
        }
        else if (skeletPartsRemain == 2)
        {
            skeletParts.rightFoot.transform.parent = RoadSpawnerScr.CurrBlocs[1].transform;
        }
        else
        {
            StartCoroutine(DeathWithCoroutine());
            return;
        }
        skeletPartsRemain -= 1;
        StartCoroutine(RespWithCoroutine());
    }

    IEnumerator enableSpheres()
    {
        foreach(Transform x in invSpheres.transform.Find("animator"))
        {
            x.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }

    void checkTouchWithObstacle(Collision other)
    {
        if (affectedByDeath) return;
        if (other.gameObject.tag == "poo" && !other.gameObject.GetComponent<poo>().inactive)
        {
            GameObject expl = Instantiate(explosionGovno, other.gameObject.transform);
            expl.transform.parent = RoadSpawnerScr.CurrBlocs[1].transform;
            if (shreakBuffOn)
            {
                other.gameObject.GetComponent<poo>().shreakCrush();
                StartCoroutine(other.gameObject.GetComponent<poo>().shieldCrush());
                other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10f, 10f), ForceMode.VelocityChange);
                GetShrekBaff();
                return;
            }
            else if (antiGovno == true)
            {
                audioSource.PlayOneShot(breakSound, soundEffectsVolume);
                StartCoroutine(other.gameObject.GetComponent<poo>().shieldCrush());
                rb.Sleep();
                other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 20f, 20f), ForceMode.VelocityChange);
                shield_baff -= 1f;
                Mng.AS.ProtectionShit += 1;
                // BlockShield.AddComponent<Rigidbody>();
                // BlockShield.GetComponent<Rigidbody>().AddForce(new Vector3(0, 20f, 20f), ForceMode.VelocityChange);
            }
            else if (valueResp == false)
            {
                causeDeath = causeDeathType.POP;
                if (skeletOn)
                {
                    checkSkeletParts();
                    return;
                }
                Mng.AS.DeathShit += 1;
                StartCoroutine(DeathWithCoroutine());
            }
            else
            {
                Mng.HeadstoneShutdown();
                Mng.AS.SecondChanceMax += 1;
                StartCoroutine(RespWithCoroutine());
            }
        }
        else if (other.gameObject.tag == "obstacle" || other.gameObject.tag == "wall")
        {
            if (putin_baff > 1) 
            {
                if (other.gameObject.tag == "wall")
                {
                    if (obstacleInFront())
                    {
                        Mng.audioSource.PlayOneShot(deathSound, soundEffectsVolume); 
                        rb.AddForce(new Vector3(0, 15f, -30f), ForceMode.VelocityChange);
                    }
                    if (obstacleInTheSide())
                    {
                        Mng.audioSource.PlayOneShot(deathSound, soundEffectsVolume); 
                        laneNumber = prLaneNumber;
                    }
                }
                else if (other.gameObject.tag == "obstacle" && other.gameObject.GetComponent<Rigidbody>())
                {
                    other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 100f, 500f), ForceMode.Impulse);
                }
            }
            else if (obstacleInFront())
            {
                GameObject expl = Instantiate(explosion, transform);
                expl.transform.parent = RoadSpawnerScr.CurrBlocs[1].transform;
                if (valueResp == false)
                {
                    causeDeath = causeDeathType.STUNN;
                    if (skeletOn)
                    {
                        checkSkeletParts();
                        return;
                    }                    
                    StartCoroutine(DeathWithCoroutine());   
                }
                else
                {
                    Mng.HeadstoneShutdown();
                    Mng.AS.SecondChanceMax += 1;
                    StartCoroutine(RespWithCoroutine());
                }
            }
            else if (obstacleInTheSide())
            {
                laneNumber = prLaneNumber;
                GameObject expl = Instantiate(explosion, transform);
                expl.transform.parent = RoadSpawnerScr.CurrBlocs[1].transform;
                if (valueResp == false)
                {
                    causeDeath = causeDeathType.STUNN;
                    if (skeletOn)
                    {
                        checkSkeletParts();
                        return;
                    }  
                    StartCoroutine(DeathWithCoroutine());   
                }
                else
                {
                    Mng.HeadstoneShutdown();
                    StartCoroutine(RespWithCoroutine());
                }
            } 
        }
        ACIG.CheckAchievement();
    }

    void OnTriggerEnter(Collider other)
    {
        if (an == null) return;
        else if (other.gameObject.tag == "puddle") StackInPuddle();
        else if (other.gameObject.tag == "lopata") GetCatapulted(other.gameObject.GetComponent<lopata>());
        else if (other.gameObject.tag == "deathZone" && !affectedByDeath) 
        {
            causeDeath = causeDeathType.FALL;
            StartCoroutine(buyBackOffer());
        }
        else if (skeletOn) return;
        else if (other.gameObject.tag == "shield") GetShieldBaff(other);
        else if (other.gameObject.tag == "headstone") GetRespBaff(other);
    }

    void OnCollisionEnter(Collision other)
    {
        // if (Impregnable) return;
        checkTouchWithObstacle(other);
        if (other.gameObject.tag == "road") 
        {
            // particles.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z );
        }
        else if (other.gameObject.tag == "platform") 
        {
            forceJump(40);
            an.Play("flyingChel");
        }
        else if (skeletOn) return;
        else if (other.gameObject.tag == "coin") GetCoin(other);
    }

    void OnCollisionStay(Collision other)
    {
        checkTouchWithObstacle(other);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "lopata") GetCatapulted(other.gameObject.GetComponent<lopata>());
        if (other.gameObject.tag == "deathZone" && !affectedByDeath)
        {
            causeDeath = causeDeathType.FALL;
            StartCoroutine(buyBackOffer());
        }
        else if (other.gameObject.tag == "cloud") cloudMoney += 0.5f;
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) 
    {
        if (showResult == ShowResult.Finished)
        {
            adsCheck = 0;
        }
        else if (showResult == ShowResult.Skipped)
        {
            adsCheck = 3;
        }
        else adsCheck = 1;
    }
    public void OnUnityAdsReady (string placementId) { }
    public void OnUnityAdsDidError (string message) { }
    public void OnUnityAdsDidStart (string placementId) { }
}
