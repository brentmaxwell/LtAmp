echo START
set token=sqp_b888add02f8de1cbd54d9d04cfd59536a40458ca
echo BEGIN
dotnet sonarscanner begin /k:"LtAmp" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="%token%"
echo BUILD
dotnet build
echo END
dotnet sonarscanner end /d:sonar.token="%token%"
echo DONE