Console.WriteLine("Jogo Batalha Naval !!");

Console.WriteLine("Digite a quantidade de Jogadores");

int playerAmmount = Convert.ToInt32(Console.ReadLine());

string playerName01;
string playerName02;

List<string> player1Ships = new List<string>();
List<string> player2Ships = new List<string>();



// Lendo a quantidade de Jogadores
while (playerAmmount > 2 || playerAmmount < 1)
{
    Console.Clear();
    Console.WriteLine("Numero de jogadores inválido, digite novamente:");
    playerAmmount = Convert.ToInt32(Console.ReadLine());
}

// Lendo o nome do player caso esteja jogand sozinho
if (playerAmmount == 1)
{
    Console.WriteLine("Digite o nome do Player");
    do
    {
        playerName01 = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(playerName01));

    playerName02 = "Computer";
}

// Lendo o nome dos players caso sejam 2
else
{
    Console.WriteLine("Digite o nome do Player 01");
    do
    {
        playerName01 = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(playerName01));
    
    Console.WriteLine("Digite o nome do Player 02");

    do
    {
        playerName02 = Console.ReadLine();
    } while (!string.IsNullOrWhiteSpace(playerName02));

}

// Recebendo o Board do player 01:
AddPlayer(playerName01, player1Ships);

// Recebendo o Board do player 02, se houver o segundo player:
if (playerAmmount == 2)
    AddPlayer(playerName02, player2Ships);

// Criando o board do player Computador, se nao houver segundo player
if (playerAmmount == 1)
    AddComputerShip(player2Ships);

// Jogando single player
if (playerAmmount == 1)
    GameSinglePlayer(playerName01, playerName02, player1Ships, player2Ships);

// Jogando multiplayer
else if (playerAmmount == 2)
    GameMultiPlayer(playerName01, playerName02, player1Ships, player2Ships);



// Métodos utilizados para a execução do programa 

// Adiciona navio na posição especificada e salva as posições que o navio ocupa
static void AddShip(int[] position, List<string> playerShips)
{

    int aux;
    if (position[0] > position[2])
    {
        aux = position[0];
        position[0] = position[2];
        position[2] = aux;
    }

    if (position[1] > position[3])
    {
        aux = position[1];
        position[1] = position[3];
        position[3] = aux;
    }

    for (int i = position[0]; i <= position[2]; i++)
    {
        for (int j = position[1]; j <= position[3]; j++)
        {
            playerShips.Add($"{i}{j}");
        }
    }
}

// Checa para saber se o navio a ser adicionado é valido
static int ValidShip(int[] position, int size)
{
    int x1 = position[0];
    int x2 = position[2];
    int y1 = position[1];
    int y2 = position[3];

    if (x1 != x2 && y1 != y2)
        return 0;

    if (Math.Abs(x1 - x2) + 1 == size)
        return 1;
    if (Math.Abs(y1 - y2) + 1 == size)
        return 1;

    return 0;
}

// Retorna o tamanho do navio de acordo com o codigo informado
static int ShipSize(string code)
{
    if (code == "PS")
        return 5;
    if (code == "NT")
        return 4;
    if (code == "DS")
        return 3;
    if (code == "SB")
        return 2;
    return 0;
}

// Converte a entrada da posição para coordenada de matriz
static int[] ConvertPosition(string position)
{
    string aux;

    if (position.Contains("10"))
        aux = position.Replace("10", ":");
    else
        aux = position;

    int[] exit = new int[aux.Length];

    for (int i = 0; i < aux.Length; i++)
    {
        switch (aux[i])
        {
            case ('A'):
                exit[i] = 0;
                break;
            case ('B'):
                exit[i] = 1;
                break;
            case ('C'):
                exit[i] = 2;
                break;
            case ('D'):
                exit[i] = 3;
                break;
            case ('E'):
                exit[i] = 4;
                break;
            case ('F'):
                exit[i] = 5;
                break;
            case ('G'):
                exit[i] = 6;
                break;
            case ('H'):
                exit[i] = 7;
                break;
            case ('I'):
                exit[i] = 8;
                break;
            case ('J'):
                exit[i] = 9;
                break;

            default:
                exit[i] += Convert.ToInt32(aux[i] - 49);
                break;
        }
    }
    return exit;
}

// Imprime o board do player que vai receber o ataque
static void PrintBoard(string[,] board)
{
    for (int i = 0; i < 10; i++)
    {
        for (int j = 0; j < 10; j++)
        {
            switch (board[i, j])
            {
                case null:
                    Console.Write("|   |");
                    break;
                case "1":
                    Console.Write("| X |");
                    break;
                case "0":
                    Console.Write("| A |");
                    break;

                default:
                    break;
            }

        }
        Console.WriteLine();
    }
}

