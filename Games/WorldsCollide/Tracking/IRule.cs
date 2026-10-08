namespace FF.Rando.Companion.Games.WorldsCollide.Tracking;

internal interface IRule
{
    bool IsActive(State state);
}