using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NativeWebSocket;
using TMPro;

public class instrucoes2 : MonoBehaviour
{
    //DEFINE E MANIPULA AS INSTRUCOES DO JOGO

    public GameObject painelInstrucoes;
    public TMP_Text instrucoesText;
    public TMP_Text paginaText;
    public List<string> instrLista = new List<string>();
    private int indiceInstrucaoAtual = 0;
    private int pagAtual = 1;
    private int instrucoesPorPagina = 1;

    private void Start()
    {
        
        DefinirInstrucoes();
        AtualizarInstrucao();
    }

    public void DefinirInstrucoes()
    {
        // string inst1 = "INSTRUÇÔES PAGINA 1";
        // string inst2 = "INSTRUÇÔES PAGINA 2";   
        // string inst3 = "INSTRUÇÔES PAGINA 3";
        // string inst4 = "INSTRUÇÔES PAGINA 4";

        string inst1 = @"ENTRADA
- Digite o seu nome e o código da sua equipe que o(a) professor(a) vai dar para você.
- Espere até que todos os jogadores tenham entrado.

LÍDER
- A cada etapa, um jogador da equipe será escolhido aleatoriamente para ser o líder. 
- O líder é a pessoa que irá estimular a comunicação entre os jogadores para confirmar a resposta da equipe";

        string inst2 = @"REGRAS DO JOGO
- Responda a pergunta escolhendo a resposta certa antes que o tempo acabe.
- Toda vez que você escolher uma resposta, você precisa confirmar a sua escolha.
- Se estiver com dificuldade para escolher uma resposta, você pode usar uma dica. A dica pode ser usada quantas vezes quiser.
- Espere até que todos os jogadores tenham respondido às suas perguntas.
- O jogo tem 3 níveis: fácil, médio e difícil. 
- Todos os integrantes da equipe podem ver quais alternativas foram mais escolhidas pelo grupo.
- Vocês devem conversar no chat com os membros da equipe para decidirem juntos qual é a melhor resposta para escolher.";   

        string inst3 = @"AJUDA
- Qualquer jogador da equipe pode pedir ao líder que use uma ajuda (escolher entre duas opções: 'pular' ou '50 50').
- 50 50 - Essa ajuda elimina metade das opções. A equipe só pode usá-la o número de vezes indicado no botão. Use com cuidado.
- Pular - A pergunta atual será trocada por uma diferente. O 'pular' só pode ser usado uma vez pela equipe.
                        
REFLEXÃO
- Sempre que a fase mudar, você tem a chance de refletir e dar feedbacks positivos aos membros da sua equipe.";

        string inst4 =  @"PONTUAÇÃO
- A cada questão certa, a equipe ganhará 10 pontos.
- Se todos os jogadores conversarem no chat, a equipe ganhará um bônus de 3 pontos pela interação.
- Se a resposta da equipe estiver errada, não receberão pontos por aquela pergunta. No entanto, se todos os jogadores conversarem no chat, a equipe ganhará um bônus de 3 pontos pela interação, mesmo que a resposta esteja errada.
- Se vocês terminarem o jogo sem usar as ajudas '50 50' ou 'Pular', a equipe ganhará um bônus por cada ajuda que não foi usada.
- Quando o jogo terminar, você verá a pontuação da sua equipe e o ranking com as pontuações de todas as equipes. Lembre-se de esperar até que todas as equipes terminem para ver a classificação geral.";

        instrLista.Add(inst1);
        instrLista.Add(inst2);
        instrLista.Add(inst3);
        instrLista.Add(inst4);
        

    }

    public void AtivaDesativaInstrucoes()
    {
        painelInstrucoes.SetActive(!painelInstrucoes.activeSelf);
    }

    public void ProximaInstrucao()
    {
        if (indiceInstrucaoAtual < instrLista.Count - 1)
        {
            indiceInstrucaoAtual++;
            if (indiceInstrucaoAtual % instrucoesPorPagina == 0)
            {
                pagAtual++;
            }
            AtualizarInstrucao();
        }
    }

    public void InstrucaoAnterior()
    {
        if (indiceInstrucaoAtual > 0)
        {
            indiceInstrucaoAtual--;
            if (indiceInstrucaoAtual % instrucoesPorPagina == instrucoesPorPagina - 1 || indiceInstrucaoAtual == 0)
            {
                pagAtual--;
            }
            AtualizarInstrucao();
        }
    }

    private void AtualizarInstrucao()
    {
        instrucoesText.text = instrLista[indiceInstrucaoAtual];
        paginaText.text = pagAtual + "/" + instrLista.Count;
    }

}
