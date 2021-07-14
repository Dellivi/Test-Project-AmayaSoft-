using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{

    public GameObject prefab;
    public GameObject gameOverPrefab;
    private GameObject levelObj;

    private Tweening tween;
    private Level level;

    public UnityEvent TextEvent;

    int count = 3;
    int levelCount = 0;
    readonly int levelMax = 3;

    private void Awake()
    {
        tween = gameObject.transform.GetComponent<Tweening>();
    }

    private void Start()
    {
        StartCoroutine(CreateLevel());
        TextEvent.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextLevel();
        }

    }

    public IEnumerator CreateLevel()
    {
        Debug.Log("Create Level");
        levelObj = Instantiate(prefab, transform);
        level = levelObj.transform.GetComponent<Level>();

        yield return new WaitForSeconds(0.2f);

        if (levelCount == 0)
        {
            TextEvent.Invoke();
            StartChildTween();
        }

        StopCoroutine(CreateLevel());

    }

    public void NextLevel()
    {
        Destroy(levelObj);

        levelCount++;

        if (levelCount < levelMax)
        {
            count += 3;

            StartCoroutine(CreateLevel());
            level.SetLevelSettings(count);
        }
        else
        {
            Instantiate(gameOverPrefab, transform.parent);

            levelCount = 0;
            count = 3;

            Debug.Log("End");
        }
    }

    public void Restart()
    {
        StartCoroutine(CreateLevel());
        level.SetLevelSettings(count);
    }

    public void StartChildTween()
    {
        StartCoroutine(ChildTween(levelObj.transform));
    }

    private IEnumerator ChildTween(Transform transform)
    {
        int i = 0;
        while (transform.childCount > i)
        {
            tween.Bounce(transform.GetChild(i).transform);
            i++;

            yield return new WaitForSeconds(0.8f);
        }
        StopCoroutine(ChildTween(transform));
    }
}
