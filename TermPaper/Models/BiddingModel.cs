﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TermPaper.Models
{
    public class BiddingModel
    {
        public int Id { get; set; }

        public decimal Sum { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public int LotId { get; set; }
    }
}