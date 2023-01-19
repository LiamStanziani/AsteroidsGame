using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SharpDX.Direct3D9;
using Asteroids;

/* MenuComponent.cs
 * Asteroids
 * Revision History
 * Liam Stanziani & Nathan Garrity, 2022.11.29: Created
 * Liam Stanziani & Nathan Garrity, 2022.11.29: Added Code
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Debugging complete
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Comments added
*/

namespace Asteroids
{
    public class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont regularFont, hilightFont, titleFont;
        private Color regularColor = Color.White;
        private Color hilightColor = Color.Red;
        private List<string> menuItems;
        public int selectedIndex { get; set; }
        private Vector2 position;

        private KeyboardState oldState;

        /// <summary>
        /// A constructor for the MenuComponent class
        /// </summary>
        /// <param name="game">A variable for the Game class</param>
        /// <param name="spriteBatch">A variable that is a SpriteBatch</param>
        /// <param name="regularFont">A variable that is a SpriteFont</param>
        /// <param name="hilightFont">A variable that is a SpriteFont</param>
        /// <param name="titleFont">A variable that is a SpriteFont</param>
        /// <param name="menus">A variable that is an array</param>
        public MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont regularFont, SpriteFont hilightFont, SpriteFont titleFont, string[] menus) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.hilightFont = hilightFont;
            this.titleFont = titleFont;
            menuItems = menus.ToList<string>();
            position = new Vector2(Shared.stage.X / 2 , Shared.stage.Y / 2);

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            Vector2 tempPos = position;
            Vector2 titlePos = new Vector2(100, 160);
            spriteBatch.DrawString(titleFont, "Asteroids", titlePos, regularColor);
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (selectedIndex == i)
                {
                    spriteBatch.DrawString(hilightFont, menuItems[i], tempPos, hilightColor);
                    tempPos.Y += hilightFont.LineSpacing;
                }
                else
                {
                    spriteBatch.DrawString(regularFont, menuItems[i], tempPos, regularColor);
                    tempPos.Y += regularFont.LineSpacing;
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Count)
                {
                    selectedIndex = 0;
                }
            }
            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex == -1)
                {
                    selectedIndex = menuItems.Count - 1;
                }
            }
            oldState = ks;

            base.Update(gameTime);
        }
    }
}