// Executa o ataque
static int Attack(string attack, List<string> attacksDone, string[,] board, List<string> ships)
{
    string aux;
    string position = "";

    if (attack.Contains("10"))
        aux = attack.Replace("10", ":");
    else
        aux = attack;

    int[] positionInt = new int[aux.Length];

    positionInt = ConvertPosition(attack);

    foreach (var letter in aux)
    {
        if (letter > 112)
            return 0;

        switch (letter)
        {
            case ('A'):
                position += '0';
                break;
            case ('B'):
                position += '1';
                break;
            case ('C'):
                position += '2';
                break;
            case ('D'):
                position += '3';
                break;
            case ('E'):
                position += '4';
                break;
            case ('F'):
                position += '5';
                break;
            case ('G'):
                position += '6';
                break;
            case ('H'):
                position += '7';
                break;
            case ('I'):
                position += '8';
                break;
            case ('J'):
                position += '9';
                break;

            default:
                position += Convert.ToChar(letter - 1);
                break;
        }
    }

    if (attacksDone.Contains(position) || ValidAttack(position) == 0)
        return 0;

    string attackResult = "0";
    attacksDone.Add(position);
    
    if (ships.Contains(position))
    {
        attackResult = "1";
        ships.Remove(position);
    }

    board[positionInt[0], positionInt[1]] = attackResult;
    return 1;

}

// Valida se o ataque é válido
static int ValidAttack(string attack)
{
    string aux;

    if (attack.Contains("10"))
        aux = attack.Replace("10", ":");
    else
        aux = attack;

    int[] positionInt = new int[aux.Length];

    for (int i = 0; i < positionInt.Length; i++)
    {
        if (positionInt[i] < 0 || positionInt[i] > 9)
            return 0;
    }
    return 1;
}

// Checa se a entrada do Computador é válida
static int ValidComputerShip(List<string> playerShips, int size)
{
    var rand = new Random();
    List<string> checkShips = new List<string>();

    int[] pos = new int[4];
    int orientation = rand.Next() % 2;

    pos[0] = rand.Next() % 10;
    pos[1] = rand.Next() % 10;

    if (orientation == 0)
    {
        pos[2] = pos[0];
        pos[3] = pos[1] + size - 1;
        if (pos[3] > 9)
            return 0;
    }

    else if (orientation == 1)
    {
        pos[2] = pos[0] + size - 1;
        pos[3] = pos[1];

        if (pos[2] > 9)
            return 0;
    }

    if (ValidShip(pos, size) == 1)
    {
        checkShips.Clear();
        AddShip(pos, checkShips);

        if (checkShips.Intersect(playerShips).Any())
        {
            return 0;
        }
        else
        {
            AddShip(pos, playerShips);
            return 1;
        }
    }
    else
        return 0;

}

// Executa as jogadas de forma randomica para o Computador, quando jogando apenas um player
static int ComputerAttack(List<string> attacksDone, string[,] board, List<string> ships)
{
    var rand = new Random();
    string attack = "";

    int[] position = new int[2];

    position[0] = Math.Abs(rand.Next() % 10) + 1;
    position[1] = Math.Abs(rand.Next() % 10) + 1;

    for (int i = 0; i < 2; i++)
    {
        attack += Convert.ToString(position[i]);
    }

    return Attack(attack, attacksDone, board, ships);
}

