using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;

namespace InventoryMgmt.Model
{
    public class Notification : HubMessageDTO
    {
        [Key]
        public int Id {get; set;}
        
    }
}
