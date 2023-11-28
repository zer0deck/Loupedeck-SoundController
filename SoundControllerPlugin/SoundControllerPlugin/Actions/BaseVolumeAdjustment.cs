namespace Loupedeck.SoundControllerPlugin
{
    using System;
    using System.Diagnostics;
    using System.IO;

    public abstract class BaseVolumeAdjustment : PluginDynamicAdjustment
    {
        private Int32 _counter = new Int32();
        private Process _process = null;
        private Boolean _active = true;
        private String _processname = null;
        private readonly String _image0ResourcePath;
        private readonly String _image1ResourcePath;

        ///<summary>
        ///This is base parent constructor
        ///</summary>
        /// <param name="displayName">Name of function displayed in Loupedeck software (can differ from C# name) <see cref="https://loupedeck.github.io/Add-a-simple-command/"/></param>
        /// <param name="description">Description displayed in the Loupedeck software</param>
        /// <param name="groupName">Helps to group items in folders</param>
        /// <param name="processname">The name of the process or device for which you want to configure the sound. You can get the process name in the Task Manager from the "Process name" field (usually you need to expand the task). The device name can be obtained from Device Manager. More information about titles and names can be found in the NirSoft documentation <see cref="https://www.nirsoft.net/utils/sound_volume_command_line.html"/>.</param>
        /// <param name="image0ResourceName">Icon(On) name from folder Icons (ICON MUST BE IN "Embedded Resource" MODE).</param>
        /// <param name="image0ResourceName">Icon(Off) name from folder Icons (ICON MUST BE IN "Embedded Resource" MODE).</param>
        /// <returns></returns>
        public BaseVolumeAdjustment(string displayName, string description, string groupName, string processname, string image0ResourceName = "MasterOn.png", string image1ResourceName = "MasterOff.png")
            : base(displayName: displayName, description: description, groupName: groupName, hasReset: true)
        {
            this._image0ResourcePath = EmbeddedResources.FindFile(image0ResourceName);
            this._image1ResourcePath = EmbeddedResources.FindFile(image1ResourceName);
            this._processname = processname;
        }

        /// <summary>
        /// Data can be changed to store data in different way. Now I'm using default API option and separately for every adjustment. Not the best decision as I think.
        /// </summary>
        /// <returns></returns>
        protected override Boolean OnLoad()
        {
            if (IoHelpers.EnsureDirectoryExists(SoundControllerPlugin.pluginDataDirectory))
            {
                var filePath = Path.Combine(SoundControllerPlugin.pluginDataDirectory, this._processname + "StoredData.bin");
                if (!IoHelpers.FileExists(filePath))
                {
                    using (StreamWriter streamWriter = new StreamWriter(filePath))
                    {
                        streamWriter.Write("100");
                    }
                }
                StreamReader streamReader = new StreamReader(filePath);
                this._counter = Int32.Parse(streamReader.ReadToEnd());
            }
            this.ApplyAdjustment(null, 0);
            return base.OnLoad();
        }

        protected override Boolean OnUnload()
        {
            if (IoHelpers.EnsureDirectoryExists(SoundControllerPlugin.pluginDataDirectory))
            {
                var filePath = Path.Combine(SoundControllerPlugin.pluginDataDirectory, this._processname + "StoredData.bin");
                using (StreamWriter streamWriter = new StreamWriter(filePath))
                {
                    streamWriter.Write(this._counter.ToString());
                }
            }
            return base.OnUnload();
        }


        /// <summary>
        /// This is changing function. It calls svcl in hidden style and applies volume.
        /// </summary>
        protected override void ApplyAdjustment(String actionParameter, Int32 diff)
        {
            if (SoundControllerPlugin.svclPath != null)
            {
                this._counter += diff;
                this._counter = (this._counter < 0) ? 0 : (this._counter > 100) ? 100 : this._counter;

                this._process = new Process();
                this._process.StartInfo.FileName = SoundControllerPlugin.svclPath;
                this._process.StartInfo.Arguments = "/SetVolume " + this._processname + " " + this._counter.ToString();
                this._process.StartInfo.CreateNoWindow = true;
                this._process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                this._process.Start();
            }

            this.AdjustmentValueChanged();
            this.ActionImageChanged();
        }

        /// <summary>
        /// Default realization. More on <see cref="https://loupedeck.github.io/Change-a-button-image/"/>
        /// </summary>
        /// <param name="actionParameter"></param>
        /// <param name="imageSize"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Same as BaseVolumeAdjustment.ApplyAdjustment, but this time use powershell capsule because svcl stdout supports powershell only. Gets Mute status and switches if possible. Image also switches.
        /// </summary>
        /// <param name="actionParameter"></param>
        protected override void RunCommand(String actionParameter)
        {
            if (SoundControllerPlugin.svclPath != null)
            {
                this._process = new Process();
                this._process.StartInfo.FileName = "powershell.exe";
                this._process.StartInfo.Arguments = "\"" + SoundControllerPlugin.svclPath + "\" /Stdout /GetMute " + this._processname;
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
                    this._process.StartInfo.Arguments = "/Unmute " + this._processname;
                    this._active = true;
                }
                else if (output[0] == '0')
                {
                    this._process.StartInfo.Arguments = "/Mute " + this._processname;
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

