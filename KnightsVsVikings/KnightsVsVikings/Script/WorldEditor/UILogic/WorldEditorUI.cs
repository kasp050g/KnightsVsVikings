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
    public class WorldEditorUI
    {
        Scene myScene;
        GameObject actionBar;
        GroundTileGridBar groundTile;
        ResourceTileGridBar resourceTileGridBar;
        BuildingTileGridBar buildingTileGridBar;

        GUIButton groundBtn;
        GUIButton resourcesBtn;
        GUIButton builingsBtn;

        PlaceTileWithMouse placeTile;

        public WorldEditorUI(Scene myScene, PlaceTileWithMouse placeTile)
        {
            this.myScene = myScene;
            this.placeTile = placeTile;
        }

        public void MadeUI()
        {
            actionBar = TheActionBar();

            groundTile = new GroundTileGridBar(myScene, actionBar, placeTile);
            groundTile.MadeUI();

            resourceTileGridBar = new ResourceTileGridBar(myScene, actionBar, placeTile);
            resourceTileGridBar.MadeUI();
            resourceTileGridBar.mainGameObject.SetIsActive(false);

            buildingTileGridBar = new BuildingTileGridBar(myScene, actionBar, placeTile);
            buildingTileGridBar.MadeUI();
            buildingTileGridBar.mainGameObject.SetIsActive(false);

            groundBtn = TopButtom(new Vector2(-270, -30), "Ground Tile");
            resourcesBtn = TopButtom(new Vector2(-150, -30), "Resources");
            builingsBtn = TopButtom(new Vector2(-40, -30), "Buildings");


            groundBtn.OnClick += () => { OverLayButtom(); groundTile.mainGameObject.SetIsActive(true); };
            resourcesBtn.OnClick += () => { OverLayButtom(); resourceTileGridBar.mainGameObject.SetIsActive(true); };
            builingsBtn.OnClick += () => { OverLayButtom(); buildingTileGridBar.mainGameObject.SetIsActive(true); };
        }

        private void OverLayButtom()
        {
            groundTile.mainGameObject.SetIsActive(false);
            resourceTileGridBar.mainGameObject.SetIsActive(false);
            buildingTileGridBar.mainGameObject.SetIsActive(false);
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

            TextToSlotBar(text, new Vector2(sr.Sprite.Width, sr.Sprite.Height) * go.Transform.Scale, go);

            myScene.Instantiate(go);

            return btn;
        }

        private void TextToSlotBar(string text, Vector2 size, GameObject myParent)
        {
            GameObject go = new GameObject();
            GUIText guiText = new GUIText(SpriteContainer.Instance.NormalFont, Color.White, new Vector2(0.4f, 0.4f), text, EOriginPosition.TopMid);

            go.AddComponent<GUIText>(guiText);

            guiText.LayerDepth = 0.13f;
            guiText.FontScale = guiText.FontScale * GraphicsSetting.Instance.ScreenScale;
            guiText.OffsetPosition = new Vector2(0, 10 * GraphicsSetting.Instance.ScreenScale.X);
            go.MyParent = myParent;
            go.Transform.Scale = size;
            go.Transform.Position = myParent.Transform.Position;

            myScene.Instantiate(go);
        }
    }
}
