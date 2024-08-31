using NativeWebSocket;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;
using TMPro;

public class Jogo : MonoBehaviour, IClient
{

    private ConnectionManager cm = ConnectionManager.getInstance();


// --------- VARIÁVEIS ---------

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

    public string txt_5050_individual;
    public string txt_pular_individual;


    public TMP_Text tempoQuestao; 
    public float timer = 20f;

    public GameObject CanvasJogo;
    public GameObject CanvasRCerta;
    public GameObject CanvasRErrada;
    public GameObject CanvasFase;
    public GameObject CanvasAvaliacao;


    public GameObject[] qntAlternatives;

    public GameObject confirmaAlternativa;
    public GameObject painelDica;
    public GameObject painelConfirma;
    public GameObject painelAjuda5050;
    public GameObject painelAjudaPular;
    public GameObject painelPulou;

    public GameObject painelMensagensProntas;

    public GameObject fundoPainel;


    public GameObject painel_aguarde;
    public TMP_Text txt_painelGeral;

    public GameObject quadroChat;
    public GameObject painelTexto;
    public GameObject painelChat;

    public TMP_InputField chatBox;

    public Button btnDica;
    public Button confirmaDica;
    public Button btnOK;
    public Button btnConfig;
    public Button btnOK_painel;
    public Button btnPular;
    public Button btn5050;
    public Button btnProfessor;

    public Button btnAbrirMensagensProntas;
    private float transparencia = 0.3f;
    private float sem_transparencia = 1.0f;
    private Color cor_btn_professor;
    private bool transparencia_btn_professor = false;

    public int[] ordem_alternativas;
    public int[] alt;
    public int[] alt5050;

    private int[] indice_5050;


    private DadosJogo perguntaAtual;
    private int numeroQuestao = 0;
    private int totalQuestoes;
    private int interaction = 0;
    private int nr5050 = 0;
    bool houveConsenso;
    bool houveInteracao;
    bool acertaramQuestao;

    static List<int> listaInteracoes = new List<int>();

    private Dictionary<int, int> enviouMSG = new Dictionary<int, int>();

    public Scrollbar bar;

    int zerouTimer = 0;

    int ID_TEAM;


    private string correctAnswer;

    [SerializeField]
    public List<msgCHAT> messageList = new List<msgCHAT>();

    public int chatMax = 25;

    public ScrollRect scrollRect;

    public ans answer;
    private int level;
    private int nrQuestion;
    private RespostasGrupo ansGroup;
    private int correct;
    private int pulou = 0;
    private int pulou_no_facil = 0;
    public bool pulou_na_fase = false;

    public bool entrou_nova_fase = false;

    private int qst;

    private int level_qst = 0;
    private int indice_qst = 0;
    private int qst_respondidas = 0;

    public TMP_Text txt_lider_jogo;

    //TELA FASE
    public float sec;
    public TMP_Text txt_nivel;
    public TMP_Text txt_lider; 

    public TMP_Text txt_nrEquipe;

    public int pontuacao_equipe;
    int bonusInteracao;


    public TMP_Text txt_pontuacao_correto;
    public TMP_Text txt_pontuacao_errada;

    public Transform ContentPlayers;
    public GameObject prefabPlayerAval;
    private int m_Itens;

    public static List<GameObject> quadrosPlayerAval = new List<GameObject>();

    //Fila de desconexão
    private Queue<int> filaDesconexao = new Queue<int>();

// --------- VARIÁVEIS ESTRELAS ---------

    
// --------- START ---------
    
    // Start is called before the first frame update
    void Start()
    {

        scrollRect.GetComponent<ScrollRect> ();
            
        tempoQuestao.text = "" + timer;
        pontuacao.text = "Pontuação: ";
        pontuacao.text = pontuacao.text + "0";

        txt_geral.enabled = false;

        dadosTimes.SetEquipe();
        SetQuadroEquipe();

        SetAlpha();
        SetQntAlternatives(0);

        painelDica.gameObject.SetActive(false);
        painelConfirma.gameObject.SetActive(false);
        painel_aguarde.SetActive(false);
        painelAjuda5050.SetActive(false);
        painelAjudaPular.SetActive(false);
        painelPulou.SetActive(false);

        Manager.totalQuestoes = 0;
        Manager.totalFacil = 0;
        Manager.totalMedio = 0;
        Manager.totalDificil = 0;
        qst = 0;
        indice_qst = 0;
        level_qst = 0;

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

    }

// --------- SETUPS ---------

    void atualizaTelaAvaliacao()
    {

        for (; filaDesconexao.Count > 0;)
        {
            int item = filaDesconexao.Dequeue();

            for (int i = 0; i < quadrosPlayerAval.Count; i++)
            {
                GameObject equipe = quadrosPlayerAval[i];

                if (equipe.GetComponent<id_avaliacao>().id_auxiliar_avaliacao == item)
                {
                    Destroy(equipe);
                }

            }

        }
    }

