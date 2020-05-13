using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class MessageHub : Hub
    {
        public async Task NewMessage(Message msg) {
        
            await Clients.All.SendAsync("MessageReceived", msg);
    }

        
    }
}

public class Message
{
    public string clientuniqueid { get; set; }
    public string type { get; set; }
    public string message { get; set; }
    public DateTime date { get; set; }


    //my
    public int Id { get; set; }
    public int VendorId { get; set; }
    public int CustomerId { get; set; }
    public string IpAddress { get; set; }
    public int ProductId { get; set; }
    public int ProductVariantDetailId { get; set; }
    public string CustomerMsg { get; set; }
    public string VendorMsg { get; set; }
    public int SenderId { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public bool IsActive { get; set; }
}

public class getOldMsgModel
{
    public int CustomerId { get; set; }
    public int VendorId { get; set; }
    public string IpAddress { get; set; }
    public int ProductId { get; set; }
    public int ProductVariantDetailId { get; set; }
}

public class OldMessage
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string VendorName { get; set; }
    public int VendorId { get; set; }
    public string IpAddress { get; set; }
    public string CustomerMsg { get; set; }
    public string VendorMsg { get; set; }
    public int ProductVariantDetailId { get; set; }
    public string ProductName { get; set; }
    public string ProductImage { get; set; }
    public DateTime Date { get; set; }
}

public class AllChats
{
    public int CustomerId { get; set; }
    public int VendorId { get; set; }
    public string VendorName { get; set; }
    public string CustomerName { get; set; }
    public string LastMsg { get; set; }
    public DateTime Date { get; set; }
    public int ProductVariantDetailId { get; set; }
    public int ProductId { get; set; }
    public string ProductImage { get; set; }
}