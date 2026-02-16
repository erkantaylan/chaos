using Chaos.Samples;
using Xunit;

namespace Chaos.EntityFrameworkCore.Domains;

[Collection(ChaosTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<ChaosEntityFrameworkCoreTestModule>
{

}
