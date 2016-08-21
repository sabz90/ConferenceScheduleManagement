using ConferenceScheduleManagement.ConferenceScheduler;
using ConferenceScheduleManagement.IOControllers;
using System;

/*
 * AUTHOR: SABARISH K R E
 * EMAIL: sabarish90@outlook.com
 * */
namespace ConferenceScheduleManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = args.Length > 0 ? args[0] : string.Empty;

            IInputController inputFileReader = new FileInputProcessor(inputFile);
            IOutputController consoleOutput = new ConsoleOutput();

            try
            {
                CSMController csmController = new CSMController(inputFileReader, consoleOutput);
                csmController.Run();
            }
            catch (ConferenceSchedulerException ex)
            {
                consoleOutput.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fatal Error: \nMessage: " + ex.Message + "\nStacktrace: " + ex.StackTrace);
            }
            Console.WriteLine("\n\nPress any key to exit.");
            Console.ReadLine();
        }        
    }
}
