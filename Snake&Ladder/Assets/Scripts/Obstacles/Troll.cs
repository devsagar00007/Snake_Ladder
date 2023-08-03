using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : MonoBehaviour,IObstacleController
{
    private Animator animator;
    private ObstacleManager obstacleManager;
    private GameObject child;
    private Vector3 targetPos = new Vector3(-0.4f,0,0);
    [SerializeField] private float speed;
    private bool move = false;
    [SerializeField] AudioClip trollFootStep, trollAttackSound, trollRoarSound;
    // Start is called before the first frame update
    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        child.transform.localPosition = new Vector3(-0.40f, 0f, -3.6f);
        animator = GetComponent<Animator>();
        obstacleManager = GetComponent<ObstacleManager>();
    }
    public void StartObstacle()
    {
        child.transform.localPosition = new Vector3(-0.4f, 0f, -3.6f);
        animator.SetTrigger("Start");
        child.SetActive(true);
        move = true;
    }
    private void Attack()
    {
        move = false;
        animator.SetTrigger("Attack");
    }
    public void AnimAttackCallback()
    {

        MovePlayerCallback();
    }
    public void MovePlayerCallback()
    {
        obstacleManager.MovePlayerCallback();
    }
    private void Update()
    {
        if (move) Movement();
    }
    private void Movement()
    {
        if (child.transform.localPosition != targetPos)
        {
            // transform.LookAt(posPoints[nextPoint]);
            child.transform.localPosition = Vector3.MoveTowards(child.transform.localPosition, targetPos, speed * Time.deltaTime);
        }
        else
        {
            Attack();
        }

    }
    internal void TrollWalkSound()
    {
        SoundController.instance.PlaySoundOnce(trollFootStep);
    }
    internal void TrollAttackSound()
    {
        SoundController.instance.PlaySoundOnce(trollAttackSound);
    }
    
}