    void setTelaAvaliacao()
    {

        m_Itens = dadosTimes.meuTime.Count;

        foreach (GameObject player in quadrosPlayerAval)
        {
            Destroy(player);
        }

        for (int i = 0; i < m_Itens; i++)
        {
            GameObject novoPlayer = Instantiate(prefabPlayerAval, transform.position, Quaternion.identity);
            novoPlayer.transform.SetParent(ContentPlayers);
            novoPlayer.transform.localScale = new Vector3(3.07787f, 0.276828f, 0.6935131f);
            quadrosPlayerAval.Add(novoPlayer);

            // Button btn = novaEquipe.GetComponentInChildren<Button>();
            // btn.onClick.AddListener(() => btnTeamChat(teamId));
        }

        for (int i = 0; i < quadrosPlayerAval.Count; i++)
        {
            GameObject equipe = quadrosPlayerAval[i];
            TextMeshProUGUI[] textFields = equipe.GetComponentsInChildren<TextMeshProUGUI>();


            foreach (TextMeshProUGUI textField in textFields)
            {

                if (textField.CompareTag("txt_nome_player"))
                {
                    if (dadosTimes.meuTime.Count > i)
                    {
                        textField.text = dadosTimes.meuTime[i].name;

                        Debug.Log(equipe.GetComponent<id_avaliacao>().id_auxiliar_avaliacao);
                        Debug.Log(dadosTimes.meuTime[i].id);

                        equipe.GetComponent<id_avaliacao>().id_auxiliar_avaliacao =  dadosTimes.meuTime[i].id;
  
                        Debug.Log(equipe.GetComponent<id_avaliacao>().id_auxiliar_avaliacao);


                    }
                    break;
                }
            }
        }
        
    }

    public void SetQuadroEquipe() 
    {
        equipe_nr.text = "Equipe " + Manager.teamId;

        equipe_players.text = "";

        foreach (User user in dadosTimes.meuTime)
        {
            equipe_players.text += "\n" + user.name;
        }
    }

    public void SetLevelText()
    {
        txt_nivel.text = Manager.FASE;
        if (Manager.FASE == "Nível Médio")
        {
            txt_nivel.color =  new Color(1.0f, 0.64f, 0.0f);
        }
        if (Manager.FASE == "Nível Difícil")
        {
            txt_nivel.color = Color.red;
        }

    }

    public void SetLeaderText()
    {
        string t = dadosTimes.GetUser(Manager.leaderId);

        txt_lider.text = "Líder da fase: " + t;
        txt_lider_jogo.text = "Líder da fase: " + t;
    }

    void DesativaTXT()
    {
        txt_geral.enabled = false;
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

    public void SetAlpha()
    {
        Image btn5050Image = btn5050.image;
        Image btnPularImage = btnPular.image;
        Image btnProfessorImage = btnProfessor.image;

        if (Manager.MOMENTO == "INDIVIDUAL")
        {
            Color corAtual5050 = btn5050Image.color;
            corAtual5050.a = transparencia;
            btn5050Image.color = corAtual5050;

            Color corAtualPular = btnPularImage.color;
            corAtualPular.a = transparencia;
            btnPularImage.color = corAtualPular;

            Color corAtualProf = btnProfessorImage.color;
            corAtualProf.a = transparencia;
            btnProfessorImage.color = corAtualProf;
        }
        if (Manager.MOMENTO == "GRUPO") {
            Color corAtual5050 = btn5050Image.color;
            corAtual5050.a = sem_transparencia;
            btn5050Image.color = corAtual5050;

            Color corAtualPular = btnPularImage.color;
            corAtualPular.a = sem_transparencia;
            btnPularImage.color = corAtualPular;

            Color corAtualProf = btnProfessorImage.color;
            corAtualProf.a = sem_transparencia;
            btnProfessorImage.color = corAtualProf;
        }
        
    }

// --------- ALTERAÇÕES DE TELAS ---------


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
    }

    public void AtivarTelaFimDeJogo()
    {
        SceneManager.LoadScene("Fim");    

    }

    public void AtivarTelaJogo() 
    {
        CanvasJogo.SetActive(true);
        CanvasRCerta.SetActive(false);
        CanvasRErrada.SetActive(false);
    }

// --------- MANIPULAÇÃO DAS QUESTÕES E ALTERNATIVAS ---------

    void PrimeiraQuestao()
    {
        MSG_NOVA_QUESTAO(Manager.msgPrimeiraQuestao);
    }

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
        
        if (perguntaAtual.nivel == "facil")
        {
            answer.level = 0;
            nivel.text = "Nível Fácil";
            nivel.color = Color.green;
        }
        else if (perguntaAtual.nivel == "medio")
        {
            answer.level = 1;
            nivel.text = "Nível Médio";
            nivel.color = new Color(1.0f, 0.64f, 0.0f);

        }
        else
        {
            answer.level = 2;
            nivel.text = "Nível Difícil";
            nivel.color = Color.red;

        }


