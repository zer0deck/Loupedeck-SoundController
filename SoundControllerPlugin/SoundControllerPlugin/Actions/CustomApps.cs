namespace Loupedeck.SoundControllerPlugin
{
    using System;
    using System.Diagnostics;
    using System.IO;

    public class CustomVolumeAdjustment : PluginDynamicAdjustment
    {
        private Int32 _counter = new Int32();
        private Process _process = null;
        private Boolean _active = true;
        private readonly String _image0ResourcePath;
        private readonly String _image1ResourcePath;

        /// <summary>
        /// This custom constructor. Data cannot be stored for now. Working on it.
        /// </summary>
        public CustomVolumeAdjustment()
            : base(displayName: "Custom Adjustment", description: "This is custom app volume adjustment. Don't open icon setting otherwise it may cause an error", groupName: "", hasReset: true)
        {
            this._image0ResourcePath = EmbeddedResources.FindFile("MasterOn.png");
            this._image1ResourcePath = EmbeddedResources.FindFile("MasterOff.png");
            this.MakeProfileAction("text;Enter the process name (from TaskManager) or device name (from settings):");
            this._counter = 50;
        }

        protected override void ApplyAdjustment(String actionParameter, Int32 diff)
        {
            if (SoundControllerPlugin.svclPath != null)
            {
                this._counter += diff;
                this._counter = (this._counter < 0) ? 0 : (this._counter > 100) ? 100 : this._counter;

                this._process = new Process();
                this._process.StartInfo.FileName = SoundControllerPlugin.svclPath;
                this._process.StartInfo.Arguments = "/SetVolume " + actionParameter + " " + this._counter.ToString();
                this._process.StartInfo.CreateNoWindow = true;
                this._process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                this._process.Start();
            }

            this.AdjustmentValueChanged();
            this.ActionImageChanged();
        }

        protected override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            if (this._active)
            {
                return EmbeddedResources.ReadImage(this._image0ResourcePath);
            }
            else
            {
                return EmbeddedResources.ReadImage(this._image1ResourcePath);
            }
        }

        protected override void RunCommand(String actionParameter)
        {
            if (SoundControllerPlugin.svclPath != null)
            {
                this._process = new Process();
                this._process.StartInfo.FileName = "powershell.exe";
                this._process.StartInfo.Arguments = "\"" + SoundControllerPlugin.svclPath + "\" /Stdout /GetMute " + actionParameter;
                this._process.StartInfo.CreateNoWindow = true;
                this._process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                this._process.StartInfo.UseShellExecute = false;
                this._process.StartInfo.RedirectStandardOutput = true;
                this._process.Start();
                String output = this._process.StandardOutput.ReadToEnd();
                this._process.WaitForExit();

                this._process = new Process();
                this._process.StartInfo.FileName = SoundControllerPlugin.svclPath;
                if (output[0] == '1')
                {
                    this._process.StartInfo.Arguments = "/Unmute " + actionParameter;
                    this._active = true;
                }
                else if (output[0] == '0')
                {
                    this._process.StartInfo.Arguments = "/Mute " + actionParameter;
                    this._active = false;
                }
                this._process.StartInfo.CreateNoWindow = true;
                this._process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                this._process.Start();
            }

            this.AdjustmentValueChanged();
            this.ActionImageChanged();
        }

        protected override String GetAdjustmentValue(String actionParameter) => this._counter.ToString();

    }

}

