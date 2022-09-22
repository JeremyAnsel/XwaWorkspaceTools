using JeremyAnsel.Xwa.Workspace;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XwaWorkspaceViewer
{
    public static class AppSettings
    {
        public static string WorkingDirectory { get; private set; }

        public static XwaWorkspace Workspace { get; private set; }

        public static void SetData(string directory)
        {
            if (!Directory.Exists(directory))
            {
                return;
            }

            WorkingDirectory = directory;
            Workspace = new XwaWorkspace(WorkingDirectory);
        }
    }
}
