namespace Loupedeck.SoundControllerPlugin
{
    public class MasterVolume : BaseVolumeAdjustment
    {
        public MasterVolume()
            : base(
                  displayName: "Master Volume",
                  description: "Changes System Volume (ONLY IF WINDOWS USES RUSSIAN)",
                  groupName: "ReadyToGo",
                  processname: "Динамики",
                  image0ResourceName: "MasterOn.png",
                  image1ResourceName: "MasterOff.png"
                  )
        {
        }

    }

    public class MicVolume : BaseVolumeAdjustment
    {
        public MicVolume()
            : base(
                  displayName: "Mic Volume",
                  description: "Changes Mic Volume",
                  groupName: "ReadyToGo",
                  processname: "Microphone",
                  image0ResourceName: "MicOn.png",
                  image1ResourceName: "MicOff.png"
                  )
        {
        }

    }

    public class ChromeVolume : BaseVolumeAdjustment
    {
        public ChromeVolume()
            : base(
                  displayName: "Chrome Volume",
                  description: "Changes Chrome Volume",
                  groupName: "ReadyToGo",
                  processname: "chrome.exe",
                  image0ResourceName: "ChromeOn.png",
                  image1ResourceName: "ChromeOff.png"
                  )
        {
        }

    }

    public class iTunesVolume : BaseVolumeAdjustment
    {
        public iTunesVolume()
            : base(
                  displayName: "iTunes Volume",
                  description: "Changes iTunes Volume",
                  groupName: "ReadyToGo",
                  processname: "iTunes.exe",
                  image0ResourceName: "iTunesOn.png",
                  image1ResourceName: "iTunesOff.png"
                  )
        {
        }

    }

    public class TelegramVolume : BaseVolumeAdjustment
    {
        public TelegramVolume()
            : base(
                  displayName: "Telegram Volume",
                  description: "Changes Telegram Volume",
                  groupName: "ReadyToGo",
                  processname: "Telegram.exe",
                  image0ResourceName: "TelegramOn.png",
                  image1ResourceName: "TelegramOff.png"
                  )
        {
        }

    }

    public class VLCVolume : BaseVolumeAdjustment
    {
        public VLCVolume()
            : base(
                  displayName: "VLC Volume",
                  description: "Changes VLC Volume",
                  groupName: "ReadyToGo",
                  processname: "vlc.exe",
                  image0ResourceName: "VLCOn.png",
                  image1ResourceName: "VLCOff.png"
                  )
        {
        }

    }


    public class DiscordVolume : BaseVolumeAdjustment
    {
        public DiscordVolume()
            : base(
                  displayName: "Discord Volume",
                  description: "Changes Discord Volume",
                  groupName: "ReadyToGo",
                  processname: "Discord.exe",
                  image0ResourceName: "DiscordOn.png",
                  image1ResourceName: "DiscordOff.png"
                  )
        {
        }

    }

    public class ZoomVolume : BaseVolumeAdjustment
    {
        public ZoomVolume()
            : base(
                  displayName: "Zoom Volume",
                  description: "Changes Zoom Volume",
                  groupName: "ReadyToGo",
                  processname: "Zoom.exe",
                  image0ResourceName: "ZoomOn.png",
                  image1ResourceName: "ZoomOff.png"
                  )
        {
        }

    }

    public class ApexVolume : BaseVolumeAdjustment
    {
        public ApexVolume()
            : base(
                  displayName: "Apex Volume",
                  description: "Changes Apex Legends Volume",
                  groupName: "ReadyToGo",
                  processname: "r5apex.exe",
                  image0ResourceName: "ApexOn.png",
                  image1ResourceName: "ApexOff.png"
                  )
        {
        }

    }

    public class SystemMessagesVolume : BaseVolumeAdjustment
    {
        public SystemMessagesVolume()
            : base(
                  displayName: "System Notifications Volume",
                  description: "Changes System Notifications Volume (ONLY IF WINDOWS USES RUSSIAN)",
                  groupName: "ReadyToGo",
                  processname: "Системные звуки",
                  image0ResourceName: "NotificationsOn.png",
                  image1ResourceName: "NotificationsOff.png"
                  )
        {
        }

    }
}
