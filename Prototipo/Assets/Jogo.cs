using NativeWebSocket;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;


public class Jogo : MonoBehaviour, IClient
{

    private ConnectionManager cm = ConnectionManager.getInstance();

    public TMP_Text pergunta;
    public TMP_Text dica;
    public TMP_Text[] alternativas; 
    public Button[] btnAlternativas;
    public TMP_Text altA;
    public TMP_Text altB;
    public TMP_Text altC;
    public TMP_Text altD;

    public TMP_Text txt_correto_resposta;
    public TMP_Text txt_errado_resposta;
    public TMP_Text txt_errado_resposta_dada;
    
    public TMP_Text numeroQuestaoText;
    public TMP_Text nivel;
    public TMP_Text pontuacao; 

    public TMP_Text txt_geral; 
    public TMP_Text equipe_players;
    public TMP_Text equipe_nr;


    public TMP_Text tempoQuestao; 
    public float timer = 20f;

    public GameObject CanvasJogo;
    public GameObject CanvasRCerta;
    public GameObject CanvasRErrada;
    public GameObject CanvasFase;


    public GameObject[] qntAlternatives;

    public GameObject confirmaAlternativa;
    public GameObject painelDica;
    public GameObject painelConfirma;

    public GameObject painel_aguarde;
    public TMP_Text txt_painelGeral;

    public GameObject quadroChat;

    public Button btnDica;
    public Button confirmaDica;
    public Button btnOK;

    public Button btnPular;
    public Button btn5050;



    public int[] alt;
    public int[] alt5050;

    private DadosJogo perguntaAtual;
    private int numeroQuestao = 0;
    private int totalQuestoes;

    private string correctAnswer;
    public ans answer;
    private int level;
    private int nrQuestion;
    private RespostasGrupo ansGroup;
    private int correct;
    private int pulou = 0;

    private int qst;
    private int qst_respondidas = 0;

    public TMP_Text txt_lider_jogo;

    //TELA FASE
    public float sec;
    public TMP_Text txt_nivel;
    public TMP_Text txt_lider; 

    public TMP_Text txt_nrEquipe;


    public TMP_Text txt_pontuacao_correto;
    public TMP_Text txt_pontuacao_errada;

    // private void Start()
    // {
    //     SetLevelText();
    //     SetLeaderText();
    //     Invoke("NextQ", sec);
    // }

    public void SetQuadroEquipe() 
    {
        equipe_nr.text = "Equipe " + Manager.teamId;

        foreach (User user in dadosTimes.meuTime)
        {
            equipe_players.text += "\n" + user.name;
        }
    }

    void ActivateCanvasJogo()
    {
        CanvasJogo.SetActive(true);
    }

    void NextQ()
    {


        CanvasRCerta.SetActive(false);
        CanvasRErrada.SetActive(false);
        CanvasFase.SetActive(false);
        CanvasJogo.SetActive(true);
        // Invoke("ActivateCanvasJogo", 4.0f);

    }

    public void SetLevelText()
    {
        txt_nivel.text = Manager.FASE;
    }

    public void SetLeaderText()
    {
        string t = dadosTimes.GetUser(Manager.leaderId);

        txt_lider.text = "Líder da fase: " + t;
        txt_lider_jogo.text = "Líder da fase: " + t;
    }


    // Start is called before the first frame update
    void Start()
    {
        // tempoQuestao.text = timer.ToString();


            
        tempoQuestao.text = "Tempo Restante: " + timer;
        pontuacao.text = "Pontuação: ";
        pontuacao.text = pontuacao.text + "0";

        txt_geral.enabled = false;

        dadosTimes.SetEquipe();
        SetQuadroEquipe();

        // txt_nrEquipe.text = "Equipe " + Manager.teamId;
        SetQntAlternatives(0);


        painelDica.gameObject.SetActive(false);
        painelConfirma.gameObject.SetActive(false);
        painel_aguarde.SetActive(false);

        Manager.totalQuestoes = 0;
        Manager.totalFacil = 0;
        Manager.totalMedio = 0;
        Manager.totalDificil = 0;
        qst = 0;

        // btn5050.gameObject.SetActive(false);
        // btnPular.gameObject.SetActive(false);


        carregaDados.Load();
        carregaDados.Select();
        totalQuestoes = carregaDados.listaDados.Count;

        Manager.countQuestoesJogo();

        CanvasJogo.SetActive(false);
        CanvasFase.SetActive(true);
        SetLevelText();
        SetLeaderText();
        Invoke("NextQ", sec);

        PrimeiraQuestao();


        
        
        // CarregarPerguntas();
        
        // generalCommands.DisableAllObjectsInteractions();
        // btnDica.interactable = true;
        // quadroChat.SetActive(true);
        // generalCommands.EnableInteraction(quadroChat);
        // Debug.Log(Manager.totalQuestoes);
        // Debug.Log(totalQuestoes);
    }

