using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConfigJson
{
    public Setting training;
    public Setting simulation;
}

[System.Serializable]
public class Setting
{
    public int[] numObstacles;
}

public class ObstacleManager : MonoBehaviour
{
    public GameObject Obstacle;
    public int numObstacles = 1;
    private int[] numObstaclesArray;
    [HideInInspector]
    public List<GameObject> obstacleObjs;
    private static string configFileName = "RollerBallConfig";

    // Start is called before the first frame update
    void Start()
    {
        obstacleObjs = new List<GameObject>();
        GetConfigParams();

    }

    // Reset obstacle objects and create new instances
    public void ResetObstacles()
    {
        foreach (var obstacle in obstacleObjs)
        {
            DestroyImmediate(obstacle);
        }
        obstacleObjs.Clear();

        if (numObstaclesArray != null && numObstaclesArray.Length != 0)
        {
            numObstacles = numObstaclesArray[Random.Range(0, numObstaclesArray.Length)];
            Debug.Log(numObstacles);
        }

        for (var i = 0; i < numObstacles; i++)
        {
            var obstacleObj = Instantiate(Obstacle, this.transform.position, Quaternion.identity);
            obstacleObj.transform.localPosition = new Vector3(Random.value * 8 - 4,
                                                        0.5f,
                                                        Random.value * 8 - 4);
            obstacleObjs.Add(obstacleObj);
        }
    }

    private void GetConfigParams()
    {
        string configString = Resources.Load<TextAsset>(configFileName).ToString();
        ConfigJson obj = JsonUtility.FromJson<ConfigJson>(configString);
        numObstaclesArray = obj.simulation.numObstacles;
    }
}
