using Assets.Scripts.NPCs;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaidPlanningUIController : MonoBehaviour
{
    public GameObject maskContainer;
    public GameObject maskEntryPrefab;
    public GameObject StartButton;

    public MaskData[] avialableMask;

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

        foreach (MaskData mask in avialableMask)
        {
            var newMaskObject = Instantiate(maskEntryPrefab, maskContainer.transform);
            MaskEntryController newMaskEntry = newMaskObject.GetComponent<MaskEntryController>();

            currentMaskEntries.Add(newMaskEntry);
            newMaskEntry.init(mask);
            newMaskEntry.onMaskSelected += OnMaskSelected;

            if (NPC.equippedMask == mask)
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
