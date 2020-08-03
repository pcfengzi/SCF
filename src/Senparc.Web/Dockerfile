# 添加基础镜像
FROM microsoft/aspnetcore:2.1 AS base
#容器中系统的工作空间
WORKDIR /app
#设置Docker容器对外暴露的端口
EXPOSE 80

FROM microsoft/aspnetcore-build:2.1 AS build
WORKDIR /src
#拷贝当前文件夹下的文件到容器中系统的工作空间
COPY Senparc.Core.sln ./
COPY Senparc.Web/Senparc.Web.csproj Senparc.Web/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/Senparc.Web
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
#容器中使用 ["dotnet","系统启动的dll"] 来运行应用程序
#使用ENTRYPOINT ["dotnet","系统启动的dll"]
#或使用 CMD ["dotnet","系统启动的dll"]
ENTRYPOINT ["dotnet", "Senparc.Web.dll"]
