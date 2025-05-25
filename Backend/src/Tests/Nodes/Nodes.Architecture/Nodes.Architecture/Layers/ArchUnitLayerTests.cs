using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using Xunit;

namespace Nodes.Architecture.Layers;

public class ArchUnitLayerTests : ArchUnitBaseTests
{
    public static IEnumerable<object[]> LayerDependencyRules => new List<object[]>
    {
        new object[] { DomainLayer, ApplicationLayer },
        new object[] { DomainLayer, InfrastructureLayer },
        new object[] { ApplicationLayer, InfrastructureLayer },
        new object[] { ApplicationLayer, ApiLayer },
        new object[] { InfrastructureLayer, ApiLayer }
    };

    [Theory]
    [MemberData(nameof(LayerDependencyRules))]
    public void SourceLayer_Should_NotHaveDependencyOnForbiddenLayer(IObjectProvider<IType> sourceLayer, IObjectProvider<IType> forbiddenDependency)
    {
        ArchRuleDefinition
            .Types()
            .That()
            .Are(sourceLayer)
            .Should()
            .NotDependOnAny(forbiddenDependency)
            .Check(Architecture);
    }
}
