using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour,IObstacleController
{
    private Animator animator;
    private ObstacleManager obstacleManager;
    private GameObject child;
    [SerializeField] private GameObject flameThrower,fireRing;
    private Vector3 targetPos = new Vector3(0.64f, 0.7f, -5f);
    //[SerializeField] private Transform targetPos;
    [SerializeField] private float speed;
    private bool move = false;

    [SerializeField] AudioClip dragonWingSound;
    // Start is called before the first frame update
    private void Start()
    {
        transform.position = new Vector3(-7.72f, 0.7f, -5f);
        child = transform.GetChild(0).gameObject;
        animator = GetComponent<Animator>();
        obstacleManager = GetComponent<ObstacleManager>();
    }
    public void StartObstacle()
    {
        transform.position = new Vector3(-7.72f, 1.46f, -3f);
        child.SetActive(true);
        animator.SetTrigger("Start");
        move = true;  
    }
    private void BreathFire()
    {
        move = false;
        animator.SetTrigger("Attack");
    }
    public void BreathFireCallback()
    {
        flameThrower.SetActive(true);
    }
    public void FireRingCallback()
    {
        fireRing.SetActive(true);
        Invoke("MovePlayerCallback", 2f);
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
        if (transform.position != targetPos)
        {
           // transform.LookAt(posPoints[nextPoint]);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else
        {
            BreathFire();
        }

    }

    public void DragonWingSound()
    {
        SoundController.instance.PlaySoundOnce(dragonWingSound);
    }
}
