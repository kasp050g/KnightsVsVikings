using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnightsVsVikings;

namespace MainSystemFramework
{
    public class SceneContainer
    {
        private List<Scene> scenes = new List<Scene>();
        public List<Scene> Scenes { get => scenes; set => scenes = value; }

        public void Initialize()
        {
            MakeScenes();
        }

        public void MakeScenes()
        {
            PickScene pickScene = new PickScene()
            {
                Name = "Pick Scene"
            };
            Scenes.Add(pickScene);

            AsmundScene asmundScene = new AsmundScene()
            {
                Name = "Asmund Scene"
            };
            Scenes.Add(asmundScene);

            LukasScene lukasScene = new LukasScene()
            {
                Name = "Lukas Scene"
            };
            Scenes.Add(lukasScene);

            KasperScene kasperScene = new KasperScene()
            {
                Name = "Kasper Scene"
            };
            Scenes.Add(kasperScene);

            MainMenuScene mainMenuScene = new MainMenuScene()
            {
                Name = "Main Menu Scene"
            };
            Scenes.Add(mainMenuScene);

            GameScene gameScene = new GameScene()
            {
                Name = "Game Scene"
            };
            Scenes.Add(gameScene);

            WorldEditorScene worldEditorScene = new WorldEditorScene()
            {
                Name = "World Editor Scene"
            };
            Scenes.Add(worldEditorScene);
        }
    }
}
