using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    [SerializeField] private GameObject worldTilePrefab;

    private void Start()
    {
        SetStartTiles();
    }

    private void SetStartTiles()
    {
        SpriteRenderer spriteRenderer = worldTilePrefab.GetComponent<SpriteRenderer>();
        float tileWidth = spriteRenderer.bounds.size.x;

        float startPosx = tileWidth / -2;
        float newPosX = startPosx + tileWidth;

        Debug.Log("Placing tiles");
        Instantiate(worldTilePrefab, new Vector3(startPosx, 0, 0), Quaternion.identity);
        Instantiate(worldTilePrefab, new Vector3(newPosX, 0, 0), Quaternion.identity);

    }
}
