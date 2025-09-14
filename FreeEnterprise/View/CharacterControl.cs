using System.ComponentModel;
using System.Drawing;
using FF.Rando.Companion.FreeEnterprise.Settings;
using FF.Rando.Companion.View;

namespace FF.Rando.Companion.FreeEnterprise.View;

public partial class CharacterControl : ImageControl<ISeed, ICharacter>
{
    private readonly PartySettings _settings;

    public CharacterControl(ISeed seed, PartySettings settings, ICharacter character)
        : base(seed, settings, character)
    {
        Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
        _settings = settings;
    }

    protected override void Value_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.Value_PropertyChanged(sender, e);
        switch (e.PropertyName)
        {
            case (nameof(ICharacter.IsAnchor)):
                // _anchor.Visible = _character?.IsAnchor ?? false;
                break;
        }
    }

    protected override void UpdateImage()
    {
        MinimumSize = Value.Image.Size;
        Image = Value.Image;
        Size = Settings.Scale(new Size(48, 48));
    }

    public override void Rescale()
    {
        Size = Settings.Scale(new Size(48, 48));
    }
}
