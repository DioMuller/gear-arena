gear-arena
==========

Gear Arena

ENUNCIADO:

    Faça um jogo simples, estilo worms / scorched earth:
    1. Dois canhões, um em cada lado da tela;
    2. Na hora do tiro, permita que o jogador escolha:
    2.1. Inclinação do canhão: Que dá o ângulo da bala;
    2.2. Força: Enquanto pressionar tecla/mouse a força variará de 0 até o máximo do canhão. O jogador não sabe que máximo é esse;
    2.3 Que bala usar (ver abaixo);
    3. Atuarão sobre a bala a força da propulsão do canhão e do vento. O vento terá uma chance de variar a cada tiro;
    4. O jogador poderá escolher bala de massa 1, 2 ou 3. O dano será proporcional a bala:
    4.1. Bala de massa 3 tira 20. O jogador terá 3 balas desse tipo.
    4.2. Bala de massa 2 tira 10. O jogador terá 5 balas desse tipo.
    4.3. Bala de massa 1 tira 5. Infinitas balas desse tipo.
    5. Cada jogador terá 100 de vida.

    Critérios de avaliação (cada critério vale 2 pontos):
    a) Implementação vetorial da função de gravidade
    b) Faz uso de vetores na forma de offsets, para a bala sair da ponta do canhão;
    c) Implementou corretamente o cálculo de deslocamento da bala, levando em conta a inércia e fazendo uso de vetores;
    d) Cumpriu todos os requisitos funcionais do trabalho;
    e) Incluiu acabamento: Sons, boas imagens, menu, controles amigáveis, etc.