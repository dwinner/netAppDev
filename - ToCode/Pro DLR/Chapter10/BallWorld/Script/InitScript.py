import clr
import world
clr.AddReference("PresentationCore")
from System.Windows.Media import (Colors)

world.AddBall(Colors.Blue, 50, 250, 200, -50, 50);
world.AddBall(Colors.Red, 30, 200, 400, 50, 50);
world.AddBall(Colors.Black, 30, 100, 400, 20, 40);
world.AddBall(Colors.Green, 40, 250, 300, -30, 50);
world.AddBall(Colors.Purple, 35, 320, 270, 40, -40);
