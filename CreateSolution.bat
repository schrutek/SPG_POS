@echo off
set /p solutionName="Solution Name: (Spg.MySolutionName) "
set /p projectName="Project Name: (MyFrontEndName) "
set /p projectType="Project Type (console, mvc, webapi, wpf): ("%projectType%") "

md %solutionName%
cd %solutionName%

dotnet new sln --name %solutionName%

md libs
md docs
md src
md test

cd src
dotnet new %projectType% --name %solutionName%.%projectName%
dotnet new classlib --name %solutionName%.Application
dotnet new classlib --name %solutionName%.Infrastructure

cd %solutionName%.Infrastructure
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Bogus
cd..

dotnet new classlib --name %solutionName%.DomainModel
cd %solutionName%.DomainModel
md Exceptions
cd Exceptions

echo namespace %solutionName%.DomainModel.Exceptions > ProductCreateException.cs
echo { >> ProductCreateException.cs
echo     public class ProductCreateException { } >> ProductCreateException.cs
echo } >> ProductCreateException.cs
cd..
md Interfaces
cd Interfaces
echo namespace %solutionName%.DomainModel.Interfaces > ICustomInterface.cs
echo { >> ICustomInterface.cs
echo     public interface ICustomInterface { } >> ICustomInterface.cs
echo } >> ICustomInterface.cs
cd..
md Dtos
cd Dtos
echo namespace %solutionName%.DomainModel.Dtos > ProductDto.cs
echo { >> ProductDto.cs
echo     public class ProductDto { } >> ProductDto.cs
echo } >> ProductDto.cs
cd..
md Model
cd Model
echo namespace %solutionName%.DomainModel.Model > Product.cs
echo { >> Product.cs
echo     public class Product { } >> Product.cs
echo } >> Product.cs
cd..

cd..
dotnet new classlib --name %solutionName%.Repository

cd..
cd test
dotnet new xunit --name %solutionName%.%projectName%.Test

cd %solutionName%.%projectName%.Test
dotnet add package Moq
cd..

cd %solutionName%.%projectName%.Test
md Helpers
cd Helpers
echo namespace %solutionName%.%projectName%.Test.Helpers > DatabaseUtilities.cs
echo { >> DatabaseUtilities.cs
echo     public static class DatabaseUtilities { } >> DatabaseUtilities.cs
echo } >> DatabaseUtilities.cs
cd..
cd..
dotnet new xunit --name %solutionName%.Application.Test

cd %solutionName%.Application.Test
dotnet add package Moq
cd..

cd %solutionName%.Application.Test
md Helpers
cd Helpers
echo namespace %solutionName%.Application.Test.Helpers > DatabaseUtilities.cs
echo { >> DatabaseUtilities.cs
echo     public static class DatabaseUtilities { } >> DatabaseUtilities.cs
echo } >> DatabaseUtilities.cs
cd..
cd..
dotnet new xunit --name %solutionName%.DomainModel.Test

cd %solutionName%.DomainModel.Test
dotnet add package Moq
cd..

cd %solutionName%.DomainModel.Test
md Helpers
cd Helpers
echo namespace %solutionName%.DomainModel.Test.Helpers > DatabaseUtilities.cs
echo { >> DatabaseUtilities.cs
echo     public static class DatabaseUtilities { } >> DatabaseUtilities.cs
echo } >> DatabaseUtilities.cs
cd..
cd..
dotnet new xunit --name %solutionName%.Repository.Test

cd %solutionName%.Repository.Test
dotnet add package Moq
cd..

cd %solutionName%.Repository.Test
md Helpers
cd Helpers
echo namespace %solutionName%.Repository.Test.Helpers > DatabaseUtilities.cs
echo { >> DatabaseUtilities.cs
echo     public static class DatabaseUtilities { } >> DatabaseUtilities.cs
echo } >> DatabaseUtilities.cs
cd..
cd..

cd..

dotnet sln add src\%solutionName%.%projectName%\%solutionName%.%projectName%.csproj
dotnet sln add src\%solutionName%.Application\%solutionName%.Application.csproj
dotnet sln add src\%solutionName%.Infrastructure\%solutionName%.Infrastructure.csproj
dotnet sln add src\%solutionName%.DomainModel\%solutionName%.DomainModel.csproj
dotnet sln add src\%solutionName%.Repository\%solutionName%.Repository.csproj

dotnet sln add test\%solutionName%.%projectName%.Test\%solutionName%.%projectName%.Test.csproj
dotnet sln add test\%solutionName%.Application.Test\%solutionName%.Application.Test.csproj
dotnet sln add test\%solutionName%.DomainModel.Test\%solutionName%.DomainModel.Test.csproj
dotnet sln add test\%solutionName%.Repository.Test\%solutionName%.Repository.Test.csproj

dotnet add src\%solutionName%.%projectName%\%solutionName%.%projectName%.csproj reference src\%solutionName%.DomainModel\%solutionName%.DomainModel.csproj
dotnet add src\%solutionName%.Application\%solutionName%.Application.csproj reference src\%solutionName%.DomainModel\%solutionName%.DomainModel.csproj
dotnet add src\%solutionName%.Repository\%solutionName%.Repository.csproj reference src\%solutionName%.DomainModel\%solutionName%.DomainModel.csproj
dotnet add src\%solutionName%.Infrastructure\%solutionName%.Infrastructure.csproj reference src\%solutionName%.DomainModel\%solutionName%.DomainModel.csproj

dotnet add src\%solutionName%.%projectName%\%solutionName%.%projectName%.csproj reference src\%solutionName%.Application\%solutionName%.Application.csproj

dotnet add src\%solutionName%.Application\%solutionName%.Application.csproj reference src\%solutionName%.Repository\%solutionName%.Repository.csproj

dotnet add src\%solutionName%.Repository\%solutionName%.Repository.csproj reference src\%solutionName%.Infrastructure\%solutionName%.Infrastructure.csproj

dotnet add test\%solutionName%.%projectName%.Test\%solutionName%.%projectName%.Test.csproj reference src\%solutionName%.%projectName%\%solutionName%.%projectName%.csproj
dotnet add test\%solutionName%.Application.Test\%solutionName%.Application.Test.csproj reference src\%solutionName%.Application\%solutionName%.Application.csproj
dotnet add test\%solutionName%.Repository.Test\%solutionName%.Repository.Test.csproj reference src\%solutionName%.Repository\%solutionName%.Repository.csproj

dotnet add test\%solutionName%.%projectName%.Test\%solutionName%.%projectName%.Test.csproj reference src\%solutionName%.DomainModel\%solutionName%.DomainModel.csproj
dotnet add test\%solutionName%.Application.Test\%solutionName%.Application.Test.csproj reference src\%solutionName%.DomainModel\%solutionName%.DomainModel.csproj
dotnet add test\%solutionName%.Repository.Test\%solutionName%.Repository.Test.csproj reference src\%solutionName%.DomainModel\%solutionName%.DomainModel.csproj

dotnet new gitignore
