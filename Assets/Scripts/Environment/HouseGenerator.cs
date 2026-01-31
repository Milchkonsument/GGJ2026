using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting;

public class HouseGenerator : MonoBehaviour
{
    [SerializeField] private List<Sprite> houseBaseSprites;
    [SerializeField] private List<Sprite> doorSprites;
    [SerializeField] private List<Sprite> windowSprites;
    [SerializeField] private Transform doorPosition;
    [SerializeField] private Transform[] windowPositions = new Transform[2];

    private SpriteRenderer doorRenderer;
    private SpriteRenderer leftWindowRenderer;
    private SpriteRenderer rightWindowRenderer;

    private void Start()
    {
        GenerateHouse();
    }

    private void GenerateHouse()
    {
        //Generate House Base
        int randomHouseIndex = Random.Range(0, houseBaseSprites.Count);
        SpriteRenderer houseBaseRenderer = GetComponent<SpriteRenderer>();
        houseBaseRenderer.sprite = houseBaseSprites[randomHouseIndex];

        //Generate Door
        int randomDoorIndex = Random.Range(0, doorSprites.Count);
        Sprite doorSprite = doorSprites[randomDoorIndex];
        doorRenderer = doorPosition.GetComponent<SpriteRenderer>();
        doorRenderer.sprite = doorSprite;

        //Generate Left Window
        int randomWindowIndexLeft = Random.Range(0, windowSprites.Count);
        leftWindowRenderer = windowPositions[0].GetComponent<SpriteRenderer>();
        leftWindowRenderer.sprite = windowSprites[randomWindowIndexLeft];

        //Generate Right Window
        int randomWindowIndexRight = Random.Range(0, windowSprites.Count);
        rightWindowRenderer = windowPositions[1].GetComponent<SpriteRenderer>();
        rightWindowRenderer.sprite = windowSprites[randomWindowIndexRight];
    }
}
