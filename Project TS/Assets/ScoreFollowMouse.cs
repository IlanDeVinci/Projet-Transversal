using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PrimeTween;

public class ScoreFollowMouse : MonoBehaviour
{
    private float offset;

    [SerializeField] private TextMeshProUGUI textm;
    public int value = 0;
    private Vector3 startPos;

    // Start is called before the first frame update
    private void Awake()
    {
        PrimeTweenConfig.warnTweenOnDisabledTarget = false;
        textm.text = "";
        textm.alpha = 1;
        textm.text = $"+{value}";
        Tween.Alpha(textm, 0, 2);
        Vector3 mouse = Input.mousePosition;
        transform.position = new Vector3(mouse.x, mouse.y, 0);
        startPos = transform.position;
        offset = 0;
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        textm.text = $"+{value}";
        offset += Time.deltaTime * 100;
        transform.position = new Vector3(startPos.x, startPos.y + offset, 0);
    }
}