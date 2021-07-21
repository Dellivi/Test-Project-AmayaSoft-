using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UseObject : MonoBehaviour
{
    private Level levelObj;
    private LevelController levelController;
    private GenerateTask task;

    public GameObject particlePrefab;

    private TaskObjectEntity objectEntity = new TaskObjectEntity();
    private Tweening tween = new Tweening();

    public TaskObjectEntity ObjectEntity { get => objectEntity; protected set => objectEntity = value; }

    private void Start()
    {
        levelObj = gameObject.transform.parent.GetComponent<Level>();
        levelController = levelObj.transform.parent.GetComponent<LevelController>();
        task = levelObj.GetGenerateTask();
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = ObjectEntity.GetTaskObject().sprite;

        AddEvent(this.gameObject, EventTriggerType.PointerClick, delegate { OnPointerClick(); });
    }

    private void OnPointerClick()
    {
        StartCoroutine(TaskObjectEquals());
    }

    private IEnumerator TaskObjectEquals()
    {
        if (task.TaskObject.taskName == ObjectEntity.GetTaskObject().taskName)
        {
            tween.Bounce(this.transform);
            yield return new WaitForSeconds(.1f);

            GameObject a = Instantiate(particlePrefab, transform);
            yield return new WaitForSeconds(1f);

            Destroy(a);
            levelController.NextLevel();
        }
        else
        {
            tween.EasyInBounce(this.transform);
        }

        yield break;
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

public class TaskObjectEntity
{

    TaskObject taskObject;

    public virtual void SetTaskObject(TaskObject taskObject)
    {
        this.taskObject = taskObject;
    }

    public TaskObject GetTaskObject()
    {
        return this.taskObject;
    }
}
