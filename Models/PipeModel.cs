using System;

namespace dotnet.flappy_bird.Models {
    public class PipeModel {
        public int DistanceFromLeft { get; set; } = 550;
        public int DistanceFromBottom { get; set; } = new Random().Next(0, 90);
        public int Speed { get; set; } = 2;
        public int Gap { get; set; } = 130;

        public readonly int Height = 300;
        public readonly int Width = 60;

        public int GabBottom => DistanceFromBottom + Height;
        public int GabTop => GabBottom + Gap;

        public bool IsInCollisionZone(GameManager gm) {
            bool hasEnteredCollisionZone = DistanceFromLeft <= (gm.SceneWidth / 2) + (gm.Bird.Width / 2);
            bool hasLeftCollisionZone = DistanceFromLeft <= (gm.SceneWidth / 2) - (gm.Bird.Width / 2) - Width;

            return hasEnteredCollisionZone && !hasLeftCollisionZone;
        }

        public void Move() {
            DistanceFromLeft -= Speed;
        }

        public bool IsOffScreen() {
            return DistanceFromLeft <= -Width;
        }
    }
}