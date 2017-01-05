FROM microsoft/dotnet:runtime

ENV HOME=/usr/src/app
RUN mkdir -p $HOME
WORKDIR $HOME
COPY build .

ENV ASPNETCORE_URLS="http://*:5000"
EXPOSE 5000

ENTRYPOINT ["dotnet", "server.dll"]