        // Printa as perguntas e respostas
        if (perguntaAtual != null)
        {
            correctAnswer = perguntaAtual.resposta; 
            pergunta.text = perguntaAtual.pergunta;
            dica.text = perguntaAtual.dica;

            // Debug.Log("LEVEL_QST = " + level_qst);
            // Debug.Log("INDICE_QST = " + indice_qst);    

            carregaDados.Shuffle(ref perguntaAtual, alt);

            // Debug.Log(perguntaAtual.resposta);
            // Debug.Log(perguntaAtual.r2);
            // Debug.Log(perguntaAtual.r3);
            // Debug.Log(perguntaAtual.r4);


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

    // Marca a alternativa e seleciona ela como resposta
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
        fundoPainel.SetActive(true);

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
        if (qst_respondidas <= Manager.nQ_total)
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

// --------- CONFIRMAÇÃO DAS RESPOSTAS E PONTUAÇÃO ---------

    void calculaInteracao(int id)
    {
        if(listaInteracoes.Contains(id))
        {

        }
        else
        {
            interaction++;
            listaInteracoes.Add(id);
        }

    }

    private int calculaBonus()
    {
        int bonus;

        float percentualInteracao = ((float) interaction)/((float) Manager.nrPlayerTeam);

        if (percentualInteracao <= 0.35)
        {
            bonus = 1;
        }
        else if (percentualInteracao <= 0.68)
        {
            bonus = 2;
        }
        else
        {
            bonus = 3;
        }

        return bonus;
    }

    public int bonificacao(bool houveConsenso, bool houveInteracao, bool acertaramQuestao)
    {

        int bonus;

        if (houveConsenso)
        {
            if (houveInteracao)
            {

                bonus = calculaBonus();

                if (acertaramQuestao)
                {
                    return bonus;
                }
                else
                {
                    return bonus;
                }
            }
            else
            {
                return 0;
            }
        }
        else
        {
            if (houveInteracao)
            {

                bonus = calculaBonus();

                if (acertaramQuestao)
                {
                    return bonus;
                }
                else
                {
                    return bonus;
                }
            }
            else
            {
                return 0;
            }
        }
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
    }

    public void ConfirmarRespostaIndividual()
    {
        fundoPainel.SetActive(false);
        painelConfirma.SetActive(false);

        correct = VerificaResposta();
        
        if (correct == 1) {
            dadosTimes.player.indScore += 10;
        }

        if (level_qst == 0) answer.nrQ = Manager.qEasy[indice_qst];
        else if (level_qst == 1) answer.nrQ = Manager.qMedium[indice_qst];
        else if (level_qst == 2) answer.nrQ = Manager.qHard[indice_qst];

        Debug.Log("TEAMID = " + Manager.teamId);
        Debug.Log("TEAMID = " + ID_TEAM);

        var msg = new RespostaIndividual("RESPOSTA_INDIVIDUAL", dadosTimes.player, ID_TEAM, Manager.sessionId,
                                        Manager.gameId, answer.alternativa, answer.level, answer.nrQ);

        zerouTimer = 1;

        cm.send(msg);

        btnAlternativas[0].gameObject.SetActive(false);
        btnAlternativas[1].gameObject.SetActive(false);
        btnAlternativas[2].gameObject.SetActive(false);
        btnAlternativas[3].gameObject.SetActive(false);


        // txt_geral.enabled = true;
        // txt_geral.text = "Aguarde até que todos enviem suas respostas.";
        painelAguarde("Aguarde até que todos enviem suas respostas.", 0);

    }

    public void ConfirmarRespostaFinal()
    {
        painelConfirma.SetActive(false);
        fundoPainel.SetActive(false);

        correct = VerificaResposta();
        
        // Debug.Log("Pessoas que enviaram msg: " + interaction);

        var msg = new RespostaFinal("RESPOSTA_FINAL", dadosTimes.player, ID_TEAM, Manager.sessionId, 
                                    Manager.gameId, answer.alternativa, correct, interaction);

        cm.send(msg);

        btnAlternativas[0].gameObject.SetActive(false);
        btnAlternativas[1].gameObject.SetActive(false);
        btnAlternativas[2].gameObject.SetActive(false);
        btnAlternativas[3].gameObject.SetActive(false);

    }

// --------- ATIVAÇÃO E DESATIVAÇÃO DE PAINEIS ---------

    public void painelAguarde(string s, int btn_ok)
    {
        painel_aguarde.SetActive(true);
        fundoPainel.SetActive(true);
        txt_painelGeral.text = "" + s;
        
        if (btn_ok == 1) btnOK_painel.gameObject.SetActive(true);
        else btnOK_painel.gameObject.SetActive(false);
    }
    
    public void fechaPainelAguarde()
    {
        painel_aguarde.SetActive(false);
        fundoPainel.SetActive(false);
    }

    public void fechaConfirma()
    {
        painelConfirma.SetActive(false);
        fundoPainel.SetActive(false);
    }

    public void fechaPainel5050()
    {
        painelAjuda5050.SetActive(false);
        fundoPainel.SetActive(false);

    }

    public void fechaPainelPULAR()
    {
        painelAjudaPular.SetActive(false);
        fundoPainel.SetActive(false);
    }

    public void ajudaDica()
    {
        painelDica.SetActive(true);
        fundoPainel.SetActive(true);
    }

    public void confirmarDica()
    {
        painelDica.SetActive(false);
        fundoPainel.SetActive(false);
    }

    public void fechaPulou()
    {
        painelPulou.SetActive(false);
        fundoPainel.SetActive(false);
    }


// --------- FUNÇÕES DAS AJUDAS ---------


    public void confirmaPULAR()
    {
                var msg = new PedirAjuda("PEDIR_AJUDA", dadosTimes.player, ID_TEAM, Manager.sessionId,
                                        Manager.gameId, answer.level, answer.nrQ, "pular");

                cm.send(msg);

                painelAjudaPular.SetActive(false);
                // txt_geral.text = "Computando ajuda...";
                // txt_geral.enabled = true;
                // Invoke("DesativaTXT", 3f);
                painelAguarde("Computando ajuda...", 1);
    }

    public void ajudaPula()
    {
        fundoPainel.SetActive(true);
        if (Manager.MOMENTO == "INDIVIDUAL") 
        {
            // txt_geral.text = txt_pular_individual;
            // txt_geral.enabled = true;
            // Invoke("DesativaTXT", 5f);
            painelAguarde("PULAR só pode ser usada no momento em grupo.", 1);

        }
        if(pulou == 1)
        {
            painelAguarde("A equipe utilizou todos os pulos (1).", 1);
        }
        else {
            if (Manager.MOMENTO == "GRUPO"){
                if (Manager.leaderId == dadosTimes.player.id){

                    painelAjudaPular.SetActive(true);

                }else {
                    // txt_geral.text = "Somente o líder pode solicitar esse tipo de ajuda. Converse com ele via chat para usá-la.";
                    // txt_geral.enabled = true;
                    // Invoke("DesativaTXT", 5f);
                    painelAguarde("Somente o líder pode solicitar esse tipo de ajuda. Converse com ele via chat para usá-la.", 1);
                }
            }
        }
    }

    public void ajudaGasta(int pulou)
    {

        Image btnPularImage = btnPular.image;
        Image btn5050Image = btn5050.image;

        if (pulou == 1)
        {
            Color corAtualPular = btnPularImage.color;
            corAtualPular.a = transparencia;
            btnPularImage.color = corAtualPular;
        }
        if (nr5050 == Manager.nrHelp5050)
        {
            Color corAtual5050 = btn5050Image.color;
            corAtual5050.a = transparencia;
            btn5050Image.color = corAtual5050;
        }
    }

    public void confirma5050()
    {
                var msg = new PedirAjuda("PEDIR_AJUDA", dadosTimes.player, ID_TEAM, Manager.sessionId,
                                        Manager.gameId, answer.level, answer.nrQ, "5050");

                cm.send(msg);

                painelAjuda5050.SetActive(false);

                
                // txt_geral.text = "Computando ajuda...";
                // txt_geral.enabled = true;
                // Invoke("DesativaTXT", 3f);
                painelAguarde("Computando ajuda...", 1);

                
    }

    public void ajuda5050()
    {
        fundoPainel.SetActive(true);
        if (Manager.MOMENTO == "INDIVIDUAL") 
        {
            // txt_geral.text = txt_5050_individual;
            // txt_geral.enabled = true;
            // Invoke("DesativaTXT", 5f);
            painelAguarde("50/50 só pode ser usada no momento em grupo.", 1);

        }
        if (nr5050 == Manager.nrHelp5050)
        {
            painelAguarde("A equipe utilizou todas as ajudas 50/50 ("+Manager.nrHelp5050+").", 1);
        }
        else {
            if (Manager.MOMENTO == "GRUPO"){
                if (Manager.leaderId == dadosTimes.player.id) {
                    
                    painelAjuda5050.SetActive(true);

                } else {
                    // txt_geral.text = "Somente o líder pode solicitar esse tipo de ajuda. Converse com ele via chat para usá-la.";
                    // txt_geral.enabled = true;
                    // Invoke("DesativaTXT", 5f);
                    painelAguarde("Somente o líder pode solicitar esse tipo de ajuda. Converse com ele via chat para usá-la.", 1);

                }
            }
        }
    }

    public void Ajuda5050(int[] alternativas_mantidas)
    {
        // ordem_alternativas
        nr5050+=1;

        for(int i=0; i < alternativas_mantidas.Length; i++)
        {
            for(int j=0; j < ordem_alternativas.Length; j++)
            {
                if (ordem_alternativas[j] == alternativas_mantidas[i])
                {
                    ordem_alternativas[j] = -1;
                }
            }
        }

        for(int k=0; k < ordem_alternativas.Length; k++)
        {
            Debug.Log(ordem_alternativas[k]);
        }

        for(int k=0; k < ordem_alternativas.Length; k++)
        {
           if (ordem_alternativas[k] != -1)
           {
                btnAlternativas[k].gameObject.SetActive(false);
                alternativas[k].enabled = false;
                qntAlternatives[k].gameObject.SetActive(false);
           }
        }

        ajudaGasta(pulou);
    }

    public void ajudaDuvida()
    {
        var msg = new Duvida("DUVIDA", ID_TEAM, Manager.sessionId, Manager.gameId);

        cm.send(msg);

        if (!transparencia_btn_professor)
        {
            cor_btn_professor = btnProfessor.image.color;
            Color newColor = cor_btn_professor;
            newColor.a = 0.4f;
            btnProfessor.image.color = newColor;
            btnProfessor.interactable = false;


            StartCoroutine(ResetTransparencyAfterDelay());
        }
    }

    private System.Collections.IEnumerator ResetTransparencyAfterDelay()
    {
        transparencia_btn_professor = true;
        yield return new WaitForSeconds(20f);
        btnProfessor.image.color = cor_btn_professor;
        transparencia_btn_professor = false;
        btnProfessor.interactable = true;
    }

     public void mensagemPronta()
    {
        painelMensagensProntas.SetActive(true);
        fundoPainel.SetActive(true);
    }
     public void fechaMensagemPronta()
    {
        painelMensagensProntas.SetActive(false);
        fundoPainel.SetActive(false);
    }
     public void mensagemProntaBtns(int id)
    {
        string textoMensagemPronta;
        if(id == 0){
            textoMensagemPronta = "Vamos com a maioria!";
        }
        else if(id == 1){
            textoMensagemPronta = "Vamos usar ajuda 50/50!";
        }
        else{
            textoMensagemPronta = "Acho melhor pular...";
        }
        var msg = new mensagemChat("MENSAGEM_CHAT", dadosTimes.player, ID_TEAM, Manager.sessionId, Manager.gameId, textoMensagemPronta, false);
        cm.send(msg);
        painelMensagensProntas.SetActive(false);
        fundoPainel.SetActive(false);
        chatBox.ActivateInputField ();
    }
    


// --------- TIMER ---------


    public void atualizaTimer()
    {
        timer -= Time.deltaTime;
        
        if (zerouTimer == 0)
        {
            if (timer <= 0f)
            {
            zerouTimer = 1;
            
            if (Manager.MOMENTO == "INDIVIDUAL") 
            {
                    if (level_qst == 0) answer.nrQ = Manager.qEasy[indice_qst];
                    else if (level_qst == 1) answer.nrQ = Manager.qMedium[indice_qst];
                    else if (level_qst == 2) answer.nrQ = Manager.qHard[indice_qst];

                    Debug.Log("NUMERO QUESTAO: " + answer.nrQ);
                    var msg = new RespostaIndividual("RESPOSTA_INDIVIDUAL", dadosTimes.player, ID_TEAM, Manager.sessionId,
                                            Manager.gameId, "", answer.level, answer.nrQ);

                    cm.send(msg);
            }
            }

            int min = Mathf.FloorToInt(timer / 60f);
            int sec = Mathf.FloorToInt(timer % 60f);


            string timeString = string.Format("{0:00}:{1:00}", min, sec);

            tempoQuestao.text = "" + timeString;
        }   
        else {
            // tempoQuestao.text = "Aguarde até que todos enviem suas respostas.";
            tempoQuestao.text = "";
        } 
    }

    public void zeraTimer()
    {

        timer = Manager.time;
        // timer = 100f;
    }

// --------- SET MOMENTOS ---------

    public void SetIndividual()
    {
        Manager.MOMENTO = "INDIVIDUAL";
        txt_geral.enabled = false;
        tempoQuestao.enabled = true;

        txt_5050_individual = "50/50 só pode ser usada no momento em grupo.";
        txt_pular_individual = "PULAR só pode ser usada no momento em grupo.";

        fechaPainelAguarde();
        fundoPainel.SetActive(false);

        btnAlternativas[0].gameObject.SetActive(true);
        btnAlternativas[1].gameObject.SetActive(true);
        btnAlternativas[2].gameObject.SetActive(true);
        btnAlternativas[3].gameObject.SetActive(true);
        alternativas[0].enabled = true;
        alternativas[1].enabled = true;
        alternativas[2].enabled = true;
        alternativas[3].enabled = true;


        // btn5050.gameObject.SetActive(false);
        // btnPular.gameObject.SetActive(false);
        
        btn5050.interactable = true;
        btnPular.interactable = true;    
        btnProfessor.interactable = false;
        SetAlpha();
        Debug.Log("PULOS: " + pulou);
        ajudaGasta(pulou);

           
        foreach (Button btn in btnAlternativas)        
        {
            btn.gameObject.SetActive(true);
        }

        generalCommands.EnableAllObjectsInteractions();
        SetQntAlternatives(0);
        quadroChat.SetActive(false);
        chatBox.gameObject.SetActive(false);
        btnAbrirMensagensProntas.gameObject.SetActive(false);
        painelMensagensProntas.SetActive(false);
    }

    public void SetGrupo()
    {
        entrou_nova_fase = false;
        zeraTimer();
        tempoQuestao.enabled = false;
        
        Manager.MOMENTO = "GRUPO";
        txt_geral.enabled = false;

        btn5050.gameObject.SetActive(true);
        btnPular.gameObject.SetActive(true);
        btnProfessor.gameObject.SetActive(true);
        Debug.Log("PULOS: " + pulou);
        
        SetQntAlternatives(1);
        quadroChat.SetActive(true);
        chatBox.gameObject.SetActive(true);
        btnAbrirMensagensProntas.gameObject.SetActive(true);


        SetAlpha();
        ajudaGasta(pulou);


        if (Manager.leaderId == dadosTimes.player.id)
        {
            painelAguarde("Como líder, converse com sua equipe e envie a respota final do grupo.", 1);
            fundoPainel.SetActive(true);
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

            painelAguarde("Discutam a solução e aguarde a confirmação da resposta final pelo líder.", 1);
            fundoPainel.SetActive(true);

            btnAlternativas[0].gameObject.SetActive(false);
            btnAlternativas[1].gameObject.SetActive(false);
            btnAlternativas[2].gameObject.SetActive(false);
            btnAlternativas[3].gameObject.SetActive(false);
            
            generalCommands.DisableAllObjectsInteractions();
            btnDica.interactable = true;
            btnOK.interactable = true;
            btn5050.interactable = true;
            btnPular.interactable = true;
            btnProfessor.interactable = true;
            confirmaDica.interactable = true;
            btnConfig.interactable = true;
            generalCommands.EnableInteraction(quadroChat);
            generalCommands.EnableInteraction(chatBox.gameObject);
            generalCommands.EnableInteraction(btnAbrirMensagensProntas.gameObject);
        }
    }

    void MOMENTO_AVALIACAO()
    {
        CanvasRCerta.SetActive(false);
        CanvasRErrada.SetActive(false);
        CanvasJogo.SetActive(false);
        CanvasFase.SetActive(false);
        CanvasAvaliacao.SetActive(true);

        setTelaAvaliacao();


        atualizaTelaAvaliacao();                  
    }

// --------- ENCERRAMENTO DAS QUESTÕES ---------


    public void EncerraQuestao(string ans, int correct) 
    {
        
        if (correct == 1) {
            CanvasJogo.SetActive(false);
            CanvasRCerta.SetActive(true);
            txt_correto_resposta.text = "" + correctAnswer;
            if (bonusInteracao != 3)
            {
                txt_pontuacao_correto.text = "Sua equipe ganhou " + (10+bonusInteracao) + " pontos\nLembrem-se de que a colaboração é importante";
            }
            else 
            {
                txt_pontuacao_correto.text = "Sua equipe ganhou "  + (10+bonusInteracao) + " pontos\nQue bom que colaboraram!";

            }

            Manager.grpScore += 10;
        }
        else {
            CanvasJogo.SetActive(false);
            CanvasRErrada.SetActive(true);
            txt_errado_resposta.text = "\nA resposta correta é " + correctAnswer;
            if (bonusInteracao != 0)
            {
                txt_pontuacao_errada.text = "Sua equipe ganhou "  + (bonusInteracao) + " pontos pela colaboração\nLembrem-se de que a colaboração é importante";

            }
            else 
            {
                txt_pontuacao_errada.text = "Sua equipe ganhou "  + (bonusInteracao) + " pontos\nLembre-se que a colaboração é importante";

            }
        }

        if (Manager.leaderId == dadosTimes.player.id)
        {

            if (qst_respondidas == Manager.nQ_easy)
            {
                entrou_nova_fase = true;
                var msg = new ProxFase("PROXIMA_FASE", dadosTimes.player, ID_TEAM, Manager.sessionId,
                                                            Manager.gameId);

                 cm.send(msg);
                  pulou_na_fase = false;
                 indice_qst = 0;

            } else if (qst_respondidas == Manager.nQ_easy + Manager.nQ_medium)
            {
                entrou_nova_fase = true;
                var msg = new ProxFase("PROXIMA_FASE", dadosTimes.player, ID_TEAM, Manager.sessionId,
                                                            Manager.gameId);

                cm.send(msg);
                pulou_na_fase = false;
                indice_qst = 0;

            } else if (qst_respondidas == Manager.nQ_easy + Manager.nQ_medium + Manager.nQ_hard)
            {

                // var msg = new FimDeJogo("FIM_DE_JOGO", dadosTimes.player, ID_TEAM, Manager.sessionId,
                //                                             Manager.gameId, Manager.grpScore, Manager.gameTime);
                entrou_nova_fase = true;
                pulou_na_fase = false;

                // cm.send(msg);    

                // Invoke("AtivarTelaFimDeJogo", 5f);

                avaliacaoUltimaFase();
            } 

        } else if (qst_respondidas == Manager.nQ_easy + Manager.nQ_medium + Manager.nQ_hard)
        {

                // var msg = new FimDeJogo("FIM_DE_JOGO", dadosTimes.player, ID_TEAM, Manager.sessionId,
                //                                             Manager.gameId, Manager.grpScore, Manager.gameTime);
                entrou_nova_fase = true;
                pulou_na_fase = false;
                // cm.send(msg);

                // Invoke("AtivarTelaFimDeJogo", 5f);
                avaliacaoUltimaFase();
        }

        alternativas[0].enabled = true;
        alternativas[1].enabled = true;
        alternativas[2].enabled = true;
        alternativas[3].enabled = true;


        if (Manager.leaderId == dadosTimes.player.id && (qst_respondidas != Manager.nQ_easy + Manager.nQ_medium + Manager.nQ_hard))
        {
            var msg_prox = new ProxQuestao("PROXIMA_QUESTAO", dadosTimes.player, ID_TEAM, Manager.sessionId,
                                        Manager.gameId, pulou_na_fase, entrou_nova_fase);

            cm.send(msg_prox);

            indice_qst++;

        } else {
            indice_qst++;
        }
    }

// --------- CONFIRMA AVALIAÇÃO ---------

    public async void avaliacaoUltimaFase()
    {
        await Task.Delay(5000);
        MOMENTO_AVALIACAO();
        
    }

    public void confirmaAvaliacao(){

        var avaliacao = new Aval("AVALIACAO", dadosTimes.player, ID_TEAM, dadosTimes.meuTime, Manager.sessionId, Manager.gameId);
        
        cm.send(avaliacao);

        Manager.reset_estrelas = 1;

        CanvasAvaliacao.SetActive(false);

    /*    if (qst_respondidas == Manager.nQ_easy)
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
            Invoke("NextQ", 10f);
            // CanvasJogo.SetActive(true);


        } else if (qst_respondidas == Manager.nQ_easy + Manager.nQ_medium)
        {

            if (pulou == 0 || pulou_no_facil == 1)
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
            Invoke("NextQ", 10f);
                // CanvasJogo.SetActive(true);


        } else if (qst_respondidas == Manager.nQ_easy + Manager.nQ_medium + Manager.nQ_hard)
        {*/

            Manager.teamId = ID_TEAM;
            var msg = new FimDeJogo("FIM_DE_JOGO", dadosTimes.player, ID_TEAM, Manager.sessionId,
                                                            Manager.gameId, Manager.grpScore, Manager.gameTime);

                

            cm.send(msg);    

            Invoke("AtivarTelaFimDeJogo", 5f);
            SceneManager.LoadScene("Fim");    

        
       /* }
        ProximaQuestao();*/

    }

     private void EscondeClicandoFora(GameObject panel) {
        if (Input.GetMouseButton(0) && panel.activeSelf && 
            !RectTransformUtility.RectangleContainsScreenPoint(
                panel.GetComponent<RectTransform>(), 
                Input.mousePosition, 
                Camera.main)) { 
            panel.SetActive(false);
            fundoPainel.SetActive(false);
        }
    }


// --------- HANDLE ---------

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
        else if (messageType == "ENCERRAR"){
            var msg = new FimDeJogo("FIM_DE_JOGO", dadosTimes.player, ID_TEAM, Manager.sessionId,
                                    Manager.gameId, Manager.grpScore, Manager.gameTime);

            cm.send(msg);  
            
            SceneManager.LoadScene("Fim");
        }
        else if (messageType == "MENSAGEM_CHAT")
        {
            MSG_CHAT(ms);
        }
        else if (messageType == "DESCONEXAO")
        {
            MSG_DESCONEXAO(ms);
        }
    }

// --------- DESENVOLVIMENTO DAS MSG ---------

