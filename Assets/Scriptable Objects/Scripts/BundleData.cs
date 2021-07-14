using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Bundle Data", menuName = "ObjectData/ new Bundle Data", order = 1)]
public class BundleData : ScriptableObject
{
    public ObjectData[] container;

}