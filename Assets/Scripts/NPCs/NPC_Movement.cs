using Unity.VisualScripting;
using UnityEngine;

public class NPC_Movement : MonoBehaviour
{
    [Header("Walk Destinations")]
    [SerializeField] private Transform housePosition;
    [SerializeField] private Transform entrancePosition;

    [Header("Movement Settings")]
    [SerializeField]private float minSpeed = 0.3f;
    [SerializeField] private float maxSpeed = 2f;
    [SerializeField] private float speedChangeRate = 1f;
    
    private float walkSpeed;
    private bool increasing = false;
    private Animator animator;
    private bool isWalking;

    private void Start()
    {
        animator = GetComponent<Animator>();
        walkSpeed = Random.Range(minSpeed, maxSpeed);
    }

    private void FixedUpdate()
    {
        UpdateWalkSpeed();
        NPCWalk();
    }

    private void UpdateWalkSpeed()
    {
        if (increasing)
        {
            walkSpeed += speedChangeRate * Time.fixedDeltaTime;
            if (walkSpeed >= maxSpeed)
            {
                walkSpeed = maxSpeed;
                increasing = false;
            }
        }
        else
        {
            walkSpeed -= speedChangeRate * Time.fixedDeltaTime;
            if (walkSpeed <= minSpeed)
            {
                walkSpeed = minSpeed;
                increasing = true;
            }
        }
    }

    private void NPCWalk()
    {
        transform.position = new Vector3(transform.position.x + walkSpeed * Time.fixedDeltaTime, transform.position.y, transform.position.z);
        if(!isWalking)
        {
            animator.CrossFade("Walk", 0.5f);
            isWalking = true;
        }
    }

    private void StopWalking()
    {
        if(isWalking)
        {
            animator.CrossFade("Idle", 0.5f);
            isWalking = false;
        }
    }
}