    public void MSG_DESCONEXAO(string msgJSON)
    {
        msgDESCONEXAO message = JsonUtility.FromJson<msgDESCONEXAO>(msgJSON);

        Debug.Log("Alguém se desconectou:");
        Debug.Log("Usuário: "+ message.user);
        Debug.Log("Time: "+ message.teamId);
        Debug.Log(message);
        Debug.Log(message.user);
        Debug.Log(message.user.id);
        

        if (message.leaderId == -1)
        {
            Debug.Log("O membro NÃO era o líder do grupo");
            
            filaDesconexao.Enqueue(message.user.id);
            dadosTimes.removeFromEquipe(message.user.id);
            SetQuadroEquipe();
        }
        else 
        {
            Debug.Log("O membro era o líder do grupo.\nO novo líder é o membro de ID: " + message.leaderId);

            filaDesconexao.Enqueue(message.user.id);
            dadosTimes.removeFromEquipe(message.user.id);
            SetQuadroEquipe();

            Manager.leaderId = message.leaderId;
            SetLeaderText();

            if (Manager.leaderId == dadosTimes.player.id)
            {
                painelAguarde("O líder se desconectou. Você é o novo líder da equipe.", 1);
                fundoPainel.SetActive(false);

                generalCommands.EnableAllObjectsInteractions();
            
                if (Manager.MOMENTO == "GRUPO")
                {
                    foreach (Button btn in btnAlternativas)        
                    {
                        btn.gameObject.SetActive(true);
                    }
                }

            }

        }

        
    }


