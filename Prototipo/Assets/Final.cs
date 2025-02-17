using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
public class Final : MonoBehaviour
{
    public Image comeco;

    public Image tiposFinal;
    public Sprite[] spritearray;

    public CanvasGroup canvasGroup;

    public CanvasGroup canvasGroupCarta;
    public bool fadeOut;

    public bool fadein;

    public int final;

    public AudioClip[] falas;
    public AudioSource fala;

    public AudioClip[] efeitos;
    public AudioSource efeito;

    private String[] roteiro;
    public Text texto;

    private string roteiro_aux;
    public Image UI;

    public Sprite[] UIs;

    private String[] nomes;
    public Text nome;

    private int carrega;
    public int num_texto;

    public float velocidade;
    public bool fim;

    public float teste;

    public int pontuacao;

    public int pontuacao_total;

    public int intervalos;

    
    public AudioSource pularaudio;

    public AudioClip[] variantes_carta;

    // Start is called before the first frame update

    void Start()
    {
        pontuacao = Manager.grpScore;
        PontuacaoIntervalos();
        CarregarTexto();
        canvasGroup.alpha = 1;
        fadeOut = true;
        //Informacoes.SetCaminhos(1);
        carrega = 0;
        fim = false;
        roteiro_aux = roteiro[num_texto++];
        texto.text = "";
        //PontuacaoIntervalos();
    }

    public int quantidadeFacil = Manager.nQ_easy;
    public int quantidadeMedio = Manager.nQ_medium;
    public int quantidadeDificil = Manager.nQ_hard;
    public int quantidade5050 = Manager.nrHelp5050;
    //public int quantidadeAcertos = Informacoes.GetQuantidadeAcertos();

    void PontuacaoIntervalos(){

        Debug.Log("quantidadeTotal é " + Manager.nQ_total);

        pontuacao_total = 13*Manager.nQ_total;
      
        intervalos = pontuacao_total/4;

        canvasGroupCarta.alpha = 1;
        Debug.Log("pontuacao é " + pontuacao);
        Debug.Log("intervalo é " + intervalos);
        if(pontuacao <= intervalos){
            final = 0;
        }
        if(pontuacao > intervalos && pontuacao <= 2*intervalos){
            final = 1;
        }
        if(pontuacao > 2*intervalos && pontuacao <= 3*intervalos){
            final = 2;
        }
        if(pontuacao > 3*intervalos){
            final = 3;
        }


        if (final == 0)
        {
            tiposFinal.sprite = spritearray[0];
        }
        else if (final == 1)
        {
            tiposFinal.sprite = spritearray[1];
        }
        else if (final == 2)
        {
            tiposFinal.sprite = spritearray[2];
        }
        else if (final == 3)
        {
            tiposFinal.sprite = spritearray[3];
        }

    }

    public void Pular(){
        comeco.gameObject.SetActive(true);
		fadein = true;

    }

    public void Prosseguir(){

        if(fim == true){
            roteiro_aux = roteiro[num_texto++];
            texto.text = "";
            carrega = 0;
            fim = false;
        }else{
            roteiro_aux = roteiro[num_texto-1];
            texto.text = roteiro_aux;
            carrega = roteiro_aux.Length;
            fim = true;
            if(num_texto >= 10){

                comeco.gameObject.SetActive(true);
                fadein = true;
            }
            velocidade = 0;
        }
    }

