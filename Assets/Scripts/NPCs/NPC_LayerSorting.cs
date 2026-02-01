using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC_LayerSorting : MonoBehaviour
{
    public static NPC_LayerSorting Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private int baseSortingOrder = 5;
    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    private void Start()
    {
        SortKids();
    }

    private void CollectKids()
    {
        GameObject[] kids = GameObject.FindGameObjectsWithTag("Kid");
        foreach (GameObject kid in kids)
        {
            SpriteRenderer sr = kid.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                spriteRenderers.Add(sr);
            }
        }
    }

    public void SortKids()
    {
        spriteRenderers.Clear();
        CollectKids();
        spriteRenderers.Sort((a, b) => b.transform.position.y.CompareTo(a.transform.position.y));
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            spriteRenderers[i].sortingOrder = i + baseSortingOrder;
            var NPCSlectable = spriteRenderers[i].gameObject.GetComponent<NPC_Selectable>();

            if (NPCSlectable == null)
                continue;

            var maskOverlay = NPCSlectable.maskOverlay;

            if (maskOverlay == null)
                continue;

            maskOverlay.sortingOrder = i + baseSortingOrder + 1;
        }
    }
}
