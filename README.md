dotnet new webapi --use-controllers -o MedicineApi<br />
dotnet dev-certs https --trust<br />
dotnet run --launch-profile https --project MedicineApi/<br />
dotnet new gitignore<br />
dotnet add MedicineApi/ package MongoDB.Driver<br />
dotnet new xunit -n MedicineApi.Tests<br />
dotnet add MedicineApi.Tests/MedicineApi.Tests.csproj reference MedicineApi/MedicineApi.csproj<br />
dotnet add MedicineApi.Tests package Moq<br />
dotnet add MedicineApi.Tests package Microsoft.AspNetCore.Mvc.Testing<br />
dotnet add package Mongo2Go
