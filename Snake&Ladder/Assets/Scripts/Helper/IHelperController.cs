using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface IHelperController 
{
    public void Init(HelperManager helperManager) { }
    public void StartHelper(GameObject _playerObj) { }
    public void EndHelper() { }
   
}
