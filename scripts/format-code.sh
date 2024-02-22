# install tool
dotnet tool install -g dotnet-format

# run format check
command="$1"
dotnet-format --verbosity detailed $command "${PWD//\/scripts/}/FestivalApp/FestivalApp.sln"
error=$?
[[ $error -ne 0 ]] && echo "Bad code styling found. Please run '.\scripts\format-code.sh'."
exit $error
