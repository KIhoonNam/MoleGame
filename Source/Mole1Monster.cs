using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole1Monster : MonsterBase
{

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        MolePoolling.ReturnMole1(this);
    }
}
