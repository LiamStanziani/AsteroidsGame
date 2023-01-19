using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Presentation;
using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;
using Keys = Microsoft.Xna.Framework.Input.Keys;

/* GameEndScene.cs
 * Asteroids
 * Revision History
 * Liam Stanziani & Nathan Garrity, 2022.12.07: Created
 * Liam Stanziani & Nathan Garrity, 2022.12.07: Added Code
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Debugging complete
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Comments added
*/

namespace Asteroids
{
    public class GameEndScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Game1 g;
        private Texture2D tex;
        private SpriteFont font;
        public static string playerName = "";
        private StartScene startScene;
        private HighScore hs;
        public static int highScore;
        private KeyboardState _currentks;
        private KeyboardState _previousks;
        private string value;
        public static bool pressedEnter;

        /// <summary>
        /// A constructor for the GameEndSceneClass
        /// </summary>
        /// <param name="game">A variable that is for the Game class</param>
        public GameEndScene(Game game) : base(game)
        {
            this.g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            font = game.Content.Load<SpriteFont>("fonts/regularFont");
            tex = game.Content.Load<Texture2D>("images/gameOverScreen");
            pressedEnter = false;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, Vector2.Zero, Color.White);
            if (pressedEnter == true) 
            {
               
            }
            else
            {
                spriteBatch.DrawString(font, "Input your name and press enter to store your highscore", new Vector2(20, 100), Color.White);
                spriteBatch.DrawString(font, playerName, new Vector2(150, 150), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            _currentks = Keyboard.GetState();
            Keys[] keys = _currentks.GetPressedKeys();
            if (pressedEnter == false && _currentks.IsKeyDown(Keys.A) && _previousks.IsKeyUp(Keys.A) || _currentks.IsKeyDown(Keys.B) && _previousks.IsKeyUp(Keys.B) || _currentks.IsKeyDown(Keys.C) && _previousks.IsKeyUp(Keys.C) || _currentks.IsKeyDown(Keys.D) && _previousks.IsKeyUp(Keys.D) || _currentks.IsKeyDown(Keys.E) && _previousks.IsKeyUp(Keys.E) || _currentks.IsKeyDown(Keys.F) && _previousks.IsKeyUp(Keys.F) || _currentks.IsKeyDown(Keys.G) && _previousks.IsKeyUp(Keys.G) || _currentks.IsKeyDown(Keys.H) && _previousks.IsKeyUp(Keys.H) || _currentks.IsKeyDown(Keys.I) && _previousks.IsKeyUp(Keys.I) || _currentks.IsKeyDown(Keys.J) && _previousks.IsKeyUp(Keys.J) || _currentks.IsKeyDown(Keys.K) && _previousks.IsKeyUp(Keys.K) || _currentks.IsKeyDown(Keys.L) && _previousks.IsKeyUp(Keys.L) || _currentks.IsKeyDown(Keys.M) && _previousks.IsKeyUp(Keys.M) || _currentks.IsKeyDown(Keys.N) && _previousks.IsKeyUp(Keys.N) || _currentks.IsKeyDown(Keys.O) && _previousks.IsKeyUp(Keys.O) || _currentks.IsKeyDown(Keys.P) && _previousks.IsKeyUp(Keys.P) || _currentks.IsKeyDown(Keys.Q) && _previousks.IsKeyUp(Keys.Q) || _currentks.IsKeyDown(Keys.R) && _previousks.IsKeyUp(Keys.R) || _currentks.IsKeyDown(Keys.S) && _previousks.IsKeyUp(Keys.S) || _currentks.IsKeyDown(Keys.T) && _previousks.IsKeyUp(Keys.T) || _currentks.IsKeyDown(Keys.U) && _previousks.IsKeyUp(Keys.U) || _currentks.IsKeyDown(Keys.V) && _previousks.IsKeyUp(Keys.V) || _currentks.IsKeyDown(Keys.W) && _previousks.IsKeyUp(Keys.W) || _currentks.IsKeyDown(Keys.X) && _previousks.IsKeyUp(Keys.X) || _currentks.IsKeyDown(Keys.Y) && _previousks.IsKeyUp(Keys.Y) || _currentks.IsKeyDown(Keys.Z) && _previousks.IsKeyUp(Keys.Z))
            {
                value = keys[0].ToString();
                playerName += value;
            }
            if (pressedEnter == false && _currentks.IsKeyDown(Keys.Back) && _previousks.IsKeyUp(Keys.Back))
            {
                try
                {
                    playerName = playerName.Remove(playerName.Length - 1, 1);
                }
                catch (ArgumentOutOfRangeException)
                {

                }
            }
            if (pressedEnter == false && _currentks.IsKeyDown(Keys.Enter))
            {
                hs = new HighScore(highScore);
                hs.StoreHighScore(highScore, playerName);
                pressedEnter = true;

            }

            _previousks = _currentks;

            base.Update(gameTime);
        }
    }
}
