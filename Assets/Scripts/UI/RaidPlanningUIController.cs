using Assets.Scripts.NPCs;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaidPlanningUIController : MonoBehaviour
{
    public GameObject maskContainer;
    public GameObject maskEntryPrefab;
    public GameObject StartButton;

    public MaskController[] avialableMask;

    public List<MaskEntryController> currentMaskEntries;


    public void Awake()
    {
        //avialableMask = new MaskData[12];
        currentMaskEntries = new List<MaskEntryController>();
        NPC_Manager.Instance.onNPCSelected += OnNpcSelected;
    }

    public void OnDestroy()
    {
        NPC_Manager.Instance.onNPCSelected -= OnNpcSelected;
    }

    public void OnNpcSelected(NPC_Selectable NPC)
    {
        Debug.Log("on NPC SELECTED delegate");

        foreach (Transform maskEntry in maskContainer.transform)
        {
            Destroy(maskEntry.gameObject);
        }
        currentMaskEntries = new List<MaskEntryController>();

        var availableMasks = PlayerController.Instance.UnassignedMasks;
        availableMasks.Add(NPC.characterController.Mask);

        foreach (MaskController mask in availableMasks)
        {
            if (mask == null)
                continue;

            if (mask.Data == null)
                continue;

            var newMaskObject = Instantiate(maskEntryPrefab, maskContainer.transform);
            MaskEntryController newMaskEntry = newMaskObject.GetComponent<MaskEntryController>();

            currentMaskEntries.Add(newMaskEntry);
            newMaskEntry.init(mask);
            newMaskEntry.onMaskSelected += OnMaskSelected;

            if (NPC.characterController.Mask == mask.Data)
            {
                newMaskEntry.bIsSelected = true;
                newMaskEntry.UpdateSelected();
            }

        }
    }

    public void OnMaskSelected(MaskEntryController maskEntryController)
    {
        foreach( var Entry in currentMaskEntries)
        {
            if(Entry != maskEntryController)
            {
                Entry.bIsSelected = false;
                Entry.UpdateSelected();
            }
        }
    }

    public void StartRaid()
    {
        // TODO
    }

}
