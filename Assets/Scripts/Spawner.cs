using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool isTriggerSpawn = true;

    string[] foodString = { "Food", };
    int index;
    public static Spawner instance;
    [SerializeField] private float spawnTime = 2;


    [SerializeField] private ObjectPool objectPool = null;



    bool isSpawnStarted = false;

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
        }
        EventSetter(true);
    }

    void Start()
    {

    }


  
    private void OnDisable()
    {
        EventSetter(false);
    }

    public IEnumerator SpawnRoutine()
    {

        while (isSpawnStarted)
        {
            index = Random.Range(0, foodString.Length);
          
            ObjectPool.instance.SpawnFromPool(foodString[index], new Vector3(UnityEngine.Random.Range(-6f, +6f), (0.29f), UnityEngine.Random.Range(-5f, +5f)));

            yield return new WaitForSeconds(spawnTime);
        }
       
    }
    public void TriggerSpawn()
    {
        
        index = Random.Range(0, foodString.Length);
        Debug.Log(foodString[index]);
        ObjectPool.instance.SpawnFromPool(foodString[index], new Vector3(0, 0, 0));
       
    }

    private void SpawnStarted()
    {
        isSpawnStarted = true;
        StartCoroutine(SpawnRoutine());
    }
    private void SpawnStop()
    {
        isSpawnStarted = false;
    }

    private void EventSetter(bool enable)
    {
        if (enable)
        {

            EventManager.OnGameStart += SpawnStarted;
            EventManager.OnLose += SpawnStop;


        }
        else
        {

            EventManager.OnWin -= SpawnStarted;
            EventManager.OnLose -= SpawnStop;
        }
    }

}
