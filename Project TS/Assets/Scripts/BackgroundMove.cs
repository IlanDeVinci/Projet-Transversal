using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;
public class BackgroundMove : MonoBehaviour
{
    [SerializeField] ShakeSettings settings;
    private Tween tween;
    // Start is called before the first frame update
    void Start()
    {
       tween= Tween.ShakeLocalPosition(transform, settings);
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        tween.Stop();   
    }
}
