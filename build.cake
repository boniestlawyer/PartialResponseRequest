#tool "nuget:?package=xunit.runner.console&version=2.4.0"
#tool nuget:?package=ReportGenerator&version=4.3.0

#addin nuget:?package=Cake.Coverlet&version=2.3.4

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Pack");
var configuration = Argument("configuration", "Release");
var version = Argument("package-version", "1.0.0.0");
var outputDirectory = Argument("outputDirectory", "./output");
var testResultsDirectoryArgument = Argument("testResultsDirectory", @".\test-results\");

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx => Information("Running tasks..."));
Teardown(ctx => Information("Finished running tasks."));

TaskSetup(setupContext =>
{
   if(TeamCity.IsRunningOnTeamCity)
   {
      TeamCity.WriteStartBuildBlock(setupContext.Task.Description ?? setupContext.Task.Name);
      TeamCity.WriteStartProgress(setupContext.Task.Description ?? setupContext.Task.Name);
   }
});

TaskTeardown(teardownContext =>
{
   if(TeamCity.IsRunningOnTeamCity)
   {
      TeamCity.WriteEndProgress(teardownContext.Task.Description ?? teardownContext.Task.Name);
      TeamCity.WriteEndBuildBlock(teardownContext.Task.Description ?? teardownContext.Task.Name);
   }
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

var packageProjects =  new [] {
   "Core",
   "Fields",
   "Filters",
   "AspNetCore.ResponsePruner"
};

var packTask = Task("Pack")
   .IsDependentOn("Build");

foreach(var project in packageProjects) {
   Task($"Pack-{project}")
      .Does(() => {
         var settings = new DotNetCorePackSettings
         {
               ArgumentCustomization = args => args.Append($"-p:PackageVersion={version}"),
               Configuration = configuration,
               OutputDirectory = outputDirectory,
         };

         DotNetCorePack($"./src/PartialResponseRequest.{project}/PartialResponseRequest.{project}.csproj", settings);
      });

   packTask.IsDependentOn($"Pack-{project}");
}

Task("Build")
   .Does(() => {
      DotNetCoreBuild("./src/PartialResponseRequest.sln", new DotNetCoreBuildSettings(){
         Configuration = configuration
      });
   });

var testResultsDirectory = new DirectoryPath(testResultsDirectoryArgument);
var opencoverFileResult = "results";
Task("Test")
   .IsDependentOn("Build")
   .Does(() => {
      var outputFormat = CoverletOutputFormat.cobertura;
      if(TeamCity.IsRunningOnTeamCity)
      {
         outputFormat |= CoverletOutputFormat.teamcity;
      }

      var coverletSettings = new CoverletSettings
      {
        CollectCoverage = true,
        CoverletOutputFormat = outputFormat,
        CoverletOutputDirectory = testResultsDirectory,
        CoverletOutputName = opencoverFileResult
      };

      var testProject = GetFiles("./src/PartialResponseRequest.Tests/*.csproj").Select(x => x.FullPath).ToArray().First();

      var testSettings = new DotNetCoreTestSettings() { 
         Logger = $"trx;LogFileName={opencoverFileResult}.trx",
         ResultsDirectory = testResultsDirectory
      };

      DotNetCoreTest(testProject, testSettings, coverletSettings);

      if(TeamCity.IsRunningOnTeamCity)
      {
         TeamCity.ImportData("mstest", testResultsDirectory.CombineWithFilePath(new FilePath($"{opencoverFileResult}.trx")));
      }
   });

Task("Report")
   .IsDependentOn("Test")
   .Does(() => {
      var reportGeneratorTool = new FilePath("./tools/ReportGenerator.4.3.0/tools/netcoreapp2.1/ReportGenerator.dll");
      var rawTestResultFile = testResultsDirectory.CombineWithFilePath(new FilePath($"{opencoverFileResult}.cobertura.xml"));

      DotNetCoreExecute(reportGeneratorTool.FullPath, 
         new ProcessArgumentBuilder()
            .Append($"-reports:{rawTestResultFile.FullPath}")
            .Append($"-targetdir:{testResultsDirectory.FullPath}")
            .Append("-reportTypes:Html"));
   });

RunTarget(target);