using BizHawk.FreeEnterprise.Companion.Extensions;
using BizHawk.FreeEnterprise.Companion.Sprites;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BizHawk.FreeEnterprise.Companion.Configuration
{
    public class Settings
    {
        public Layout Layout { get; set;}
        public int ViewScale { get; set; }
        public bool IconScaling { get; set; }
        public InterpolationMode InterpolationMode { get; set; }
        public bool BordersEnabled { get; set; }
        public bool Dock { get; set; }
        public int DockOffset { get; set; }
        public DockSide DockSide { get; set; }
        public AspectRatio AspectRatio { get; set; }
        public int RefreshEveryNFrames { get; set; }
        public bool KeyItemsDisplay { get; set; }
        public KeyItemStyle KeyItemsStyle { get; set; }
        public bool PartyDisplay { get; set; }
        public Pose PartyPose { get; set; }
        public bool PartyAnimate { get; set; }
        public bool PartyShowAnchor { get; set; }
        public bool ObjectivesDisplay { get; set; }
        public bool BossesDisplay { get; set; }
        public bool LocationsDisplay { get; set; }
        public bool LocationsShowKeyItems { get; set; }
        public bool LocationsShowCharacters { get; set; }
        public bool KeyItemEventEnabled { get; set; }
        public bool KeyItemBonkDefaultText { get; set; }
        public string KeyItemBonkCustomText { get; set; }
        public TimeFormat TimeFormat { get; set; }

        public bool Maximized { get; set; }
        public bool Minimized { get; set; }
        public System.Drawing.Point Location { get; set; }
        public System.Drawing.Size Size { get; set; }

        [JsonIgnore]
        public int TileSize => Scale(8);

        [JsonIgnore]
        public float TileSizeF => ScaleF(8);

        [JsonIgnore]
        public double ViewScaleF => ViewScale / 100.0;

        [JsonIgnore]
        public string TimeFormatString => TimeFormat.GetDescription().Replace(":", "':'").Replace(".", "'.'");

        public int Scale(int size)
            => (int)(size * ViewScaleF);

        public float ScaleF(float size)
            => (float)(size * ViewScaleF);

        public Size Scale(Size size)
            => new Size((int)(size.Width * ViewScaleF), (int)(size.Height * ViewScaleF));

        public int SetToTileInterval(int size)
            => (int)Math.Ceiling(size / TileSizeF) * TileSize;

        public Settings()
        {
            Layout = Layout.Alternate;
            ViewScale = 100;
            IconScaling = true;
            InterpolationMode = InterpolationMode.Bicubic;
            BordersEnabled = true;
            Dock = false;
            DockOffset = -16;
            DockSide = DockSide.Right;
            AspectRatio = AspectRatio._16x9;
            RefreshEveryNFrames = 60;
            KeyItemsDisplay = true;
            KeyItemsStyle = KeyItemStyle.Text;
            PartyDisplay = true;
            PartyPose = Pose.Stand;
            PartyAnimate = true;
            PartyShowAnchor = false;
            ObjectivesDisplay = true;
            BossesDisplay = false;
            LocationsDisplay = true;
            LocationsShowKeyItems = true;
            LocationsShowCharacters = true;
            KeyItemEventEnabled = true;
            KeyItemBonkDefaultText = true;
            KeyItemBonkCustomText = string.Empty;
            TimeFormat = TimeFormat.HHMMSS;
            Location = new System.Drawing.Point(0, 0);
            Size = new System.Drawing.Size(450, 800);
        }
    }
}
