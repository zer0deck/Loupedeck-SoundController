namespace Loupedeck.SoundControllerPlugin
{
    using System;
    using System.Diagnostics;
    using System.IO;

    public partial class SoundControllerPlugin : Plugin
    {

        [Obsolete]
        private void LoadSVCLPathSetting()
        {
            // Try to load setting from plugin info, otherwise user have to add path
            if (this.TryGetPluginSetting("svclPath", out String pathValue))
            {
                if (File.Exists(pathValue))
                {
                    svclPath = pathValue;
                    this.OnPluginStatusChanged(Loupedeck.PluginStatus.Normal, null, null);
                    this.svclPathSetup.ReportLogin(svclPath, svclPath, "");
                }
                else
                {
                    this.OnPluginStatusChanged(
                        Loupedeck.PluginStatus.Error,
                        "Incorrect path. Please use \"PathToSVCLFolder\\svcl.exe\" format and check if path is correct and don't has any special characters.",
                        ""
                        );
                    svclPath = null;
                    this.DeletePluginSetting("svclPath");
                }
            }
            else
            {
                this.OnPluginStatusChanged(
                    Loupedeck.PluginStatus.Error,
                    "Path to svcl.exe is not set; Please enter the path via command below",
                    ""
                    );
            }
        }

        private void OnPathSet(object sender, EventArgs e)
        {
            var p = new Process();
            p.StartInfo.FileName = Path.Combine(pluginDataDirectory, "PathSelector.html");
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();
        }

        [Obsolete]
        private void OnPathUnset(object sender, EventArgs e)
        {
            this.DeletePluginSetting("svclPath");
            svclPath = null;
            this.svclPathSetup.ReportLogout();
            this.OnPluginStatusChanged(
                Loupedeck.PluginStatus.Error,
                "Path to svcl.exe is not set; Please enter the path via command below",
                ""
                );
        }

        private void OnPathReceived(object sender, UrlCallbackReceivedEventArgs e)
        {
            if ((e.Uri != null) && e.Uri.LocalPath.Contains("setPath") && !String.IsNullOrEmpty(e.Uri.Query))
            {
                this.SetPluginSetting("svclPath", e.Uri.Query.Substring(1).Replace("%5C", "/").Replace("%20", " ").Replace("%22", null), false);
                this.LoadSVCLPathSetting();
            }
        }
    }
}
