using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NativeWebSocket;
using TMPro;

public class classificacao : MonoBehaviour, IClient
{

    private ConnectionManager cm = ConnectionManager.getInstance();

    [SerializeField] private Transform ContentClassf;
    [SerializeField] private GameObject prefabClassf;

    private List<GameObject> quadrosClassf = new List<GameObject>();
    public static List<teams_classf> classfGeral = new List<teams_classf>();  

    [SerializeField] private int m_ItemsToGenerate;
  
    
    // Start is called before the first frame update
    void Start()
    {
        
    }



    public void MSG_CLASSIFICACAO_FINAL(string msgJSON){

        msgCLASSIFICACAO_FINAL message = JsonUtility.FromJson<msgCLASSIFICACAO_FINAL>(msgJSON);

        classfGeral = message.teams;

        Debug.Log("Entrou MSG_CLASSIFICACAO_FINAL");


        m_ItemsToGenerate = classfGeral.Count;

        Debug.Log("Items: " + m_ItemsToGenerate);
        
        foreach (GameObject quadroClassf in quadrosClassf)
        {
            Destroy(quadroClassf);
        }

        quadrosClassf.Clear();

        for (int i = 0; i < m_ItemsToGenerate; i++)
        {
            GameObject novo = Instantiate(prefabClassf, transform.position, Quaternion.identity);
            novo.transform.SetParent(ContentClassf);
            novo.transform.localScale = new Vector3(1.894364f, 0.179433f, 0.23102f);
        
            quadrosClassf.Add(novo);
        }

        for (int i = 0; i < quadrosClassf.Count; i++)
        {

            GameObject quadroClassf = quadrosClassf[i];
            TextMeshProUGUI[] textFieldsC = quadroClassf.GetComponentsInChildren<TextMeshProUGUI>();

            foreach (TextMeshProUGUI textFieldC in textFieldsC)
            {
                if (textFieldC.CompareTag("txt_codigo"))
                {
                    if (classfGeral.Count > i)
                    {
                        textFieldC.text = classfGeral[i].ranking + ") " + "Equipe " + classfGeral[i].idTeam + " " + "Pontuação: " + classfGeral[i].point; 
                    }
                    break;
                }
            }
        }

        Debug.Log("Fim MSG_CLASSIFICACAO_FINAL");



    }

    public void handle(string ms){
        //string messageType = ms.messageType;

        Debug.Log("Entrou Handle");
        //executa JSON->messageType dentro do handle
        string messageType = JsonUtility.FromJson<ServerMessage>(ms).messageType;
        Debug.Log(messageType);


        // route message to handler based on message type
        Debug.Log(ms);

        if (messageType == "CLASSIFICACAO_FINAL") 
        {
            Debug.Log("msg é classificacao_final");
            
            MSG_CLASSIFICACAO_FINAL(ms);
        }
    }

    // Update is called once per frame
    void Update()
    {
        cm.retrieveMessages(this);
    }

}

[System.Serializable] 
public class teams_classf
{
    public int idTeam;
    public int point;
    public int gameTime;
    public int ranking;
}

[System.Serializable] 
public class msgCLASSIFICACAO_FINAL
{
    public string messageType;
    public List<teams_classf> teams;
    public int sessionId;
    public int gameId;
}

