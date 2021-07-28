using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StoreAppModel;

namespace StoreAppWebUI.Models
{
    public class ManagerVM
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public int StoreFrontID { get; set; }

        
        public ManagerVM()
        { }

        public ManagerVM(Manager p_manager)
        {
            ID = p_manager.ManagerID;
            Name = p_manager.ManagerName;
            Username = p_manager.ManagerUsername;
            StoreFrontID = p_manager.StoreFront.StoreFrontID;
        }
    }
}