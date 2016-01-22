using FRS.MT940Loader;

namespace FRS.ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {

            MT940LoaderMain l = new MT940LoaderMain(@"C:\ISTWORK\CODE\GF.FRS\GF.FRS.MT940Loader\Samples\KSA\SCB Vostro - 031001548008 -940d.txt",
                                                      "{1:F01AAALSARIAXXX.SN...ISN.}{2:I940SCBLGB20XWEBN}{3:{108:xxxxx}}{4:",
                                                      "-}");
            l.ValidateFile();
        }
    }
}
