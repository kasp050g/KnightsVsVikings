using MainSystemFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsVsVikings
{
    // Kasper  Fly
    public class WorldEditorUI
    {
        Scene myScene;
        TileGrid tileGrid;
        GameObject actionBar;
        GameObject actionBarFactionAndTeam;
        // UI stuff
        GroundTileGridBar groundTile;
        ResourceTileGridBar resourceTileGridBar;
        BuildingTileGridBar buildingTileGridBar;
        UnitTileGridBar unitTileGridBar;
        LoadSaveUIBar loadSaveUIBar;

        GUIButton groundBtn;
        GUIButton resourcesBtn;
        GUIButton builingsBtn;
        GUIButton unitBtn;

        GUIButton knightBtn;
        GUIButton vikingBtn;

        GUIButton team01Btn;
        GUIButton team02Btn;
        GUIButton team03Btn;

        PlaceTileWithMouse placeTile;

        public WorldEditorUI(Scene myScene, PlaceTileWithMouse placeTile, TileGrid tileGrid)
        {
            this.myScene = myScene;
            this.placeTile = placeTile;
            this.tileGrid = tileGrid;
        }

        public void MadeUI()
        {
            actionBar = TheActionBar();
            actionBarFactionAndTeam = ActionBarToFactionAndTeam();

            groundTile = new GroundTileGridBar(myScene, actionBar, placeTile);
            groundTile.MadeUI();

            resourceTileGridBar = new ResourceTileGridBar(myScene, actionBar, placeTile);
            resourceTileGridBar.MadeUI();
            resourceTileGridBar.mainGameObject.SetIsActive(false);

            buildingTileGridBar = new BuildingTileGridBar(myScene, actionBar, placeTile);
            buildingTileGridBar.MadeUI();
            buildingTileGridBar.mainGameObject.SetIsActive(false);

            unitTileGridBar = new UnitTileGridBar(myScene, actionBar, placeTile);
            unitTileGridBar.MadeUI();
            unitTileGridBar.mainGameObject.SetIsActive(false);

            loadSaveUIBar = new LoadSaveUIBar(myScene,tileGrid);
            loadSaveUIBar.MakeUI();
            loadSaveUIBar.mainGameObject.SetIsActive(true);

            groundBtn = TopButtom(new Vector2(-270, -30), "Ground Tile");
            resourcesBtn = TopButtom(new Vector2(-150, -30), "Resources");
            builingsBtn = TopButtom(new Vector2(-40, -30), "Buildings");
            unitBtn = TopButtom(new Vector2(58, -30), "Units");

            knightBtn = ShowFaction(new Vector2(-210, 15), EFaction.Knights);
            vikingBtn = ShowFaction(new Vector2(-210, 50), EFaction.Vikings);

            team01Btn = ShowTeam(new Vector2(-110, 15), ETeam.Team01);
            team02Btn = ShowTeam(new Vector2(-110, 45), ETeam.Team02);
            team03Btn = ShowTeam(new Vector2(-110, 75), ETeam.Team03);

            groundBtn.OnClick += () => { OverLayButtom(groundBtn); groundTile.mainGameObject.SetIsActive(true); };
            resourcesBtn.OnClick += () => { OverLayButtom(resourcesBtn); resourceTileGridBar.mainGameObject.SetIsActive(true); };
            builingsBtn.OnClick += () => { OverLayButtom(builingsBtn); buildingTileGridBar.mainGameObject.SetIsActive(true); };
            unitBtn.OnClick += () => { OverLayButtom(unitBtn); unitTileGridBar.mainGameObject.SetIsActive(true); };

            // Set Start Color
            OverLayButtom(groundBtn);
            ResetColorOnTeam(team01Btn);
            SetColorOnFaction(knightBtn);
            groundTile.mainGameObject.SetIsActive(true);
        }

        private void ResetColorOnTeam(GUIButton btn)
        {
            team01Btn.Color = Color.White;
            team02Btn.Color = Color.White;
            team03Btn.Color = Color.White;

            btn.Color = Color.Red;
        }

        private void SetColorOnFaction(GUIButton btn)
        {
            knightBtn.Color = Color.White;
            vikingBtn.Color = Color.White;

            btn.Color = Color.Red;
        }

        private void OverLayButtom(GUIButton btn)
        {
            groundBtn.Color = Color.White;
            resourcesBtn.Color = Color.White;
            builingsBtn.Color = Color.White;
            unitBtn.Color = Color.White;

            groundTile.mainGameObject.SetIsActive(false);
            resourceTileGridBar.mainGameObject.SetIsActive(false);
            buildingTileGridBar.mainGameObject.SetIsActive(false);
            unitTileGridBar.mainGameObject.SetIsActive(false);

            btn.Color = Color.Red;
        }

        private GameObject TheActionBar()
        {
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer("ActionBar");
            GUIImage image = new GUIImage(sr, false, true, Color.White, EOriginPosition.BottomMid, 0.1f);

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<GUIImage>(image);

            sr.LayerDepth = 0.02f;
            go.Transform.Scale = new Vector2(0.55f, 0.5f) * GraphicsSetting.Instance.ScreenScale;
            go.Transform.Position = new Vector2(GraphicsSetting.Instance.ScreenSize.X / 2, GraphicsSetting.Instance.ScreenSize.Y * 1);
            Console.WriteLine(go.Transform.Position);
            myScene.Instantiate(go);

            return go;
        }

        private GUIButton TopButtom(Vector2 offSet, string text)
        {
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer("SlotNameBar");
            Texture2D texture1 = SpriteContainer.Instance.Sprite["SlotNameBar"];
            Texture2D texture2 = SpriteContainer.Instance.Sprite["SlotNameBar"];
            GUIButton btn = new GUIButton(sr, texture1, texture2, Color.White, Color.YellowGreen);

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<GUIButton>(btn);

            float newScale = SpriteContainer.Instance.NormalFont.MeasureString(text).X / texture1.Width;
            Vector2 scale = new Vector2(newScale, 3);

            sr.LayerDepth = 0.01f;
            sr.SetOrigin(EOriginPosition.TopLeft);
            go.MyParent = actionBar;
            go.Transform.Scale = new Vector2(0.5f, 0.5f) * GraphicsSetting.Instance.ScreenScale * scale;
            go.Transform.Position = go.MyParent.Transform.Position + new Vector2(offSet.X * GraphicsSetting.Instance.ScreenScale.X, -go.MyParent.GetComponent<CSpriteRenderer>().Sprite.Height * go.MyParent.Transform.Scale.Y + (offSet.Y * GraphicsSetting.Instance.ScreenScale.Y));

            TextToSlotBar(text, new Vector2(sr.Sprite.Width, sr.Sprite.Height) * go.Transform.Scale, go, EOriginPosition.TopMid, new Vector2(0, 10));

            myScene.Instantiate(go);

            return btn;
        }

        private void TextToSlotBar(string text, Vector2 size, GameObject myParent, EOriginPosition originPosition, Vector2 padding)
        {
            GameObject go = new GameObject();
            GUIText guiText = new GUIText(SpriteContainer.Instance.NormalFont, Color.White, new Vector2(0.4f, 0.4f), text, originPosition);

            go.AddComponent<GUIText>(guiText);

            guiText.LayerDepth = 0.13f;
            guiText.FontScale = guiText.FontScale * GraphicsSetting.Instance.ScreenScale;
            guiText.OffsetPosition = new Vector2(padding.X * GraphicsSetting.Instance.ScreenScale.X, padding.Y * GraphicsSetting.Instance.ScreenScale.Y);
            go.MyParent = myParent;
            go.Transform.Scale = size;
            go.Transform.Position = myParent.Transform.Position;

            myScene.Instantiate(go);
        }

        private GameObject ActionBarToFactionAndTeam()
        {
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer("ActionBar");
            GUIImage image = new GUIImage(sr, false, true, Color.White, EOriginPosition.BottomRight, 0.1f);

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<GUIImage>(image);

            sr.LayerDepth = 0.02f;
            go.Transform.Scale = new Vector2(0.2f, 0.5f) * GraphicsSetting.Instance.ScreenScale;
            go.Transform.Position = new Vector2(GraphicsSetting.Instance.ScreenSize.X, GraphicsSetting.Instance.ScreenSize.Y * 1);

            myScene.Instantiate(go);

            return go;
        }

        private GUIButton ShowFaction(Vector2 offSet, EFaction faction)
        {
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer("SlotNameBar");
            Texture2D texture1 = SpriteContainer.Instance.Sprite["SlotNameBar"];
            Texture2D texture2 = SpriteContainer.Instance.Sprite["SlotNameBar"];
            GUIButton btn = new GUIButton(sr, texture1, texture2, Color.White, Color.YellowGreen);

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<GUIButton>(btn);

            float newScale = SpriteContainer.Instance.NormalFont.MeasureString(faction.ToString()).X / texture1.Width;
            Vector2 scale = new Vector2(newScale, 0.8f);

            sr.LayerDepth = 0.03f;
            sr.SetOrigin(EOriginPosition.TopLeft);
            go.MyParent = actionBarFactionAndTeam;
            go.Transform.Scale = new Vector2(0.5f, 0.5f) * GraphicsSetting.Instance.ScreenScale * scale;
            go.Transform.Position = go.MyParent.Transform.Position + new Vector2(offSet.X * GraphicsSetting.Instance.ScreenScale.X, -go.MyParent.GetComponent<CSpriteRenderer>().Sprite.Height * go.MyParent.Transform.Scale.Y + (offSet.Y * GraphicsSetting.Instance.ScreenScale.Y));

            TextToSlotBar(faction.ToString(), new Vector2(sr.Sprite.Width, sr.Sprite.Height) * go.Transform.Scale, go, EOriginPosition.Mid, new Vector2(0, 0));

            myScene.Instantiate(go);

            btn.OnClick += () => { placeTile.Faction = faction; placeTile.GameObjectTileMouse.IsActive = false; SetColorOnFaction(btn); };

            return btn;
        }

        private GUIButton ShowTeam(Vector2 offSet, ETeam team)
        {
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer("SlotNameBar");
            Texture2D texture1 = SpriteContainer.Instance.Sprite["SlotNameBar"];
            Texture2D texture2 = SpriteContainer.Instance.Sprite["SlotNameBar"];
            GUIButton btn = new GUIButton(sr, texture1, texture2, Color.White, Color.YellowGreen);

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<GUIButton>(btn);

            float newScale = SpriteContainer.Instance.NormalFont.MeasureString(team.ToString()).X / texture1.Width;
            Vector2 scale = new Vector2(newScale, 0.6f);

            sr.LayerDepth = 0.03f;
            sr.SetOrigin(EOriginPosition.TopLeft);
            go.MyParent = actionBarFactionAndTeam;
            go.Transform.Scale = new Vector2(0.5f, 0.5f) * GraphicsSetting.Instance.ScreenScale * scale;
            go.Transform.Position = go.MyParent.Transform.Position + new Vector2(offSet.X * GraphicsSetting.Instance.ScreenScale.X, -go.MyParent.GetComponent<CSpriteRenderer>().Sprite.Height * go.MyParent.Transform.Scale.Y + (offSet.Y * GraphicsSetting.Instance.ScreenScale.Y));

            TextToSlotBar(team.ToString(), new Vector2(sr.Sprite.Width, sr.Sprite.Height) * go.Transform.Scale, go, EOriginPosition.Mid, new Vector2(0, 0));

            myScene.Instantiate(go);

            btn.OnClick += () => { placeTile.Team = team; placeTile.GameObjectTileMouse.IsActive = false; ResetColorOnTeam(btn); };

            return btn;
        }
    }
}
