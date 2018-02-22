# NetHttpClient
Http Client for .NET

### Installing from Nuget Package
> https://www.nuget.org/packages/devjc.NetHttpClient/


### Installing from Visual Studio
Right click on the project

![alt Install from Nuget Package](https://image.prntscr.com/image/h_GMYR4MRMuFWV4MqQ__lQ.png  "Install from Nuget Package")


Search package and install
> devjc.nethttpclient
<img src="https://image.prntscr.com/image/DoGHcnpZRgKFPH0gwmRj3w.png" width="700" alt="Search Package">



## Usage
We can set NetHttpClient to work with Http verbs GET, POST, PUT and DELETE

* HttpPost()
* HttpGet()
* HttpPut()
* HttpDelete()

**When calling the service, the expected response object must be specified**

```
Http.NetHttpClient clientRest = Http.NetHttpClient.Builder()
    .BaseAddress("http://localhost:5000/")
    .Requesturi("api/v1/Auth/test/post")
    .HttpPost()
    .Build();

var auth = clientRest.ConsumeAsync<UserDto>().Result;
```

### Using Callback

```
Http.NetHttpClient clientRest = Http.NetHttpClient.Builder()
    .FullUrl("http://localhost:5000/api/v1/Auth/test/requireAuthorization")
    .HttpPost()
    .OnSuccessEvent((HttpResponseDTO<object> response) => {
        var data = response.Body;
    })
    .OnFailureEvent(delegate (HttpResponseDTO<object> response) {
        string errorMessage = response.Message;
        // Error code here
    })
    .Build();

clientRest.Call<List<UserDto>>();
```


### Add custom Handler
To establish a handler we must define a class that inherits from **HttpClientHandler** and pass it as a parameter to the **Handler** method
```
Http.NetHttpClient clientRest = Http.NetHttpClient.Builder()
	.FullUrl("http://localhost:5000/api/v1/Auth/test/requireAuthorization")
	.HttpPost()
	.Handler(new CheckTokenHandler())
	.Build();
```


## Author
* **Jorge Luis Castro Medina - Software Developer**
