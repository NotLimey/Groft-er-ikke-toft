using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject Tile;
    public Transform Car;
    public float ZSpawn = 0;
    public float TileLength = 100;
    public int NumberOfTiles = 5;

    private List<GameObject> _activeTiles = new List<GameObject>();

    void Start()
    {
        for(int i = 0; i < NumberOfTiles; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        if (Car.position.x - 105 > ZSpawn - (NumberOfTiles * TileLength))
        {
            SpawnTile();
            DeleteTile();
        }
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

}
