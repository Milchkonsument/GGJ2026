using Assets.Scripts.NPCs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class NPC_Selectable : MonoBehaviour
{
    public MaskData equippedMask;

    public void OnMouseDown()
    {
        Debug.Log("clicked NPC");
        
        NPC_Manager.Instance.SetSelectedNPC(this);
    }

    public void ConsumeCandy(CandyData candy)
    {

    }

    public void EquipMask(MaskData mask)
    {
        equippedMask = mask;
    }

    public void UnequipMask()
    {
        equippedMask = null;
    }
}
