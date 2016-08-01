using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Game1
{

    public class PigmanPunch : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameManager gameManager;

        public PigmanPunch()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            gameManager = new GameManager();

            LoadLevel("Level1");

            //Spawn PigMan
            gameManager.RegisterGameObject("PigMan", this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            Gravity();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);

            //Draw background
            spriteBatch.Begin();
            spriteBatch.Draw(gameManager.backgroundTexture, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected void LoadLevel(string level)
        {
            gameManager.backgroundTexture = Content.Load<Texture2D>(level);
        }

        protected void Gravity()
        {
            for (int x = 0; x < gameManager.gameObjects.Count; x++)
            {
                //Find out if the object not currently grounded
                if (gameManager.gameObjects[x].position.Y < Window.ClientBounds.Height - gameManager.gameObjects[x].bounds.Height + gameManager.gameObjects[x].groundPosition)
                {
                    gameManager.gameObjects[x].velocity.Y += .2f;
                }
                else
                {
                    if (gameManager.gameObjects[x].velocity.Y > 0)
                    gameManager.gameObjects[x].velocity.Y = 0;
                }
                gameManager.gameObjects[x].position.Y += gameManager.gameObjects[x].velocity.Y;
            }
        }
    }

    public class GameManager
    {
        //Game objects
        public List<GameObject> gameObjects = new List<GameObject>();

        //Background texture
        public Texture2D backgroundTexture;

        public void RegisterGameObject(string objectType, Game game, Vector2 objectPos = default(Vector2))
        {

            switch (objectType)
            {
                case "PigMan":
                    PigMan pigman = new PigMan(game, objectPos);
                    this.gameObjects.Add(pigman);
                    game.Components.Add(pigman);
                    break;
                case "Foot":
                    Foot foot = new Foot(game, objectPos);
                    this.gameObjects.Add(foot);
                    game.Components.Add(foot);
                    break;
                default:
                    return;
            }
        }
    }
}
