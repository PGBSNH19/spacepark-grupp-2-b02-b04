using RestSharp;
using SpaceParkWeb.Models;
using SpaceParkWeb.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkWeb
{
    public class RestSharpCaller
    {
        private RestClient client;
        public RestSharpCaller()
        {
            //client = new RestClient($"http://spacepark-api-dev.northeurope.azurecontainer.io/api/v1.0/");
            client = new RestClient($"https://localhost:44386/api/v1.0/");
        
        }
        public async Task<Person> GetCustomer(string input)
        {
            var jsonSerializer = NewtonsoftJsonSerializer.Default;
            client.AddHandler("application/json", jsonSerializer);

            var request = new RestRequest
            {
                Method = Method.GET,
                RequestFormat = DataFormat.Json,
                JsonSerializer = NewtonsoftJsonSerializer.Default,
                Resource = $"person?name={input}"
            };

            var result = await client.GetAsync<Person>(request);
            return result;
        }
        public async Task<Spaceship> PostSpaceship(Spaceship spaceship)
        {
            var jsonSerializer = NewtonsoftJsonSerializer.Default;
            client.AddHandler("application/json", jsonSerializer);

            var request = new RestRequest
            {
                Method = Method.POST,
                RequestFormat = DataFormat.Json,
                JsonSerializer = NewtonsoftJsonSerializer.Default,
                Resource = $"spaceship/postspaceship"
            };
            request.AddJsonBody(spaceship);
            var result = await client.PostAsync<Spaceship>(request);

            return result;
        }

        public async Task<Spaceship> ParkSpaceship(Spaceship spaceship)
        {
            var request = new RestRequest
            {
                Method = Method.POST,
                RequestFormat = DataFormat.Json,
                JsonSerializer = NewtonsoftJsonSerializer.Default,
                Resource = $"spaceship/parkspaceship"
            };
            request.AddJsonBody(spaceship);
            var result = await client.PostAsync<Spaceship>(request);

            return result;
        }
        public async Task<List<Spaceship>> GetSpaceships(string input)
        {
            var jsonSerializer = NewtonsoftJsonSerializer.Default;
            client.AddHandler("application/json", jsonSerializer);

            var request = new RestRequest
            {
                Method = Method.GET,
                RequestFormat = DataFormat.Json,
                JsonSerializer = NewtonsoftJsonSerializer.Default,
                Resource = $"spaceship/GetSpaceShips?name={input}"
            };
            var result = await client.GetAsync<List<Spaceship>>(request);
            return result;
        }

        public async Task<Spaceship> GetSpaceshipById(int id)
        {
            var jsonSerializer = NewtonsoftJsonSerializer.Default;
            client.AddHandler("application/json", jsonSerializer);

            var request = new RestRequest
            {
                Method = Method.GET,
                RequestFormat = DataFormat.Json,
                JsonSerializer = NewtonsoftJsonSerializer.Default,
                Resource = $"spaceship/{id}"
            };
            var result = await client.GetAsync<Spaceship>(request);
            return result;
        }

        public async Task<Person> PutPerson(Person person)
        {
            var jsonSerializer = NewtonsoftJsonSerializer.Default;
            client.AddHandler("application/json", jsonSerializer);

            var request = new RestRequest
            {
                Method = Method.PUT,
                RequestFormat = DataFormat.Json,
                JsonSerializer = NewtonsoftJsonSerializer.Default,
                Resource = $"person"
            };
            request.AddJsonBody(person);
            var result = await client.PutAsync<Person>(request);
            return result;
        }

        public async Task<Person> GetPerson(string input)
        {
            var jsonSerializer = NewtonsoftJsonSerializer.Default;
            client.AddHandler("application/json", jsonSerializer);

            var request = new RestRequest
            {
                Method = Method.GET,
                RequestFormat = DataFormat.Json,
                JsonSerializer = NewtonsoftJsonSerializer.Default,
                Resource = $"person?name={input}"
            };

            var result = await client.GetAsync<Person>(request);
            return result;
        }

        public async Task<Spaceship> CheckoutShip(int shipId)
        {
            var request = new RestRequest
            {
                Method = Method.DELETE,
                RequestFormat = DataFormat.Json,
                JsonSerializer = NewtonsoftJsonSerializer.Default,
                Resource = $"spaceship/{shipId}"
            };

            return await client.DeleteAsync<Spaceship>(request);
        }
        public async Task<Person> PostPerson(string input)
        {
            var jsonSerializer = NewtonsoftJsonSerializer.Default;
            client.AddHandler("application/json", jsonSerializer);

            var request = new RestRequest
            {
                Method = Method.POST,
                RequestFormat = DataFormat.Json,
                JsonSerializer = NewtonsoftJsonSerializer.Default,
                Resource = $"person?name={input}"
            };
            var result = await client.PostAsync<Person>(request);

            return result;
        }
    }

   
}
