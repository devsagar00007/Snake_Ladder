using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private IObstacleController obstacleController;
    // Start is called before the first frame update
    private void Start()
    {
        obstacleController = GetComponent<IObstacleController>();
    }
    internal void StartObstacle()
    {
        obstacleController.StartObstacle();
    }
    internal void MovePlayerCallback()
    {
        PlayController.instance.ObstacleCallback();
    }
    
}
