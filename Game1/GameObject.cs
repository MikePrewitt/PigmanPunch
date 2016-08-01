using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{

    public class GameObject : DrawableGameComponent
    {
        public Texture2D texture;
        public Vector2 position, velocity;
        public int groundPosition;
        public Rectangle bounds;
        public string objectType;

        SpriteBatch spriteBatch;

        public GameObject(Game game) : base(game)
        {

        }

        public Texture2D Texture
        {
            get { return texture; }
        }

        public Rectangle ObjectBounds
        {
            get { return bounds; }
        }

        public override void Initialize()
        {
            base.Initialize();

            texture = Game.Content.Load<Texture2D>(this.objectType);
            bounds = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.End();
        }

        public void MoveLeft()
        {
            position.X -= 5;
        }

        public void MoveRight()
        {
            position.X += 5;
        }

        public void MoveUp()
        {
            if (velocity.Y == 0)
            {
                position.Y -= 5;
                groundPosition -= 5;
            }
        }

        public void MoveDown()
        {
            if (velocity.Y == 0)
            {
                position.Y += 5;
                groundPosition += 5;
            }
        }

        public void Jump()
        {
            if (velocity.Y == 0)
                velocity.Y = -8;
        }
    }

    public class PigMan : GameObject
    {
        public PigMan(Game game, Vector2 objectPosition) : base(game)
        {
            velocity = Vector2.Zero;
            objectType = "PigMan";
            position = objectPosition;
        }

        public override void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                this.MoveUp();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                this.MoveLeft();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                this.MoveDown();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                this.MoveRight();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                this.Jump();
            }
        }
    }

    public class Foot : GameObject
    {
        public Foot(Game game, Vector2 objectPosition) : base(game)
        {
            velocity = Vector2.Zero;
            objectType = "Foot";
            position = objectPosition;
        }
    }
}
