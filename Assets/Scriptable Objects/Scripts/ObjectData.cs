using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Object Data", menuName = "ObjectData/new Object Data", order = 2)]
public class ObjectData : ScriptableObject
{
    public List<TaskObject> data;

}

[System.Serializable]
public class TaskObject
{
    public string taskName;
    public Sprite sprite;
}
