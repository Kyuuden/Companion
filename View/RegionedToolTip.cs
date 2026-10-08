using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace FF.Rando.Companion.View;
public class RegionedToolTip
{
    private Region? _lastRegion = null;
    private readonly Control _parent;
    private readonly ToolTip _toolTip = new() { ShowAlways = true };
    private readonly List<Region> _regions = [];

    private record Region(Rectangle Area, string Tooltip);

    public RegionedToolTip(Control parent)
    {
        _parent = parent;
        _parent.MouseMove += MouseMove;
    }

    private void MouseMove(object sender, MouseEventArgs e)
    {
        Point? pt = _parent.PointToClient(Cursor.Position);

        switch (_parent)
        {
            case PictureBox pb when pb.SizeMode == PictureBoxSizeMode.Zoom:
                pt = GetImagePointFromZoom(pb, pt.Value);
                break;
        }

        if (!pt.HasValue)
            return;

        foreach (var region in _regions)
        {
            if (region.Area.Contains(pt.Value))
            {
                if (_lastRegion == region)
                    return;

                _lastRegion = region;
                _toolTip.SetToolTip(_parent, region.Tooltip);
                return;
            }
        }

        _toolTip.SetToolTip(_parent, null);
        _lastRegion = null;
    }

    private static Point? GetImagePointFromZoom(PictureBox pb, Point client)
    {
        if (pb.Image == null) return null;

        // Get the ratios of image size vs control size
        float ratioX = (float)pb.ClientSize.Width / pb.Image.Width;
        float ratioY = (float)pb.ClientSize.Height / pb.Image.Height;

        // Zoom mode uses the smaller ratio to fit the image without stretching
        float zoomScale = Math.Min(ratioX, ratioY);

        // Calculate the actual size of the displayed image inside the PictureBox
        float displayedWidth = pb.Image.Width * zoomScale;
        float displayedHeight = pb.Image.Height * zoomScale;

        // Calculate the blank offset padding (top/bottom or sides)
        float offsetX = (pb.ClientSize.Width - displayedWidth) / 2f;
        float offsetY = (pb.ClientSize.Height - displayedHeight) / 2f;

        // Subtract the offset from the mouse position, then scale it back to original pixels
        int imgX = (int)((client.X - offsetX) / zoomScale);
        int imgY = (int)((client.Y - offsetY) / zoomScale);

        // Validate that the click actually fell within the image bounds
        if (imgX >= 0 && imgX < pb.Image.Width && imgY >= 0 && imgY < pb.Image.Height)
        {
            return new Point(imgX, imgY);
        }

        return null; // Clicked on the empty padding area
    }

    public void ClearRegions()
    {
        _regions.Clear();
    }

    public void AddRegion(Rectangle area, string tooltip)
    {
        _regions.Add(new Region(area, tooltip));
    }
}
