using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class ComboManager : MonoBehaviour
{
    [SerializeField] private Slider boostSlider;
    [SerializeField] private TextMeshProUGUI boostText;
    private float boostMaxDuration = 4f;
    private float boostDuration;

    private void Start()
    {
        boostDuration = 0;
        boostText.text = $"{GlobalManager.comboValue}x";
        boostSlider.value = 0;
    }

    public void IncreaseCombo()
    {
        boostDuration = boostMaxDuration;
        GlobalManager.comboValue += 1;
    }

    private void Update()
    {
        boostSlider.value = boostDuration / boostMaxDuration;
        boostDuration -= Time.deltaTime;
        switch (GlobalManager.comboValue)
        {
            case 0:
                GlobalManager.comboMultiplier = 1f;
                break;
            case 5:
                GlobalManager.comboMultiplier = 1.25f;
                break;
            case 10:
                GlobalManager.comboMultiplier = 1.5f;
                break;
            case 15:
                GlobalManager.comboMultiplier = 1.75f;
                break;
            case 25:
                GlobalManager.comboMultiplier = 2f;
                break;
            case 50:
                GlobalManager.comboMultiplier = 5f;
                break;
        }

        boostText.text = $"{GlobalManager.comboValue}x";
        if (boostDuration <= 0)
        {
            GlobalManager.comboValue = 0;
        }
    }
}