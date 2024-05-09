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

    public TMP_Text txt_nome;

    public float elogio_comunicativo;
    public double elogio_engajado;
    public float elogio_gentil;

    public Sprite estrela_cheia;
    public Sprite estrela_meia;
    public Sprite estrela_vazia;

    public SpriteRenderer estrela_comunicativo_1;
    public SpriteRenderer estrela_comunicativo_2;
    public SpriteRenderer estrela_comunicativo_3;
    public SpriteRenderer estrela_engajado_1;
    public SpriteRenderer estrela_engajado_2;
    public SpriteRenderer estrela_engajado_3;
    public SpriteRenderer estrela_gentil_1;
    public SpriteRenderer estrela_gentil_2;
    public SpriteRenderer estrela_gentil_3;
  
    
    // Start is called before the first frame update
    void Start()
    {
        txt_nome.text = dadosTimes.player.name + ",";

        SpriteRenderer image_comunicativo_1 = estrela_comunicativo_1.GetComponent<SpriteRenderer>();
        image_comunicativo_1.sprite = estrela_vazia;

        SpriteRenderer image_comunicativo_2 = estrela_comunicativo_2.GetComponent<SpriteRenderer>();
        image_comunicativo_2.sprite = estrela_vazia;

        SpriteRenderer image_comunicativo_3 = estrela_comunicativo_3.GetComponent<SpriteRenderer>();
        image_comunicativo_3.sprite = estrela_vazia;

        SpriteRenderer image_engajado_1 = estrela_engajado_1.GetComponent<SpriteRenderer>();
        image_engajado_1.sprite = estrela_vazia;

        SpriteRenderer image_engajado_2 = estrela_engajado_2.GetComponent<SpriteRenderer>();
        image_engajado_2.sprite = estrela_vazia;

        SpriteRenderer image_engajado_3 = estrela_engajado_3.GetComponent<SpriteRenderer>();
        image_engajado_3.sprite = estrela_vazia;

        SpriteRenderer image_gentil_1 = estrela_gentil_1.GetComponent<SpriteRenderer>();
        image_gentil_1.sprite = estrela_vazia;

        SpriteRenderer image_gentil_2 = estrela_gentil_2.GetComponent<SpriteRenderer>();
        image_gentil_2.sprite = estrela_vazia;

        SpriteRenderer image_gentil_3 = estrela_gentil_3.GetComponent<SpriteRenderer>();
        image_gentil_3.sprite = estrela_vazia;
        
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
                    if (classfGeral[i].idTeam == Manager.teamId) {
                        textFieldC.text = "<b>" + textFieldC.text + "</b>";
                    }
                    break;
                }
            }
        }

        Debug.Log("Fim MSG_CLASSIFICACAO_FINAL");
    }

    public void MSG_RETORNA_AVALIACAO(string msgJSON)
    {
        msgRETORNA_AVALIACAO message = JsonUtility.FromJson<msgRETORNA_AVALIACAO>(msgJSON);

        dadosTimes.player.elogio1 = message.user.elogio1;
        dadosTimes.player.elogio2 = message.user.elogio2;
        dadosTimes.player.elogio3 = message.user.elogio3;

        setEstrelasAvaliadas();

        // var msg = new FimDeJogo("FIM_DE_JOGO", dadosTimes.player, Manager.teamId, Manager.sessionId,
        //                         Manager.gameId, Manager.grpScore, Manager.gameTime);

                

        // cm.send(msg);   
    }

    public void setEstrelasAvaliadas()
    {
        elogio_comunicativo = (float)dadosTimes.player.elogio1 / (3.0f * (float)Manager.nrPlayerTeam);
        elogio_engajado = (float)dadosTimes.player.elogio2 / (3.0f * (float)Manager.nrPlayerTeam);
        elogio_gentil = (float)dadosTimes.player.elogio3 / (3.0f * (float)Manager.nrPlayerTeam);

        setEstrelaComunicativo(elogio_comunicativo);
        setEstrelaEngajado(elogio_engajado);
        setEstrelaGentil(elogio_gentil);
    }

    public void setEstrelaComunicativo(float n)
    {
        if (n > 0 && n < 0.75)
        {
            SpriteRenderer image_comunicativo_1 = estrela_comunicativo_1.GetComponent<SpriteRenderer>();
            image_comunicativo_1.sprite = estrela_meia;

            SpriteRenderer image_comunicativo_2 = estrela_comunicativo_2.GetComponent<SpriteRenderer>();
            image_comunicativo_2.sprite = estrela_vazia;

            SpriteRenderer image_comunicativo_3 = estrela_comunicativo_3.GetComponent<SpriteRenderer>();
            image_comunicativo_3.sprite = estrela_vazia;
        }
        else if (n >= 0.75 && n < 1.25) 
        {
            SpriteRenderer image_comunicativo_1 = estrela_comunicativo_1.GetComponent<SpriteRenderer>();
            image_comunicativo_1.sprite = estrela_cheia;

            SpriteRenderer image_comunicativo_2 = estrela_comunicativo_2.GetComponent<SpriteRenderer>();
            image_comunicativo_2.sprite = estrela_vazia;

            SpriteRenderer image_comunicativo_3 = estrela_comunicativo_3.GetComponent<SpriteRenderer>();
            image_comunicativo_3.sprite = estrela_vazia;
        }
        else if (n >= 1.25 && n < 1.75)
        {
            SpriteRenderer image_comunicativo_1 = estrela_comunicativo_1.GetComponent<SpriteRenderer>();
            image_comunicativo_1.sprite = estrela_cheia;

            SpriteRenderer image_comunicativo_2 = estrela_comunicativo_2.GetComponent<SpriteRenderer>();
            image_comunicativo_2.sprite = estrela_meia;

            SpriteRenderer image_comunicativo_3 = estrela_comunicativo_3.GetComponent<SpriteRenderer>();
            image_comunicativo_3.sprite = estrela_vazia;       
        }
        else if (n >= 1.75 && n < 2.25)
        {
            SpriteRenderer image_comunicativo_1 = estrela_comunicativo_1.GetComponent<SpriteRenderer>();
            image_comunicativo_1.sprite = estrela_cheia;

            SpriteRenderer image_comunicativo_2 = estrela_comunicativo_2.GetComponent<SpriteRenderer>();
            image_comunicativo_2.sprite = estrela_cheia;

            SpriteRenderer image_comunicativo_3 = estrela_comunicativo_3.GetComponent<SpriteRenderer>();
            image_comunicativo_3.sprite = estrela_vazia; 
        }
        else if (n >= 2.25 && n < 2.75)
        {
            SpriteRenderer image_comunicativo_1 = estrela_comunicativo_1.GetComponent<SpriteRenderer>();
            image_comunicativo_1.sprite = estrela_cheia;

            SpriteRenderer image_comunicativo_2 = estrela_comunicativo_2.GetComponent<SpriteRenderer>();
            image_comunicativo_2.sprite = estrela_cheia;

            SpriteRenderer image_comunicativo_3 = estrela_comunicativo_3.GetComponent<SpriteRenderer>();
            image_comunicativo_3.sprite = estrela_meia; 
        } else if (n >= 2.75)
        {
            SpriteRenderer image_comunicativo_1 = estrela_comunicativo_1.GetComponent<SpriteRenderer>();
            image_comunicativo_1.sprite = estrela_cheia;

            SpriteRenderer image_comunicativo_2 = estrela_comunicativo_2.GetComponent<SpriteRenderer>();
            image_comunicativo_2.sprite = estrela_cheia;

            SpriteRenderer image_comunicativo_3 = estrela_comunicativo_3.GetComponent<SpriteRenderer>();
            image_comunicativo_3.sprite = estrela_cheia; 
        }

    }

    public void setEstrelaEngajado(double n)
    {
        if (n > 0 && n < 0.75)
        {
            SpriteRenderer image_engajado_1 = estrela_engajado_1.GetComponent<SpriteRenderer>();
            image_engajado_1.sprite = estrela_meia;

            SpriteRenderer image_engajado_2 = estrela_engajado_2.GetComponent<SpriteRenderer>();
            image_engajado_2.sprite = estrela_vazia;

            SpriteRenderer image_engajado_3 = estrela_engajado_3.GetComponent<SpriteRenderer>();
            image_engajado_3.sprite = estrela_vazia;
        }
        else if (n >= 0.75 && n < 1.25) 
        {
            SpriteRenderer image_engajado_1 = estrela_engajado_1.GetComponent<SpriteRenderer>();
            image_engajado_1.sprite = estrela_cheia;

            SpriteRenderer image_engajado_2 = estrela_engajado_2.GetComponent<SpriteRenderer>();
            image_engajado_2.sprite = estrela_vazia;

            SpriteRenderer image_engajado_3 = estrela_engajado_3.GetComponent<SpriteRenderer>();
            image_engajado_3.sprite = estrela_vazia;
        }
        else if (n >= 1.25 && n < 1.75)
        {
            SpriteRenderer image_engajado_1 = estrela_engajado_1.GetComponent<SpriteRenderer>();
            image_engajado_1.sprite = estrela_cheia;

            SpriteRenderer image_engajado_2 = estrela_engajado_2.GetComponent<SpriteRenderer>();
            image_engajado_2.sprite = estrela_meia;

            SpriteRenderer image_engajado_3 = estrela_engajado_3.GetComponent<SpriteRenderer>();
            image_engajado_3.sprite = estrela_vazia;       
        }
        else if (n >= 1.75 && n < 2.25)
        {
            SpriteRenderer image_engajado_1 = estrela_engajado_1.GetComponent<SpriteRenderer>();
            image_engajado_1.sprite = estrela_cheia;

            SpriteRenderer image_engajado_2 = estrela_engajado_2.GetComponent<SpriteRenderer>();
            image_engajado_2.sprite = estrela_cheia;

            SpriteRenderer image_engajado_3 = estrela_engajado_3.GetComponent<SpriteRenderer>();
            image_engajado_3.sprite = estrela_vazia; 
        }
        else if (n >= 2.25 && n < 2.75)
        {
            SpriteRenderer image_engajado_1 = estrela_engajado_1.GetComponent<SpriteRenderer>();
            image_engajado_1.sprite = estrela_cheia;

            SpriteRenderer image_engajado_2 = estrela_engajado_2.GetComponent<SpriteRenderer>();
            image_engajado_2.sprite = estrela_cheia;

            SpriteRenderer image_engajado_3 = estrela_engajado_3.GetComponent<SpriteRenderer>();
            image_engajado_3.sprite = estrela_meia; 
        } 
        else if (n >= 2.75)
        {
            SpriteRenderer image_engajado_1 = estrela_engajado_1.GetComponent<SpriteRenderer>();
            image_engajado_1.sprite = estrela_cheia;

            SpriteRenderer image_engajado_2 = estrela_engajado_2.GetComponent<SpriteRenderer>();
            image_engajado_2.sprite = estrela_cheia;

            SpriteRenderer image_engajado_3 = estrela_engajado_3.GetComponent<SpriteRenderer>();
            image_engajado_3.sprite = estrela_cheia; 
        }

    }

    public void setEstrelaGentil(float n)
    {
        if (n > 0 && n < 0.75)
        {
            SpriteRenderer image_gentil_1 = estrela_gentil_1.GetComponent<SpriteRenderer>();
            image_gentil_1.sprite = estrela_meia;

            SpriteRenderer image_gentil_2 = estrela_gentil_2.GetComponent<SpriteRenderer>();
            image_gentil_2.sprite = estrela_vazia;

            SpriteRenderer image_gentil_3 = estrela_gentil_3.GetComponent<SpriteRenderer>();
            image_gentil_3.sprite = estrela_vazia;
        }
        else if (n >= 0.75 && n < 1.25) 
        {
            SpriteRenderer image_gentil_1 = estrela_gentil_1.GetComponent<SpriteRenderer>();
            image_gentil_1.sprite = estrela_cheia;

            SpriteRenderer image_gentil_2 = estrela_gentil_2.GetComponent<SpriteRenderer>();
            image_gentil_2.sprite = estrela_vazia;

            SpriteRenderer image_gentil_3 = estrela_gentil_3.GetComponent<SpriteRenderer>();
            image_gentil_3.sprite = estrela_vazia;
        }
        else if (n >= 1.25 && n < 1.75)
        {
            SpriteRenderer image_gentil_1 = estrela_gentil_1.GetComponent<SpriteRenderer>();
            image_gentil_1.sprite = estrela_cheia;

            SpriteRenderer image_gentil_2 = estrela_gentil_2.GetComponent<SpriteRenderer>();
            image_gentil_2.sprite = estrela_meia;

            SpriteRenderer image_gentil_3 = estrela_gentil_3.GetComponent<SpriteRenderer>();
            image_gentil_3.sprite = estrela_vazia;       
        }
        else if (n >= 1.75 && n < 2.25)
        {
            SpriteRenderer image_gentil_1 = estrela_gentil_1.GetComponent<SpriteRenderer>();
            image_gentil_1.sprite = estrela_cheia;

            SpriteRenderer image_gentil_2 = estrela_gentil_2.GetComponent<SpriteRenderer>();
            image_gentil_2.sprite = estrela_cheia;

            SpriteRenderer image_gentil_3 = estrela_gentil_3.GetComponent<SpriteRenderer>();
            image_gentil_3.sprite = estrela_vazia; 
        }
        else if (n >= 2.25 && n < 2.75)
        {
            SpriteRenderer image_gentil_1 = estrela_gentil_1.GetComponent<SpriteRenderer>();
            image_gentil_1.sprite = estrela_cheia;

            SpriteRenderer image_gentil_2 = estrela_gentil_2.GetComponent<SpriteRenderer>();
            image_gentil_2.sprite = estrela_cheia;

            SpriteRenderer image_gentil_3 = estrela_gentil_3.GetComponent<SpriteRenderer>();
            image_gentil_3.sprite = estrela_meia; 
        } 
        else if (n >= 2.75)
        {
            SpriteRenderer image_gentil_1 = estrela_gentil_1.GetComponent<SpriteRenderer>();
            image_gentil_1.sprite = estrela_cheia;

            SpriteRenderer image_gentil_2 = estrela_gentil_2.GetComponent<SpriteRenderer>();
            image_gentil_2.sprite = estrela_cheia;

            SpriteRenderer image_gentil_3 = estrela_gentil_3.GetComponent<SpriteRenderer>();
            image_gentil_3.sprite = estrela_cheia; 
        }

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
        if (messageType == "RETORNA_AVALIACAO")
        {
            MSG_RETORNA_AVALIACAO(ms);
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

[System.Serializable] 
public class msgRETORNA_AVALIACAO
{
    public string messageType;
    public generic_user user;
    public Received received;
    public int sessionId;
    public int gameId;
}

[System.Serializable]
public class generic_user
{
    public string _id;
    public int id;
    public string name;
    public int teamId;
    public int indScore;
    public int interaction;
    public int elogio1;
    public int elogio2;
    public int elogio3;
    public int sessionId;
    public string ws_id;
}

[System.Serializable]
public class Received
{
    public int elogio1;
    public int elogio2;
    public int elogio3;
}


