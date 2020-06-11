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
    public class LoadSaveUIBar
    {
        public GameObject mainGameObject;

        GameObject actionbar;
        Scene myScene;
        TileGrid tileGrid;
        PlaceTileWithMouse placeTile;

        GUIButton saveBtn;
        GUIButton loadBtn;

        public LoadSaveUIBar(Scene myScene, TileGrid tileGrid)
        {
            this.myScene = myScene;
            this.mainGameObject = new GameObject();
            this.tileGrid = tileGrid;
        }

        public void MakeUI()
        {
            actionbar = ActionBarToFactionAndTeam();
            saveBtn = MakeBtn(new Vector2(-150,20),"Save");
            loadBtn = MakeBtn(new Vector2(-150,60),"Load");

            loadBtn.OnClick += () => { tileGrid.LoadfromSQLite(1); };
            saveBtn.OnClick += () => { tileGrid.SaveToSQLite(); };
        }

        private GameObject ActionBarToFactionAndTeam()
        {
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer("ActionBar");
            GUIImage image = new GUIImage(sr, false, true, Color.White, EOriginPosition.BottomRight, 0.1f);

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<GUIImage>(image);

            sr.LayerDepth = 0.02f;
            go.Transform.Scale = new Vector2(0.5f, 0.8f) * GraphicsSetting.Instance.ScreenScale;
            go.Transform.Position = new Vector2(GraphicsSetting.Instance.ScreenSize.X, 0);
            go.Transform.Rotation = -90f;
            myScene.Instantiate(go);

            return go;
        }

        private GUIButton MakeBtn(Vector2 offSet, string text)
        {
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer("SlotNameBar");
            Texture2D texture1 = SpriteContainer.Instance.Sprite["SlotNameBar"];
            Texture2D texture2 = SpriteContainer.Instance.Sprite["SlotNameBar"];
            GUIButton btn = new GUIButton(sr, texture1, texture2, Color.White, Color.YellowGreen);
            Vector2 padding = new Vector2(0, 0);

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<GUIButton>(btn);

            float newScale = SpriteContainer.Instance.NormalFont.MeasureString(text).X / texture1.Width;
            Vector2 scale = new Vector2(newScale, 0.6f);

            sr.LayerDepth = 0.03f;
            sr.SetOrigin(EOriginPosition.TopLeft);
            go.MyParent = actionbar;
            go.Transform.Scale = new Vector2(0.7f, 0.7f) * GraphicsSetting.Instance.ScreenScale * scale;
            //go.Transform.Scale += new Vector2(padding.X / texture1.Width, padding.Y / texture1.Height) * scale;
            go.Transform.Position = go.MyParent.Transform.Position + new Vector2(offSet.X * GraphicsSetting.Instance.ScreenScale.X,  offSet.Y * GraphicsSetting.Instance.ScreenScale.Y);

            TextToSlotBar(text, new Vector2(sr.Sprite.Width, sr.Sprite.Height) * go.Transform.Scale, go, EOriginPosition.Mid, padding);

            myScene.Instantiate(go);

            return btn;
        }

        private void TextToSlotBar(string text, Vector2 size, GameObject myParent, EOriginPosition originPosition, Vector2 padding)
        {
            GameObject go = new GameObject();
            GUIText guiText = new GUIText(SpriteContainer.Instance.NormalFont, Color.White, new Vector2(0.4f, 0.4f), text, originPosition);

            go.AddComponent<GUIText>(guiText);

            guiText.LayerDepth = 0.15f;
            guiText.FontScale = guiText.FontScale * GraphicsSetting.Instance.ScreenScale;
            guiText.OffsetPosition = new Vector2(padding.X * GraphicsSetting.Instance.ScreenScale.X, padding.Y * GraphicsSetting.Instance.ScreenScale.Y);
            go.MyParent = myParent;
            go.Transform.Scale = size;
            go.Transform.Position = myParent.Transform.Position;

            myScene.Instantiate(go);
        }
    }
}
