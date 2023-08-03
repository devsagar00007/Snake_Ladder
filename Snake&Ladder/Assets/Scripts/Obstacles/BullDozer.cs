using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullDozer : MonoBehaviour,IObstacleController
{
    private Animator animator;
    private ObstacleManager obstacleManager;
    private GameObject child;

    [SerializeField] AudioClip bulldozerMove, bulldozerAttack;
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
        SoundController.instance.PlaySoundOnce(bulldozerMove);
    }
    public void AnimStartCallback()
    {
        Invoke("Attack", 1f);
    }
    private void Attack()
    {
        animator.SetTrigger("Close");
        SoundController.instance.PlaySoundOnce(bulldozerAttack);
    }
    public void AnimEndCallback()
    {
        obstacleManager.MovePlayerCallback();
        Invoke("End", 1);
    }
    private void End()
    { 
        animator.SetTrigger("End");
    }
    
}