    void PrimeiraQuestao()
    {
        MSG_NOVA_QUESTAO(Manager.msgPrimeiraQuestao);
    }
    // void CarregarPerguntas()
    // {
    //     // numeroQuestao = 1;
    //     CarregarPergunta();
    //     // SetIndividual();
    //     // ProximaQuestao();
    // }

    // Carrega uma nova pergunta
    void CarregarPergunta()
    {
        if (carregaDados.listaDados.Count > 0)
        {
            perguntaAtual = carregaDados.listaDados[0];
            carregaDados.listaDados.RemoveAt(0);
        }
        else
        {
            perguntaAtual = null;
            //fim de jogo
        }

        // Placar de questões e tempo
        numeroQuestaoText.text = "Questão " + (qst_respondidas+1) + " de " + Manager.nQ_total;
        
        //numeroQuestaoText.text = "Questão " + numeroQuestao + " de " + Manager.nQ_total;
        
        nivel.text = "Nível " + perguntaAtual.nivel;
        
        if (perguntaAtual.nivel == "facil")
        {
            answer.level = 0;
        }
        else if (perguntaAtual.nivel == "medio")
        {
            answer.level = 1;
        }
        else
        {
            answer.level = 2;
        }


        // Printa as perguntas e respostas
        if (perguntaAtual != null)
        {
            correctAnswer = perguntaAtual.resposta; 
            pergunta.text = perguntaAtual.pergunta;
            dica.text = perguntaAtual.dica;
            answer.nrQ = Manager.numQ[qst];


            carregaDados.Shuffle(ref perguntaAtual, alt);
            
            // string[] alt_string = {perguntaAtual.resposta, perguntaAtual.r2, perguntaAtual.r3, perguntaAtual.r4};
            // List<string> novaOrdem = new List<string>();

            // foreach (int i in alt)
            // {
            //     if (i >= 0 && i < alt_string.Length)
            //     {
            //         novaOrdem.Add(alt_string[i]);
            //     }
            // }

            // if (novaOrdem.Count == alt_string.Length)
            // {
            //     perguntaAtual.resposta = novaOrdem[0];
            //     perguntaAtual.r2 = novaOrdem[1];
            //     perguntaAtual.r3 = novaOrdem[2];
            //     perguntaAtual.r4 = novaOrdem[3];
            // }

            Debug.Log(perguntaAtual.resposta);
            Debug.Log(perguntaAtual.r2);
            Debug.Log(perguntaAtual.r3);
            Debug.Log(perguntaAtual.r4);


            for (int i = 0; i < btnAlternativas.Length; i++)
            {  
                
                alternativas[i].text = ObterAlternativa(i);
                btnAlternativas[i].gameObject.SetActive(true);
            }

            confirmaAlternativa.SetActive(false);
        }
        else
        {

            for (int i = 0; i < btnAlternativas.Length; i++)
            {
                btnAlternativas[i].gameObject.SetActive(false);
            }
        }
    }


    

    public void SelecionaAlternativa(string tag)
    {
  
        switch (tag)
        {   
            case "A":
                Debug.Log(perguntaAtual.resposta);
                answer.s = perguntaAtual.resposta;
                answer.alternativa = "A";
                break;
            case "B":
                Debug.Log(perguntaAtual.r2);
                answer.s = perguntaAtual.r2;
                answer.alternativa = "B";
                break;
            case "C":
                Debug.Log(perguntaAtual.r3);
                answer.s = perguntaAtual.r3;
                answer.alternativa = "C";
                break;
            case "D":
                answer.s = perguntaAtual.r4;
                answer.alternativa = "D";
                Debug.Log(perguntaAtual.r4);
                break;
            default:
                Debug.Log("Nada selecionado");
                break;
        }

        painelConfirma.SetActive(true);


        answer.alternativa = tag;

    }

