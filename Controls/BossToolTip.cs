﻿using BizHawk.FreeEnterprise.Companion.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BizHawk.FreeEnterprise.Companion.Controls
{
    public class BossToolTip : ToolTip
    {
        public BossToolTip(IContainer container)
            :base(container)
        {
            OwnerDraw = true;            
            Popup += OnPopup;
            Draw += OnDraw;
            
        }

        private RomData? _data;
        private Settings? _settings;

        public void Initialize(RomData romData, Settings settings)
        {
            _data = romData;
            _settings = settings;
        }

        private void OnDraw(object sender, DrawToolTipEventArgs e)
        {
            if (_data == null || _settings == null)
                return;

            _data.Font.RenderBox(e.Graphics, 0, 0, 24, e.Bounds.Height / _settings.TileSize);
            var lineNum = 2;

            foreach (var line in (Description ?? string.Empty).Split('\n'))
                _data.Font.RenderText(e.Graphics, e.Bounds.Width / 2 - (line.Length * _settings.TileSize) / 2, _settings.TileSize * lineNum++, 22, line, Sprites.TextMode.Normal, 0);
        }

        private void OnPopup(object sender, PopupEventArgs e)
        {
            if (_data == null || _settings == null)
                return;
            e.ToolTipSize = new Size(_settings.Scale(192), _settings.TileSize * (4 + (Description ?? string.Empty).Split('\n').Count()));
        }

        public string? Description { get; set; }
    }
}
