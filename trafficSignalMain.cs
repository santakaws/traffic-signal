using System;
using System.Windows.Forms;

public class trafficSignalMain {
    static void Main(string[] args) {
        System.Console.WriteLine("Welcome to the Main method of the Traffic Signal program.");
        trafficSignalInterface trafSigApp = new trafficSignalInterface();
        Application.Run(trafSigApp);
        System.Console.WriteLine("Main method will now shutdown.");
    }
}