    string ObterAlternativa(int index)
    {
        switch (index)
        {
            case 0:
                return perguntaAtual.resposta;
            case 1:
                return perguntaAtual.r2;
            case 2:
                return perguntaAtual.r3;
            case 3:
                return perguntaAtual.r4;
            default:
                return "";
        }
    }

    public void ProximaQuestao()
    {
        numeroQuestao++;

        if (numeroQuestao <= Manager.nQ_total)
        {
            SetIndividual();
            Invoke("NextQ", 3f);
            CarregarPergunta();
        }
        else {
            SceneManager.LoadScene("Fim");
        }
        zeraTimer();

    }

    public int VerificaResposta()
    {
        if (correctAnswer == answer.s) return 1;
        else return 0;

    }

    public void ConfirmarResposta()
    {
        Debug.Log("RESPOSTA: "+ answer.alternativa);
        Debug.Log(Manager.MOMENTO);
        
        if (Manager.MOMENTO == "INDIVIDUAL")
        {
            ConfirmarRespostaIndividual();
        }
        if (Manager.MOMENTO == "GRUPO")
        {
            ConfirmarRespostaFinal();
        }

        // painelConfirma.SetActive(false);
        // //ProximaQuestao();
    }

    public void ConfirmarRespostaIndividual()
    {
        painelConfirma.SetActive(false);


        correct = VerificaResposta();
        
        if (correct == 1) {
            Manager.indScore += 10;
        }

        var msg = new RespostaIndividual("RESPOSTA_INDIVIDUAL", dadosTimes.player, Manager.teamId, Manager.sessionId,
                                        Manager.gameId, answer.alternativa, answer.level, answer.nrQ);

        cm.send(msg);

        btnAlternativas[0].gameObject.SetActive(false);
        btnAlternativas[1].gameObject.SetActive(false);
        btnAlternativas[2].gameObject.SetActive(false);
        btnAlternativas[3].gameObject.SetActive(false);


        // txt_geral.enabled = true;
        // txt_geral.text = "Aguarde até que todos enviem suas respostas.";
        painelAguarde("Aguarde até que todos enviem suas respostas.");



    }

    public void painelAguarde(string s)
    {
        painel_aguarde.SetActive(true);
        txt_painelGeral.text = "" + s;
    }
    
    public void fechaPainelAguarde()
    {
        painel_aguarde.SetActive(false);
    }

    public void fechaConfirma()
    {
        painelConfirma.SetActive(false);
    }

    public void ConfirmarRespostaFinal()
    {
        painelConfirma.SetActive(false);

        correct = VerificaResposta();

        var msg = new RespostaFinal("RESPOSTA_FINAL", dadosTimes.player, Manager.teamId, Manager.sessionId, 
                                    Manager.gameId, answer.alternativa, correct);

        cm.send(msg);

        // qst++;

     }

    public void ajudaDica()
    {
        painelDica.SetActive(true);
    }

    public void confirmarDica()
    {
        painelDica.SetActive(false);
    }



    public void ajudaPula()
    {
        if (Manager.MOMENTO == "GRUPO"){
            var msg = new PedirAjuda("PEDIR_AJUDA", dadosTimes.player, Manager.teamId, Manager.sessionId,
                                    Manager.gameId, answer.level, answer.nrQ, "pular");

            cm.send(msg);
        }
    }

    public void ajuda5050()
    {
        if (Manager.MOMENTO == "GRUPO"){
            var msg = new PedirAjuda("PEDIR_AJUDA", dadosTimes.player, Manager.teamId, Manager.sessionId,
                                    Manager.gameId, answer.level, answer.nrQ, "5050");

            cm.send(msg);
        }
    }

    void DesativaTXT()
    {
        txt_geral.enabled = false;
    }



    void Update()
    {
        atualizaTimer();

        cm.retrieveMessages(this);


    }

    public void SetQntAlternatives(int i)
    {
        if (i == 0)
        {
            foreach (GameObject obj in qntAlternatives)
            {
                obj.SetActive(false);
            }
        }
        else 
        {
            foreach (GameObject obj in qntAlternatives)
            {
                obj.SetActive(true);
            } 
        }
    }

