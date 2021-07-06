using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolePoolling : MonoBehaviour
{
    public static MolePoolling Instance;

    Queue<MonsterBase> Mole1 = new Queue<MonsterBase>();

    private void Awake()
    {
        Instance = this;
    }

    void Initialize()
    {
        for(int i =0; i <9; i++)
            Mole1.Enqueue(Mole1Create());
    }




    MonsterBase Mole1Create()
    {
        GameObject game = Resources.Load<GameObject>("Mole1") as GameObject;

        var newObj = Instantiate(game).GetComponent<MonsterBase>();

        newObj.gameObject.SetActive(false);

        newObj.transform.SetParent(transform);

        return newObj;
    }

    public static MonsterBase GetMole1()
    {
        var robj = Instance.Mole1.Dequeue();

        robj.transform.SetParent(null);

        robj.gameObject.SetActive(true);
        
        
        return robj;
    }


    public static void ReturnMole1(MonsterBase monster)
    {
        monster.gameObject.SetActive(false);

        monster.transform.SetParent(Instance.transform);

        monster.GetRigidbdy().simulated = true;

        Instance.Mole1.Enqueue(monster);



    }
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame

}
