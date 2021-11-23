FROM mcr.microsoft.com/dotnet/sdk:5.0

COPY ./publish /publish
WORKDIR /publish
EXPOSE 5000/tcp
ENTRYPOINT ["dotnet", "QuizzCorrector.dll"]