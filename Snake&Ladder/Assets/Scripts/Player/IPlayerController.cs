using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface IPlayerController 
{
    // Start is called before the first frame update

    public void Init(List<Transform> posPoints, PlayerManager _playerManager) { }
   public void Move(int placesToMove) { }
    public void Attack() { }

    public void ObstaclePlayerMove(int pos) { }

    public void ChangePos(int pos) { }

    public void SlideToSide(Vector2 offset) { }

    public void ChangePosForObstacle() { }

    public void HelperPlayerMove(int pos) { }

}
