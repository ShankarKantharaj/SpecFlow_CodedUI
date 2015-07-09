using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using FreshCodedUIProject._01_Manager_Tier.EnvironmentFiles;

namespace FreshCodedUIProject._02_Utility_Tier
{
    class CommonFunctions
    {
        public static void setRelativePath()
        {
            string strProjectName = Assembly.GetExecutingAssembly().GetName().Name;
            DirectoryInfo oDirInfo = Directory.GetParent(Directory.GetCurrentDirectory());
            GlbVar.strRelativePath = oDirInfo.Parent.Parent.FullName + GlbVar.sysFileSeperator + strProjectName + GlbVar.sysFileSeperator;
            Console.WriteLine(GlbVar.strRelativePath);

        }
    }
}
