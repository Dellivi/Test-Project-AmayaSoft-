using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private LevelManager levelManager;
    private Tweening tween;
    private Image image;

    private void OnEnable()
    {
        levelManager = gameObject.transform.parent.GetChild(1).GetComponent<LevelManager>();
        tween = gameObject.transform.parent.GetChild(1).GetComponent<Tweening>();
        image = gameObject.transform.GetComponent<Image>();
    }

    private void Start()
    {
        tween.FadeImage(image, 0.5f, 1f);

    }

    public void Restart()
    {
        StartCoroutine(RestartCoroutine());
    }

    private IEnumerator RestartCoroutine()
    {
        tween.FadeImage(image, 1f, 1f);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        tween.FadeImage(image, 0f, 0.7f);

        yield return new WaitForSeconds(0.3f);

        StartCoroutine(levelManager.CreateLevel());

        yield return new WaitForSeconds(0.5f);

        Destroy(this.gameObject);
    }

}
