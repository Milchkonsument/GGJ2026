using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    [SerializeField] private GameObject worldTilePrefab;

    private List<GameObject> worldTiles = new List<GameObject>();
    private float recentTilePosX;
    private float tileWidth;

    private void Start()
    {
        SetStartTiles();
    }

    private void SetStartTiles()
    {
        SpriteRenderer spriteRenderer = worldTilePrefab.GetComponent<SpriteRenderer>();
        tileWidth = spriteRenderer.bounds.size.x;

        float startPosx = tileWidth / -2;
        recentTilePosX = startPosx + tileWidth;

        Debug.Log("Placing tiles");
        GameObject firstTile = Instantiate(worldTilePrefab, new Vector3(startPosx, 0, 0), Quaternion.identity);
        GameObject secondTile = Instantiate(worldTilePrefab, new Vector3(recentTilePosX, 0, 0), Quaternion.identity);
        worldTiles.Add(firstTile);
        worldTiles.Add(secondTile);
    }

    private void Update()
    {
        if (Camera.main.transform.position.x > recentTilePosX - (tileWidth / 2))
        {
            GenerateNewTile();
        }

        if (Camera.main.transform.position.x > worldTiles[0].transform.position.x + tileWidth * 2)
        {
            DeleteOldestTile();
        }
    }

    private void GenerateNewTile()
    {
        GameObject newTile = Instantiate(worldTilePrefab, new Vector3(recentTilePosX + tileWidth, 0, 0), Quaternion.identity);
        recentTilePosX += tileWidth;
        worldTiles.Add(newTile);
    }

    private void DeleteOldestTile()
    {
        GameObject oldestTile = worldTiles[0];
        worldTiles.RemoveAt(0);
        Destroy(oldestTile);
    }
}
