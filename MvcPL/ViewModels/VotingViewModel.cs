using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface.Entities;

namespace MvcPL.ViewModels
{
    public class VotingViewModel
    {
        public int UserId { get; set; }
        public int PhotoId { get; set; }
        public int Rating { get; set; }
    }
}