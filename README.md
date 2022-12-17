## Welcome to Quizzd, the StackOverflow guessing game

### Local Application Execution

#### Prerequisites
1. Install VisualStudio 2022
1. Install .Net 7 SDK.
   * You can find the installer [here](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
1. Update AzureFunction toolset
   * Within VisualStudio navigate to `Tools > Options > Azure Functions`
   * Click the `Check for Updates` button

### Local Execution
1. Build solution with VisualStudio
1. Configure multiple startup projects
   * Right click the solution in `Solution Explorer`
   * Select `Properties` from the context menu
   * From the `Startup Project` view
   * Select `Multiple startup projects`
   * For the Api and Client in the table select `Start` as the action from the dropdown
   * Click `Ok` to save the changes
1. Start the site by navigating to `Debug > Start Wihout Debugging`