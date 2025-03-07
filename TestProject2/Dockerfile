FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env  
  
RUN apt-get update && \  
    apt-get install -y --no-install-recommends wget gnupg2 unzip && \  
    wget -q -O - https://dl.google.com/linux/linux_signing_key.pub | apt-key add - && \  
    echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" > /etc/apt/sources.list.d/google-chrome.list && \  
    apt-get update && \  
    apt-get install -y google-chrome-stable && \  
    apt-get clean && \  
    rm -rf /var/lib/apt/lists/*  
  
WORKDIR /app  
  
COPY TestProject2.csproj ./ 
RUN dotnet restore  
  
COPY . ./  
RUN dotnet publish -c Release -o out  

RUN ls -la out 
  
FROM mcr.microsoft.com/dotnet/aspnet:7.0  
WORKDIR /app  
  
COPY --from=build-env /app/out .  
  
ENTRYPOINT ["dotnet", "TestProject2.dll"]  