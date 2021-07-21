using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private LevelController levelManager;
    private Tweening tween = new Tweening();
    private Image image;

    private void OnEnable()
    {
        levelManager = gameObject.transform.parent.GetComponent<LevelController>();
        image = gameObject.transform.GetComponent<Image>();
    }

    private void Start()
    {
        tween.FadeImage(image, 0.5f, 1f);
    }

    public void RestartGame()
    {
        StartCoroutine(RestartGameCoroutine());
    }

    private IEnumerator RestartGameCoroutine()
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
