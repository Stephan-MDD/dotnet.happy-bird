using System;

namespace dotnet.flappy_bird.Models {
    public class UIControls {
        public int GameOverTextOpacity { get; set; } = 0;
        public double GameOverTextSize { get; set; } = 3;

        public void HideGameOverText() {
            GameOverTextOpacity = 0;
            GameOverTextSize = 0.6;
        }

        public void ShowGameOverText() {
            GameOverTextOpacity = 1;
            GameOverTextSize = 1.6;
        }

    }
}