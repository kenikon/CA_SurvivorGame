using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;

    void Start()
    {
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        int EnemiesToSpawn = 5;
        for (int i = 0; i < EnemiesToSpawn; i++)
        {
            GameObject temp = Instantiate(EnemyPrefab, transform);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }
    }

    Vector3 GetRandomPointInCollider (Collider collider)
    {
        Vector3 Point = new(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );
        if (Point != collider.ClosestPoint(Point))
        {
            Point = GetRandomPointInCollider(collider);
        }
        Point.y = 1;
        return Point;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}