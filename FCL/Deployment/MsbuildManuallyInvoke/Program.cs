/**
 * Программный вызов msbuild
 */

namespace MsbuildManuallyInvoke
{
    internal static class Program
    {
        private const string OmniSampleSolution =
            @"C:\Users\SimpleUser\Documents\SVN\Samples\Visual Studio\OmniSample\OmniSample (vs2010).sln";

        private static void Main()
        {
            using (var buildManager = new MsbuildManager(OmniSampleSolution))
            {
                buildManager.Rebuild();
            }
        }
    }
}