// Adiciona os navios do player
static void AddPlayer(string playerName, List<string> playerShips)
{
    string code;
    string position;
    int[] positionDecoded = new int[4];
    List<string> checkShips = new List<string>();

    int countPS = 1;
    int countNT = 1;
    int countDS = 1;
    int countSB = 1;

    // Adiciona os navios enquanto houver navios a serem adicionados
    while (countDS > 0 || countSB > 0 || countNT > 0 || countPS > 0)
    {
        Console.Clear();
        Console.WriteLine($"Player {playerName}, digite o codigo do navio que quer add: ");
        Console.WriteLine($"{countPS} - Porta Aviões restantes - Código PS");
        Console.WriteLine($"{countNT} - Navio Tanque restantes - Código NT");
        Console.WriteLine($"{countDS} - Destroyers restantes - Código DS");
        Console.WriteLine($"{countSB} - Submarinos restantes - Código SB");

        // Recebendo o código do navio que o player deseja inserir
        do
        {
            code = Console.ReadLine().ToUpper().Trim();

        } while (!string.IsNullOrWhiteSpace(code) && (ShipSize(code) == 0));

        // Das linas 78 a 108, testando se há navios disponiveis do tipo indicado acima
        if (code.Contains("PS") && countPS == 0)
        {
            Console.WriteLine("Não há mais navios desse tipo disponiveis, tente novamente. ");
            Console.WriteLine("Pressione qualquer tecla para continuar:");
            Console.ReadKey();
            continue;
        }

        if (code.Contains("NT") && countNT == 0)
        {
            Console.WriteLine("Não há mais navios desse tipo disponiveis, tente novamente. ");
            Console.WriteLine("Pressione qualquer tecla para continuar:");
            Console.ReadKey();
            continue;
        }

        if (code.Contains("DS") && countDS == 0)
        {
            Console.WriteLine("Não há mais navios desse tipo disponiveis, tente novamente. ");
            Console.WriteLine("Pressione qualquer tecla para continuar:");
            Console.ReadKey();
            continue;
        }

        if (code.Contains("SB") && countSB == 0)
        {
            Console.WriteLine("Não há mais navios desse tipo disponiveis, tente novamente. ");
            Console.WriteLine("Pressione qualquer tecla para continuar:");
            Console.ReadKey();
            continue;
        }

        Console.WriteLine($"Player {playerName}, digite a posição onde quer add o navio: ");

        // Recebendo as coordenadas de onde deseja inserir o navio
        do
        {
            position = Console.ReadLine().ToUpper().Trim();

        } while (string.IsNullOrWhiteSpace(position));

        // Convertendo a posição indicada de string para um array de int
        positionDecoded = ConvertPosition(position);

        // Validando se a posição onde se deseja inserir o navio é válida
        if (ValidShip(positionDecoded, ShipSize(code)) == 1)
        {
            checkShips.Clear();
            AddShip(positionDecoded, checkShips);

            if (checkShips.Intersect(playerShips).Any())
            {
                Console.WriteLine("Posição para adicionar navio inválida, tente novamente");
                Console.WriteLine("Pressione qualquer tecla para continuar:");
                Console.ReadKey();
                continue;
            }

            else
            {
                AddShip(positionDecoded, playerShips);
                Console.WriteLine($"Navio do tipo {code} adicionado com sucesso");
            }
        }

        else
        {
            Console.WriteLine("Tamanho do navio invalido");
            Console.WriteLine("Pressione qualquer tecla para continuar:");
            Console.ReadKey();
            continue;
        }

        // Decrescendo o contador das quantidades de navios a serem inseridos
        if (code.Contains("PS") && countPS > 0)
            countPS--;
        if (code.Contains("NT") && countNT > 0)
            countNT--;
        if (code.Contains("DS") && countDS > 0)
            countDS--;
        if (code.Contains("SB") && countSB > 0)
            countSB--;

        Console.WriteLine("Pressione qualquer tecla para continuar:");
        Console.ReadKey();

    }


}

// Adiciona os navios do Computador
static void AddComputerShip(List<string> playerShips)
{
    int countAirCarrier = 1;
    int countTanker = 2;
    int countDestroyer = 3;
    int countSubmarine = 4;
    int validation;
    int size;

    while (countAirCarrier > 0)
    {
        size = 5;
        validation = 0;
        while (validation == 0)
        {
            validation = ValidComputerShip(playerShips, size);
        }
        countAirCarrier--;
    }

    while (countTanker > 0)
    {
        size = 4;
        validation = 0;
        while (validation == 0)
        {
            validation = ValidComputerShip(playerShips, size);
        }
        countTanker--;
    }
    while (countDestroyer > 0)
    {
        size = 3;
        validation = 0;
        while (validation == 0)
        {
            validation = ValidComputerShip(playerShips, size);
        }
        countDestroyer--;
    }
    while (countSubmarine > 0)
    {
        size = 2;
        validation = 0;
        while (validation == 0)
        {
            validation = ValidComputerShip(playerShips, size);
        }
        countSubmarine--;
    }
}

