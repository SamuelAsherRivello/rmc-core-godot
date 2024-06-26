using RMC.Core.Events;

namespace RMC.Core.Templates
{
	/// <summary>
	/// TODO: Add comments
	/// </summary>
	public partial class TemplateClass 
	{
		//  Events ----------------------------------------
		public readonly RmcEvent OnThingCompleted = new RmcEvent();
        
		
		//  Properties ------------------------------------
		public string SampleMessage { get { return _sampleMessage;} }
        
		
		//  Fields ----------------------------------------
		private string _sampleMessage = "";
        
		
		//  Initialization  -------------------------------
		public TemplateClass()
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