using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class avaliacao : MonoBehaviour
{

    public Button estrela_comunicativo_1;
    public Button estrela_comunicativo_2;
    public Button estrela_comunicativo_3;
    public Button estrela_engajado_1;
    public Button estrela_engajado_2;
    public Button estrela_engajado_3;
    public Button estrela_gentil_1;
    public Button estrela_gentil_2;
    public Button estrela_gentil_3;

    int estrela_comunicativo = 0;
    int estrela_engajado = 0;
    int estrela_gentil = 0;

    public Sprite estrela_clicada;
    public Sprite estrela_n_clicada;


    public void click_estrela_comunicativo_1()
    {
        if (estrela_comunicativo != 1) {
            estrela_comunicativo = 1;
            Image image_comunicativo_1 = estrela_comunicativo_1.GetComponent<Image>();
            image_comunicativo_1.sprite = estrela_clicada;

            Image image_comunicativo_2 = estrela_comunicativo_2.GetComponent<Image>();
            image_comunicativo_2.sprite = estrela_n_clicada;

            Image image_comunicativo_3 = estrela_comunicativo_3.GetComponent<Image>();
            image_comunicativo_3.sprite = estrela_n_clicada;
        }
        else {
            estrela_comunicativo = 0;
            Image image_comunicativo_1 = estrela_comunicativo_1.GetComponent<Image>();
            image_comunicativo_1.sprite = estrela_n_clicada;

            Image image_comunicativo_2 = estrela_comunicativo_2.GetComponent<Image>();
            image_comunicativo_2.sprite = estrela_n_clicada;

            Image image_comunicativo_3 = estrela_comunicativo_3.GetComponent<Image>();
            image_comunicativo_3.sprite = estrela_n_clicada;
        }

    }

    public void click_estrela_comunicativo_2()
    {
        if (estrela_comunicativo != 2) {
            estrela_comunicativo = 2;
            Image image_comunicativo_1 = estrela_comunicativo_1.GetComponent<Image>();
            image_comunicativo_1.sprite = estrela_clicada;

            Image image_comunicativo_2 = estrela_comunicativo_2.GetComponent<Image>();
            image_comunicativo_2.sprite = estrela_clicada;

            Image image_comunicativo_3 = estrela_comunicativo_3.GetComponent<Image>();
            image_comunicativo_3.sprite = estrela_n_clicada;
        }
        else {
            estrela_comunicativo = 0;
            Image image_comunicativo_1 = estrela_comunicativo_1.GetComponent<Image>();
            image_comunicativo_1.sprite = estrela_clicada;

            Image image_comunicativo_2 = estrela_comunicativo_2.GetComponent<Image>();
            image_comunicativo_2.sprite = estrela_n_clicada;

            Image image_comunicativo_3 = estrela_comunicativo_3.GetComponent<Image>();
            image_comunicativo_3.sprite = estrela_n_clicada;
        }

    }

    public void click_estrela_comunicativo_3()
    {
        if (estrela_comunicativo != 3) {
            estrela_comunicativo = 3;
            Image image_comunicativo_1 = estrela_comunicativo_1.GetComponent<Image>();
            image_comunicativo_1.sprite = estrela_clicada;

            Image image_comunicativo_2 = estrela_comunicativo_2.GetComponent<Image>();
            image_comunicativo_2.sprite = estrela_clicada;

            Image image_comunicativo_3 = estrela_comunicativo_3.GetComponent<Image>();
            image_comunicativo_3.sprite = estrela_clicada;
        }
        else {
            estrela_comunicativo = 0;
            Image image_comunicativo_1 = estrela_comunicativo_1.GetComponent<Image>();
            image_comunicativo_1.sprite = estrela_clicada;

            Image image_comunicativo_2 = estrela_comunicativo_2.GetComponent<Image>();
            image_comunicativo_2.sprite = estrela_clicada;

            Image image_comunicativo_3 = estrela_comunicativo_3.GetComponent<Image>();
            image_comunicativo_3.sprite = estrela_n_clicada;
        }

    }

    public void click_estrela_engajado_1()
    {
        if (estrela_engajado != 1) {
            estrela_engajado = 1;
            Image image_engajado_1 = estrela_engajado_1.GetComponent<Image>();
            image_engajado_1.sprite = estrela_clicada;

            Image image_engajado_2 = estrela_engajado_2.GetComponent<Image>();
            image_engajado_2.sprite = estrela_n_clicada;

            Image image_engajado_3 = estrela_engajado_3.GetComponent<Image>();
            image_engajado_3.sprite = estrela_n_clicada;
        }
        else {
            estrela_engajado = 0;
            Image image_engajado_1 = estrela_engajado_1.GetComponent<Image>();
            image_engajado_1.sprite = estrela_n_clicada;

            Image image_engajado_2 = estrela_engajado_2.GetComponent<Image>();
            image_engajado_2.sprite = estrela_n_clicada;

            Image image_engajado_3 = estrela_engajado_3.GetComponent<Image>();
            image_engajado_3.sprite = estrela_n_clicada;
        }
    }

    public void click_estrela_engajado_2()
    {
        if (estrela_engajado != 2) {
            estrela_engajado = 2;
            Image image_engajado_1 = estrela_engajado_1.GetComponent<Image>();
            image_engajado_1.sprite = estrela_clicada;

            Image image_engajado_2 = estrela_engajado_2.GetComponent<Image>();
            image_engajado_2.sprite = estrela_clicada;

            Image image_engajado_3 = estrela_engajado_3.GetComponent<Image>();
            image_engajado_3.sprite = estrela_n_clicada;
        }
        else {
            estrela_engajado = 0;
            Image image_engajado_1 = estrela_engajado_1.GetComponent<Image>();
            image_engajado_1.sprite = estrela_clicada;

            Image image_engajado_2 = estrela_engajado_2.GetComponent<Image>();
            image_engajado_2.sprite = estrela_n_clicada;

            Image image_engajado_3 = estrela_engajado_3.GetComponent<Image>();
            image_engajado_3.sprite = estrela_n_clicada;
        }

    }

    public void click_estrela_engajado_3()
    {
        if (estrela_engajado != 3) {
            estrela_engajado = 3;
            Image image_engajado_1 = estrela_engajado_1.GetComponent<Image>();
            image_engajado_1.sprite = estrela_clicada;

            Image image_engajado_2 = estrela_engajado_2.GetComponent<Image>();
            image_engajado_2.sprite = estrela_clicada;

            Image image_engajado_3 = estrela_engajado_3.GetComponent<Image>();
            image_engajado_3.sprite = estrela_clicada;
        }
        else {
            estrela_engajado = 0;
            Image image_engajado_1 = estrela_engajado_1.GetComponent<Image>();
            image_engajado_1.sprite = estrela_clicada;

            Image image_engajado_2 = estrela_engajado_2.GetComponent<Image>();
            image_engajado_2.sprite = estrela_clicada;

            Image image_engajado_3 = estrela_engajado_3.GetComponent<Image>();
            image_engajado_3.sprite = estrela_n_clicada;
        }

    }

    public void click_estrela_gentil_1()
    {
        if (estrela_gentil != 1) {
            estrela_gentil = 1;
            Image image_gentil_1 = estrela_gentil_1.GetComponent<Image>();
            image_gentil_1.sprite = estrela_clicada;

            Image image_gentil_2 = estrela_gentil_2.GetComponent<Image>();
            image_gentil_2.sprite = estrela_n_clicada;

            Image image_gentil_3 = estrela_gentil_3.GetComponent<Image>();
            image_gentil_3.sprite = estrela_n_clicada;
        }
        else {
            estrela_gentil = 0;
            Image image_gentil_1 = estrela_gentil_1.GetComponent<Image>();
            image_gentil_1.sprite = estrela_n_clicada;

            Image image_gentil_2 = estrela_gentil_2.GetComponent<Image>();
            image_gentil_2.sprite = estrela_n_clicada;

            Image image_gentil_3 = estrela_gentil_3.GetComponent<Image>();
            image_gentil_3.sprite = estrela_n_clicada;
        }

    }

    public void click_estrela_gentil_2()
    {
        if (estrela_gentil != 2) {
            estrela_gentil = 2;
            Image image_gentil_1 = estrela_gentil_1.GetComponent<Image>();
            image_gentil_1.sprite = estrela_clicada;

            Image image_gentil_2 = estrela_gentil_2.GetComponent<Image>();
            image_gentil_2.sprite = estrela_clicada;

            Image image_gentil_3 = estrela_gentil_3.GetComponent<Image>();
            image_gentil_3.sprite = estrela_n_clicada;
        }
        else {
            estrela_gentil = 0;
            Image image_gentil_1 = estrela_gentil_1.GetComponent<Image>();
            image_gentil_1.sprite = estrela_clicada;

            Image image_gentil_2 = estrela_gentil_2.GetComponent<Image>();
            image_gentil_2.sprite = estrela_n_clicada;

            Image image_gentil_3 = estrela_gentil_3.GetComponent<Image>();
            image_gentil_3.sprite = estrela_n_clicada;
        }

    }

    public void click_estrela_gentil_3()
    {
        if (estrela_gentil != 3) {
            estrela_gentil = 3;
            Image image_gentil_1 = estrela_gentil_1.GetComponent<Image>();
            image_gentil_1.sprite = estrela_clicada;

            Image image_gentil_2 = estrela_gentil_2.GetComponent<Image>();
            image_gentil_2.sprite = estrela_clicada;

            Image image_gentil_3 = estrela_gentil_3.GetComponent<Image>();
            image_gentil_3.sprite = estrela_clicada;
        }
        else {
            estrela_gentil = 0;
            Image image_gentil_1 = estrela_gentil_1.GetComponent<Image>();
            image_gentil_1.sprite = estrela_clicada;

            Image image_gentil_2 = estrela_gentil_2.GetComponent<Image>();
            image_gentil_2.sprite = estrela_clicada;

            Image image_gentil_3 = estrela_gentil_3.GetComponent<Image>();
            image_gentil_3.sprite = estrela_n_clicada;
        }

    }

    // Start is called before the first frame update
    void Start()
    {        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
