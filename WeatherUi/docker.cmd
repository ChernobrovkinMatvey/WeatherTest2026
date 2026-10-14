cd ..

docker build -f .\WeatherUi\Dockerfile -t weather-ui .

rem docker images                   rem посмотреть все образы
rem docker rmi weather-ui:latest    rem либо IMAGE_ID       rem удалить образ

rem запустить в контейнере weather-app
docker run -d -p 3080:8080 --name weather-app weather-ui

rem docker stop weather-app         rem остановить контейнер
rem docker rm -f weather-app        rem удалить контейнер

rem docker ps -a    rem просмотр контейнеров
rem docker ps 		rem только запущенные

rem версия linux внутри запущенного контейнера
rem docker exec weather-app cat /etc/os-release

rem docker exec -it weather-app sh      rem зайти в shell контейнера
rem docker logs weather-app             rem посмотреть логи контейнера