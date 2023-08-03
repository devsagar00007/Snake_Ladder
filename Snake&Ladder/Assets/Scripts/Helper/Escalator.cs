using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escalator : MonoBehaviour
{
    [SerializeField] private List<Transform> steps = new List<Transform>();
    [SerializeField] private List<Vector3> steppos = new List<Vector3>();
    [SerializeField] private int[] indexes;
    [SerializeField]int maxindex;
    int index = 0;
    [SerializeField] float movespeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        int len = transform.childCount;
        maxindex = len;
        indexes = new int[len];
        for(int i = 0;i<len;i++)
        {
            int next = i + 1;
            if (next >= len) next = 0;
            Transform temp = transform.GetChild(i);
            Transform temp2 = transform.GetChild(next) ;
            steps.Add(temp);
            steppos.Add(temp2.localPosition);
            indexes[i] = i;
        }
    }

    // Update is called once per frame
    void Update()
    {/*
          steps[0].localPosition = Vector3.MoveTowards(steps[0].localPosition, steppos[index], movespeed*Time.deltaTime);
        steps[0].rotation = Quaternion.Lerp(steps[index].rotation, steprot[index], (movespeed)*Time.deltaTime);

        if (steps[0].localPosition == steppos[index])
        {
           
            index += 1;
            if (index  >= maxindex) index = 0;
            Debug.Log("index is "+index);

        }*/

        for (int i = 0; i < maxindex; i++)
        {

            steps[i].localPosition = Vector3.MoveTowards(steps[i].localPosition, steppos[indexes[i]], movespeed * Time.deltaTime);

            if (steps[i].localPosition == steppos[indexes[i]])
            {
                Debug.Log("position reached");
                indexes[i] += 1;
                if (indexes[i] >= maxindex) indexes[i] = 0;

            }


        }
    }
}
