using Xunit;

namespace Chaos.EntityFrameworkCore;

[CollectionDefinition(ChaosTestConsts.CollectionDefinitionName)]
public class ChaosEntityFrameworkCoreCollection : ICollectionFixture<ChaosEntityFrameworkCoreFixture>
{

}
