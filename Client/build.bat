call "%~dp0/svnupdate.bat" 1
echo �������!

set UnityExe="D:/Unity52/Editor/Unity.exe"
set UnityProject=%~dp0

%UnityExe% -nographics -batchmode -projectPath %UnityProject% -args %1 -quit -executeMethod BuildUtil.BuildByCommonline
echo ���!
