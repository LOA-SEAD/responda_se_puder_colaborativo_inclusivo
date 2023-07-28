using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NativeWebSocket;

public class Receive : MonoBehaviour, IClient
{
    string messageType;
    string playerName;
    string teamID;

    private ConnectionManager cm = ConnectionManager.getInstance();
    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(cm);
    }

    // Update is called once per frame
    void Update()
    {
        cm.retrieveMessages(this);

    }

    public void handle(string ms){
        // {"messageType":"ENTRAR SESSAO","playerName":"antonio","teamId":"AAAA"}
        //JsonUtility.FromJson<Receive>(ms);
    
        //string messageType = JsonUtility.FromJson<ServerMessage>(messageJSON).messageType;
    
        //var obj = cm.converte(messageType, ms);

        //var obj = JsonUtility.FromJson<ServerMessage>(messageJSON);
        //var obj = this.converte(messageType, messageJSON);
        
        //msg = ms;
        
        Debug.Log(ms);
    }


}
