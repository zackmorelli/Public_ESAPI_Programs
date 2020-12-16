using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;


/*
    Liver Only Normal Tissue Complication Probability (NTCP) Calculator - ESAPI 15.6 version (3/16/2020)
    This program is expressely written as a plug-in script for use with Varian's Eclipse Treatment Planning System, and requires Varian's API files to run properly.
    This program also requires .NET Framework 4.5.0 to run properly, and the MathNet.Numerics class library package, which is freely availible on the NuGet Package manager in Visual Studio, However I have also included a copy here.
    The MathNet.Numerics package contains an Error function method which is used in the NTCP calculation.
    This C# source code file is technically for a class library, or DLL, not an executable program. However, the code below, and the ESAPI class libraries, allows this to be run by Eclipse like it is an executable program. 
    In terms of how the script runs in Eclipse, the program starts here and then starts a GUI, which is a Windows Forms class library. Most of the work of this program is actaully in the GUI.cs file.
    When the NTCPcalc Visual Studio project is compiled, it will make a single DLL file called "LiverOnlyNTCPcalc.esapi". The .esapi file extensions allows Eclipse to recognize it as an Eclipse script.
    The only files you need to put in your Eclipse published scripts folder for the Script to work is the DLL file mentioned above, the ESAPI DLL files, and the MathNet.Numerics DLL.

    Copyright (C) 2020 Zackary Thomas Ricci Morelli
    
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.



    I can be contacted at: zackmorelli@gmail.com
                           zackary.t.morelli@lahey.org


*/

namespace VMS.TPS
{
    public class Script  // creates a class called Script within the VMS.TPS Namesapce
    {

       public Script() { }  // instantiates a Script class


        // Global Variable Declaration

       public String pl = null;

      // Execution begins with the "Execute" function.

       // Thread Prog = new Thread(Script());

        public void Execute(ScriptContext context)     // PROGRAM START - sending a return to Execute will end the program
        {

          //  MessageBox.Show("Trig 1");
            //Variable declaration space

            IEnumerable<PlanSum> Plansums = context.PlanSumsInScope;
            IEnumerable<PlanSetup> Plans = context.PlansInScope;

            //  MessageBox.Show("Trig 1");
            if (context.Patient == null)
            {
                MessageBox.Show("Please load a patient with a treatment plan before running this script!");
                return;
            }
            

            //GUI starts here
           //  MessageBox.Show("Trig 4");

            System.Windows.Forms.Application.EnableVisualStyles();
           // System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);      //This method breaks the script when it runs multiple times, because it will throw an exception if a window has already been created.
            Start(Plansums, Plans );                                                           //The Windows .NET documentation specifically says this method should NOT be called in a Windows Forms program hosted in another application, like this, so it is ommitted
                                                                                            //It is a legacy method anyway from eary versions of .NET, there should be no need to call it.

            //Starts GUI for Dose objective check in a separate thread
            //  Thread GUI = new Thread(new ThreadStart(Go));

            // MessageBox.Show("Trig End");


        }

        public static void Start(IEnumerable<PlanSum> Plansums, IEnumerable<PlanSetup> Plans )
        {
            System.Windows.Forms.Application.Run(new NTCPcalc.GUI(Plansums, Plans ));
        }

        //  static void Go (IEnumerable<PlanSum> Plansums, IEnumerable<PlanSetup> Plans, List<string> p1, List<string> p2, List<string> p3, List<string> p4, List<string> p5, List<string> p6, List<string> p7, List<string> p8)
        //  {
        //      System.Windows.Forms.Application.Run(new NTCPcalc.GUI(Plansums, Plans, p1, p2, p3, p4, p5, p6, p7, p8));
        //   }
                     


    }
}