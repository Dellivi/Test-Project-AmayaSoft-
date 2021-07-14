using UnityEngine.UI;

public class Level : LevelObject
{
    private void Start()
    {
        CreateLevel();
    }

    public void SetLevelSettings(int countObjects)
    {
        objectCount = countObjects;
        gameObject.GetComponent<GridLayoutGroup>().constraintCount = countObjects / 3;
    }



}
