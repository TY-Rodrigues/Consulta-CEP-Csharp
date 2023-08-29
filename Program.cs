using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsultaCEPConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Digite o CEP: ");
            string cep = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(cep))
            {
                await ConsultarCEP(cep);
            }
            else
            {
                Console.WriteLine("CEP inválido.");
            }
        }

        static async Task ConsultarCEP(string cep)
        {
            string url = $"https://viacep.com.br/ws/{cep}/json/";
            // Tentei aplicar o URL entregue por voces porem não obtive nehum retorno positivo.
            // Apliquei em inumeros códigos porem nada. Com isso eu Ultilizei esse URL(ViaCEP).

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    Endereco endereco = JsonConvert.DeserializeObject<Endereco>(jsonResponse);

                    Console.WriteLine($"CEP: {endereco.Cep}");
                    Console.WriteLine($"Logradouro: {endereco.Logradouro}");
                    Console.WriteLine($"Bairro: {endereco.Bairro}");
                    Console.WriteLine($"Localidade: {endereco.Localidade}");
                    Console.WriteLine($"UF: {endereco.Uf}");
                }
                else
                {
                    Console.WriteLine("Erro ao consultar o CEP.");
                }
            }
        }

        class Endereco
        {
            public string Cep { get; set; }
            public string Logradouro { get; set; }
            public string Bairro { get; set; }
            public string Localidade { get; set; }
            public string Uf { get; set; }
        }
    }
}
