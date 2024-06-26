using Godot;

namespace RMC.Core.Debug
{
    /// <summary>
    /// Demo of <see cref="Logger"/>
    /// </summary>
    public partial class LoggerExample : Node
    {
        //  Fields ----------------------------------------
        
        //  Godot Methods ---------------------------------
		
        public override void _Ready()
        {
            ILogger logger = new Logger(true) { Prefix = "[LoggerExample]", Suffix = "...!"};

            logger.PrintEmptyLine();
            logger.PrintHeader("Print");
            logger.Print("hello world 1");
            logger.Print("hello world 2");
            
            logger.PrintEmptyLine();
            logger.PrintHeader("PrintErr");
            logger.PrintErr("Just testing this red text :) ");
        }


        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
    }
}