using Microsoft.AspNetCore.Authorization;
using Volo.Abp.AspNetCore.SignalR;

namespace Chaos.Todos;

[Authorize]
public class TodoHub : AbpHub
{
}
