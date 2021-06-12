using System;

namespace dotnet.flappy_bird.Models {
    public class BirdModel {
        public int DistanceFromGround { get; set; } = 100;
        public int JumpStrength { get; set; } = 40;
        public readonly int Height = 48;
        public readonly int Width = 48;
        public int Angle { get; set; } = 0;

        public void Fall(int gravity) {
            Angle = Math.Min(Angle + 1, 45);
            DistanceFromGround -= Math.Min(DistanceFromGround, gravity);
        }

        public void Jump() {
            Angle = 0;

            if (DistanceFromGround <= 400) {
                DistanceFromGround += JumpStrength;
            }
        }

        public bool IsOnGround() {
            return DistanceFromGround <= 0;
        }
    }
}