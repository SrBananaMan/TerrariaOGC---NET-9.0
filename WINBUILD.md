## Getting Started
In order to get started with TerrariaOGC, you will need to acquire some prerequisites:
- [FNA](https://github.com/FNA-XNA/FNA)
- [Terraria Content Conversion Suite](https://github.com/PPrism/TCCS)
- [Visual Studio](https://visualstudio.microsoft.com/downloads/) that is capable of building .NET Framework 4.0 projects.

### Directory Preparation
Firstly, you will need to acquire the contents of the repo. Do not open the .sln or .csproj for TerrariaOGC just yet, as the environment needs to be setup first.
You can clone the repo using the following command in [Git](https://git-scm.com/downloads):
`git clone https://github.com/PPrism/TerrariaOGC'

As mentioned in the 'Getting Started' section, you need a version of Visual Studio that is capable of building .NET Framework 4.0 projects. If you have Visual Studio 2022 or higher, this will not support .NET Framework 4.0 by default, leaving you with 3 options:
- Get TerrariaOGC working on Visual Studio 2019 or lower.
- Retarget the project to .NET Framework 4.5.1 or higher and make the necessary adjustments to dependencies.
- Copy the .NET 4.0 [NuGet](https://www.nuget.org/packages/microsoft.netframework.referenceassemblies.net40) reference assemblies over to your current reference assemblies. This is often found at: C:\Program files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework.

### Setting up FNA
Please note that this will only cover what is needed for the project. For instructions to setup FNA, you can get started [here](https://fna-xna.github.io/docs/1:-Setting-Up-FNA/).

Firstly, make sure the 'FNA' folder you have downloaded or cloned is in the same directory as the TerrariaOGC.sln. Next, copy the 'GamerServices' and 'Net' folder from the 'Dependencies' folder inside of 'TerrariaOGC' and place them in the 'src' folder inside of 'FNA'.
* These contain the stub functions needed by these versions of Terraria due to it's interfacing with the Xbox system for the XDK release and for some other functions which have been preserved for authenticity purposes.

Now, open the TerrariaOGC.sln at the root directory of this setup, and inside of the solution explorer, click 'Show All Files'. Right-click on both 'GamerServices' and 'Net' in the FNA project, and click on 'Include in project'.
While still in the solution explorer, right-click on 'Dependencies' for 'FNA.NetFramework', click 'Add Project Reference', click 'Browse' in the reference manager and go to the Lidgren.Network.dll found inside of the previously mentioned 'Dependencies' folder and then import it. 
Before you click 'Ok', go to 'Assemblies' in the reference manager and be sure to include 'System.Windows.Forms'. You can setup the rest of FNA as per your system needs.

After this, open the drop-down for the 'TerrariaOGC' project, and then right-click on the folder labelled 'Dependencies' and then click 'Exclude from Project'.

After you setup FNA, you can now click the first configuration drop-down and choose which version you would like to run, or you can go to the properties of the 'TerrariaOGC' project file, and customise the 'Debug' configuration to your desires.

Once you build, your desired configuration will have been built in the appropriate folder located in the 'TerrariaOGC/bin' directory. Please ensure you have the required libraries for FNA (e.g., FAudio.dll) in those directories as well.

### The 'Content' Folder
Now it is time to setup your 'Content' folder so that the executable can run.

To make things simple, I have developed a conversion program that can setup one's 'Content' folder for TerrariaOGC.
Once the Terraria Content Conversion Suite (TCCS) is downloaded, place the executable and the required 'Prerequisites' folder in the same directory as the 'Content' folder. 
You can then open the executable and follow the instructions to begin conversion. You can find out more about TCCS [here](https://github.com/PPrism/TCCS).

Now your 'Content' folder is ready to go, drag it into the directory where you built your specified version of TerrariaOGC and then you can start the executable without issue. A settings file will be generated after the first run which you can modify afterwards.
