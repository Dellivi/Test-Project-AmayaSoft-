using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelObject : MonoBehaviour
{
    [SerializeField] private BundleData bundleData;
    [Space]
    [SerializeField] private GameObject prefab;

    private ObjectData levelData;
    private GenerateTask generateTask;
    private LevelManager manager;

    public int objectCount;

    private static System.Random rand = new System.Random();
    int randTaskObject;
    int randBundleData;

    private static int GenerateAnotherNum(int from, int to)
    {
        return rand.Next(from, to);
    }

    public int RandTaskObject { get => randTaskObject; protected set => randTaskObject = value; }
    public int RandBundleData { get => randBundleData; set => randBundleData = value; }

    private void OnEnable()
    {
        generateTask = gameObject.transform.parent.GetChild(0).GetComponent<GenerateTask>();
        manager = gameObject.transform.parent.GetComponent<LevelManager>();
        generateTask.SetLevelObj(this);
    }

    public void CreateLevel()
    {

        GetRandomBundle();
        List<TaskObject> a = GetUniqueTaskObjects();


        for (int i = 0; i < objectCount; i++)
        {
            CreateObject(a[i]);
        }

        UpdateTask();
        

    }

    public void UpdateTask()
    {
        generateTask.UpdateTask();
    }

    public TaskObject GetTaskObject(int childIndex)
    {
        return transform.GetChild(childIndex).GetComponent<UseObject>().GetTaskObject();
    }


    public void DoneTask()
    {
        manager.NextLevel();
    }

    private void CreateObject(TaskObject _taskObj)
    {
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
        UseObject a = obj.transform.GetComponent<UseObject>();

        a.SetTaskObject(_taskObj);

    }

    #region Getters
    private void GetRandomBundle()
    {
        RandBundleData = Random.Range(0, bundleData.container.Length);

        levelData = bundleData.container[RandBundleData];
    }

    private List<TaskObject> GetUniqueTaskObjects()
    {
        var i = Shuffle(levelData.data);
        return i;
    }

    public GenerateTask GetGenerateTask()
    {
        return generateTask;
    }

    public ObjectData GetLevelData()
    {
        return this.levelData;
    }
    #endregion


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