    public void MSG_NOVA_QUESTAO(string msgJSON) 
    {
        msgNOVA_QUESTAO message = JsonUtility.FromJson<msgNOVA_QUESTAO>(msgJSON);
        
        Manager.leaderId = message.leaderId;
        alt = message.alternativas;
        ordem_alternativas = message.alternativas;
        ID_TEAM = message.teamId;

        zerouTimer = 0;

        SetIndividual();

        if (!message.entrou_nova_fase)
        {
            Debug.Log("entrou if pular");
            ProximaQuestao();
        }

    }

    public void MSG_MOMENTO_GRUPO(string msgJSON)
    {
        msgMOMENTO_GRUPO message = JsonUtility.FromJson<msgMOMENTO_GRUPO>(msgJSON);

        interaction = 0;

        Manager.leaderId = message.leaderId;
        altA.text = "" + message.answer.A;
        altB.text = "" + message.answer.B;
        altC.text = "" + message.answer.C;
        altD.text = "" + message.answer.D;

        int contadorZero = 0;

        if (message.answer.A == 0)
        {
            contadorZero++;
        }
        if (message.answer.B == 0)
        {
            contadorZero++;
        }
        if (message.answer.C == 0)
        {
            contadorZero++;
        }
        if (message.answer.D == 0)
        {
            contadorZero++;
        }
        if (contadorZero == 3)
        {
            houveConsenso = true;
        }
        else
        {
            houveConsenso = false;
        }

        listaInteracoes.Clear();
        
        SetGrupo();

    }

