using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Vector2 cameraOffset;
    [SerializeField] private Vector2 cameraSmoothing;
    [SerializeField] private Vector2 cameraDeadzone;
    // Start is called before the first frame update
    private Vector3 cameraPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CalculateCameraPosition();
        transform.position = cameraPosition;
    }

    private void CalculateCameraPosition()
    {
        cameraPosition.x = Mathf.Lerp(transform.position.x,player.position.x + cameraOffset.x, Time.deltaTime * cameraSmoothing.x);
        cameraPosition.y = Mathf.Lerp(transform.position.y,player.position.y + cameraOffset.y, Time.deltaTime *cameraSmoothing.y);
        if(cameraPosition.x - player.position.x > cameraDeadzone.x)
        {
            cameraPosition.x = player.position.x + cameraDeadzone.x;
        } else if(cameraPosition.x - player.position.x < -cameraDeadzone.x)
        {
            cameraPosition.x = player.position.x - cameraDeadzone.x;

        }
        if (cameraPosition.y - player.position.y > cameraDeadzone.y)
        {
            cameraPosition.y = player.position.y + cameraDeadzone.y;
        } else if(cameraPosition.y - player.position.y < -cameraDeadzone.y)
        {
            cameraPosition.y = player.position.y - cameraDeadzone.y;

        }
        cameraPosition.z = -10;
    }
}
