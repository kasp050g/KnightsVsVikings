using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    public class SceneController
    {
        #region Singleton
        private static SceneController instance;
        public static SceneController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SceneController();
                }
                return instance;
            }
        }
        #endregion

        private SceneContainer sceneContainer = new SceneContainer();
        private CameraFollow camera = new CameraFollow();
        private Scene currentScene;

        public CameraFollow Camera { get => camera; set => camera = value; }
        public SceneContainer SceneContainer { get => sceneContainer; set => sceneContainer = value; }
        // TODO : fix
        public Scene CurrentScene
        {
            get
            {
                return currentScene;
            }
            set
            {
                if (currentScene != value)
                {
                    if (currentScene != null)
                    {
                        // Tells the previous scene that we're switching to a different scene
                        currentScene.OnSwitchAwayFromThisScene();
                    }

                    // Changes the scene
                    currentScene = value;
                    currentScene.OnSwitchToThisScene();

                    // Tells the camera that the scene has changed
                    Camera.OnSwitchScene();
                }
            }
        }

        public void Initialize()
        {
            SceneContainer.Initialize();
            CurrentScene = SceneContainer.Scenes[0];
        }

        public void UpdateScenes()
        {
            CurrentScene.Update();

            // Update all Scenes with UpdateEnabled is true, if it is the current Scene dont update it 2 times.
            foreach (Scene scene in SceneContainer.Scenes)
            {
                if (scene.UpdateEnabled == true)
                {
                    if (CurrentScene != scene)
                    {
                        scene.Update();
                    }
                }
            }
        }

        public void DrawScenes(SpriteBatch spriteBatch)
        {
            CurrentScene.Draw(spriteBatch);

            // Draw all Scenes with DrawEnabled is true, if it is the current Scene dont Draw it 2 times.
            foreach (Scene scene in SceneContainer.Scenes)
            {
                if (scene.DrawEnabled == true)
                {
                    if (CurrentScene != scene)
                    {
                        scene.Draw(spriteBatch);
                    }
                }
            }
        }
    }
}
