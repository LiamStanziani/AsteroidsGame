using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/* ScrollingBackground.cs
 * Asteroids
 * Revision History
 * Liam Stanziani & Nathan Garrity, 2022.11.29: Created
 * Liam Stanziani & Nathan Garrity, 2022.11.29: Added Code
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Debugging complete
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Comments added
*/

namespace Asteroids
{
    public class ScrollingBackground : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Rectangle srcRect;
        private Vector2 pos1, pos2;
        private Vector2 speed;

        /// <summary>
        /// A constructor for the ScrollingBackground class
        /// </summary>
        /// <param name="game">A variable for the Game class</param>
        /// <param name="spriteBatch">A variable that is a SpriteBatch</param>
        /// <param name="tex">A variable that is a Texture2D</param>
        /// <param name="srcRect">A variable that is a Rectangle</param>
        /// <param name="position">A variable that is a Vector</param>
        /// <param name="speed">A variable that is a Vector</param>
        public ScrollingBackground(Game game, SpriteBatch spriteBatch, Texture2D tex, Rectangle srcRect, Vector2 position, Vector2 speed) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.srcRect = srcRect;
            this.pos1 = position;
            this.pos2 = new Vector2(pos1.X + srcRect.Width, pos1.Y);
            this.speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            pos1 -= speed;
            pos2 -= speed;

            if (pos1.X < -srcRect.Width)
            {
                pos1.X = pos2.X + srcRect.Width;
            }
            if (pos2.X < -srcRect.Width)
            {
                pos2.X = pos1.X + srcRect.Width;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, pos1, srcRect, Color.White);
            spriteBatch.Draw(tex, pos2, srcRect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
