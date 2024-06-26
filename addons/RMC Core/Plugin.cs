#if TOOLS
using Godot;
using RMC.Core.Debug;

namespace RMC.Core
{
    [Tool]
    public partial class Plugin : EditorPlugin
    {
        private const string PluginName = "RMC Core";
        private const bool IsLoggerEnabled = true;
        private static readonly ILogger _logger = new Logger(IsLoggerEnabled) { Prefix = $"[{PluginName}]" };
        private GdUnit4PluginBridge _pluginBridge;

        public override void _EnterTree()
        {
            _logger.PrintEmptyLine();
            _logger.PrintHeader(PluginName);
            _logger.Print($"Plugin enabled.");
            _logger.PrintDivider();

            _pluginBridge = new GdUnit4PluginBridge(_logger, this);
            //_pluginBridge.Add();
        }

        public override void _ExitTree()
        {
            //_pluginBridge.Remove();

            _logger.PrintEmptyLine();
            _logger.PrintHeader(PluginName);
            _logger.Print($"Plugin disabled.");
            _logger.PrintDivider();
        }
    }

    public class PluginBridge
    {
        protected readonly ILogger _logger;
        protected readonly EditorPlugin _parentPlugin;
        protected EditorPlugin _pluginInstance;

        public PluginBridge(ILogger logger, EditorPlugin parentPlugin)
        {
            _logger = logger;
            _parentPlugin = parentPlugin;
        }

        public virtual void Add()
        {
            // To be overridden by child classes
        }

        public virtual void Remove()
        {
            // To be overridden by child classes
        }

        protected void LoadPlugin(string pluginPath)
        {
            var gdScript = GD.Load<GDScript>(pluginPath);
            if (gdScript == null)
            {
                _logger.PrintErr($"Failed to load plugin script at {pluginPath}.");
                return;
            }

            _pluginInstance = (EditorPlugin)gdScript.New();
            if (_pluginInstance == null)
            {
                _logger.PrintErr($"Failed to instantiate plugin at {pluginPath}.");
                return;
            }

            _parentPlugin.AddChild(_pluginInstance);
            _pluginInstance.Call("_enter_tree");
            _logger.Print($"Plugin loaded successfully from {pluginPath}.");
        }

        protected void UnloadPlugin()
        {
            if (_pluginInstance != null && _pluginInstance.IsInsideTree())
            {
                _pluginInstance.Call("_exit_tree");
                _pluginInstance.QueueFree();
                _pluginInstance = null;
                _logger.Print("Plugin unloaded successfully.");
            }
        }
    }

    public class GdUnit4PluginBridge : PluginBridge
    {
        private const string GdUnitPluginPath = "res://addons/gdUnit4/plugin.gd";

        public GdUnit4PluginBridge(ILogger logger, EditorPlugin parentPlugin) : base(logger, parentPlugin) { }

        public override void Add()
        {
            _logger.Print("Loading GdUnit4 Plugin...");
            LoadPlugin(GdUnitPluginPath);
        }

        public override void Remove()
        {
            _logger.Print("Unloading GdUnit4 Plugin...");
            UnloadPlugin();
        }
    }
}
#endif
