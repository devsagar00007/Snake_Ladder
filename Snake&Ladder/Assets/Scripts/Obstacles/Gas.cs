using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour,IObstacleController
{
    private ObstacleManager obstacleManager;
    private GameObject child;
    [SerializeField] AudioClip poisonGasSound;
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
        SoundController.instance.PlaySoundOnce(poisonGasSound);
        Invoke("ActionafterAnimation", 5f);
    }

    public void ActionafterAnimation()
    {
        obstacleManager.MovePlayerCallback();
    }
}
