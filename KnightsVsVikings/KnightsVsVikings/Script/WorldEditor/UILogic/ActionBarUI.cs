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
    public class ActionBarUI
    {
        GameObject actionBar;
        Scene myScene;
        public ActionBarUI(Scene myScene)
        {
            this.myScene = myScene;
        }

        public void MakeUI()
        {
            TheBar(ref actionBar);
            MakeSlot(new Vector2(-240, -125), SpriteContainer.Instance.TileSprite.Grass02, "1");
            MakeSlot(new Vector2(-170, -125), SpriteContainer.Instance.TileSprite.Grass03, "2");
            MakeSlot(new Vector2(-100, -125), SpriteContainer.Instance.TileSprite.Water01, "3");
            MakeSlot(new Vector2(-30, -125), SpriteContainer.Instance.TileSprite.Water02, "4");
            MakeSlot(new Vector2(40, -125), SpriteContainer.Instance.TileSprite.Water03, "5");
            MakeSlot(new Vector2(110, -125), SpriteContainer.Instance.TileSprite.Water04, "6");
            MakeSlot(new Vector2(180, -125), SpriteContainer.Instance.TileSprite.Water04, "7");
        }

        public void TheBar(ref GameObject go)
        {
            go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer("ActionBar");
            GUIImage image = new GUIImage(sr, false, true, Color.White, EOriginPosition.BottomMid, 0.1f);
            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<GUIImage>(image);
            go.Transform.Scale = new Vector2(0.5f, 0.5f) * GraphicsSetting.Instance.ScreenScale;
            go.Transform.Position = new Vector2(GraphicsSetting.Instance.ScreenSize.X / 2, GraphicsSetting.Instance.ScreenSize.Y * 1.04f);

            myScene.Instantiate(go);
        }

        public void MakeSlot(Vector2 pos, TextureSheet2D image, string text)
        {
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer("Slot");
            Texture2D texture1 = SpriteContainer.Instance.Sprite["Slot"];
            Texture2D texture2 = SpriteContainer.Instance.Sprite["Slot"];
            GUIButton btn = new GUIButton(sr, texture1, texture2, Color.White, Color.YellowGreen);

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<GUIButton>(btn);

            sr.LayerDepth = 0.12f;
            go.MyParent = actionBar;
            go.Transform.Scale = new Vector2(0.5f, 0.5f) * GraphicsSetting.Instance.ScreenScale;
            go.Transform.Position = pos * GraphicsSetting.Instance.ScreenScale + go.MyParent.Transform.Position;

            SlotShowBar(new Vector2(sr.Sprite.Width / 2, sr.Sprite.Height) * go.Transform.Scale, go, text);
            ImageInSlot(new Vector2(sr.Sprite.Width, sr.Sprite.Height) * go.Transform.Scale, go, image);

            myScene.Instantiate(go);

            btn.OnClick = () => { actionBar.IsActive = false; };
        }

        public void ImageInSlot(Vector2 size, GameObject myParent, TextureSheet2D image)
        {
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer(image);
            GUIImage GUIImage = new GUIImage(sr, false, false, Color.White, EOriginPosition.Mid, 0.13f);
            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<GUIImage>(GUIImage);

            go.MyParent = myParent;
            go.Transform.Scale = new Vector2(size.X / sr.SpriteSheet.Rectangle.Width * 0.6f, size.Y / sr.SpriteSheet.Rectangle.Height * 0.6f);
            go.Transform.Position = go.MyParent.Transform.Position + new Vector2(size.X / 2, size.Y / 2);

            myScene.Instantiate(go);
        }

        public void SlotShowBar(Vector2 pos, GameObject myParent, string text)
        {
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer("SlotNameBar");
            GUIImage image = new GUIImage(sr, false, true, Color.White, EOriginPosition.Mid, 0.11f);
            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<GUIImage>(image);

            go.MyParent = myParent;
            go.Transform.Scale = new Vector2(0.5f, 0.5f) * GraphicsSetting.Instance.ScreenScale;
            go.Transform.Position = pos + go.MyParent.Transform.Position;

            TextToSlotBar(text, new Vector2(sr.Sprite.Width, sr.Sprite.Height) * go.Transform.Scale, go);
            myScene.Instantiate(go);
        }

        public void TextToSlotBar(string text, Vector2 size, GameObject myParent)
        {
            GameObject go = new GameObject();
            GUIText guiText = new GUIText(SpriteContainer.Instance.NormalFont, Color.White, new Vector2(0.4f, 0.4f), text, EOriginPosition.BottomMid);
            go.AddComponent<GUIText>(guiText);

            guiText.LayerDepth = 0.13f;
            guiText.FontScale = guiText.FontScale * GraphicsSetting.Instance.ScreenScale;
            go.MyParent = myParent;
            go.Transform.Scale = size;
            go.Transform.Position = myParent.Transform.Position - size / 2;

            myScene.Instantiate(go);
        }
    }
}
