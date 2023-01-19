using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/* HelpScene.cs
 * Asteroids
 * Revision History
 * Liam Stanziani & Nathan Garrity, 2022.11.29: Created
 * Liam Stanziani & Nathan Garrity, 2022.11.29: Added Code
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Debugging complete
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Comments added
*/

namespace Asteroids
{
    public class HelpScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Game1 g;
        private Texture2D tex;

        /// <summary>
        /// A constructor for the HelpScene class
        /// </summary>
        /// <param name="game">A variable for the Game class</param>
        public HelpScene(Game game) : base(game)
        {
            this.g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            tex = game.Content.Load<Texture2D>("images/helpPage");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
