using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using PrimeTween;

public class ClickDetection : MonoBehaviour
{
    [SerializeField] private string objectName;
    [SerializeField] private string objectDesc;
    [SerializeField] private TweenSettings<float> settings;
    [SerializeField] private ShakeSettings shakeSettings;
    [SerializeField] private Light2D objLight;
    [SerializeField] private Light2D glowLight;
    [SerializeField] private Sprite[] spritesList;
    [SerializeField] private GameObject scoreFollowMouse;
    [SerializeField] private AudioClip soundEffect;
    [SerializeField] private AudioSource soundPlayer;

    private SpriteRenderer spriteRenderer;
    [SerializeField] int score;
    private ParticleSystem particleSystems;
    private Sprite sprite;
    private bool hasBeenClicked = false;
    public float speedMultiplier = 1;
    private Sequence Bouncy;
    private int timesClicked = 0;
    private ComboManager comboManager;
    private Tween light1Tween;
    private Tween light2Tween;

    // Start is called before the first frame update
    void Start()
    {
        timesClicked = 0;
        comboManager = Camera.main.GetComponent<ComboManager>();

        var random = new System.Random();
        speedMultiplier = (float)random.NextDouble() + 1f;
        if (random.Next(0, 100) > 90)
        {
            speedMultiplier *= 2;
        }

        score = random.Next(4, 7) * 10;
        if (random.Next(0, 100) > GlobalManager.percentValue)
        {
            glowLight.intensity = 0;
            objLight.intensity = 0;
        }

        particleSystems = GetComponentInChildren<ParticleSystem>();
        particleSystems.Stop();

        sprite = spritesList[random.Next(spritesList.Length)];
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        particleSystems.textureSheetAnimation.SetSprite(0, sprite);

        Physics2DRaycaster physics2DRaycaster = FindObjectOfType<Physics2DRaycaster>();
        light1Tween = Tween.Custom(objLight.intensity, 0, 2, cycles: -1, cycleMode: CycleMode.Yoyo,
            ease: Ease.InOutSine,
            onValueChange: newVal => objLight.intensity = newVal);
        Tween.ShakeLocalRotation(transform, shakeSettings);
    }

    private IEnumerator Disappear()
    {
        light1Tween.Stop();
        soundPlayer.Stop();
        soundPlayer.clip = soundEffect;
        soundPlayer.Play();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        GameObject saveScoreFM = Instantiate(scoreFollowMouse, GameObject.FindGameObjectWithTag("Canvas").transform);
        saveScoreFM.GetComponent<ScoreFollowMouse>().value = (int)(score * GlobalManager.comboMultiplier);
        particleSystems.Play();
        Tween.Custom(objLight.intensity, 0, 1f, ease: Ease.OutSine,
            onValueChange: newVal => objLight.intensity = newVal);
        Tween.Custom(glowLight.intensity, 0, 1f, ease: Ease.OutSine,
            onValueChange: newVal => glowLight.intensity = newVal);

        Tween.Scale(transform,
            new Vector3(transform.localScale.x + 0.5f, transform.localScale.y + 0.5f, transform.localScale.z + 0.5f),
            0.7f, Ease.OutElastic);
        Tween tween = Tween.Alpha(spriteRenderer, 0, 1f);
        GlobalManager.playerScore += (int)(score * GlobalManager.comboMultiplier);
        comboManager.IncreaseCombo();
        hasBeenClicked = true;
        yield return tween.ToYieldInstruction();
        Destroy(gameObject, 0.1f);
    }

    void OnMouseDown()
    {
        if (!hasBeenClicked)
        {
            timesClicked++;
            Bouncy.Stop();
            Vector3 previousSize = transform.localScale;
            Bouncy = Sequence.Create()
                .Chain(Tween.Scale(transform,
                    endValue: new Vector3(previousSize.x + 0.2f, previousSize.y + 0.2f, previousSize.z + 0.2f),
                    duration: 0.3f, Ease.OutElastic)).Chain(Tween.Scale(transform, endValue: previousSize,
                    duration: 0.3f, Ease.OutElastic));
            if (timesClicked > 1)
            {
                StartCoroutine(Disappear());
            }
        }
    }


    // Update is called once per frame
    void LateUpdate()
    {
        float y = transform.position.y;
        var random = new System.Random();
        transform.position = new Vector2(transform.position.x, y - 0.005f * speedMultiplier);
        if (transform.position.y < -11)
        {
            Destroy(gameObject);
        }
    }
}