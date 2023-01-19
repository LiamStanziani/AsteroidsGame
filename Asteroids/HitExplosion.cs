using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

/* HitExplosion.cs
 * Asteroids
 * Revision History
 * Liam Stanziani & Nathan Garrity, 2022.12.06: Created
 * Liam Stanziani & Nathan Garrity, 2022.12.06: Added Code
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Debugging complete
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Comments added
*/

namespace Asteroids
{
    public class HitExplosion : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 dimension;
        private Vector2 origin;
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;

        private const int ROWS = 2;
        private const int COLS = 5;

        /// <summary>
        /// A constructor for the HitExplosion class
        /// </summary>
        /// <param name="game">A variable for the Game class</param>
        /// <param name="spriteBatch">A variable that is a SpriteBatch</param>
        /// <param name="tex">A variable that is a Texture2D</param>
        /// <param name="position">A variable that is a Vector</param>
        /// <param name="delay">A variable that is an int</param>
        /// <param name="origin">A variable that is a Vector</param>
        public HitExplosion(Game game, SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            int delay, Vector2 origin) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.delay = delay;
            this.origin = origin;
            this.dimension = new Vector2(tex.Width / COLS, tex.Height / ROWS);

            hide();
            //create all frames
            createFrames();
        }

        /// <summary>
        /// A method that will create the frames for the explosion animation
        /// </summary>
        private void createFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y,
                        (int)dimension.X, (int)dimension.Y);
                    frames.Add(r);
                }
            }
        }

        /// <summary>
        /// A method that will hide the animation
        /// </summary>
        private void hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        /// <summary>
        /// A method that will show the animation
        /// </summary>
        private void show()
        {
            this.Enabled = true;
            this.Visible = true;
        }

        /// <summary>
        /// A that will restart the animatin
        /// </summary>
        public void restart()
        {
            frameIndex = -1;
            delayCounter = 0;
            show();
        }

        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > ROWS * COLS - 1)
                {
                    frameIndex = -1;
                    hide();
                }
                delayCounter = 0;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (frameIndex >= 0)
            {
                //v4
                spriteBatch.Draw(tex, position, frames[frameIndex], Color.White, 0, origin * 4, 0.6F, SpriteEffects.None, 1f);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