    public void MSG_FINAL_QUESTAO(string msgJSON)
    {

        msgFINAL_QUESTAO message = JsonUtility.FromJson<msgFINAL_QUESTAO>(msgJSON);
        
        answer.s = message.finalAnswer;
        correct = message.correct;

        if (correct == 1) acertaramQuestao = true;
        else acertaramQuestao = false;

        if (interaction == 0) houveInteracao = false;
        else houveInteracao = true;

        bonusInteracao = bonificacao(houveConsenso, houveInteracao, acertaramQuestao);
        
        Manager.grpScore = Manager.grpScore + bonusInteracao;

        qst++;
        qst_respondidas++;
        EncerraQuestao(answer.alternativa, correct);
        pontuacao.text = "Pontuação:" + Manager.grpScore;

    }

    public void MSG_AJUDA(string msgJSON)
    {
        msgAJUDA_5050 message = JsonUtility.FromJson<msgAJUDA_5050>(msgJSON);

        if (message.help == "pular"){

            if(pulou == 1)
            {
                txt_geral.text = "A equipe já utilizou a ajuda PULAR (Máximo por equipe: 1).";
                txt_geral.enabled = true;
                Invoke("DesativaTXT", 5f);

            }
            else {

                txt_geral.text = "A equipe decidiu por pular a questão.";
                txt_geral.enabled = true;
                painelPulou.SetActive(true);
                fundoPainel.SetActive(true);

                pulou = 1;
                pulou_na_fase = true;
                if (perguntaAtual.nivel == "facil") pulou_no_facil = 1;

                indice_qst++;
                SetIndividual();

                Invoke("DesativaTXT", 5f);
            }
        } else {
            alt5050 = message.alternativa;
            
            Ajuda5050(alt5050);
            
        }

    }

