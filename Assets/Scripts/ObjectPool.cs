using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string type;

        public GameObject prefabs;

        public int size;
    }


    public static ObjectPool instance;

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
        }
    }

    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDictionary;
    GameObject objectToSpawn;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> obectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefabs);
                obj.transform.position = new Vector3(0, 20, 0);
                obj.SetActive(false);
                obectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.type, obectPool);
        }
    }


    public GameObject SpawnFromPool(string type, Vector3 position)
    {
        if (!poolDictionary.ContainsKey(type))
        {
            
            return null;

        }


        objectToSpawn = poolDictionary[type].Dequeue();
       

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;

        poolDictionary[type].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
