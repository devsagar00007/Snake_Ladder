using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour,IObstacleController
{
    private Animator animator;
    private ObstacleManager obstacleManager;
    private GameObject child;
    [SerializeField] GameObject explosion;
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
        Invoke("Action", 2f);
    }

    public void Action()
    {
        animator.SetTrigger("Start");
    }
    public void AnimEndAction()
    {
        explosion.SetActive(true);
        Invoke("MovePlayerCallback", 0.6f);
    }
    private void MovePlayerCallback()
    {
        obstacleManager.MovePlayerCallback();
    }
   
}
