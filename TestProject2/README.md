# Project of Automation with Selenium
  
This Project is a practice of automation using Selenium with C#, ChromeDriver and NUnit.
  
## Requirements
  
- [Docker](https://www.docker.com/get-started) installed on your machine.
  
## Build Docker image
  
To build the Docker image, run the following command in the root directory of the project (where the Dockerfile is located):
  
```bash  
docker-compose up --build
```

To run it in the second plane, use the following command:
  
Preferebly, run the following command:

```bash
docker-compose up --build -d
```

Run the test: *It does't matter if the container is marked as 'Exited'*

```bash
dotnet test TestProject2.csproj --no-restore --verbosity normal
```