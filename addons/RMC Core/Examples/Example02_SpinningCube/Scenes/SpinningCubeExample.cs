using Godot;

namespace RMC.Core.Examples
{
    /// <summary>
    /// Demo of Spinning a Cube
    /// </summary>
    public partial class SpinningCubeExample : Node3D
    {
        //  Fields ----------------------------------------
        [Export]
        private CsgBox3D _cube = null;

        [Export] 
        private float _speed = 1;
        
        //  Godot Methods ---------------------------------
		
        public override void _Ready()
        {
            GD.Print("SpinningCubeExample._Ready()");
        }

        public override void _Process(double delta)
        {
            _cube.RotateY(_speed * (float) delta);
        }
        
        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
    }
}