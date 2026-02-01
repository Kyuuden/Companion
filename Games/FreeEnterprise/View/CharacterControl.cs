using FF.Rando.Companion.Games.FreeEnterprise;
using FF.Rando.Companion.Games.FreeEnterprise.Settings;
using FF.Rando.Companion.View;
using System.ComponentModel;
using System.Drawing;

namespace FF.Rando.Companion.Games.FreeEnterprise.View;

public partial class CharacterControl : ImageControl<ISeed, ICharacter>
{
    private readonly Size _defaultSize = new(48, 48);

    public CharacterControl(ISeed seed, PartySettings settings, ICharacter character)
        : base(seed, settings, character)
    {
        Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
    }

    protected override void Value_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.Value_PropertyChanged(sender, e);
        switch (e.PropertyName)
        {
            case nameof(ICharacter.IsAnchor):
                // _anchor.Visible = _character?.IsAnchor ?? false;
                break;
        }
    }

    protected override void UpdateImage()
    {
        MinimumSize = Value.Image.Size;
        Image = Value.Image;
        Size = _defaultSize.Scale(Settings.ScaleFactor);
    }

    public override void Rescale()
    {
        Size = _defaultSize.Scale(Settings.ScaleFactor);
    }
}
