gacutil /l > gac_assemblies.txt  # Все сборки GAC
gacutil /i SharedDemo.dll /f     # Установка сборки в GAC
gacutil /l SharedDemo        # Просмотр сборки в GAC