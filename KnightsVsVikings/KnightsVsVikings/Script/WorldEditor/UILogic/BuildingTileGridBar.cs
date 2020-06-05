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
    public class BuildingTileGridBar
    {
        public GameObject mainGameObject;
        Scene myScene;
        PlaceTileWithMouse placeTile;

        public BuildingTileGridBar(Scene myScene, GameObject go, PlaceTileWithMouse placeTile)
        {
            this.myScene = myScene;
            this.mainGameObject = new GameObject();
            this.mainGameObject.MyParent = go;
            this.placeTile = placeTile;
        }

        public void MadeUI()
        {
            mainGameObject.Transform.Position = mainGameObject.MyParent.Transform.Position;

            int postion = -275;
            MakeSlot(new Vector2(postion, -100), SpriteContainer.Instance.Sprite["TownHallIcon"], "1", EBuildingType.TownHall);
            postion += 70;
            MakeSlot(new Vector2(postion, -100), SpriteContainer.Instance.Sprite["ArcheryRangeIcon"], "2", EBuildingType.ArcheryRange);
            postion += 70;
            MakeSlot(new Vector2(postion, -100), SpriteContainer.Instance.Sprite["BarracksIcon"], "3", EBuildingType.Barracks);
            postion += 70;
            MakeSlot(new Vector2(postion, -100), SpriteContainer.Instance.Sprite["BlacksmithIcon"], "4", EBuildingType.Blacksmith);
            postion += 70;
            MakeSlot(new Vector2(postion, -100), SpriteContainer.Instance.Sprite["GatheringStationIcon"], "5", EBuildingType.GatheringStation);
            postion += 70;
            MakeSlot(new Vector2(postion, -100), SpriteContainer.Instance.Sprite["TowerIcon"], "6", EBuildingType.Tower);
            postion += 70;
            MakeSlot(new Vector2(postion, -100), SpriteContainer.Instance.Sprite["FieldIcon"], "7", EBuildingType.Field);
            postion += 70;
            MakeSlot(new Vector2(postion, -100), SpriteContainer.Instance.Pixel, "8", EBuildingType.Nothing);

            myScene.Instantiate(mainGameObject);
        }

        public void MakeSlot(Vector2 pos, Texture2D image, string text, EBuildingType tileType)
        {
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer("Slot");
            Texture2D texture1 = SpriteContainer.Instance.Sprite["Slot"];
            Texture2D texture2 = SpriteContainer.Instance.Sprite["Slot"];
            GUIButton btn = new GUIButton(sr, texture1, texture2, Color.White, Color.YellowGreen);

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<GUIButton>(btn);

            sr.LayerDepth = 0.12f;
            go.MyParent = mainGameObject;
            go.SetMyParent(mainGameObject);
            go.Transform.Scale = new Vector2(0.5f, 0.5f) * GraphicsSetting.Instance.ScreenScale;
            go.Transform.Position = pos * GraphicsSetting.Instance.ScreenScale + go.MyParent.Transform.Position;

            SlotShowBar(new Vector2(sr.Sprite.Width / 2, sr.Sprite.Height) * go.Transform.Scale, go, text);
            ImageInSlot(new Vector2(sr.Sprite.Width, sr.Sprite.Height) * go.Transform.Scale, go, image);

            myScene.Instantiate(go);

            btn.OnClick += () => { placeTile.PickTile(tileType); };
        }

        public void ImageInSlot(Vector2 size, GameObject myParent, Texture2D image)
        {
            GameObject go = new GameObject();
            CSpriteRenderer sr = new CSpriteRenderer(image);
            GUIImage GUIImage = new GUIImage(sr, false, false, Color.White, EOriginPosition.Mid, 0.13f);

            go.AddComponent<CSpriteRenderer>(sr);
            go.AddComponent<GUIImage>(GUIImage);

            go.MyParent = myParent;
            go.SetMyParent(myParent);
            go.Transform.Scale = new Vector2(size.X / sr.Sprite.Width * 0.6f, size.Y / sr.Sprite.Height * 0.6f);
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
            go.SetMyParent(myParent);
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
            go.SetMyParent(myParent);
            go.Transform.Scale = size;
            go.Transform.Position = myParent.Transform.Position - size / 2;

            myScene.Instantiate(go);
        }
    }
}
