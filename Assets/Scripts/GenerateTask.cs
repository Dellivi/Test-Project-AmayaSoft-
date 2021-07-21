using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateTask : MonoBehaviour
{
    [SerializeField] private BundleData bundleData;

    private TextMeshProUGUI textTask;
    private Level level;

    //[System.NonSerialized]
    public List<TaskObject> taskData = new List<TaskObject>();

    [System.NonSerialized]
    public int rand;

    TaskObject taskObject = new TaskObject();

    public TaskObject TaskObject { get => taskObject; protected set => taskObject = value; }

    private void Awake()
    {
        textTask = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        SetTask();
    }

    public void SetLevelObj(Level _level)
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
        return level.transform.GetChild(i).GetComponent<UseObject>().ObjectEntity.GetTaskObject();
    }

    public void RemoveTaskObj()
    {
        taskData.Remove(TaskObject);
    }

    public void UpdateTask()
    {
        TaskObject = GetTaskObject();

        if (TaskObject.taskName == null)
        {
            level.CreateLevel(); 
            RemoveTaskObj();
        }
        else
        {
            textTask.text = "Find " + TaskObject.taskName;
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