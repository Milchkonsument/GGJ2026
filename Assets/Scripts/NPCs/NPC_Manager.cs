using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.NPCs
{
    public class NPC_Manager : Singleton<NPC_Manager>
    {

        public NPC_Selectable selectedNPC;
        public delegate void OnNPCSelected(NPC_Selectable selectedNPC);
        public OnNPCSelected onNPCSelected;


        public void SetSelectedNPC(NPC_Selectable newNPC)
        {

            if (newNPC == null)
                return;

            onNPCSelected(newNPC);

            Debug.Log("Set new selected NPC");
            Instance.selectedNPC = newNPC;
        }

    }
}
    