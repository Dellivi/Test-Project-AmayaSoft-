using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UseObject : MonoBehaviour
{
    private LevelObject levelObj;
    private GenerateTask task;
    private TaskObject taskObject;

    public GameObject particlePrefab;

    public UnityEvent CompleteEvent;
    public UnityEvent FailEvent;

    private void Start()
    {
        levelObj = gameObject.transform.parent.GetComponent<LevelObject>();
        task = levelObj.GetGenerateTask();

        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = taskObject.sprite;

        AddEvent(this.gameObject, EventTriggerType.PointerClick, delegate { OnPointerClick(); });
    }

    public void SetTaskObject(TaskObject _taskObj)
    {
        taskObject = _taskObj;
    }

    public TaskObject GetTaskObject()
    {
        return this.taskObject; 
    }

    private void OnPointerClick()
    {
        StartCoroutine(TaskObjectEquals());
    }

    private IEnumerator TaskObjectEquals()
    {
        if (task.TaskObj.taskName == taskObject.taskName)
        {
            CompleteEvent.Invoke();
            GameObject a = Instantiate(particlePrefab, transform);

            yield return new WaitForSeconds(1.1f);

            Destroy(a);
            task.RemoveTaskObj();
            levelObj.DoneTask();

        }
        else
        {
            FailEvent.Invoke();
        }
        StopCoroutine(TaskObjectEquals());

    }


    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (!trigger) { Debug.LogWarning("No EventTrigger component found!"); return; }
        var eventTrigger = new EventTrigger.Entry
        {
            eventID = type
        };
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
}
