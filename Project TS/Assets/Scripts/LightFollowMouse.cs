using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFollowMouse : MonoBehaviour
{
    [SerializeField] private Camera mcamera;
    [SerializeField] private Light2D mlight;

    private void Start()
    {
        if (GlobalManager.lightLevel != 0)
        {
            mlight.intensity = GlobalManager.lightLevel;
            mlight.pointLightOuterRadius = GlobalManager.lightLevel * 2.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        transform.position = mcamera.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, 0));
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}