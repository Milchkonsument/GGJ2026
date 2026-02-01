using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        HouseUIManager.OnPlanningStarted += StopCamera;
    }

    private void OnDisable()
    {
        HouseUIManager.OnPlanningStarted -= StopCamera;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        //Move camera to the right
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }

    private void StopCamera()
    {
        moveSpeed = 0f;
    }   
}
