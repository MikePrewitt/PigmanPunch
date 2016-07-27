using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 
    public class PigmanPunch : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameObjectManager gameObjectManager;

        //// Set the coordinates to draw the sprite at.
        //Vector2 spritePosition = Vector2.Zero;

        //// Store some information about the sprite's motion.
        //Vector2 spriteSpeed = new Vector2(50.0f, 50.0f);

        public PigmanPunch()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            gameObjectManager = new GameObjectManager();
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Spawn PigMan
            RegisterGameObject("PigMan");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            HandleInput();

            Gravity();

            //UpdateObjectPositions();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            for (int x = 0; x < gameObjectManager.gameObjects.Count; x++)
            {
                spriteBatch.Draw(gameObjectManager.gameObjects[x].texture, gameObjectManager.gameObjects[x].position);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected void RegisterGameObject(string objectType, Vector2 objectPos = default(Vector2))
        {
            if (gameObjectManager.gameObjectTextures.ToList().Where(x => x.Key == objectType).Count() == 0)
                gameObjectManager.gameObjectTextures.Add(objectType, Content.Load<Texture2D>(objectType));

            gameObjectManager.gameObjects.Add(new PigMan(gameObjectManager.gameObjectTextures[objectType], Vector2.Zero));
            
        }

        protected void HandleInput()
        {
            foreach (var item in gameObjectManager.gameObjects.Where(x => x.objectType == "PigMan").ToList())
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    item.Jump();
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    item.MoveLeft();
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    item.MoveRight();
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    item.Jump();

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                RegisterGameObject("foot");

        }

        protected void Gravity()
        {
            for (int x = 0; x < gameObjectManager.gameObjects.Count; x++)
            {
                if (gameObjectManager.gameObjects[x].position.Y < 480 - gameObjectManager.gameObjects[x].objectBounds.Height)
                {
                    gameObjectManager.gameObjects[x].velocity.Y += .2f;
                }
                else
                {
                    if (gameObjectManager.gameObjects[x].velocity.Y > 0)
                    gameObjectManager.gameObjects[x].velocity.Y = 0;
                }
                gameObjectManager.gameObjects[x].position.Y += gameObjectManager.gameObjects[x].velocity.Y;
            }
        }
    }

    public class GameObjectManager
    {
        //Game object textures
        public Dictionary<string, Texture2D> gameObjectTextures = new Dictionary<string, Texture2D>();

        //Game objects
        public List<GameObject> gameObjects = new List<GameObject>();
    }

    public class GameObject
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public Rectangle objectBounds;
        public string objectType;

        public void MoveLeft()
        {
            position.X -= 5;
        }

        public void MoveRight()
        {
            position.X += 5;
        }

        public void Jump()
        {
            if (velocity.Y == 0)
                velocity.Y = -10;
        }
    }

    public class PigMan : GameObject
    {
        public PigMan(Texture2D objectTexture, Vector2 objectPosition)
        {
            velocity = Vector2.Zero;
            objectType = "PigMan";
            texture = objectTexture;
            position = objectPosition;
            objectBounds = new Rectangle(0, 0, texture.Width, texture.Height);
        }
    }
}
