﻿using System;
using System.Collections.Generic;
using AvaloniaChat.Data.Entities;
using AvaloniaChat.Data.Models;

namespace AvaloniaChat.Domain.Models
{
    public partial class Message
    {
        public Guid MessageId { get; set; }
        public Guid GroupId { get; set; }
        public string MessageText { get; set; } = null!;
        public bool IsRead { get; set; }
        public int UserId { get; set; }

        public DateTime SendDate { get; set; }
        public virtual Group Group { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
