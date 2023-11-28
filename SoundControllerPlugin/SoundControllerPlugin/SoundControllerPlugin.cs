namespace Loupedeck.SoundControllerPlugin
{
    using System;
    using System.IO;


    public partial class SoundControllerPlugin : Plugin
    {
        private readonly PluginPreferenceAccount svclPathSetup;
        public static String svclPath { get; set; } = null;
        public static String pluginDataDirectory { get; set; } = null;

        public override Boolean UsesApplicationApiOnly => true;
        public override Boolean HasNoApplication => true;

        public SoundControllerPlugin()
        {
            PluginResources.Init(this.Assembly);

            this.svclPathSetup = new PluginPreferenceAccount("svcl-path")
            {
                DisplayName = "SVCL path",
                IsRequired = true,
                LoginUrlTitle = "Set path to svcl folder",
                LogoutUrlTitle = "Change path to svcl folder",
            };
            this.PluginPreferences.Add(this.svclPathSetup);
            pluginDataDirectory = this.GetPluginDataDirectory();
        }

        [Obsolete]
        public override void Load()
        {
            this.SetPluginIcons();
            this.CopyPluginData();
            this.LoadSVCLPathSetting();
            this.svclPathSetup.LoginRequested += this.OnPathSet;
            this.svclPathSetup.LogoutRequested += this.OnPathUnset;

            this.ServiceEvents.UrlCallbackReceived += this.OnPathReceived;
        }

        public override void Unload()
        {
            this.svclPathSetup.LoginRequested -= this.OnPathSet;
            this.svclPathSetup.LogoutRequested -= this.OnPathUnset;

            this.ServiceEvents.UrlCallbackReceived -= this.OnPathReceived;
        }

        public void SetPluginIcons()
        {
            this.Info.Icon16x16 = EmbeddedResources.ReadImage("Loupedeck.SoundControllerPlugin.metadata.Icon16x16.png");
            this.Info.Icon32x32 = EmbeddedResources.ReadImage("Loupedeck.SoundControllerPlugin.metadata.Icon32x32.png");
            this.Info.Icon48x48 = EmbeddedResources.ReadImage("Loupedeck.SoundControllerPlugin.metadata.Icon48x48.png");
            this.Info.Icon256x256 = EmbeddedResources.ReadImage("Loupedeck.SoundControllerPlugin.metadata.Icon256x256.png");
        }

    }
}
