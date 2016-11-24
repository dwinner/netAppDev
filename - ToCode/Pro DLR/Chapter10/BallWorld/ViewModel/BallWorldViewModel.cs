using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Collections.ObjectModel;
using Microsoft.Scripting.Hosting;
using FarseerGames.FarseerPhysics.Mathematics;
using BallWorld.PhysicsEngine;

namespace BallWorld.ViewModel
{
    public class BallWorldViewModel
    {
        private BallWorldPhysicsEngine physicsEngine = new BallWorldPhysicsEngine();
        private ObservableCollection<BallViewModel> balls;
        
        public BallWorldViewModel()
        {
            balls = new ObservableCollection<BallViewModel>();
            BorderViewModel border = new BorderViewModel(720f, 520f, 60, new Vector2(400f, 300f));
            physicsEngine.AddBorder(border);
            physicsEngine.Start();
            RunInitScript();
        }

        public ObservableCollection<BallViewModel> Balls
        {
            get { return balls; }
        }

        public void AddBall(Color color, float radius, float x, float y, float speedx, float speedy)
        {
            BallViewModel ball = new BallViewModel(color, radius, 
                new Vector2D(x, y), new Vector2D(speedx, speedy));
            balls.Add(ball);
            physicsEngine.AddBall(ball);
        }

        private void RunInitScript()
        {
            ScriptRuntime scriptRuntime = ScriptRuntime.CreateFromConfiguration();
            scriptRuntime.Globals.SetVariable("world", this);
            ScriptScope scope = scriptRuntime.ExecuteFile(@"Script\InitScript.py");
        }
    }
}
