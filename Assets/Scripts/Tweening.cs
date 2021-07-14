using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class Tweening : MonoBehaviour
{
    public void Bounce(Transform transform)
    {
        transform.DOPunchScale(new Vector3(1,1,0), 0.8f, 0, 1);
    }

    public void EasyInBounce(Transform transform)
    {
        transform.DOPunchPosition(new Vector3(4f, 0f, 0f), 1f, 10, 1);
    }

    public void FadeInText(TextMeshProUGUI text)
    {
        text.alpha = 0;
        text.DOFade(1, 2f);
    }

    public void FadeImage(Image image, float value, float time)
    {
        image.alphaHitTestMinimumThreshold = 0;
        image.DOFade(value, time);
    }

}