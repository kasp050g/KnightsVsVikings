using KnightsVsVikings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class AnimationContainer
    {
        #region Singleton
        private static AnimationContainer instance;
        public static AnimationContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AnimationContainer();
                }
                return instance;
            }
        }
        #endregion

        private Dictionary<string, CAnimation> animations = new Dictionary<string, CAnimation>();
        public Dictionary<string, CAnimation> Animations { get => animations; set => animations = value; }

        public void MakeAnimation()
        {
            MakeUnit(EFactions.Knights, EUnitType.Worker);
            MakeUnit(EFactions.Knights, EUnitType.Bowman);
            MakeUnit(EFactions.Knights, EUnitType.Footman);
            MakeUnit(EFactions.Knights, EUnitType.Spearman);

            MakeUnit(EFactions.Vikings, EUnitType.Worker);
            MakeUnit(EFactions.Vikings, EUnitType.Bowman);
            MakeUnit(EFactions.Vikings, EUnitType.Footman);
            MakeUnit(EFactions.Vikings, EUnitType.Spearman);
        }

        private void MakeUnit(EFactions factionName, EUnitType unitType)
        {
            EUnitAnimationType unitAnimationType;
            Vector2 spriteSize = new Vector2(256, 256);
            int fps = 10;

            #region Idle
            unitAnimationType = EUnitAnimationType.Idle;
            // Idle Up
            List<Vector2> vector2_UpIdle = new List<Vector2>();
            vector2_UpIdle.Add(new Vector2(2, 3) * 256);
            vector2_UpIdle.Add(new Vector2(3, 3) * 256);
            vector2_UpIdle.Add(new Vector2(4, 3) * 256);
            vector2_UpIdle.Add(new Vector2(5, 3) * 256);
            vector2_UpIdle.Add(new Vector2(6, 3) * 256);
            vector2_UpIdle.Add(new Vector2(7, 3) * 256);
            vector2_UpIdle.Add(new Vector2(0, 4) * 256);
            CAnimation UpIdle = new CAnimation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Up"], vector2_UpIdle, spriteSize, $"{factionName}{unitType}Up{unitAnimationType}", fps,true);
            animations.Add($"{factionName}{unitType}Up{unitAnimationType}", UpIdle);

            // Idle Down
            List<Vector2> vector2_DownIdle = new List<Vector2>();
            vector2_DownIdle.Add(new Vector2(2, 3) * 256);
            vector2_DownIdle.Add(new Vector2(3, 3) * 256);
            vector2_DownIdle.Add(new Vector2(4, 3) * 256);
            vector2_DownIdle.Add(new Vector2(5, 3) * 256);
            vector2_DownIdle.Add(new Vector2(6, 3) * 256);
            vector2_DownIdle.Add(new Vector2(7, 3) * 256);
            vector2_DownIdle.Add(new Vector2(0, 4) * 256);
            CAnimation DownIdle = new CAnimation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Down"], vector2_DownIdle, spriteSize, $"{factionName}{unitType}Down{unitAnimationType}", fps, true);
            animations.Add($"{factionName}{unitType}Down{unitAnimationType}", DownIdle);

            // Idle Side
            List<Vector2> vector2_SideIdle = new List<Vector2>();
            vector2_SideIdle.Add(new Vector2(2, 3) * 256);
            vector2_SideIdle.Add(new Vector2(3, 3) * 256);
            vector2_SideIdle.Add(new Vector2(4, 3) * 256);
            vector2_SideIdle.Add(new Vector2(5, 3) * 256);
            vector2_SideIdle.Add(new Vector2(6, 3) * 256);
            vector2_SideIdle.Add(new Vector2(7, 3) * 256);
            vector2_SideIdle.Add(new Vector2(0, 4) * 256);
            CAnimation SideIdle = new CAnimation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Side"], vector2_SideIdle, spriteSize, $"{factionName}{unitType}Side{unitAnimationType}", fps, true);
            animations.Add($"{factionName}{unitType}Side{unitAnimationType}", SideIdle);
            #endregion

            #region Run
            unitAnimationType = EUnitAnimationType.Run;
            // Run Up
            List<Vector2> vector2_UpRun = new List<Vector2>();
            vector2_UpRun.Add(new Vector2(2, 4) * 256);
            vector2_UpRun.Add(new Vector2(3, 4) * 256);
            vector2_UpRun.Add(new Vector2(4, 4) * 256);
            vector2_UpRun.Add(new Vector2(5, 4) * 256);
            vector2_UpRun.Add(new Vector2(6, 4) * 256);
            vector2_UpRun.Add(new Vector2(7, 4) * 256);

            vector2_UpRun.Add(new Vector2(0, 5) * 256);
            vector2_UpRun.Add(new Vector2(1, 5) * 256);
            vector2_UpRun.Add(new Vector2(2, 5) * 256);
            vector2_UpRun.Add(new Vector2(3, 5) * 256);
            CAnimation UpRun = new CAnimation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Up"], vector2_UpRun, spriteSize, $"{factionName}{unitType}Up{unitAnimationType}", fps, true);
            animations.Add($"{factionName}{unitType}Up{unitAnimationType}", UpRun);
            // Run Down
            List<Vector2> vector2_DownRun = new List<Vector2>();
            vector2_DownRun.Add(new Vector2(2, 4) * 256);
            vector2_DownRun.Add(new Vector2(3, 4) * 256);
            vector2_DownRun.Add(new Vector2(4, 4) * 256);
            vector2_DownRun.Add(new Vector2(5, 4) * 256);
            vector2_DownRun.Add(new Vector2(6, 4) * 256);
            vector2_DownRun.Add(new Vector2(7, 4) * 256);

            vector2_DownRun.Add(new Vector2(0, 5) * 256);
            vector2_DownRun.Add(new Vector2(1, 5) * 256);
            vector2_DownRun.Add(new Vector2(2, 5) * 256);
            vector2_DownRun.Add(new Vector2(3, 5) * 256);
            CAnimation DownRun = new CAnimation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Down"], vector2_DownRun, spriteSize, $"{factionName}{unitType}Down{unitAnimationType}", fps, true);
            animations.Add($"{factionName}{unitType}Down{unitAnimationType}", DownRun);
            // Run Side
            List<Vector2> vector2_SideRun = new List<Vector2>();
            vector2_SideRun.Add(new Vector2(7, 4) * 256);

            vector2_SideRun.Add(new Vector2(0, 5) * 256);
            vector2_SideRun.Add(new Vector2(1, 5) * 256);
            vector2_SideRun.Add(new Vector2(2, 5) * 256);
            vector2_SideRun.Add(new Vector2(3, 5) * 256);
            vector2_SideRun.Add(new Vector2(4, 5) * 256);
            vector2_SideRun.Add(new Vector2(5, 5) * 256);
            vector2_SideRun.Add(new Vector2(6, 5) * 256);
            vector2_SideRun.Add(new Vector2(7, 5) * 256);

            vector2_SideRun.Add(new Vector2(0, 6) * 256);
            CAnimation SideRun = new CAnimation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Side"], vector2_SideRun, spriteSize, $"{factionName}{unitType}Side{unitAnimationType}", fps, true);
            animations.Add($"{factionName}{unitType}Side{unitAnimationType}", SideRun);
            #endregion

            #region BowAttack
            unitAnimationType = EUnitAnimationType.BowAttack;
            // BowAttack Up
            List<Vector2> vector2_UpBowAttack = new List<Vector2>();
            vector2_UpBowAttack.Add(new Vector2(0, 0) * 256);
            vector2_UpBowAttack.Add(new Vector2(1, 0) * 256);
            vector2_UpBowAttack.Add(new Vector2(2, 0) * 256);
            vector2_UpBowAttack.Add(new Vector2(3, 0) * 256);
            vector2_UpBowAttack.Add(new Vector2(4, 0) * 256);
            vector2_UpBowAttack.Add(new Vector2(5, 0) * 256);
            vector2_UpBowAttack.Add(new Vector2(6, 0) * 256);
            CAnimation UpBowAttack = new CAnimation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Up"], vector2_UpBowAttack, spriteSize, $"{factionName}{unitType}Up{unitAnimationType}", fps, false,true);
            animations.Add($"{factionName}{unitType}Up{unitAnimationType}", UpBowAttack);

            // BowAttack Down
            List<Vector2> vector2_DownBowAttack = new List<Vector2>();
            vector2_DownBowAttack.Add(new Vector2(0, 0) * 256);
            vector2_DownBowAttack.Add(new Vector2(1, 0) * 256);
            vector2_DownBowAttack.Add(new Vector2(2, 0) * 256);
            vector2_DownBowAttack.Add(new Vector2(3, 0) * 256);
            vector2_DownBowAttack.Add(new Vector2(4, 0) * 256);
            vector2_DownBowAttack.Add(new Vector2(5, 0) * 256);
            vector2_DownBowAttack.Add(new Vector2(6, 0) * 256);
            CAnimation DownBowAttack = new CAnimation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Down"], vector2_DownBowAttack, spriteSize, $"{factionName}{unitType}Down{unitAnimationType}", fps, false, true);
            animations.Add($"{factionName}{unitType}Down{unitAnimationType}", DownBowAttack);

            // BowAttack Side
            List<Vector2> vector2_SideBowAttack = new List<Vector2>();
            vector2_SideBowAttack.Add(new Vector2(0, 0) * 256);
            vector2_SideBowAttack.Add(new Vector2(1, 0) * 256);
            vector2_SideBowAttack.Add(new Vector2(2, 0) * 256);
            vector2_SideBowAttack.Add(new Vector2(3, 0) * 256);
            vector2_SideBowAttack.Add(new Vector2(4, 0) * 256);
            vector2_SideBowAttack.Add(new Vector2(5, 0) * 256);
            vector2_SideBowAttack.Add(new Vector2(6, 0) * 256);
            CAnimation SideBowAttack = new CAnimation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Side"], vector2_SideBowAttack, spriteSize, $"{factionName}{unitType}Side{unitAnimationType}", fps, false, true);
            animations.Add($"{factionName}{unitType}Side{unitAnimationType}", SideBowAttack);
            #endregion

            #region SwordAttack
            unitAnimationType = EUnitAnimationType.SwordAttack;
            // SwordAttack Up
            List<Vector2> vector2_UpSwordAttack = new List<Vector2>();
            vector2_UpSwordAttack.Add(new Vector2(4, 5) * 256);
            vector2_UpSwordAttack.Add(new Vector2(5, 5) * 256);
            vector2_UpSwordAttack.Add(new Vector2(6, 5) * 256);
            vector2_UpSwordAttack.Add(new Vector2(7, 5) * 256);
            vector2_UpSwordAttack.Add(new Vector2(0, 6) * 256);
            vector2_UpSwordAttack.Add(new Vector2(1, 6) * 256);
            CAnimation UpSwordAttack = new CAnimation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Up"], vector2_UpSwordAttack, spriteSize, $"{factionName}{unitType}Up{unitAnimationType}", fps, false, true);
            animations.Add($"{factionName}{unitType}Up{unitAnimationType}", UpSwordAttack);

            // SwordAttack Down
            List<Vector2> vector2_DownSwordAttack = new List<Vector2>();
            vector2_DownSwordAttack.Add(new Vector2(4, 5) * 256);
            vector2_DownSwordAttack.Add(new Vector2(5, 5) * 256);
            vector2_DownSwordAttack.Add(new Vector2(6, 5) * 256);
            vector2_DownSwordAttack.Add(new Vector2(7, 5) * 256);
            vector2_DownSwordAttack.Add(new Vector2(0, 6) * 256);
            vector2_DownSwordAttack.Add(new Vector2(1, 6) * 256);
            CAnimation DownSwordAttack = new CAnimation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Down"], vector2_DownSwordAttack, spriteSize, $"{factionName}{unitType}Down{unitAnimationType}", fps, false, true);
            animations.Add($"{factionName}{unitType}Down{unitAnimationType}", DownSwordAttack);

            // SwordAttack Side
            List<Vector2> vector2_SideSwordAttack = new List<Vector2>();
            vector2_SideSwordAttack.Add(new Vector2(1, 6) * 256);
            vector2_SideSwordAttack.Add(new Vector2(2, 6) * 256);
            vector2_SideSwordAttack.Add(new Vector2(3, 6) * 256);
            vector2_SideSwordAttack.Add(new Vector2(4, 6) * 256);
            vector2_SideSwordAttack.Add(new Vector2(5, 6) * 256);
            vector2_SideSwordAttack.Add(new Vector2(6, 6) * 256);
            CAnimation SideSwordAttack = new CAnimation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Side"], vector2_SideSwordAttack, spriteSize, $"{factionName}{unitType}Side{unitAnimationType}", fps, false, true);
            animations.Add($"{factionName}{unitType}Side{unitAnimationType}", SideSwordAttack);
            #endregion

            #region SpearAttack
            unitAnimationType = EUnitAnimationType.SpearAttack;
            // SpearAttack Up
            List<Vector2> vector2_UpSpearAttack = new List<Vector2>();
            vector2_UpSpearAttack.Add(new Vector2(2, 6) * 256);
            vector2_UpSpearAttack.Add(new Vector2(3, 6) * 256);
            vector2_UpSpearAttack.Add(new Vector2(4, 6) * 256);
            vector2_UpSpearAttack.Add(new Vector2(5, 6) * 256);
            vector2_UpSpearAttack.Add(new Vector2(6, 6) * 256);
            vector2_UpSpearAttack.Add(new Vector2(7, 6) * 256);
            CAnimation UpSpearAttack = new CAnimation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Up"], vector2_UpSpearAttack, spriteSize, $"{factionName}{unitType}Up{unitAnimationType}", fps, false, true);
            animations.Add($"{factionName}{unitType}Up{unitAnimationType}", UpSpearAttack);

            // SpearAttack Down
            List<Vector2> vector2_DownSpearAttack = new List<Vector2>();
            vector2_DownSpearAttack.Add(new Vector2(2, 6) * 256);
            vector2_DownSpearAttack.Add(new Vector2(3, 6) * 256);
            vector2_DownSpearAttack.Add(new Vector2(4, 6) * 256);
            vector2_DownSpearAttack.Add(new Vector2(5, 6) * 256);
            vector2_DownSpearAttack.Add(new Vector2(6, 6) * 256);
            vector2_DownSpearAttack.Add(new Vector2(7, 6) * 256);
            CAnimation DownSpearAttack = new CAnimation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Down"], vector2_DownSpearAttack, spriteSize, $"{factionName}{unitType}Down{unitAnimationType}", fps, false, true);
            animations.Add($"{factionName}{unitType}Down{unitAnimationType}", DownSpearAttack);

            // SpearAttack Side
            List<Vector2> vector2_SideSpearAttack = new List<Vector2>();
            vector2_SideSpearAttack.Add(new Vector2(7, 6) * 256);
            vector2_SideSpearAttack.Add(new Vector2(0, 7) * 256);
            vector2_SideSpearAttack.Add(new Vector2(1, 7) * 256);
            vector2_SideSpearAttack.Add(new Vector2(2, 7) * 256);
            vector2_SideSpearAttack.Add(new Vector2(3, 7) * 256);
            vector2_SideSpearAttack.Add(new Vector2(4, 7) * 256);
            CAnimation SideSpearAttack = new CAnimation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Side"], vector2_SideSpearAttack, spriteSize, $"{factionName}{unitType}Side{unitAnimationType}", fps, false, true);
            animations.Add($"{factionName}{unitType}Side{unitAnimationType}", SideSpearAttack);
            #endregion

            #region Die
            unitAnimationType = EUnitAnimationType.Die;
            // Die Up
            List<Vector2> vector2_UpDie = new List<Vector2>();
            vector2_UpDie.Add(new Vector2(5, 1) * 256);
            vector2_UpDie.Add(new Vector2(6, 1) * 256);
            vector2_UpDie.Add(new Vector2(7, 1) * 256);
            vector2_UpDie.Add(new Vector2(0, 2) * 256);
            vector2_UpDie.Add(new Vector2(1, 2) * 256);
            vector2_UpDie.Add(new Vector2(2, 2) * 256);
            vector2_UpDie.Add(new Vector2(3, 2) * 256);
            vector2_UpDie.Add(new Vector2(4, 2) * 256);
            vector2_UpDie.Add(new Vector2(5, 2) * 256);
            CAnimation UpDie = new CAnimation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Up"], vector2_UpDie, spriteSize, $"{factionName}{unitType}Up{unitAnimationType}", fps, false,true,true);
            animations.Add($"{factionName}{unitType}Up{unitAnimationType}", UpDie);

            // Die Down
            List<Vector2> vector2_DownDie = new List<Vector2>();
            vector2_DownDie.Add(new Vector2(5, 1) * 256);
            vector2_DownDie.Add(new Vector2(6, 1) * 256);
            vector2_DownDie.Add(new Vector2(7, 1) * 256);
            vector2_DownDie.Add(new Vector2(0, 2) * 256);
            vector2_DownDie.Add(new Vector2(1, 2) * 256);
            vector2_DownDie.Add(new Vector2(2, 2) * 256);
            vector2_DownDie.Add(new Vector2(3, 2) * 256);
            vector2_DownDie.Add(new Vector2(4, 2) * 256);
            vector2_DownDie.Add(new Vector2(5, 2) * 256);
            CAnimation DownDie = new CAnimation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Down"], vector2_DownDie, spriteSize, $"{factionName}{unitType}Down{unitAnimationType}", fps, false,true,true);
            animations.Add($"{factionName}{unitType}Down{unitAnimationType}", DownDie);

            // Die Side
            List<Vector2> vector2_SideDie = new List<Vector2>();
            vector2_SideDie.Add(new Vector2(5, 1) * 256);
            vector2_SideDie.Add(new Vector2(6, 1) * 256);
            vector2_SideDie.Add(new Vector2(7, 1) * 256);
            vector2_SideDie.Add(new Vector2(0, 2) * 256);
            vector2_SideDie.Add(new Vector2(1, 2) * 256);
            vector2_SideDie.Add(new Vector2(2, 2) * 256);
            vector2_SideDie.Add(new Vector2(3, 2) * 256);
            vector2_SideDie.Add(new Vector2(4, 2) * 256);
            vector2_SideDie.Add(new Vector2(5, 2) * 256);
            CAnimation SideDie = new CAnimation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Side"], vector2_SideDie, spriteSize, $"{factionName}{unitType}Side{unitAnimationType}", fps, false,true);
            animations.Add($"{factionName}{unitType}Side{unitAnimationType}", SideDie);
            #endregion

            #region Cast
            unitAnimationType = EUnitAnimationType.Cast;
            // Cast Up
            List<Vector2> vector2_UpCast = new List<Vector2>();
            vector2_UpCast.Add(new Vector2(7, 0) * 256);
            vector2_UpCast.Add(new Vector2(0, 1) * 256);
            vector2_UpCast.Add(new Vector2(1, 1) * 256);
            vector2_UpCast.Add(new Vector2(2, 1) * 256);
            vector2_UpCast.Add(new Vector2(3, 1) * 256);
            vector2_UpCast.Add(new Vector2(4, 1) * 256);
            CAnimation UpCast = new CAnimation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Up"], vector2_UpCast, spriteSize, $"{factionName}{unitType}Up{unitAnimationType}", fps, false, true);
            animations.Add($"{factionName}{unitType}Up{unitAnimationType}", UpCast);

            // Cast Down
            List<Vector2> vector2_DownCast = new List<Vector2>();
            vector2_DownCast.Add(new Vector2(7, 0) * 256);
            vector2_DownCast.Add(new Vector2(0, 1) * 256);
            vector2_DownCast.Add(new Vector2(1, 1) * 256);
            vector2_DownCast.Add(new Vector2(2, 1) * 256);
            vector2_DownCast.Add(new Vector2(3, 1) * 256);
            vector2_DownCast.Add(new Vector2(4, 1) * 256);
            CAnimation DownCast = new CAnimation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Down"], vector2_DownCast, spriteSize, $"{factionName}{unitType}Down{unitAnimationType}", fps, false, true);
            animations.Add($"{factionName}{unitType}Down{unitAnimationType}", DownCast);

            // Cast Side
            List<Vector2> vector2_SideCast = new List<Vector2>();
            vector2_SideCast.Add(new Vector2(7, 0) * 256);
            vector2_SideCast.Add(new Vector2(0, 1) * 256);
            vector2_SideCast.Add(new Vector2(1, 1) * 256);
            vector2_SideCast.Add(new Vector2(2, 1) * 256);
            vector2_SideCast.Add(new Vector2(3, 1) * 256);
            vector2_SideCast.Add(new Vector2(4, 1) * 256);
            CAnimation SideCast = new CAnimation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Side"], vector2_SideCast, spriteSize, $"{factionName}{unitType}Side{unitAnimationType}", fps, false, true);
            animations.Add($"{factionName}{unitType}Side{unitAnimationType}", SideCast);
            #endregion
        }
    }
}
