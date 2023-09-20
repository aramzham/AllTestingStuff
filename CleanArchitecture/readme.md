## Clean architecture

<img src="https://miro.medium.com/v2/resize:fit:720/format:webp/1*xTb6pwbDRrjbL7Ku1WLh2g.png" alt="Clean architecture"/>

``` shell
dotnet new --install Clean.Architecture.Solution.Template
```
Create new solution
``` bash
dotnet new ca-sln
```

**Domain** is the core of your application.
Here you will define:
- entities
- core business rules
- factory interfaces
- enumerations
- value objects
- custom exceptions

**Domain** is not allowed to interact with any layer except **Application**.
This is where you want to implement your use cases.
Here you can use services or MediatR.

In **Infrastructure** you introduce any external dependencies like services, databases...
Some people like to split this into Infrastructure and Persistence layers where the latter is for database related stuff.

In **Presentation** layer you have 2 approaches:
1. to have a separate project where you place your controllers, minimal api endpoints, razor pages etc...
2. define an web project and put everything there