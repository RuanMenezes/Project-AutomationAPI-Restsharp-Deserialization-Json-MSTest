using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using AprendizadoAutomacaoApisRestSharp.Models;
using Newtonsoft.Json.Linq;

namespace AprendizadoAutomacaoApisRestSharp
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TesteAPIGetTokenBearer()
        {
            var client = new RestClient("http://httpbin.org/"); //Informo qual é a url do Server onde está a API

            //client.AddDefaultHeader("authorization", "Bearer" + "eu22eiru==");//Comando utilizado para incluir um token BEARER em uma api que consulta status de resposta 200

            var request = new RestRequest("status/"/*Complemento com nome API*/ + "{status}"/*parametro da api GET que busca o status 200*/, Method.GET);//Tipo do da API      request.AddUrlSegment($"{segmentUrl}", data);
            request.AddUrlSegment($"status", 200);//Status passado como parâmetro
            request.RequestFormat = DataFormat.Json;//RequestBody selecionado com o tipe de JSON (Serve mais para POST, PUT, DELETE, etc)
            //request.RequestFormat = DataFormat.Json;//tem a mesma função do comando acima.

            IRestResponse response = client.Execute(request);//Comando que executa sua requisição da api GETStatus que no caso queremos o 200
            int getStatusResponseAPI = (int)response.StatusCode;

            Assert.AreEqual(200/*O que eu espero*/, getStatusResponseAPI/*O que a API me retornou*/, "O Status não é o esperado");
            //O Assert acima é a classe que você utilizará para validar seus testes - Essa classe está presente nos nugets (Nunit, Mstest, Xunit, etc).
            //No caso, estamos utilizando o MSTest
            //Para executar os testes acesse o menu View >> TestExplorer- 
            //quando você efetuar o Build do seu projeto os testes deverão ser exibidos, todos os métodos que tem o gatilho [TestMethod]
        }

        [TestMethod]
        [Obsolete]
        public void TestPostCadastroUsuario()
        {
            var client = new RestClient("http://httpbin.org/");
            var request = new RestRequest("post", Method.POST);

            request.RequestFormat = DataFormat.Json;
            request.AddBody(new PostModelCadastraUsuario()
            {
                email = "Teste@gera.com.br",
                senha = "xsdserre1",
                idade = 45
            });
            
            IRestResponse response = client.Execute(request);
            
            var deserializeJson = JObject.Parse(response.Content);//Deserializando o JSON (Dinvidindo-o em Pedáços únicos)
            var emailResponse = deserializeJson.SelectToken("json.email").ToString();//Localizando os resultados no responseBody JSON apresentado na execução da API
            var senhaResponse = deserializeJson.SelectToken("json.senha").ToString();//ToString é um método da classe String que converte um objeto que possui X tipo (INT por exemplo) em string
            var idadeResponse = (int)deserializeJson.SelectToken("json.idade");

            //Testes (Assertações)
            Assert.AreEqual("Teste@gera.com.br", emailResponse , "Email não está equivalente ao esperado"/*Mensagem de erro caso a comparação entre X e Y seja inválida*/);
            Assert.AreEqual("xsdserre1", senhaResponse, "Senha não está equivalente ao esperado");
            Assert.AreEqual(45, idadeResponse, "Idade não está equivalente ao esperado");
        }
    }
}