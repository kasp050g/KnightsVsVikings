using KnightsVsVikings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    // Kasper  Fly
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
            MakeBlend(EFaction.Knights, EUnitType.Worker);
            MakeBlend(EFaction.Knights, EUnitType.Bowman);
            MakeBlend(EFaction.Knights, EUnitType.Footman);
            MakeBlend(EFaction.Knights, EUnitType.Spearman);

            MakeBlend(EFaction.Vikings, EUnitType.Worker);
            MakeBlend(EFaction.Vikings, EUnitType.Bowman);
            MakeBlend(EFaction.Vikings, EUnitType.Footman);
            MakeBlend(EFaction.Vikings, EUnitType.Spearman);
        }

        private void MakeBlend(EFaction factionName, EUnitType unitType)
        {
            CraftBlend(factionName, unitType, EUnitAnimationType.Idle);
            CraftBlend(factionName, unitType, EUnitAnimationType.Run);
            CraftBlend(factionName, unitType, EUnitAnimationType.BowAttack);
            CraftBlend(factionName, unitType, EUnitAnimationType.SpearAttack);
            CraftBlend(factionName, unitType, EUnitAnimationType.SwordAttack);
            CraftBlend(factionName, unitType, EUnitAnimationType.Die);
            CraftBlend(factionName, unitType, EUnitAnimationType.Cast);
        }

        private void CraftBlend(EFaction factionName, EUnitType unitType, EUnitAnimationType unitAnimationType)
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

        public BlendTree GetUnitBlendTree(EFaction factionName, EUnitType unitType, EUnitAnimationType unitAnimationType)
        {
            return Blendtrees[$"{factionName}{unitType}{unitAnimationType}"];
        }
    }
}
