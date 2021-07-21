using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour, ILevel
{

    [SerializeField] private BundleData bundleData;
    [Space]
    [SerializeField] private GameObject prefab;

    private ObjectData levelData;
    private GenerateTask generateTask;

    private ShuffleObjects shuffle = new ShuffleObjects();

    private int objectCount = 3;

    public int randBundleData;

    public int ObjectCount { get => objectCount; set => objectCount = value; }

    private void OnEnable()
    {
        generateTask = gameObject.transform.parent.GetChild(0).GetComponent<GenerateTask>();
        //gameObject.transform.GetChild(0).GetComponent<Image>().sprite = taskObject.sprite;
        generateTask.SetLevelObj(this);
    }

    private void Start()
    {
        CreateLevel();
    }

    public void SetLevelSettings(int countObjects)
    {
        ObjectCount = countObjects;
        gameObject.GetComponent<GridLayoutGroup>().constraintCount = countObjects / 3;
    }

    public void CreateLevel()
    {
        List<TaskObject> a = GetUniqueTaskObjects();

        if(a.Count >= ObjectCount)
        {
            for (int i = 0; i < ObjectCount; i++)
            {
                CreateObject(a[i]);
            }

            generateTask.UpdateTask();
        }
        else
        {
            CreateLevel();
        }

    }

    public void CreateObject(TaskObject _taskObj)
    {
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
        UseObject a = obj.transform.GetComponent<UseObject>();

        a.ObjectEntity.SetTaskObject(_taskObj);

    }

    #region Getters
    private List<TaskObject> GetUniqueTaskObjects()
    { 
        randBundleData = Random.Range(0, bundleData.container.Length);
        levelData = bundleData.container[randBundleData];

        var i = shuffle.Shuffle(levelData.data);

        return i;
    }

    public GenerateTask GetGenerateTask()
    {
        return generateTask;
    }
    #endregion
}

public class ShuffleObjects
{
    private System.Random rand = new System.Random();

    private int GenerateAnotherNum(int from, int to)
    {
        return rand.Next(from, to);
    }

    public List<TaskObject> Shuffle(List<TaskObject> Sequence)
    {
        for (int s = 0; s < Sequence.Count - 1; s++)
        {
            int GenObj = GenerateAnotherNum(s, Sequence.Count);

            var h = Sequence[s];
            Sequence[s] = Sequence[GenObj];
            Sequence[GenObj] = h;
        }
        return Sequence;
    }
}
