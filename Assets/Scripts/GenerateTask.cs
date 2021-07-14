using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Linq;

public  class GenerateTask : MonoBehaviour
{
    [SerializeField] private BundleData bundleData;

    private TextMeshProUGUI textTask;
    private LevelObject level;

    [System.NonSerialized]
    public List<TaskObject> taskData = new List<TaskObject>();

    [System.NonSerialized]
    public int rand;

    TaskObject taskObj = new TaskObject();
    public TaskObject TaskObj { get => taskObj; set => taskObj = value; }

    private void Awake()
    {
        textTask = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        SetTask();
    }

    public void SetLevelObj(LevelObject _level)
    {
        this.level = _level;
    }

    public void SetTask()
    {

        for (int i = 0; i < bundleData.container.Length; i++)
        {
            for(int j = 0; j < bundleData.container[i].data.Count; j++)
            {
                taskData.Add(bundleData.container[i].data[j]);
            }
        }
    }
    
    public TaskObject GetLevelTaskObject(int i)
    {
        return level.transform.GetChild(i).GetComponent<UseObject>().GetTaskObject();
    }

    public void RemoveTaskObj()
    {
        taskData.Remove(TaskObj);
        
    }

    public void UpdateTask()
    {
        TaskObj = GetTaskObject();

        if (TaskObj.taskName == null)
        {
            level.CreateLevel();
        }
        else
        {
            textTask.text = "Find " + TaskObj.taskName;
        }
    }

    public TaskObject GetTaskObject()
    {
        var a = new TaskObject();
        for (int i = Random.Range(0, level.transform.childCount); i < level.transform.childCount; i++)
        {
            for (int j = 0; j < taskData.Count; j++)
            {
                if (GetLevelTaskObject(i).Equals(taskData[j]))
                {
                    a = taskData[j];
                    return a;
                }
            }
        }
        return a;
    }
}