// Executa o jogo caso for Single Player
static void GameSinglePlayer(string playerName01, string playerName02, List<string> player1Ships, List<string> player2Ships)
{
    string[,] player1Board = new string[10, 10];
    string[,] player2Board = new string[10, 10];

    List<string> player1Attacks = new List<string>();
    List<string> player2Attacks = new List<string>();

    bool player1Turn = true;
    bool player2Turn = true; 

    string winner = "";
    string attack;

    while (player1Ships.Count() != 0 && player2Ships.Count() != 0)
    {
        Console.Clear();
        while (player1Turn)
        {
            Console.WriteLine($"{playerName01} faça sua jogada");
            PrintBoard(player2Board);

            // Recebendo a posição que o player deseja atacar
            do
            {
                attack = Console.ReadLine().Trim().ToUpper();
            } while (!string.IsNullOrWhiteSpace(attack) && ValidAttack(attack) == 0);

            // Validando a jogada do player
            if (Attack(attack, player1Attacks, player2Board, player2Ships) == 0)
            {
                Console.WriteLine("A jogada inserida não foi válida, tente novamente");
                Console.WriteLine("Pressione qualquer tecla para continuar:");
                Console.ReadKey();
                Console.Clear();
                continue;
            }
            else
            {
                Attack(attack, player1Attacks, player2Board, player2Ships);
                Console.WriteLine("A jogada foi válida!!");
                PrintBoard(player2Board);
                Console.WriteLine("Pressione qualquer tecla para continuar:");
                Console.ReadKey();
                Console.Clear();
                player1Turn = false;
                player2Turn = true;
            }
        }

        // Confere se o jogador 1 ganhou a partida 
        if (player2Ships.Count() == 0)
        {
            winner = playerName01;
            continue;
        }
        while (player2Turn)
        {

            int validation = 0;
            PrintBoard(player1Board);

            // Recebendo os valores que o player Computador deseja atacar de forma automática
            while (validation == 0)
            {
                validation = ComputerAttack(player2Attacks, player1Board, player1Ships);
            }
            Console.WriteLine($"O {playerName02} fez a jogada");

            PrintBoard(player1Board);
            Console.WriteLine("Pressione qualquer tecla para continuar:");
            Console.ReadKey();
            Console.Clear();

            player2Turn = false;
            player1Turn = true;

        }
        if (player1Ships.Count() == 0)
            winner = playerName02;
    }

    // Imprime o resultado do jogo
    Console.WriteLine($"PARABÉNS {winner}, VOCÊ É O GRANDE VENCEDOR!!!");
    Console.WriteLine("Pressione qualquer tecla para sair...");
    Console.ReadKey();
}

// Executa o jogo caso seja multi player
static void GameMultiPlayer(string playerName01, string playerName02, List<string> player1Ships, List<string> player2Ships)
{
    string[,] player1Board = new string[10, 10];
    string[,] player2Board = new string[10, 10];

    List<string> player1Attacks = new List<string>();
    List<string> player2Attacks = new List<string>();

    bool player1Turn = true;
    bool player2Turn = true;

    string winner = "";
    string attack;

    while (player1Ships.Count() != 0 && player2Ships.Count() != 0)
    {
        Console.Clear();
        while (player1Turn)
        {
            Console.WriteLine($"{playerName01} faça sua jogada");
            PrintBoard(player2Board);

            // Recebendo a posição que o player deseja atacar
            do
            {
                attack = Console.ReadLine().Trim().ToUpper();
            } while (!string.IsNullOrWhiteSpace(attack) && ValidAttack(attack) == 0);

            // Validando a jogada do player
            if (Attack(attack, player1Attacks, player2Board, player2Ships) == 0)
            {
                Console.WriteLine("A jogada inserida não foi válida, tente novamente");
                Console.WriteLine("Pressione qualquer tecla para continuar:");
                Console.ReadKey();
                Console.Clear();
                continue;
            }
            else
            {
                Attack(attack, player1Attacks, player2Board, player2Ships);
                Console.WriteLine("A jogada foi válida!!");
                PrintBoard(player2Board);
                Console.WriteLine("Pressione qualquer tecla para continuar:");
                Console.ReadKey();
                Console.Clear();
                player1Turn = false;
                player2Turn = true;
            }
        }

        // Confere se o jogador 1 ganhou a partida
        if (player2Ships.Count() == 0)
        {
            winner = playerName01;
            continue;
        }


        while (player2Turn)
        {
            Console.WriteLine($"{playerName02} faça sua jogada");
            PrintBoard(player1Board);

            // Recebendo a posição que o player deseja atacar
            do
            {
                attack = Console.ReadLine().Trim().ToUpper();
            } while (!string.IsNullOrWhiteSpace(attack) && ValidAttack(attack) == 0);

            // Validando a jogada do player
            if (Attack(attack, player2Attacks, player1Board, player1Ships) == 0)
            {
                Console.WriteLine("A jogada inserida não foi válida, tente novamente");
                Console.WriteLine("Pressione qualquer tecla para continuar:");
                Console.ReadKey();
                Console.Clear();
                continue;
            }
            else
            {
                Attack(attack, player2Attacks, player1Board, player1Ships);
                Console.WriteLine("A jogada foi válida!!");
                PrintBoard(player1Board);
                Console.WriteLine("Pressione qualquer tecla para continuar:");
                Console.ReadKey();
                Console.Clear();
                player2Turn = false;
                player1Turn = true;
            }
        }
        if (player1Ships.Count() == 0)
            winner = playerName02;

    }

    Console.WriteLine($"PARABÉNS {winner}, VOCÊ É O GRANDE VENCEDOR!!!");
    Console.WriteLine("Pressione qualquer tecla para sair...");
    Console.ReadKey();
}