    public void atualizaTimer()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0f)
        {
        //    if (Manager.MOMENTO = "INDIVIDUAL") 
        //    {

        //    }
        //    else 
        //    {

        //    }
        }

        int min = Mathf.FloorToInt(timer / 60f);
        int sec = Mathf.FloorToInt(timer % 60f);


        string timeString = string.Format("{0:00}:{1:00}", min, sec);

        tempoQuestao.text = "Tempo Restante: " + timeString;
    }

    public void zeraTimer()
    {

        timer = Manager.time;
        // timer = 100f;
    }

    public void SetIndividual()
    {
        Manager.MOMENTO = "INDIVIDUAL";
        txt_geral.enabled = false;
        fechaPainelAguarde();

        btnAlternativas[0].gameObject.SetActive(true);
        btnAlternativas[1].gameObject.SetActive(true);
        btnAlternativas[2].gameObject.SetActive(true);
        btnAlternativas[3].gameObject.SetActive(true);

        btn5050.gameObject.SetActive(false);
        btnPular.gameObject.SetActive(false);
        
        foreach (Button btn in btnAlternativas)        
        {
            btn.gameObject.SetActive(true);
        }

        generalCommands.EnableAllObjectsInteractions();
        SetQntAlternatives(0);
        quadroChat.SetActive(false);
    }

    public void SetGrupo()
    {
        zeraTimer();
        Manager.MOMENTO = "GRUPO";
        txt_geral.enabled = false;

        btn5050.gameObject.SetActive(true);
        btnPular.gameObject.SetActive(true);
        
        SetQntAlternatives(1);
        quadroChat.SetActive(true);

        if (Manager.leaderId == dadosTimes.player.id)
        {
            painelAguarde("Como líder, converse com sua equipe e envie a respota final do grupo.");

            generalCommands.EnableAllObjectsInteractions();
        
            foreach (Button btn in btnAlternativas)        
            {
                btn.gameObject.SetActive(true);
            }

        }
        // Debug.Log("ID JOGADOR: " + dadosTimes.player.id);
        // Debug.Log("ID Lider: " + Manager.leaderId);
        if (dadosTimes.player.id != Manager.leaderId)
        {

            painelAguarde("Discutam a solução e aguarde a confirmação da resposta final pelo líder.");


            btnAlternativas[0].gameObject.SetActive(false);
            btnAlternativas[1].gameObject.SetActive(false);
            btnAlternativas[2].gameObject.SetActive(false);
            btnAlternativas[3].gameObject.SetActive(false);
            
            generalCommands.DisableAllObjectsInteractions();
            btnDica.interactable = true;
            btnOK.interactable = true;
            generalCommands.EnableInteraction(quadroChat);
        }
    }

    public void EncerraQuestao(string ans, int correct) 
    {
        
        if (correct == 1) {
            CanvasJogo.SetActive(false);
            CanvasRCerta.SetActive(true);
            txt_correto_resposta.text = "A resposta correta é " + answer.s;
            txt_pontuacao_correto.text = "Sua equipe ganhou 10 pontos\nLembre-se de conversar com os colegas de equipe";

            Manager.grpScore += 10;
        }
        else {
            CanvasJogo.SetActive(false);
            CanvasRErrada.SetActive(true);
            txt_errado_resposta.text = "Sua equipe respondeu " + answer.s + "\nA resposta correta é " + perguntaAtual.resposta;
            txt_pontuacao_errada.text = "Sua equipe ganhou 0 pontos\nLembre-se de conversar com os colegas de equipe";
            // txt_errado_resposta_dada.text = "A resposta correta é " + perguntaAtual.resposta;
        }

        if (Manager.leaderId == dadosTimes.player.id)
        {

            if (qst_respondidas == Manager.nQ_easy)
            {
                var msg = new ProxFase("PROXIMA_FASE", dadosTimes.player, Manager.teamId, Manager.sessionId,
                                                            Manager.gameId);

                 cm.send(msg);

            } else if (qst_respondidas == Manager.nQ_easy + Manager.nQ_medium)
            {
                var msg = new ProxFase("PROXIMA_FASE", dadosTimes.player, Manager.teamId, Manager.sessionId,
                                                            Manager.gameId);

                cm.send(msg);

            } else if (qst_respondidas == Manager.nQ_easy + Manager.nQ_medium + Manager.nQ_hard)
            {
            
            }

        }

        alternativas[0].enabled = true;
        alternativas[1].enabled = true;
        alternativas[2].enabled = true;
        alternativas[3].enabled = true;


        if (Manager.leaderId == dadosTimes.player.id)
        {
            var msg_prox = new ProxQuestao("PROXIMA_QUESTAO", dadosTimes.player, Manager.teamId, Manager.sessionId,
                                        Manager.gameId);

            cm.send(msg_prox);
        } 
   
        // Invoke("AtivarTelaJogo", 5f);




    }

    public void AtivarTelaJogo() 
    {
        CanvasJogo.SetActive(true);
        CanvasRCerta.SetActive(false);
        CanvasRErrada.SetActive(false);
        
        // if (qst_respondidas == Manager.nQ_easy)
        // {

        //     if (pulou == 0)
        //     {
        //         SetIndividual();
        //         CarregarPergunta();
        //     }

        //     qst = Manager.nQ_easy + 1;
        //     Manager.FASE = "Nível Médio";
        //     CanvasJogo.SetActive(false);
        //     CanvasFase.SetActive(true);
        //     SetLevelText();
        //     SetLeaderText();
        //     Invoke("NextQ", 5f);

        // } else if (qst_respondidas == Manager.nQ_easy + Manager.nQ_medium)
        // {

        //     if (pulou == 0)
        //     {
        //         SetIndividual();
        //         CarregarPergunta();
        //     }

        //     qst = Manager.nQ_easy + Manager.nQ_medium + 2;
        //     Manager.FASE = "Nível Difícil";

        //     CanvasJogo.SetActive(false);
        //     CanvasFase.SetActive(true);
        //     SetLevelText();
        //     SetLeaderText();
        //     Invoke("NextQ", 5f);

        // } else if (qst_respondidas == Manager.nQ_easy + Manager.nQ_medium + Manager.nQ_hard)
        // {
        
        // }
        
        // if (Manager.leaderId != dadosTimes.player.id)
        // {

        //     if (qst_respondidas == Manager.nQ_easy)
        //     {
        //         var msg = new ProxFase("PROXIMA_FASE", dadosTimes.player, Manager.teamId, Manager.sessionId,
        //                                                     Manager.gameId);

        //          cm.send(msg);

        //     } else if (qst_respondidas == Manager.nQ_easy + Manager.nQ_medium)
        //     {
        //         var msg = new ProxFase("PROXIMA_FASE", dadosTimes.player, Manager.teamId, Manager.sessionId,
        //                                                     Manager.gameId);

        //         cm.send(msg);

        //     } else if (qst_respondidas == Manager.nQ_easy + Manager.nQ_medium + Manager.nQ_hard)
        //     {
            
        //     }
        

        // }


        // SetIndividual();
        // ProximaQuestao();
        // var msg = new ProxQuestao("PROXIMA_QUESTAO", dadosTimes.player, Manager.teamId, Manager.sessionId,
        //                             Manager.gameId);

        // cm.send(msg);

    }



    public void MSG_NOVA_QUESTAO(string msgJSON) 
    {
        msgNOVA_QUESTAO message = JsonUtility.FromJson<msgNOVA_QUESTAO>(msgJSON);
        
        Manager.leaderId = message.leaderId;
        alt = message.alternativas;

        // qst++;

        SetIndividual();

        ProximaQuestao();
    }

    public void MSG_MOMENTO_GRUPO(string msgJSON)
    {
        msgMOMENTO_GRUPO message = JsonUtility.FromJson<msgMOMENTO_GRUPO>(msgJSON);



        Manager.leaderId = message.leaderId;
        altA.text = "" + message.answer.A;
        altB.text = "" + message.answer.B;
        altC.text = "" + message.answer.C;
        altD.text = "" + message.answer.D;
        
        SetGrupo();

    }

    public void MSG_FINAL_QUESTAO(string msgJSON)
    {

        msgFINAL_QUESTAO message = JsonUtility.FromJson<msgFINAL_QUESTAO>(msgJSON);
        
        answer.s = message.finalAnswer;
        correct = message.correct;

        qst++;
        qst_respondidas++;
        EncerraQuestao(answer.alternativa, correct);
        pontuacao.text = "Pontuação:" + Manager.grpScore;

    }

    public void MSG_AJUDA(string msgJSON)
    {
        msgAJUDA_5050 message = JsonUtility.FromJson<msgAJUDA_5050>(msgJSON);
        

        if (message.help == "pular"){

            txt_geral.text = "A equipe decidiu por pular a questão.";
            txt_geral.enabled = true;

            pulou = 1;
            qst++;

            SetIndividual();

            Invoke("DesativaTXT", 5f);
        }
        else{
            alt5050 = message.alternativa;


            btnAlternativas[alt5050[0]].gameObject.SetActive(false);
            btnAlternativas[alt5050[1]].gameObject.SetActive(false);
            alternativas[alt5050[0]].enabled = false;
            alternativas[alt5050[1]].enabled = false;
            qntAlternatives[alt5050[0]].gameObject.SetActive(false);
            qntAlternatives[alt5050[1]].gameObject.SetActive(false);

            // for (i=0, i < alternativas.Length();i++)
            // {
            //     if (i != alt5050[0] || i )
            // }
            
        }

    }

    public void MSG_NOVA_FASE(string msgJSON)
    {


        msgPROX_FASE message = JsonUtility.FromJson<msgPROX_FASE>(msgJSON);

        Manager.leaderId = message.leaderId;

        if (qst_respondidas == Manager.nQ_easy)
        {

            if (pulou == 0)
            {
                SetIndividual();
                CarregarPergunta();
            }

            qst = Manager.nQ_easy + 1;
            Manager.FASE = "Nível Médio";
            CanvasRCerta.SetActive(false);
            CanvasRErrada.SetActive(false);
            CanvasJogo.SetActive(false);
            CanvasFase.SetActive(true);
            SetLevelText();
            SetLeaderText();
            Invoke("NextQ", 3f);
            CanvasJogo.SetActive(true);


        } else if (qst_respondidas == Manager.nQ_easy + Manager.nQ_medium)
        {

            if (pulou == 0)
            {
                SetIndividual();
                CarregarPergunta();
            }

            qst = Manager.nQ_easy + Manager.nQ_medium + 2;
            Manager.FASE = "Nível Difícil";
            CanvasRCerta.SetActive(false);
            CanvasRErrada.SetActive(false);
            CanvasJogo.SetActive(false);
            CanvasFase.SetActive(true);
            SetLevelText();
            SetLeaderText();
            Invoke("NextQ", 3f);
            CanvasJogo.SetActive(true);


        } else if (qst_respondidas == Manager.nQ_easy + Manager.nQ_medium + Manager.nQ_hard)
        {
        
        }

    }


    public void handle(string ms)
    {

        //string messageType = ms.messageType;

        //executa JSON->messageType dentro do handle
        string messageType = JsonUtility.FromJson<ServerMessage>(ms).messageType;


        // Debug.Log("CENA JOGO: " + ms);
        // route message to handler based on message type

        if (messageType == "NOVA_QUESTAO") 
        {
            MSG_NOVA_QUESTAO(ms);
        }
        else if (messageType == "MOMENTO_GRUPO")
        {
            MSG_MOMENTO_GRUPO(ms);
        }
        else if (messageType == "FINAL_QUESTAO")
        {
            MSG_FINAL_QUESTAO(ms);
        }
        else if (messageType == "AJUDA_EQUIPE"){
            MSG_AJUDA(ms);
        }
        else if (messageType == "INICIA_NOVA_FASE"){
            MSG_NOVA_FASE(ms);
        }
    }

}

