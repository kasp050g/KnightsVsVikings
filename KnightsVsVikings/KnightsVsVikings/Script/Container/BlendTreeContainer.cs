using KnightsVsVikings.Script.MainSystem.Enum;
using KnightsVsVikings.Script.MainSystem.In_Works_Not_Done_Animations;
using KnightsVsVikings.Script.TheGame.Enum.AnimationsEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class BlendTreeContainer
    {
        #region Singleton
        private static BlendTreeContainer instance;
        public static BlendTreeContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BlendTreeContainer();
                }
                return instance;
            }
        }
        #endregion

        private Dictionary<string, BlendTree> blendtrees = new Dictionary<string, BlendTree>();

        public Dictionary<string, BlendTree> Blendtrees { get => blendtrees; set => blendtrees = value; }

        public void MakeBlend()
        {
            MakeBlend(EFactions.Knights, EUnitType.Worker);
            MakeBlend(EFactions.Knights, EUnitType.Bowman);
            MakeBlend(EFactions.Knights, EUnitType.Footman);
            MakeBlend(EFactions.Knights, EUnitType.Spearman);

            MakeBlend(EFactions.Vikings, EUnitType.Worker);
            MakeBlend(EFactions.Vikings, EUnitType.Bowman);
            MakeBlend(EFactions.Vikings, EUnitType.Footman);
            MakeBlend(EFactions.Vikings, EUnitType.Spearman);
        }

        private void MakeBlend(EFactions factionName, EUnitType unitType)
        {
            CraftBlend(factionName, unitType, EUnitAnimationType.Idle);
            CraftBlend(factionName, unitType, EUnitAnimationType.Run);
            CraftBlend(factionName, unitType, EUnitAnimationType.BowAttack);
            CraftBlend(factionName, unitType, EUnitAnimationType.SpearAttack);
            CraftBlend(factionName, unitType, EUnitAnimationType.SwordAttack);
            CraftBlend(factionName, unitType, EUnitAnimationType.Die);
            CraftBlend(factionName, unitType, EUnitAnimationType.Cast);
        }

        private void CraftBlend(EFactions factionName, EUnitType unitType, EUnitAnimationType unitAnimationType)
        {
            BlendTree blendTree = new BlendTree(
            AnimationContainer.Instance.Animations[$"{factionName}{unitType}Up{unitAnimationType}"],
            AnimationContainer.Instance.Animations[$"{factionName}{unitType}Down{unitAnimationType}"],
            AnimationContainer.Instance.Animations[$"{factionName}{unitType}Side{unitAnimationType}"],
            AnimationContainer.Instance.Animations[$"{factionName}{unitType}Side{unitAnimationType}"],
            $"{unitAnimationType}"
            );
            Blendtrees.Add($"{factionName}{unitType}{unitAnimationType}", blendTree);
        }

        public BlendTree GetUnitBlendTree(EFactions factionName, EUnitType unitType, EUnitAnimationType unitAnimationType)
        {
            return Blendtrees[$"{factionName}{unitType}{unitAnimationType}"];
        }
    }
}
