// using NativeWebSocket;
// using System.Collections.Generic;
// using System.Reflection;
// using UnityEngine;
// using static Constants;

// public class ConnectionManager
// {

//     private WebSocket ws;
//     private string gameServerUrl = "ws://localhost:5000";
//     //private string gameServerUrl = "ws://0.tcp.ngrok.io:16554"; // example ngrok URL

//     private Queue<string> gameServerMessageQueue = new Queue<string>();

//     private static ConnectionManager instance;

//     public static ConnectionManager getInstance() {
//         if (instance == null) {
//             instance = new ConnectionManager();
//         }
//         return instance;
//     }
//     private ConnectionManager() {
//         this.InitWebSocketClient();
//     }
    
//     public void send(Message msg) {
//         Debug.Log("Sending: " + JsonUtility.ToJson(msg));
//         this.ws.SendText(JsonUtility.ToJson(msg));
//     }

//     public void retrieveMessages(IClient client)
//     {
//         //#if !UNITY_WEBGL || UNITY_EDITOR
//             this.ws.DispatchMessageQueue();
//         //#endif
        
//         // process all queued server messages

//         // Debug.Log("Count " + this.gameServerMessageQueue.Count);
//         while (this.gameServerMessageQueue.Count > 0)
//         {
//             this.HandleServerMessage(client, this.gameServerMessageQueue.Dequeue());
//         }
//     }

//     private void HandleServerMessage(IClient client, string messageJSON)
//     {
//         // parse message type

//         //string messageType = JsonUtility.FromJson<ServerMessage>(messageJSON).messageType;
    
//         //var obj = this.converte(messageType, messageJSON);

//         //var obj = JsonUtility.FromJson<ServerMessage>(messageJSON);
//         client.handle(messageJSON);
//     }

//     public void close() {
//         ws.Close();
//     }

//     // IMPLEMENTATION METHODS

//     private void InitWebSocketClient()
//     {
//         // create websocket connection
//         this.ws = new WebSocket(this.gameServerUrl);

//         this.ws.OnOpen += () =>  {
//             Debug.Log("Connection open!");
//         };

//         this.ws.OnError += (e) => {
//             Debug.Log("Error! " + e);
//         };

//         this.ws.OnClose += (e) => {
//             Debug.Log("Connection closed!");
//         };

//         // add message handler callback
        
//         this.ws.OnMessage += (bytes) => {
//             // Reading a plain text message
//             var message = System.Text.Encoding.UTF8.GetString(bytes);
//             Debug.Log("Server message received: " + message);
//             this.gameServerMessageQueue.Enqueue(message);
//         };

//         this.ws.Connect();
//     }

//     public Message converte(string type, string messageJSON) {
//         var map = new Dictionary<string, System.Type>();
//         map.Add(SERVER_MESSAGE_TYPE_GAME_STATE, typeof(ServerMessageGameState));
//         map.Add(SERVER_MESSAGE_TYPE_PLAYER_ENTER, typeof(ServerMessagePlayerEnter));
//         map.Add(SERVER_MESSAGE_TYPE_PLAYER_EXIT, typeof(ServerMessagePlayerExit));
//         map.Add(SERVER_MESSAGE_TYPE_PLAYER_UPDATE, typeof(ServerMessagePlayerUpdate));

//         MethodInfo method = typeof (JsonUtility).GetMethod("FromJson", new [] {typeof(string)});
//         MethodInfo generic = method.MakeGenericMethod(map[type]); 
//         Message obj = (Message) generic.Invoke(null, new [] {messageJSON});
//         return obj;
//     }
// }







// ============================================================================================






using NativeWebSocket;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static Constants;

public class ConnectionManager
{

    private WebSocket ws;
    // private string gameServerURL;
    private string gameServerURL; // = "ws://localhost:5000";



    private Queue<string> gameServerMessageQueue = new Queue<string>();

    private static ConnectionManager instance;

    public static ConnectionManager getInstance() {
        if (instance == null) {
            instance = new ConnectionManager(Manager.serverURL);
        }
        return instance;
    }
    private ConnectionManager(string url) {

        this.gameServerURL = url;

        Debug.Log("gameServerURL = " + this.gameServerURL);

        this.InitWebSocketClient();
    }
    
    public void send(Message msg) {
        Debug.Log("Sending: " + JsonUtility.ToJson(msg));
        this.ws.SendText(JsonUtility.ToJson(msg));
    }

    public void retrieveMessages(IClient client)
    {
        //#if !UNITY_WEBGL || UNITY_EDITOR
            this.ws.DispatchMessageQueue();
        //#endif
        
        // process all queued server messages

        // Debug.Log("Count " + this.gameServerMessageQueue.Count);
        while (this.gameServerMessageQueue.Count > 0)
        {
            this.HandleServerMessage(client, this.gameServerMessageQueue.Dequeue());
        }
    }

    private void HandleServerMessage(IClient client, string messageJSON)
    {
        // parse message type

        //string messageType = JsonUtility.FromJson<ServerMessage>(messageJSON).messageType;
    
        //var obj = this.converte(messageType, messageJSON);

        //var obj = JsonUtility.FromJson<ServerMessage>(messageJSON);
        client.handle(messageJSON);
    }

    public void close() {
        ws.Close();
    }

    // IMPLEMENTATION METHODS

    private void InitWebSocketClient()
    {
        // create websocket connection
        this.ws = new WebSocket(this.gameServerURL);

        this.ws.OnOpen += () =>  {
            Debug.Log("Connection open!");
        };

        this.ws.OnError += (e) => {
            Debug.Log("Error! " + e);
        };

        this.ws.OnClose += (e) => {
            Debug.Log("Connection closed!");
        };

        // add message handler callback
        
        this.ws.OnMessage += (bytes) => {
            // Reading a plain text message
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("Server message received: " + message);
            this.gameServerMessageQueue.Enqueue(message);
        };

        this.ws.Connect();
    }

    public Message converte(string type, string messageJSON) {
        var map = new Dictionary<string, System.Type>();
        map.Add(SERVER_MESSAGE_TYPE_GAME_STATE, typeof(ServerMessageGameState));
        map.Add(SERVER_MESSAGE_TYPE_PLAYER_ENTER, typeof(ServerMessagePlayerEnter));
        map.Add(SERVER_MESSAGE_TYPE_PLAYER_EXIT, typeof(ServerMessagePlayerExit));
        map.Add(SERVER_MESSAGE_TYPE_PLAYER_UPDATE, typeof(ServerMessagePlayerUpdate));

        MethodInfo method = typeof (JsonUtility).GetMethod("FromJson", new [] {typeof(string)});
        MethodInfo generic = method.MakeGenericMethod(map[type]); 
        Message obj = (Message) generic.Invoke(null, new [] {messageJSON});
        return obj;
    }
}
