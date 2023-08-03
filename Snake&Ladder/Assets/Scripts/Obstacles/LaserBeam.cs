using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour,IObstacleController
{
    private ObstacleManager obstacleManager;
    private GameObject child;
    [SerializeField] AudioClip LaserSound;
    // Start is called before the first frame update
    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        obstacleManager = GetComponent<ObstacleManager>();
    }
    public void StartObstacle()
    {
        Invoke("PlayFire", 1f);
    }
    private void PlayFire()
    {
        child.SetActive(true);
        SoundController.instance.PlaySoundOnce(LaserSound);
        Invoke("ActionafterAnimation", 3f);
    }

    public void ActionafterAnimation()
    {
        obstacleManager.MovePlayerCallback();
    }
}
