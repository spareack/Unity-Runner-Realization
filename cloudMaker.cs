using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudMaker : MonoBehaviour
{
    public GameObject Cloud;
    public Manager Mng;
	[SerializeField]
	float Speed;
    GameObject cloud;

    void Start()
    {
        Speed = Mng.MoveSpeed + Random.Range(1f, 2f);
        StartCoroutine(StartSpawn());
        //Начать майнинг эфериума
        //5
        //4
        //3
        //2
        //1
        //Go
        //Все пизда твоему компухтеру
    }

    void spawnCloud()
    {
        cloud = Instantiate(Cloud, transform.parent);
        cloud.GetComponent<cloud>().Mng = Mng;
        // cloud.transform.position = new Vector3(Random.Range(-100f, 100f), Random.Range(50f, 70f), transform.position.z + 400f);
        cloud.transform.position = new Vector3(Random.Range(-200f, 200f), Random.Range(20f, 60f), transform.position.z + 400f);
    }

    void spawnCloudUp()
    {
        cloud = Instantiate(Cloud, transform.parent);
        cloud.GetComponent<cloud>().Mng = Mng;
        // cloud.transform.position = new Vector3(Random.Range(-100f, 100f), Random.Range(50f, 70f), transform.position.z + 400f);
        cloud.transform.position = new Vector3(Random.Range(-40f, 40f), -40f, Random.Range(transform.position.z + 100, transform.position.z + 150));
    }

    IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(4.5f);
        // yield return new WaitForSeconds(Random.Range(0.1f, 1f)); //Малооблачно
        // yield return new WaitForSeconds(Random.Range(0.1f, 0.5f)); //Тучно
        // yield return new WaitForSeconds(0.001f); //Нихуево лупасит

        spawnCloudUp();

        StartCoroutine(StartSpawn());
    }
}
