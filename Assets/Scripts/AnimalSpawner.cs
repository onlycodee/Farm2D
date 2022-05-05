using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] Animal animalPrefab;
    [SerializeField] float timeBetweenSpawns = 2.0f;
    Vector3 topRight;
    Vector3 bottomLeft;
    float spawnTimer = .0f;
    // Start is called before the first frame update
    void Start()
    {
        topRight = Camera.main.ViewportToWorldPoint(Vector3.one);
        bottomLeft = Camera.main.ViewportToWorldPoint(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= timeBetweenSpawns)
        {
            Debug.Log("Start spawn");
            spawnTimer = 0;
            Crop [] normalCrops = FindObjectsOfType<Crop>();
            List<Crop> grownCrops = new List<Crop>();
            if (normalCrops.Length > 0)
            {
                foreach (var crop in normalCrops)
                {
                    if (crop.IsFullyGrown())
                    {
                        grownCrops.Add(crop);
                    }
                }
            }
            if (grownCrops.Count > 0)
            {
                int randomCropIndex = UnityEngine.Random.Range(0, grownCrops.Count);
                Animal animal = Instantiate(animalPrefab);
                int spawnAtRight = UnityEngine.Random.Range(0, 2);
                float randomX, randomY;
                if (spawnAtRight == 0)
                {
                    randomX = topRight.x + .5f;
                    randomY = UnityEngine.Random.Range(bottomLeft.y, topRight.y - 5);
                } else
                {
                    randomX = UnityEngine.Random.Range(bottomLeft.x, topRight.x);
                    randomY = bottomLeft.y - .5f;
                }
                animal.transform.position = new Vector3(randomX, randomY);
                animal.SetSpawnedPosition(animal.transform.position);
                animal.SetTarget(grownCrops[randomCropIndex]);
            }
            
        }
    }
}
