using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Firebase;
using Firebase.Database;

public class FireBaseDataScript : MonoBehaviour
{


    DatabaseReference reference;

    string UserId;
    string UserEmail;
    int count=1;
    // Start is called before the first frame update
    void Awake()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        
    }

    private void Start()
    {
     
    }



    public void Save()
    {
        writeNewUser(UserId, GameManger.instance.GetPlayerStat());
    }

    public void Load()
    {
        readuser(UserId);

    }

    private void writeNewUser(string _userId,PlayerState player)
    {
        PlayerState user = player;

        string json = JsonUtility.ToJson(user);

        reference.Child(_userId).SetRawJsonValueAsync(json);
        Debug.Log("Save Go");
    }

    private void readuser(string _userid)
    {
        Debug.Log(_userid.ToString()) ;
        if (_userid == null)
        {
            SLManager.Load();
        }
        else
        {
            reference.Child(_userid).GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    Debug.Log("Come In");
                    if (snapshot.Value == null)
                    {
                        GameManger.instance.SetPlayerStat(new PlayerState(UserId, UserEmail, 2, 0, 0, 0, 0));
                        Save();
                    }
                    else
                    {
                        Debug.Log("Load Go");
                        GameManger.instance.SetPlayerStat(JsonConvert.DeserializeObject<PlayerState>(snapshot.Value.ToString()));

                        foreach (DataSnapshot data in snapshot.Children)
                        {
                            IDictionary personinfo = (IDictionary)data.Value;


                        }
                    }
                }


            });
        }
    }

    public void SetUserId(string _userid,string _useremail)
    {
        UserId = _userid;
        UserEmail = _useremail;
    }
}
