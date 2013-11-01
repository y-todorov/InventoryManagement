using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace InventoryManagementMVC.Hubs
{
    //[HubName("rebindHub")]
    public class RebindHub : Hub
    {
        //[HubMethodName("RebindGrids")]
        public void RebindGrids()
        {
            Clients.Others.rebindGrids();
        }

        public void Hello()
        {
            Clients.All.hello();
        }
    }
}