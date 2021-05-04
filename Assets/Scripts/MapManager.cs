using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject Tile;
    public GameObject[] Cars;
    public Transform Car;
    public float ZSpawn = 0;
    public float TileLength = 100;
    public int NumberOfTiles = 5;

    private List<GameObject> _activeTiles = new List<GameObject>();
    private List<GameObject> _activeCars = new List<GameObject>();

    void Start()
    {
        for(int i = 0; i < NumberOfTiles; i++)
        {
            SpawnTile();
        }
        for(int i = 0; i < Random.Range(1, 25); i++)
        {
            SpawnCar(0);
        }
    }

    void Update()
    {
        if (Car.position.x - 105 > ZSpawn - (NumberOfTiles * TileLength))
        {
            SpawnTile();
            SpawnCar(Random.Range(0, Cars.Length));
            DeleteTile();
            DeleteCar();
        }
    }

    private void DeleteCar()
    {
    }

    private void DeleteTile()
    {
        Destroy(_activeTiles[0]);
        _activeTiles.RemoveAt(0);
    }

    public void SpawnTile()
    {
        GameObject go = Instantiate(Tile, transform.right * ZSpawn, transform.rotation);
        _activeTiles.Add(go);
        ZSpawn += TileLength;
    }

    private void SpawnCar(int carIndex)
    {
        GameObject car = Instantiate(Cars[carIndex], transform.right * ZSpawn, transform.rotation);
        _activeCars.Add(car);
    }

}
