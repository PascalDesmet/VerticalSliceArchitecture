# How to use this solution

You can use a VSIX project to create a VSIX installation file that installs your project as a project template in Visual Studio.
Unfortunately VSIX project can not address projects with a .Net version higher than 4.8.

Therefor you need to use the option 'Export Template...' under the 'Project' menu.

I have set up this solution as a working application to test all code.

The repository contains 2 branches 'VsaTemplate' and 'VsixTemplate'.

The working application is maintained in VsaTemplate and changes are commited to that branch.

These commits can be cherry picked into the VsixTemplate and altered there to change names to there template placeholders or conditional statements can be added to use C# version dependant code for example.

