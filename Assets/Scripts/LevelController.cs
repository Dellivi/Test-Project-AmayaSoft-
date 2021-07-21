using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{

    public GameObject prefab;
    public GameObject gameOverPrefab;
    private GameObject levelObj;

    private Level level;

    private Tweening tween = new Tweening();

    int addCount = 3;
    int levelCount = 0;
    readonly int levelMax = 5;

    private void Start()
    {
        StartCoroutine(CreateLevel());
    }

    public IEnumerator CreateLevel()
    {
        Debug.Log("Create Level");
        levelObj = Instantiate(prefab, transform);
        level = levelObj.transform.GetComponent<Level>();

        yield return new WaitForSeconds(0.3f);

        if (levelCount == 0)
        {
            StartCoroutine(tween.BounceObjects(levelObj.transform));
        }

        yield break;
    }

    public void NextLevel()
    {
        Destroy(levelObj);

        levelCount++;

        if (levelCount < levelMax)
        {
            addCount += 3;

            StartCoroutine(CreateLevel());
            level.SetLevelSettings(addCount);
        }
        else
        {
            Instantiate(gameOverPrefab, transform);

            levelCount = 0;
            addCount = 3;

            Debug.Log("End");
        }
    }

    public void RestartLevel()
    {
        StartCoroutine(CreateLevel());
        level.SetLevelSettings(addCount);
    }
}
