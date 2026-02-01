using Assets.Scripts.NPCs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MaskEntryController : MonoBehaviour, IPointerClickHandler
{
    public Image image;
    public Image background;
    public MaskController maskController;

    public bool bIsSelected = false;

    public delegate void OnMaskSelected(MaskEntryController maskEntry);
    public OnMaskSelected onMaskSelected;

    public void init(MaskController mask)
    {
        this.maskController = mask;
        image.sprite = mask.Data.Sprite;

    }



    public void OnMouseDown()
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        Debug.Log("Clicked on mask");

        bIsSelected = !bIsSelected;

        if (bIsSelected)
        {
            OnSelected();
        }
        else
        {
            OnUnselected();
        }

        UpdateSelected();
    }

    public void UpdateSelected()
    {
        if (bIsSelected)
        {
            background.color = Color.green;
        }
        else
        {
            background.color = Color.white;
        }
    }

    public void OnSelected()
    {
        NPC_Selectable NPC = NPC_Manager.Instance.selectedNPC;

        if (NPC == null)
            return;


        NPC.EquipMask(maskController);

        onMaskSelected(this);
    }

    public void OnUnselected()
    {
        NPC_Selectable NPC = NPC_Manager.Instance.selectedNPC;

        if (NPC == null)
            return;

        NPC.UnequipMask();
    }
}
