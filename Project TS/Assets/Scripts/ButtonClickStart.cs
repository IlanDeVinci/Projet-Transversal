using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PrimeTween;

public class ButtonClickStart : MonoBehaviour
{
    [SerializeField] private Image image;
    public void StartGame()
    {
        StartCoroutine(Fade());
     
    }
    private IEnumerator Fade()
    {
        Tween tween = Tween.Custom(startValue:0, endValue:1, duration:1, onValueChange: newVal => image.color = new Color(0,0,0,newVal));
        yield return new WaitUntil(() => !tween.isAlive);
        SceneManager.LoadSceneAsync("Stage1");
    }

}
