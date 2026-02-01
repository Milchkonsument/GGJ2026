using Assets.Scripts.NPCs;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class CandyUIButton : MonoBehaviour, IPointerClickHandler
    {
        public CandyData AssociatedCandy;
        public TMP_Text amount;

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Clicked Candy");
            GiveCandyToSelectedNPC();
        }

        public void GiveCandyToSelectedNPC()
        {
            NPC_Selectable selectedNPC = NPC_Manager.Instance.selectedNPC;

            if (selectedNPC == null)
                return;

            // TODO count candy

            selectedNPC.characterController.ConsumeCandy(AssociatedCandy);
            // TOOD decrease coutn
        }

    }
}
