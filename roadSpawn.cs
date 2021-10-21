using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class roadSpawn : MonoBehaviour
{
	public GameObject[] roadPrefabsUsual;
    public GameObject[] roadPrefabsUp;
    public GameObject[] roadPrefabsDown;
    public GameObject[] roadPrefabsEasy;
    public GameObject[] roadPrefabsHard;
    public GameObject[] roadPrefabsAll;

	public GameObject StartBlock;
    public GameObject StartBlockStudy;
    public roadBlockScr studyBlockScr;
    public Manager Mng;

	public float blockZPos = 0;
	public float blockLength;

    [SerializeField]
    private float dop_y = 0;
    private float startCount;
    private int lastCount;
	public Transform PlayerTransf;
	public List<GameObject> CurrBlocs = new List<GameObject>();
    public List<int> blocskIndex = new List<int>();

    public bool SPAWN = false;
    private bool double_Up = false;
    private bool double_Down = false;
    public bool studyScene = false;
    private int levelHeight = 0;
    private int prefabsCount = 0;


    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "studyScene")
        {
            studyScene = true;
            blockLength = StartBlock.GetComponent<BoxCollider>().size.z * StartBlock.transform.lossyScale.z;

            GameObject block1 = Instantiate(StartBlockStudy, transform);
            block1.transform.position = new Vector3(0, 0, 0);
            studyBlockScr = block1.GetComponent<roadBlockScr>();
            CurrBlocs.Add(block1);
            CurrBlocs.Add(block1);
            CurrBlocs.Add(block1);
            CurrBlocs.Add(block1);
        }    
    }


    void Start()
    {
        if (!studyScene)
        {
            roadPrefabsAll = new GameObject[roadPrefabsEasy.Length + roadPrefabsUp.Length + roadPrefabsDown.Length + roadPrefabsHard.Length];
            roadPrefabsEasy.CopyTo(roadPrefabsAll, 0);
            roadPrefabsUp.CopyTo(roadPrefabsAll, roadPrefabsEasy.Length);
            roadPrefabsDown.CopyTo(roadPrefabsAll, roadPrefabsEasy.Length + roadPrefabsUp.Length);
            roadPrefabsHard.CopyTo(roadPrefabsAll, roadPrefabsEasy.Length + roadPrefabsUp.Length + roadPrefabsDown.Length);

            blockLength = StartBlock.GetComponent<BoxCollider>().size.z * StartBlock.transform.lossyScale.z;

            GameObject block1 = Instantiate(StartBlock, transform);
            block1.transform.position = new Vector3(0, 0, -50); 
            CurrBlocs.Add(block1);

            for (int i = 0; i < 3; i++) 
            {   
                GameObject block = Instantiate(StartBlock, transform);
                block.transform.position = new Vector3(0, CurrBlocs[CurrBlocs.Count - 1].transform.position.y, 
                                                          CurrBlocs[CurrBlocs.Count - 1].transform.position.z + blockLength ); 
                CurrBlocs.Add(block);            
            }            
        }
    }

    void FixedUpdate()
    {
        if (studyScene) return;
    	if (CurrBlocs[CurrBlocs.Count - 1] != null && PlayerTransf.position.z > CurrBlocs[CurrBlocs.Count - 1].transform.position.z + 20) 
        {
            if (double_Up)
            {
                double_Up = false;
                if (PlayerTransf.position.y + 4 > CurrBlocs[CurrBlocs.Count-1].GetComponent<SphereCollider>().center.y *
                                                  CurrBlocs[CurrBlocs.Count-1].transform.lossyScale.y +
                                                  CurrBlocs[CurrBlocs.Count-1].transform.position.y )
                {
                    levelHeight += 1;
                    dop_y = CurrBlocs[CurrBlocs.Count - 1].GetComponent<SphereCollider>().center.y *
                            CurrBlocs[CurrBlocs.Count - 1].transform.lossyScale.y +
                            CurrBlocs[CurrBlocs.Count - 1].transform.position.y;
                }
                else dop_y = CurrBlocs[CurrBlocs.Count - 1].transform.position.y;

            }
            else if (double_Down)
            {
                double_Down = false;
                if (PlayerTransf.position.y - 4 < CurrBlocs[CurrBlocs.Count-1].GetComponent<SphereCollider>().center.y *
                                                  CurrBlocs[CurrBlocs.Count-1].transform.lossyScale.y +
                                                  CurrBlocs[CurrBlocs.Count-1].transform.position.y )
                {
                    levelHeight -= 1;
                    dop_y = CurrBlocs[CurrBlocs.Count - 1].GetComponent<SphereCollider>().center.y *
                            CurrBlocs[CurrBlocs.Count - 1].transform.lossyScale.y +
                            CurrBlocs[CurrBlocs.Count - 1].transform.position.y;
                }


                else dop_y = CurrBlocs[CurrBlocs.Count - 1].transform.position.y;
            }
        }

        if (CurrBlocs[CurrBlocs.Count - 1] != null && PlayerTransf.position.z > CurrBlocs[2].transform.position.z && !double_Up && !double_Down)
        {
            CurrBlocs.Add(null);
            StartCoroutine(SpawnBlock());
        }
    }

    IEnumerator SpawnBlock()
    {
        yield return new WaitForSeconds(0.1f);

        prefabsCount += 1;
    	GameObject block = Instantiate(roadPrefabsAll[randomPrefab()], transform);
   		block.transform.position = new Vector3(0, dop_y, CurrBlocs[CurrBlocs.Count-2].transform.position.z + blockLength );
        if (levelHeight > 6f) block.GetComponent<roadBlockScr>().setChances(100, 0, 100, 100);
        else if (levelHeight > 5) block.GetComponent<roadBlockScr>().setChances(95, 5, 90, 90 + Mng.AS.UpgradeLevel[5] * 6);
        else if (levelHeight > 4) block.GetComponent<roadBlockScr>().setChances(88, 10, 85, 80 + Mng.AS.UpgradeLevel[5] * 6);
        else if (levelHeight > 3) block.GetComponent<roadBlockScr>().setChances(81, 20, 80, 70 + Mng.AS.UpgradeLevel[5] * 6);
        else if (levelHeight > 2) block.GetComponent<roadBlockScr>().setChances(74, 30, 75, 60 + Mng.AS.UpgradeLevel[5] * 6);
        else if (levelHeight > 1) block.GetComponent<roadBlockScr>().setChances(67, 40, 70, 50 + Mng.AS.UpgradeLevel[5] * 6);
        else if (levelHeight > 0) block.GetComponent<roadBlockScr>().setChances(60, 50, 60, 40 + Mng.AS.UpgradeLevel[5] * 6);

        blockLength = block.GetComponent<BoxCollider>().size.z * block.transform.lossyScale.z;
        dop_y = block.transform.position.y + block.GetComponent<SphereCollider>().center.y * block.transform.lossyScale.y;

        if (block.tag == "road_up") 
        {
            double_Up = true;
        }
        else if (block.tag == "road_down") 
        {
            double_Down = true;
        }
   		CurrBlocs[CurrBlocs.Count - 1] = block;
        DestroyBlock();
    }

    int randomPrefab()
    {
        int count;
        if (levelHeight > 8) // zdarova
        {
            if (levelHeight % 2 == 0)
            {
                count = Random.Range(roadPrefabsEasy.Length, roadPrefabsEasy.Length + roadPrefabsUp.Length);
            }
            else
            {
                count = Random.Range(roadPrefabsEasy.Length + roadPrefabsUp.Length, roadPrefabsEasy.Length + roadPrefabsUp.Length + roadPrefabsDown.Length);
            }
            blocskIndex.Add(count);
            if (blocskIndex.Count > 2) blocskIndex.RemoveAt(0);
            return count;
        }
        else if (prefabsCount % 5 == 0)
        {
            count = Random.Range(roadPrefabsEasy.Length, roadPrefabsEasy.Length + roadPrefabsUp.Length);
        }
        else if (levelHeight > 3)
        {
            count = Random.Range(roadPrefabsEasy.Length + roadPrefabsUp.Length, roadPrefabsAll.Length);
        }
        else if (levelHeight > 1)
        {
            count = Random.Range(0, roadPrefabsEasy.Length);
        }
        else
        {
            count = Random.Range(0, roadPrefabsEasy.Length);
        }

        if (blocskIndex.Contains(count) && count != 0) 
        {
            do {
                count -= 1;
            } while (blocskIndex.Contains(count) && count != 0);
        }

        blocskIndex.Add(count);
        if (blocskIndex.Count > 2) blocskIndex.RemoveAt(0);
        return count;
    }

    void DestroyBlock()
    {
    	Destroy(CurrBlocs[0]);
    	CurrBlocs.RemoveAt(0);
    }

    // public void StopAllBlocks()
    // {
    //     foreach (GameObject blck in CurrBlocs) blck.GetComponent<roadBlockScr>().MS = 0;
    // }

    // public void StartAllBlocks()
    // {
    //     Mng.MoveSpeed = 15;
    //     foreach (GameObject blck in CurrBlocs) blck.GetComponent<roadBlockScr>().MS = Mng.MoveSpeed;
    // }
}
