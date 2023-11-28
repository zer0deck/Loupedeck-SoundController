namespace Loupedeck.SoundControllerPlugin
{
    using System;
    using System.IO;

    public partial class SoundControllerPlugin : Plugin
    {
        public override Boolean Install()
        {
            if (!IoHelpers.EnsureDirectoryExists(pluginDataDirectory))
            {
                Tracer.Error("Plugin data is not created. Cannot continue installation");
                return false;
            }

            var filePathHTML = Path.Combine(pluginDataDirectory, "PathSelector.html");
            var filePathIcon = Path.Combine(pluginDataDirectory, "Icon256x256.png");

            this.Assembly.ExtractFile("Loupedeck.SoundControllerPlugin.Resources.PathSelector.html", filePathHTML);
            this.Assembly.ExtractFile("Loupedeck.SoundControllerPlugin.metadata.Icon256x256.png", filePathIcon);

            return true;
        }

        public void CopyPluginData()
        {
            var filePathHTML = Path.Combine(pluginDataDirectory, "PathSelector.html");
            var filePathIcon = Path.Combine(pluginDataDirectory, "Icon256x256.png");
            this.Assembly.ExtractFile("Loupedeck.SoundControllerPlugin.Resources.PathSelector.html", filePathHTML);
            this.Assembly.ExtractFile("Loupedeck.SoundControllerPlugin.metadata.Icon256x256.png", filePathIcon);
        }

    }
}
