using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private List<GameObject> Circle = new List<GameObject>();

    List<bool> Check;

    private void Awake()
    {
        for(int i =0; i<transform.childCount;i++)
        {
            var CircleObject = this.transform.GetChild(i).gameObject;
            Circle.Add(CircleObject);
        }
        Check = new List<bool>(new bool[Circle.Count]);
    }


    public List<bool> GetCheck()
    {
        return Check;
    }

    public List<GameObject> GetCircle()
    {
        return Circle;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
