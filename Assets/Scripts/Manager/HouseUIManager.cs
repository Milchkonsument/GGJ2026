using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HouseUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text houseNameText;
    [SerializeField] private Image houseSprite;

    [Header("House Sprites")]
    [SerializeField] private List<Sprite> houseSprites;

    private void Awake()
    {
        //subcribe to event
    }

    private void OnDisable()
    {
        //unsubscribe from event
    }

    public void UpdateHouseUI()
    {
        HouseData houseData = GameController.Instance.GetCurrentHouse();
        houseNameText.text = houseData.HouseName;
        houseSprite.sprite = houseSprites[Random.Range(0, houseSprites.Count)];
    }
}
