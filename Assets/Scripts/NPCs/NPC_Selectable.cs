using Assets.Scripts.NPCs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class NPC_Selectable : MonoBehaviour
{

    public SpriteRenderer maskOverlay;
    public CharacterController characterController;

    public void OnMouseDown()
    {
        Debug.Log("clicked NPC");
        
        NPC_Manager.Instance.SetSelectedNPC(this);
    }

    public void EquipMask(MaskController mask)
    {
        PlayerController.Instance.AttachMaskToCharacter(mask, characterController);
        maskOverlay.sprite = mask.Data.Sprite;
    }

    public void UnequipMask()
    {
        PlayerController.Instance.RemoveMaskFromCharacter(characterController);
    }
}
