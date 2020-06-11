using KnightsVsVikings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSystemFramework
{
    // Kasper  Fly
    public class Scene
    {
        protected string name;
        protected bool updateEnabled;
        protected bool drawEnabled;
        protected bool pauseGame;
        protected bool isMouseOverUI = false;
        protected bool isInitialized;

        protected List<GameObject> gameObjects = new List<GameObject>();
        protected List<GameObject> guis = new List<GameObject>();

        protected List<GameObject> gameObjectsToBeCreated = new List<GameObject>();
        protected List<GameObject> gameObjectsToBeDestroyed = new List<GameObject>();

        public List<CCollider> Colliders { get; set; } = new List<CCollider>();
        public List<CSelectable> SelectedEnabled { get; set; } = new List<CSelectable>();
        public List<GUI> UIColliders { get; set; } = new List<GUI>();
        public string Name { get { return name; } set { name = value; } }
        public bool UpdateEnabled { get { return updateEnabled; } set { updateEnabled = value; } }
        public bool DrawEnabled { get { return drawEnabled; } set { drawEnabled = value; } }
        public bool PauseGame { get => pauseGame; set => pauseGame = value; }
        public bool IsMouseOverUI { get => isMouseOverUI; set => isMouseOverUI = value; }


        public virtual void Initialize()
        {
            isInitialized = true;
        }

        public virtual void OnSwitchToThisScene()
        {
            if (isInitialized == false)
            {
                this.Initialize();
            }
        }

        public virtual void OnSwitchAwayFromThisScene()
        {

        }

        public virtual void Update()
        {
            IsMouseOverUI = false;
            CheckForGUI();
            if (!pauseGame)
            {
                foreach (GameObject gameObject in gameObjects)
                {
                    if (gameObject.IsActive)
                    {
                        if (gameObject.IsFristUpdate)
                        {
                            gameObject.IsFristUpdate = false;
                            gameObject.Start();
                        }
                        gameObject.Update();
                    }
                }
            }

            if (Input.GetKeyDown(Keys.P))
            {
                pauseGame = !pauseGame;
            }

            foreach (GameObject gameObject in guis)
            {
                if (gameObject.IsActive)
                {
                    if (gameObject.IsFristUpdate)
                    {
                        gameObject.IsFristUpdate = false;
                        gameObject.Start();
                    }
                    gameObject.Update();
                }
            }

            ColliderCheck();
            CallDestroyGameObject();
            CallInstantiate();
            SceneController.Instance.Camera.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Draw all GameObjects
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, transformMatrix: SceneController.Instance.Camera.Transform);
            foreach (GameObject item in gameObjects)
            {
                if (item.IsActive)
                {
                    item.Draw(spriteBatch);
                }
            }
            spriteBatch.End();

            // Draw all GUIs
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            foreach (GameObject item in guis)
            {
                if (item.IsActive)
                {
                    item.Draw(spriteBatch);
                }
            }
            spriteBatch.End();
        }

        public void CheckForGUI()
        {
            MouseState currentMouse = MouseSettings.Instance.GetMouseState();
            Rectangle mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);            

            GUI[] tmpColliders = UIColliders.ToArray();

            List<GUI> mousePositionHit = new List<GUI>();

            for (int i = 0; i < tmpColliders.Length; i++)
            {
                if (tmpColliders[i].GameObject.IsActive && (tmpColliders[i].GameObject.MyParent != null ? tmpColliders[i].GameObject.MyParent.IsActive : true) && tmpColliders[i].OnCollisionEnter(mouseRectangle))
                {
                    mousePositionHit.Add(tmpColliders[i]);
                }
            }

            if (mousePositionHit.Count > 0)
            {
                FindLargetsGuiLayerDepth(mousePositionHit.ToArray()).MouseIsHovering = true;
            }
        }

        public GUI FindLargetsGuiLayerDepth(GUI[] classes)
        {
            float currentLargest = 0;
            int largestIndex = 0;

            for (int i = 0; i < classes.Length; i++)
            {
                if (classes[i].LayerDepth > currentLargest)
                {
                    currentLargest = classes[i].LayerDepth;
                    largestIndex = i;
                }
            }

            return classes[largestIndex];
        }

        public void ColliderCheck()
        {
            CCollider[] tmpColliders = Colliders.ToArray();

            for (int i = 0; i < tmpColliders.Length; i++)
            {
                for (int j = 0; j < tmpColliders.Length; j++)
                {
                    tmpColliders[i].OnCollisionEnter(tmpColliders[j]);
                }
            }
        }

        #region Instantiate And Destroy
        /// <summary>
        /// Will instantiate a new gameObject in to the game.
        /// </summary>
        /// <param name="gameObject">The GameObject to be add to game.</param>
        public void Instantiate(GameObject gameObject)
        {
            this.gameObjectsToBeCreated.Add(gameObject);
        }

        /// <summary>
        /// Will destroy this gameobject
        /// </summary>
        /// <param name="gameObject">destroy this gameobject</param>
        public void Destroy(GameObject gameObject)
        {
            this.gameObjectsToBeDestroyed.Add(gameObject);
        }

        /// <summary>
        /// Call this to Destroy all GameObjects
        /// </summary>
        public void DestroyAllGameObjects()
        {
            this.gameObjects.Clear();
            this.guis.Clear();
        }

        /// <summary>
        /// Add all GameObjects To Be Created to current GameObject List.
        /// </summary>
        private void CallInstantiate()
        {
            if (this.gameObjectsToBeCreated.Count > 0)
            {
                List<GameObject> awakeCall = new List<GameObject>();
                awakeCall.AddRange(this.gameObjectsToBeCreated);
                this.gameObjectsToBeCreated.Clear();

                foreach (GameObject go in awakeCall)
                {
                    go.MyScene = this;
                    go.Awake();                    

                    bool gameobjectIsUI = false;
                    foreach (Component x in go.Components.Values)
                    {
                        if (x is GUI && (x as GUI).IsWorldUI == false)
                        {
                            gameobjectIsUI = true;
                        }
                    }

                    if (gameobjectIsUI == true)
                    {
                        guis.Add(go);
                    }
                    else
                    {
                        gameObjects.Add(go);
                    }

                    if (go.GetComponent<CCollider>() != null)
                    {
                        Colliders.Add(go.GetComponent<CCollider>());
                    }      
                    
                    if (go.GetComponent<CSelectable>() != null)
                    {
                        SelectedEnabled.Add(go.GetComponent<CSelectable>());
                    }

                    if (go.GetComponent<GUI>() != null)
                    {
                        foreach (var item in go.Components.Values)
                        {
                            if (item is GUI && (item as GUI).BlockGUI)
                            {
                                UIColliders.Add(item as GUI);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Remove all GameObjects To Be Remove from current GameObject List.
        /// </summary>
        private void CallDestroyGameObject()
        {
            if (this.gameObjectsToBeDestroyed.Count > 0)
            {
                List<GameObject> destroyCall = new List<GameObject>();
                destroyCall.AddRange(this.gameObjectsToBeDestroyed);
                this.gameObjectsToBeDestroyed.Clear();

                foreach (GameObject go in destroyCall)
                {
                    foreach (Component component in go.Components.Values)
                    {
                        component.Destroy();
                    }

                    if (gameObjects.Contains(go))
                    {
                        gameObjects.Remove(go);
                    }
                    else if (guis.Contains(go))
                    {
                        guis.Remove(go);
                    }
                }
            }
        }
        #endregion
    }
}
