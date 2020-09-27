using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using System.Text;
using System.IO;

public class RollerAgent : Agent
{
    public Transform Target;
    Rigidbody rBody;
    public ObstacleManager obstacle;
    [HideInInspector]
    private StreamWriter sw;
    private static string logName = "RollerBallSummaryLog.csv";
    private int episodeCount = 0;

    void Start ()
    {
        rBody = GetComponent<Rigidbody>();
        if (IsTrainingMode() == false) 
        {
            string datetimeStr = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string filePath = Application.dataPath + "/Logs";
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
            string logFile = filePath + "/" + datetimeStr + logName;
            sw = new StreamWriter(logFile, true, Encoding.UTF8);
            // header
            SaveLog(0);
        }
    }

    void OnApplicationQuit()
    {
        if (IsTrainingMode() == false)
        {
            sw.Flush();
            sw.Close();
        }
    }

    public override void OnEpisodeBegin()
    {
        if (this.transform.localPosition.y < 0)
        {
            // If the Agent fell, zero its momentum
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3( 0, 0.5f, 0);
        }

        // Move the target to a new spot
        Target.localPosition = new Vector3(Random.value * 8 - 4,
                                           0.5f,
                                           Random.value * 8 - 4);
        obstacle.ResetObstacles();
        episodeCount++;
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        // Actions, Descrete, Branch size = 1, 5 steps
        int action = (int)vectorAction[0];
        Vector3 controlSignal = Vector3.zero;
        if (action == 1) controlSignal.z -= 0.1f;
        if (action == 2) controlSignal.z += 0.1f;
        if (action == 3) controlSignal.x -= 0.1f;
        if (action == 4) controlSignal.x += 0.1f;
        this.transform.localPosition += controlSignal;

        // Calculate distance to target
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);

        // Rewards
        // Reached target
        if (distanceToTarget < 1.42f)
        {
            if (IsTrainingMode() == false) SaveLog(1);
            SetReward(1.0f);
            EndEpisode();
            return;
        }

        // Collided with obstacles
        foreach (var actor in obstacle.obstacleObjs)
        {
            float distanceToActor = Vector3.Distance(this.transform.localPosition, actor.transform.localPosition);
            if (distanceToActor < 1.42f)
            {
                if (IsTrainingMode() == false) SaveLog(2);
                SetReward(-1.0f);
                EndEpisode();
                break;
            }
        }

        // Fell off platform
        if (this.transform.localPosition.y < 0)
        {
            if (IsTrainingMode() == false) SaveLog(3);
            SetReward(-1.0f);
            EndEpisode();
        }
    }

    public override void Heuristic(float[] actionsOut)
    {
        // actionsOut[0] = Input.GetAxis("Horizontal");
        // actionsOut[1] = Input.GetAxis("Vertical");
        float[] action = new float[1];
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (v < 0.0f) actionsOut[0] = 1;
        if (v > 0.0f) actionsOut[0] = 2;
        if (h < 0.0f) actionsOut[0] = 3;
        if (h > 0.0f) actionsOut[0] = 4;
    }

    public static bool IsTrainingMode()
    {
        // return Academy.Instance.IsCommunicatorOn;
        return false;
    }

    public void SaveLog(int status)
    {
        if (IsTrainingMode()) return;

        string message = "";
        string[] record = new string[6];

        switch(status)
        {
            // Write header in csv
            case 0:
                message = "Write a header in CSV.";
                record[0] = "play_id";
                record[1] = "number_of_obstacles";
                record[2] = "cleared?";
                record[3] = "status";
                record[4] = "message";
                record[5] = "created_at";
                break;

            case 1:
                message = "Success! You reached a target.";
                record[0] = episodeCount.ToString();
                record[1] = obstacle.numObstacles.ToString();
                record[2] = "True";
                record[3] = status.ToString();
                record[4] = message;
                record[5] = System.DateTime.Now.ToString();
                break;

            case 2:
                message = "Failed! You collided with an abstacle.";
                record[0] = episodeCount.ToString();
                record[1] = obstacle.numObstacles.ToString();
                record[2] = "False";
                record[3] = status.ToString();
                record[4] = message;
                record[5] = System.DateTime.Now.ToString();
                break;

            case 3:
                message = "Failed! You fell off the floor.";
                record[0] = episodeCount.ToString();
                record[1] = obstacle.numObstacles.ToString();
                record[2] = "False";
                record[3] = status.ToString();
                record[4] = message;
                record[5] = System.DateTime.Now.ToString();
                break;
            
            default:
                break;
        }
        Debug.Log(message);
        string csv = string.Join(",", record);
        sw.WriteLine(csv);
        sw.Flush();
    }
}
