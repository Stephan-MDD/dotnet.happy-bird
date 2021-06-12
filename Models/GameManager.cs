using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet.flappy_bird.Models {
    public class GameManager {
        private readonly int Gravity = 3;
        public readonly int SceneWidth = 500;
        public readonly int SceneHeight = 720;
        public readonly int GroundHeight = 150;
        public double Points { get; set; }
        public double HighScore { get; set; }
        public bool DisplayWelcomeText { get; set; } = true;

        public event EventHandler MainLoopCompleted;

        public BirdModel Bird { get; set; }
        public List<PipeModel> Pipes { get; set; }
        public UIControls UIControls { get; set; }


        public bool IsRunning { get; set; } = false;

        public GameManager() {
            Bird = new BirdModel();
            Pipes = new List<PipeModel>();
            UIControls = new UIControls();
        }

        public async void MainLoop() {
            while (IsRunning) {
                MoveObjects();
                CheckForCollisions();
                ManagePipes();


                MainLoopCompleted?.Invoke(this, EventArgs.Empty);
                await Task.Delay(20);
            }
        }

        public void MoveObjects() {
            Bird.Fall(Gravity);
            foreach (var pipe in Pipes) {
                pipe.Move();
            }
        }

        public void CheckForCollisions() {
            if (Bird.IsOnGround())
                GameOver();

            var centerPipe = Pipes.FirstOrDefault((p) => p.IsInCollisionZone(this));

            if (centerPipe != null) {
                bool bottomColision = Bird.DistanceFromGround < centerPipe.GabBottom - GroundHeight;
                bool topColision = Bird.DistanceFromGround + Bird.Height > centerPipe.GabTop - GroundHeight;

                if (bottomColision || topColision)
                    GameOver();
            }

            Points += 0.2;
        }

        public void ManagePipes() {
            if (!Pipes.Any() || Pipes.Last().DistanceFromLeft <= 250)
                Pipes.Add(new PipeModel());

            if (Pipes.First().IsOffScreen())
                Pipes.Remove(Pipes.First());
        }

        public void StartGame() {
            Points = 0;
            DisplayWelcomeText = false;

            if (!IsRunning) {
                UIControls.HideGameOverText();
                IsRunning = true;
                Bird = new BirdModel();
                Pipes = new List<PipeModel>();
                MainLoop();
            }
        }

        public void GameOver() {
            IsRunning = false;
            UIControls.ShowGameOverText();

            if (HighScore < Points) {
                HighScore = Points;
            }
        }

        public void Jump() {
            if (IsRunning) {
                Bird.Jump();
            }
        }
    }
}