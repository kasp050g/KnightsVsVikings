﻿using KnightsVsVikings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    // Kasper  Fly
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

        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
        public Dictionary<string, Animation> Animations { get => animations; set => animations = value; }

        public void MakeAnimation()
        {
            MakeUnit(EFaction.Knights, EUnitType.Worker);
            MakeUnit(EFaction.Knights, EUnitType.Bowman);
            MakeUnit(EFaction.Knights, EUnitType.Footman);
            MakeUnit(EFaction.Knights, EUnitType.Spearman);

            MakeUnit(EFaction.Vikings, EUnitType.Worker);
            MakeUnit(EFaction.Vikings, EUnitType.Bowman);
            MakeUnit(EFaction.Vikings, EUnitType.Footman);
            MakeUnit(EFaction.Vikings, EUnitType.Spearman);
        }

        private void MakeUnit(EFaction factionName, EUnitType unitType)
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

            Animation UpIdle = new Animation(
                SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Up"], 
                vector2_UpIdle, spriteSize, 
                $"{factionName}{unitType}Up{unitAnimationType}", 
                fps,true);

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
            Animation DownIdle = new Animation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Down"], vector2_DownIdle, spriteSize, $"{factionName}{unitType}Down{unitAnimationType}", fps, true);
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
            Animation SideIdle = new Animation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Side"], vector2_SideIdle, spriteSize, $"{factionName}{unitType}Side{unitAnimationType}", fps, true);
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
            Animation UpRun = new Animation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Up"], vector2_UpRun, spriteSize, $"{factionName}{unitType}Up{unitAnimationType}", fps, true);
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
            Animation DownRun = new Animation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Down"], vector2_DownRun, spriteSize, $"{factionName}{unitType}Down{unitAnimationType}", fps, true);
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
            Animation SideRun = new Animation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Side"], vector2_SideRun, spriteSize, $"{factionName}{unitType}Side{unitAnimationType}", fps, true);
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
            Animation UpBowAttack = new Animation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Up"], vector2_UpBowAttack, spriteSize, $"{factionName}{unitType}Up{unitAnimationType}", fps, false,true);
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
            Animation DownBowAttack = new Animation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Down"], vector2_DownBowAttack, spriteSize, $"{factionName}{unitType}Down{unitAnimationType}", fps, false, true);
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
            Animation SideBowAttack = new Animation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Side"], vector2_SideBowAttack, spriteSize, $"{factionName}{unitType}Side{unitAnimationType}", fps, false, true);
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
            Animation UpSwordAttack = new Animation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Up"], vector2_UpSwordAttack, spriteSize, $"{factionName}{unitType}Up{unitAnimationType}", fps, false, true);
            animations.Add($"{factionName}{unitType}Up{unitAnimationType}", UpSwordAttack);

            // SwordAttack Down
            List<Vector2> vector2_DownSwordAttack = new List<Vector2>();
            vector2_DownSwordAttack.Add(new Vector2(4, 5) * 256);
            vector2_DownSwordAttack.Add(new Vector2(5, 5) * 256);
            vector2_DownSwordAttack.Add(new Vector2(6, 5) * 256);
            vector2_DownSwordAttack.Add(new Vector2(7, 5) * 256);
            vector2_DownSwordAttack.Add(new Vector2(0, 6) * 256);
            vector2_DownSwordAttack.Add(new Vector2(1, 6) * 256);
            Animation DownSwordAttack = new Animation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Down"], vector2_DownSwordAttack, spriteSize, $"{factionName}{unitType}Down{unitAnimationType}", fps, false, true);
            animations.Add($"{factionName}{unitType}Down{unitAnimationType}", DownSwordAttack);

            // SwordAttack Side
            List<Vector2> vector2_SideSwordAttack = new List<Vector2>();
            vector2_SideSwordAttack.Add(new Vector2(1, 6) * 256);
            vector2_SideSwordAttack.Add(new Vector2(2, 6) * 256);
            vector2_SideSwordAttack.Add(new Vector2(3, 6) * 256);
            vector2_SideSwordAttack.Add(new Vector2(4, 6) * 256);
            vector2_SideSwordAttack.Add(new Vector2(5, 6) * 256);
            vector2_SideSwordAttack.Add(new Vector2(6, 6) * 256);
            Animation SideSwordAttack = new Animation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Side"], vector2_SideSwordAttack, spriteSize, $"{factionName}{unitType}Side{unitAnimationType}", fps, false, true);
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
            Animation UpSpearAttack = new Animation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Up"], vector2_UpSpearAttack, spriteSize, $"{factionName}{unitType}Up{unitAnimationType}", fps, false, true);
            animations.Add($"{factionName}{unitType}Up{unitAnimationType}", UpSpearAttack);

            // SpearAttack Down
            List<Vector2> vector2_DownSpearAttack = new List<Vector2>();
            vector2_DownSpearAttack.Add(new Vector2(2, 6) * 256);
            vector2_DownSpearAttack.Add(new Vector2(3, 6) * 256);
            vector2_DownSpearAttack.Add(new Vector2(4, 6) * 256);
            vector2_DownSpearAttack.Add(new Vector2(5, 6) * 256);
            vector2_DownSpearAttack.Add(new Vector2(6, 6) * 256);
            vector2_DownSpearAttack.Add(new Vector2(7, 6) * 256);
            Animation DownSpearAttack = new Animation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Down"], vector2_DownSpearAttack, spriteSize, $"{factionName}{unitType}Down{unitAnimationType}", fps, false, true);
            animations.Add($"{factionName}{unitType}Down{unitAnimationType}", DownSpearAttack);

            // SpearAttack Side
            List<Vector2> vector2_SideSpearAttack = new List<Vector2>();
            vector2_SideSpearAttack.Add(new Vector2(7, 6) * 256);
            vector2_SideSpearAttack.Add(new Vector2(0, 7) * 256);
            vector2_SideSpearAttack.Add(new Vector2(1, 7) * 256);
            vector2_SideSpearAttack.Add(new Vector2(2, 7) * 256);
            vector2_SideSpearAttack.Add(new Vector2(3, 7) * 256);
            vector2_SideSpearAttack.Add(new Vector2(4, 7) * 256);
            Animation SideSpearAttack = new Animation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Side"], vector2_SideSpearAttack, spriteSize, $"{factionName}{unitType}Side{unitAnimationType}", fps, false, true);
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
            Animation UpDie = new Animation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Up"], vector2_UpDie, spriteSize, $"{factionName}{unitType}Up{unitAnimationType}", fps, false,true,true);
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
            Animation DownDie = new Animation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Down"], vector2_DownDie, spriteSize, $"{factionName}{unitType}Down{unitAnimationType}", fps, false,true,true);
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
            Animation SideDie = new Animation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Side"], vector2_SideDie, spriteSize, $"{factionName}{unitType}Side{unitAnimationType}", fps, false,true);
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
            Animation UpCast = new Animation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Up"], vector2_UpCast, spriteSize, $"{factionName}{unitType}Up{unitAnimationType}", fps, false, true);
            animations.Add($"{factionName}{unitType}Up{unitAnimationType}", UpCast);

            // Cast Down
            List<Vector2> vector2_DownCast = new List<Vector2>();
            vector2_DownCast.Add(new Vector2(7, 0) * 256);
            vector2_DownCast.Add(new Vector2(0, 1) * 256);
            vector2_DownCast.Add(new Vector2(1, 1) * 256);
            vector2_DownCast.Add(new Vector2(2, 1) * 256);
            vector2_DownCast.Add(new Vector2(3, 1) * 256);
            vector2_DownCast.Add(new Vector2(4, 1) * 256);
            Animation DownCast = new Animation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Down"], vector2_DownCast, spriteSize, $"{factionName}{unitType}Down{unitAnimationType}", fps, false, true);
            animations.Add($"{factionName}{unitType}Down{unitAnimationType}", DownCast);

            // Cast Side
            List<Vector2> vector2_SideCast = new List<Vector2>();
            vector2_SideCast.Add(new Vector2(7, 0) * 256);
            vector2_SideCast.Add(new Vector2(0, 1) * 256);
            vector2_SideCast.Add(new Vector2(1, 1) * 256);
            vector2_SideCast.Add(new Vector2(2, 1) * 256);
            vector2_SideCast.Add(new Vector2(3, 1) * 256);
            vector2_SideCast.Add(new Vector2(4, 1) * 256);
            Animation SideCast = new Animation(SpriteContainer.Instance.Sprite[$"{factionName}{unitType}Side"], vector2_SideCast, spriteSize, $"{factionName}{unitType}Side{unitAnimationType}", fps, false, true);
            animations.Add($"{factionName}{unitType}Side{unitAnimationType}", SideCast);
            #endregion
        }
    }
}