[System.Serializable]
public class msgNOVA_QUESTAO
{
    public string message_type;
    public int[] alternativas;
    public int teamId;
    public int leaderId;
    public int sessionId;
    public int gameId;
}

[System.Serializable]
public class msgAJUDA_PULAR
{
    public string message_type;
    public int teamId;
    public int sessionId;
    public int gameId;
    public string help;
}

[System.Serializable]
public class msgPROX_FASE
{
    public string message_type;
    public int teamId;
    public int leaderId;
    public int sessionId;
    public int gameId;
}

[System.Serializable]
public class msgAJUDA_5050
{
    public string message_type;
    public int teamId;
    public int sessionId;
    public int gameId;
    public string help;
    public int[] alternativa;

}


[System.Serializable]
public class msgMOMENTO_GRUPO
{
    public string message_type;
    public int teamId;
    public int leaderId;
    public string sessionId;
    public int gameId;
    public RespostasGrupo answer;
}

[System.Serializable]
public class RespostasGrupo
{
    public int A;
    public int B;
    public int C;
    public int D;
}

[System.Serializable]
public class msgFINAL_QUESTAO
{
    public string message_type;
    public int teamId;
    public string sessionId;
    public int gameId;
    public string finalAnswer;
    public int correct;
}

[System.Serializable]
public class ans
{
    public string s;
    public string alternativa;
    public int level;
    public int nrQ;

}