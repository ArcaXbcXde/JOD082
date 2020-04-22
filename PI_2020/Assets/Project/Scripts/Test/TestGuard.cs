using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGuard : MonoBehaviour {

    /* ____________________________________________________________________________________________
     * DESCRIÇÃO
     * ____________________________________________________________________________________________
     * O guarda faz a patrulha entre os trechos em uma rota circular, e ao chegar no ultimo trecho ele anda de volta para o primeiro trecho.*¹
     * 
     * Com o método <OnDrawGizmos> os pontos que o guarda deve ir aparecem no editor assim como suas ligações.
     * 
     * O guarda possui uma visão representada por uma luz, e sua cor indica o que ele está fazendo
     * 
     * O guarda possui os "modos":
     *      normal, onde patrulha normalmente;
     *          -sua luz estará normal;
     *          
     *      atento, quando ele "pensa" ter visto algo;
     *          -sua luz irá mudando gradativamente do normal até amarelo;
     *          
     *      alerta, quando ele viu o alvo;
     *          -sua luz ficará vermelha;
     *          
     *      perigo, quando ele já viu o alvo e já sabe que há algo por aí*²;
     *          -sua luz dependerá do modo em que se encontra;
     * 
     * O guarda ao ver o alvo a uma distância menor que <distVisao>, fica encarando a posição que ele "pensa" que viu o alvo
     *      - ou por <tempoVisto> tempo até que esse tempo passa de <tempoPerceber>, que é quando o guarda começa a perseguir o alvo
     *      - ou até que <tempoVisto> volte a 0 e o guarda volta para sua patrulha.
     * 
     * Se o alvo ficar numa distância abaixo de <distAlerta> do guarda, o guarda imediatamente fica no modo alerta
     * 
     * Quando o guarda começa a perseguir o alvo, enquanto o alvo ficar na vista do guarda, o guarda ficará em estado de alerta e
     *      <tempoPerceber> ficará travado em <tempoDesistencia>
     *      
     * Enquanto o guarda estiver alerta, e o jogador se aproximar a menos de <distAlerta>, mesmo que não esteja em
     *      seu campo de visão o guarda irá perceber o jogador e <tempoPerceber> irá renovar a duração em <tempoDesistencia> segundos.
     * 
     * Se o guarda começa a perseguir o alvo, e o alvo sair de vista, o guarda vai andar até a ultima posição que ele viu o alvo
     *      e irá esperar lá*³ até <tempoDesistencia> chegar em 0, e então o guarda irá voltar para sua patrulha como se nada tivesse acontecido*² 
     * 
     * Se o guarda se aproximar a uma distância menor que <distDano> do alvo, ele ataca causando <danoGuarda> de dano a cada <cdAtaque> segundos.
     * 
     * ____________________________________________________________________________________________
     * ALTERAÇÕES NECESSÁRIAS
     * ____________________________________________________________________________________________
     * *¹ -> Talvez seja necessário implementar novos tipos de rotas de patrulha, com a atual é possível apenas fazer
     *          ele ir numa rota circular ou reta entre dois pontos.
     * 
     * *² -> Implementar as mudanças do guarda no estado de perigo após ter ficado no estado de alerta alguma vez, mudando:
     *          1. aumentar sua velocidade de patrulha;
     *          2. aumentar campo de visão;
     *          3. reduzir o tempo até ficar atento, pois algo aconteceu;
     *          4. ficar continuamente procurando algo, mesmo enquanto patrulha.
     * 
     * *³ -> trocar a implementação de "esperar lá" para a de "procurar por lá".
     * 
     * !BUG! O guarda fica devagar enquanto persegue o jogador.
     * !BUG! O guarda acompanha com a visão acompanha através da parede enquanto está em alerta,
     *          não vendo ele, não renovando as variáveis mas ainda olhando em direção ao jogador.
     * 
     * ____________________________________________________________________________________________
     * VARIÁVEIS
     * ____________________________________________________________________________________________
     * float vel             : Velocidade do guarda
     * float velPersegue     : Velocidade de perseguição do guarda
     * float velRotacao      : Velocidade de rotação do guarda
     * float tempoEspera     : Tempo de espera que o guarda fica em cada ponto
     * float distVisao       : Distância que o guarda enxerga o alvo
     * float distAlerta      : Distância entre o guarda e o alvo que o guarda entra imediatamente em alerta
     * float distDano        : Distância para que a capsula cause dano
     * float tempoPerceber   : Tempo até o guarda perceber a presença do jogador
     * float tempoDesistencia: Tempo até o guarda desistir de perseguir o jogador
     * float cdAtaque        : Tempo que leva até o guarda poder atacar novamente
     * float danoGuarda      : Dano que o guarda causa no ataque
     * 
     * bool loopCaminho      : O caminho aparece como um loop no editor?
     * 
     * Transform meuCaminho  : Posição do objeto parente do caminho do guarda
     * 
     * Light luz             : A "lanterna" do guarda
     * 
     * LayerMask mascaraVisao: Uma máscara para dizer que camadas são obstáculos para a visão do guarda
     * 
     * float anguloVisao     : Ângulo de visão do guarda
     * float tempoVisto      : Tempo que o guarda conseguiu ver o jogador
     * float recargaAtaque   : Tempo de recarga do ataque do guarda
     * 
     * bool atencao          : O guarda está atento e procurando um alvo?
     * bool alerta           : O guarda está alerta e perseguindo o alvo?
     * bool perigo           : O guarda já viu algo e está mais atento aos seus arredores?
     * 
     * Transform alvo        : Transform do alvo do guarda
     * 
     * Color corLuzIni       : Cor inicial da luz do guarda
     * 
     */

    public static event System.Action guardaViuJogador;

    public float vel = 1.0f;
    public float velPersegue = 2.0f;
    public float velRotacao = 90.0f;
    public float tempoEspera = 0.2f;
    public float distVisao = 15.0f;
    public float distAlerta = 8.0f;
    public float distDano = 2.0f;
    public float tempoPerceber = 1.0f;
    public float tempoDesistencia = 5.0f;
    public float cdAtaque = 2.0f;
    public float danoGuarda = 20.0f;
    public bool loopCaminho = true;
    public Transform meuCaminho;
    public Light luz;
    public LayerMask mascaraVisao;
    
    private float anguloVisao;
    private float tempoVisto = 0.0f;
    private float recargaAtaque = 0.0f;
    private bool atencao = false;
    private bool alerta = false;
    private bool perigo = false;
    private Transform alvo;
    private Color corLuzIni;
    
    // Start
    private void Start() {
        
        // Diz quem é o alvo
        alvo = GameObject.FindGameObjectWithTag("Player").transform;
        
        // Atribui o ângulo de visão do guarda ao ângulo que ele ilumina
        anguloVisao = luz.spotAngle;

        // Define qual a cor inicial da luz
        corLuzIni = luz.color;

        // Vetor que lista todos os trechos no caminho do guarda
        Vector3[] trechos = new Vector3[meuCaminho.childCount];
        
        // for que passa os trechos para o vetor, mas usa apenas os eixos X e Z dos trechos
        for (int i = 0; i < trechos.Length; i++) {

            trechos[i] = meuCaminho.GetChild(i).position;
            trechos[i] = new Vector3 (trechos[i].x, transform.position.y, trechos[i].z);
        }

        // Faz o guarda começar a ronda
        StartCoroutine(SeguirCaminhoCircular(trechos));
    }

    // Update
    private void Update() {

        // Se o guarda vê o alvo, a luz muda de cor e o guarda entra em um estado de atenção ou alerta, caso contrário volta ao normal
        ControleTempoVisto();
        
        // Controle do quão atento o guarda está ao buscar o alvo
        ControleAtencao();

        // Se o ataque do guarda está em recarga, faz a contagem
        if (recargaAtaque > 0) {

            recargaAtaque -= Time.deltaTime;
        }
    }

    private void ControleTempoVisto() {

        if (alerta == false) {
            if (VerJogador() == true) {
                // Com o alvo muito próximo, o guarda percebe imediatamente
                if (Vector3.Distance(transform.position, alvo.position) < distAlerta) {

                    tempoVisto = tempoPerceber;
                } else if (tempoVisto < tempoPerceber) {

                    tempoVisto += Time.deltaTime;
                }
            } else if (tempoVisto > 0) {

                tempoVisto -= Time.deltaTime;
            }
        } else {
            if (VerJogador() == true) {

                tempoVisto = tempoDesistencia;
            } else {

                tempoVisto -= Time.deltaTime;
            }
        }
    }
    
    // Controla o quão atento o 
    private void ControleAtencao() {

        /* - Se o guarda não sabe da presença do alvo, a luz fica normal e o guarda fica sem atenção
         * - Se o guarda percebeu algo, a luz começa a mudar de cor e o guarda fica atento
         * - Se o guarda viu o alvo:
         *     - a luz fica vermelha
         *     - ele fica alerta
         *     - fica mais difícil de escapar dele futuramente
         *     - começa um evento de quando ele viu o jogador
         */
        if (tempoVisto <= 0) {

            luz.color = corLuzIni;
            atencao = false;
            alerta = false;
        } else if (tempoVisto > 0 && tempoVisto < tempoPerceber) {

            tempoVisto = Mathf.Clamp(tempoVisto, 0, tempoPerceber);
            luz.color = Color.Lerp(corLuzIni, Color.yellow, tempoVisto / tempoPerceber);
            atencao = true;
        } else if (tempoVisto >= tempoPerceber) {
            atencao = true;
            alerta = true;
            perigo = true;
            luz.color = Color.red;
            guardaViuJogador?.Invoke();
        }
    }

    // Busca o jogador
    private bool VerJogador() {

        /* Verifica se:
         * 
         * 1- o alvo está dentro da distância de visão;
         * 2- o alvo está dentro do ângulo de visão;
         * 3- há nenhum obstáculo entre o alvo e o guarda;
         * 
         * 
         * Se sim, retorna verdadeiro, se não, retorna falso.
         */
        if ((Vector3.Distance (transform.position, alvo.position) < distVisao)
            && (Vector3.Angle(transform.forward, (alvo.position - transform.position).normalized) < anguloVisao / 2.0f)
            && (Physics.Linecast(transform.position, alvo.position, mascaraVisao) == false)) {

            return true;
        } else {
            
            return false;
        }
    }

    private bool JogadorVisivel() {

        // Verifica se o jogador está visível
        if ((Physics.Linecast(transform.position, alvo.position, mascaraVisao) == false)) {

            return true;
        } else {

            return false;
        }
    }

    // Método que define que o guarda fará uma ronda circular
    private IEnumerator SeguirCaminhoCircular(Vector3[] trechos) {

        // Coloca o guarda no primeiro trecho
        transform.position = trechos[0];

        /* int indexTrechoAlvo   : Declaração de qual trecho o guarda está indo
         * Vector3 trechoAlvo    : Declaração do vetor que diz a posição do trecho que o guarda deve ir agora
         * Vector3 ultimoAvistado: Declaração da posição da ultima vez que o guarda viu o jogador
         */
        int indexTrechoAlvo = 1;
        Vector3 trechoAlvo = trechos[indexTrechoAlvo];
        Vector3 ultimoAvistado = alvo.position;

        while (true) {
            // se não está alerta, segue caminho normal, caso contrário segue o alvo
            if (atencao == false) {
                // Faz o movimento
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(trechoAlvo.x, transform.position.y, trechoAlvo.z), vel * Time.deltaTime);
                transform.LookAt(new Vector3(trechoAlvo.x, transform.position.y, trechoAlvo.z));

                // Se o guarda chegou na posição X e Z do trecho que ele estava indo
                if (transform.position.x == trechoAlvo.x && transform.position.z == trechoAlvo.z) {

                    // Define que agora o trecho alvo é o próximo
                    indexTrechoAlvo = (indexTrechoAlvo + 1) % trechos.Length;
                    trechoAlvo = trechos[indexTrechoAlvo];
                    // Retorna um tempo de espera e uma rotação antes que ele possa seguir para o próximo trecho
                    yield return new WaitForSeconds(tempoEspera);
                    yield return StartCoroutine(VirarParaCaminho(trechoAlvo));

                }
            } else if (atencao == true) {
                
                // Diz que a nova posição que o guarda deve ir é a ultima posição que o alvo foi avistado, ou se o alvo estiver muito próximo
                if (VerJogador() == true || (Vector3.Distance(transform.position, alvo.position) < distAlerta && (Physics.Linecast(transform.position, alvo.position, mascaraVisao) == false))) {

                    ultimoAvistado = alvo.position;
                    if ((Physics.Linecast(transform.position, alvo.position, mascaraVisao) == false)) {

                    yield return StartCoroutine(VirarParaCaminho(ultimoAvistado));
                    }
                }
                // Movimento se estiver fora do alcance do ataque, com uma folga
                if (alerta == true && Vector3.Distance(transform.position, alvo.position) > distDano * 0.9) {

                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(ultimoAvistado.x, transform.position.y, ultimoAvistado.z), velPersegue * Time.deltaTime);
                
                // Se estiver no alcance do ataque, ataca
                } else if (Vector3.Distance(transform.position, alvo.position) < distDano && recargaAtaque <= 0) {
                    
                    // Deixa o ataque em recarga
                    recargaAtaque = cdAtaque;

                    // Reduz o hp do jogador (ficou meio mal implementado mas funciona legal)
                    alvo.GetComponent<PlayerResources>().hp -= danoGuarda;

                }

            }

            // Evita reclamações do programa por que o método sempre precisa de algum retorno
            yield return null;
        }
    }

    // Método que define que ele irá olhar para a direção que anda
    private IEnumerator VirarParaCaminho (Vector3 girar) {

        //Direção para a posição do olhar do guarda
        Vector3 direcaoParaOlhar = (girar - transform.position).normalized;
        float anguloAlvo = 90 - Mathf.Atan2(direcaoParaOlhar.z, direcaoParaOlhar.x) * Mathf.Rad2Deg;

        // while para ir girando o guarda frame a frame até alcançar o ângulo desejado com uma pequena brecha para imprecisão do programa
        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, anguloAlvo)) > 0.1f) {

            // Faz o guarda girar
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, anguloAlvo, velRotacao * Time.deltaTime);
            yield return null;
        }

    }
    
    // Método que desenha os gizmos para visualizar o caminho do guarda no editor
    private void OnDrawGizmos() {

        /* Vector3 posInicial : Posição onde os trechos começam
         * Vector3 posAnterior: Posição anterior à atual do foreach
         */
        Vector3 posInicial = meuCaminho.GetChild(0).position;
        Vector3 posAnterior = posInicial;

        // Foreach para percorrer cada trecho no caminho do guarda
        foreach (Transform trecho in meuCaminho) {

            // Desenha as esferas de cada ponto que marca os trechos
            Gizmos.DrawSphere(trecho.position, 0.5f);
            // Desenha linhas entre as esferas de cada ponto que marca os trechos
            Gizmos.DrawLine(posAnterior, trecho.position);

            // Define que a posição atual agora será a posição anterior
            posAnterior = trecho.position;

        }

        if (loopCaminho == true) {
            // Desenha uma linha entre o primeiro trecho e o último
            Gizmos.DrawLine(posAnterior, posInicial);
        }

        // Desenha um raio para frente que indica a distância de visão do guarda
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * distVisao);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * distAlerta);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * distDano);

        //Desenha um raio entre o alvo e o garda, se visível
        if ((Physics.Linecast(transform.position, alvo.position, mascaraVisao) == false)) {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, alvo.position);
        }
    }

}
