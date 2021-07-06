using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public struct MonsterState
{
    public int MaxHp;
    public int Hp;
    public uint Score;
    public int Num;
    public float LifeTime;

};

public class MonsterBase : MonoBehaviour
{
  
    public MonsterState MonsterStat;
    Rigidbody2D Rg;
    AudioClip HitSfx;
    SpriteRenderer EnemyCurrentImage;
    List<Sprite> Hitsprite;
    Text HpText;
    Image HpCurrent;
    GameObject Place;
    StageManager Stage;
    SpawnPoint spawn;

    private void Awake()
    {
        EnemyCurrentImage = GetComponent<SpriteRenderer>();
        Rg = GetComponent<Rigidbody2D>();
        Hitsprite = new List<Sprite>(new Sprite[2]);
        Stage = FindObjectOfType<StageManager>();
        spawn = FindObjectOfType<SpawnPoint>();
        HpText = gameObject.GetComponentInChildren<Text>();
        HpCurrent = gameObject.GetComponentInChildren<Image>();
        Place = GameObject.Find("DamageObject");
    }

    // Start is called before the first frame update
    void Start()
    {
        HitSfx = Resources.Load<AudioClip>("SFX/Enemy_Hit");
        Hitsprite[0] = Resources.Load<Sprite>("Mole1_Idle");
        Hitsprite[1] = Resources.Load<Sprite>("Mole1_Hit");
    }

    // Update is called once per frame
    void Update()
    {
        MonsterStat.LifeTime -= Time.deltaTime;

        if(MonsterStat.LifeTime <= 0)
        {
            MolePoolling.ReturnMole1(this);
            Stage.GameOver();
        }
    }


    public virtual void TakeDamage(int damage)
    {
        MonsterStat.Hp -= damage;
        EnemyCurrentImage.sprite = Hitsprite[1];
        HpCurrent.fillAmount = ((float)MonsterStat.Hp / (float)MonsterStat.MaxHp);
        HpText.text = MonsterStat.Hp.ToString();
        SoundManager.instance.SFX("EnemyHit", HitSfx);

        if (MonsterStat.Hp <= 0)
        {
            HpText.text = "";
            MonsterStat.LifeTime = 10.0f;
            Rg.simulated = false;
            Invoke("MonsterDown",0.3f);
        }
        else
        {
            Invoke("MonsterHit", 0.1f);
        }
    }



    void MonsterHit()
    {
        EnemyCurrentImage.sprite = Hitsprite[0];
    }
    void MonsterDown()
    {
        Stage.ComboUp();
        var prefab = Resources.Load<Text>("DamageText");
        var obj = Instantiate(prefab, Place.transform, true);       
        obj.text = "x " + Stage.GetCombo();
        obj.transform.position = new Vector2(transform.position.x, transform.position.y);
        obj.transform.localScale = new Vector2(1.0f, 1.0f);
        spawn.GetCheck()[MonsterStat.Num] = false;
        EnemyCurrentImage.sprite = Hitsprite[0];
        MolePoolling.ReturnMole1(this);
     
    }

    public void SetLifeTime(float time)
    {
        MonsterStat.LifeTime = time;
    }

    public Rigidbody2D GetRigidbdy()
    {
        return Rg;
    }
    public void SetHp(int hp)
    {
        MonsterStat.Hp = hp;
        MonsterStat.MaxHp = hp;

        HpCurrent.fillAmount = (MonsterStat.Hp / MonsterStat.MaxHp);
        HpText.text = MonsterStat.Hp.ToString();
    }
}
