using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
//using System.Linq;
//using System.Text;

public class ObjectPoolerBackgroundObjects : MonoBehaviour
{
    public List<ChancedPrefab> chancedPrefabList;
    public int poolSize = 30;
    private List<GameObject> pool;
    private List<float> chanceSpread;

    [System.Serializable]
    public class ChancedPrefab
    {
        public GameObject prefab;
        public float chance;
    }
    void Start()
    {
        CreatePool();

    }
    private void CreatePool()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }
    static int FindLargestSmallerOrEqual(List<float> list, float target)
    {
        int left = 0;
        int right = list.Count - 1;
        int result = -1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (list[mid] == target)
            {
                return mid;
            }
            else if (list[mid] < target)
            {
                result = mid;
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        return result;
    }
    private List<float> CreateChanceSpreadList()
    {
        List<float> list = new List<float>();
        float ChanceSum = 0;
        list.Add(0f);
        foreach (ChancedPrefab chancedPrefab in chancedPrefabList)
        {
            ChanceSum += chancedPrefab.chance;
            list.Add(ChanceSum);
        }
        return list;
    }
    private GameObject CreateNewObject()
    {
        if (chanceSpread == null || chanceSpread.Count == 0)
        {
            chanceSpread = CreateChanceSpreadList();
        }

        float random = Random.Range(0f, chanceSpread[chanceSpread.Count-1]);
        int index = FindLargestSmallerOrEqual(chanceSpread, random);
        //Debug.Log("Random:" + random + " Index:" + index);
        GameObject obj = Instantiate(chancedPrefabList[index].prefab, transform.position, transform.rotation);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }
    public GameObject GetPooledObject()
    {
        //Debug.Log(GeneratePoolStateReport(pool));
        for (int i = pool.Count - 1; i >= 0; i--)
        {
            GameObject obj = pool[i];

            if (obj == null)
            {
                pool.RemoveAt(i);
                continue;
            }

            if (!obj.activeSelf)
            {
                return obj;
            }
        }
        return CreateNewObject();
    }
    // Need System.Linq; System.Text;
    //public string GeneratePoolStateReport(List<GameObject> poolCopy)
    //{
    //    if (poolCopy == null || poolCopy.Count == 0)
    //        return "Pool is empty.";

    //    var sb = new StringBuilder();
    //    int total = poolCopy.Count;

    //    var grouped = poolCopy
    //        .Where(obj => obj != null)
    //        .GroupBy(obj => obj.name)
    //        .Select(g => new
    //        {
    //            Name = g.Key,
    //            Count = g.Count(),
    //            Percentage = (double)g.Count() / total * 100
    //        })
    //        .OrderByDescending(x => x.Count)
    //        .ThenBy(x => x.Name);

    //    foreach (var item in grouped)
    //    {
    //        sb.AppendLine($"Prefab: {item.Name} | Count: {item.Count} | Percentage: {item.Percentage:F2}%");
    //    }

    //    return sb.ToString();
    //}
}