    public void MSG_NOVA_FASE(string msgJSON)
    {

        msgPROX_FASE message = JsonUtility.FromJson<msgPROX_FASE>(msgJSON);

        Manager.leaderId = message.leaderId;
        Manager.teamId = message.teamId;

        indice_qst = 0;
        level_qst++;
        
        if (qst_respondidas == Manager.nQ_easy)
        {
            Debug.Log("valor pulou = " + pulou);
            if (pulou == 0)
            {
                //SetIndividual();
                //CarregarPergunta();
                carregaDados.listaDados.RemoveAt(0);
            }

            qst = Manager.nQ_easy + 1;
            Manager.FASE = "Nível Médio";

            CanvasRCerta.SetActive(false);
            CanvasRErrada.SetActive(false);
            CanvasJogo.SetActive(false);
            CanvasFase.SetActive(true);
            SetLevelText();
            SetLeaderText();
            Invoke("NextQ", 10f);
            // CanvasJogo.SetActive(true);


        } else if (qst_respondidas == Manager.nQ_easy + Manager.nQ_medium)
        {

            if (pulou == 0 || pulou_no_facil == 1)
            {
                //SetIndividual();
                //CarregarPergunta();
                carregaDados.listaDados.RemoveAt(0);
            }

            qst = Manager.nQ_easy + Manager.nQ_medium + 2;
            Manager.FASE = "Nível Difícil";
            CanvasRCerta.SetActive(false);
            CanvasRErrada.SetActive(false);
            CanvasJogo.SetActive(false);
            CanvasFase.SetActive(true);
            SetLevelText();
            SetLeaderText();
            Invoke("NextQ", 10f);

        }
        
        ProximaQuestao();

    }

