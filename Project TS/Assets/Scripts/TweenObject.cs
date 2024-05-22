using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TweenObject : MonoBehaviour
{
    private Color savedColor;
    [SerializeField] private Color color;
    [SerializeField] TweenSettings<Vector3> settings;


    private void OnMouseEnter()
    {
        //Tween.Scale(transform, settings);
    }
    public void OnMouseExit(){
        //Tween.Scale(transform, settings.WithDirection(toEndValue: false));

    }

    // Start is called before the first frame update
    void Start()
    {
        savedColor = color;
        PrimeTweenConfig.warnEndValueEqualsCurrent = false;
    }

}
