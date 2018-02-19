using NetHttpClient.Http;
using NetHttpClient.Http.Response;
using System;
using System.Collections.Generic;

namespace NetHttpClient.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Consume First Endpoint
            Http.NetHttpClient clientRest = Http.NetHttpClient.Builder()
                .BaseAddress("http://localhost:5000/")
                .Requesturi("api/v1/Auth/test/post")
                .HttpPost()
                .Build();

            //Task<UserDto> auth = clientRest.ConsumeRecordAsync<UserDto>();
            //UserDto auth = clientRest.ConsumeRecordAsync<UserDto>().Result;
            var auth = clientRest.ConsumeAsync<UserDto>().Result;

















            //Consume Second Endpoint
            Http.NetHttpClient clientRest2 = Http.NetHttpClient.Builder()
                .BaseAddress("http://localhost:5000/")
                .Requesturi("api/v1/Auth/test/post2")
                .HttpPost()
                .Payload(new
                {
                    name = "Jorge Castro",
                    fullName = "Jorge Luis Castro Medina"
                })
                .OnSuccessEvent((HttpResponseDTO<object> response) => {
                    var res = response.Body;
                })
                .Build();

            //var listAuth = clientRest2.ConsumeAsync<List<UserDto>>().Result;
            clientRest2.Call<List<UserDto>>();












            ////Consume Second Endpoint
            Http.NetHttpClient clientRest3 = Http.NetHttpClient.Builder()
                .FullUrl("http://localhost:5000/api/v1/Auth/test/requireAuthorization")
                .HttpPost()
                .OnSuccessEvent((HttpResponseDTO<object> response) => {
                    var res = response.Body;
                })
                .OnFailureEvent(delegate (HttpResponseDTO<object> response) {
                    Console.WriteLine(response.Message);
                    Console.ReadLine();
                })
                .Build();
            
            //var listAuth3 = clientRest3.ConsumeAsync<List<UserDto>>().Result;
            clientRest3.Call<List<UserDto>>();



        }
        
    }
}
