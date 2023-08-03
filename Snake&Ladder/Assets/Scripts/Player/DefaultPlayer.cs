using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPlayer : MonoBehaviour , IPlayerController
{

    private List<Transform> posPoints = new List<Transform>();

    private bool isMoving { get; set; }

    private int currentPoint = 0;
    private int pointToMoveTo = 0;
    private int nextPoint = 0;

    [SerializeField] private int Speed = 10;

    private PlayerManager playerManager;
    private ParticleSystem dieEffect;
    // Start is called before the first frame update
    #region interfaceFunctions
    public void Init(List<Transform> _posPoints, PlayerManager _playerManager)
    {
        posPoints = _posPoints;
        playerManager = _playerManager;
    }
    public void Move(int placesToMove)
    {
        pointToMoveTo = currentPoint + placesToMove;
        nextPoint = currentPoint;
        Debug.Log("Player start moving to "+pointToMoveTo);
        isMoving = true;
    }
    public void Attack()
    {
        // will move or do something
    }
    
    public void ObstaclePlayerMove(int pos)
    {
        Respwan(pos);
    }
    public void HelperPlayerMove(int pos)
    {
        Debug.Log("Changeplayepos");
        Respwan(pos);
    }


    public void SlideToSide(Vector2 offset)
    {
        transform.position = new Vector3(posPoints[currentPoint-1].position.x + offset[0], posPoints[currentPoint-1].position.y, posPoints[currentPoint-1].position.z + offset[1]);
       // Debug.Log("pos is " + posPoints[currentPoint - 1].position.x + offset[0] + " , " + posPoints[currentPoint - 1].position.z + offset[1]);
    }
    public void ChangePosForObstacle() 
    {
      /*  dieEffect.transform.position = transform.position;
        dieEffect.gameObject.SetActive(true);
        dieEffect.Play();*/
        playerManager.DeactivePlayerCallBack();
    }
    #endregion
    public void Respwan(int pos)
    {
        Debug.Log("REswpaw");
        transform.position = new Vector3(posPoints[pos - 1].position.x, 0, posPoints[pos - 1].position.z);
        currentPoint = pos;
    }
    private void Update()
    {
        if (isMoving) Movement();
    }

    private void MovementComplete()
    {
        isMoving = false;
        currentPoint = pointToMoveTo;
        Debug.Log("current point is " + currentPoint);
        playerManager.SetToIdle();
    }

    private void Movement()
    {
        if(nextPoint < pointToMoveTo)
        {
            transform.LookAt(posPoints[nextPoint]);
            transform.position = Vector3.MoveTowards(transform.position, posPoints[nextPoint].position, Speed * Time.deltaTime);
            
            if (transform.position == posPoints[nextPoint].position) nextPoint +=1; 
        }
        else
        {
            MovementComplete();
        }

    }

  
}
