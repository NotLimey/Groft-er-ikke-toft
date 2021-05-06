using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] carPrefabs;
    private float startDelay = 0f;
    private float spawnInterval = 2f;

    private List<GameObject> _activeCars = new List<GameObject>();

    void Start()
    {
        Invoke("SpawnVehicles", startDelay);
    }
    
    public Transform GetSpawnPoints()
    {
        int index = Random.Range(0, spawnPoints.Length);
        return spawnPoints[index];
    }
    public GameObject GetVehicle()
    {
        int index = Random.Range(0, carPrefabs.Length);
        return carPrefabs[index];
    }

    private void DeleteCar()
    {
        Destroy(_activeCars[0]);
        _activeCars.RemoveAt(0);
    }

    public GameObject SpawnVehicles()
    {
        Transform spawnpoint = GetSpawnPoints();
        GameObject Vehicle = GetVehicle();
        GameObject v = Instantiate(Vehicle, spawnpoint.position, spawnpoint.rotation) as GameObject;
        _activeCars.Add(v);
        Invoke("SpawnVehicles", Random.Range(1, 6));
        return v;
    }
}
