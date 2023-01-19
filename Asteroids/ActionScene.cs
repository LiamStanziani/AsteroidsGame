using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroids;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Asteroids;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using SharpDX.Direct2D1;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

/* ActionScene.cs
 * Asteroids
 * Revision History
 * Liam Stanziani & Nathan Garrity, 2022.11.29: Created
 * Liam Stanziani & Nathan Garrity, 2022.11.29: Added Code
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Debugging complete
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Comments added
*/

namespace Asteroids
{
	public class ActionScene : GameScene
	{
		private SpriteBatch spriteBatch;
		private Game1 g;
		private Spaceship spaceShip;
		private Missile missile;
		private Asteroid asteroid;
        private SpriteFont font;
        private HighScore hs;
        public static int storedHighScore;
        private GameManager gm;
		private GameEndScene gameEndScene;
        private bool isSpaceshipActive = false;

        /// <summary>
        /// A constructor for the ActionScene class
        /// </summary>
        /// <param name="game">A variable for the Game class</param>
        public ActionScene(Game game) : base(game)
		{ 
		    g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            font = game.Content.Load<SpriteFont>("fonts/regularFont");
            SoundEffect shootSound = game.Content.Load<SoundEffect>("sounds/shootsound");
				SoundEffect hitSound = game.Content.Load<SoundEffect>("sounds/hitsound");
            //instantiate all game components
            
                Texture2D spaceshipTex = game.Content.Load<Texture2D>("images/spaceship");

                Vector2 spaceshipPos = new Vector2(Shared.stage.X / 2 - spaceshipTex.Width / 2,
					Shared.stage.Y / 2 - spaceshipTex.Height / 2);
				Vector2 spaceShipSpeed = new Vector2(5f, 5f);

				Song movementSound = game.Content.Load<Song>("sounds/movement");

            gameEndScene = new GameEndScene(game);
            game.Components.Add(gameEndScene);
            Texture2D missileTex = game.Content.Load<Texture2D>("images/missile");
            Texture2D asteroidTex = game.Content.Load<Texture2D>("images/asteroid");
            Texture2D explosionTex = game.Content.Load<Texture2D>("images/ExplosionSprite");
           
            this.gm = new GameManager(game, spriteBatch, asteroidTex, spaceShip, asteroid, hitSound, missileTex, missile, shootSound,
				explosionTex, spaceshipTex, spaceshipPos, spaceShipSpeed, movementSound, gameEndScene,  this);
            this.Components.Add(this.gm);

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //spriteBatch.Draw(tex, Vector2.Zero, Color.White);
            if (isSpaceshipActive == false)
            {
                storedHighScore = 0;
                hs = new HighScore(storedHighScore);
                spriteBatch.DrawString(font, "Curent Score: " + storedHighScore.ToString(), new Vector2(60, 830), Color.White);
                isSpaceshipActive = true;
            } else
            {
                hs = new HighScore(storedHighScore);
                spriteBatch.DrawString(font, "Curent Score: " + storedHighScore.ToString(), new Vector2(60, 830), Color.White);
            }

            KeyboardState kS = Keyboard.GetState();
            if ( kS.IsKeyDown(Keys.Escape)) {
                isSpaceshipActive = false;
                this.Enabled = false;
                this.Visible = false;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
