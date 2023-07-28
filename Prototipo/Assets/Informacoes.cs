public class Informacoes
{
    private const int INICIO = 0;
    private const int MEIO = 1;
    private const int STATUSOPCOES = 2;
    private const int FACIL = 0;
    private const int MEDIO = 1;
    private const int DIFICIL = 2;
    private static int status = INICIO;
    private static int status5050 = 0;
    private static int status_pular = 0;
    //private static int pagina_instrucoes = 1;
    //private static int pagina_creditos = 1;

    private const int MENU = 0;
    private const int JOGO = 1;
    private static int origem = 0;

    private static float valueMusicaFundo = 0.5f;
    private static float valueEfeitos = 0.5f;
    private static float valueLeituraTexto = 0.5f;

    private static int quantidade_perguntas;
    private static int quantidade_facil;
    private static int quantidade_medio;
    private static int quantidade_dificil;
    private static int nivel_atual;
    private static int pontos;
    private static string[] perguntas_facil = new string[11];
    private static string[] perguntas_medio = new string[11];
    private static string[] perguntas_dificil = new string[11];
    private static int[] respostas_facil = new int[11];
    private static int[] respostas_medio = new int[11];
    private static int[] respostas_dificil = new int[11];
    private static string[,] respostas_possiveis_facil = new string[11, 4];
    private static string[,] respostas_possiveis_medio = new string[11, 4];
    private static string[,] respostas_possiveis_dificil = new string[11, 4];
    private static string[] dicas_facil = new string[11];
    private static string[] dicas_medio = new string[11];
    private static string[] dicas_dificil = new string[11];
    private static string[] audios_perguntas = new string[33];
    private static string[,] audios_alternativas = new string[33, 4];
    private static string[] audios_dicas = new string[33];

    private static int numero_questao;

    public static void SetOrigem(int origemNova)
    {
        origem = origemNova;
    }

    public static int GetOrigem()
    {
        return origem;
    }

    public static void SetNumeroQuestao(int numero_questaoNovo)
    {
        numero_questao = numero_questaoNovo;
    }

    public static int GetNumeroQuestao()
    {
        return numero_questao;
    }

    public static void SetStatus(int statusNovo)
    {
        status = statusNovo;
    }
    public static int GetStatus()
    {
        return status;
    }

    public static void SetQuantidadePerguntas(int quantidadeNova)
    {
        quantidade_perguntas = quantidadeNova;
    }
    public static int GetQuantidadePerguntas()
    {
        return quantidade_perguntas;
    }

    public static void SetQuantidadeFacil(int quantidadeNova)
    {
        quantidade_facil = quantidadeNova;
    }
    public static int GetQuantidadeFacil()
    {
        return quantidade_facil;
    }

    public static void SetQuantidadeMedio(int quantidadeNova)
    {
        quantidade_medio = quantidadeNova;
    }
    public static int GetQuantidadeMedio()
    {
        return quantidade_medio;
    }

    public static void SetQuantidadeDificil(int quantidadeNova)
    {
        quantidade_dificil = quantidadeNova;
    }
    public static int GetQuantidadeDificil()
    {
        return quantidade_dificil;
    }

    public static void SetNivel(int nova)
    {
        nivel_atual = nova;
    }
    public static int GetNivel()
    {
        return nivel_atual;
    }

    public static void SetPontos(int pontosNovos)
    {
        pontos = pontosNovos;
    }
    public static int GetPontos()
    {
        return pontos;
    }

    public static void SetPerguntasFacil(string[] nova)
    {
        perguntas_facil = nova;
    }
    public static string[] GetPerguntasFacil()
    {
        return perguntas_facil;
    }

    public static void SetPerguntasMedio(string[] nova)
    {
        perguntas_medio = nova;
    }
    public static string[] GetPerguntasMedio()
    {
        return perguntas_medio;
    }

    public static void SetPerguntasDificil(string[] nova)
    {
        perguntas_dificil = nova;
    }
    public static string[] GetPerguntasDificil()
    {
        return perguntas_dificil;
    }

    public static void SetRespostasFacil(int[] nova)
    {
        respostas_facil = nova;
    }
    public static int[] GetRespostasFacil()
    {
        return respostas_facil;
    }

    public static void SetRespostasMedio(int[] nova)
    {
        respostas_medio = nova;
    }
    public static int[] GetRespostasMedio()
    {
        return respostas_medio;
    }

    public static void SetRespostasDificil(int[] nova)
    {
        respostas_dificil = nova;
    }
    public static int[] GetRespostasDificil()
    {
        return respostas_dificil;
    }

    public static void SetRespostasPossiveisFacil(string[,] nova)
    {
        respostas_possiveis_facil = nova;
    }
    public static string[,] GetRespostasPossiveisFacil()
    {
        return respostas_possiveis_facil;
    }

    public static void SetRespostasPossiveisMedio(string[,] nova)
    {
        respostas_possiveis_medio = nova;
    }
    public static string[,] GetRespostasPossiveisMedio()
    {
        return respostas_possiveis_medio;
    }

    public static void SetRespostasPossiveisDificil(string[,] nova)
    {
        respostas_possiveis_dificil = nova;
    }
    public static string[,] GetRespostasPossiveisDificil()
    {
        return respostas_possiveis_dificil;
    }

    public static void SetDicasFacil(string[] dicasJogo)
    {
        dicas_facil = dicasJogo;
    }
    public static string[] GetDicasFacil()
    {
        return dicas_facil;
    }

    public static void SetDicasMedio(string[] dicasJogo)
    {
        dicas_medio = dicasJogo;
    }
    public static string[] GetDicasMedio()
    {
        return dicas_medio;
    }

    public static void SetDicasDificil(string[] dicasJogo)
    {
        dicas_dificil = dicasJogo;
    }
    public static string[] GetDicasDificil()
    {
        return dicas_dificil;
    }

    public static void SetStatus5050(int status)
    {
        status5050 = status;
    }
    public static int GetStatus5050()
    {
        return status5050;
    }

    public static void SetStatusPular(int status)
    {
        status_pular = status;
    }
    public static int GetStatusPular()
    {
        return status_pular;
    }

    public static float GetValueMusicaFundo(){
        return valueMusicaFundo;
    }
    public static void SetValueMusicaFundo(float value){
        valueMusicaFundo = value;
    }

    public static float GetValueEfeitos(){
        return valueEfeitos;
    }
    public static void SetValueEfeitos(float value){
        valueEfeitos = value;
    }

    public static float GetValueLeituraTexto(){
        return valueLeituraTexto;
    }
    public static void SetValueLeituraTexto(float value){
        valueLeituraTexto = value;
    }

    public static void SetAudiosPerguntas(string[] audioPerguntas)
    {
        audios_perguntas = audioPerguntas;
    }
    public static string[] GetAudiosPerguntas()
    {
        return audios_perguntas;
    }

    public static void SetAudiosAlternativas(string[,] audioAlternativas)
    {
        audios_alternativas = audioAlternativas;
    }
    public static string[,] GetAudiosAlternativas()
    {
        return audios_alternativas;
    }

    public static void SetAudiosDicas(string[] audioDicas)
    {
        audios_dicas = audioDicas;
    }
    public static string[] GetAudiosDicas()
    {
        return audios_dicas;
    }

    /*
    public static float GetPaginaInstrucoes(){
        return pagina_instrucoes;
    }

    public static void SetPaginaInstrucoes(float value){
        pagina_instrucoes = value;
    }

    public static float GetPaginaCreditos(){
        return pagina_creditos;
    }

    public static void SetPaginaCreditos(float value){
        pagina_creditos = value;
    }*/

}