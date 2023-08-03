using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour,IObstacleController
{
    private Animator animator;
    [SerializeField] private GameObject blastEffect;
    private ObstacleManager obstacleManager;
    private GameObject child;
    [SerializeField] AudioClip asteroidSound;
    // Start is called before the first frame update
    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        animator = GetComponent<Animator>();
        obstacleManager = GetComponent<ObstacleManager>();
    }
    public void StartObstacle()
    {
        child.SetActive(true);
        animator.SetTrigger("Start");
        SoundController.instance.PlaySoundOnce(asteroidSound);
    }

    public void ActionafterAnimation()
    {
        blastEffect.gameObject.SetActive(true);
        child.SetActive(false);
        Invoke("MovePlayerCallback", 1f);
    }
    public void MovePlayerCallback()
    {
        obstacleManager.MovePlayerCallback();
    }
}
