using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

class Program
{
    static void Main()
    {
        Console.WriteLine("Escolha um desafio:");
        Console.WriteLine("1 - Soma do índice");
        Console.WriteLine("2 - Verificar número na sequência de Fibonacci");
        Console.WriteLine("3 - Faturamento diário de uma distribuidora");
        Console.WriteLine("4 - Percentual de faturamento por estado");
        Console.WriteLine("5 - Inverter string");
        Console.Write("Digite sua escolha: ");

        int escolha = int.Parse(Console.ReadLine());

        switch (escolha)
        {
            case 1:
                SomaIndice();
                break;
            case 2:
                VerificarFibonacci();
                break;
            case 3:
                FaturamentoDistribuidora();
                break;
            case 4:
                PercentualFaturamento();
                break;
            case 5:
                InverterString();
                break;
            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
    }

    // Desafio 1
    static void SomaIndice()
    {
        int INDICE = 13, SOMA = 0, K = 0;

        while (K < INDICE)
        {
            K = K + 1;
            SOMA = SOMA + K;
        }

        Console.WriteLine($"O valor de SOMA é: {SOMA}");
    }


    // Desafio 2
    static void VerificarFibonacci()
    {
        Console.Write("Digite um número: ");
        int numero = int.Parse(Console.ReadLine());

        int a = 0, b = 1;

        while (b < numero)
        {
            int temp = b;
            b = a + b;
            a = temp;
        }

        if (b == numero || numero == 0)
        {
            Console.WriteLine($"O número {numero} pertence à sequência de Fibonacci.");
        }
        else
        {
            Console.WriteLine($"O número {numero} NÃO pertence à sequência de Fibonacci.");
        }
    }


    // Desafio 3

    public class DiaFaturamento
    {
        [JsonPropertyName("dia")]
        public int Dia { get; set; }

        [JsonPropertyName("valor")]
        public double Valor { get; set; }
    }
    static void FaturamentoDistribuidora()
    {
        try
        {
            string caminhoArquivo = "arquivo/dados.json";
            string json = File.ReadAllText(caminhoArquivo);

            var faturamentoMensal = JsonSerializer.Deserialize<List<DiaFaturamento>>(json);

            if (faturamentoMensal != null)
            {
                Console.WriteLine("Dados lidos com sucesso:");

            }
            else
            {
                Console.WriteLine("Erro ao desserializar os dados.");
            }

            var valores = faturamentoMensal.Where(d => d.Valor > 0).Select(d => d.Valor).ToList();

            if (valores.Any())
            {
                double menorValor = valores.Min();
                double maiorValor = valores.Max();
                double mediaMensal = valores.Average();
                int diasAcimaMedia = valores.Count(v => v > mediaMensal);

                Console.WriteLine($"Menor faturamento: R${menorValor:F2}");
                Console.WriteLine($"Maior faturamento: R${maiorValor:F2}");
                Console.WriteLine($"Dias acima da média mensal: {diasAcimaMedia}");
            }
            else
            {
                Console.WriteLine("Não há faturamento válido para calcular.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao processar o arquivo: {ex.Message}");
        }
    }


    // Desafio 4
    static void PercentualFaturamento()
    {
        var faturamentoEstados = new Dictionary<string, double>
        {
            { "SP", 67836.43 },
            { "RJ", 36678.66 },
            { "MG", 29229.88 },
            { "ES", 27165.48 },
            { "Outros", 19849.53 }
        };

        double total = faturamentoEstados.Values.Sum();

        Console.WriteLine("Percentuais de faturamento por estado:");
        foreach (var estado in faturamentoEstados)
        {
            double percentual = (estado.Value / total) * 100;
            Console.WriteLine($"{estado.Key}: {percentual:F2}%");
        }
    }

    // Desafio 5
    static void InverterString()
    {
        Console.Write("Digite uma string: ");
        string? entrada = Console.ReadLine();

        string resultado = "";

        foreach (char c in entrada)
        {
            resultado = c + resultado;
        }

        Console.WriteLine($"String invertida: {resultado}");
    }
}