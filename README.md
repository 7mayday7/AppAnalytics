В ходе разработки были использованы следующие технологии: C# + ASP.NET Core, СУБД PostgreSQL (через отдельный Docker-образ) с исползованием alpine. 
Для развертывания окружения испоьльзовася Docker Compose.

1. В терминале Windows ввести команду docker build -t demo/asp_core:1 .
2. Затем docker-compose up postgres_db -d
3. Подождите пока база данных запустится, далее docker-compose up app -d
4. Для проверки введите команду docker-compose ps -a
5. Затем попробуйте теперь добавить заметки в postman
6. Затем добавить переменную окружения в конфигурации Visual Studio (https://www.tutorialsteacher.com/core/aspnet-core-environment-variable#:~:text=ASP.NET%20Core%20uses%20an,is%20case%20sensitive%20on%20Linux)
