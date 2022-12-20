using NP.DependencyInjection.Attributes;
using NP.Utilities;
using NP.Utilities.PluginUtils;
using System.Xml.Serialization;

namespace SecurityTestViewModelPlugin;

[RegisterType(typeof(IPlugin), resolutionKey: nameof(SecurityTestViewModel), isSingleton: true)]
public class SecurityTestViewModel : VMBase, IPlugin
{
    [XmlAttribute]
    public string? Symbol { get; set; }

    [XmlAttribute]
    public string? Description { get; set; }

    [XmlAttribute]
    public decimal Ask { get; set; }

    [XmlAttribute]
    public decimal Bid { get; set; }

    public override string ToString()
    {
        return $"StockViewModel: Symbol={Symbol}, Ask={Ask}";
    }
}