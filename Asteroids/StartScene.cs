using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Asteroids;

/* StartScene.cs
 * Asteroids
 * Revision History
 * Liam Stanziani & Nathan Garrity, 2022.11.29: Created
 * Liam Stanziani & Nathan Garrity, 2022.11.29: Added Code
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Debugging complete
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Comments added
*/

namespace Asteroids
{
    public class StartScene : GameScene
    {
        public MenuComponent Menu { get; set; }
        private SpriteBatch spriteBatch;
        string[] menuItems = {
            "Start Game",
            "Help",
            "High Score",
            "Credits",
            "Quit"
        };

        /// <summary>
        /// A constructor for the StartScene class
        /// </summary>
        /// <param name="game">A variable that is for the Game clas</param>
        public StartScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g._spriteBatch;

            SpriteFont regularFont = game.Content.Load<SpriteFont>("fonts/regularFont");
            SpriteFont hilightFont = g.Content.Load<SpriteFont>("fonts/hilightFont");
            SpriteFont titleFont = g.Content.Load<SpriteFont>("fonts/titleFont");

            Menu = new MenuComponent(game, spriteBatch, regularFont, hilightFont, titleFont, menuItems);
            this.Components.Add(Menu);
        }
    }
}
