#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

#endregion

namespace angry_bird
{


	public class MainGame : Game

	{

		#region Mrgn

		List<Start>_strLst;
		List<Pig> _pigLst,_newPigLst;
		Bird _brd;
		Bird.Firer _firer;
		SpriteFont _fnt;
		private void DrawStartGame ()
		{

	
			//draw pig
			foreach (var item in _pigLst) {
				item.Draw ();
				
	
				//draw bird
		//if(!_brdLst [Bird.Index].InterSected)
				_brd .Draw ();	

				//draw firer
				_firer.Draw ();
			}
		}

		private void InitGame ()
		{
			//create onjects of lists
			_strLst =new List<Start>();
			_pigLst= new List<Pig> ();
			_newPigLst = new List<Pig> ();

			//------------------------------------
			//------------------------------------
			//fill lists
			_strLst.Add (new Start(graphics,spriteBatch,Content.Load<Texture2D> ("img/strt/bak1"),Content.Load<Song>("snd/main")));
			//------------------------------------_
			_pigLst.Add (new Pig(Content.Load<Texture2D>("img/pig/pig1"),new Vector2(0,0),3,Content.Load<SoundEffect>("snd/pig")));
			_pigLst.Add (new Pig(Content.Load<Texture2D>("img/pig/pig2"),new Vector2(0,0),3,Content.Load<SoundEffect>("snd/pig")));
			_pigLst.Add (new Pig(Content.Load<Texture2D>("img/pig/pig0"),new Vector2(0,0),3,Content.Load<SoundEffect>("snd/pig")));
			_pigLst.Add (new Pig(Content.Load<Texture2D>("img/pig/pig1"),new Vector2(0,0),3,Content.Load<SoundEffect>("snd/pig")));
			//_pigLst.Add (new Pig(Content.Load<Texture2D>("img/pig/pig2"),new Vector2(0,0),3,Content.Load<SoundEffect>("snd/pig")));			
			//_pigLst.Add (new Pig(Content.Load<Texture2D>("img/pig/pig0"),new Vector2(0,0),3,Content.Load<SoundEffect>("snd/pig")));			
		
			
			//------------------------------------_
			_firer=new Bird.Firer(Content.Load<Texture2D>("img/firer/N"));

			_brd=new Bird(Content.Load<Texture2D>("img/brd/red"),_firer,2,Content.Load<SoundEffect>("snd/brd_kill"));
			//------------------------------------
		
		}
		private void Reset(){
		
			Bird.Reset ();
			InitGame ();
		}
		#endregion
	
		#region pg
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public MainGame ()
		{

			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";	
			this.Window.AllowUserResizing = true;
			graphics.PreferredBackBufferHeight =500;
			graphics.PreferredBackBufferWidth = 800;
			graphics.ApplyChanges ();
			//graphics.IsFullScreen = true;		

		}


		protected override void Initialize ()
		{
			base.Initialize ();
				
		}

		protected override void LoadContent ()
		{

			spriteBatch = new SpriteBatch (GraphicsDevice);
			this.InitGame();

		}

	

		protected override void Update (GameTime gameTime)
		{
			if (!Start.Started && Keyboard.GetState ().IsKeyDown (Keys.Enter)) {
				Start.Started = true;
				_strLst[0] .StartGame (Content.Load<Texture2D> ("img/lvl/angry_birds_background_by_gsgill37-d3kogmx"), Content.Load<Song> ("snd/playing"));
			
			}
			if (Start.Started) {
			
				
				_brd .UpDate ();
				_newPigLst.Clear();
				for (int i = 0; i < _pigLst.Count; i++) {

					if(_pigLst [i].InterSect (_brd )&&!_brd.InterSected){
					}
						_newPigLst.Add (_pigLst [i]);
					//last item of pig
					if (i == _pigLst.Count - 1) {
						_pigLst [i].UpDate ();
						break;
					}

					_pigLst [i].UpDate ();
				
				}

				foreach (var item in _newPigLst) {
					if (item.Died){
						_pigLst.Remove (item);
					
					}
				}

				if(_pigLst.Count<=0){
					Start.Started=false;
					this.Reset ();
				}
				
			
	
			}

			base.Update (gameTime);
		}


		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);


			//draw back at start at begin game
			foreach (var item in _strLst) {
				item.Draw ();
			}

			//check if started
			if (Start.Started) {
				this.DrawStartGame ();

			}
			base.Draw (gameTime);
		}
			}
		#endregion

}	





