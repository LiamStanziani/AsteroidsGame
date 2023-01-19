using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

/* Game1.cs
 * Asteroids
 * Revision History
 * Liam Stanziani & Nathan Garrity, 2022.12.01: Created
 * Liam Stanziani & Nathan Garrity, 2022.12.01: Added Code
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Debugging complete
 * Liam Stanziani & Nathan Garrity, 2022.12.08: Comments added
*/

namespace Asteroids
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        

        private StartScene startScene;
        private HelpScene helpScene;
        private ActionScene actionScene;
        private HighScoreScene highScoreScene;
        private AboutScene aboutScene;
        private GameManager gm;
        private GameEndScene gameEndScene;

        /// <summary>
        /// A constructor for the Game1 class
        /// </summary>
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.PreferredBackBufferWidth = 1200;
        }
        
        /// <summary>
        /// A method that hides all of the GameComponent items, which will hide all of the menu items
        /// </summary>
        public void hideAllScenes()
        {
            foreach (GameComponent item in Components)
            {
                if (item is GameScene)
                {
                    GameScene gs = (GameScene)item;
                    gs.hide();
                }
                
            }

        }

        /// <summary>
        /// A method that will initilize the Game stage height and width
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Shared.stage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        /// <summary>
        /// A method that will load all of the background images, menu songs, and will link to the all of the different classes
        /// that are used for the menu options
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here

			Texture2D tex = this.Content.Load<Texture2D>("images/background");
			Rectangle srcRect = new Rectangle(0, 0, tex.Width, tex.Height);

			Vector2 position = new Vector2(0, _graphics.PreferredBackBufferHeight - srcRect.Height);
			Vector2 speed = new Vector2(2, 0);

			ScrollingBackground sb = new ScrollingBackground(this, _spriteBatch, tex, srcRect, position, speed);

			Song backgroundMusic = this.Content.Load<Song>("sounds/menutheme");
			MediaPlayer.IsRepeating = true;
			MediaPlayer.Play(backgroundMusic);

			this.Components.Add(sb);
			startScene = new StartScene(this);
            this.Components.Add(startScene);
            startScene.show();

            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);

            highScoreScene = new HighScoreScene(this);
            this.Components.Add(highScoreScene);

            aboutScene = new AboutScene(this);
            this.Components.Add(aboutScene);

            actionScene = new ActionScene(this);
            this.Components.Add(actionScene);


        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();
            if (startScene.Enabled)
            {
                GameEndScene.pressedEnter = false;
                GameEndScene.playerName = "";
                selectedIndex = startScene.Menu.selectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    actionScene.show();
                }
                else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    helpScene.show();
                }
                else if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    highScoreScene.show();
                }
                else if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    aboutScene.show();
                }
                else if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }
            if (helpScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    startScene.show();
                }
            }
            if (actionScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    
                    startScene.show();
                }
            }
            if (highScoreScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    startScene.show();
                }
            }
            if (aboutScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {

                    hideAllScenes();
                    startScene.show();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}