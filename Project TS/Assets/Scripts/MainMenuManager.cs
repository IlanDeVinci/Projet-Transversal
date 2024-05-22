using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject menuPopUp;
    [SerializeField] private Image image;
    [SerializeField] private Button startButton;
    [SerializeField] private TweenButton tweenButton;
    [SerializeField] private TextMeshProUGUI input;
    [SerializeField] private Image outline;
    [SerializeField] private Sprite outline1080;
    [SerializeField] private Sprite outline1200;

    private CanvasGroup canvasGroup;
    private Tween tween;
    private Tween tweenButtonColor;


    private void Start()
    {
        if (Screen.currentResolution.ToString().Contains("1200"))
        {
            outline.sprite = outline1200;
        }
        else
        {
            outline.sprite = outline1080;
        }

        PrimeTweenConfig.warnTweenOnDisabledTarget = false;
        canvasGroup = menuPopUp.GetComponent<CanvasGroup>();
        menuPopUp.SetActive(false);
        startButton.interactable = false;
    }

    public void HideStartPopup()
    {
        menuPopUp.SetActive(false);
    }

    private void Update()
    {
        if (input.text.Length >= 3)
        {
            tweenButtonColor.Stop();
            startButton.interactable = true;
            tweenButtonColor = Tween.Color(startButton.image, new Color(1, 1, 1, 1), 1);
        }
        else
        {
            tweenButton.tween.Stop();
            tweenButton.tweenScale.Stop();

            tweenButtonColor.Stop();
            startButton.interactable = false;
            tweenButtonColor = Tween.Color(startButton.image, new Color(0.3f, 0.3f, 0.3f, 0.77f), 1);
        }
    }

    public void StartGame()
    {
        GlobalManager.playerName = input.text;
        GlobalManager.hasDoneTutorial = false;
        GlobalManager.lightLevel = 0;
        GlobalManager.speedLevel = 0;
        GlobalManager.percentOfGlowingLevel = 0;
        GlobalManager.lightValue = 1;
        GlobalManager.speedValue = 1;
        GlobalManager.clickRadius = 0;
        GlobalManager.percentValue = 20;
        GlobalManager.comboMultiplier = 1;
        GlobalManager.comboValue = 0;
        GlobalManager.amountOfTimesPlayed = 0;
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        Tween tween = Tween.Custom(startValue: 0, endValue: 1, duration: 1,
            onValueChange: newVal => image.color = new Color(0, 0, 0, newVal));
        yield return new WaitUntil(() => !tween.isAlive);
        tween.Stop();
        SceneManager.LoadSceneAsync("Stage1");
    }

    public void AskUsername()
    {
        tween.Stop();
        menuPopUp.SetActive(true);
        canvasGroup.alpha = 0;
        tween = Tween.Alpha(canvasGroup, 1, 1);
    }

    private void OnDestroy()
    {
        tween.Stop();
        tweenButton.tween.Stop();
        tweenButton.tweenScale.Stop();
        tweenButtonColor.Stop();
    }
}