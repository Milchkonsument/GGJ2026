using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HouseSelectionManager : MonoBehaviour
{
    [Header("House Display Elements")]
    [SerializeField] private List<Sprite> houseSprites;
    [SerializeField] private Image houseImage;
    [SerializeField] private TMP_Text houseNameText;
    [SerializeField] private TMP_Text houseDifficultyText;

    [Header("Generate House Selection Panel")]
    [SerializeField] private GameObject selectionPanelPrefab;
    [SerializeField] private Transform selectionPanelContainer;

    private List<string> houseNames = new List<string> { "Candy", "Pain", "Treats", "Tricks", "Nightmares", "Mystery", "Delight", "Houses" };

    private void Start()
    {
        GenerateHouseDetails();
        GenerateHouseDetails();
        GenerateHouseDetails();        
    }

    private void GenerateHouseDetails()
    {
        var panel = Instantiate(selectionPanelPrefab, selectionPanelContainer);
        Image[] panelImage = panel.GetComponentsInChildren<Image>();
        Image housePanelImage = panelImage[1];
        TMP_Text[] textComponents = panel.GetComponentsInChildren<TMP_Text>();
        TMP_Text panelText = textComponents[0];
        TMP_Text panelDifficultyText = textComponents[1];

        housePanelImage.sprite = houseSprites[Random.Range(0, houseSprites.Count)];
        panelText.text = "House of " + houseNames[Random.Range(0, houseNames.Count)];
    }
}
