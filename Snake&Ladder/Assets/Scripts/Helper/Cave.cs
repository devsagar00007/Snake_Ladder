using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave : MonoBehaviour,IHelperController
{
    private HelperManager helperManager;
    private GameObject player;
    private Transform child;
    // Start is called before the first frame update

    private void Start()
    {
        child = transform.GetChild(0);
        helperManager = GetComponent<HelperManager>();
    }
    public void StartHelper(GameObject _playerObj)
    {
        player = _playerObj;
        Invoke("Helper", 1f);
    }
    private void Helper()
    {
        child.gameObject.SetActive(true);
        Invoke("SendCallback", 3f);
    }
    private void SendCallback()
    {
        player.gameObject.SetActive(true);
        helperManager.changePlayerPos();
    }
}
