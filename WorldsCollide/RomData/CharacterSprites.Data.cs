using FF.Rando.Companion.WorldsCollide.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Rando.Companion.WorldsCollide.Rendering;

public partial class CharacterSprites
{
    private static IEnumerable<CharacterPose> GetPoseDataFor(CharacterEx character)
    {
        switch (character)
        {
            case CharacterEx.Terra:
                foreach (var p in DefaultPCPoses())
                    yield return p.OffsetFor(character, 0);
                yield return new CharacterPose { Pose = Pose.Timid, SpriteSize = new Size(2, 3), Character = CharacterEx.Terra, TileIndicies = [157, 158, 159, 160, 161, 162] };
                yield return new CharacterPose { Pose = Pose.HairFlowing1, SpriteSize = new Size(3, 3), Character = CharacterEx.Terra, TileIndicies = [6504, 6505, 6512, 6506, 6507, 6514, 0, 0, 6513] };
                yield return new CharacterPose { Pose = Pose.HairFlowing2, SpriteSize = new Size(3, 3), Character = CharacterEx.Terra, TileIndicies = [6508, 6509, 6516, 6510, 6511, 6518, 0, 0, 6517] };
                break;
            case CharacterEx.Locke:
                foreach (var p in DefaultPCPoses())
                    yield return p.OffsetFor(character, 181);
                break;
            case CharacterEx.Cyan:
                foreach (var p in DefaultPCPoses())
                    yield return p.OffsetFor(character, 362);
                break;
            case CharacterEx.Shadow:
                foreach (var p in DefaultPCPoses())
                    yield return p.OffsetFor(character, 543);
                break;
            case CharacterEx.Edgar:
            case CharacterEx.Gerad:
                foreach (var p in DefaultPCPoses())
                    yield return p.OffsetFor(character, 724);
                break;
            case CharacterEx.Sabin:
            case CharacterEx.ShadowyCharacter:
                foreach (var p in DefaultPCPoses())
                    yield return p.OffsetFor(character, 905);
                break;
            case CharacterEx.Celes:
                foreach (var p in DefaultPCPoses())
                    yield return p.OffsetFor(character, 1086);
                yield return new CharacterPose { Character = character, Pose = Pose.Chained, SpriteSize = new Size(2, 3), TileIndicies = [5811, 5812, 5813, 5814, 5815, 5816] };
                break;
            case CharacterEx.Strago:
                foreach (var p in DefaultPCPoses())
                    yield return p.OffsetFor(character, 1267);
                break;
            case CharacterEx.Relm:
                foreach (var p in DefaultPCPoses())
                    yield return p.OffsetFor(character, 1448);
                break;
            case CharacterEx.Setzer:
                foreach (var p in DefaultPCPoses())
                    yield return p.OffsetFor(character, 1629);
                break;
            case CharacterEx.Mog:
                foreach (var p in DefaultPCPoses())
                    yield return p.OffsetFor(character, 1810);
                break;
            case CharacterEx.Gau:
                foreach (var p in DefaultPCPoses())
                    yield return p.OffsetFor(character, 1991);
                break;
            case CharacterEx.Gogo:
                foreach (var p in DefaultPCPoses())
                    yield return p.OffsetFor(character, 2172);
                break;
            case CharacterEx.Umaro:
                foreach (var p in DefaultPCPoses())
                    yield return p.OffsetFor(character, 2353);
                break;
            case CharacterEx.ImperialCadet:
            case CharacterEx.ImperialSoldier:
            case CharacterEx.ImperialCorporal:
            case CharacterEx.ImperialCommander:
                foreach (var p in DefaultPCPoses())
                    yield return p.OffsetFor(character, 2534);
                break;
            case CharacterEx.Imp:
                foreach (var p in DefaultPCPoses())
                    yield return p.OffsetFor(character, 2715);
                break;
            case CharacterEx.GeneralLeo:
                foreach (var p in DefaultPCPoses().Where(pc => pc.Pose is not Pose.JumpRide1 and not Pose.JumpRide2))
                    yield return p.OffsetFor(character, 2896);
                break;
            case CharacterEx.DuncanHarcourt:
            case CharacterEx.Bannon:
                foreach (var p in DefaultPCPoses().Where(pc => pc.Pose is not Pose.JumpRide1 and not Pose.JumpRide2))
                    yield return p.OffsetFor(character, 3067);
                break;
            case CharacterEx.EsperTerra:
                foreach (var p in DefaultPCPoses().Where(pc => pc.Pose is not Pose.JumpRide1 and not Pose.JumpRide2))
                    yield return p.OffsetFor(character, 3238);
                break;
            case CharacterEx.DomaGuard:
            case CharacterEx.Merchant:
            case CharacterEx.DracosMen:
                foreach (var p in DefaultPCPoses().Where(pc => pc.Pose is not Pose.JumpRide1 and not Pose.JumpRide2))
                    yield return p.OffsetFor(character, 3409);
                break;
            case CharacterEx.Ghost:
                foreach (var p in DefaultPCPoses().Where(pc => pc.Pose is not Pose.JumpRide1 and not Pose.JumpRide2))
                    yield return p.OffsetFor(character, 3580);
                break;
            case CharacterEx.Kefka:
                foreach (var p in DefaultPCPoses().Where(pc => pc.Pose is not Pose.JumpRide1 and not Pose.JumpRide2))
                    yield return p.OffsetFor(character, 3751);
                break;
            case CharacterEx.EmperorGestahl:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 3922);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Sad, TileIndicies = [3967, 3968, 3969, 3970, 3930, 3931] };
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Dead, TileIndicies = Enumerable.Range(3971, 6).ToList() };
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Salute, TileIndicies = [3965, 3923, 3966, 3925, 3964, 3931] };
                break;
            case CharacterEx.GausFather:
            case CharacterEx.Gungho:
            case CharacterEx.ThamasaMayor:
            case CharacterEx.NarsheElder:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 3977);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Sad, TileIndicies = [4022, 4023, 4024, 4025, 3985, 3986] };
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Salute, TileIndicies = [4020, 3978, 4021, 3980, 4019, 3986] };
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Surprised, TileIndicies = Enumerable.Range(4026, 6).ToList() };
                break;
            case CharacterEx.Auctioneer:
            case CharacterEx.Duane:
            case CharacterEx.YoungMan:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 4032);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Sad, TileIndicies = [4077, 4078, 4079, 4080, 4040, 4041] };
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Salute, TileIndicies = [4075, 4033, 4076, 4039, 4074, 4041] };
                break;
            case CharacterEx.Doberman:
            case CharacterEx.Interceptor:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 4087);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Sit, TileIndicies = Enumerable.Range(4130, 6).ToList() };
                break;
            case CharacterEx.Maria:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 4142);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Sing, TileIndicies = Enumerable.Range(4188, 6).ToList() };
                break;
            case CharacterEx.Maestro:
            case CharacterEx.Scholar:
            case CharacterEx.PrinceRalse:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 4198);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Sing, TileIndicies = Enumerable.Range(4244, 6).ToList() };
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Salute, TileIndicies = [4250, 4251, 4252,4253, 4206, 4207 ] };
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Dead, TileIndicies = Enumerable.Range(4237, 6).ToList() };
                break;
            case CharacterEx.Draco:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 4254);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Sad, TileIndicies = [4306, 4307, 4308, 4309, 4302, 4303] };
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Dead, TileIndicies = Enumerable.Range(4294, 6).ToList() };
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Sing, TileIndicies = Enumerable.Range(4300, 6).ToList() };
                break;
            case CharacterEx.Arvis:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 4310);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Sad, TileIndicies = Enumerable.Range(4350, 6).ToList() };
                break;
            case CharacterEx.Returner:
            case CharacterEx.AirshipCrew:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 4356);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Dead, TileIndicies = Enumerable.Range(4396, 6).ToList() };
                break;
            case CharacterEx.Ultros:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 4402);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Surprised, TileIndicies = Enumerable.Range(4442, 6).ToList() };
                break;
            case CharacterEx.MyFairGau:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 4448);

                break;
            case CharacterEx.SouthFigaroDancer:
            case CharacterEx.YoungWoman:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 4494);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Dance, TileIndicies = Enumerable.Range(4534, 6).ToList() };
                break;
            case CharacterEx.FigaroChancellor:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 4540);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Surprised, TileIndicies = Enumerable.Range(4580, 6).ToList() };
                break;
            case CharacterEx.Clyde:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 4586);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.LookLeft, TileIndicies = Enumerable.Range(4626, 6).ToList() };
                break;
            case CharacterEx.OldWoman1:
            case CharacterEx.OldWoman2:
            case CharacterEx.FigaroMatron:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 4632);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Surprised, TileIndicies = Enumerable.Range(4672, 6).ToList() };
                break;
            case CharacterEx.Aisha:
            case CharacterEx.YoungWoman1:
            case CharacterEx.YoungWoman2:
            case CharacterEx.YoungWoman3:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 4678);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Dead, TileIndicies = Enumerable.Range(4718, 6).ToList() };
                break;
            case CharacterEx.Owain:
            case CharacterEx.YoungBoy1:
            case CharacterEx.YoungBoy2:
            case CharacterEx.YoungBoy3:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 4724);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Dead, TileIndicies = Enumerable.Range(4764, 6).ToList() };
                break;
            case CharacterEx.YoungGirl1:
            case CharacterEx.YoungGirl2:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 4782);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Surprised, TileIndicies = Enumerable.Range(4810, 6).ToList() };
                break;
            case CharacterEx.Rachel:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 4862);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Sad, TileIndicies = Enumerable.Range(4902, 6).ToList() };
                break;
            case CharacterEx.Katarin:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 4908);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Sad, TileIndicies = Enumerable.Range(4948, 6).ToList() };
                break;
            case CharacterEx.OperaImpresario:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 4954);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.FacePalm, TileIndicies = Enumerable.Range(4994, 6).ToList() };
                break;
            case CharacterEx.EsperElder:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 5000);
                break;
            case CharacterEx.Yura:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 5046);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Dead, TileIndicies = Enumerable.Range(5086, 6).ToList() };
                break;
            case CharacterEx.Sigfried:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 5092);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Surprised, TileIndicies = Enumerable.Range(5132, 6).ToList() };
                break;
            case CharacterEx.Cid:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 5138);

                break;
            case CharacterEx.Maduin:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 5184);
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Dead, TileIndicies = Enumerable.Range(5224, 6).ToList() };
                break;
            case CharacterEx.Baram:
            case CharacterEx.Pirate:
            case CharacterEx.TzenThief:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 5230);
                break;
            case CharacterEx.Vargas:
            case CharacterEx.Dadaluma:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 5270);

                break;
            case CharacterEx.SrBehemoth:
            case CharacterEx.Vomammoth:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 5310);

                break;
            case CharacterEx.NarsheGuard:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 5350);

                break;
            case CharacterEx.TrainConductor:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 5390);

                break;
            case CharacterEx.Shopkeeper:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 5430);

                break;
            case CharacterEx.FairyEsper:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 5470);

                break;
            case CharacterEx.WolfEsper:
            case CharacterEx.LoneWolf:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 5510);

                break;
            case CharacterEx.StormDragon:
            case CharacterEx.GoldDragon:
            case CharacterEx.DirtDragon:
            case CharacterEx.WhiteDragon:
            case CharacterEx.RedDragon:
            case CharacterEx.IceDragon:
            case CharacterEx.BlueDragon:
            case CharacterEx.SkullDragon:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 5550);
                break;
            case CharacterEx.OperaGuard:
            case CharacterEx.FigaroGuard:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 5630);

                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.JumpRide1, TileIndicies = Enumerable.Range(5801, 6).ToList() };
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Dead, TileIndicies = Enumerable.Range(5913, 6).ToList() };
                break;
            case CharacterEx.Daryl:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 5670);

                break;
            case CharacterEx.Hidon:
            case CharacterEx.Chupon:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 5710);
                break;
            case CharacterEx.ImperialElite:
            case CharacterEx.Cultist:
            case CharacterEx.Wrexsoul:
                foreach (var pose in DefaultNpcPoses())
                    yield return pose.OffsetFor(character, 5750);

                break;
            case CharacterEx.Ramuh:
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Stand, TileIndicies = Enumerable.Range(5790, 6).ToList() };
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Salute, TileIndicies = [5796, 5791, 5797, 5793, 5798, 5795] };
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Sad, TileIndicies = [5799, 5800, 5792, 5793, 5794, 5795] };
                break;
            case CharacterEx.DomaKing:
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Dead, TileIndicies = Enumerable.Range(5829, 6).ToList() };
                break;
            case CharacterEx.Inferno:
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Stand, TileIndicies = Enumerable.Range(5835, 6).ToList() };
                break;
            case CharacterEx.Ifrit:
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Stand, TileIndicies = Enumerable.Range(5853, 6).ToList() };
                break;
            case CharacterEx.Phantom:
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Stand, TileIndicies = Enumerable.Range(5859, 6).ToList() };
                break;
            case CharacterEx.Shiva:
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Stand, TileIndicies = Enumerable.Range(5865, 6).ToList() };
                break;
            case CharacterEx.Unicorn:
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Stand, TileIndicies = Enumerable.Range(5871, 6).ToList() };
                break;
            case CharacterEx.Bismarck:
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Stand, TileIndicies = Enumerable.Range(5877, 6).ToList() };
                break;
            case CharacterEx.Carbuncle:
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Stand, TileIndicies = Enumerable.Range(5883, 6).ToList() };
                break;
            case CharacterEx.Shoat:
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Stand, TileIndicies = Enumerable.Range(5889, 6).ToList() };
                break;
            case CharacterEx.Owzer:
                yield return new CharacterPose
                {
                    Character = character,
                    SpriteSize = new Size(4, 3),
                    Pose = Pose.Stand,
                    TileIndicies =
                    [
                        5895, 5896,5901,5902,
                        5897, 5898, 5903, 5904,
                        5899,5900,5905,5906
                    ]
                };
                break;
            case CharacterEx.Number024:
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Stand, TileIndicies = Enumerable.Range(5919, 6).ToList() };
                break;
            case CharacterEx.AtmaWeapon:
                yield return new CharacterPose { Character = character, SpriteSize = new Size(2, 3), Pose = Pose.Stand, TileIndicies = Enumerable.Range(5937, 6).ToList() };
                break;
            case CharacterEx.Tritoch:
                yield return new CharacterPose { Character = character, SpriteSize = new Size(4, 4), Pose = Pose.Stand, TileIndicies = Enumerable.Range(6408, 16).ToList() };
                break;
            case CharacterEx.Odin:
                yield return new CharacterPose { Character = character, SpriteSize = new Size(4, 4), Pose = Pose.Stand, TileIndicies = Enumerable.Range(6424, 16).ToList() };
                break;

                //case CharacterEx.Chocobo:
                //    yield return new ActorPose { Pose = Pose.WalkAway1, TileIndicies = [], SpriteSize = new Size(4, 4) };
                //    yield return new ActorPose { Pose = Pose.WalkAway2, TileIndicies = [], SpriteSize = new Size(4, 4) };
                //    yield return new ActorPose { Pose = Pose.StandAway, TileIndicies = [], SpriteSize = new Size(4, 4) };
                //    yield return new ActorPose { Pose = Pose.Walk1, TileIndicies = [], SpriteSize = new Size(4, 4) };
                //    yield return new ActorPose { Pose = Pose.Walk2, TileIndicies = [], SpriteSize = new Size(4, 4) };
                //    yield return new ActorPose { Pose = Pose.Stand, TileIndicies = [], SpriteSize = new Size(4, 4) };
                //    yield return new ActorPose { Pose = Pose.WalkLeft1, TileIndicies = [], SpriteSize = new Size(4, 4) };
                //    yield return new ActorPose { Pose = Pose.WalkLeft2, TileIndicies = [], SpriteSize = new Size(4, 4) };
                //    yield return new ActorPose { Pose = Pose.StandLeft, TileIndicies = [], SpriteSize = new Size(4, 4) };
                //    break;
                //case CharacterEx.MagitechArmor:
                //    yield return new ActorPose { Pose = Pose.WalkAway1, TileIndicies = [], SpriteSize = new Size(4, 4) };
                //    yield return new ActorPose { Pose = Pose.WalkAway2, TileIndicies = [], SpriteSize = new Size(4, 4) };
                //    yield return new ActorPose { Pose = Pose.StandAway, TileIndicies = [1], SpriteSize = new Size(4, 4) };
                //    yield return new ActorPose { Pose = Pose.Walk1, TileIndicies = [], SpriteSize = new Size(4, 4) };
                //    yield return new ActorPose { Pose = Pose.Walk2, TileIndicies = [], SpriteSize = new Size(4, 4) };
                //    yield return new ActorPose { Pose = Pose.Stand, TileIndicies = [], SpriteSize = new Size(4, 4) };
                //    yield return new ActorPose { Pose = Pose.WalkLeft1, TileIndicies = [], SpriteSize = new Size(4, 4) };
                //    yield return new ActorPose { Pose = Pose.WalkLeft2, TileIndicies = [], SpriteSize = new Size(4, 4) };
                //    yield return new ActorPose { Pose = Pose.StandLeft, TileIndicies = [], SpriteSize = new Size(4, 4) };
                //    yield return new ActorPose { Pose = Pose.Sit, TileIndicies = [], SpriteSize = new Size(4, 4) };
                //    break;
        }

        yield break;
    }

    private static IEnumerable<CharacterPose> DefaultPCPoses()
    {
        yield return new CharacterPose { Pose = Pose.WalkAway1, TileIndicies = [12, 13, 14, 15, 16, 17], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.WalkAway2, TileIndicies = [12, 13, 22, 23, -17, -16], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.StandAway, TileIndicies = [12, 13, 18, 19, 20, 21], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Walk1, TileIndicies = [0, 1, 2, 3, 4, 5], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Walk2, TileIndicies = [0, 1, 10, 11, -5, -4], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Stand, TileIndicies = [0, 1, 6, 7, 8, 9], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.WalkLeft1, TileIndicies = [24, 25, 26, 27, 28, 29], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.WalkLeft2, TileIndicies = [24, 25, 36, 37, 38, 39], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.StandLeft, TileIndicies = [30, 31, 32, 33, 34, 35], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.SaluteAway, TileIndicies = [12, 132, 18, 133, 20, 117], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Salute, TileIndicies = [130, 1, 131, 3, 110, 5], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.HandsUpAway, TileIndicies = [112, 113, 114, 115, 116, 117], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.HandsUp, TileIndicies = [106, 107, 108, 109, 110, 111], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Tisk1, TileIndicies = [0, 1, 152, 153, 154, 155], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Tisk2, TileIndicies = [0, 1, 156, 153, 154, 155], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Laugh1, TileIndicies = [96, 97, 98, 99, 100, 101], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Laugh2, TileIndicies = [102, 103, 104, 105, 100, 101], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Wink, TileIndicies = [0, 1, 6, 95, 8, 9], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Blink, TileIndicies = [0, 1, 93, 94, 8, 9], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.LookLeft, TileIndicies = [146, 147, 148, 149, 8, 9,], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.LookRight, TileIndicies = [-147, -146, -149, -148, 8, 9], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Sad, TileIndicies = [134, 135, 136, 137, 8, 9], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.SadLeft, TileIndicies = [142, 143, 144, 145, 34, 35,], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.SadAway, TileIndicies = [138, 139, 140, 141, 20, 21], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.BlinkLeft, TileIndicies = [150, 31, 151, 33, 34, 35], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Threaten, TileIndicies = [118, 119, 120, 121, 122, 123], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Surprised, TileIndicies = [124, 125, 126, 127, 128, 129], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Dead, TileIndicies = [81, 82, 83, 84, 85, 86], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Sleep, TileIndicies = [87, 88, 89, 90, 91, 92], SpriteSize = new Size(3, 2) };
        yield return new CharacterPose { Pose = Pose.JumpRide1, TileIndicies = [171, 172, 173, 174, 175, 176], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.JumpRide2, TileIndicies = [177, 178, 179, 180, 175, 176], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Tent, TileIndicies = [165, 166, 167, 168, 169, 170], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Hit, TileIndicies = [68, 69, 70, 71, 72, 73], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Cast1, TileIndicies = [74, 75, 76, 77, 78, 79], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Cast2, TileIndicies = [74, 75, 80, 77, 78, 79], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Ready, TileIndicies = [62, 63, 64, 65, 66, 67], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Celebrate1, TileIndicies = [24, 46, 47, 48, 49, 50], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Celebrate2, TileIndicies = [30, 51, 52, 53, 54, 55], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.CombatStandLeft, TileIndicies = [30, 31, 40, 41, 42, 43], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.CombatWalkLeft, TileIndicies = [24, 25, 44, 27, 45, 29], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Kneel, TileIndicies = [56, 57, 58, 59, 60, 61], SpriteSize = new Size(2, 3) };
    }

    private static IEnumerable<CharacterPose> DefaultNpcPoses()
    {
        yield return new CharacterPose { Pose = Pose.WalkAway1, TileIndicies = [12, 13, 14, 15, 16, 17], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.WalkAway2, TileIndicies = [12, 13, 22, 23, -17, -16], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.StandAway, TileIndicies = [12, 13, 18, 19, 20, 21], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Walk1, TileIndicies = [0, 1, 2, 3, 4, 5], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Walk2, TileIndicies = [0, 1, 10, 11, -5, -4], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.Stand, TileIndicies = [0, 1, 6, 7, 8, 9], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.WalkLeft1, TileIndicies = [24, 25, 26, 27, 28, 29], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.WalkLeft2, TileIndicies = [24, 25, 36, 37, 38, 39], SpriteSize = new Size(2, 3) };
        yield return new CharacterPose { Pose = Pose.StandLeft, TileIndicies = [30, 31, 32, 33, 34, 35], SpriteSize = new Size(2, 3) };
    }

    private static int PaletteIndexFor(CharacterEx actor)
        => actor switch
        {
            CharacterEx.Terra => 2,
            CharacterEx.Locke => 1,
            CharacterEx.Cyan => 4,
            CharacterEx.Shadow => 4,
            CharacterEx.Edgar => 0,
            CharacterEx.Gerad => 1,
            CharacterEx.Sabin => 0,
            CharacterEx.ShadowyCharacter => 6,
            CharacterEx.Celes => 0,
            CharacterEx.Strago => 3,
            CharacterEx.Relm => 3,
            CharacterEx.Setzer => 4,
            CharacterEx.Mog => 5,
            CharacterEx.Gau => 3,
            CharacterEx.Gogo => 3,
            CharacterEx.Umaro => 5,
            CharacterEx.ImperialCadet => 0,
            CharacterEx.ImperialSoldier => 1,
            CharacterEx.ImperialCorporal => 2,
            CharacterEx.ImperialCommander => 4,
            CharacterEx.Imp => 0,
            CharacterEx.GeneralLeo => 0,
            CharacterEx.DuncanHarcourt => 1,
            CharacterEx.Bannon => 3,
            CharacterEx.EsperTerra => 6, //8??
            CharacterEx.DomaGuard => 0,
            CharacterEx.Merchant => 1,
            CharacterEx.DracosMen => 2,
            CharacterEx.Ghost => 0,
            CharacterEx.Kefka => 3,
            CharacterEx.EmperorGestahl => 3,
            CharacterEx.GausFather => 0,
            CharacterEx.Gungho => 1,
            CharacterEx.ThamasaMayor => 3,
            CharacterEx.NarsheElder => 4,
            CharacterEx.Auctioneer => 3,
            CharacterEx.Duane => 1,
            CharacterEx.YoungMan => 0,
            CharacterEx.Doberman => 1,
            CharacterEx.Interceptor => 4,
            CharacterEx.Maria => 0,
            CharacterEx.Maestro => 0,
            CharacterEx.Scholar => 1,
            CharacterEx.PrinceRalse => 2,
            CharacterEx.Draco => 4,
            CharacterEx.Arvis => 4,
            CharacterEx.Returner => 1,
            CharacterEx.AirshipCrew => 3,
            CharacterEx.Ultros => 5,
            CharacterEx.MyFairGau => 3,
            CharacterEx.SouthFigaroDancer => 0,
            CharacterEx.YoungWoman => 2,
            CharacterEx.FigaroChancellor => 2,
            CharacterEx.Clyde => 1,
            CharacterEx.OldWoman1 => 0,
            CharacterEx.OldWoman2 => 3,
            CharacterEx.FigaroMatron => 4,
            CharacterEx.Aisha => 0,
            CharacterEx.YoungWoman1 => 1,
            CharacterEx.YoungWoman2 => 2,
            CharacterEx.YoungWoman3 => 3,
            CharacterEx.Owain => 1,
            CharacterEx.YoungBoy1 => 0,
            CharacterEx.YoungBoy2 => 2,
            CharacterEx.YoungBoy3 => 3,
            CharacterEx.YoungGirl1 => 0,
            CharacterEx.YoungGirl2 => 1,
            CharacterEx.Rachel => 0,
            CharacterEx.Katarin => 4,
            CharacterEx.OperaImpresario => 4,
            CharacterEx.EsperElder => 4,
            CharacterEx.Yura => 4,
            CharacterEx.Sigfried => 4,
            CharacterEx.Cid => 3,
            CharacterEx.Maduin => 4,
            CharacterEx.Baram => 0,
            CharacterEx.Pirate => 1,
            CharacterEx.TzenThief => 3,
            CharacterEx.Vargas => 4,
            CharacterEx.Dadaluma => 7,
            CharacterEx.SrBehemoth => 2,
            CharacterEx.Vomammoth => 4,
            CharacterEx.NarsheGuard => 1,
            CharacterEx.TrainConductor => 4,
            CharacterEx.Shopkeeper => 1,
            CharacterEx.FairyEsper => 2,
            CharacterEx.WolfEsper => 2,
            CharacterEx.LoneWolf => 4,
            CharacterEx.StormDragon => 0,
            CharacterEx.GoldDragon => 2,
            CharacterEx.DirtDragon => 2,
            CharacterEx.WhiteDragon => 2,
            CharacterEx.RedDragon => 2,
            CharacterEx.IceDragon => 3,
            CharacterEx.BlueDragon => 3,
            CharacterEx.SkullDragon => 4,
            CharacterEx.OperaGuard => 1,
            CharacterEx.FigaroGuard => 2,
            CharacterEx.Daryl => 2,
            CharacterEx.Hidon => 0,
            CharacterEx.Chupon => 5,
            CharacterEx.ImperialElite => 2,
            CharacterEx.Cultist => 4,
            CharacterEx.Wrexsoul => 8,
            CharacterEx.Ramuh => 4,
            CharacterEx.DomaKing => 2,
            CharacterEx.Inferno => 4,
            CharacterEx.Ifrit => 3,
            CharacterEx.Phantom => 4,
            CharacterEx.Shiva => 0,
            CharacterEx.Unicorn => 3,
            CharacterEx.Bismarck => 4,
            CharacterEx.Carbuncle => 2,
            CharacterEx.Shoat => 2,
            CharacterEx.Owzer => 3,
            CharacterEx.Number024 => 5,
            CharacterEx.AtmaWeapon => 3,
            CharacterEx.Tritoch => 2,
            CharacterEx.Odin => 21,
            //CharacterEx.Chocobo => 7,
            //CharacterEx.MagitechArmor => 7,
            _ => 0
        };
}
