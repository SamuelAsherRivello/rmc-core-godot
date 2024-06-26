using Godot;
using RMC.Core.Events;


namespace RMC.Core.Templates
{
	/// <summary>
	/// TODO: Add comments
	/// </summary>
	public partial class TemplateNode : Node
	{
		//  Events ----------------------------------------
		public readonly RmcEvent OnThingCompleted = new RmcEvent();
        
		
		//  Properties ------------------------------------
		public string SampleMessage { get { return _sampleMessage;} }
        
		
		//  Fields ----------------------------------------
		private string _sampleMessage = "";
        
		
		//  Initialization  -------------------------------
		public TemplateNode()
		{
			
		}
        
		
		//  Godot Methods ---------------------------------
		/// <summary>
		/// Called when the node enters the scene tree for the first time.
		/// </summary>
		public override void _Ready()
		{
		}

        
		/// <summary>
		/// Called every frame. 'delta' is the elapsed time since the previous frame.
		/// </summary>
		public override void _Process(double delta)
		{
		}
		
		
		/// <summary>
		/// Called when the node is about to leave the SceneTree
		/// </summary>
		public override void _ExitTree()  
		{
		}

		
		//  Methods ---------------------------------------
		public string SamplePublicMethod (string message) 
		{
			return message;
		}
        
		
		//  Event Handlers --------------------------------
		private void Target_OnActionCompleted(string message) 
		{
			OnThingCompleted.Invoke();
		}
	}
}