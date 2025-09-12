using System.Security.Cryptography;

namespace Token_Generator;

internal class Program
{
    static void Main(string[] args)
    {
        string chave = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        Console.Write("Chave Gerada 32 Bytes: " + chave);

        Console.ReadKey();
    }
}
