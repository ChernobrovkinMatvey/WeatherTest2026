docker compose up -d weatherui

rem                 --wait      rem ждёт healthcheckи
rem docker compose stop weatherui
rem docker compose down         rem останавливает всё, и сети тоже

rem docker compose config       rem показать полный docker compose file

rem docker compose up -d        rem запуск всего файла
rem docker compose down
rem docker compose ps           rem все запущенные контейнеры

rem docker compose exec postgres psql -U postgres -d postgres       rem подкючиться к пг в контейнере