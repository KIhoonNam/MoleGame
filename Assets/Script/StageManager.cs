using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    GameObject Panel;
    AdBomScript Ad;
    Canvas GameOverCanvas;
    Text CountDown;
    int Combo = 0;
    float time = 0;
    float TotalTime = 0;
    SpawnPoint spawn;
    bool timeStart = false;
    int EnemyHp = 0;
    int FailedCount = 0;
    int PlayerDamage = 0;
    float SpawnTime = 0;
    

    private void Awake()
    {
        Ad = FindObjectOfType<AdBomScript>();
        CountDown = GameObject.Find("CountDown").GetComponent<Text>();
        spawn = FindObjectOfType<SpawnPoint>();
        PlayerDamage = GameManger.instance.PlayerStat.Damage;
        GameOverCanvas = GameObject.Find("GameOverCanvas").GetComponent<Canvas>();
        Panel = GameObject.Find("DamageObject");
    }
    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;
        Time.timeScale = 1.0f;
        GameOverCanvas.gameObject.SetActive(false);
        StartCoroutine(GameStart());
        
    }


    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1.0f);

        CountDown.text = "2";
        yield return new WaitForSeconds(1.0f);

        CountDown.text = "1";
        yield return new WaitForSeconds(1.0f);

        CountDown.text = "START!";
        yield return new WaitForSeconds(0.5f);
        CountDown.gameObject.SetActive(false);
        timeStart = true;
    }

    // Update is called once per frame
    void Update()
    {
    
        if(timeStart)
        {

            SpawnCount();

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                int mask = (1 << 8);
                int Miss = (1 << 9);
                RaycastHit2D Hit = Physics2D.Raycast(ray.origin, ray.direction, 10.0f, mask);
                RaycastHit2D Misshit = Physics2D.Raycast(ray.origin, ray.direction, 10.0f, Miss);

                if (Hit)
                {
                    Hit.collider.GetComponent<MonsterBase>().TakeDamage(GameManger.instance.PlayerStat.Damage);

                }
                else if (Misshit)
                {
                    Combo = 0;
                    var prefab = Resources.Load<Text>("DamageText");
                    var obj = Instantiate(prefab, Panel.transform, true);
                    obj.text = "Miss";
                    obj.transform.position = new Vector2(Misshit.collider.transform.position.x, Misshit.collider.transform.position.y);
                    obj.transform.localScale = new Vector2(1.0f, 1.0f);
                }
            }

            
        }
    }

    public void ComboUp()
    {
        Combo++;
    }
    public int GetCombo()
    {
        return Combo;
    }

    void SpawnMole()
    {


        
        int Rand = Random.Range(0, 9);
        if (!spawn.GetCheck()[Rand])
        {
            spawn.GetCheck()[Rand] = true;
            
            var obj = MolePoolling.GetMole1();
            obj.transform.position = spawn.GetCircle()[Rand].transform.position;
            obj.transform.position += new Vector3(0,0.5f,0);
            obj.MonsterStat.Num = Rand;

            obj.SetHp(EnemyHp);
            obj.SetLifeTime(SpawnTime + 1);

            time = 0;
            Debug.Log("Spawn"+SpawnTime  );
            Debug.Log("Die" + (EnemyHp / PlayerDamage));

        }

    }

    public  void GameOver()
    {
        instance.GameOverCanvas.gameObject.SetActive(true);
        GameManger.instance.PlayerStat.GamePlayTime++;
        timeStart = false;
        if(Combo >= 10)
        {
            FireBaseAuth.instance.OnAddAchievment(GPGSIds.achievement_combo_x100);
        }
        else if(Combo >= 1000)
        {
            FireBaseAuth.instance.OnAddAchievment(GPGSIds.achievement_combo_x1000);
        }
        else if(Combo >= 10000)
        {
            FireBaseAuth.instance.OnAddAchievment(GPGSIds.achievement_combo_x_10000);
        }
        FireBaseAuth.instance.OnShowLeaderBoard(Combo);

        if (GameManger.instance.PlayerStat.GamePlayTime % 5 == 0)
            Ad.GameOver();
        Time.timeScale = 0.0f;
    }

    void SpawnCount()
    {
        time += Time.deltaTime;
        TotalTime += Time.deltaTime;
        EnemyHp = ((int)TotalTime / 10) + 1 + FailedCount;
        if ((EnemyHp / PlayerDamage) <= 1.0f)
        {
            if (TotalTime > 3)
            {
                SpawnTime = 0.5f;
            }
            else
                SpawnTime = 1.0f;
            if (time >= SpawnTime)
                SpawnMole();
        }
        else if ((EnemyHp / PlayerDamage) <= 4.0f)
        {
            SpawnTime = Random.Range(0.5f, 1.0f);
            if (time >= SpawnTime)
                SpawnMole();
        }
        else if ((EnemyHp / PlayerDamage) <= 7.0f)
        {
            SpawnTime = Random.Range(0.75f, 1.25f);
            if (time >= SpawnTime)
                SpawnMole();
        }
        else if ((EnemyHp / PlayerDamage) <= 10.0f)
        {
            SpawnTime = Random.Range(1.25f, 1.5f);
            if (time >= SpawnTime)
                SpawnMole();
        }
        else if ((EnemyHp / PlayerDamage) <= 15.0f)
        {
            SpawnTime = Random.Range(1.5f, 2.0f);
            if (time >= SpawnTime)
                SpawnMole();
        }
        else if ((EnemyHp / PlayerDamage) <= 20.0f)
        {
            SpawnTime = Random.Range(2.0f, 2.5f);
            if (time >= SpawnTime)
                SpawnMole();
        }
        else if ((EnemyHp / PlayerDamage) > 20.0f)
        {
            SpawnTime = Random.Range(2.5f, 3.0f);
            if (time >= SpawnTime)
                SpawnMole();
        }
    }
}
