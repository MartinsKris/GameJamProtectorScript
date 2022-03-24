using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyReference;

    [SerializeField]
    private Transform leftPos, rightPos;

    private GameObject spawnedEnemy;
    private int randomIndex;
    private int randomSide;
    private int _speedMin = 4;
    private int _speedMax = 10;
    GameObject vipObject;
    private string _vip = "VIP";
    int layerOfDead = 7;

    void Awake()
    {
        vipObject = GameObject.FindGameObjectWithTag(_vip);
    }
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (vipObject.layer != layerOfDead)
        {
            yield return new WaitForSeconds(Random.Range(3, 5));

            randomIndex = Random.Range(0, enemyReference.Length);
            randomSide = Random.Range(0, 2);
            spawnedEnemy = Instantiate(enemyReference[randomIndex]);

            if (randomSide == 0)
            {
                spawnedEnemy.transform.position = leftPos.position;
                spawnedEnemy.GetComponent<Enemy>().speed = Random.Range(_speedMin, _speedMax);
            }
            else
            {
                spawnedEnemy.transform.position = rightPos.position;
                spawnedEnemy.GetComponent<Enemy>().speed = -Random.Range(_speedMin, _speedMax);
                spawnedEnemy.transform.localScale = new Vector3(-0.65f, 0.65f, 0.65f);
            }
        }
    }
}
