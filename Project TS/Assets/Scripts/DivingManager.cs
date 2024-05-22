using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DivingManager : MonoBehaviour
{
    public bool canOxygenDeplete = false;
    [SerializeField] private CameraFollowMouse cameraFollow;
    [SerializeField] private OxygenManager oxygenManager;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI overText;
    [SerializeField] private Image overImage;
    [SerializeField] private Image fade;
    [SerializeField] private GameObject dechetPrefab;

    [SerializeField] private float delay = 2;

    // Start is called before the first frame update
    void Start()
    {
        overText.gameObject.SetActive(false);
        overImage.gameObject.SetActive(false);

        GlobalManager.playerScore = 0;
        cameraFollow.canMove = false;
        canOxygenDeplete = false;
        fade.color = new Color(0, 0, 0, 1);
        StartCoroutine(Fade(0));
        StartCoroutine(GameplayStart());
    }

    void Awake()
    {
        GlobalManager.amountOfTimesPlayed += 1;
    }

    private IEnumerator Fade(int end)
    {
        Tween tween = Tween.Custom(fade.color.a, endValue: end, duration: 2, ease: Ease.InSine,
            onValueChange: newVal => fade.color = new Color(0, 0, 0, newVal));
        yield return tween.ToYieldInstruction();
    }

    private void Update()
    {
        scoreText.text = $"Score : {GlobalManager.playerScore}";
    }

    private IEnumerator SpawnTrash()
    {
        while (!oxygenManager.isOxygenDepleted())
        {
            var random = new System.Random();
            Vector2 randomPos = new Vector2(random.Next(-18, 18), 11);
            Instantiate(dechetPrefab, randomPos, Quaternion.identity);
            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator GameplayStart()
    {
        yield return new WaitUntil(() => dialogueManager.isDialogueFinished);
        canOxygenDeplete = true;
        oxygenManager.StartDeplete();
        cameraFollow.canMove = true;
        StartCoroutine(SpawnTrash());
        yield return new WaitUntil(() => oxygenManager.isOxygenDepleted());
        if (GlobalManager.amountOfTimesPlayed < 3)
        {
            StartCoroutine(ShopTransition());
        }
        else
        {
            StartCoroutine(FinishedGame());
        }
    }

    private IEnumerator FinishedGame()
    {
        overText.alpha = 0;
        overText.text = "Vous avez réussi !";
        overText.gameObject.SetActive(true);
        overImage.color = new Color(255, 255, 255, 0);
        overImage.gameObject.SetActive(true);
        Tween.Color(overImage, new Color(255, 255, 255, 1), 1);
        Tween tween = Tween.Alpha(overText, 1, 1);
        yield return tween.ToYieldInstruction();
        fade.color = new Color(0, 0, 0, 0);
        GlobalManager.TotalScore += GlobalManager.playerScore;
        yield return StartCoroutine(Fade(1));
        SceneManager.LoadSceneAsync("End");
    }

    private IEnumerator ShopTransition()
    {
        overText.alpha = 0;
        overText.gameObject.SetActive(true);
        overImage.color = new Color(255, 255, 255, 0);
        overImage.gameObject.SetActive(true);
        Tween.Color(overImage, new Color(255, 255, 255, 1), 1);
        Tween tween = Tween.Alpha(overText, 1, 1);
        yield return tween.ToYieldInstruction();
        fade.color = new Color(0, 0, 0, 0);
        GlobalManager.playerMoney += (2 * GlobalManager.playerScore);
        GlobalManager.TotalScore += GlobalManager.playerScore;
        yield return StartCoroutine(Fade(1));
        SceneManager.LoadSceneAsync("Shop");
    }
}