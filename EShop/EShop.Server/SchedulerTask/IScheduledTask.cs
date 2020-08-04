using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


/// <summary>
/// Schedule format
/// </summary>
//*    *    *    *    *  
//┬    ┬    ┬    ┬    ┬
//│    │    │    │    │
//│    │    │    │    │
//│    │    │    │    └───── day of week(0 - 6) (Sunday=0 )
//│    │    │    └────────── month(1 - 12)
//│    │    └─────────────── day of month(1 - 31)
//│    └──────────────────── hour(0 - 23)
//└───────────────────────── min (0 - 59)
namespace EShop.Server.SchedulerTask
{
    public interface IScheduledTask
    {
        // time span per execute update
        string Schedule { get; }
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