    public void MSG_CHAT(string msgJSON)
    {

        Color cor;
        msgCHAT message = JsonUtility.FromJson<msgCHAT>(msgJSON);

        if(messageList.Count >= chatMax){
            // Destroy(messageList[0].painelTexto.gameObject);
            // messageList.Remove(messageList[0]);
        }

        int remetenteId = message.user.id;

        Debug.Log(remetenteId);
        
        calculaInteracao(remetenteId);

        msgCHAT textoChat = new msgCHAT();

        if (message.moderator)
        {
            textoChat.texto = "MODERADOR - " + message.user.name + ": " + message.texto; // Se é moderador
        }
        else
        {
           textoChat.texto = message.user.name + ":" + message.texto; 
        }

        GameObject novoChat = Instantiate(painelTexto, painelChat.transform);

        textoChat.painelTexto = novoChat.GetComponent<Text>();

        textoChat.painelTexto.text = textoChat.texto;
       if(scrollRect.normalizedPosition.y < 0.0001f)
            scrollRect.velocity = new Vector2 (0f, 1000f);

        if(message.moderator){
            ColorUtility.TryParseHtmlString("#f41004", out cor);
            textoChat.painelTexto.fontStyle = FontStyle.Bold;
        }
        else{
            //textoChat.painelTexto.fontStyle = FontStyle.Regular;
            if(message.user.name == dadosTimes.player.name)
                ColorUtility.TryParseHtmlString("#0505B1", out cor);
            else
                ColorUtility.TryParseHtmlString("#112A46", out cor);
        }

        textoChat.painelTexto.color = cor;
        messageList.Add(textoChat);


        Debug.Log(textoChat.texto);
    }


// --------- UPDATE ---------

    void Update()
    {
        atualizaTimer();

        cm.retrieveMessages(this);

        if(chatBox.text != "")
        {
            if(Input.GetKeyDown(KeyCode.Return)){
                var msg = new mensagemChat("MENSAGEM_CHAT", dadosTimes.player, ID_TEAM, Manager.sessionId, Manager.gameId, chatBox.text, false);
                //var msg = new mensagemChat("MENSAGEM_CHAT", dadosTimes.player, Manager.teamId, Manager.sessionId, Manager.gameId, chatBox.text, Manager.moderator);
                cm.send(msg);
               // readChat(chatBox.text);
                chatBox.text = "";
                chatBox.ActivateInputField ();
            }
        }
        if(chatBox.isFocused == true){
            quadroChat.SetActive(true);
        }
        else{
            quadroChat.SetActive(false);
        }
        EscondeClicandoFora(painelMensagensProntas);
        EscondeClicandoFora(painelDica);
        EscondeClicandoFora(painelConfirma);
        EscondeClicandoFora(painelAjuda5050);
        EscondeClicandoFora(painelAjudaPular);
        EscondeClicandoFora(painelPulou);


        // if (Manager.reset_estrelas == 1){
        //     resetaEstrelas();
        //     Manager.reset_estrelas = 0;
        // }
            
    }

}

// --------- ESTRELAS ---------



// --------- MENSAGENS ---------

[System.Serializable]
public class msgDESCONEXAO
{
    public string message_type;
    public User user;
    public int teamId;
    public int sessionId;
    public int gameId;
    public int leaderId;
}

[System.Serializable]
public class msgCHAT
{
    public string message_type;
    public string texto;

    public Text painelTexto;
    public User user;

    public int teamId;
    public string sessionId;
    public int gameId;
    public bool moderator;
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
    public bool pulou_na_fase;

    public bool entrou_nova_fase;
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