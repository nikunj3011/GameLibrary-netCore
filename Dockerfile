Create Dockerfile in the web application folder
Put the following content into it

FROM microsoft/aspnetcore

COPY ./publish /publish
WORKDIR /publish
EXPOSE 5000/tcp
ENTRYPOINT ["dotnet", "QuizzCorrector.dll"]
Build and publish the project by running dotnet publish -c Release -o publish
Build Docker image by running docker build -t QuizzCorrector