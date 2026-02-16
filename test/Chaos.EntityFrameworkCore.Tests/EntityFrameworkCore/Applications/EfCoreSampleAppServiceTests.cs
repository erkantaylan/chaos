using Chaos.Samples;
using Xunit;

namespace Chaos.EntityFrameworkCore.Applications;

[Collection(ChaosTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<ChaosEntityFrameworkCoreTestModule>
{

}
