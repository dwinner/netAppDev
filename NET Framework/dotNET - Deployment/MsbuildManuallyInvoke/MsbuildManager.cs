using System;
using System.Collections.Generic;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;

namespace MsbuildManuallyInvoke
{
    public sealed class MsbuildManager : IDisposable
    {
        private const string BuildTarget = "Build";
        private const string CleanTarget = "Clean";

        public MsbuildManager(string solutionPath)
        {
            SolutionPath = solutionPath;
        }

        public string SolutionPath { get; private set; }

        public void Dispose()
        {
            Clean(SolutionPath);
        }

        public bool Rebuild()
        {
            return Clean(SolutionPath) && Build(SolutionPath);
        }

        private static BuildResultCode MsbuildExecute(string solution, string target)
        {
            var projectCollection = new ProjectCollection();
            var globalProperties = new Dictionary<string, string>
            {
                {"Configuration", "Debug"},
                {"Platform", Environment.Is64BitOperatingSystem ? "x64" : "x86"}
            };
            var buildRequest = new BuildRequestData(solution, globalProperties, null, new[] {target}, null);
            var buildResult =
                BuildManager.DefaultBuildManager.Build(new BuildParameters(projectCollection), buildRequest);

            return buildResult.OverallResult;
        }

        private static bool Build(string solution)
        {
            return MsbuildExecute(solution, BuildTarget) == BuildResultCode.Success;
        }

        private static bool Clean(string solution)
        {
            return MsbuildExecute(solution, CleanTarget) == BuildResultCode.Success;
        }
    }
}