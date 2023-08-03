using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateObject : MonoBehaviour
{
    [SerializeField] float _dTime;
    private void OnEnable() => Invoke("DeactivatePlayer", _dTime);
   
    private void DeactivatePlayer()
    {
        gameObject.SetActive(false);
    }
}