    void Cartas(){



        if(final == 0){
            roteiro[2] = " Só queria mandar um super obrigado por toda a ajuda, sem ela não conseguiria uma moradia.";
            roteiro[3] = " Você foi demais! Sem sua orientação, eu provavelmente ainda estaria procurando um lugar adequado.";
            roteiro[4] = " Estou adorando o novo espaço. É perfeito para recarregar minhas baterias.";
            roteiro[5] = " Valeu mesmo por tudo! Se precisar de qualquer coisa, estou por aqui, pronto para ajudar também.";

            falas[3] = variantes_carta[0];
            falas[4] = variantes_carta[1];
            falas[5] = variantes_carta[2];
            falas[6] = variantes_carta[3];
        }else if(final == 1){
            roteiro[2] = " Gostaria de agradecer pela incrível oportunidade de participar do programa.";
            roteiro[3] = " Graças a ele, atualizei minhas peças enferrujadas e agora não consigo parar de me admirar no espelho.";
            roteiro[4] = " Quem diria que um robô velho como eu se tornaria algo tão brilhante? Agora sou praticamente um modelo de revista da RoboVogue!";
            roteiro[5] = " Valeu mesmo por tudo! Se precisar de qualquer coisa, estou por aqui, pronto para ajudar também.";

            falas[3] = variantes_carta[4];
            falas[4] = variantes_carta[5];
            falas[5] = variantes_carta[6];
            falas[6] = variantes_carta[7];
        }else if(final == 2){
            roteiro[2] = " Gostaria de agradecer pela oportunidade de participar do programa.";
            roteiro[3] = " Graças a ele, consegui atualizar minhas peças e até ganhei ânimo para ir à academia. ";
            roteiro[4] = " Agora, além de brilhar com minhas novas atualizações, estou malhando para garantir que minha bateria dure ainda mais, para não precisar me recarregar tanto.";
            roteiro[5] = " Valeu mesmo! Se precisar de qualquer coisa, qualquer coisinha, estou por aqui, pronto para ajudar também.";

            falas[3] = variantes_carta[8];
            falas[4] = variantes_carta[9];
            falas[5] = variantes_carta[10];
            falas[6] = variantes_carta[11];
        }else if(final == 3){
            roteiro[2] = " Só queria agradecer pela oportunidade de participar do programa. Muito obrigado por ajudar a realizar meu sonho de morar na praia.";
            roteiro[3] = " Estou adorando sentir o vento nos cabelos e na minha lataria.";
            roteiro[4] = " Quem diria que um robô vindo de um ferro-velho, cheio de parafusos enferrujados, agora estaria aproveitando a vida na praia? Nunca imaginei que meus circuitos precisariam de protetor solar.";
            roteiro[5] = " Valeu mesmo por tudo! Agora vou surfar por ai.";

            falas[3] = variantes_carta[12];
            falas[4] = variantes_carta[13];
            falas[5] = variantes_carta[14];
            falas[6] = variantes_carta[15];
        }

    }

    void CarregarTexto(){
        roteiro = new String[11];
        texto.text = "";
        num_texto = 0;
        roteiro[0] = " Agora vamos ver como nosso último participante está hoje em dia... Ele nos mandou uma foto com uma carta. Vamos apresentá-la a vocês.";
        roteiro[1] = " Ei, Apresentador!";
        Cartas();
        roteiro[6] = " Grande abraço!";
        roteiro[7] = " Uau, olha só... Ele está diferente...";
        roteiro[8] = " ...";
        roteiro[9] = " Que história linda... ela está muito melhor... Alguém deseja ter essa oportunidade ? Responda a próxima pergunta para ser nosso próximo jogador… Qual a…";

    }
    // Update is called once per frame

    void CarregaTextoNaTela(){

        velocidade += Time.deltaTime*10;
        if(velocidade > 0.6){
            texto.text += roteiro_aux[carrega];
            carrega++;
            velocidade = 0;

            if(carrega == roteiro_aux.Length){
                fim = true;

                if(num_texto >= 10){
                    comeco.gameObject.SetActive(true);
                    fadein = true;
                }
                
            }

        }
    }
    void Update()
    {
        if(fim == false){
            CarregaTextoNaTela();
        }
        
        if (fadeOut)
        {
            canvasGroup.alpha = canvasGroup.alpha - Time.deltaTime;
        }

        if (canvasGroup.alpha <= 0 && fadeOut)
        {
            fadeOut = false;
            canvasGroup.alpha = 0;

        }

        if(fadein)
        {
            canvasGroup.alpha += Time.deltaTime;
        }

        if (canvasGroup.alpha >= 1)
        {
            fadein = false;
            SceneManager.LoadScene("Menu");

        }

        teste = canvasGroup.alpha;

        if(num_texto > 1 && num_texto < 8){
            nome.text = "Robs";
        }else{
            nome.text = "Apresentador";
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            Prosseguir();
        }
    }   
}
