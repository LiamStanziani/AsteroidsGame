using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroids;
using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using Mouse = Microsoft.Xna.Framework.Input.Mouse;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Reflection;
using SharpDX.MediaFoundation;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System.Reflection.Metadata;

/* SpaceShip.cs
 * Asteroids
 * Revision History
 * Liam Stanziani & Nathan Garrity, 2022.11.29: Created
 * Liam Stanziani & Nathan Garrity, 2022.11.29: Added Code
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Debugging complete
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Comments added
*/

namespace Asteroids
{
    public class Spaceship : DrawableGameComponent
    {
        public SpriteBatch spriteBatch { get; set; }

        public Vector2 position;
        public Vector2 speed { get; set; }
        public Texture2D tex { get; set; }
        public Vector2 direction { get; set; }
        public Vector2 scale { get; set; }

        public Vector2 origin { get; set; }

        public float rotation { get; set; }

		private Rectangle rect;

        Song movementSound { get; set; }

        SoundEffect hitSound { get; set; }

        public int i = 0;
        public int timer = 0;

        /// <summary>
        /// A constructor for the Spaceship class
        /// </summary>
        /// <param name="game">A variable that is for the Game class</param>
        /// <param name="spriteBatch">A variable that is a SpriteBatch</param>
        /// <param name="position">A variable that is a vector</param>
        /// <param name="speed">A variable that is a vector</param>
        /// <param name="tex">A variable that is a Texture2D</param>
        /// <param name="movementSound">A variable that is a Song</param>
        /// <param name="hitSound">A variable that is a SoundEffect</param>
		public Spaceship(Game game,
            SpriteBatch spriteBatch,
            Vector2 position,
            Vector2 speed,
            Texture2D tex,
            Song movementSound,
            SoundEffect hitSound) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.position = position;
            this.speed = speed;
            this.tex = tex;
            this.scale = new Vector2(1, 1);
            this.origin = new Vector2(tex.Width / 2, tex.Height /2);
            rect = new Rectangle(0,0, tex.Width, tex.Height);
            this.movementSound = movementSound;
            this.hitSound = hitSound;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                rotation -= 0.08f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                rotation += 0.08f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                position += direction * speed;
                base.Update(gameTime);
            }
            while (i < 1) // play sound while i < 1
            {
                MediaPlayer.Play(movementSound);
                MediaPlayer.IsRepeating = true;
                i++; // increment i so sound only plays once
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Up))
            {
            	i = 0;
                MediaPlayer.Stop();
            }

            if (position.X > 1250) { position.X = -50; }
            if (position.X < -50) { position.X = 1250; }
            if (position.Y > 950) { position.Y = -50; }
            if (position.Y < -50) { position.Y = 950; }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Enabled = false;
                this.Visible = false;
                MediaPlayer.Stop();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, rect, Color.White, rotation, origin, scale, SpriteEffects.None, 1f);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// A method that gets the bounds for the spaceship
        /// </summary>
        /// <returns>A new rectangle that will be the bounds for the spaceship</returns>
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
