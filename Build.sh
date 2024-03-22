echo First remove old binary files
rm *.dll
rm *.exe

echo View the list of source files
ls -l

echo Compile trafficSignalInterface.cs to create the file: trafficSignalInterface.dll
mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -out:trafficSignalInterface.dll trafficSignalInterface.cs

echo Compile trafficSignalMain.cs and link the two previously created dll files to create an executable file. 
mcs -r:System -r:System.Windows.Forms -r:trafficSignalInterface.dll -out:trafSig.exe trafficSignalMain.cs

echo View the list of files in the current folder
ls -l

echo Run the Assignment 1 program.
./trafSig.exe

echo The script has terminated.