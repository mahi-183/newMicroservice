#FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443

#FROM microsoft/dotnet:2.2-sdk AS build
#WORKDIR /src
#COPY NotesMicroservice/NotesMicroservice.csproj NotesMicroservice/
#COPY RepositoryManager/RepositoryManager.csproj RepositoryManager/
#COPY CommanLayer/CommanLayer.csproj CommanLayer/
#COPY BusinessManager/BusinessManager.csproj BusinessManager/
#RUN dotnet restore NotesMicroservice/NotesMicroservice.csproj
#COPY . .
#WORKDIR /src/NotesMicroservice
#RUN dotnet build NotesMicroservice.csproj -c Release -o /app

#FROM build AS publish
#RUN dotnet publish NotesMicroservice.csproj -c Release -o /app

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "NotesMicroservice.dll"]
FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app
ENV ASPNETCORE_URLS http://+:96
EXPOSE 94
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "NotesMicroservice.dll"]
