using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;

namespace undertal;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    bool isBroken;

    Texture2D peter;

    Texture2D blue;

    Texture2D pixel;

    Texture2D broken;

    Texture2D heart;

    SpriteFont font;

    Song theme;

    Song destroyed;

    Song ending;

    DateTime starttime;



    Rectangle fiende = new Rectangle(300,30,200,200);

    Rectangle player = new Rectangle(400,370,20,20);

    Rectangle flame = new Rectangle (390,30,30,40);

    Rectangle ball = new Rectangle(395,350,10,10);
    Rectangle ball2 = new Rectangle(395,370,10,10);
    Rectangle ball3 = new Rectangle(395,350,10,10);

    Rectangle övreGräns = new Rectangle(350,300,150,10);

    Rectangle undreGräns = new Rectangle(350,450,150,10);

    Rectangle västraGräns = new Rectangle(350,300,10,150);

    Rectangle östraGräns = new Rectangle (500,300,10,160);

    int heartSpeedY = 3;

    int heartSpeedX = 3;

    int ballSpeedY = -3;
    int ballSpeedX = -3;

    int ballSpeedY2 = 3;
    int ballSpeedX2 = 3;
    bool playedending = false;

    string wow ="It's a beautiful day outside. Birds are singing, ";
    string wow1 ="flowers are blooming. . . On days like these, kids like you. . . ";
    string wow2 ="Should be burning in hell. ";

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        starttime = DateTime.UtcNow;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        peter = Content.Load<Texture2D>("peter");
        pixel = Content.Load<Texture2D>("pixel");
        heart = Content.Load<Texture2D>("heart");
        broken = Content.Load<Texture2D>("broken");
        font = Content.Load<SpriteFont>("file");
        blue = Content.Load<Texture2D>("blue");
        theme = Content.Load<Song>("Megalovania");
        destroyed = Content.Load<Song>("Shatter");
        ending = Content.Load<Song>("Determination");
        MediaPlayer.Play(theme);
        

        

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (isBroken && !playedending)
        {
            MediaPlayer.Play(theme);
            MediaPlayer.Play(destroyed);
            MediaPlayer.Play(ending);
            playedending = true;
        }

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

            KeyboardState kState = Keyboard.GetState();

              if(kState.IsKeyDown(Keys.W) && player.Y >= 310)
            player.Y-= heartSpeedY;
            
            if (kState.IsKeyDown(Keys.S) && player.Y + player.Height <450)
            player.Y+= heartSpeedY;

            if(kState.IsKeyDown(Keys.A) && player.X >= 360)
            player.X-= heartSpeedX;
            
            if (kState.IsKeyDown(Keys.D) && player.X <= 480)
            player.X+= heartSpeedX;


var rand = new Random();

             ball.Y += ballSpeedY * rand.Next(1,2);
            ball.X += ballSpeedX * rand.Next(1,2);

            if (ball.Y <= 0 || (ball.Y+ball.Height >= 480))
            ballSpeedY *= -1;

            if(ball.X <= 0)
            {
                ball.X = 395;
                ball.Y = 235;
                ballSpeedX = -4;
                //ball.Location = new Point(395,235);
            }

            if (ball.X+ball.Height >= 800)
        {
            ball.X = 395;
            ball.Y = 235;
            ballSpeedX = -4;
        }
            

            if (västraGräns.Intersects(ball))
            ballSpeedX *= -1;
            

           if (östraGräns.Intersects(ball))
            ballSpeedX *= -1;
            

           if (övreGräns.Intersects(ball))
           ballSpeedY *= -1;
            

           if (undreGräns.Intersects(ball))
           ballSpeedY *= -1;
            

            if (player.Intersects(ball))
            isBroken = true;






             ball2.Y += ballSpeedY2 * rand.Next(1,2);
            ball2.X += ballSpeedX2 * rand.Next(1,2);

            if (ball2.Y <= 0 || (ball2.Y+ball2.Height >= 480))
            ballSpeedY2 *= -1;

            if(ball2.X <= 0)
            {
                ball2.X = 395;
                ball2.Y = 235;
                ballSpeedX2 = -4;
                //ball.Location = new Point(395,235);
            }

            if (ball2.X+ball2.Height >= 800)
        {
            ball2.X = 395;
            ball2.Y = 235;
            ballSpeedX2 = -4;
        }

        long elapsedTicks = DateTime.Now.Ticks - starttime.Ticks;
        TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
        if (elapsedSpan.Seconds > 5)
        {
                   if (player.Intersects(ball2))
                   isBroken = true;
        }
            

            if (västraGräns.Intersects(ball2))
            ballSpeedX2 *= -1;
            

           if (östraGräns.Intersects(ball2))
            ballSpeedX2 *= -1;
            

           if (övreGräns.Intersects(ball2))
           ballSpeedY2 *= -1;
            

           if (undreGräns.Intersects(ball2))
           ballSpeedY2 *= -1;
            




        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here

        base.Draw(gameTime);

        _spriteBatch.Begin();
        if (isBroken)
        {
            _spriteBatch.Draw(broken, player, Color.White);
        }
        else
        {
            _spriteBatch.Draw(heart, player, Color.White);
        }
    
        _spriteBatch.Draw(peter, fiende, Color.White);
        _spriteBatch.Draw(pixel, övreGräns, Color.White);
        _spriteBatch.Draw(pixel, undreGräns, Color.White);
        _spriteBatch.Draw(pixel, västraGräns, Color.White);
        _spriteBatch.Draw(pixel, östraGräns, Color.White);
        _spriteBatch.Draw(blue, flame, Color.White);
        _spriteBatch.Draw(pixel, ball, Color.White);
        long elapsedTicks = DateTime.Now.Ticks - starttime.Ticks;
        TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
        if (elapsedSpan.Seconds > 5)
        {
            _spriteBatch.Draw(pixel, ball2, Color.White);
        }

        _spriteBatch.DrawString(font, wow, new Vector2(450,40), Color.White);
        _spriteBatch.DrawString(font, wow1, new Vector2(450,55), Color.White);
        _spriteBatch.DrawString(font, wow2, new Vector2(450,70), Color.White);
        _spriteBatch.End();
    }
}
