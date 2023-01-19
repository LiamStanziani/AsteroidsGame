using DocumentFormat.OpenXml.Presentation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* HighScoreScene.cs
 * Asteroids
 * Revision History
 * Liam Stanziani & Nathan Garrity, 2022.12.03: Created
 * Liam Stanziani & Nathan Garrity, 2022.12.03: Added Code
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Debugging complete
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Comments added
*/

namespace Asteroids
{
    public class HighScoreScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Game1 g;
        private Texture2D tex;
        private SpriteFont font;
        private HighScore hs;
        public static float xyIncreaser = 100;
        private const int MAX_NUM_OF_SCORES = 10;
        public static int highScores;
        public static int storedHighScore;
        public static string[] allHighScores;

        /// <summary>
        /// A constructor for the HighScoreScene class
        /// </summary>
        /// <param name="game">A variable for the Game class</param>
        public HighScoreScene(Game game) : base(game)
        {
            this.g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            font = game.Content.Load<SpriteFont>("fonts/regularFont");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //spriteBatch.Draw(tex, Vector2.Zero, Color.White);
            hs = new HighScore(storedHighScore);
            try
            {
                for (int i = 0; i < MAX_NUM_OF_SCORES; i++)
                {
                    spriteBatch.DrawString(font, hs.allHighScores[i], new Vector2(xyIncreaser, xyIncreaser * i / 2 + xyIncreaser), Color.White);
                }
            }
            catch (IndexOutOfRangeException)
            {
                Debug.WriteLine("Probably deleted some lines from the txt file, it should be 11 lines altogether");
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
