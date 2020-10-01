using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpacePark.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using SpacePark.Services;
using Microsoft.AspNetCore.Http;

namespace SpacePark
{
    public class ParkingEngine
    {
        public static async Task<PersonResult> GetPersonData(string input)
        {
            var client = new RestClient("https://swapi.dev/api/");
            var request = new RestRequest(input, DataFormat.Json);
            var apiResponse = await client.GetAsync<PersonResult>(request);

            return apiResponse;
        }

        public static async Task<Spaceship> GetSpaceShipData(string input)
        {
            var client = new RestClient(input);
            var request = new RestRequest("", DataFormat.Json);
            var apiResponse = await client.GetAsync<Spaceship>(request);

            return apiResponse;
        }
    }
}
