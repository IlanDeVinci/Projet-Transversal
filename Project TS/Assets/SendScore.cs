using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SendScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI usernameText;
    [SerializeField] private TextMeshProUGUI sentText;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip soundClip;


    [SerializeField] private Image fade;
    private bool hasBeenClicked = false;

    private IEnumerator Fade(int end)
    {
        Tween tween = Tween.Custom(fade.color.a, endValue: end, duration: 2, ease: Ease.InSine,
            onValueChange: newVal => fade.color = new Color(0, 0, 0, newVal));
        yield return tween.ToYieldInstruction();
    }

    // Start is called before the first frame update
    void Start()
    {
        fade.color = new Color(255, 255, 255, 1);
        StartCoroutine(EndCoroutine());
    }

    private IEnumerator EndCoroutine()
    {
        usernameText.text = GlobalManager.playerName;
        StartCoroutine(Fade(0));
        int finalscore = 0;
        int audioCounter = 0;
        while (finalscore <= GlobalManager.TotalScore)
        {
            if (audioCounter % 3 == 0)
            {
                soundSource.Stop();
                soundSource.clip = soundClip;
                soundSource.Play();
            }

            audioCounter++;

            finalscore += 10;
            scoreText.text = finalscore.ToString();
            yield return new WaitForSeconds(0.01f);
        }

        scoreText.text = GlobalManager.TotalScore.ToString();
    }

    private IEnumerator ClickedCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", GlobalManager.playerName);
        form.AddField("score", GlobalManager.TotalScore);
        sentText.text = "Chargement...";
        using (var w = UnityWebRequest.Post("fakeurl", form))
        {
            yield return w.SendWebRequest();
            if (w.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(w.error);
            }
            else
            {
                Debug.Log("Success");
            }
        }

        sentText.text = "Ton score a été soumis !";
    }

    public void Clicked()
    {
        if (!hasBeenClicked)
        {
            hasBeenClicked = true;
            StartCoroutine(ClickedCoroutine());
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}