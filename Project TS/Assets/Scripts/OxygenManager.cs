using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using PrimeTween;

[Serializable]
public class OxygenManager : MonoBehaviour
{
    [SerializeField] private Slider oxygenSlider;
    [SerializeField] private int maxOxygen = 100;
    [SerializeField] private int oxygenDepleteSpeed = 2;
    private int currentOxygen = 100;

    private void Awake()
    {
        oxygenSlider.value = maxOxygen;
        currentOxygen = maxOxygen;
    }


    public void StartDeplete()
    {
        StartCoroutine(OxygenDeplete());
    }

    public bool isOxygenDepleted()
    {
        return currentOxygen <= 0;
    }

    private IEnumerator OxygenDeplete()
    {
        while (currentOxygen > 0)
        {
            currentOxygen -= oxygenDepleteSpeed;
            Tween tween = Tween.Custom(startValue: oxygenSlider.value, (float)currentOxygen / maxOxygen, 1,
                onValueChange: newVal => oxygenSlider.value = newVal);
            yield return tween.ToYieldInstruction();
        }
    }
}